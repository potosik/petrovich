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
    public class LogRepository : BaseRepostory<Log>, ILogRepository
    {
        public LogRepository(IPetrovichContext context)
            : base(context)
        {
        }
        
        public override async Task<Log> FindAsync(int id)
        {
            return await context.Logs.FirstOrDefaultAsync(item => item.LogId == id).ConfigureAwait(false);
        }

        public async Task<IList<Log>> ListAsync(int pageIndex, int pageSize)
        {
            return await context.Logs
                .OrderByDescending(item => item.LogId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
