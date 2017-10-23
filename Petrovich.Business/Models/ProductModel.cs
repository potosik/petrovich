using Petrovich.Business.Models.Base;
using Petrovich.Business.Models.Enumerations;
using Petrovich.Core;
using System;

namespace Petrovich.Business.Models
{
    public class ProductModel : BaseEntityModel, IPriceableEntityModel
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public double? Price { get; set; }
        public PriceTypeBusiness? PriceType { get; set; }
        public double AssessedValue { get; set; }

        public int InventoryPart { get; set; }

        public int? PurchaseYear { get; set; }
        public int? PurchaseMonth { get; set; }

        public Guid? ImageFullId { get; set; }
        public byte[] ImageFull { get; set; }

        public string ImageDefault { get; set; }
        public string ImageSmall { get; set; }
        
        public Guid BranchId { get; set; }
        public string BranchTitle { get; set; }

        public CategoryModel Category { get; set; }
        public GroupModel Group { get; set; }

        public string BranchInventoryPart { get; set; }
        public int CategoryInventoryPart { get; set; }
        public int GroupInventoryPart { get; set; }
        public string InventoryNumber
        {
            get
            {
                var categoryInventoryPart = CategoryInventoryPart.ToString(Constants.CategoryInventoryPartStringFormat);
                var groupInventoryPart = GroupInventoryPart.ToString(Constants.GroupInventoryPartStringFormat);
                var productInventoryPart = InventoryPart.ToString(Constants.ProductInventoryPartStringFormat);
                return $"{BranchInventoryPart}{categoryInventoryPart}{groupInventoryPart}{productInventoryPart}";
            }
        }
    }
}
