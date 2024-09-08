using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models
{
    public class Instruction
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Direction { get; set; }

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
