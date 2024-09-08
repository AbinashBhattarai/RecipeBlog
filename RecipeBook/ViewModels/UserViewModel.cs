using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string? ImageString { get; set; }
        public string FullName { get; set; }

        public string UserName { get; set; }

        public ICollection<Recipe> UserRecipes { get; set; }
    }
}
