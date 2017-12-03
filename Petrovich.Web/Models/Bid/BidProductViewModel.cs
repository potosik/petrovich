using Petrovich.Business.Models;
using Petrovich.Web.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.Bid
{
    public class BidProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public double AssessedValue { get; set; }

        public string BranchTitle { get; set; }
        public string CategoryTitle { get; set; }
        public string GroupTitle { get; set; }

        public static BidProductViewModel Create(ProductModel product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var priceDTO = product.GetHierarchicalPriceDTO();
            return new BidProductViewModel()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Price = priceDTO.Price,
                AssessedValue = product.AssessedValue,

                BranchTitle = product.Category.BranchTitle,
                CategoryTitle = product.Category.Title,
                GroupTitle = product.Group?.Title,
            };
        }
    }
}