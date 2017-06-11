using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models.Base;

namespace Petrovich.Web.Models.Products
{
    public class ProductViewModel : BaseViewModel
    {
        public ProductViewModel()
        {
        }

        public ProductViewModel(IChangeTrackableEntity entity) 
            : base(entity)
        {
        }

        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string BranchTitle { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public Guid? GroupId { get; set; }
        public string GroupTitle { get; set; }

        public string InventoryNumber { get; set; }

        public static ProductViewModel Create(Product product)
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

                BranchTitle = product.BranchTitle,

                CategoryId = product.CategoryId,
                CategoryTitle = product.CategoryTitle,

                GroupId = product.GroupId,
                GroupTitle = product.GroupTitle,

                InventoryNumber = product.InventoryNumber,
            };
        }
    }
}