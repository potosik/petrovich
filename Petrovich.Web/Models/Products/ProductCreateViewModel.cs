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

        public ProductCreateViewModel()
        {
            Branches = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
            Groups = new List<SelectListItem>();
        }
    }
}