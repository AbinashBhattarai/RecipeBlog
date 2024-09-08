using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50, ErrorMessage = "First Name too long, can't exceed 50 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(50, ErrorMessage = "Last Name too long, can't exceed 50 characters")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username must be between 5 to 50 characters")]
        [Remote(action: "IsUserNameAvailable", controller: "User", AdditionalFields =nameof(Id))]
        public string UserName { get; set; }

        [Display(Name = "Profile picture")]
        public IFormFile? Image { get; set; }

        public string? ImageString { get; set; }
    }
}
