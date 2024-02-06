using DemoProjectECommerce.productCategory;
using DemoProjectECommerce.productCategory.Services;
using DemoProjectECommerce.productCategory.Static;
using DemoProjectECommerce.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoProjectECommerce.productCategory.Enums;

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
        public  IActionResult Index()
        {
            var allProducts = _service.getAllAsync().Result.OrderByDescending(n => n.createdAt).Take(100).ToList();
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
            var a = new NewProductViewModel()
            {
                createdAt = DateTime.Now,
                productName = product.productName,
                productDescription = product.productDescription,
                productCategory = product.productCategory,
                productPrice = product.productPrice,
                productQuantity=product.productQuantity,

            };
           
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _service.addNewProductAsync(a);
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
                createdAt = new DateTime()
            };

            return View(response);
        }





        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, NewProductViewModel product)
        {
            if(id == product.productId)
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





        public async Task<IActionResult> markAsHot(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isHot = true;
            return RedirectToAction(nameof(Index));
        }






        public async Task<IActionResult> markAsTrending(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isTrending = true;
            return RedirectToAction(nameof(Index));
        }







        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isUnavailable = true;
            return RedirectToAction(nameof(Index));
        }






        public async Task<IActionResult> disableHot(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isHot = false;
            return RedirectToAction(nameof(Index));
        }





        public async Task<IActionResult> disableTrending(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isTrending = false;
            return RedirectToAction(nameof(Index));
        }





        public async Task<IActionResult> makeAvailable(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isUnavailable = false;
            return RedirectToAction(nameof(Index));
        }


        /*public async Task<IActionResult> Filter(int category, int price_start, int price_end)
        {
            var allProducts = await _service.getAllAsync();
            var productCategory = (ProductCategory)category; 
            var filteredResult = allProducts.Where(n => (n.productCategory = productCategory) && (n.productPrice > price_start) && (n.productPrice < price_end));
            return View("Index", filteredResult);
        }*/
    }
}
