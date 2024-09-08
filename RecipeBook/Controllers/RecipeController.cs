using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using RecipeBook.Data;
using RecipeBook.Enum;
using RecipeBook.FileUploadHelper;
using RecipeBook.Models;
using RecipeBook.ViewModels;

namespace RecipeBook.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileHelper _fileHelper;
        private readonly UserManager<AppUser> _userManager;
        public RecipeController(AppDbContext context, IFileHelper fileHelper, UserManager<AppUser> userManager)
        {
            _context = context;
            _fileHelper = fileHelper;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, int page=1, int pageSize=8)
        {
            IEnumerable<Recipe> recipes = [];
            if(searchString == "Vegetarian")
            {
                recipes = await GetVegetarianRecipes(page, pageSize);

                ViewData["Title"] = "Vegetarian";
                return View(recipes);
            }
            if (searchString == "NonVegetarian")
            {
                recipes = await GetNonVegetarianRecipes(page, pageSize);

                ViewData["Title"] = "Non Vegetarian";
                return View(recipes);
            }
            if (searchString == "Vegan")
            {
                recipes = await GetVeganRecipes(page, pageSize);

                ViewData["Title"] = "Vegan";
                return View(recipes);
            }

            recipes = await GetPopularRecipes(page, pageSize);
            ViewData["Title"] = "Popular";
            return View(recipes);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> OnLoadMore(string searchString, int page, int pageSize=8)
        {
            IEnumerable<Recipe> recipes = [];
            if (searchString == "Vegetarian")
            {
                recipes = await GetVeganRecipes(page, pageSize);
                return PartialView("_LoadMorePartial" ,recipes);
            }
            if (searchString == "NonVegetarian")
            {
                recipes = await GetNonVegetarianRecipes(page, pageSize);
                return PartialView("_LoadMorePartial", recipes);
            }
            if (searchString == "Vegan")
            {
                recipes = await GetVeganRecipes(page, pageSize);
                return PartialView("_LoadMorePartial", recipes);
            }

            recipes = await GetPopularRecipes(page, pageSize);
            return PartialView("_LoadMorePartial", recipes);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeViewModel createRecipeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createRecipeVM);
            }

            if (createRecipeVM.Image.Length > 1 * 1024 * 1024)
            {
                ModelState.AddModelError("", "Image size must be smaller than 1mb");
                return View(createRecipeVM);
            }
            string imageName = await _fileHelper.SaveFileAsync(createRecipeVM.Image);

            var recipe = new Recipe
            {
                Name = createRecipeVM.Name,
                Description = createRecipeVM.Description,
                Image = imageName,
                Category = createRecipeVM.Category,
                PostedOn = DateTime.Now,
                AppUserId = GetCurrentUserId(),
                Ingredients = createRecipeVM.Ingredients.Select(i => new Ingredient
                {
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    Item = i.Item,
                }).ToList(),
                Instructions = createRecipeVM.Instructions.Select(i => new Instruction
                {
                    Direction = i.Direction
                }).ToList()
            };
            await _context.Recipe.AddAsync(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await GetRecipeByIdAsNoTracking(id);
            var userId = GetCurrentUserId();

            if(userId == recipe.AppUserId)
            {
                var editRecipeVM = new EditRecipeViewModel
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Description = recipe.Description,
                    Category = recipe.Category,
                    ImageString = recipe.Image,
                    AppUserId = recipe.AppUserId,
                    Ingredients = recipe.Ingredients.Select(i => new EditIngredientViewModel
                    {
                        Id = i.Id,
                        Quantity = i.Quantity,
                        Unit = i.Unit,
                        Item = i.Item,
                    }).ToList(),
                    Instructions = recipe.Instructions.Select(i => new EditInstructionViewModel
                    {
                        Id = i.Id,
                        Direction = i.Direction
                    }).ToList()
                };
                return View(editRecipeVM);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeViewModel editRecipeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editRecipeVM);
            }

            var recipeModel = await GetRecipeById(editRecipeVM.Id);
            var userId = GetCurrentUserId();

            if(userId == recipeModel.AppUserId)
            { 
                string oldImage = recipeModel.Image;

                if (editRecipeVM.Image != null)
                {
                    if (editRecipeVM.Image.Length > 1 * 1024 * 1024)
                    {
                        ModelState.AddModelError("", "Image size must be smaller than 1mb");
                        return View(editRecipeVM);
                    }
                    string imageName = await _fileHelper.SaveFileAsync(editRecipeVM.Image);
                    recipeModel.Image = imageName;
                }

                recipeModel.Name = editRecipeVM.Name;
                recipeModel.Description = editRecipeVM.Description;
                recipeModel.Category = editRecipeVM.Category;
                recipeModel.Ingredients = editRecipeVM.Ingredients.Select(i => new Ingredient
                {
                    Id = i.Id,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    Item = i.Item,
                }).ToList();
                recipeModel.Instructions = editRecipeVM.Instructions.Select(i => new Instruction
                {
                    Id = i.Id,
                    Direction = i.Direction,
                }).ToList();

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    if (editRecipeVM.Image != null)
                    {
                        _fileHelper.DeleteFile(oldImage);
                    }
                    return RedirectToAction("Index");
                }
                return View(editRecipeVM);
            }
            return RedirectToAction("Index");
        }


        [AllowAnonymous]
        public async Task<IActionResult> ViewRecipe(int id)
        {
            var recipe = await GetRecipeByIdAsNoTracking(id);
            var userId = GetCurrentUserId();
            var existingLike = recipe.Likes.FirstOrDefault(l => l.AppUserId == userId);
            if(existingLike != null)
            {
                ViewData["likeClass"] = "bi bi-heart-fill";
            }
            else
            {
                ViewData["likeClass"] = "bi bi-heart";
            }
            return View(recipe);
        }


        [HttpGet]
        public async Task<IActionResult> ToggleLike(int id)
        {
            var mes = "";
            var userId = GetCurrentUserId();

            var recipe = await GetRecipeByIdAsNoTracking(id);
            if (userId == recipe.AppUserId) return BadRequest("You can't like your own recipe");

            var existingLike = recipe.Likes.FirstOrDefault(l => l.AppUserId == userId);
            if (existingLike == null)
            {
                await _context.LikeRecipe.AddAsync(new LikeRecipe
                {
                    AppUserId = userId,
                    RecipeId = id
                });
                mes = "bi bi-heart-fill";
            }
            else
            {
                _context.LikeRecipe.Remove(existingLike);
                mes = "bi bi-heart";
            }
            await _context.SaveChangesAsync();

            var likes = await _context.LikeRecipe.Where(l => l.RecipeId == id).ToListAsync();
            var likeCount = likes.Count();
            var result = new { likeCount, mes };
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddComment(string description, int recipeId)
        {
            Comment comment = new Comment();
            comment.Description = description;
            comment.PostedOn = DateTime.Now;
            comment.RecipeId = recipeId;
            comment.AppUserId = GetCurrentUserId();

            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();

            var newComment = await _context.Comment
                                    .Include(c => c.AppUser)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.Id == comment.Id);
                                    

            return PartialView("_CommentPartial", newComment);
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            var recipeModel = await _context.Recipe
                                    .Include(r => r.AppUser)
                                    .Include(r => r.Ingredients)
                                    .Include(r => r.Instructions)
                                    .FirstOrDefaultAsync(r => r.Id == id);
            return recipeModel;
        }

        public async Task<Recipe> GetRecipeByIdAsNoTracking(int id)
        {
            var recipeModel = await _context.Recipe
                                    .Include(r => r.AppUser)    
                                    .Include(r => r.Ingredients)
                                    .Include(r => r.Instructions)
                                    .Include(r => r.Likes)
                                    .Include(r => r.Comments)
                                    .ThenInclude(c => c.AppUser)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(r => r.Id == id);
            return recipeModel;
        }

        public async Task<IEnumerable<Recipe>> GetVegetarianRecipes(int page, int pageSize)
        {
            var recipes = await _context.Recipe
                     .Where(r => r.Category == Category.Vegetarian)
                     .Include(r => r.AppUser)
                     .Include(r => r.Ingredients)
                     .Include(r => r.Instructions)
                     .Include(r => r.Likes)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .AsNoTracking()
                     .ToListAsync();

            return recipes;
        }

        public async Task<IEnumerable<Recipe>> GetNonVegetarianRecipes(int page, int pageSize)
        {
            var recipes = await _context.Recipe
                     .Where(r => r.Category == Category.NonVegetarian)
                     .Include(r => r.AppUser)
                     .Include(r => r.Ingredients)
                     .Include(r => r.Instructions)
                     .Include(r => r.Likes)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .AsNoTracking()
                     .ToListAsync();

            return recipes;
        }

        public async Task<IEnumerable<Recipe>> GetVeganRecipes(int page, int pageSize)
        {
            var recipes = await _context.Recipe
                     .Where(r => r.Category == Category.Vegan)
                     .Include(r => r.AppUser)
                     .Include(r => r.Ingredients)
                     .Include(r => r.Instructions)
                     .Include(r => r.Likes)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .AsNoTracking()
                     .ToListAsync();

            return recipes;
        }

        public async Task<IEnumerable<Recipe>> GetPopularRecipes(int page, int pageSize)
        {
            var recipes = await _context.Recipe
                     .Include(r => r.AppUser)
                     .Include(r => r.Ingredients)
                     .Include(r => r.Instructions)
                     .Include(r => r.Likes)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .AsNoTracking()
                     .ToListAsync();

            return recipes;
        }

        public string GetCurrentUserId()
        {
            var userId = _userManager.GetUserId(User);
            return userId;
        }
    }
}
