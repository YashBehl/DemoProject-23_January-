using DemoProjectECommerce.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectECommerce.productCategory.Cart
{
    public class Favourite
    {
        private ECommerceDbContext _context { get; set; }

        public string favouriteId { get; set; }
        public List<FavouriteItem> favouriteItems { get; set; }
        public Favourite(ECommerceDbContext context)
        {
            _context = context;
        }

        public static Favourite getFavourite(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ECommerceDbContext>();

            string favId = session.GetString("FavId") ?? Guid.NewGuid().ToString();
            session.SetString("FavId", favId);

            return new Favourite(context) { favouriteId = favId };
        }

        public void addItemToFav(Product product)
        {
            var favouriteItem = _context.tbl_FavouriteItems.FirstOrDefault(n => n.product.productId == product.productId
            && n.favouriteId == favouriteId);

            if (favouriteItem == null)
            {
                favouriteItem = new FavouriteItem()
                {
                    favouriteId = favouriteId,
                    product = product,
                };

                _context.tbl_FavouriteItems.Add(favouriteItem);
            }
            _context.SaveChanges();
        }

        public void removeItemFromFav(Product product)
        {
            var favouriteItem = _context.tbl_FavouriteItems.FirstOrDefault(n => n.product.productId == product.productId
            && n.favouriteId == favouriteId);

            if (favouriteItem != null)
            {
                    _context.tbl_FavouriteItems.Remove(favouriteItem);   
            }
            _context.SaveChanges();
        }


        public List<FavouriteItem> getFavouriteItems()
        {
            return favouriteItems ?? (favouriteItems = _context.tbl_FavouriteItems.Where(n => n.favouriteId == favouriteId).Include(n => n.product).ToList());
        }
    }
}
