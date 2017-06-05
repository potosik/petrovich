﻿using Petrovich.Context;
using Petrovich.Context.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Petrovich.Repositories.Concrete
{
    public class ProductRepository : BaseRepostory<Product>, IProductRepository
    {
        public ProductRepository(IPetrovichContext context)
            : base(context)
        {
        }

        public override async Task<Product> FindAsync(Guid id)
        {
            return await context.Products.FirstOrDefaultAsync(item => item.ProductId == id).ConfigureAwait(false);
        }

        public async Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            return await context.Products.Where(item => item.CategoryId == categoryId).AnyAsync().ConfigureAwait(false);
        }

        public async Task<bool> IsExistsForGroupAsync(Guid groupId)
        {
            return await context.Products.Where(item => item.GroupId.HasValue && item.GroupId.Value == groupId).AnyAsync().ConfigureAwait(false);
        }

        public async Task<IList<int>> ListUsedInventoryPartsAsync(Guid categoryId)
        {
            return await context.Products.Where(item => item.CategoryId == categoryId).Select(item => item.InventoryPart).ToListAsync().ConfigureAwait(false);
        }
    }
}
