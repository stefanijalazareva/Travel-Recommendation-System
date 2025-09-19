using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelRecommendationSystem.Data;
using TravelRecommendationSystem.Models;
using System.Text.Json;

namespace TravelRecommendationSystem.Controllers;

public class ChatbotController : Controller
{
    private readonly ApplicationDbContext _context;

    public ChatbotController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> GetResponse([FromBody] ChatMessage message)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Json(new ChatResponse 
                { 
                    Response = "I didn't understand that. Could you please rephrase your question?",
                    Success = false,
                    ErrorMessage = "Invalid message format"
                });
            }

            if (string.IsNullOrWhiteSpace(message.Content))
            {
                return Json(new ChatResponse 
                { 
                    Response = "I didn't understand that. Could you please rephrase your question?",
                    Success = false,
                    ErrorMessage = "Empty message"
                });
            }

            var userMessage = message.Content.ToLower().Trim();
            string botResponse = await GenerateResponse(userMessage);

            return Json(new ChatResponse { Response = botResponse });
        }
        catch (Exception)
        {
            // Log the exception here if you have logging configured
            return Json(new ChatResponse 
            { 
                Response = "I'm having trouble right now. Please try again in a moment.",
                Success = false,
                ErrorMessage = "Server error"
            });
        }
    }

    private async Task<string> GenerateResponse(string message)
    {
        // Greetings
        if (message.Contains("hello") || message.Contains("hi") || message.Contains("hey"))
        {
            return "Hello! 👋 I'm your travel assistant. I can help you discover amazing destinations, find activities, or answer questions about travel. What would you like to explore today?";
        }

        // Help/What can you do
        if (message.Contains("help") || message.Contains("what can you do") || message.Contains("how can you help"))
        {
            return "I can help you with:\n• 🌍 Find destinations by country or activity\n• 🏖️ Recommend beaches, adventures, or cultural sites\n• 🎯 Suggest places based on your interests\n• 📍 Get details about specific destinations\n• ✈️ General travel advice\n\nTry asking something like 'Show me beach destinations' or 'What can I do in Paris?'";
        }

        // Destination search
        if (message.Contains("destination") || message.Contains("place") || message.Contains("country"))
        {
            if (message.Contains("beach"))
            {
                return await GetDestinationsByCategory("Beach");
            }
            else if (message.Contains("adventure") || message.Contains("hiking") || message.Contains("mountain"))
            {
                return await GetDestinationsByCategory("Adventure");
            }
            else if (message.Contains("culture") || message.Contains("history") || message.Contains("museum"))
            {
                return await GetDestinationsByCategory("Culture");
            }
            else if (message.Contains("food") || message.Contains("cuisine") || message.Contains("restaurant"))
            {
                return await GetDestinationsByCategory("Food");
            }
            else if (message.Contains("nature") || message.Contains("wildlife") || message.Contains("park"))
            {
                return await GetDestinationsByCategory("Nature");
            }
            else if (message.Contains("nightlife") || message.Contains("party") || message.Contains("night"))
            {
                return await GetDestinationsByCategory("Nightlife");
            }
            else if (message.Contains("romance") || message.Contains("romantic") || message.Contains("honeymoon"))
            {
                return await GetDestinationsByCategory("Romance");
            }
        }

        // Specific destination queries
        if (message.Contains("paris") || message.Contains("france"))
        {
            return await GetDestinationInfo("Paris");
        }
        else if (message.Contains("tokyo") || message.Contains("japan"))
        {
            return await GetDestinationInfo("Tokyo");
        }
        else if (message.Contains("santorini") || message.Contains("greece"))
        {
            return await GetDestinationInfo("Santorini");
        }
        else if (message.Contains("bali") || message.Contains("indonesia"))
        {
            return await GetDestinationInfo("Bali");
        }

        // Activity-based recommendations
        if (message.Contains("what to do") || message.Contains("activities") || message.Contains("attractions"))
        {
            return "Here are some popular activity categories you can explore:\n\n🏖️ **Beach & Water Sports**\n🏔️ **Adventure & Hiking**\n🏛️ **Culture & History**\n🍽️ **Food & Cuisine**\n🌿 **Nature & Wildlife**\n🌃 **Nightlife & Entertainment**\n💕 **Romance & Relaxation**\n\nWhich type of activity interests you most?";
        }

        // Budget questions
        if (message.Contains("budget") || message.Contains("cheap") || message.Contains("expensive") || message.Contains("cost"))
        {
            return "💰 Here are some budget-friendly tips:\n\n**Budget Destinations:** Look for places with lower cost of living\n**Best Times:** Travel during shoulder seasons for better prices\n**Accommodations:** Consider hostels, guesthouses, or apartments\n**Local Transport:** Use public transportation\n**Food:** Try local street food and markets\n\nWould you like recommendations for budget-friendly destinations?";
        }

        // Weather/Best time to visit
        if (message.Contains("weather") || message.Contains("best time") || message.Contains("season"))
        {
            return "🌤️ **Best Times to Travel:**\n\n**Spring (Mar-May):** Mild weather, fewer crowds\n**Summer (Jun-Aug):** Peak season, warm weather\n**Fall (Sep-Nov):** Great weather, fewer tourists\n**Winter (Dec-Feb):** Cold in many places, but great for warm destinations\n\nTell me about a specific destination and I can give you more detailed timing advice!";
        }

        // General travel advice
        if (message.Contains("advice") || message.Contains("tips") || message.Contains("planning"))
        {
            return "✈️ **Travel Planning Tips:**\n\n📅 **Plan Ahead:** Book flights and accommodations early\n📋 **Research:** Check visa requirements and local customs\n💼 **Pack Smart:** Bring essentials and check luggage restrictions\n📱 **Stay Connected:** Download offline maps and translation apps\n🏥 **Stay Safe:** Get travel insurance and keep emergency contacts\n\nWhat specific aspect of travel planning would you like help with?";
        }

        // Farewell
        if (message.Contains("bye") || message.Contains("goodbye") || message.Contains("thank"))
        {
            return "You're welcome! 😊 Safe travels and feel free to ask me anything else about your next adventure. Bon voyage! 🌟";
        }

        // Default response for unrecognized queries
        return "I'd love to help you plan your next adventure! 🌟 Try asking me about:\n\n• Specific destinations (Paris, Tokyo, Bali, etc.)\n• Types of activities (beach, adventure, culture, food)\n• Travel planning tips\n• Budget advice\n• Best times to visit places\n\nWhat interests you most?";
    }

    private async Task<string> GetDestinationsByCategory(string category)
    {
        var destinations = await _context.Destinations
            .Include(d => d.Tags)
            .Where(d => d.IsActive && d.Tags.Any(t => t.TagName.ToLower().Contains(category.ToLower())))
            .OrderByDescending(d => d.AverageRating)
            .Take(3)
            .ToListAsync();

        if (destinations.Any())
        {
            var response = $"🌟 **Top {category} Destinations:**\n\n";
            foreach (var dest in destinations)
            {
                var rating = new string('⭐', Math.Min(5, (int)Math.Round(dest.AverageRating)));
                response += $"**{dest.Name}, {dest.Country}** {rating}\n{dest.Description.Substring(0, Math.Min(100, dest.Description.Length))}...\n\n";
            }
            response += "Would you like more details about any of these destinations?";
            return response;
        }
        else
        {
            return $"I don't have any {category.ToLower()} destinations in our database right now. But I'd recommend checking our full destination list for other amazing places to visit!";
        }
    }

    private async Task<string> GetDestinationInfo(string destinationName)
    {
        var destination = await _context.Destinations
            .Include(d => d.Attractions.Where(a => a.IsActive))
            .Include(d => d.Tags)
            .Include(d => d.Reviews.Where(r => r.IsActive))
            .FirstOrDefaultAsync(d => d.Name.ToLower().Contains(destinationName.ToLower()) && d.IsActive);

        if (destination != null)
        {
            var rating = new string('⭐', Math.Min(5, (int)Math.Round(destination.AverageRating)));
            var tags = string.Join(", ", destination.Tags.Take(3).Select(t => t.TagName));
            
            var response = $"✈️ **{destination.Name}, {destination.Country}** {rating}\n\n";
            response += $"📍 {destination.Description}\n\n";
            response += $"🏷️ **Categories:** {tags}\n";
            response += $"🌤️ **Best Time:** {destination.BestTimeToVisit}\n";
            response += $"💭 **Reviews:** {destination.TotalReviews} travelers reviewed this destination\n\n";
            
            if (destination.Attractions.Any())
            {
                response += "🎯 **Top Attractions:**\n";
                foreach (var attraction in destination.Attractions.Take(3))
                {
                    response += $"• {attraction.Name}\n";
                }
                response += "\n";
            }
            
            response += "Want to book a trip or see more details? Check out our destination page!";
            return response;
        }
        else
        {
            return $"I don't have detailed information about {destinationName} yet. Try browsing our destination list or ask me about Paris, Tokyo, Santorini, or Bali - I have lots of info on those! 😊";
        }
    }
}
