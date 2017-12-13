using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Petrovich.Business.Models;
using Petrovich.Core;

namespace Petrovich.Web.Models.DataStructure
{
    public class CategoryEditViewModel : ChangeTrackableViewModel
    {
        public CategoryEditViewModel()
        {
            PriceCalculationTypes = new List<SelectListItem>();
        }

        public CategoryEditViewModel(IChangeTrackableEntityModel entity)
            : base(entity)
        {
            PriceCalculationTypes = new List<SelectListItem>();
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Часть инвентарного номера")]
        public int InventoryPart { get; set; }

        [Display(Name = "Базовая цена проката (BYN)")]
        public double? BasePrice { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Схема расчета цены")]
        public int PriceCalculationType { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Раздел")]
        public Guid BranchId { get; set; }

        public string BranchTitle { get; set; }

        public List<SelectListItem> PriceCalculationTypes { get; set; }

        public static CategoryEditViewModel Create(CategoryModel category)
        {
            Guard.NotNullArgument(category, nameof(category));

            return new CategoryEditViewModel(category)
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                InventoryPart = category.InventoryPart,
                BasePrice = category.Price,
                PriceCalculationType = (int)category.PriceCalculationType,
                BranchId = category.BranchId,

                BranchTitle = category.BranchTitle,
            };
        }
    }
}