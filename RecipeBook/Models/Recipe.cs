using RecipeBook.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; }

        public string Image { get; set; }

        public Category Category { get; set; }

        public DateTime PostedOn { get; set; }


        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public IList<Ingredient> Ingredients { get; set; }
        public IList<Instruction> Instructions { get; set; }
        public IList<LikeRecipe>? Likes { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}
