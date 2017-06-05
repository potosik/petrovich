using Petrovich.Business.Models.Base;
using Petrovich.Core;
using System;

namespace Petrovich.Business.Models
{
    public class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public int InventoryPart { get; set; }

        public string BranchTitle { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public Guid? GroupId { get; set; }
        public string GroupTitle { get; set; }

        public string BranchInventoryPart { get; set; }
        public int CategoryInventoryPart { get; set; }
        public string InventoryNumber
        {
            get
            {
                var categoryInventoryPart = CategoryInventoryPart.ToString(Constants.CategoryInventoryPartStringFormat);
                var productInventoryPart = InventoryPart.ToString(Constants.ProductInventoryPartStringFormat);
                return $"{BranchInventoryPart}{categoryInventoryPart}{productInventoryPart}";
            }
        }
    }
}
