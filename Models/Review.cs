using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelRecommendationSystem.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int? DestinationId { get; set; }
        public int? AttractionId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Content { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Display(Name = "Visit Date")]
        public DateTime? VisitDate { get; set; }

        [Display(Name = "Helpful Votes")]
        public int HelpfulVotes { get; set; }

        [Display(Name = "Total Votes")]
        public int TotalVotes { get; set; }

        public bool IsVerified { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey("DestinationId")]
        public virtual Destination? Destination { get; set; }

        [ForeignKey("AttractionId")]
        public virtual Attraction? Attraction { get; set; }

        public virtual ICollection<ReviewHelpful> ReviewHelpful { get; set; } = new List<ReviewHelpful>();

        // Computed property
        [Display(Name = "Helpful Percentage")]
        public double HelpfulPercentage => TotalVotes > 0 ? (double)HelpfulVotes / TotalVotes * 100 : 0;
    }
}
