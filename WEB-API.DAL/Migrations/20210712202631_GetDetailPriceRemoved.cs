using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.DAL.Migrations
{
    public partial class GetDetailPriceRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop function GetDetailPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
