using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelRecommendationSystem.Data;
using TravelRecommendationSystem.Models;

namespace TravelRecommendationSystem.Controllers;

[Authorize]
public class BookingsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public BookingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Bookings
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var bookings = await _context.Bookings
            .Include(b => b.Destination)
                .ThenInclude(d => d.Images)
            .Where(b => b.UserId == user.Id)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return View(bookings);
    }

    // GET: Bookings/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var booking = await _context.Bookings
            .Include(b => b.Destination)
                .ThenInclude(d => d.Images)
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == user.Id);

        if (booking == null)
        {
            return NotFound();
        }

        return View(booking);
    }

    // GET: Bookings/Create
    public async Task<IActionResult> Create(int? destinationId)
    {
        if (destinationId == null)
        {
            return NotFound();
        }

        var destination = await _context.Destinations
            .Include(d => d.Images)
            .FirstOrDefaultAsync(d => d.Id == destinationId && d.IsActive);

        if (destination == null)
        {
            return NotFound();
        }

        var booking = new Booking
        {
            DestinationId = destinationId.Value,
            CheckInDate = DateTime.Today.AddDays(7),
            CheckOutDate = DateTime.Today.AddDays(14),
            Adults = 1,
            Children = 0,
            NumberOfGuests = 1,
            Currency = "USD"
        };

        ViewBag.Destination = destination;
        return View(booking);
    }

    // POST: Bookings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Booking booking)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var destination = await _context.Destinations
            .FirstOrDefaultAsync(d => d.Id == booking.DestinationId && d.IsActive);

        if (destination == null)
        {
            return NotFound();
        }

        // Custom validation
        if (booking.CheckInDate < DateTime.Today)
        {
            ModelState.AddModelError(nameof(booking.CheckInDate), "Check-in date cannot be in the past");
        }

        if (booking.CheckOutDate <= booking.CheckInDate)
        {
            ModelState.AddModelError(nameof(booking.CheckOutDate), "Check-out date must be after check-in date");
        }

        if (booking.Adults < 1 || booking.Adults > 10)
        {
            ModelState.AddModelError(nameof(booking.Adults), "Number of adults must be between 1 and 10");
        }

        if (booking.Children < 0 || booking.Children > 8)
        {
            ModelState.AddModelError(nameof(booking.Children), "Number of children must be between 0 and 8");
        }

        var totalGuests = booking.Adults + booking.Children;
        if (totalGuests < 1 || totalGuests > 10)
        {
            ModelState.AddModelError(nameof(booking.Adults), "Total number of guests must be between 1 and 10");
        }

        if (ModelState.IsValid)
        {
            // Calculate total amount based on destination price level and duration
            var duration = (booking.CheckOutDate - booking.CheckInDate).Days;
            var basePrice = (int)destination.AveragePriceLevel switch
            {
                1 => 50,  // Budget
                2 => 100, // Mid-range
                3 => 200, // Luxury
                4 => 500, // Premium
                _ => 100
            };

            // Calculate total guests from adults and children
            booking.NumberOfGuests = booking.Adults + booking.Children;
            booking.TotalAmount = basePrice * duration * booking.NumberOfGuests;
            booking.UserId = user.Id;
            booking.BookingReference = GenerateBookingReference();
            booking.Status = BookingStatus.Pending;
            booking.BookingType = BookingType.Hotel;
            booking.ConfirmationEmailSent = false;
            booking.CreatedAt = DateTime.UtcNow;
            booking.UpdatedAt = DateTime.UtcNow;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Booking created successfully! Your booking reference is: {booking.BookingReference}";
            return RedirectToAction(nameof(Details), new { id = booking.Id });
        }

        ViewBag.Destination = destination;
        return View(booking);
    }

    // GET: Bookings/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var booking = await _context.Bookings
            .Include(b => b.Destination)
                .ThenInclude(d => d.Images)
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == user.Id);

        if (booking == null)
        {
            return NotFound();
        }

        // Only allow editing if booking is pending or confirmed
        if (booking.Status == BookingStatus.Cancelled || booking.Status == BookingStatus.CheckedOut)
        {
            TempData["Error"] = "Cannot edit a cancelled or completed booking";
            return RedirectToAction(nameof(Details), new { id });
        }

        ViewBag.Destination = booking.Destination;
        return View(booking);
    }

    // POST: Bookings/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Booking booking)
    {
        if (id != booking.Id)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var existingBooking = await _context.Bookings
            .Include(b => b.Destination)
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == user.Id);

        if (existingBooking == null)
        {
            return NotFound();
        }

        // Only allow editing if booking is pending or confirmed
        if (existingBooking.Status == BookingStatus.Cancelled || existingBooking.Status == BookingStatus.CheckedOut)
        {
            TempData["Error"] = "Cannot edit a cancelled or completed booking";
            return RedirectToAction(nameof(Details), new { id });
        }

        // Custom validation
        if (booking.CheckInDate < DateTime.Today)
        {
            ModelState.AddModelError(nameof(booking.CheckInDate), "Check-in date cannot be in the past");
        }

        if (booking.CheckOutDate <= booking.CheckInDate)
        {
            ModelState.AddModelError(nameof(booking.CheckOutDate), "Check-out date must be after check-in date");
        }

        if (booking.Adults < 1 || booking.Adults > 10)
        {
            ModelState.AddModelError(nameof(booking.Adults), "Number of adults must be between 1 and 10");
        }

        if (booking.Children < 0 || booking.Children > 8)
        {
            ModelState.AddModelError(nameof(booking.Children), "Number of children must be between 0 and 8");
        }

        var totalGuests = booking.Adults + booking.Children;
        if (totalGuests < 1 || totalGuests > 10)
        {
            ModelState.AddModelError(nameof(booking.Adults), "Total number of guests must be between 1 and 10");
        }

        if (ModelState.IsValid)
        {
            // Recalculate total amount
            var duration = (booking.CheckOutDate - booking.CheckInDate).Days;
            var basePrice = (int)existingBooking.Destination.AveragePriceLevel switch
            {
                1 => 50,  // Budget
                2 => 100, // Mid-range
                3 => 200, // Luxury
                4 => 500, // Premium
                _ => 100
            };

            existingBooking.CheckInDate = booking.CheckInDate;
            existingBooking.CheckOutDate = booking.CheckOutDate;
            existingBooking.Adults = booking.Adults;
            existingBooking.Children = booking.Children;
            existingBooking.NumberOfGuests = booking.Adults + booking.Children;
            existingBooking.TotalAmount = basePrice * duration * existingBooking.NumberOfGuests;
            existingBooking.Notes = booking.Notes;
            existingBooking.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Booking updated successfully!";
            return RedirectToAction(nameof(Details), new { id });
        }

        ViewBag.Destination = existingBooking.Destination;
        return View(booking);
    }

    // POST: Bookings/Cancel/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == user.Id);

        if (booking == null)
        {
            return NotFound();
        }

        if (booking.Status == BookingStatus.Cancelled)
        {
            TempData["Error"] = "Booking is already cancelled";
        }
        else if (booking.Status == BookingStatus.CheckedOut)
        {
            TempData["Error"] = "Cannot cancel a completed booking";
        }
        else
        {
            booking.Status = BookingStatus.Cancelled;
            booking.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Booking has been cancelled successfully";
        }

        return RedirectToAction(nameof(Details), new { id });
    }

    private string GenerateBookingReference()
    {
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var random = new Random().Next(1000, 9999);
        return $"TR{timestamp}{random}";
    }
}
