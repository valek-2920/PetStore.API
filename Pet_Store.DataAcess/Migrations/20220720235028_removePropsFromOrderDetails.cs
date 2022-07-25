using Microsoft.EntityFrameworkCore.Migrations;

namespace Pet_Store.DataAcess.Migrations
{
    public partial class removePropsFromOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderHeaders_OrderHeaderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_OrderDetails_ProductsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderHeaderId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderHeaderId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "Products",
                newName: "OrderDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductsId",
                table: "Products",
                newName: "IX_Products_OrderDetailsId");

            migrationBuilder.AddColumn<int>(
                name: "OrderHeaderOrderId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderOrderId",
                table: "OrderDetails",
                column: "OrderHeaderOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderHeaders_OrderHeaderOrderId",
                table: "OrderDetails",
                column: "OrderHeaderOrderId",
                principalTable: "OrderHeaders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OrderDetails_OrderDetailsId",
                table: "Products",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "OrderDetailsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderHeaders_OrderHeaderOrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_OrderDetails_OrderDetailsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderHeaderOrderId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderHeaderOrderId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "OrderDetailsId",
                table: "Products",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_OrderDetailsId",
                table: "Products",
                newName: "IX_Products_ProductsId");

            migrationBuilder.AddColumn<int>(
                name: "OrderHeaderId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderHeaders_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId",
                principalTable: "OrderHeaders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OrderDetails_ProductsId",
                table: "Products",
                column: "ProductsId",
                principalTable: "OrderDetails",
                principalColumn: "OrderDetailsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
