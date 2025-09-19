using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelRecommendationSystem.Data;
using TravelRecommendationSystem.Models;

namespace TravelRecommendationSystem.Controllers;

public class DestinationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DestinationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Destinations
    public async Task<IActionResult> Index(string searchString, string category, string sortBy = "rating", int page = 1, int pageSize = 6)
    {
        var query = _context.Destinations
            .Where(d => d.IsActive)
            .Include(d => d.Tags)
            .Include(d => d.Images)
            .Include(d => d.Attractions)
            .AsQueryable();

        // Search functionality
        if (!string.IsNullOrEmpty(searchString))
        {
            var term = searchString.ToLower();
            query = query.Where(d => d.Name.ToLower().Contains(term)
                || d.Country.ToLower().Contains(term)
                || (d.Region != null && d.Region.ToLower().Contains(term))
                || d.Description.ToLower().Contains(term)
                || d.Tags.Any(t => t.TagName.ToLower().Contains(term)));
        }

        // Category filtering
        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(d => d.Tags.Any(t => t.TagName.ToLower().Contains(category.ToLower())));
        }

        // Pagination and sorting
        var totalItems = await query.CountAsync();
        var allDestinations = await query.ToListAsync();

        // Sort on client side to avoid SQLite decimal ordering issue
        var sortedDestinations = sortBy.ToLower() switch
        {
            "name" => allDestinations.OrderBy(d => d.Name),
            "country" => allDestinations.OrderBy(d => d.Country),
            "price" => allDestinations.OrderBy(d => d.AveragePriceLevel),
            "rating" => allDestinations.OrderByDescending(d => d.AverageRating),
            _ => allDestinations.OrderByDescending(d => d.AverageRating)
        };

        var destinations = sortedDestinations
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.SearchString = searchString;
        ViewBag.SortBy = sortBy;
        ViewBag.SelectedCategory = category;
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        ViewBag.TotalItems = totalItems;

        return View(destinations);
    }

    // GET: Destinations/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var destination = await _context.Destinations
            .Include(d => d.Tags)
            .Include(d => d.Images)
            .Include(d => d.Attractions.Where(a => a.IsActive))
            .Include(d => d.Reviews.Where(r => r.IsActive))
                .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);

        if (destination == null)
        {
            return NotFound();
        }

        // Get related destinations (same country or similar tags)
        var relatedQuery = await _context.Destinations
            .Where(d => d.IsActive && d.Id != id && 
                (d.Country == destination.Country || 
                 d.Tags.Any(t => destination.Tags.Select(dt => dt.TagName).Contains(t.TagName))))
            .Include(d => d.Tags)
            .Include(d => d.Images)
            .ToListAsync();

        // Sort on client side
        var relatedDestinations = relatedQuery
            .OrderByDescending(d => d.AverageRating)
            .Take(4)
            .ToList();

        ViewBag.RelatedDestinations = relatedDestinations;

        return View(destination);
    }

    // GET: Destinations/Search
    [HttpGet]
    public async Task<IActionResult> Search(string term)
    {
        if (string.IsNullOrEmpty(term))
        {
            return Json(new List<object>());
        }

        var lower = term.ToLower();
        var suggestions = await _context.Destinations
            .Where(d => d.IsActive && (d.Name.ToLower().Contains(lower) || d.Country.ToLower().Contains(lower)))
            .Select(d => new { 
                id = d.Id, 
                name = d.Name, 
                country = d.Country,
                imageUrl = d.ImageUrl
            })
            .Take(10)
            .ToListAsync();

        return Json(suggestions);
    }

    // GET: Destinations/ByTag
    public async Task<IActionResult> ByTag(string tag, int page = 1, int pageSize = 9)
    {
        if (string.IsNullOrEmpty(tag))
        {
            return RedirectToAction(nameof(Index));
        }

        var query = await _context.Destinations
            .Where(d => d.IsActive && d.Tags.Any(t => t.TagName.ToLower().Contains(tag.ToLower())))
            .Include(d => d.Tags)
            .Include(d => d.Images)
            .Include(d => d.Attractions)
            .ToListAsync();

        // Sort on client side
        var sortedQuery = query.OrderByDescending(d => d.AverageRating);

        var totalItems = query.Count();
        var destinations = sortedQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.Tag = tag;
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        ViewBag.TotalItems = totalItems;

        return View("Index", destinations);
    }
}
