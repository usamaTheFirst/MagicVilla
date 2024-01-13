using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.MagicApi.Migrations
{
    /// <inheritdoc />
    public partial class addRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JwtTokenId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Refresh_Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 9, 19, 57, 24, 676, DateTimeKind.Local).AddTicks(4482), new DateTime(2024, 1, 9, 19, 57, 24, 676, DateTimeKind.Local).AddTicks(4477) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 9, 19, 57, 24, 676, DateTimeKind.Local).AddTicks(4487), new DateTime(2024, 1, 9, 19, 57, 24, 676, DateTimeKind.Local).AddTicks(4485) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 9, 19, 57, 24, 676, DateTimeKind.Local).AddTicks(4490), new DateTime(2024, 1, 9, 19, 57, 24, 676, DateTimeKind.Local).AddTicks(4488) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 9, 19, 57, 24, 676, DateTimeKind.Local).AddTicks(4493), new DateTime(2024, 1, 9, 19, 57, 24, 676, DateTimeKind.Local).AddTicks(4491) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 9, 19, 57, 24, 676, DateTimeKind.Local).AddTicks(4496), new DateTime(2024, 1, 9, 19, 57, 24, 676, DateTimeKind.Local).AddTicks(4495) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2561), new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2556) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2566), new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2564) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2569), new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2568) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2571), new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2574), new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2573) });
        }
    }
}
