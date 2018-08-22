using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModernMarketTM.Data.Migrations
{
    public partial class TypesAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TypeId",
                table: "Categories",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Types_TypeId",
                table: "Categories",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Types_TypeId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Categories_TypeId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Categories");
        }
    }
}
