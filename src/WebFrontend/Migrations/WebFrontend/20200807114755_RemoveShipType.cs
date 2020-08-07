using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrontend.Migrations.WebFrontend
{
    public partial class RemoveShipType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShipmentType_ShipmentID",
                table: "Order");

            migrationBuilder.DropTable(
                name: "ShipmentType");

            migrationBuilder.DropIndex(
                name: "IX_Order_ShipmentID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShipmentID",
                table: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShipmentID",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShipmentType",
                columns: table => new
                {
                    ShipmentTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentType", x => x.ShipmentTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShipmentID",
                table: "Order",
                column: "ShipmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShipmentType_ShipmentID",
                table: "Order",
                column: "ShipmentID",
                principalTable: "ShipmentType",
                principalColumn: "ShipmentTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
