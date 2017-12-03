using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Models.DataStructure
{
    public class CategoryCreateViewModel
    {
        public CategoryCreateViewModel()
        {
            Branches = new List<SelectListItem>();
            PriceCalculationTypes = new List<SelectListItem>();
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Базовая цена проката (BYN)")]
        public double? BasePrice { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Схема расчета цены")]
        public int PriceCalculationType { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Раздел")]
        public Guid BranchId { get; set; }

        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> PriceCalculationTypes { get; set; }
    }
}