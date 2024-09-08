using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBook.Data.Migrations
{
    /// <inheritdoc />
    public partial class LikeModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "LikeRecipe",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "LikeRecipe");
        }
    }
}
