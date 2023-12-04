using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.MagicApi.Migrations
{
    /// <inheritdoc />
    public partial class seedingDataWithDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4396), new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4409) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4415), new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4416) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4419), new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4419) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4422), new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4422) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4425), new DateTime(2023, 12, 3, 15, 36, 22, 412, DateTimeKind.Local).AddTicks(4425) });
        }
    }
}
