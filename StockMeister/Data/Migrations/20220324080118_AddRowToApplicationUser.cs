using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMeister.Data.Migrations
{
    public partial class AddRowToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComapnyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ComapnyId",
                table: "AspNetUsers",
                column: "ComapnyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_ComapnyId",
                table: "AspNetUsers",
                column: "ComapnyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_ComapnyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ComapnyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ComapnyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");
        }
    }
}
