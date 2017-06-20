using Petrovich.Business.Data;
using Petrovich.Business.Logging;
using Petrovich.Core.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.PerformanceCounters
{
    internal class FullImagePerformanceCounter : PerformanceCounterBase, IFullImageDataSource
    {
        private readonly IFullImageDataSource innerDataSource;

        public FullImagePerformanceCounter(IFullImageDataSource dataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<Guid> CreateAsync(byte[] content)
        {
            using (new PerformanceMonitor(EventSource.CreateFullImageAsync, new { content.Length }))
            {
                return await innerDataSource.CreateAsync(content);
            }
        }

        public async Task<Guid> UpdateOrCreateAsync(byte[] content, Guid? imageId)
        {
            using (new PerformanceMonitor(EventSource.UpdateOrCreateFullImageAsync, new { content.Length, imageId }))
            {
                return await innerDataSource.UpdateOrCreateAsync(content, imageId);
            }
        }

        public async Task<byte[]> FindAsync(Guid id)
        {
            using (new PerformanceMonitor(EventSource.FindFullImageAsync, new { id }))
            {
                return await innerDataSource.FindAsync(id);
            }
        }
    }
}
