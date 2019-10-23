using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketPracticingPlatform.Migrations
{
    public partial class MainSub_Products_entity_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainSubProducts",
                columns: table => new
                {
                    MainProductId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", false),
                    SubProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainSubProducts", x => x.MainProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainSubProducts");
        }
    }
}
