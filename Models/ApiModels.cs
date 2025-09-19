using System.Text.Json.Serialization;

namespace TravelRecommendationSystem.Models
{
    // API-Ninjas City API Response Models
    public class CityApiResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("population")]
        public long Population { get; set; }

        [JsonPropertyName("is_capital")]
        public bool IsCapital { get; set; }

        [JsonPropertyName("latitude")]
        public decimal Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public decimal Longitude { get; set; }
    }

    // Geoapify Places API Response Models
    public class GeoapifyResponse
    {
        [JsonPropertyName("features")]
        public List<GeoapifyFeature> Features { get; set; } = new List<GeoapifyFeature>();
    }

    public class GeoapifyFeature
    {
        [JsonPropertyName("properties")]
        public GeoapifyProperties Properties { get; set; } = new GeoapifyProperties();

        [JsonPropertyName("geometry")]
        public GeoapifyGeometry Geometry { get; set; } = new GeoapifyGeometry();
    }

    public class GeoapifyProperties
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("formatted")]
        public string Formatted { get; set; } = string.Empty;

        [JsonPropertyName("categories")]
        public List<string> Categories { get; set; } = new List<string>();

        [JsonPropertyName("datasource")]
        public GeoapifyDatasource Datasource { get; set; } = new GeoapifyDatasource();
    }

    public class GeoapifyGeometry
    {
        [JsonPropertyName("coordinates")]
        public List<decimal> Coordinates { get; set; } = new List<decimal>();
    }

    public class GeoapifyDatasource
    {
        [JsonPropertyName("sourcename")]
        public string Sourcename { get; set; } = string.Empty;

        [JsonPropertyName("attribution")]
        public string Attribution { get; set; } = string.Empty;
    }

    // Internal model for destination creation
    public class ApiDestinationData
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public long Population { get; set; }
        public bool IsCapital { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public string Description { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
    }
}
