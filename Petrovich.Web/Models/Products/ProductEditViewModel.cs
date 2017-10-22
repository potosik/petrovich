using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Petrovich.Business.Models;
using Petrovich.Core.Extensions;

namespace Petrovich.Web.Models.Products
{
    public class ProductEditViewModel : ChangeTrackableViewModel
    {
        public ProductEditViewModel()
        {
            Groups = new List<SelectListItem>();
            PriceTypes = new List<SelectListItem>();
        }

        public ProductEditViewModel(IChangeTrackableEntityModel entity) 
            : base(entity)
        {
            Groups = new List<SelectListItem>();
            PriceTypes = new List<SelectListItem>();
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public Guid ProductId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Цена проката (BYN)")]
        public double? Price { get; set; }

        [Display(Name = "Ценовой срок")]
        public int? PriceType { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Оценочная стоимость (BYN)")]
        public double AssessedValue { get; set; }

        [Display(Name = "Год закупки")]
        [Range(2000, 2100, ErrorMessageResourceName = "Product_PurchaseYear_Range_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int? PurchaseYear { get; set; }

        [Display(Name = "Месяц закупки")]
        [Range(1, 12, ErrorMessageResourceName = "Product_PurchaseMonth_Range_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int? PurchaseMonth { get; set; }

        public Guid? ImageFullId { get; set; }

        [Display(Name = "Фотография")]
        public string ImageFull { get; set; }

        [Display(Name = "Фотография")]
        public string ImageDefault { get; set; }

        [Display(Name = "Фотография")]
        public string ImageSmall { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Инвентарный номер")]
        public int InventoryPart { get; set; }

        [Display(Name = "Раздел")]
        public string BranchTitle { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Категория")]
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        [Display(Name = "Группа")]
        public Guid? GroupId { get; set; }

        public IList<SelectListItem> Groups { get; set; }
        public IList<SelectListItem> PriceTypes { get; set; }


        public static ProductEditViewModel Create(ProductModel product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductEditViewModel(product)
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                PriceType = (int?)product.PriceType,
                AssessedValue = product.AssessedValue,
                InventoryPart = product.InventoryPart,

                PurchaseYear = product.PurchaseYear,
                PurchaseMonth = product.PurchaseMonth,

                ImageFullId = product.ImageFullId,
                ImageFull = product.ImageFull.ToBase64String(),

                ImageDefault = product.ImageDefault,
                ImageSmall = product.ImageSmall,

                BranchTitle = product.BranchTitle,

                CategoryId = product.Category.CategoryId,
                CategoryTitle = product.Category.Title,

                GroupId = product.Group?.GroupId,
            };
        }
    }
}