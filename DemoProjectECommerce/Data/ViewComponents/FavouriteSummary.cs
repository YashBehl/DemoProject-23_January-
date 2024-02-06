using DemoProjectECommerce.productCategory.Cart;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectECommerce.productCategory.ViewComponents
{
    public class FavouriteSummary:ViewComponent
    {
        private readonly Favourite _favourite;
        public FavouriteSummary(Favourite favourite)
        {
            _favourite = favourite;
        }

        public IViewComponentResult Invoke()
        {
            var items = _favourite.getFavouriteItems();
            return View(items.Count);
        }
    }
}
