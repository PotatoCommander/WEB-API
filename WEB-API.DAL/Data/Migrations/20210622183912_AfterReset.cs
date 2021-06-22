using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.DAL.Data.Migrations
{
    public partial class AfterReset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeRating", "Category", "CreationTime", "Genre", "Name", "Price", "Rating" },
                values: new object[,]
                {
                    { 1, (byte)4, 3, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, "Resident Evil Village", 1748m, 8.6f },
                    { 2, (byte)4, 1, new DateTime(2020, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)11, "Death stranding", 1389m, 9.2f },
                    { 3, (byte)2, 7, new DateTime(2011, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)10, "Terraria", 199m, 8.8f },
                    { 4, (byte)4, 1, new DateTime(2018, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)6, "Battlefield 5", 789m, 7.9f },
                    { 5, (byte)3, 1, new DateTime(2014, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)9, "Hearts of Iron", 259m, 8.5f },
                    { 6, (byte)4, 1, new DateTime(2018, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)4, "Fallout 76", 399m, 5.6f },
                    { 7, (byte)4, 3, new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, "Days Gone", 2599m, 8.2f },
                    { 8, (byte)2, 2, new DateTime(2020, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, "FIFA21", 2100m, 7.5f }
                });
        }
    }
}
