using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DemoProjectECommerce.Models.Domain
{
    public class AppUser:IdentityUser
    {
        [Display(Name = "Full Name")]
        public string fullName { get; set; }
        //public string password { get; set; }
    }
}
