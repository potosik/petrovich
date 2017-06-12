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
        private const int ShowLastMonthsNumber = 3;

        public LogRepository(IPetrovichContext context)
            : base(context)
        {
        }
        
        public override async Task<Log> FindAsync(Guid id)
        {
            return await context.Logs.FirstOrDefaultAsync(item => item.LogId == id).ConfigureAwait(false);
        }

        public override Task<IList<Log>> ListAllAsync()
        {
            throw new Exception("Method is not allowed for logs repository");
        }

        public override async Task<IList<Log>> ListAsync(int pageIndex, int pageSize)
        {
            var minDate = GetMinLogDate();
            return await context.Logs.Where(item => item.Created >= minDate).OrderByDescending(item => item.Created).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);
        }

        public override async Task<int> ListCountAsync()
        {
            var minDate = GetMinLogDate();
            return await context.Logs.Where(item => item.Created >= minDate).CountAsync();
        }

        private DateTime GetMinLogDate()
        {
            return DateTime.UtcNow.AddMonths(-ShowLastMonthsNumber);
        }
    }
}
