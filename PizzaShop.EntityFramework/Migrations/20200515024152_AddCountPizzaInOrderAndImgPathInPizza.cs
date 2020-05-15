using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaShop.EntityFramework.Migrations
{
    public partial class AddCountPizzaInOrderAndImgPathInPizza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Pizzas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "OrderHasPrice",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "OrderHasPrice");
        }
    }
}
