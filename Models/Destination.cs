using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelRecommendationSystem.Models
{
    public class Destination
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Destination Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Region { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Main Image")]
        public string? ImageUrl { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal Longitude { get; set; }

        [StringLength(50)]
        public string? Climate { get; set; }

        [Display(Name = "Best Time to Visit")]
        [StringLength(100)]
        public string? BestTimeToVisit { get; set; }

        [Display(Name = "Average Price Level")]
        public PriceLevel AveragePriceLevel { get; set; }

        [Display(Name = "Average Rating")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageRating { get; set; }

        [Display(Name = "Total Reviews")]
        public int TotalReviews { get; set; }

        [Display(Name = "Featured")]
        public bool IsFeatured { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<UserFavorite> UserFavorites { get; set; } = new List<UserFavorite>();
        public virtual ICollection<DestinationImage> Images { get; set; } = new List<DestinationImage>();
        public virtual ICollection<DestinationTag> Tags { get; set; } = new List<DestinationTag>();
    }

    public enum PriceLevel
    {
        Budget = 1,
        Moderate = 2,
        Expensive = 3,
        Luxury = 4
    }
}
