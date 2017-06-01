using Petrovich.Context;
using Petrovich.Context.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Concrete
{
    public class BranchRepository : BaseRepostory<Branch>, IBranchRepository
    {
        public BranchRepository(IPetrovichContext context)
            : base(context)
        {
        }

        public override async Task<Branch> FindAsync(int id)
        {
            return await context.Branches.FirstOrDefaultAsync(item => item.BranchId == id).ConfigureAwait(false);
        }
    }
}
