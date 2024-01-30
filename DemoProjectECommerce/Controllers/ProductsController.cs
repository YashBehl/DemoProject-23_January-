using DemoProjectECommerce.Data;
using DemoProjectECommerce.Data.Services;
using DemoProjectECommerce.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectECommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.getAllAsync();
            return View(allProducts);
        }

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

            return View();
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
