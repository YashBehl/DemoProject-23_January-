using DemoProjectECommerce.Data.Cart;
using DemoProjectECommerce.Data.Services;
using DemoProjectECommerce.Data.Static;
using DemoProjectECommerce.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectECommerce.Controllers
{
    [Authorize(Roles = UserRoles.user)]
    public class CartsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ShoppingCart _shoppingCart;

        public CartsController(IProductsService productsService, ShoppingCart shoppingCart)
        {
            _productsService = productsService;
            _shoppingCart = shoppingCart;
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.getShoppingCartItems();
            _shoppingCart.shoppingCartItems = items;

            var response = new ShoppingCartViewModel()
            {
                shoppingCart = _shoppingCart,
                shoppingCartTotal = _shoppingCart.getShoppingCartTotal()
            };


            return View(response);
        }

        public async Task<IActionResult> addItemToCart(Guid id)
        {
            var item = await _productsService.getProductByIdAsync(id);

            if(item != null)
            {
                _shoppingCart.addItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> removeItemFromCart(Guid id)
        {
            var item = await _productsService.getProductByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.removeItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
    }
}
