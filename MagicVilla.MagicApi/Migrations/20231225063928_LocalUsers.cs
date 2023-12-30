using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.MagicApi.Migrations
{
    /// <inheritdoc />
    public partial class LocalUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(454), new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(450) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(458), new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(456) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(461), new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(459) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(463), new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(462) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(466), new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(465) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 7, 20, 38, 50, 486, DateTimeKind.Local).AddTicks(761), new DateTime(2023, 12, 7, 20, 38, 50, 486, DateTimeKind.Local).AddTicks(757) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 7, 20, 38, 50, 486, DateTimeKind.Local).AddTicks(767), new DateTime(2023, 12, 7, 20, 38, 50, 486, DateTimeKind.Local).AddTicks(765) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 7, 20, 38, 50, 486, DateTimeKind.Local).AddTicks(770), new DateTime(2023, 12, 7, 20, 38, 50, 486, DateTimeKind.Local).AddTicks(768) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 7, 20, 38, 50, 486, DateTimeKind.Local).AddTicks(773), new DateTime(2023, 12, 7, 20, 38, 50, 486, DateTimeKind.Local).AddTicks(771) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 7, 20, 38, 50, 486, DateTimeKind.Local).AddTicks(775), new DateTime(2023, 12, 7, 20, 38, 50, 486, DateTimeKind.Local).AddTicks(774) });
        }
    }
}
