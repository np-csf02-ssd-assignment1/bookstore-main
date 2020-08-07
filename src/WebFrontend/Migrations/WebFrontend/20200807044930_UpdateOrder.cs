using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrontend.Migrations.WebFrontend
{
    public partial class UpdateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "ShoppingCartItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "Order",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "Order");
        }
    }
}
