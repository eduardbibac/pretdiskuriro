using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinPretDiskuri.Migrations
{
    /// <inheritdoc />
    public partial class capacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacityInGB",
                table: "Products");

            migrationBuilder.AddColumn<float>(
                name: "CapacityInTB",
                table: "Products",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacityInTB",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CapacityInGB",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
