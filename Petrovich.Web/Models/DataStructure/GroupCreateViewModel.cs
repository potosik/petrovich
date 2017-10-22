using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Models.DataStructure
{
    public class GroupCreateViewModel
    {
        public GroupCreateViewModel()
        {
            Categories = new List<SelectListItem>();
            PriceTypes = new List<SelectListItem>();
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Базовая цена проката (BYN)")]
        public double? BasePrice { get; set; }

        [Display(Name = "Ценовой срок")]
        public int? PriceType { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Категория")]
        public Guid CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> PriceTypes { get; set; }
    }
}