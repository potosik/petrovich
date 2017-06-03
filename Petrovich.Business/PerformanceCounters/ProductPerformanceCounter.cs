using Petrovich.Business.Data;
using System;

namespace Petrovich.Business.PerformanceCounters
{
    public class ProductPerformanceCounter : IProductDataSource
    {
        private readonly IProductDataSource innerDataSource;

        public ProductPerformanceCounter(IProductDataSource dataSource)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }
    }
}
