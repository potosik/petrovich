using Petrovich.Business.Models;
using Petrovich.Core.Navigation;
using Petrovich.Web.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Models.Search
{
    public class ProductFastViewModel
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string InventoryNumber { get; set; }
        public string ImageSmall { get; set; }

        public string SelfUri { get; set; }

        public string BranchTitle { get; set; }
        public string CategoryTitle { get; set; }
        public string GroupTitle { get; set; }
        
        public static ProductFastViewModel Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductFastViewModel()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                InventoryNumber = product.InventoryNumber,
                ImageSmall = product.ImageSmall,

                SelfUri = PetrovichRoutes.Manager.ProductDetails.GetLink(new { id = product.ProductId }),

                BranchTitle = product.BranchTitle,
                CategoryTitle = product.Category.Title,
                GroupTitle = product.Group?.Title,
            };
        }
    }
}