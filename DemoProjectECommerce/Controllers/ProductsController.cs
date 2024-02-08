using DemoProjectECommerce.productCategory;
using DemoProjectECommerce.productCategory.Services;
using DemoProjectECommerce.productCategory.Static;
using DemoProjectECommerce.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoProjectECommerce.productCategory.Enums;
using System.Linq;

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
        public IActionResult Index()
        {
            var allProducts = _service.getAllAsync().Result.OrderByDescending(n => ((n.isHot == true) || (n.isTrending == true) || (n.isUnavailable == true))).ThenByDescending(n => n.createdAt).Take(100).ToList();
            return View(allProducts);
        }



        public IActionResult AllProducts()
        {
            var allProducts = _service.getAllAsync().Result.OrderByDescending(n => ((n.isHot == true) || (n.isTrending == true) || (n.isUnavailable == true))).ThenByDescending(n => n.createdAt).ToList();
            return View(allProducts);
        }





        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchString)
        {
            var allProducts = await _service.getAllAsync();

            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts.Where(n => n.productName.ToLower().Contains(searchString.ToLower()) || n.productDescription.ToLower().Contains(searchString.ToLower())).ToList();
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





        public async Task<IActionResult> Create()
        {
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> Create(NewProductViewModel product)
        {
            var newProduct = new NewProductViewModel()
            {
                productName = product.productName,
                productDescription = product.productDescription,
                productCategory = product.productCategory,
                productPrice = product.productPrice,
                productQuantity = product.productQuantity,
                productImageUrl = product.productImageUrl,
                createdAt = DateTime.Now,
            };
           
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _service.addNewProductAsync(newProduct);
            return RedirectToAction(nameof(Index));
        }



        
        public async Task<IActionResult> Edit(Guid id)
        {
            var productDetails = await _service.getProductByIdAsync(id);
            if(productDetails == null) return View("NotFound");

            var response = new NewProductViewModel()
            {
                productId = productDetails.productId,
                productName = productDetails.productName,
                productDescription = productDetails.productDescription,
                productPrice = productDetails.productPrice,
                productImageUrl = productDetails.productImageUrl,
                productQuantity = productDetails.productQuantity,
                productCategory = productDetails.productCategory,
                createdAt = DateTime.Now,
                isHot = productDetails.isHot,
                isTrending = productDetails.isTrending,
                isUnavailable = productDetails.isUnavailable
            };

            return View(response);
        }





        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, NewProductViewModel product)
        {
            var productDetails = await _service.getProductByIdAsync(id);
            if(productDetails != null)
            {
                productDetails.productName = product.productName;
                productDetails.productDescription = product.productDescription;
                productDetails.productPrice = product.productPrice;
                productDetails.productImageUrl = product.productImageUrl;
                productDetails.productQuantity = product.productQuantity;
                productDetails.productCategory = product.productCategory;
                productDetails.createdAt = DateTime.Now;
                productDetails.isHot = product.isHot;
                productDetails.isTrending = product.isTrending;
                productDetails.isUnavailable = product.isUnavailable;
                await _service.UpdateProductAsync(product);
            }
            
            
            return RedirectToAction(nameof(Index));
        }





        public async Task<IActionResult> markAsHot(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isHot = true;
            var productDetails = new NewProductViewModel()
            {
                productId = product.productId,
                productName = product.productName,
                productDescription = product.productDescription,
                productPrice = product.productPrice,
                productImageUrl = product.productImageUrl,
                productQuantity = product.productQuantity,
                productCategory = product.productCategory,
                createdAt = DateTime.Now,
                isHot = product.isHot,
                isTrending = product.isTrending,
                isUnavailable = product.isUnavailable
            };
            await _service.UpdateProductAsync(productDetails);
            return RedirectToAction(nameof(Index));
        }






        public async Task<IActionResult> markAsTrending(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isTrending = true;
            var productDetails = new NewProductViewModel()
            {
                productId = product.productId,
                productName = product.productName,
                productDescription = product.productDescription,
                productPrice = product.productPrice,
                productImageUrl = product.productImageUrl,
                productQuantity = product.productQuantity,
                productCategory = product.productCategory,
                createdAt = DateTime.Now,
                isHot = product.isHot,
                isTrending = product.isTrending,
                isUnavailable = product.isUnavailable
            };
            await _service.UpdateProductAsync(productDetails);
            return RedirectToAction(nameof(Index));
        }







        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isUnavailable = true;
            var productDetails = new NewProductViewModel()
            {
                productId = product.productId,
                productName = product.productName,
                productDescription = product.productDescription,
                productPrice = product.productPrice,
                productImageUrl = product.productImageUrl,
                productQuantity = product.productQuantity,
                productCategory = product.productCategory,
                createdAt = DateTime.Now,
                isHot = product.isHot,
                isTrending = product.isTrending,
                isUnavailable = product.isUnavailable
            };
            await _service.UpdateProductAsync(productDetails);
            return RedirectToAction(nameof(Index));
        }






        public async Task<IActionResult> disableHot(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isHot = false;
            var productDetails = new NewProductViewModel()
            {
                productId = product.productId,
                productName = product.productName,
                productDescription = product.productDescription,
                productPrice = product.productPrice,
                productImageUrl = product.productImageUrl,
                productQuantity = product.productQuantity,
                productCategory = product.productCategory,
                createdAt = DateTime.Now,
                isHot = product.isHot,
                isTrending = product.isTrending,
                isUnavailable = product.isUnavailable
            };
            await _service.UpdateProductAsync(productDetails);
            return RedirectToAction(nameof(Index));
        }





        public async Task<IActionResult> disableTrending(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isTrending = false;
            var productDetails = new NewProductViewModel()
            {
                productId = product.productId,
                productName = product.productName,
                productDescription = product.productDescription,
                productPrice = product.productPrice,
                productImageUrl = product.productImageUrl,
                productQuantity = product.productQuantity,
                productCategory = product.productCategory,
                createdAt = DateTime.Now,
                isHot = product.isHot,
                isTrending = product.isTrending,
                isUnavailable = product.isUnavailable
            };
            await _service.UpdateProductAsync(productDetails);
            return RedirectToAction(nameof(Index));
        }





        public async Task<IActionResult> makeAvailable(Guid id)
        {
            var product = await _service.getProductByIdAsync(id);
            product.isUnavailable = false;
            var productDetails = new NewProductViewModel()
            {
                productId = product.productId,
                productName = product.productName,
                productDescription = product.productDescription,
                productPrice = product.productPrice,
                productImageUrl = product.productImageUrl,
                productQuantity = product.productQuantity,
                productCategory = product.productCategory,
                createdAt = DateTime.Now,
                isHot = product.isHot,
                isTrending = product.isTrending,
                isUnavailable = product.isUnavailable
            };
            await _service.UpdateProductAsync(productDetails);
            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        public IActionResult Filter()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Filter(FilterProductsViewModel filterProducts)
        {
            var allProducts = await _service.getAllAsync();
            var newInputProducts = new FilterProductsViewModel()
            {
                productCategory = filterProducts.productCategory,
            };
            var filteredResult = allProducts;
            if (newInputProducts.productCategory == (ProductCategory)1)
            {
                if ((filterProducts.startPrice != null) || (filterProducts.endPrice != null))
                {
                    if ((filterProducts.startPrice != null))
                    {
                        newInputProducts.startPrice = filterProducts.startPrice;
                        filteredResult = allProducts.Where(n => (n.productPrice >= newInputProducts.startPrice));
                    }
                    if ((filterProducts.endPrice != null))
                    {
                        newInputProducts.endPrice = filterProducts.endPrice;
                        filteredResult = allProducts.Where(n =>(n.productPrice <= newInputProducts.endPrice));
                    }
                    if ((filterProducts.startPrice != null) && (filterProducts.endPrice != null))
                    {
                        newInputProducts.startPrice = filterProducts.startPrice;
                        newInputProducts.endPrice = filterProducts.endPrice;
                        filteredResult = allProducts.Where(n => (n.productPrice >= newInputProducts.startPrice) && (n.productPrice <= newInputProducts.endPrice));
                    }
                }
            }
            else
            {
                filteredResult = allProducts.Where(n => n.productCategory == newInputProducts.productCategory);
                if ((filterProducts.startPrice != null) || (filterProducts.endPrice != null))
                {
                    if ((filterProducts.startPrice != null))
                    {
                        newInputProducts.startPrice = filterProducts.startPrice;
                        filteredResult = allProducts.Where(n => (n.productCategory == newInputProducts.productCategory) && (n.productPrice >= newInputProducts.startPrice));
                    }
                    if ((filterProducts.endPrice != null))
                    {
                        newInputProducts.endPrice = filterProducts.endPrice;
                        filteredResult = allProducts.Where(n => (n.productCategory == newInputProducts.productCategory) && (n.productPrice <= newInputProducts.endPrice));
                    }
                    if ((filterProducts.startPrice != null) && (filterProducts.endPrice != null))
                    {
                        newInputProducts.startPrice = filterProducts.startPrice;
                        newInputProducts.endPrice = filterProducts.endPrice;
                        filteredResult = allProducts.Where(n => (n.productCategory == newInputProducts.productCategory) && (n.productPrice >= newInputProducts.startPrice) && (n.productPrice <= newInputProducts.endPrice));
                    }
                }
            }

            
            return View("Index", filteredResult);
        }
    }
}
