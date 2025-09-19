using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelRecommendationSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int DestinationId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Booking Reference")]
        public string BookingReference { get; set; } = string.Empty;

        [Display(Name = "Check-in Date")]
        public DateTime CheckInDate { get; set; }

        [Display(Name = "Check-out Date")]
        public DateTime CheckOutDate { get; set; }

        [Display(Name = "Number of Guests")]
        [Range(1, 20)]
        public int NumberOfGuests { get; set; }

        [Display(Name = "Adults")]
        [Range(1, 10)]
        public int Adults { get; set; } = 1;

        [Display(Name = "Children")]
        [Range(0, 8)]
        public int Children { get; set; } = 0;

        [StringLength(100)]
        [Display(Name = "Accommodation Type")]
        public string? AccommodationType { get; set; }

        [StringLength(1000)]
        [Display(Name = "Special Requests")]
        public string? SpecialRequests { get; set; }

        [Display(Name = "Flight Included")]
        public bool FlightIncluded { get; set; }

        [Display(Name = "Transfer Included")]
        public bool TransferIncluded { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [StringLength(3)]
        public string Currency { get; set; } = "USD";

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public BookingType BookingType { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [StringLength(100)]
        [Display(Name = "Provider Name")]
        public string? ProviderName { get; set; }

        [Display(Name = "Provider Booking URL")]
        public string? ProviderBookingUrl { get; set; }

        [Display(Name = "Confirmation Email Sent")]
        public bool ConfirmationEmailSent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey("DestinationId")]
        public virtual Destination Destination { get; set; } = null!;

        // Computed properties
        [Display(Name = "Duration (Days)")]
        public int Duration => (CheckOutDate - CheckInDate).Days;

        [Display(Name = "Is Active")]
        public bool IsActive => Status != BookingStatus.Cancelled;
    }

    public enum BookingStatus
    {
        Pending = 0,
        Confirmed = 1,
        CheckedIn = 2,
        CheckedOut = 3,
        Cancelled = 4,
        NoShow = 5
    }

    public enum BookingType
    {
        Hotel = 0,
        Flight = 1,
        Package = 2,
        Activity = 3,
        Restaurant = 4
    }
}
