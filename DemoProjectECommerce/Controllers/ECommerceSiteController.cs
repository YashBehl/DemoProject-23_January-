using Microsoft.AspNetCore.Mvc;

namespace DemoProjectECommerce.Controllers
{
    public class ECommerceSiteController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }
    }
}
