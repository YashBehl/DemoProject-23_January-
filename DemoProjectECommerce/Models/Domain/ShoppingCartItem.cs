using System.ComponentModel.DataAnnotations;

namespace DemoProjectECommerce.Models.Domain
{
    public class ShoppingCartItem
    {
        [Key]
        public Guid shoppingCartItemId { get; set; }
        public Product product { get; set; }
        public int quantity { get; set; }
        public string shoppingCartId {  get; set; } 
    }
}
