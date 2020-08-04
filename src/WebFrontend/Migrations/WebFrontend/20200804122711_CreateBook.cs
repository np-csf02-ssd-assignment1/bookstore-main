using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrontend.Migrations.WebFrontend
{
    public partial class CreateBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Product");
        }
    }
}
