﻿using Petrovich.Context;
using Petrovich.Context.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Petrovich.Repositories.Concrete
{
    public class CategoryRepository : BaseRepostory<Category>, ICategoryRepository
    {
        public CategoryRepository(IPetrovichContext context)
            : base(context)
        {
        }

        public override async Task<Category> FindAsync(Guid id)
        {
            return await context.Categories.FirstOrDefaultAsync(item => item.CategoryId == id).ConfigureAwait(false);
        }

        public override async Task<IList<Category>> ListAllAsync()
        {
            return await context.Categories.Include(item => item.Branch).OrderByDescending(item => item.Created).ToListAsync();
        }

        public async Task<Category> FindByInventoryPartAsync(int inventoryPart, Guid branchId)
        {
            return await context.Categories.FirstOrDefaultAsync(item => item.BranchId == branchId && item.InventoryPart == inventoryPart).ConfigureAwait(false);
        }

        public async Task<IList<int>> ListUsedInventoryPartsAsync(Guid branchId)
        {
            return await context.Categories.Where(item => item.BranchId == branchId).Select(item => item.InventoryPart).ToListAsync().ConfigureAwait(false);
        }

        public async Task<bool> IsExistsForBranchIdAsync(Guid branchId)
        {
            return await context.Categories.Where(item => item.BranchId == branchId).AnyAsync().ConfigureAwait(false);
        }
    }
}
