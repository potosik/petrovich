﻿using Petrovich.Business.Models;
using Petrovich.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.Manager
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string InventoryNumbers { get; set; }
        
        public static CategoryViewModel Create(CategoryModel category)
        {
            Guard.NotNullArgument(category, nameof(category));

            return new CategoryViewModel()
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                InventoryNumbers = $"{category.BranchInventoryPart}{category.InventoryPart.ToString("D2")}*",
            };
        }
    }
}