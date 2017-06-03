using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Models.DataStructure
{
    public class CreateCategoryModel
    {
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Раздел")]
        public Guid BranchId { get; set; }

        public List<SelectListItem> Branches { get; set; }
    }
}