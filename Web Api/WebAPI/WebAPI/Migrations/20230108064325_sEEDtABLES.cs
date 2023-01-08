using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class sEEDtABLES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "Area", "CreatedDate", "Details", "ImageURl", "ModifiedDate", "Name", "Occupancy" },
                values: new object[,]
                {
                    { 1, "", 500, new DateTime(2023, 1, 8, 12, 13, 25, 676, DateTimeKind.Local).AddTicks(6803), "House 1", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "House 1", 5 },
                    { 2, "", 1000, new DateTime(2023, 1, 8, 12, 13, 25, 676, DateTimeKind.Local).AddTicks(6813), "House 2", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "House 2", 10 }
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
        }
    }
}
