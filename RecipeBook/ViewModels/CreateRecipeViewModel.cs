using RecipeBook.Enum;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.ViewModels
{
    public class CreateRecipeViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name too long, can't exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(250, ErrorMessage = "Description too long, can't exceed 250 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }

        public Category Category { get; set; }

        public IList<CreateIngredientViewModel> Ingredients { get; set; }

        public IList<CreateInstructionViewModel> Instructions { get; set; }
    }
}
