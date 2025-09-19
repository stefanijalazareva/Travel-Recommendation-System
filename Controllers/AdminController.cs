using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelRecommendationSystem.Services;

namespace TravelRecommendationSystem.Controllers
{
    [Authorize(Roles = "Admin")] // Only admins should be able to import data
    public class AdminController : Controller
    {
        private readonly IDestinationApiService _destinationApiService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IDestinationApiService destinationApiService, ILogger<AdminController> logger)
        {
            _destinationApiService = destinationApiService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportDestinations()
        {
            try
            {
                // European countries to import
                var europeanCountries = new List<string>
                {
                    "France", "Italy", "Spain", "Germany", "Netherlands", "United Kingdom", 
                    "Greece", "Portugal", "Austria", "Czech Republic", "Hungary", "Sweden", 
                    "Denmark", "Ireland", "Switzerland", "Belgium", "Norway", "Finland",
                    "Poland", "Romania", "Croatia", "Slovenia", "Slovakia", "Bulgaria",
                    "Estonia", "Latvia", "Lithuania", "Luxembourg", "Malta", "Cyprus"
                };

                var importedCount = await _destinationApiService.ImportDestinationsFromApiAsync(europeanCountries, 3);
                
                TempData["Success"] = $"Successfully imported {importedCount} destinations from external APIs!";
                _logger.LogInformation($"Imported {importedCount} destinations from APIs");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to import destinations. Check the logs for details.";
                _logger.LogError(ex, "Failed to import destinations from APIs");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ImportSpecificCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                TempData["Error"] = "Please provide a country name.";
                return RedirectToAction("Index");
            }

            try
            {
                var importedCount = await _destinationApiService.ImportDestinationsFromApiAsync(new List<string> { country }, 5);
                TempData["Success"] = $"Successfully imported {importedCount} destinations for {country}!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Failed to import destinations for {country}. Check the logs for details.";
                _logger.LogError(ex, $"Failed to import destinations for {country}");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> PreviewApiData(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                return Json(new { error = "Country name is required" });
            }

            try
            {
                var cities = await _destinationApiService.FetchCitiesFromApiAsync(country, 5);
                return Json(new { success = true, cities = cities });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to preview data for {country}");
                return Json(new { error = "Failed to fetch preview data" });
            }
        }
    }
}
