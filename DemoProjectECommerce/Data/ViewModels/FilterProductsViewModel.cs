using DemoProjectECommerce.productCategory.Base;
using DemoProjectECommerce.productCategory.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoProjectECommerce.Models.Domain
{
    public class FilterProductsViewModel
    {
        public Guid productId { get; set; }

        [Display(Name = "Price (in INR) from: ")]
        public int? startPrice { get; set; }

        [Display(Name = "Price (in INR) till: ")]
        public int? endPrice { get; set; }

        [Display(Name = "Select Product's Category")]
        [Required(ErrorMessage = "Product category is required")]
        public ProductCategory productCategory { get; set; }
    }
}
