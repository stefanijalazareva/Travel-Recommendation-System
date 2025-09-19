using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TravelRecommendationSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Profile Picture")]
        public string? ProfilePictureUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }

        // Navigation properties
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<UserFavorite> Favorites { get; set; } = new List<UserFavorite>();
        public virtual UserPreferences? UserPreferences { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
