using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelRecommendationSystem.Data;
using TravelRecommendationSystem.Models;

namespace TravelRecommendationSystem.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: User/Profile
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var userWithDetails = await _context.Users
            .Include(u => u.Reviews.Where(r => r.IsActive))
                .ThenInclude(r => r.Destination)
            .Include(u => u.Bookings)
                .ThenInclude(b => b.Destination)
            .Include(u => u.Favorites)
                .ThenInclude(f => f.Destination)
                    .ThenInclude(d => d.Images)
            .FirstOrDefaultAsync(u => u.Id == user.Id);

        // Get user preferences separately
        var userPreferences = await _context.UserPreferences
            .FirstOrDefaultAsync(p => p.UserId == user.Id);

        ViewBag.UserPreferences = userPreferences;

        return View(userWithDetails);
    }

    // GET: User/EditProfile
    public async Task<IActionResult> EditProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        return View(user);
    }

    // POST: User/EditProfile
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProfile(ApplicationUser model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        // Only update allowed fields
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.DateOfBirth = model.DateOfBirth;
        user.ProfilePictureUrl = model.ProfilePictureUrl;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction(nameof(Profile));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    // GET: User/Preferences
    public async Task<IActionResult> Preferences()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var preferences = await _context.UserPreferences
            .FirstOrDefaultAsync(p => p.UserId == user.Id);

        if (preferences == null)
        {
            preferences = new UserPreferences
            {
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        return View(preferences);
    }

    // POST: User/Preferences
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Preferences(UserPreferences model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var existingPreferences = await _context.UserPreferences
            .FirstOrDefaultAsync(p => p.UserId == user.Id);

        if (existingPreferences == null)
        {
            model.UserId = user.Id;
            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;
            _context.UserPreferences.Add(model);
        }
        else
        {
            existingPreferences.PreferredBudget = model.PreferredBudget;
            existingPreferences.PreferredClimate = model.PreferredClimate;
            existingPreferences.PreferredTripDuration = model.PreferredTripDuration;
            existingPreferences.LikesAdventure = model.LikesAdventure;
            existingPreferences.LikesCulture = model.LikesCulture;
            existingPreferences.LikesBeach = model.LikesBeach;
            existingPreferences.LikesMountains = model.LikesMountains;
            existingPreferences.LikesNightlife = model.LikesNightlife;
            existingPreferences.LikesFoodTourism = model.LikesFoodTourism;
            existingPreferences.LikesShopping = model.LikesShopping;
            existingPreferences.LikesHistory = model.LikesHistory;
            existingPreferences.PreferredGroupSize = model.PreferredGroupSize;
            existingPreferences.TravelStyle = model.TravelStyle;
            existingPreferences.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        TempData["Success"] = "Preferences updated successfully!";
        return RedirectToAction(nameof(Profile));
    }

    // GET: User/Favorites
    public async Task<IActionResult> Favorites()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var favorites = await _context.UserFavorites
            .Include(f => f.Destination)
                .ThenInclude(d => d.Images)
            .Include(f => f.Destination)
                .ThenInclude(d => d.Tags)
            .Where(f => f.UserId == user.Id)
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync();

        return View(favorites);
    }

    // POST: User/ToggleFavorite
    [HttpPost]
    public async Task<IActionResult> ToggleFavorite(int destinationId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Json(new { success = false, message = "Please log in" });
        }

        var destination = await _context.Destinations.FindAsync(destinationId);
        if (destination == null)
        {
            return Json(new { success = false, message = "Destination not found" });
        }

        var existingFavorite = await _context.UserFavorites
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.DestinationId == destinationId);

        bool isFavorite;
        if (existingFavorite != null)
        {
            _context.UserFavorites.Remove(existingFavorite);
            isFavorite = false;
        }
        else
        {
            var favorite = new UserFavorite
            {
                UserId = user.Id,
                DestinationId = destinationId,
                CreatedAt = DateTime.UtcNow
            };
            _context.UserFavorites.Add(favorite);
            isFavorite = true;
        }

        await _context.SaveChangesAsync();

        return Json(new { 
            success = true, 
            isFavorite = isFavorite,
            message = isFavorite ? "Added to favorites" : "Removed from favorites"
        });
    }

    // GET: User/Reviews
    public async Task<IActionResult> Reviews()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var reviews = await _context.Reviews
            .Include(r => r.Destination)
            .Include(r => r.Attraction)
                .ThenInclude(a => a.Destination)
            .Where(r => r.UserId == user.Id)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return View(reviews);
    }

    // GET: User/Recommendations
    public async Task<IActionResult> Recommendations()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var preferences = await _context.UserPreferences
            .FirstOrDefaultAsync(p => p.UserId == user.Id);

        var userFavorites = await _context.UserFavorites
            .Where(f => f.UserId == user.Id)
            .Select(f => f.DestinationId)
            .ToListAsync();

        // Get recommended destinations based on preferences and favorites
        var recommendedDestinations = await GetRecommendedDestinations(preferences, userFavorites);

        ViewBag.Preferences = preferences;
        return View(recommendedDestinations);
    }

    private async Task<List<Destination>> GetRecommendedDestinations(UserPreferences? preferences, List<int> userFavorites)
    {
        var query = _context.Destinations
            .Where(d => d.IsActive && !userFavorites.Contains(d.Id))
            .Include(d => d.Tags)
            .Include(d => d.Images)
            .AsQueryable();

        if (preferences != null)
        {
            // Filter by budget preference
            if (preferences.PreferredBudget.HasValue)
            {
                var maxBudget = preferences.PreferredBudget.Value;
                query = query.Where(d => (int)d.AveragePriceLevel <= maxBudget);
            }

            // Filter by preferred interests
            var preferredTags = new List<string>();
            if (preferences.LikesAdventure) preferredTags.Add("Adventure");
            if (preferences.LikesCulture) preferredTags.Add("Culture");
            if (preferences.LikesBeach) preferredTags.Add("Beach");
            if (preferences.LikesMountains) preferredTags.Add("Mountains");
            if (preferences.LikesNightlife) preferredTags.Add("Nightlife");
            if (preferences.LikesFoodTourism) preferredTags.Add("Food");
            if (preferences.LikesShopping) preferredTags.Add("Shopping");
            if (preferences.LikesHistory) preferredTags.Add("History");

            if (preferredTags.Any())
            {
                query = query.Where(d => d.Tags.Any(t => preferredTags.Contains(t.TagName)));
            }
        }

        var allDestinations = await query.ToListAsync();

        // Sort on client side to avoid SQLite decimal ordering issue
        return allDestinations
            .OrderByDescending(d => d.AverageRating)
            .ThenByDescending(d => d.TotalReviews)
            .Take(12)
            .ToList();
    }
}
