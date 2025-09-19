using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelRecommendationSystem.Data;
using TravelRecommendationSystem.Models;

namespace TravelRecommendationSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Get featured destinations for the homepage
        var featuredDestinations = await _context.Destinations
            .Where(d => d.IsFeatured && d.IsActive)
            .Include(d => d.Tags)
            .Include(d => d.Images)
            .ToListAsync();

        // Sort on client side to avoid SQLite decimal ordering issue
        featuredDestinations = featuredDestinations
            .OrderByDescending(d => d.AverageRating)
            .Take(6)
            .ToList();

        return View(featuredDestinations);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
