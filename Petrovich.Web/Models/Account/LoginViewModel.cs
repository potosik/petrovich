using System.ComponentModel.DataAnnotations;

namespace Petrovich.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}