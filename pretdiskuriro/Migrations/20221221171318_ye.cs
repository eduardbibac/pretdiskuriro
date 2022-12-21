using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pretdiskuriro.Migrations
{
    /// <inheritdoc />
    public partial class ye : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyPrice_Products_ProductId",
                table: "DailyPrice");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "DailyPrice",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPrice_Products_ProductId",
                table: "DailyPrice",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyPrice_Products_ProductId",
                table: "DailyPrice");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "DailyPrice",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPrice_Products_ProductId",
                table: "DailyPrice",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
