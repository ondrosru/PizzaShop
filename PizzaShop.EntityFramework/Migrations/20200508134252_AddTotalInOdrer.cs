using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaShop.EntityFramework.Migrations
{
    public partial class AddTotalInOdrer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Order",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Order");
        }
    }
}
