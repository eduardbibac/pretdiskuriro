using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pretdiskuriro.Migrations
{
    /// <inheritdoc />
    public partial class new2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketProduct_Markets_ZMarketsId",
                table: "MarketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketProduct",
                table: "MarketProduct");

            migrationBuilder.DropIndex(
                name: "IX_MarketProduct_ZMarketsId",
                table: "MarketProduct");

            migrationBuilder.RenameColumn(
                name: "ZMarketsId",
                table: "MarketProduct",
                newName: "MarketsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketProduct",
                table: "MarketProduct",
                columns: new[] { "MarketsId", "ProductsId" });

            migrationBuilder.CreateIndex(
                name: "IX_MarketProduct_ProductsId",
                table: "MarketProduct",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketProduct_Markets_MarketsId",
                table: "MarketProduct",
                column: "MarketsId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketProduct_Markets_MarketsId",
                table: "MarketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketProduct",
                table: "MarketProduct");

            migrationBuilder.DropIndex(
                name: "IX_MarketProduct_ProductsId",
                table: "MarketProduct");

            migrationBuilder.RenameColumn(
                name: "MarketsId",
                table: "MarketProduct",
                newName: "ZMarketsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketProduct",
                table: "MarketProduct",
                columns: new[] { "ProductsId", "ZMarketsId" });

            migrationBuilder.CreateIndex(
                name: "IX_MarketProduct_ZMarketsId",
                table: "MarketProduct",
                column: "ZMarketsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketProduct_Markets_ZMarketsId",
                table: "MarketProduct",
                column: "ZMarketsId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
