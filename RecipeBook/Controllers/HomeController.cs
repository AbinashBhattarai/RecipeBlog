using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.Enum;
using RecipeBook.Models;
using RecipeBook.ViewModels;
using System.Diagnostics;

namespace RecipeBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vegetarianRecipes = await _context.Recipe
                .Where(r => r.Category == Category.Vegetarian)
                .Include(r => r.AppUser)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r => r.Likes)
                .Take(4)
                .AsNoTracking()
                .ToListAsync();

            var nonVegetarianRecipes = await _context.Recipe
                .Where(r => r.Category == Category.NonVegetarian)
                .Include(r => r.AppUser)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r => r.Likes)
                .Take(4)
                .AsNoTracking()
                .ToListAsync();

            var veganRecipes = await _context.Recipe
                .Where(r => r.Category == Category.Vegan)
                .Include(r => r.AppUser)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r => r.Likes)
                .Take(4)
                .AsNoTracking()
                .ToListAsync();

            var homeVM = new HomeViewModel
            {
                VegetarianRecipes = vegetarianRecipes,
                NonVegetarianRecipes = nonVegetarianRecipes,
                VeganRecipes = veganRecipes,
            };

            return View(homeVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
