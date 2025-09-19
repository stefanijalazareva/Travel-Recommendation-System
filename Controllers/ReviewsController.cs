using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelRecommendationSystem.Data;
using TravelRecommendationSystem.Models;

namespace TravelRecommendationSystem.Controllers;

[Authorize]
public class ReviewsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReviewsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Reviews/Create
    public async Task<IActionResult> Create(int? destinationId, int? attractionId)
    {
        if (destinationId == null && attractionId == null)
        {
            return BadRequest("Either destination or attraction must be specified");
        }

        var model = new Review();

        if (destinationId.HasValue)
        {
            var destination = await _context.Destinations.FindAsync(destinationId.Value);
            if (destination == null)
            {
                return NotFound();
            }
            model.DestinationId = destinationId.Value;
            ViewBag.DestinationName = destination.Name;
        }

        if (attractionId.HasValue)
        {
            var attraction = await _context.Attractions
                .Include(a => a.Destination)
                .FirstOrDefaultAsync(a => a.Id == attractionId.Value);
            if (attraction == null)
            {
                return NotFound();
            }
            model.AttractionId = attractionId.Value;
            ViewBag.AttractionName = attraction.Name;
            ViewBag.DestinationName = attraction.Destination.Name;
        }

        return View(model);
    }

    // POST: Reviews/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Review review)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        // Validate that either destination or attraction is specified
        if (review.DestinationId == null && review.AttractionId == null)
        {
            ModelState.AddModelError("", "Either destination or attraction must be specified");
        }

        // Check if user has already reviewed this destination/attraction
        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r => r.UserId == user.Id && 
                ((r.DestinationId == review.DestinationId && review.DestinationId != null) ||
                 (r.AttractionId == review.AttractionId && review.AttractionId != null)));

        if (existingReview != null)
        {
            ModelState.AddModelError("", "You have already reviewed this item");
        }

        if (ModelState.IsValid)
        {
            review.UserId = user.Id;
            review.CreatedAt = DateTime.UtcNow;
            review.UpdatedAt = DateTime.UtcNow;
            review.IsActive = true;
            review.IsVerified = false;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Update destination/attraction rating
            await UpdateAverageRating(review);

            TempData["Success"] = "Your review has been submitted successfully!";

            if (review.DestinationId.HasValue)
            {
                return RedirectToAction("Details", "Destinations", new { id = review.DestinationId });
            }
            else if (review.AttractionId.HasValue)
            {
                var attraction = await _context.Attractions.Include(a => a.Destination)
                    .FirstOrDefaultAsync(a => a.Id == review.AttractionId);
                return RedirectToAction("Details", "Destinations", new { id = attraction.DestinationId });
            }
        }

        // Reload ViewBag data if model is invalid
        if (review.DestinationId.HasValue)
        {
            var destination = await _context.Destinations.FindAsync(review.DestinationId.Value);
            ViewBag.DestinationName = destination?.Name;
        }

        if (review.AttractionId.HasValue)
        {
            var attraction = await _context.Attractions
                .Include(a => a.Destination)
                .FirstOrDefaultAsync(a => a.Id == review.AttractionId.Value);
            ViewBag.AttractionName = attraction?.Name;
            ViewBag.DestinationName = attraction?.Destination.Name;
        }

        return View(review);
    }

    // GET: Reviews/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var review = await _context.Reviews
            .Include(r => r.Destination)
            .Include(r => r.Attraction)
                .ThenInclude(a => a.Destination)
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == user.Id);

        if (review == null)
        {
            return NotFound();
        }

        if (review.Destination != null)
        {
            ViewBag.DestinationName = review.Destination.Name;
        }

        if (review.Attraction != null)
        {
            ViewBag.AttractionName = review.Attraction.Name;
            ViewBag.DestinationName = review.Attraction.Destination.Name;
        }

        return View(review);
    }

    // POST: Reviews/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Review review)
    {
        if (id != review.Id)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == user.Id);

        if (existingReview == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            existingReview.Title = review.Title;
            existingReview.Content = review.Content;
            existingReview.Rating = review.Rating;
            existingReview.ImageUrl = review.ImageUrl;
            existingReview.VisitDate = review.VisitDate;
            existingReview.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            await UpdateAverageRating(existingReview);

            TempData["Success"] = "Your review has been updated successfully!";

            if (existingReview.DestinationId.HasValue)
            {
                return RedirectToAction("Details", "Destinations", new { id = existingReview.DestinationId });
            }
            else if (existingReview.AttractionId.HasValue)
            {
                var attraction = await _context.Attractions.Include(a => a.Destination)
                    .FirstOrDefaultAsync(a => a.Id == existingReview.AttractionId);
                return RedirectToAction("Details", "Destinations", new { id = attraction.DestinationId });
            }
        }

        return View(review);
    }

    // POST: Reviews/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == user.Id);

        if (review == null)
        {
            return NotFound();
        }

        var destinationId = review.DestinationId;
        var attractionId = review.AttractionId;

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        await UpdateAverageRating(review);

        TempData["Success"] = "Your review has been deleted successfully!";

        if (destinationId.HasValue)
        {
            return RedirectToAction("Details", "Destinations", new { id = destinationId });
        }
        else if (attractionId.HasValue)
        {
            var attraction = await _context.Attractions.Include(a => a.Destination)
                .FirstOrDefaultAsync(a => a.Id == attractionId);
            return RedirectToAction("Details", "Destinations", new { id = attraction.DestinationId });
        }

        return RedirectToAction("Index", "Home");
    }

    // POST: Reviews/MarkHelpful/5
    [HttpPost]
    public async Task<IActionResult> MarkHelpful(int reviewId, bool isHelpful)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Json(new { success = false, message = "Please log in to vote" });
        }

        var existingVote = await _context.ReviewHelpful
            .FirstOrDefaultAsync(rh => rh.ReviewId == reviewId && rh.UserId == user.Id);

        if (existingVote != null)
        {
            existingVote.IsHelpful = isHelpful;
            existingVote.CreatedAt = DateTime.UtcNow;
        }
        else
        {
            var vote = new ReviewHelpful
            {
                ReviewId = reviewId,
                UserId = user.Id,
                IsHelpful = isHelpful,
                CreatedAt = DateTime.UtcNow
            };
            _context.ReviewHelpful.Add(vote);
        }

        await _context.SaveChangesAsync();

        // Update review helpful counts
        var review = await _context.Reviews.FindAsync(reviewId);
        if (review != null)
        {
            var helpfulCount = await _context.ReviewHelpful
                .CountAsync(rh => rh.ReviewId == reviewId && rh.IsHelpful);
            var totalVotes = await _context.ReviewHelpful
                .CountAsync(rh => rh.ReviewId == reviewId);

            review.HelpfulVotes = helpfulCount;
            review.TotalVotes = totalVotes;
            await _context.SaveChangesAsync();

            return Json(new { 
                success = true, 
                helpfulVotes = helpfulCount, 
                totalVotes = totalVotes 
            });
        }

        return Json(new { success = false, message = "Review not found" });
    }

    private async Task UpdateAverageRating(Review review)
    {
        if (review.DestinationId.HasValue)
        {
            var destination = await _context.Destinations.FindAsync(review.DestinationId.Value);
            if (destination != null)
            {
                var reviews = await _context.Reviews
                    .Where(r => r.DestinationId == destination.Id && r.IsActive)
                    .ToListAsync();

                destination.AverageRating = reviews.Any() ? (decimal)reviews.Average(r => r.Rating) : 0;
                destination.TotalReviews = reviews.Count;
                destination.UpdatedAt = DateTime.UtcNow;
            }
        }

        if (review.AttractionId.HasValue)
        {
            var attraction = await _context.Attractions.FindAsync(review.AttractionId.Value);
            if (attraction != null)
            {
                var reviews = await _context.Reviews
                    .Where(r => r.AttractionId == attraction.Id && r.IsActive)
                    .ToListAsync();

                attraction.AverageRating = reviews.Any() ? (decimal)reviews.Average(r => r.Rating) : 0;
                attraction.TotalReviews = reviews.Count;
                attraction.UpdatedAt = DateTime.UtcNow;
            }
        }

        await _context.SaveChangesAsync();
    }
}
