using System.ComponentModel.DataAnnotations;

namespace RecipeBook.ViewModels
{
    public class EditIngredientViewModel : CreateIngredientViewModel
    {
        public int Id { get; set; }
    }

    public class CreateIngredientViewModel
    {
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, 10, ErrorMessage = "Quantity must be between 0 and 10")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit is required")]
        [MaxLength(15, ErrorMessage = "Unit too long, can't exceed 15 characters")]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Item name is required")]
        [MaxLength(25, ErrorMessage = "Item Name too long, can't exceed 25 characters")]
        public string Item { get; set; }

        public int RecipeId { get; set; }
    }
}
