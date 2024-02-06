using DemoProjectECommerce.productCategory.Cart;
using DemoProjectECommerce.productCategory.Services;
using DemoProjectECommerce.productCategory.Static;
using DemoProjectECommerce.productCategory.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectECommerce.Controllers
{
    [Authorize(Roles = UserRoles.user)]
    public class FavouritesController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly Favourite _favourite;

        public FavouritesController(IProductsService productsService, Favourite favourite)
        {
            _productsService = productsService;
            _favourite = favourite;
        }

        public IActionResult Favourite()
        {
            var items = _favourite.getFavouriteItems();
            _favourite.favouriteItems = items;

            var response = new FavouriteViewModel()
            {
                favourite = _favourite,
            };


            return View(response);
        }

        public async Task<IActionResult> addItemToFav(Guid id)
        {
            var item = await _productsService.getProductByIdAsync(id);

            if(item != null)
            {
                _favourite.addItemToFav(item);
            }
            return RedirectToAction(nameof(Favourite));
        }

        public async Task<IActionResult> removeItemFromFav(Guid id)
        {
            var item = await _productsService.getProductByIdAsync(id);

            if (item != null)
            {
                _favourite.removeItemFromFav(item);
            }
            return RedirectToAction(nameof(Favourite));
        }
    }
}
