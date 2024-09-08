using RecipeBook.Models;

namespace RecipeBook.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Recipe> VegetarianRecipes {  get; set; }
        public IEnumerable<Recipe> NonVegetarianRecipes { get; set; }
        public IEnumerable<Recipe> VeganRecipes { get; set; }
    }
}
