using Petrovich.Context.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petrovich.Context;
using System.Data.Entity;
using System;

namespace Petrovich.Repositories.Concrete
{
    public class LogRepository : BaseRepostory<Log>, ILogRepository
    {
        public LogRepository(IPetrovichContext context)
            : base(context)
        {
        }
        
        public override async Task<Log> FindAsync(Guid id)
        {
            return await context.Logs.FirstOrDefaultAsync(item => item.LogId == id).ConfigureAwait(false);
        }
    }
}
