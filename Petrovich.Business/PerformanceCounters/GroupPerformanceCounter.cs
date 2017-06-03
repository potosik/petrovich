using Petrovich.Business.Data;
using System;

namespace Petrovich.Business.PerformanceCounters
{
    public class GroupPerformanceCounter : IGroupDataSource
    {
        private readonly IGroupDataSource innerDataSource;

        public GroupPerformanceCounter(IGroupDataSource dataSource)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }
    }
}
