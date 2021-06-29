using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.DAL.Migrations
{
    public partial class ProductBalanceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Balance",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Products");
        }
    }
}
