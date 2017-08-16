﻿using System;
using Petrovich.Business.Models.Base;
using Petrovich.Business.Models;
using Petrovich.Core.Utils;

namespace Petrovich.Web.Models.Manager
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        public ProductDetailsViewModel()
        {
        }

        public ProductDetailsViewModel(IChangeTrackableEntity entity) 
            : base(entity)
        {
        }
        
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InventoryNumber { get; set; }
        public string PurchaseDate { get; set; }

        public Guid? ImageFullId { get; set; }
        public string ImageDefault { get; set; }
        
        public Guid BranchId { get; set; }
        public string BranchTitle { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        
        public Guid? GroupId { get; set; }
        public string GroupTitle { get; set; }

        public static ProductDetailsViewModel Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductDetailsViewModel(product)
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                InventoryNumber = product.InventoryNumber,
                PurchaseDate = DateTimeUtils.CreatePurchaseDate(product.PurchaseYear, product.PurchaseMonth),

                ImageFullId = product.ImageFullId,
                ImageDefault = product.ImageDefault,

                BranchId = product.BranchId,
                BranchTitle = product.BranchTitle,

                CategoryId = product.CategoryId,
                CategoryTitle = product.CategoryTitle,

                GroupId = product.GroupId,
                GroupTitle = product.GroupTitle,
            };
        }
    }
}