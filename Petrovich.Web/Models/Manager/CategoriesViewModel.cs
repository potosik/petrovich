using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;

namespace Petrovich.Web.Models.Manager
{
    public class CategoriesViewModel
    {
        public Guid BranchId { get; set; }
        public string BranchTitle { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        internal static object Create(Branch branch, CategoryCollection categories)
        {
            return new CategoriesViewModel()
            {
                BranchId = branch.BranchId,
                BranchTitle = branch.Title,
                Categories = categories.Select(item => CategoryViewModel.Create(item)).OrderBy(item => item.Title),
            };
        }
    }
}