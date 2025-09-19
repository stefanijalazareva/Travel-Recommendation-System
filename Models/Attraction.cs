using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelRecommendationSystem.Models
{
    public class Attraction
    {
        public int Id { get; set; }

        [Required]
        public int DestinationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Category { get; set; }

        public string? ImageUrl { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Longitude { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public string? Website { get; set; }

        [StringLength(200)]
        public string? OpeningHours { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? EntryFee { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageRating { get; set; }

        public int TotalReviews { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("DestinationId")]
        public virtual Destination Destination { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
