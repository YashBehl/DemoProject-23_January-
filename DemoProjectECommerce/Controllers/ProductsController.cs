using DemoProjectECommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectECommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ECommerceDbContext _context;

        public ProductsController(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _context.tbl_Products.ToListAsync();
            return View();
        }
    }
}
