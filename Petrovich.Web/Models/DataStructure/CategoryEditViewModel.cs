using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Petrovich.Business.Models;

namespace Petrovich.Web.Models.DataStructure
{
    public class CategoryEditViewModel : ChangeTrackableViewModel
    {
        public CategoryEditViewModel()
        {
        }

        public CategoryEditViewModel(IChangeTrackableEntity entity)
            : base(entity)
        {
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Часть инвентарного номера")]
        public int InventoryPart { get; set; }

        [Display(Name = "Базовая цена товаров")]
        public double? BasePrice { get; set; }

        [Display(Name = "Ценовой срок")]
        public int? PriceType { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Раздел")]
        public Guid BranchId { get; set; }

        public string BranchTitle { get; set; }

        public static CategoryEditViewModel Create(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return new CategoryEditViewModel(category)
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                InventoryPart = category.InventoryPart,
                BasePrice = category.BasePrice,
                BranchId = category.BranchId,

                BranchTitle = category.BranchTitle,
            };
        }
    }
}