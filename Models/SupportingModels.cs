using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelRecommendationSystem.Models
{
    public class UserFavorite
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int DestinationId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey("DestinationId")]
        public virtual Destination Destination { get; set; } = null!;
    }

    // UserPreferences is defined in Models/UserPreferences.cs

    public class DestinationImage
    {
        public int Id { get; set; }

        [Required]
        public int DestinationId { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Caption { get; set; }

        public bool IsPrimary { get; set; }

        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("DestinationId")]
        public virtual Destination Destination { get; set; } = null!;
    }

    public class DestinationTag
    {
        public int Id { get; set; }

        [Required]
        public int DestinationId { get; set; }

        [Required]
        [StringLength(50)]
        public string TagName { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("DestinationId")]
        public virtual Destination Destination { get; set; } = null!;
    }

    public class ReviewHelpful
    {
        public int Id { get; set; }

        [Required]
        public int ReviewId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public bool IsHelpful { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ReviewId")]
        public virtual Review Review { get; set; } = null!;

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
    }

    // GroupSize enum is defined in Models/UserPreferences.cs

    // TravelStyle enum is defined in Models/UserPreferences.cs
}
