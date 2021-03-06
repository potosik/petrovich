﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;
using Petrovich.Core;

namespace Petrovich.Web.Models.Manager
{
    public class GroupsViewModel
    {
        public Guid BranchId { get; set; }
        public string BranchTitle { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public IEnumerable<GroupViewModel> Groups { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }

        internal static GroupsViewModel Create(BranchModel branch, CategoryModel category, GroupModelCollection groups, ProductModelCollection products)
        {
            Guard.NotNullArgument(branch, nameof(branch));
            Guard.NotNullArgument(category, nameof(category));
            Guard.NotNullArgument(groups, nameof(groups));
            Guard.NotNullArgument(products, nameof(products));

            return new GroupsViewModel()
            {
                BranchId = branch.BranchId,
                BranchTitle = branch.Title,
                CategoryId = category.CategoryId,
                CategoryTitle = category.Title,
                Groups = groups.Select(item => GroupViewModel.Create(item)).OrderBy(item => item.Title),
                Products = products.Select(item => ProductViewModel.Create(item)).OrderBy(item => item.Title),
            };
        }
    }
}