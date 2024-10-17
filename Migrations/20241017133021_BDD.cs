using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BidHub_Poject.Migrations
{
    /// <inheritdoc />
    public partial class BDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDocuments_Products_ProductId",
                table: "ProductDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductPhotos_PhotoId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PhotoId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductDocuments_ProductId",
                table: "ProductDocuments");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductDocuments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PhotoId",
                table: "Products",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocuments_ProductId",
                table: "ProductDocuments",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDocuments_Products_ProductId",
                table: "ProductDocuments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductPhotos_PhotoId",
                table: "Products",
                column: "PhotoId",
                principalTable: "ProductPhotos",
                principalColumn: "PhotoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
