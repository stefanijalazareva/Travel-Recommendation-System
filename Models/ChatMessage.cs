using System.ComponentModel.DataAnnotations;

namespace TravelRecommendationSystem.Models;

public class ChatMessage
{
    [Required]
    [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
    public string Content { get; set; } = string.Empty;
}

public class ChatResponse
{
    public string Response { get; set; } = string.Empty;
    public bool Success { get; set; } = true;
    public string? ErrorMessage { get; set; }
}
