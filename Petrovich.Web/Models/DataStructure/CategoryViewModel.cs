using Petrovich.Business.Models;
using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.DataStructure
{
    public class CategoryViewModel : BaseViewModel
    {
        public CategoryViewModel()
        {
        }

        public CategoryViewModel(IChangeTrackableEntity entity)
            : base(entity)
        {
        }

        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public int InventoryPart { get; set; }
        public string BranchTitle { get; set; }

        public static CategoryViewModel Create(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return new CategoryViewModel(category)
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                InventoryPart = category.InventoryPart,
                BranchTitle = category.BranchTitle,
            };
        }
    }
}