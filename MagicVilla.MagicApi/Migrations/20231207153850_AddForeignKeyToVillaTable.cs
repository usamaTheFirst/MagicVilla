using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.MagicApi.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "VillaNumbers");

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
    }
}
