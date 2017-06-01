using Petrovich.Context;
using Petrovich.Context.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Concrete
{
    public class GroupRepository : BaseRepostory<Group>, IGroupRepository
    {
        public GroupRepository(IPetrovichContext context)
            : base(context)
        {
        }

        public override async Task<Group> FindAsync(int id)
        {
            return await context.Groups.FirstOrDefaultAsync(item => item.GroupId == id).ConfigureAwait(false);
        }
    }
}
