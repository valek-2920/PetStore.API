using Microsoft.EntityFrameworkCore.Migrations;

namespace Pet_Store.DataAcess.Migrations
{
    public partial class fixDataTypeFromSC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Subtotal",
                table: "ShoppingCarts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "ShoppingCarts");
        }
    }
}
