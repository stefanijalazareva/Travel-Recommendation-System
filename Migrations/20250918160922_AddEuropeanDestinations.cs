using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelRecommendationSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddEuropeanDestinations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(2135), new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(2136) });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(2142), new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(2142) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1330), new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1331) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1338), new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1338) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1344), new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1344) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1349), new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1350) });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "AveragePriceLevel", "AverageRating", "BestTimeToVisit", "Climate", "Country", "CreatedAt", "Description", "ImageUrl", "IsActive", "IsFeatured", "Latitude", "Longitude", "Name", "Region", "TotalReviews", "UpdatedAt" },
                values: new object[,]
                {
                    { 5, 3, 4.7m, "April to June, September to October", "Mediterranean", "Italy", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1355), "The Eternal City, home to ancient Roman ruins, Vatican City, incredible art, and world-renowned cuisine.", "https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=800", true, true, 41.9028m, 12.4964m, "Rome", "Lazio", 2156, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1355) },
                    { 6, 2, 4.6m, "May to June, September to October", "Mediterranean", "Spain", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1361), "A vibrant city known for Gaudí's architecture, beautiful beaches, rich culture, and amazing nightlife.", "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800", true, true, 41.3851m, 2.1734m, "Barcelona", "Catalonia", 1876, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1361) },
                    { 7, 3, 4.5m, "April to May, September to November", "Oceanic", "Netherlands", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1366), "A charming city famous for its canals, museums, vibrant culture, and liberal atmosphere.", "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800", true, true, 52.3676m, 4.9041m, "Amsterdam", "North Holland", 1432, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1366) },
                    { 8, 3, 4.4m, "May to September", "Temperate oceanic", "United Kingdom", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1438), "A historic and cosmopolitan capital offering world-class museums, theaters, royal palaces, and diverse neighborhoods.", "https://images.unsplash.com/photo-1513635269975-59663e0ac1ad?w=800", true, true, 51.5074m, -0.1278m, "London", "England", 3421, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1439) },
                    { 9, 2, 4.6m, "April to May, September to October", "Continental", "Austria", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1444), "Imperial elegance meets modern culture in this musical capital famous for its coffee houses, palaces, and classical heritage.", "https://images.unsplash.com/photo-1516550135131-fe3dcb0bedc7?w=800", true, false, 48.2082m, 16.3738m, "Vienna", "Vienna", 987, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1444) },
                    { 10, 1, 4.7m, "March to May, September to November", "Continental", "Czech Republic", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1449), "A fairytale city with stunning medieval architecture, charming bridges, and vibrant nightlife at affordable prices.", "https://images.unsplash.com/photo-1541849546-216549ae216d?w=800", true, true, 50.0755m, 14.4378m, "Prague", "Prague", 1654, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1450) },
                    { 11, 2, 4.3m, "May to September", "Continental", "Germany", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1454), "A city where history meets cutting-edge culture, famous for its museums, nightlife, and contemporary art scene.", "https://images.unsplash.com/photo-1587330979470-3861ff3014e4?w=800", true, false, 52.5200m, 13.4050m, "Berlin", "Berlin", 2187, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1455) },
                    { 12, 2, 4.8m, "April to June, September to October", "Mediterranean", "Italy", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1460), "The birthplace of Renaissance art and culture, featuring world-class museums, stunning architecture, and Tuscan cuisine.", "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800", true, true, 43.7696m, 11.2558m, "Florence", "Tuscany", 1321, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1460) },
                    { 13, 1, 4.5m, "March to May, September to November", "Continental", "Hungary", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1465), "The Pearl of the Danube, famous for its thermal baths, stunning architecture, and vibrant ruin bar scene.", "https://images.unsplash.com/photo-1541849546-216549ae216d?w=800", true, false, 47.4979m, 19.0402m, "Budapest", "Central Hungary", 1098, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1466) },
                    { 14, 2, 4.6m, "March to May, September to October", "Mediterranean", "Portugal", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1471), "A coastal capital with colorful architecture, historic trams, vibrant neighborhoods, and incredible seafood.", "https://images.unsplash.com/photo-1555881400-74d7acaacd8b?w=800", true, false, 38.7223m, -9.1393m, "Lisbon", "Lisbon", 876, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1471) },
                    { 15, 3, 4.4m, "May to September", "Continental", "Sweden", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1476), "A stunning Nordic capital built on 14 islands, known for its design, museums, and beautiful archipelago.", "https://images.unsplash.com/photo-1509356843151-3e7d96241e11?w=800", true, false, 59.3293m, 18.0686m, "Stockholm", "Stockholm County", 743, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1477) },
                    { 16, 3, 4.5m, "May to August", "Oceanic", "Denmark", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1482), "A design-forward Scandinavian capital famous for hygge culture, cycling, innovative cuisine, and colorful harbors.", "https://images.unsplash.com/photo-1513622470522-26c3c8a854bc?w=800", true, false, 55.6761m, 12.5683m, "Copenhagen", "Capital Region", 892, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1483) },
                    { 17, 2, 4.3m, "April to June, September to October", "Temperate oceanic", "Ireland", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1488), "A friendly capital known for its literary heritage, traditional pubs, Georgian architecture, and warm hospitality.", "https://images.unsplash.com/photo-1549918864-48ac978761a4?w=800", true, false, 53.3498m, -6.2603m, "Dublin", "Leinster", 654, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1488) },
                    { 18, 2, 4.6m, "May to September", "Temperate oceanic", "Scotland", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1493), "A medieval city with a dramatic castle, rich history, world-famous festivals, and stunning highland landscapes nearby.", "https://images.unsplash.com/photo-1549918864-48ac978761a4?w=800", true, false, 55.9533m, -3.1883m, "Edinburgh", "Scotland", 567, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1494) },
                    { 19, 3, 4.4m, "April to June, September to October", "Continental", "Switzerland", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1499), "A pristine city combining financial prowess with natural beauty, featuring mountains, lakes, and world-class amenities.", "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800", true, false, 47.3769m, 8.5417m, "Zurich", "Zurich", 432, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1499) },
                    { 20, 1, 4.2m, "April to June, September to November", "Mediterranean", "Greece", new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1504), "The cradle of democracy and Western civilization, home to ancient monuments, vibrant neighborhoods, and Mediterranean culture.", "https://images.unsplash.com/photo-1555993539-1732b0258235?w=800", true, false, 37.9838m, 23.7275m, "Athens", "Attica", 1234, new DateTime(2025, 9, 18, 16, 9, 21, 390, DateTimeKind.Utc).AddTicks(1505) }
                });

            migrationBuilder.InsertData(
                table: "DestinationTags",
                columns: new[] { "Id", "DestinationId", "TagName" },
                values: new object[,]
                {
                    { 21, 5, "History" },
                    { 22, 5, "Culture" },
                    { 23, 5, "Art" },
                    { 24, 5, "Food" },
                    { 25, 5, "Architecture" },
                    { 26, 6, "Architecture" },
                    { 27, 6, "Beach" },
                    { 28, 6, "Nightlife" },
                    { 29, 6, "Culture" },
                    { 30, 6, "Food" },
                    { 31, 7, "Culture" },
                    { 32, 7, "Art" },
                    { 33, 7, "History" },
                    { 34, 7, "Nightlife" },
                    { 35, 7, "Architecture" },
                    { 36, 8, "History" },
                    { 37, 8, "Culture" },
                    { 38, 8, "Theater" },
                    { 39, 8, "Museums" },
                    { 40, 8, "Shopping" },
                    { 41, 9, "Music" },
                    { 42, 9, "Culture" },
                    { 43, 9, "Architecture" },
                    { 44, 9, "History" },
                    { 45, 9, "Coffee" },
                    { 46, 10, "Architecture" },
                    { 47, 10, "History" },
                    { 48, 10, "Nightlife" },
                    { 49, 10, "Budget" },
                    { 50, 10, "Culture" },
                    { 51, 11, "History" },
                    { 52, 11, "Nightlife" },
                    { 53, 11, "Culture" },
                    { 54, 11, "Art" },
                    { 55, 11, "Museums" },
                    { 56, 12, "Art" },
                    { 57, 12, "History" },
                    { 58, 12, "Architecture" },
                    { 59, 12, "Food" },
                    { 60, 12, "Renaissance" },
                    { 61, 13, "Architecture" },
                    { 62, 13, "Thermal Baths" },
                    { 63, 13, "Nightlife" },
                    { 64, 13, "Budget" },
                    { 65, 13, "History" },
                    { 66, 14, "Culture" },
                    { 67, 14, "Architecture" },
                    { 68, 14, "Food" },
                    { 69, 14, "History" },
                    { 70, 14, "Ocean" },
                    { 71, 15, "Design" },
                    { 72, 15, "Museums" },
                    { 73, 15, "Architecture" },
                    { 74, 15, "Nature" },
                    { 75, 15, "Peaceful" },
                    { 76, 16, "Design" },
                    { 77, 16, "Food" },
                    { 78, 16, "Architecture" },
                    { 79, 16, "Hygge" },
                    { 80, 16, "Cycling" },
                    { 81, 17, "Culture" },
                    { 82, 17, "Literature" },
                    { 83, 17, "Pubs" },
                    { 84, 17, "History" },
                    { 85, 17, "Friendly" },
                    { 86, 18, "History" },
                    { 87, 18, "Architecture" },
                    { 88, 18, "Festivals" },
                    { 89, 18, "Culture" },
                    { 90, 18, "Nature" },
                    { 91, 19, "Nature" },
                    { 92, 19, "Peaceful" },
                    { 93, 19, "Luxury" },
                    { 94, 19, "Mountains" },
                    { 95, 19, "Clean" },
                    { 96, 20, "History" },
                    { 97, 20, "Culture" },
                    { 98, 20, "Architecture" },
                    { 99, 20, "Budget" },
                    { 100, 20, "Ancient" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "DestinationTags",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8827), new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8827) });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8832), new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8833) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8507), new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8507) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8512), new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8512) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8516), new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8517) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8520), new DateTime(2025, 9, 18, 14, 27, 26, 984, DateTimeKind.Utc).AddTicks(8521) });
        }
    }
}
