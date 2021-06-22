using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.DAL.Migrations
{
    public partial class AverageFuncAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var func =
                "CREATE FUNCTION GetAverage(@Id int)\n" +
                "    RETURNS REAL\nAS\nBEGIN\n" +
                "    declare @bav real\n    select @bav =  AVG (ProductRating) FROM Ratings WHERE ProductId = @Id\n" +
                "    RETURN ISNULL(@bav,0)\nEND\ngo\n\n";
            migrationBuilder.Sql(func);
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Products",
                type: "real",
                nullable: false,
                computedColumnSql: "ApiAdmin.GetAverage([Id])",
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Products",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldComputedColumnSql: "ApiAdmin.GetAverage([Id])");
        }
    }
}
