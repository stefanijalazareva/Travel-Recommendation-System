using TravelRecommendationSystem.Models;

namespace TravelRecommendationSystem.ViewModels;

public class UserDashboardViewModel
{
    public ApplicationUser User { get; set; } = null!;
    public List<Booking> RecentBookings { get; set; } = new();
    public List<Review> RecentReviews { get; set; } = new();
    public List<Destination> FavoriteDestinations { get; set; } = new();
    
    public int BookingsCount { get; set; }
    public int ReviewsCount { get; set; }
    public int FavoritesCount { get; set; }
}
