using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models.Enumerations;
using Petrovich.Business.Models;

namespace Petrovich.Business.PerformanceCounters
{
    public class 
        LogPerformanceCounter : ILogDataSource
    {
        private readonly ILogDataSource innerDataSource;

        public LogPerformanceCounter(ILogDataSource dataSource)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<Log> FindAsync(int id)
        {
            return await innerDataSource.FindAsync(id);
        }

        public async Task<LogCollection> ListAsync(int pageIndex, int pageSize)
        {
            return await innerDataSource.ListAsync(pageIndex, pageSize);
        }

        public async Task WriteLogAsync(Log entity)
        {
            await innerDataSource.WriteLogAsync(entity);
        }
    }
}
