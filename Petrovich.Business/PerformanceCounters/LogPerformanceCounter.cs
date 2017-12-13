using Petrovich.Business.Data;
using System;
using System.Threading.Tasks;
using Petrovich.Business.Models;

namespace Petrovich.Business.PerformanceCounters
{
    public class LogPerformanceCounter : ILogDataSource
    {
        private readonly ILogDataSource innerDataSource;

        public LogPerformanceCounter(ILogDataSource dataSource)
        {
            innerDataSource = dataSource;
        }

        public Task<LogModel> FindAsync(Guid id)
        {
            return innerDataSource.FindAsync(id);
        }

        public Task<LogModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            return innerDataSource.ListAsync(pageIndex, pageSize);
        }

        public Task WriteLogAsync(LogModel entity)
        {
            return innerDataSource.WriteLogAsync(entity);
        }
    }
}
