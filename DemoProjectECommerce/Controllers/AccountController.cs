using DemoProjectECommerce.Data;
using DemoProjectECommerce.Data.Static;
using DemoProjectECommerce.Data.ViewModels;
using DemoProjectECommerce.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectECommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ECommerceDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ECommerceDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.emailAddress);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.password);
                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Products");
                    }
                }
                TempData["Error"] = "Invalid credentials, please try again";
                return View(loginViewModel);
            }

            TempData["Error"] = "Invalid credentials, please try again";
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.emailAddress);
            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                fullName = registerViewModel.fullName,
                Email = registerViewModel.emailAddress,
                UserName = registerViewModel.emailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.password);

            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.user);
            }

            return View("RegisterCompleted");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }
    }
}
