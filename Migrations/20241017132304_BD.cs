using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BidHub_Poject.Migrations
{
    /// <inheritdoc />
    public partial class BD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctioneers_Companies_CompanyId",
                table: "Auctioneers");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctioneers_Products_ProductId",
                table: "Auctioneers");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctioneers_Users_UserId",
                table: "Auctioneers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductDocuments_DocumentId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DocumentId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Auctioneers_CompanyId",
                table: "Auctioneers");

            migrationBuilder.DropIndex(
                name: "IX_Auctioneers_ProductId",
                table: "Auctioneers");

            migrationBuilder.DropIndex(
                name: "IX_Auctioneers_UserId",
                table: "Auctioneers");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Auctioneers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Auctioneers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Auctioneers");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuctioneersCompany",
                columns: table => new
                {
                    AuctioneersAuctioneerId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctioneersCompany", x => new { x.AuctioneersAuctioneerId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_AuctioneersCompany_Auctioneers_AuctioneersAuctioneerId",
                        column: x => x.AuctioneersAuctioneerId,
                        principalTable: "Auctioneers",
                        principalColumn: "AuctioneerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctioneersCompany_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuctioneersProducts",
                columns: table => new
                {
                    AuctioneersAuctioneerId = table.Column<int>(type: "int", nullable: false),
                    ProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctioneersProducts", x => new { x.AuctioneersAuctioneerId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_AuctioneersProducts_Auctioneers_AuctioneersAuctioneerId",
                        column: x => x.AuctioneersAuctioneerId,
                        principalTable: "Auctioneers",
                        principalColumn: "AuctioneerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctioneersProducts_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuctioneersUsers",
                columns: table => new
                {
                    AuctioneersAuctioneerId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctioneersUsers", x => new { x.AuctioneersAuctioneerId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_AuctioneersUsers_Auctioneers_AuctioneersAuctioneerId",
                        column: x => x.AuctioneersAuctioneerId,
                        principalTable: "Auctioneers",
                        principalColumn: "AuctioneerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctioneersUsers_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocuments_ProductId",
                table: "ProductDocuments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctioneersCompany_CompanyId",
                table: "AuctioneersCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctioneersProducts_ProductsProductId",
                table: "AuctioneersProducts",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctioneersUsers_UsersUserId",
                table: "AuctioneersUsers",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDocuments_Products_ProductId",
                table: "ProductDocuments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDocuments_Products_ProductId",
                table: "ProductDocuments");

            migrationBuilder.DropTable(
                name: "AuctioneersCompany");

            migrationBuilder.DropTable(
                name: "AuctioneersProducts");

            migrationBuilder.DropTable(
                name: "AuctioneersUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProductDocuments_ProductId",
                table: "ProductDocuments");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductDocuments");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Auctioneers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Auctioneers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Auctioneers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_DocumentId",
                table: "Products",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctioneers_CompanyId",
                table: "Auctioneers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctioneers_ProductId",
                table: "Auctioneers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctioneers_UserId",
                table: "Auctioneers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctioneers_Companies_CompanyId",
                table: "Auctioneers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctioneers_Products_ProductId",
                table: "Auctioneers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctioneers_Users_UserId",
                table: "Auctioneers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductDocuments_DocumentId",
                table: "Products",
                column: "DocumentId",
                principalTable: "ProductDocuments",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
