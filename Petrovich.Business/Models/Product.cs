﻿using Petrovich.Business.Models.Base;
using Petrovich.Business.Models.Enumerations;
using Petrovich.Core;
using System;

namespace Petrovich.Business.Models
{
    public class Product : BaseEntity, IPriceableEntity
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public double? Price { get; set; }
        public PriceType? PriceType { get; set; }

        public int InventoryPart { get; set; }

        public int? PurchaseYear { get; set; }
        public int? PurchaseMonth { get; set; }

        public Guid? ImageFullId { get; set; }
        public byte[] ImageFull { get; set; }

        public string ImageDefault { get; set; }
        public string ImageSmall { get; set; }
        
        public Guid BranchId { get; set; }
        public string BranchTitle { get; set; }

        public Category Category { get; set; }
        public Group Group { get; set; }

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
