using Petrovich.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Context;
using System.Data.Entity;

namespace Petrovich.Repositories.Concrete
{
    public class FullImageRepository : BaseRepostory<FullImage>, IFullImageRepository
    {
        public FullImageRepository(IPetrovichContext context) 
            : base(context)
        {
        }

        public override async Task<FullImage> FindAsync(Guid id)
        {
            return await context.FullImages.FirstOrDefaultAsync(item => item.FullImageId == id).ConfigureAwait(false);
        }
    }
}
