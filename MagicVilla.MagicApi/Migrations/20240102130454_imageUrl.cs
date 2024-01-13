using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.MagicApi.Migrations
{
    /// <inheritdoc />
    public partial class imageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImageLocalPath",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageLocalPath", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2561), null, new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2556) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageLocalPath", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2566), null, new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2564) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageLocalPath", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2569), null, new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2568) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageLocalPath", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2571), null, new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageLocalPath", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2574), null, new DateTime(2024, 1, 2, 18, 4, 53, 814, DateTimeKind.Local).AddTicks(2573) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLocalPath",
                table: "Villas");

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 30, 19, 15, 6, 697, DateTimeKind.Local).AddTicks(7278), new DateTime(2023, 12, 30, 19, 15, 6, 697, DateTimeKind.Local).AddTicks(7274) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 30, 19, 15, 6, 697, DateTimeKind.Local).AddTicks(7282), new DateTime(2023, 12, 30, 19, 15, 6, 697, DateTimeKind.Local).AddTicks(7281) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 30, 19, 15, 6, 697, DateTimeKind.Local).AddTicks(7285), new DateTime(2023, 12, 30, 19, 15, 6, 697, DateTimeKind.Local).AddTicks(7284) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 30, 19, 15, 6, 697, DateTimeKind.Local).AddTicks(7287), new DateTime(2023, 12, 30, 19, 15, 6, 697, DateTimeKind.Local).AddTicks(7286) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 12, 30, 19, 15, 6, 697, DateTimeKind.Local).AddTicks(7290), new DateTime(2023, 12, 30, 19, 15, 6, 697, DateTimeKind.Local).AddTicks(7289) });
        }
    }
}
