using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;
using Petrovich.Web.Core.Extensions;
using Petrovich.Business.Models.Enumerations;
using Petrovich.Core;

namespace Petrovich.Web.Models.Manager
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Defects { get; set; }
        public double? Price { get; set; }
        public double AssessedValue { get; set; }
        public string ImageSmall { get; set; }

        public string PriceText { get; set; }
        public string InventoryNumber { get; set; }

        public static ProductViewModel Create(ProductModel product)
        {
            Guard.NotNullArgument(product, nameof(product));

            var priceDTO = product.GetHierarchicalPriceDTO();
            return new ProductViewModel()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Defects = product.Defects,
                Price = priceDTO.Price,
                PriceText = product.GetHierarchicalPrice(),
                AssessedValue = product.AssessedValue,
                ImageSmall = product.ImageSmall,
                InventoryNumber = product.InventoryNumber,
            };
        }
    }
}