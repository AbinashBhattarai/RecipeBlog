using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.FileUploadHelper;
using RecipeBook.Models;
using RecipeBook.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecipeBook.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IFileHelper _fileHelper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IFileHelper fileHelper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileHelper = fileHelper;
        }

        [HttpGet]
        public IActionResult Login(int? recipeId)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["RecipeId"] = recipeId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM, int recipeId)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var userName = await _userManager.FindByNameAsync(loginVM.UserName);
            if(userName == null)
            {
                ModelState.AddModelError("UserName", "Incorrect username");
                return View(loginVM);
            }
            var passCheck = await _userManager.CheckPasswordAsync(userName, loginVM.Password);
            if (!passCheck)
            {
                ModelState.AddModelError("Password", "Incorrect password");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(userName, loginVM.Password, loginVM.RememberMe, false);
            if (result.Succeeded)
            {
                if(recipeId > 0)
                {
                    return RedirectToAction("ViewRecipe", "Recipe", new { id = recipeId });
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View(loginVM);  
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await _userManager.FindByEmailAsync(registerVM.Email);
                if(userEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already registered. Proceed to login");
                    return View(registerVM);
                }

                if (registerVM.Image.Length > 1 * 1024 * 1024)
                {
                    ModelState.AddModelError("", "Image size must be smaller than 1mb");
                    return View(registerVM);
                }
                string imageName = await _fileHelper.SaveFileAsync(registerVM.Image);

                var user = new AppUser
                {
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Email = registerVM.Email,
                    UserName = registerVM.UserName,
                    Image = imageName
                };

                var result = await _userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerVM);
        }


        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsUserNameAvailableForRegistration(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Username {userName} is not available");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
