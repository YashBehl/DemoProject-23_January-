using DemoProjectECommerce.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace DemoProjectECommerce.productCategory.Cart
{
    public class ShoppingCart
    {
        private ECommerceDbContext _context { get; set; }

        
        public string shoppingCartId { get; set; }


        public List<ShoppingCartItem> shoppingCartItems { get; set; }
        public ShoppingCart(ECommerceDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart getShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ECommerceDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { shoppingCartId = cartId };
        }

        public void addItemToCart(Product product)
        {
            var shoppingCartItem = _context.tbl_ShoppingCartItems.FirstOrDefault(n => n.product.productId == product.productId
            && n.shoppingCartId == shoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    shoppingCartId = shoppingCartId,
                    product = product,
                    quantity = 1
                };

                _context.tbl_ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.quantity++;
            }
            _context.SaveChanges();
        }

        public void removeItemFromCart(Product product)
        {
            var shoppingCartItem = _context.tbl_ShoppingCartItems.FirstOrDefault(n => n.product.productId == product.productId
            && n.shoppingCartId == shoppingCartId);

            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.quantity > 1)
                {
                    shoppingCartItem.quantity--;
                }
                else
                {
                    _context.tbl_ShoppingCartItems.Remove(shoppingCartItem);
                }   
            }
            _context.SaveChanges();
        }


        public List<ShoppingCartItem> getShoppingCartItems()
        {
            return shoppingCartItems ?? (shoppingCartItems = _context.tbl_ShoppingCartItems.Where(n => n.shoppingCartId == shoppingCartId).Include(n => n.product).ToList());
        }

        public double getShoppingCartTotal()
        {
            var total = _context.tbl_ShoppingCartItems.Where(n => n.shoppingCartId == shoppingCartId).Select(n => n.product.productPrice * n.quantity).Sum();
            return total;
        }
    }
}
