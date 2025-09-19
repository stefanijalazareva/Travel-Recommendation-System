using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelRecommendationSystem.Models;

namespace TravelRecommendationSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }
        public DbSet<DestinationImage> DestinationImages { get; set; }
        public DbSet<DestinationTag> DestinationTags { get; set; }
        public DbSet<ReviewHelpful> ReviewHelpful { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure decimal precision
            builder.Entity<Destination>()
                .Property(e => e.AverageRating)
                .HasColumnType("decimal(3,2)");

            builder.Entity<Destination>()
                .Property(e => e.Latitude)
                .HasColumnType("decimal(9,6)");

            builder.Entity<Destination>()
                .Property(e => e.Longitude)
                .HasColumnType("decimal(9,6)");

            builder.Entity<Attraction>()
                .Property(e => e.AverageRating)
                .HasColumnType("decimal(3,2)");

            builder.Entity<Attraction>()
                .Property(e => e.Latitude)
                .HasColumnType("decimal(9,6)");

            builder.Entity<Attraction>()
                .Property(e => e.Longitude)
                .HasColumnType("decimal(9,6)");

            builder.Entity<Attraction>()
                .Property(e => e.EntryFee)
                .HasColumnType("decimal(10,2)");

            builder.Entity<Booking>()
                .Property(e => e.TotalAmount)
                .HasColumnType("decimal(10,2)");

            // Configure relationships
            builder.Entity<UserFavorite>()
                .HasIndex(e => new { e.UserId, e.DestinationId })
                .IsUnique();

            builder.Entity<UserPreferences>()
                .HasIndex(e => e.UserId)
                .IsUnique();

            builder.Entity<ReviewHelpful>()
                .HasIndex(e => new { e.ReviewId, e.UserId })
                .IsUnique();

            builder.Entity<DestinationImage>()
                .HasIndex(e => new { e.DestinationId, e.SortOrder });

            // Configure cascade deletes
            builder.Entity<Review>()
                .HasOne(e => e.Destination)
                .WithMany(e => e.Reviews)
                .HasForeignKey(e => e.DestinationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Review>()
                .HasOne(e => e.Attraction)
                .WithMany(e => e.Reviews)
                .HasForeignKey(e => e.AttractionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Booking>()
                .HasOne(e => e.Destination)
                .WithMany(e => e.Bookings)
                .HasForeignKey(e => e.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Seed destinations
            builder.Entity<Destination>().HasData(
                new Destination
                {
                    Id = 1,
                    Name = "Paris",
                    Country = "France",
                    Region = "Île-de-France",
                    Description = "The City of Light, known for its art, fashion, gastronomy, and culture. Home to iconic landmarks like the Eiffel Tower and Louvre Museum.",
                    ImageUrl = "https://images.unsplash.com/photo-1502602898536-47ad22581b52?w=800",
                    Latitude = 48.8566m,
                    Longitude = 2.3522m,
                    Climate = "Temperate oceanic",
                    BestTimeToVisit = "April to June, September to October",
                    AveragePriceLevel = PriceLevel.Expensive,
                    AverageRating = 4.6m,
                    TotalReviews = 1245,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 2,
                    Name = "Tokyo",
                    Country = "Japan",
                    Region = "Kanto",
                    Description = "A vibrant metropolis blending ultra-modern and traditional elements. Experience cutting-edge technology alongside ancient temples.",
                    ImageUrl = "https://images.unsplash.com/photo-1540959733332-eab4deabeeaf?w=800",
                    Latitude = 35.6762m,
                    Longitude = 139.6503m,
                    Climate = "Humid subtropical",
                    BestTimeToVisit = "March to May, September to November",
                    AveragePriceLevel = PriceLevel.Expensive,
                    AverageRating = 4.8m,
                    TotalReviews = 987,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 3,
                    Name = "Santorini",
                    Country = "Greece",
                    Region = "South Aegean",
                    Description = "A stunning Greek island known for its white-washed buildings, blue-domed churches, and breathtaking sunsets over the Aegean Sea.",
                    ImageUrl = "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=800",
                    Latitude = 36.3932m,
                    Longitude = 25.4615m,
                    Climate = "Mediterranean",
                    BestTimeToVisit = "April to early November",
                    AveragePriceLevel = PriceLevel.Expensive,
                    AverageRating = 4.7m,
                    TotalReviews = 756,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 4,
                    Name = "Bali",
                    Country = "Indonesia",
                    Region = "Lesser Sunda Islands",
                    Description = "A tropical paradise offering beautiful beaches, ancient temples, lush rice terraces, and a rich cultural heritage.",
                    ImageUrl = "https://images.unsplash.com/photo-1537953773345-d172ccf13cf1?w=800",
                    Latitude = -8.3405m,
                    Longitude = 115.0920m,
                    Climate = "Tropical",
                    BestTimeToVisit = "April to October",
                    AveragePriceLevel = PriceLevel.Moderate,
                    AverageRating = 4.5m,
                    TotalReviews = 1123,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                // European Cities
                new Destination
                {
                    Id = 5,
                    Name = "Rome",
                    Country = "Italy",
                    Region = "Lazio",
                    Description = "The Eternal City, home to ancient Roman ruins, Vatican City, incredible art, and world-renowned cuisine.",
                    ImageUrl = "https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=800",
                    Latitude = 41.9028m,
                    Longitude = 12.4964m,
                    Climate = "Mediterranean",
                    BestTimeToVisit = "April to June, September to October",
                    AveragePriceLevel = PriceLevel.Expensive,
                    AverageRating = 4.7m,
                    TotalReviews = 2156,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 6,
                    Name = "Barcelona",
                    Country = "Spain",
                    Region = "Catalonia",
                    Description = "A vibrant city known for Gaudí's architecture, beautiful beaches, rich culture, and amazing nightlife.",
                    ImageUrl = "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800",
                    Latitude = 41.3851m,
                    Longitude = 2.1734m,
                    Climate = "Mediterranean",
                    BestTimeToVisit = "May to June, September to October",
                    AveragePriceLevel = PriceLevel.Moderate,
                    AverageRating = 4.6m,
                    TotalReviews = 1876,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 7,
                    Name = "Amsterdam",
                    Country = "Netherlands",
                    Region = "North Holland",
                    Description = "A charming city famous for its canals, museums, vibrant culture, and liberal atmosphere.",
                    ImageUrl = "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800",
                    Latitude = 52.3676m,
                    Longitude = 4.9041m,
                    Climate = "Oceanic",
                    BestTimeToVisit = "April to May, September to November",
                    AveragePriceLevel = PriceLevel.Expensive,
                    AverageRating = 4.5m,
                    TotalReviews = 1432,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 8,
                    Name = "London",
                    Country = "United Kingdom",
                    Region = "England",
                    Description = "A historic and cosmopolitan capital offering world-class museums, theaters, royal palaces, and diverse neighborhoods.",
                    ImageUrl = "https://images.unsplash.com/photo-1513635269975-59663e0ac1ad?w=800",
                    Latitude = 51.5074m,
                    Longitude = -0.1278m,
                    Climate = "Temperate oceanic",
                    BestTimeToVisit = "May to September",
                    AveragePriceLevel = PriceLevel.Expensive,
                    AverageRating = 4.4m,
                    TotalReviews = 3421,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 9,
                    Name = "Vienna",
                    Country = "Austria",
                    Region = "Vienna",
                    Description = "Imperial elegance meets modern culture in this musical capital famous for its coffee houses, palaces, and classical heritage.",
                    ImageUrl = "https://images.unsplash.com/photo-1516550135131-fe3dcb0bedc7?w=800",
                    Latitude = 48.2082m,
                    Longitude = 16.3738m,
                    Climate = "Continental",
                    BestTimeToVisit = "April to May, September to October",
                    AveragePriceLevel = PriceLevel.Moderate,
                    AverageRating = 4.6m,
                    TotalReviews = 987,
                    IsFeatured = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 10,
                    Name = "Prague",
                    Country = "Czech Republic",
                    Region = "Prague",
                    Description = "A fairytale city with stunning medieval architecture, charming bridges, and vibrant nightlife at affordable prices.",
                    ImageUrl = "https://images.unsplash.com/photo-1541849546-216549ae216d?w=800",
                    Latitude = 50.0755m,
                    Longitude = 14.4378m,
                    Climate = "Continental",
                    BestTimeToVisit = "March to May, September to November",
                    AveragePriceLevel = PriceLevel.Budget,
                    AverageRating = 4.7m,
                    TotalReviews = 1654,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 11,
                    Name = "Berlin",
                    Country = "Germany",
                    Region = "Berlin",
                    Description = "A city where history meets cutting-edge culture, famous for its museums, nightlife, and contemporary art scene.",
                    ImageUrl = "https://images.unsplash.com/photo-1587330979470-3861ff3014e4?w=800",
                    Latitude = 52.5200m,
                    Longitude = 13.4050m,
                    Climate = "Continental",
                    BestTimeToVisit = "May to September",
                    AveragePriceLevel = PriceLevel.Moderate,
                    AverageRating = 4.3m,
                    TotalReviews = 2187,
                    IsFeatured = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 12,
                    Name = "Florence",
                    Country = "Italy",
                    Region = "Tuscany",
                    Description = "The birthplace of Renaissance art and culture, featuring world-class museums, stunning architecture, and Tuscan cuisine.",
                    ImageUrl = "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800",
                    Latitude = 43.7696m,
                    Longitude = 11.2558m,
                    Climate = "Mediterranean",
                    BestTimeToVisit = "April to June, September to October",
                    AveragePriceLevel = PriceLevel.Moderate,
                    AverageRating = 4.8m,
                    TotalReviews = 1321,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 13,
                    Name = "Budapest",
                    Country = "Hungary",
                    Region = "Central Hungary",
                    Description = "The Pearl of the Danube, famous for its thermal baths, stunning architecture, and vibrant ruin bar scene.",
                    ImageUrl = "https://images.unsplash.com/photo-1541849546-216549ae216d?w=800",
                    Latitude = 47.4979m,
                    Longitude = 19.0402m,
                    Climate = "Continental",
                    BestTimeToVisit = "March to May, September to November",
                    AveragePriceLevel = PriceLevel.Budget,
                    AverageRating = 4.5m,
                    TotalReviews = 1098,
                    IsFeatured = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 14,
                    Name = "Lisbon",
                    Country = "Portugal",
                    Region = "Lisbon",
                    Description = "A coastal capital with colorful architecture, historic trams, vibrant neighborhoods, and incredible seafood.",
                    ImageUrl = "https://images.unsplash.com/photo-1555881400-74d7acaacd8b?w=800",
                    Latitude = 38.7223m,
                    Longitude = -9.1393m,
                    Climate = "Mediterranean",
                    BestTimeToVisit = "March to May, September to October",
                    AveragePriceLevel = PriceLevel.Moderate,
                    AverageRating = 4.6m,
                    TotalReviews = 876,
                    IsFeatured = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 15,
                    Name = "Stockholm",
                    Country = "Sweden",
                    Region = "Stockholm County",
                    Description = "A stunning Nordic capital built on 14 islands, known for its design, museums, and beautiful archipelago.",
                    ImageUrl = "https://images.unsplash.com/photo-1509356843151-3e7d96241e11?w=800",
                    Latitude = 59.3293m,
                    Longitude = 18.0686m,
                    Climate = "Continental",
                    BestTimeToVisit = "May to September",
                    AveragePriceLevel = PriceLevel.Expensive,
                    AverageRating = 4.4m,
                    TotalReviews = 743,
                    IsFeatured = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 16,
                    Name = "Copenhagen",
                    Country = "Denmark",
                    Region = "Capital Region",
                    Description = "A design-forward Scandinavian capital famous for hygge culture, cycling, innovative cuisine, and colorful harbors.",
                    ImageUrl = "https://images.unsplash.com/photo-1513622470522-26c3c8a854bc?w=800",
                    Latitude = 55.6761m,
                    Longitude = 12.5683m,
                    Climate = "Oceanic",
                    BestTimeToVisit = "May to August",
                    AveragePriceLevel = PriceLevel.Expensive,
                    AverageRating = 4.5m,
                    TotalReviews = 892,
                    IsFeatured = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 17,
                    Name = "Dublin",
                    Country = "Ireland",
                    Region = "Leinster",
                    Description = "A friendly capital known for its literary heritage, traditional pubs, Georgian architecture, and warm hospitality.",
                    ImageUrl = "https://images.unsplash.com/photo-1549918864-48ac978761a4?w=800",
                    Latitude = 53.3498m,
                    Longitude = -6.2603m,
                    Climate = "Temperate oceanic",
                    BestTimeToVisit = "April to June, September to October",
                    AveragePriceLevel = PriceLevel.Moderate,
                    AverageRating = 4.3m,
                    TotalReviews = 654,
                    IsFeatured = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 18,
                    Name = "Edinburgh",
                    Country = "Scotland",
                    Region = "Scotland",
                    Description = "A medieval city with a dramatic castle, rich history, world-famous festivals, and stunning highland landscapes nearby.",
                    ImageUrl = "https://images.unsplash.com/photo-1549918864-48ac978761a4?w=800",
                    Latitude = 55.9533m,
                    Longitude = -3.1883m,
                    Climate = "Temperate oceanic",
                    BestTimeToVisit = "May to September",
                    AveragePriceLevel = PriceLevel.Moderate,
                    AverageRating = 4.6m,
                    TotalReviews = 567,
                    IsFeatured = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 19,
                    Name = "Zurich",
                    Country = "Switzerland",
                    Region = "Zurich",
                    Description = "A pristine city combining financial prowess with natural beauty, featuring mountains, lakes, and world-class amenities.",
                    ImageUrl = "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800",
                    Latitude = 47.3769m,
                    Longitude = 8.5417m,
                    Climate = "Continental",
                    BestTimeToVisit = "April to June, September to October",
                    AveragePriceLevel = PriceLevel.Expensive,
                    AverageRating = 4.4m,
                    TotalReviews = 432,
                    IsFeatured = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Id = 20,
                    Name = "Athens",
                    Country = "Greece",
                    Region = "Attica",
                    Description = "The cradle of democracy and Western civilization, home to ancient monuments, vibrant neighborhoods, and Mediterranean culture.",
                    ImageUrl = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=800",
                    Latitude = 37.9838m,
                    Longitude = 23.7275m,
                    Climate = "Mediterranean",
                    BestTimeToVisit = "April to June, September to November",
                    AveragePriceLevel = PriceLevel.Budget,
                    AverageRating = 4.2m,
                    TotalReviews = 1234,
                    IsFeatured = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            // Seed destination tags
            builder.Entity<DestinationTag>().HasData(
                // Paris tags
                new DestinationTag { Id = 1, DestinationId = 1, TagName = "Culture" },
                new DestinationTag { Id = 2, DestinationId = 1, TagName = "Art" },
                new DestinationTag { Id = 3, DestinationId = 1, TagName = "History" },
                new DestinationTag { Id = 4, DestinationId = 1, TagName = "Fashion" },
                new DestinationTag { Id = 5, DestinationId = 1, TagName = "Food" },
                
                // Tokyo tags
                new DestinationTag { Id = 6, DestinationId = 2, TagName = "Technology" },
                new DestinationTag { Id = 7, DestinationId = 2, TagName = "Culture" },
                new DestinationTag { Id = 8, DestinationId = 2, TagName = "Food" },
                new DestinationTag { Id = 9, DestinationId = 2, TagName = "Shopping" },
                new DestinationTag { Id = 10, DestinationId = 2, TagName = "Nightlife" },
                
                // Santorini tags
                new DestinationTag { Id = 11, DestinationId = 3, TagName = "Beach" },
                new DestinationTag { Id = 12, DestinationId = 3, TagName = "Romance" },
                new DestinationTag { Id = 13, DestinationId = 3, TagName = "Sunset" },
                new DestinationTag { Id = 14, DestinationId = 3, TagName = "Photography" },
                new DestinationTag { Id = 15, DestinationId = 3, TagName = "Wine" },
                
                // Bali tags
                new DestinationTag { Id = 16, DestinationId = 4, TagName = "Beach" },
                new DestinationTag { Id = 17, DestinationId = 4, TagName = "Nature" },
                new DestinationTag { Id = 18, DestinationId = 4, TagName = "Spiritual" },
                new DestinationTag { Id = 19, DestinationId = 4, TagName = "Adventure" },
                new DestinationTag { Id = 20, DestinationId = 4, TagName = "Wellness" },
                
                // Rome tags
                new DestinationTag { Id = 21, DestinationId = 5, TagName = "History" },
                new DestinationTag { Id = 22, DestinationId = 5, TagName = "Culture" },
                new DestinationTag { Id = 23, DestinationId = 5, TagName = "Art" },
                new DestinationTag { Id = 24, DestinationId = 5, TagName = "Food" },
                new DestinationTag { Id = 25, DestinationId = 5, TagName = "Architecture" },
                
                // Barcelona tags
                new DestinationTag { Id = 26, DestinationId = 6, TagName = "Architecture" },
                new DestinationTag { Id = 27, DestinationId = 6, TagName = "Beach" },
                new DestinationTag { Id = 28, DestinationId = 6, TagName = "Nightlife" },
                new DestinationTag { Id = 29, DestinationId = 6, TagName = "Culture" },
                new DestinationTag { Id = 30, DestinationId = 6, TagName = "Food" },
                
                // Amsterdam tags
                new DestinationTag { Id = 31, DestinationId = 7, TagName = "Culture" },
                new DestinationTag { Id = 32, DestinationId = 7, TagName = "Art" },
                new DestinationTag { Id = 33, DestinationId = 7, TagName = "History" },
                new DestinationTag { Id = 34, DestinationId = 7, TagName = "Nightlife" },
                new DestinationTag { Id = 35, DestinationId = 7, TagName = "Architecture" },
                
                // London tags
                new DestinationTag { Id = 36, DestinationId = 8, TagName = "History" },
                new DestinationTag { Id = 37, DestinationId = 8, TagName = "Culture" },
                new DestinationTag { Id = 38, DestinationId = 8, TagName = "Theater" },
                new DestinationTag { Id = 39, DestinationId = 8, TagName = "Museums" },
                new DestinationTag { Id = 40, DestinationId = 8, TagName = "Shopping" },
                
                // Vienna tags
                new DestinationTag { Id = 41, DestinationId = 9, TagName = "Music" },
                new DestinationTag { Id = 42, DestinationId = 9, TagName = "Culture" },
                new DestinationTag { Id = 43, DestinationId = 9, TagName = "Architecture" },
                new DestinationTag { Id = 44, DestinationId = 9, TagName = "History" },
                new DestinationTag { Id = 45, DestinationId = 9, TagName = "Coffee" },
                
                // Prague tags
                new DestinationTag { Id = 46, DestinationId = 10, TagName = "Architecture" },
                new DestinationTag { Id = 47, DestinationId = 10, TagName = "History" },
                new DestinationTag { Id = 48, DestinationId = 10, TagName = "Nightlife" },
                new DestinationTag { Id = 49, DestinationId = 10, TagName = "Budget" },
                new DestinationTag { Id = 50, DestinationId = 10, TagName = "Culture" },
                
                // Berlin tags
                new DestinationTag { Id = 51, DestinationId = 11, TagName = "History" },
                new DestinationTag { Id = 52, DestinationId = 11, TagName = "Nightlife" },
                new DestinationTag { Id = 53, DestinationId = 11, TagName = "Culture" },
                new DestinationTag { Id = 54, DestinationId = 11, TagName = "Art" },
                new DestinationTag { Id = 55, DestinationId = 11, TagName = "Museums" },
                
                // Florence tags
                new DestinationTag { Id = 56, DestinationId = 12, TagName = "Art" },
                new DestinationTag { Id = 57, DestinationId = 12, TagName = "History" },
                new DestinationTag { Id = 58, DestinationId = 12, TagName = "Architecture" },
                new DestinationTag { Id = 59, DestinationId = 12, TagName = "Food" },
                new DestinationTag { Id = 60, DestinationId = 12, TagName = "Renaissance" },
                
                // Budapest tags
                new DestinationTag { Id = 61, DestinationId = 13, TagName = "Architecture" },
                new DestinationTag { Id = 62, DestinationId = 13, TagName = "Thermal Baths" },
                new DestinationTag { Id = 63, DestinationId = 13, TagName = "Nightlife" },
                new DestinationTag { Id = 64, DestinationId = 13, TagName = "Budget" },
                new DestinationTag { Id = 65, DestinationId = 13, TagName = "History" },
                
                // Lisbon tags
                new DestinationTag { Id = 66, DestinationId = 14, TagName = "Culture" },
                new DestinationTag { Id = 67, DestinationId = 14, TagName = "Architecture" },
                new DestinationTag { Id = 68, DestinationId = 14, TagName = "Food" },
                new DestinationTag { Id = 69, DestinationId = 14, TagName = "History" },
                new DestinationTag { Id = 70, DestinationId = 14, TagName = "Ocean" },
                
                // Stockholm tags
                new DestinationTag { Id = 71, DestinationId = 15, TagName = "Design" },
                new DestinationTag { Id = 72, DestinationId = 15, TagName = "Museums" },
                new DestinationTag { Id = 73, DestinationId = 15, TagName = "Architecture" },
                new DestinationTag { Id = 74, DestinationId = 15, TagName = "Nature" },
                new DestinationTag { Id = 75, DestinationId = 15, TagName = "Peaceful" },
                
                // Copenhagen tags
                new DestinationTag { Id = 76, DestinationId = 16, TagName = "Design" },
                new DestinationTag { Id = 77, DestinationId = 16, TagName = "Food" },
                new DestinationTag { Id = 78, DestinationId = 16, TagName = "Architecture" },
                new DestinationTag { Id = 79, DestinationId = 16, TagName = "Hygge" },
                new DestinationTag { Id = 80, DestinationId = 16, TagName = "Cycling" },
                
                // Dublin tags
                new DestinationTag { Id = 81, DestinationId = 17, TagName = "Culture" },
                new DestinationTag { Id = 82, DestinationId = 17, TagName = "Literature" },
                new DestinationTag { Id = 83, DestinationId = 17, TagName = "Pubs" },
                new DestinationTag { Id = 84, DestinationId = 17, TagName = "History" },
                new DestinationTag { Id = 85, DestinationId = 17, TagName = "Friendly" },
                
                // Edinburgh tags
                new DestinationTag { Id = 86, DestinationId = 18, TagName = "History" },
                new DestinationTag { Id = 87, DestinationId = 18, TagName = "Architecture" },
                new DestinationTag { Id = 88, DestinationId = 18, TagName = "Festivals" },
                new DestinationTag { Id = 89, DestinationId = 18, TagName = "Culture" },
                new DestinationTag { Id = 90, DestinationId = 18, TagName = "Nature" },
                
                // Zurich tags
                new DestinationTag { Id = 91, DestinationId = 19, TagName = "Nature" },
                new DestinationTag { Id = 92, DestinationId = 19, TagName = "Peaceful" },
                new DestinationTag { Id = 93, DestinationId = 19, TagName = "Luxury" },
                new DestinationTag { Id = 94, DestinationId = 19, TagName = "Mountains" },
                new DestinationTag { Id = 95, DestinationId = 19, TagName = "Clean" },
                
                // Athens tags
                new DestinationTag { Id = 96, DestinationId = 20, TagName = "History" },
                new DestinationTag { Id = 97, DestinationId = 20, TagName = "Culture" },
                new DestinationTag { Id = 98, DestinationId = 20, TagName = "Architecture" },
                new DestinationTag { Id = 99, DestinationId = 20, TagName = "Budget" },
                new DestinationTag { Id = 100, DestinationId = 20, TagName = "Ancient" }
            );

            // Seed attractions
            builder.Entity<Attraction>().HasData(
                new Attraction
                {
                    Id = 1,
                    DestinationId = 1,
                    Name = "Eiffel Tower",
                    Description = "Iconic iron tower and symbol of Paris, offering panoramic views of the city.",
                    Category = "Landmark",
                    ImageUrl = "https://images.unsplash.com/photo-1511739001486-6bfe10ce785f?w=800",
                    Latitude = 48.8584m,
                    Longitude = 2.2945m,
                    Address = "Champ de Mars, 5 Avenue Anatole France, 75007 Paris",
                    OpeningHours = "9:30 AM - 11:45 PM",
                    EntryFee = 26.80m,
                    AverageRating = 4.5m,
                    TotalReviews = 2341,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Attraction
                {
                    Id = 2,
                    DestinationId = 2,
                    Name = "Senso-ji Temple",
                    Description = "Ancient Buddhist temple and Tokyo's oldest, located in Asakusa district.",
                    Category = "Temple",
                    ImageUrl = "https://images.unsplash.com/photo-1545569341-9eb8b30979d9?w=800",
                    Latitude = 35.7148m,
                    Longitude = 139.7967m,
                    Address = "2 Chome-3-1 Asakusa, Taito City, Tokyo",
                    OpeningHours = "6:00 AM - 5:00 PM",
                    EntryFee = 0m,
                    AverageRating = 4.6m,
                    TotalReviews = 1876,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
