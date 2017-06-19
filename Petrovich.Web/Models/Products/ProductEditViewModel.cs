using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Petrovich.Business.Models;

namespace Petrovich.Web.Models.Products
{
    public class ProductEditViewModel : BaseViewModel
    {
        public ProductEditViewModel()
        {
        }

        public ProductEditViewModel(IChangeTrackableEntity entity) 
            : base(entity)
        {
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public Guid ProductId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
        
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
        public string GroupTitle { get; set; }

        public static ProductEditViewModel Create(Product product)
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
                InventoryPart = product.InventoryPart,

                ImageFull = product.ImageFull,
                ImageDefault = product.ImageDefault,
                ImageSmall = product.ImageSmall,

                BranchTitle = product.BranchTitle,

                CategoryId = product.CategoryId,
                CategoryTitle = product.CategoryTitle,

                GroupId = product.GroupId,
                GroupTitle = product.GroupTitle,
            };
        }
    }
}