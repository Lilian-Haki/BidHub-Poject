using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BidHub_Poject.Migrations
{
    /// <inheritdoc />
    public partial class biddates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BViewings_Products_ProductsProductId",
                table: "BViewings");

            migrationBuilder.DropIndex(
                name: "IX_BViewings_ProductsProductId",
                table: "BViewings");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "BViewings");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "BViewings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BViewings_ProductId",
                table: "BViewings",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BViewings_Products_ProductId",
                table: "BViewings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BViewings_Products_ProductId",
                table: "BViewings");

            migrationBuilder.DropIndex(
                name: "IX_BViewings_ProductId",
                table: "BViewings");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "BViewings");

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "BViewings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BViewings_ProductsProductId",
                table: "BViewings",
                column: "ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BViewings_Products_ProductsProductId",
                table: "BViewings",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
