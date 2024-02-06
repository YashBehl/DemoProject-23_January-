using System.ComponentModel.DataAnnotations;

namespace DemoProjectECommerce.Models.Domain
{
    public class FavouriteItem
    {
        [Key]
        public Guid favouriteItemId { get; set; }
        public Product product { get; set; }
        public string favouriteId {  get; set; } 
    }
}
