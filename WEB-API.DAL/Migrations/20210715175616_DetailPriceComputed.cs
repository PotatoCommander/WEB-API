using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.DAL.Migrations
{
    public partial class DetailPriceComputed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var func = @"CREATE FUNCTION DetailPrice(@Id int, @quantity bigint)
                            RETURNS DECIMAL
                        AS
                        BEGIN
                            declare @price decimal
                            select @price =  @quantity * Price FROM Products WHERE Id = @Id
                            RETURN ISNULL(@price,0)
                        END
                        go";
            migrationBuilder.Sql(func);
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "dbo.DetailPrice([ProductId], [Quantity])",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "ApiAdmin.DetailPrice([ProductId], [Quantity])");
        }
    }
}
