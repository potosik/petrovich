using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.Client
{
    public class ClientCreateViewModel
    {
        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Адрес проживания")]
        public string Address { get; set; }

        [Display(Name = "Адрес регистрации")]
        public string Registered { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "№ паспорта")]
        public string PassportId { get; set; }

        [Display(Name = "Паспорт выдан (кем, когда)")]
        public string PassportData { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Идентификационный №")]
        public string PersonalId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; } = DateTime.Now.AddYears(-18);

        [Display(Name = "Номера телефонов")]
        public string PhonesJson { get; set; }
    }
}