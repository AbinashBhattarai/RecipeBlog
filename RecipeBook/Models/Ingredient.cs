using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Unit { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        public string Item { get; set; }

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
