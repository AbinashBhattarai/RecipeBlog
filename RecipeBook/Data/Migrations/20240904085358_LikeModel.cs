using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBook.Data.Migrations
{
    /// <inheritdoc />
    public partial class LikeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Recipe");

            migrationBuilder.CreateTable(
                name: "LikeRecipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeRecipe_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LikeRecipe_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeRecipe_AppUserId",
                table: "LikeRecipe",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeRecipe_RecipeId",
                table: "LikeRecipe",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeRecipe");

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Recipe",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
