namespace RecipeBook.Models
{
    public class LikeRecipe
    {
        public int Id { get; set; }
        public int? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
