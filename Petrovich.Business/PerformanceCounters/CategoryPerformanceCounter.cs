using Petrovich.Business.Data;
using System;

namespace Petrovich.Business.PerformanceCounters
{
    public class CategoryPerformanceCounter : ICategoryDataSource
    {
        private readonly ICategoryDataSource innerDataSource;

        public CategoryPerformanceCounter(ICategoryDataSource dataSource)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }
    }
}
