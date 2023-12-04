using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla.MagicApi.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenities", "CreatedDate", "Details", "ImageURL", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Swimming pool, jacuzzi, sauna, gym", new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4396), "This is a beautiful villa with stunning views.", "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop", "Villa 1", 4, 1000.0, 2000, new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4409) },
                    { 2, "Garden, barbecue, fireplace", new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4415), "This is a cozy villa with a private garden.", "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop", "Villa 2", 2, 800.0, 1500, new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4416) },
                    { 3, "Rooftop terrace, city views, outdoor kitchen", new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4419), "This is a modern villa with a rooftop terrace.", "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop", "Villa 3", 6, 1200.0, 2500, new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4419) },
                    { 4, "Private pool, ocean views, butler service", new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4422), "This is a luxurious villa with a private pool.", "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop", "Villa 4", 8, 1500.0, 3000, new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4422) },
                    { 5, "Fireplace, wood-burning oven, hammock", new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4425), "This is a charming villa with a rustic feel.", "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop", "Villa 5", 3, 700.0, 1200, new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4425) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
