using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;
using Petrovich.Core;

namespace Petrovich.Web.Models.Manager
{
    public class ProductsViewModel
    {
        public Guid BranchId { get; set; }
        public string BranchTitle { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public Guid GroupId { get; set; }
        public string GroupTitle { get; set; }
        
        public IEnumerable<ProductViewModel> Products { get; set; }

        public static ProductsViewModel Create(BranchModel branch, CategoryModel category, GroupModel group, ProductModelCollection products)
        {
            Guard.NotNullArgument(branch, nameof(branch));
            Guard.NotNullArgument(category, nameof(category));
            Guard.NotNullArgument(group, nameof(group));
            Guard.NotNullArgument(products, nameof(products));

            return new ProductsViewModel()
            {
                BranchId = branch.BranchId,
                BranchTitle = branch.Title,
                CategoryId = category.CategoryId,
                CategoryTitle = category.Title,
                GroupId = group.GroupId,
                GroupTitle = group.Title,
                Products = products.Select(item => ProductViewModel.Create(item)).OrderBy(item => item.Title),
            };
        }
    }
}