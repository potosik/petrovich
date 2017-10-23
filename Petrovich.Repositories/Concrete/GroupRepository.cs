using Petrovich.Context;
using Petrovich.Context.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Petrovich.Repositories.Concrete
{
    public class GroupRepository : BaseRepostory<Group>, IGroupRepository
    {
        public GroupRepository(IPetrovichContext context)
            : base(context)
        {
        }

        public override async Task<Group> FindAsync(Guid id)
        {
            return await context.Groups.Include(item => item.Category).Include(item => item.Category.Branch).FirstOrDefaultAsync(item => item.GroupId == id).ConfigureAwait(false);
        }

        public override async Task<IList<Group>> ListAllAsync()
        {
            return await context.Groups.Include(item => item.Category).Include(item => item.Category.Branch).OrderByDescending(item => item.Created).ToListAsync();
        }

        public async Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            return await context.Groups.Where(item => item.CategoryId == categoryId).AnyAsync().ConfigureAwait(false);
        }

        public async Task<IList<Group>> ListByCategoryIdAsync(Guid categoryId)
        {
            return await context.Groups.Include(item => item.Category).Include(item => item.Category.Branch).Where(item => item.CategoryId == categoryId).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IList<int>> ListUsedInventoryPartsAsync(Guid categoryId)
        {
            return await context.Groups.Include(item => item.Category).Include(item => item.Category.Branch).Where(item => item.CategoryId == categoryId).Select(item => item.InventoryPart).ToListAsync().ConfigureAwait(false);
        }
    }
}
