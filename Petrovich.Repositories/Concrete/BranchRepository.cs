using Petrovich.Context;
using Petrovich.Context.Entities;
using System.Data.Entity;
using System.Threading.Tasks;
using System;

namespace Petrovich.Repositories.Concrete
{
    public class BranchRepository : BaseRepostory<Branch>, IBranchRepository
    {
        public BranchRepository(IPetrovichContext context)
            : base(context)
        {
        }

        public override async Task<Branch> FindAsync(Guid id)
        {
            return await context.Branches.FirstOrDefaultAsync(item => item.BranchId == id).ConfigureAwait(false);
        }

        public async Task<Branch> FindByInventoryPartAsync(string inventoryPart)
        {
            return await context.Branches.FirstOrDefaultAsync(item => item.InventoryPart == inventoryPart).ConfigureAwait(false);
        }
    }
}
