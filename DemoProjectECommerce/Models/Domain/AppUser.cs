using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using DemoProjectECommerce.productCategory.Static;
using DemoProjectECommerce.productCategory.Cart;


namespace DemoProjectECommerce.Models.Domain
{
    public class AppUser:IdentityUser
    {

        [Display(Name = "Full Name")]
        public string fullName { get; set; }
        public string password { get; set; }
        public bool? isActive { get; set; }

        public string? userCartId { get; set; }
    }
}
