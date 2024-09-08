using System.ComponentModel.DataAnnotations;

namespace RecipeBook.ViewModels
{
    public class LoginViewModel
    {

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username must be between 5 to 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }    
    }
}
