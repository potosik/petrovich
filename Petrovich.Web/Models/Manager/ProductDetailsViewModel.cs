﻿using System;
using Petrovich.Business.Models.Base;
using Petrovich.Business.Models;
using Petrovich.Core.Utils;
using Petrovich.Business.Models.Enumerations;
using Petrovich.Web.Core.Extensions;
using Petrovich.Core;

namespace Petrovich.Web.Models.Manager
{
    public class ProductDetailsViewModel : ChangeTrackableViewModel
    {
        public ProductDetailsViewModel()
        {
        }

        public ProductDetailsViewModel(IChangeTrackableEntityModel entity) 
            : base(entity)
        {
        }
        
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Defects { get; set; }
        public double? Price { get; set; }
        public string PriceText { get; set; }
        public double AssessedValue { get; set; }
        public string InventoryNumber { get; set; }
        public string PurchaseDate { get; set; }

        public Guid? ImageFullId { get; set; }
        public string ImageDefault { get; set; }
        public string ImageSmall { get; set; }

        public Guid BranchId { get; set; }
        public string BranchTitle { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        
        public Guid? GroupId { get; set; }
        public string GroupTitle { get; set; }

        public static ProductDetailsViewModel Create(ProductModel product)
        {
            Guard.NotNullArgument(product, nameof(product));

            var priceDTO = product.GetHierarchicalPriceDTO();
            return new ProductDetailsViewModel(product)
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Defects = product.Defects,
                Price = priceDTO.Price,
                PriceText = product.GetHierarchicalPrice(),
                AssessedValue = product.AssessedValue,
                InventoryNumber = product.InventoryNumber,
                PurchaseDate = DateTimeUtils.CreatePurchaseDate(product.PurchaseYear, product.PurchaseMonth),

                ImageFullId = product.ImageFullId,
                ImageDefault = product.ImageDefault ?? String.Empty,
                ImageSmall = product.ImageSmall ?? String.Empty,

                BranchId = product.BranchId,
                BranchTitle = product.BranchTitle,

                CategoryId = product.Category.CategoryId,
                CategoryTitle = product.Category.Title,

                GroupId = product.Group?.GroupId,
                GroupTitle = product.Group?.Title,
            };
        }
    }
}