using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMeister.Migrations
{
    public partial class UpdateCompanyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Categories_CategoryId",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Companies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Categories_CategoryId",
                table: "Companies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Categories_CategoryId",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Categories_CategoryId",
                table: "Companies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
