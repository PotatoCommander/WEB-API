using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.DAL.Data.Migrations
{
    public partial class ProductChangedV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfProduction",
                table: "Products",
                newName: "CreationTime");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)4, 3, (byte)3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)4, 1, (byte)11 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)2, 7, (byte)10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)4, 1, (byte)6 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)3, 1, (byte)9 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)4, 1, (byte)4 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)4, 3, (byte)3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)2, 2, (byte)3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Products",
                newName: "DateOfProduction");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)3, 2, (byte)2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)3, 0, (byte)10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)1, 6, (byte)9 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)3, 0, (byte)5 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)2, 0, (byte)8 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)3, 0, (byte)3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)3, 2, (byte)2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AgeRating", "Category", "Genre" },
                values: new object[] { (byte)1, 1, (byte)2 });
        }
    }
}
