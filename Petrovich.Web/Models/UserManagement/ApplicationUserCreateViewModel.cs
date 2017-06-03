using Petrovich.Core.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Petrovich.Web.Models.UserManagement
{
    public class ApplicationUserCreateViewModel : IUpdateApplicationUserViewModel
    {
        public string Id { get; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [EmailAddress]
        [Display(Name = "Логин (email)")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        [StringLength(100, ErrorMessage = "Пароль {0} должен содержать не менее {2} символов.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля должны совпадать.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Права доступа")]
        public List<string> Claims { get; set; }

        public List<SelectListItem> AllClaims {
            get
            {
                return ClaimUtils.GetPublicClaims().Select(item => new SelectListItem()
                    {
                        Text = item.ToString(),
                        Value = item.ToString(),
                    }).ToList();
            }
        }
    }
}