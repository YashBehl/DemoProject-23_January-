using DemoProjectECommerce.productCategory.Base;
using DemoProjectECommerce.productCategory.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoProjectECommerce.Models.Domain
{
    public class NewProductViewModel
    {
        public Guid productId { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Name is required")]
        public string productName { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "A desciption is required")]
        public string productDescription { get; set; }

        [Display(Name = "Product's Price in INR")]
        [Required(ErrorMessage = "Price is required")]
        public int productPrice { get; set; }

        [Display(Name = "Product's Image URL")]
        public string? productImageUrl { get; set; }

        [Display(Name = "Product's quantity in stock")]
        [Required(ErrorMessage = "Quantity is required")]
        public int productQuantity { get; set; }

        [Display(Name = "Select Product's Category")]
        [Required(ErrorMessage = "Product category is required")]
        public ProductCategory productCategory { get; set; }

        
        public DateTime createdAt {  get; set; }

    }
}
