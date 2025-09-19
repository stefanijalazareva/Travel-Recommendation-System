using System.Text.Json;
using TravelRecommendationSystem.Models;
using TravelRecommendationSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace TravelRecommendationSystem.Services
{
    public interface IDestinationApiService
    {
        Task<List<ApiDestinationData>> FetchCitiesFromApiAsync(string countryName, int limit = 10);
        Task<List<ApiDestinationData>> FetchPlacesFromGeoapifyAsync(string query, int limit = 10);
        Task<int> ImportDestinationsFromApiAsync(List<string> countries, int maxPerCountry = 5);
    }

    public class DestinationApiService : IDestinationApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DestinationApiService> _logger;

        // API Keys - In production, these should come from configuration/environment variables
        private readonly string _apiNinjasKey = "FREE_TRIAL"; // API-Ninjas allows free requests without key for testing
        private readonly string _geoapifyKey = "FREE_TRIAL"; // Geoapify has a free tier for testing

        public DestinationApiService(HttpClient httpClient, ApplicationDbContext context, ILogger<DestinationApiService> logger)
        {
            _httpClient = httpClient;
            _context = context;
            _logger = logger;
        }

        public async Task<List<ApiDestinationData>> FetchCitiesFromApiAsync(string countryName, int limit = 10)
        {
            try
            {
                // For demo purposes, we'll simulate API responses with predefined data
                // In production with actual API keys, replace with real API calls
                var mockCities = GetMockCityData(countryName, limit);
                if (mockCities.Any())
                {
                    _logger.LogInformation($"Returning mock data for {countryName}");
                    return mockCities;
                }

                // If API keys are properly configured, use real API
                if (_apiNinjasKey != "FREE_TRIAL")
                {
                    var url = $"https://api.api-ninjas.com/v1/city?country={Uri.EscapeDataString(countryName)}&limit={limit}";
                    
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _apiNinjasKey);

                    var response = await _httpClient.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError($"API-Ninjas API returned {response.StatusCode} for country {countryName}");
                        return new List<ApiDestinationData>();
                    }

                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var cities = JsonSerializer.Deserialize<List<CityApiResponse>>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (cities == null) return new List<ApiDestinationData>();

                    return cities.Select(city => new ApiDestinationData
                    {
                        Name = city.Name,
                        Country = city.Country,
                        Region = "", // API-Ninjas doesn't provide region
                        Latitude = city.Latitude,
                        Longitude = city.Longitude,
                        Population = city.Population,
                        IsCapital = city.IsCapital,
                        Description = GenerateDescription(city.Name, city.Country, city.IsCapital, city.Population),
                        Tags = GenerateTags(city.Name, city.Country, city.IsCapital, city.Population)
                    }).ToList();
                }

                return new List<ApiDestinationData>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching cities from API-Ninjas for country {countryName}");
                return new List<ApiDestinationData>();
            }
        }

        public async Task<List<ApiDestinationData>> FetchPlacesFromGeoapifyAsync(string query, int limit = 10)
        {
            try
            {
                var url = $"https://api.geoapify.com/v2/places?categories=tourism&filter=countrycode:none&limit={limit}&apiKey={_geoapifyKey}&text={Uri.EscapeDataString(query)}";
                
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Geoapify API returned {response.StatusCode} for query {query}");
                    return new List<ApiDestinationData>();
                }

                var jsonContent = await response.Content.ReadAsStringAsync();
                var placesResponse = JsonSerializer.Deserialize<GeoapifyResponse>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (placesResponse?.Features == null) return new List<ApiDestinationData>();

                return placesResponse.Features.Select(feature => new ApiDestinationData
                {
                    Name = feature.Properties.Name,
                    Country = feature.Properties.Country,
                    Region = feature.Properties.State,
                    Latitude = feature.Geometry.Coordinates.Count > 1 ? feature.Geometry.Coordinates[1] : 0,
                    Longitude = feature.Geometry.Coordinates.Count > 0 ? feature.Geometry.Coordinates[0] : 0,
                    Categories = feature.Properties.Categories,
                    Description = GenerateDescriptionFromCategories(feature.Properties.Name, feature.Properties.Country, feature.Properties.Categories),
                    Tags = GenerateTagsFromCategories(feature.Properties.Categories)
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching places from Geoapify for query {query}");
                return new List<ApiDestinationData>();
            }
        }

        public async Task<int> ImportDestinationsFromApiAsync(List<string> countries, int maxPerCountry = 5)
        {
            int importedCount = 0;
            int currentMaxId = await _context.Destinations.AnyAsync() ? await _context.Destinations.MaxAsync(d => d.Id) : 20;

            foreach (var country in countries)
            {
                try
                {
                    // Check if we already have destinations for this country
                    var existingCount = await _context.Destinations.CountAsync(d => d.Country.ToLower() == country.ToLower());
                    if (existingCount >= maxPerCountry)
                    {
                        _logger.LogInformation($"Skipping {country} - already has {existingCount} destinations");
                        continue;
                    }

                    var citiesFromApi = await FetchCitiesFromApiAsync(country, maxPerCountry);
                    
                    foreach (var cityData in citiesFromApi.Take(maxPerCountry))
                    {
                        // Check if destination already exists
                        var exists = await _context.Destinations.AnyAsync(d => 
                            d.Name.ToLower() == cityData.Name.ToLower() && 
                            d.Country.ToLower() == cityData.Country.ToLower());
                            
                        if (exists) continue;

                        currentMaxId++;
                        
                        var destination = new Destination
                        {
                            Id = currentMaxId,
                            Name = cityData.Name,
                            Country = cityData.Country,
                            Region = cityData.Region,
                            Description = cityData.Description,
                            ImageUrl = GetDefaultImageUrl(cityData.Name),
                            Latitude = cityData.Latitude,
                            Longitude = cityData.Longitude,
                            Climate = DetermineClimate(cityData.Latitude),
                            BestTimeToVisit = DetermineBestTimeToVisit(cityData.Latitude),
                            AveragePriceLevel = DeterminePriceLevel(cityData.Country, cityData.IsCapital),
                            AverageRating = 4.0m + (decimal)(new Random().NextDouble() * 0.8), // Random between 4.0-4.8
                            TotalReviews = new Random().Next(100, 1000),
                            IsFeatured = cityData.IsCapital || cityData.Population > 1000000,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };

                        _context.Destinations.Add(destination);
                        
                        // Add tags for this destination
                        int tagId = await _context.DestinationTags.AnyAsync() ? await _context.DestinationTags.MaxAsync(t => t.Id) + 1 : 101;
                        foreach (var tag in cityData.Tags.Take(5))
                        {
                            _context.DestinationTags.Add(new DestinationTag
                            {
                                Id = tagId++,
                                DestinationId = currentMaxId,
                                TagName = tag
                            });
                        }

                        importedCount++;
                    }

                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Imported destinations for {country}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error importing destinations for {country}");
                }
            }

            return importedCount;
        }

        private string GenerateDescription(string cityName, string country, bool isCapital, long population)
        {
            var descriptions = new List<string>();
            
            if (isCapital)
            {
                descriptions.Add($"The capital city of {country}");
            }
            else
            {
                descriptions.Add($"A beautiful city in {country}");
            }

            if (population > 5000000)
            {
                descriptions.Add("a major metropolitan area with rich culture and attractions");
            }
            else if (population > 1000000)
            {
                descriptions.Add("a vibrant urban center with diverse attractions");
            }
            else if (population > 100000)
            {
                descriptions.Add("a charming city with local culture and attractions");
            }
            else
            {
                descriptions.Add("a peaceful destination with authentic local experiences");
            }

            return string.Join(", ", descriptions) + $". {cityName} offers visitors a unique blend of local culture, history, and modern amenities.";
        }

        private string GenerateDescriptionFromCategories(string placeName, string country, List<string> categories)
        {
            var categoryDescriptions = categories.Select(cat => cat switch
            {
                "tourism" => "a popular tourist destination",
                "accommodation" => "excellent accommodation options",
                "entertainment" => "vibrant entertainment scene",
                "natural" => "beautiful natural attractions",
                "heritage" => "rich historical heritage",
                _ => "unique attractions"
            });

            return $"{placeName} in {country} offers {string.Join(", ", categoryDescriptions)}.";
        }

        private List<string> GenerateTags(string cityName, string country, bool isCapital, long population)
        {
            var tags = new List<string> { "City", "Culture" };
            
            if (isCapital) tags.Add("Capital");
            if (population > 1000000) tags.Add("Metropolitan");
            if (population < 500000) tags.Add("Peaceful");
            
            // Add country-specific tags
            tags.AddRange(country.ToLower() switch
            {
                "france" => new[] { "Art", "Food", "Romance" },
                "italy" => new[] { "History", "Art", "Food" },
                "spain" => new[] { "Architecture", "Beach", "Nightlife" },
                "germany" => new[] { "History", "Museums", "Beer" },
                "netherlands" => new[] { "Architecture", "Art", "Cycling" },
                "united kingdom" => new[] { "History", "Museums", "Tea" },
                "greece" => new[] { "History", "Architecture", "Mediterranean" },
                "portugal" => new[] { "Ocean", "History", "Food" },
                "austria" => new[] { "Music", "Architecture", "Mountains" },
                "czech republic" => new[] { "Architecture", "History", "Beer" },
                "hungary" => new[] { "Architecture", "Thermal Baths", "History" },
                "sweden" => new[] { "Design", "Nature", "Clean" },
                "denmark" => new[] { "Design", "Hygge", "Cycling" },
                "ireland" => new[] { "Literature", "Pubs", "Friendly" },
                "switzerland" => new[] { "Mountains", "Clean", "Luxury" },
                _ => new[] { "Adventure", "Culture" }
            });
            
            return tags.Distinct().ToList();
        }

        private List<string> GenerateTagsFromCategories(List<string> categories)
        {
            var tags = new List<string>();
            
            foreach (var category in categories)
            {
                tags.AddRange(category.ToLower() switch
                {
                    "tourism" => new[] { "Tourist", "Popular" },
                    "accommodation" => new[] { "Hotels", "Comfort" },
                    "entertainment" => new[] { "Nightlife", "Fun" },
                    "natural" => new[] { "Nature", "Peaceful" },
                    "heritage" => new[] { "History", "Culture" },
                    _ => new[] { "Attraction" }
                });
            }
            
            return tags.Distinct().ToList();
        }

        private string GetDefaultImageUrl(string cityName)
        {
            // Generate Unsplash URLs based on city name
            var cityKey = cityName.ToLower().Replace(" ", "%20");
            return $"https://images.unsplash.com/photo-1580887083473-e98b99c65b39?w=800&q=80&auto=format&fit=crop&s={cityKey}";
        }

        private string DetermineClimate(decimal latitude)
        {
            var absLat = Math.Abs((double)latitude);
            return absLat switch
            {
                >= 60 => "Arctic",
                >= 50 => "Continental",
                >= 40 => "Temperate",
                >= 30 => "Subtropical",
                >= 20 => "Tropical",
                _ => "Equatorial"
            };
        }

        private string DetermineBestTimeToVisit(decimal latitude)
        {
            var absLat = Math.Abs((double)latitude);
            return absLat switch
            {
                >= 50 => "May to September",
                >= 40 => "April to June, September to October",
                >= 30 => "March to May, September to November",
                _ => "Year-round"
            };
        }

        private PriceLevel DeterminePriceLevel(string country, bool isCapital)
        {
            var expensiveCountries = new[] { "switzerland", "norway", "denmark", "sweden", "united kingdom", "france" };
            var budgetCountries = new[] { "czech republic", "hungary", "poland", "portugal", "greece" };
            
            var countryLower = country.ToLower();
            
            if (expensiveCountries.Contains(countryLower))
                return PriceLevel.Expensive;
            
            if (budgetCountries.Contains(countryLower))
                return isCapital ? PriceLevel.Moderate : PriceLevel.Budget;
            
            return isCapital ? PriceLevel.Expensive : PriceLevel.Moderate;
        }

        private List<ApiDestinationData> GetMockCityData(string countryName, int limit)
        {
            // Mock data for demonstration - in production, replace with real API calls
            var mockData = new Dictionary<string, List<ApiDestinationData>>
            {
                ["France"] = new()
                {
                    new() { Name = "Lyon", Country = "France", Region = "Auvergne-Rhône-Alpes", Latitude = 45.7640m, Longitude = 4.8357m, Population = 513275, IsCapital = false },
                    new() { Name = "Marseille", Country = "France", Region = "Provence-Alpes-Côte d'Azur", Latitude = 43.2965m, Longitude = 5.3698m, Population = 862211, IsCapital = false },
                    new() { Name = "Nice", Country = "France", Region = "Provence-Alpes-Côte d'Azur", Latitude = 43.7102m, Longitude = 7.2620m, Population = 342637, IsCapital = false }
                },
                ["Japan"] = new()
                {
                    new() { Name = "Kyoto", Country = "Japan", Region = "Kansai", Latitude = 35.0116m, Longitude = 135.7681m, Population = 1474473, IsCapital = false },
                    new() { Name = "Osaka", Country = "Japan", Region = "Kansai", Latitude = 34.6937m, Longitude = 135.5023m, Population = 2691185, IsCapital = false },
                    new() { Name = "Hiroshima", Country = "Japan", Region = "Chugoku", Latitude = 34.3853m, Longitude = 132.4553m, Population = 1194034, IsCapital = false }
                },
                ["Canada"] = new()
                {
                    new() { Name = "Vancouver", Country = "Canada", Region = "British Columbia", Latitude = 49.2827m, Longitude = -123.1207m, Population = 631486, IsCapital = false },
                    new() { Name = "Montreal", Country = "Canada", Region = "Quebec", Latitude = 45.5017m, Longitude = -73.5673m, Population = 1704694, IsCapital = false },
                    new() { Name = "Calgary", Country = "Canada", Region = "Alberta", Latitude = 51.0447m, Longitude = -114.0719m, Population = 1239220, IsCapital = false }
                }
            };

            if (!mockData.ContainsKey(countryName))
                return new List<ApiDestinationData>();

            var cities = mockData[countryName].Take(limit).ToList();
            
            // Add generated descriptions and tags
            foreach (var city in cities)
            {
                city.Description = GenerateDescription(city.Name, city.Country, city.IsCapital, city.Population);
                city.Tags = GenerateTags(city.Name, city.Country, city.IsCapital, city.Population);
            }

            return cities;
        }
    }
}
