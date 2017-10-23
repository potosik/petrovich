using Petrovich.Context;
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
        
        public async Task<IList<int>> ListUsedInventoryPartsAsync(Guid branchId)
        {
            return await context.Categories.Where(item => item.BranchId == branchId).Select(item => item.InventoryPart).ToListAsync().ConfigureAwait(false);
        }

        public async Task<bool> IsExistsForBranchAsync(Guid branchId)
        {
            return await context.Categories.Where(item => item.BranchId == branchId).AnyAsync().ConfigureAwait(false);
        }

        public async Task<IList<Category>> ListByBranchIdAsync(Guid branchId)
        {
            return await context.Categories.Where(item => item.BranchId == branchId).ToListAsync().ConfigureAwait(false);
        }
    }
}
