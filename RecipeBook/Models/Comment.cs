namespace RecipeBook.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime PostedOn { get; set; }

        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public int? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
