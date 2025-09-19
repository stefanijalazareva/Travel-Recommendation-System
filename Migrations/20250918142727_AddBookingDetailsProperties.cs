using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelRecommendationSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingDetailsProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccommodationType",
                table: "Bookings",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "Bookings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "Bookings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "FlightIncluded",
                table: "Bookings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SpecialRequests",
                table: "Bookings",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TransferIncluded",
                table: "Bookings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccommodationType",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Adults",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FlightIncluded",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SpecialRequests",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TransferIncluded",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3380), new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3380) });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3385), new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3386) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3136), new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3136) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3141), new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3141) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3145), new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3146) });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3149), new DateTime(2025, 9, 18, 13, 17, 51, 46, DateTimeKind.Utc).AddTicks(3150) });
        }
    }
}
