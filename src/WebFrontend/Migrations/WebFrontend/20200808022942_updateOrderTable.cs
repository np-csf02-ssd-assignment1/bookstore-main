using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrontend.Migrations.WebFrontend
{
    public partial class updateOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_PaymentType_PaymentID",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "OrderList");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PaymentID",
                table: "OrderList",
                newName: "IX_OrderList_PaymentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderList",
                table: "OrderList",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderList_PaymentType_PaymentID",
                table: "OrderList",
                column: "PaymentID",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderList_PaymentType_PaymentID",
                table: "OrderList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderList",
                table: "OrderList");

            migrationBuilder.RenameTable(
                name: "OrderList",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_OrderList_PaymentID",
                table: "Order",
                newName: "IX_Order_PaymentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_PaymentType_PaymentID",
                table: "Order",
                column: "PaymentID",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
