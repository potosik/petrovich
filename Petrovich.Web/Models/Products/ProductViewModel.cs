using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.Products
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string BranchTitle { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public Guid? GroupId { get; set; }
        public string GroupTitle { get; set; }

        public string InventoryNumber { get; set; }

        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }

        public static ProductViewModel Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductViewModel()
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

                Created = product.Created,
                CreatedBy = product.CreatedBy,
                Modified = product.Modified,
                ModifiedBy = product.ModifiedBy,
            };
        }
    }
}