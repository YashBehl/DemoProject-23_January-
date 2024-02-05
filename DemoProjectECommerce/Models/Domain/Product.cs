using DemoProjectECommerce.Data.Base;
using DemoProjectECommerce.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace DemoProjectECommerce.Models.Domain
{
    public class Product:IEntityBase
    {
        [Key]
        public Guid productId { get; set; }

        public string productName { get; set; }
        public string productDescription { get; set; }
        public int productPrice { get; set; }
        public string? productImageUrl { get; set; }
        public int productQuantity { get; set; }    

        public ProductCategory productCategory { get; set; }

        public DateTime createdAt { get; set; }
    }
}
