using DemoProjectECommerce.Data;
using DemoProjectECommerce.Data.Services;
using DemoProjectECommerce.Data.Static;
using DemoProjectECommerce.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectECommerce.Controllers
{
    [Authorize(Roles = UserRoles.admin)]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.getAllAsync();
            return View(allProducts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchString)
        {
            var allProducts = await _service.getAllAsync();

            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts.Where(n => n.productName.Contains(searchString) || n.productDescription.Contains(searchString));
                return View("Index", filteredResult);
            }

            return View("Index", allProducts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var productDetail = await _service.getProductByIdAsync(id);
            return View(productDetail);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductViewModel product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }

            await _service.addNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var productDetails = await _service.getProductByIdAsync(id);
            if(productDetails == null) 
            {
                return View("NotFound");
            }
            var response = new NewProductViewModel()
            {
                productId = productDetails.productId,
                productName = productDetails.productName,
                productDescription = productDetails.productDescription,
                productPrice = productDetails.productPrice,
                productImageUrl = productDetails.productImageUrl,
                productQuantity = productDetails.productQuantity,
                productCategory = productDetails.productCategory
            };

            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, NewProductViewModel product)
        {
            if(id != product.productId)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _service.updateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
