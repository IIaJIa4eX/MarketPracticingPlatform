using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketPracticingPlatform.Migrations
{
    public partial class added_new_entity_mainsubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainSubProducts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    MainProductId = table.Column<int>(nullable: false),
                    SubProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainSubProducts", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainSubProducts");
        }
    }
}
