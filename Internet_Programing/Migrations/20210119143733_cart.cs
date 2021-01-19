using Microsoft.EntityFrameworkCore.Migrations;

namespace Internet_Programing.Migrations
{
    public partial class cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Customer_CustomerId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CustomerId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.ProductsId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_CartProduct_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartProduct_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_CustomerId",
                table: "CartProduct",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProduct");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CustomerId",
                table: "Product",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Customer_CustomerId",
                table: "Product",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
