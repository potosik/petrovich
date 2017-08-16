using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;

namespace Petrovich.Web.Models.Manager
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }

        public static ProductViewModel Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductViewModel()
            {
                ProductId = product.ProductId,
                Title = product.Title,
            };
        }
    }
}