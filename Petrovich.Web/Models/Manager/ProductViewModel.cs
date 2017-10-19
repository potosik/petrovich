using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;
using Petrovich.Web.Core.Extensions;
using Petrovich.Business.Models.Enumerations;

namespace Petrovich.Web.Models.Manager
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? PriceType { get; set; }

        public string PriceText { get; set; }

        public static ProductViewModel Create(ProductModel product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductViewModel()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                PriceType = (int?)product.PriceType,
                PriceText = product.GetHierarchicalPrice(),
            };
        }
    }
}