using System.ComponentModel.DataAnnotations;

namespace DemoProjectECommerce.productCategory.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string emailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
