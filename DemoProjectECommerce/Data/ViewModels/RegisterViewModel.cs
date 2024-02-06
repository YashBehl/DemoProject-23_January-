using System.ComponentModel.DataAnnotations;

namespace DemoProjectECommerce.productCategory.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name is required")]
        public string fullName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string emailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }


        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Passwords do not match!")]
        public string confirmPassword { get; set; }
    }
}
