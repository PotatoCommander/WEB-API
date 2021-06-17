using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.DAL.Data.Migrations
{
    public partial class ProductAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<byte>(type: "tinyint", nullable: false),
                    AgeRating = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    DateOfProduction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeRating", "Category", "DateOfProduction", "Genre", "Name", "Price", "Rating" },
                values: new object[,]
                {
                    { 1, (byte)3, 2, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, "Resident Evil Village", 1748m, 8.6f },
                    { 2, (byte)3, 0, new DateTime(2020, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)10, "Death stranding", 1389m, 9.2f },
                    { 3, (byte)1, 6, new DateTime(2011, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)9, "Terraria", 199m, 8.8f },
                    { 4, (byte)3, 0, new DateTime(2018, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)5, "Battlefield 5", 789m, 7.9f },
                    { 5, (byte)2, 0, new DateTime(2014, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)8, "Hearts of Iron", 259m, 8.5f },
                    { 6, (byte)3, 0, new DateTime(2018, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, "Fallout 76", 399m, 5.6f },
                    { 7, (byte)3, 2, new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, "Days Gone", 2599m, 8.2f },
                    { 8, (byte)1, 1, new DateTime(2020, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, "FIFA21", 2100m, 7.5f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
