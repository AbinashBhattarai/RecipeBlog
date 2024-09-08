using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBook.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecipeUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Recipe",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_AppUserId",
                table: "Recipe",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_AspNetUsers_AppUserId",
                table: "Recipe",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_AspNetUsers_AppUserId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_AppUserId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Recipe");
        }
    }
}
