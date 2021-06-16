using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.DAL.Data.Migrations
{
    public partial class UserChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewEmail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewEmail",
                table: "AspNetUsers");
        }
    }
}
