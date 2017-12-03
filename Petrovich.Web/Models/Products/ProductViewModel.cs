using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models.Base;
using Petrovich.Web.Core.Extensions;

namespace Petrovich.Web.Models.Products
{
    public class ProductViewModel : ChangeTrackableViewModel
    {
        public ProductViewModel()
        {
        }

        public ProductViewModel(IChangeTrackableEntityModel entity) 
            : base(entity)
        {
        }

        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Defects { get; set; }
        public string Price { get; set; }

        public string BranchTitle { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public Guid? GroupId { get; set; }
        public string GroupTitle { get; set; }

        public string InventoryNumber { get; set; }

        public static ProductViewModel Create(ProductModel product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductViewModel(product)
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Defects = product.Defects,
                Price = product.GetPriceInformation(product.Category.PriceCalculationType),

                BranchTitle = product.BranchTitle,

                CategoryId = product.Category.CategoryId,
                CategoryTitle = product.Category.Title,

                GroupId = product.Group?.GroupId,
                GroupTitle = product.Group?.Title,

                InventoryNumber = product.InventoryNumber,
            };
        }
    }
}