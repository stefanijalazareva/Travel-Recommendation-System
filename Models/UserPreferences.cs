using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelRecommendationSystem.Models
{
    public class UserPreferences
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Display(Name = "Preferred Budget Level")]
        [Range(1, 4)]
        public int? PreferredBudget { get; set; } // 1-4 corresponding to PriceLevel enum

        [StringLength(50)]
        [Display(Name = "Preferred Climate")]
        public string? PreferredClimate { get; set; }

        [Display(Name = "Preferred Trip Duration (Days)")]
        [Range(1, 365)]
        public int? PreferredTripDuration { get; set; }

        [Display(Name = "Likes Adventure")]
        public bool LikesAdventure { get; set; }

        [Display(Name = "Likes Culture")]
        public bool LikesCulture { get; set; }

        [Display(Name = "Likes Beach")]
        public bool LikesBeach { get; set; }

        [Display(Name = "Likes Mountains")]
        public bool LikesMountains { get; set; }

        [Display(Name = "Likes Nightlife")]
        public bool LikesNightlife { get; set; }

        [Display(Name = "Likes Food Tourism")]
        public bool LikesFoodTourism { get; set; }

        [Display(Name = "Likes Shopping")]
        public bool LikesShopping { get; set; }

        [Display(Name = "Likes History")]
        public bool LikesHistory { get; set; }

        [Display(Name = "Preferred Group Size")]
        public GroupSize PreferredGroupSize { get; set; } = GroupSize.Couple;

        [Display(Name = "Travel Style")]
        public TravelStyle TravelStyle { get; set; } = TravelStyle.Balanced;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
    }

    public enum GroupSize
    {
        Solo = 0,
        Couple = 1,
        Family = 2,
        SmallGroup = 3,
        LargeGroup = 4
    }

    public enum TravelStyle
    {
        Budget = 0,
        Comfort = 1,
        Luxury = 2,
        Adventure = 3,
        Cultural = 4,
        Relaxed = 5,
        Balanced = 6
    }
}
