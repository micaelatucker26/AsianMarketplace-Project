using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsianMarketplace_WebAPI.Migrations
{
    public partial class UpdateDatabaseSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategoryId",
                table: "SubCategory");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "SubCategory",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategory",
                newName: "IX_SubCategory_CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "CategoryID");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Shopper",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_CategoryID",
                table: "SubCategory",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategoryID",
                table: "SubCategory");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Shopper");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "SubCategory",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategory_CategoryID",
                table: "SubCategory",
                newName: "IX_SubCategory_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Category",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_CategoryId",
                table: "SubCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
