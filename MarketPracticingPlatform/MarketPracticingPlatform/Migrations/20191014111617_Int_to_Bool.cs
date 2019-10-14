using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketPracticingPlatform.Migrations
{
    public partial class Int_to_Bool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SoldOut",
                table: "Products",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "SoldOut",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
