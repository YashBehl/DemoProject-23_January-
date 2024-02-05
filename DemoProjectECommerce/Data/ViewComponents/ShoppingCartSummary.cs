using DemoProjectECommerce.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectECommerce.Data.ViewComponents
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.getShoppingCartItems();
            return View(items.Count);
        }
    }
}
