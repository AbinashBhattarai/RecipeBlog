using RecipeBook.Enum;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.ViewModels
{
    public class EditRecipeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name too long, can't exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(250, ErrorMessage = "Description too long, can't exceed 250 characters")]
        public string Description { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageString { get; set; }

        public Category Category { get; set; }


        [Required(ErrorMessage = "User is required")]
        public string AppUserId { get; set; }

        public IList<EditIngredientViewModel> Ingredients { get; set; }

        public IList<EditInstructionViewModel> Instructions { get; set; }
    }
}
