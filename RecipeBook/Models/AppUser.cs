using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        public string Image {  get; set; }

        [NotMapped]
        [ValidateNever]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
    }
}
