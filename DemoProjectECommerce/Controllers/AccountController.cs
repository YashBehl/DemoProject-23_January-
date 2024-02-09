using DemoProjectECommerce.productCategory;
using DemoProjectECommerce.productCategory.Static;
using DemoProjectECommerce.productCategory.ViewModels;
using DemoProjectECommerce.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DemoProjectECommerce.productCategory.Cart;
using DemoProjectECommerce.Email;


namespace DemoProjectECommerce.Controllers
{
    [Authorize(Roles = UserRoles.admin)]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ECommerceDbContext _context;
        private readonly ShoppingCart _shoppingCart;
        private readonly EmailSender _emailSender;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ECommerceDbContext context, ShoppingCart shoppingCart, EmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _shoppingCart = shoppingCart;
            _emailSender = emailSender;
        }


        public IActionResult Users()
        { 
            var users =  _context.Users.ToListAsync().Result.Skip(1);
            return View(users);   
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
           
            var user = await _userManager.FindByEmailAsync(loginViewModel.emailAddress);
            
            if (user != null)
            {
                bool? checkActivation = _userManager.Users.First(n => n.Email == loginViewModel.emailAddress).isActive;
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.password, false, false);
                    if (checkActivation == true)
                    {
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Products");
                        }
                        TempData["Error"] = "Invalid credentials, please try again";
                        return View(loginViewModel);
                    }
                    else
                    {
                        TempData["Deactivation"] = "Your account has been deactivated by the admin";
                        return View(loginViewModel);
                    }
                }
            }
            TempData["Error"] = "Invalid credentials, please try again";
            return View(loginViewModel);
        }


        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }


        [AllowAnonymous]
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
                UserName = registerViewModel.emailAddress,
                PhoneNumber = registerViewModel.phoneNumber,
                password = registerViewModel.password,
                userCartId = _shoppingCart.shoppingCartId,
                isActive = true
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.password);

            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.user);
            }

            return View("RegisterCompleted");
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }



        public async Task<IActionResult> Activate(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                user.isActive = true;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Users");
        }


        public async Task<IActionResult> Deactivate(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.isActive = false;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Users");
        }



        public async Task<IActionResult> Edit(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return View("NotFound");
            var userDetails = new RegisterViewModel()
            {
                fullName = user.fullName,
                emailAddress = user.Email,
                phoneNumber = user.PhoneNumber,
                password = user.password,
            };
            return View(userDetails);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string email, RegisterViewModel registerViewModel)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var oldPassword = user.password;
            if (user != null)
            {
                user.fullName = registerViewModel.fullName;
                user.PhoneNumber = registerViewModel.phoneNumber;
                user.password = registerViewModel.password;
                await _userManager.ChangePasswordAsync(user, oldPassword, registerViewModel.password);
                user.isActive = true;
                
                await _userManager.UpdateAsync(user);
            }
            if (oldPassword != user.password)
            {
                await _emailSender.SendMails(email, "The password of your Demo Ecommerce is changed Successfully by Admin", "New Password-" + user.password);
            }
            return RedirectToAction(nameof(Users));
        }


    }
}
