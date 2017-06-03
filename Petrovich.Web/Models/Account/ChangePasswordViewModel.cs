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
        [StringLength(100, ErrorMessage = "Пароль {0} должен содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароль и подтверждение пароля должны совпадать.")]
        public string ConfirmPassword { get; set; }
    }
}