using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.Search
{
    public class ProductFastViewModel
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string InventoryNumber { get; set; }

        public string BranchTitle { get; set; }
        public string CategoryTitle { get; set; }
        public string GroupTitle { get; set; }
        
        public static ProductFastViewModel Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductFastViewModel()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                InventoryNumber = product.InventoryNumber,

                BranchTitle = product.BranchTitle,
                CategoryTitle = product.CategoryTitle,
                GroupTitle = product.GroupTitle,
            };
        }
    }
}