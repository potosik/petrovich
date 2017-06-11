using System.ComponentModel.DataAnnotations;

namespace Petrovich.Web.Models.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Новый пароль")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "Password_LengthValidation_Error")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "Password_MatchConfirmPassword_Error")]
        public string ConfirmPassword { get; set; }
    }
}