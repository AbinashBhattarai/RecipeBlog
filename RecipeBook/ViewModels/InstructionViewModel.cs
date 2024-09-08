using System.ComponentModel.DataAnnotations;

namespace RecipeBook.ViewModels
{
    public class EditInstructionViewModel : CreateInstructionViewModel
    {
        public int Id { get; set; }
    }

    public class CreateInstructionViewModel
    {
        [Required(ErrorMessage = "Direction is required")]
        [MaxLength(100, ErrorMessage = "Direction too long, can't exceed 100 characters")]
        public string Direction { get; set; }

        public int RecipeId { get; set; }
    }
}
