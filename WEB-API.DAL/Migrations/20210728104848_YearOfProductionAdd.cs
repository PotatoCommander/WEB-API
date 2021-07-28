using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.DAL.Migrations
{
    public partial class YearOfProductionAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearOfProduction",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearOfProduction",
                table: "Products");
        }
    }
}
