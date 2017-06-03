using Petrovich.Business.Models;
using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.DataStructure
{
    public class CategoryModel
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public int InventoryPart { get; set; }
        public string BranchTitle { get; set; }

        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }

        public static CategoryModel Create(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return new CategoryModel()
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                InventoryPart = category.InventoryPart,
                BranchTitle = category.BranchTitle,

                Created = category.Created,
                CreatedBy = category.CreatedBy,
                Modified = category.Modified,
                ModifiedBy = category.ModifiedBy,
            };
        }
    }
}