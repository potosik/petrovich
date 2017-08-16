using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;

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

        public static ProductsViewModel Create(Branch branch, Category category, Group group, ProductCollection products)
        {
            if (branch == null)
            {
                throw new ArgumentNullException(nameof(branch));
            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

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