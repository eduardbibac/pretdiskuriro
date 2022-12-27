using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pretdiskuriro.Migrations
{
    /// <inheritdoc />
    public partial class marketproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyPrices_Products_ProductId",
                table: "DailyPrices");

            migrationBuilder.DropTable(
                name: "MarketProduct");

            migrationBuilder.DropIndex(
                name: "IX_DailyPrices_ProductId",
                table: "DailyPrices");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "DailyPrices",
                newName: "MarketProductProductId");

            migrationBuilder.AddColumn<int>(
                name: "MarketProductId",
                table: "DailyPrices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarketProductMarketId",
                table: "DailyPrices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MarketProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketProducts", x => new { x.MarketId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_MarketProducts_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyPrices_MarketProductMarketId_MarketProductProductId",
                table: "DailyPrices",
                columns: new[] { "MarketProductMarketId", "MarketProductProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_MarketProducts_ProductId",
                table: "MarketProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPrices_MarketProducts_MarketProductMarketId_MarketProductProductId",
                table: "DailyPrices",
                columns: new[] { "MarketProductMarketId", "MarketProductProductId" },
                principalTable: "MarketProducts",
                principalColumns: new[] { "MarketId", "ProductId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyPrices_MarketProducts_MarketProductMarketId_MarketProductProductId",
                table: "DailyPrices");

            migrationBuilder.DropTable(
                name: "MarketProducts");

            migrationBuilder.DropIndex(
                name: "IX_DailyPrices_MarketProductMarketId_MarketProductProductId",
                table: "DailyPrices");

            migrationBuilder.DropColumn(
                name: "MarketProductId",
                table: "DailyPrices");

            migrationBuilder.DropColumn(
                name: "MarketProductMarketId",
                table: "DailyPrices");

            migrationBuilder.RenameColumn(
                name: "MarketProductProductId",
                table: "DailyPrices",
                newName: "ProductId");

            migrationBuilder.CreateTable(
                name: "MarketProduct",
                columns: table => new
                {
                    MarketsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketProduct", x => new { x.MarketsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_MarketProduct_Markets_MarketsId",
                        column: x => x.MarketsId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyPrices_ProductId",
                table: "DailyPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketProduct_ProductsId",
                table: "MarketProduct",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPrices_Products_ProductId",
                table: "DailyPrices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
