using Microsoft.EntityFrameworkCore.Migrations;

namespace ModernMarketTM.Data.Migrations
{
    public partial class discountAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SoldPrice",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoldPrice",
                table: "Orders");
        }
    }
}
