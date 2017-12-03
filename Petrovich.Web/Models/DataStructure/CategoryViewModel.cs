using Petrovich.Business.Models;
using Petrovich.Business.Models.Base;
using Petrovich.Web.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.DataStructure
{
    public class CategoryViewModel : ChangeTrackableViewModel
    {
        public CategoryViewModel()
        {
        }

        public CategoryViewModel(IChangeTrackableEntityModel entity)
            : base(entity)
        {
        }

        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public int InventoryPart { get; set; }
        public string Price { get; set; }
        public string InventoryPartString { get { return InventoryPart.ToString("D2"); } }
        public string BranchTitle { get; set; }

        public static CategoryViewModel Create(CategoryModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return new CategoryViewModel(category)
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                Price = category.GetPriceInformation(category.PriceCalculationType),
                InventoryPart = category.InventoryPart,
                BranchTitle = category.BranchTitle,
            };
        }
    }
}