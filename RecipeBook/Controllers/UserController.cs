using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.FileUploadHelper;
using RecipeBook.Models;
using RecipeBook.ViewModels;

namespace RecipeBook.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileHelper _fileHelper;
        private readonly UserManager<AppUser> _userManager;
        public UserController(AppDbContext context, IFileHelper fileHelper, UserManager<AppUser> userManager)
        {
            _context = context; 
            _fileHelper = fileHelper;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Profile(string id)
        {
            var user = await GetUser(id);
            var recipes = await _context.Recipe
                                .Where(r => r.AppUserId == id)
                                .Include(r => r.AppUser)
                                .Include(r => r.Likes)
                                .AsNoTracking()
                                .ToListAsync();

            var userVM = new UserViewModel
            {
                Id = user.Id,
                ImageString = user.Image,
                FullName = user.FullName,
                UserName = user.UserName,
                UserRecipes = recipes
            };
            
            return View(userVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await GetUser(id);
            var userId = _userManager.GetUserId(User);

            if(user.Id == userId)
            {
                var editUserVM = new EditUserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    ImageString = user.Image
                };
                return View(editUserVM);
            }
            return RedirectToAction("Profile", new { id = user.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel editUserVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editUserVM);
            }

            var user = await GetUser(editUserVM.Id);
            var userId = _userManager.GetUserId(User);

            if(user.Id == userId)
            {
                string oldImage = user.Image;

                if (editUserVM.Image != null)
                {
                    if (editUserVM.Image.Length > 1 * 1024 * 1024)
                    {
                        ModelState.AddModelError("", "Image size must be smaller than 1mb");
                        return View(editUserVM);
                    }
                    string imageName = await _fileHelper.SaveFileAsync(editUserVM.Image);
                    user.Image = imageName;
                }

                user.FirstName = editUserVM.FirstName;
                user.LastName = editUserVM.LastName;
                user.Email = editUserVM.Email;
                user.UserName = editUserVM.UserName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (editUserVM.Image != null)
                    {
                        _fileHelper.DeleteFile(oldImage);
                    }
                    return RedirectToAction("Profile", new { id = user.Id });
                }
                return View(editUserVM);
            }
            return RedirectToAction("Profile", new { id = user.Id });
        }

        public async Task<AppUser> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsUserNameAvailable(string userName, string Id)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Json(true);
            }
            else if(user != null &&  user.Id == Id)
            {
                return Json(true);
            }
            else
            {
                return Json($"Username {userName} is not available");
            }
        }
    }
}
