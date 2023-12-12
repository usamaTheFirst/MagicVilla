using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.MagicApi.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumbers", x => x.VillaNo);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 7, 18, 50, 42, 998, DateTimeKind.Local).AddTicks(5888), new DateTime(2023, 12, 7, 18, 50, 42, 998, DateTimeKind.Local).AddTicks(5884) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 7, 18, 50, 42, 998, DateTimeKind.Local).AddTicks(5894), new DateTime(2023, 12, 7, 18, 50, 42, 998, DateTimeKind.Local).AddTicks(5891) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 7, 18, 50, 42, 998, DateTimeKind.Local).AddTicks(5897), new DateTime(2023, 12, 7, 18, 50, 42, 998, DateTimeKind.Local).AddTicks(5895) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 7, 18, 50, 42, 998, DateTimeKind.Local).AddTicks(5900), new DateTime(2023, 12, 7, 18, 50, 42, 998, DateTimeKind.Local).AddTicks(5898) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 7, 18, 50, 42, 998, DateTimeKind.Local).AddTicks(5902), new DateTime(2023, 12, 7, 18, 50, 42, 998, DateTimeKind.Local).AddTicks(5901) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 3, 15, 37, 55, 439, DateTimeKind.Local).AddTicks(2098), new DateTime(2023, 12, 3, 15, 37, 55, 439, DateTimeKind.Local).AddTicks(2094) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 3, 15, 37, 55, 439, DateTimeKind.Local).AddTicks(2107), new DateTime(2023, 12, 3, 15, 37, 55, 439, DateTimeKind.Local).AddTicks(2103) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 3, 15, 37, 55, 439, DateTimeKind.Local).AddTicks(2111), new DateTime(2023, 12, 3, 15, 37, 55, 439, DateTimeKind.Local).AddTicks(2109) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 3, 15, 37, 55, 439, DateTimeKind.Local).AddTicks(2114), new DateTime(2023, 12, 3, 15, 37, 55, 439, DateTimeKind.Local).AddTicks(2113) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 3, 15, 37, 55, 439, DateTimeKind.Local).AddTicks(2118), new DateTime(2023, 12, 3, 15, 37, 55, 439, DateTimeKind.Local).AddTicks(2117) });
        }
    }
}
