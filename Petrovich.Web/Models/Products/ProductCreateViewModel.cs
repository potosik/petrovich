using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Models.Products
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Дефекты")]
        public string Defects { get; set; }

        [Display(Name = "Цена проката (BYN)")]
        public double? Price { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Оценочная стоимость (BYN)")]
        public double AssessedValue { get; set; }

        [Display(Name = "Год закупки")]
        [Range(2000, 2100, ErrorMessageResourceName = "Product_PurchaseYear_Range_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int? PurchaseYear { get; set; }

        [Display(Name = "Месяц закупки")]
        [Range(1, 12, ErrorMessageResourceName = "Product_PurchaseMonth_Range_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int? PurchaseMonth { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Раздел")]
        public Guid BranchId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Категория")]
        public Guid CategoryId { get; set; }

        [Display(Name = "Группа")]
        public Guid? GroupId { get; set; }

        public IList<SelectListItem> Branches { get; set; }
        public IList<SelectListItem> Categories { get; set; }
        public IList<SelectListItem> Groups { get; set; }
        public IList<SelectListItem> PriceTypes { get; set; }

        public ProductCreateViewModel()
        {
            PurchaseYear = DateTime.Now.Year;
            PurchaseMonth = DateTime.Now.Month;

            Branches = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
            Groups = new List<SelectListItem>();
            PriceTypes = new List<SelectListItem>();
        }
    }
}