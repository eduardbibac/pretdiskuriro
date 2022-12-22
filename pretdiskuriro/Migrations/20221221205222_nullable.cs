using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pretdiskuriro.Migrations
{
    /// <inheritdoc />
    public partial class nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyPrice_Products_ProductId",
                table: "DailyPrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyPrice",
                table: "DailyPrice");

            migrationBuilder.RenameTable(
                name: "DailyPrice",
                newName: "DailyPrices");

            migrationBuilder.RenameIndex(
                name: "IX_DailyPrice_ProductId",
                table: "DailyPrices",
                newName: "IX_DailyPrices_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyPrices",
                table: "DailyPrices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPrices_Products_ProductId",
                table: "DailyPrices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyPrices_Products_ProductId",
                table: "DailyPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyPrices",
                table: "DailyPrices");

            migrationBuilder.RenameTable(
                name: "DailyPrices",
                newName: "DailyPrice");

            migrationBuilder.RenameIndex(
                name: "IX_DailyPrices_ProductId",
                table: "DailyPrice",
                newName: "IX_DailyPrice_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyPrice",
                table: "DailyPrice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPrice_Products_ProductId",
                table: "DailyPrice",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
