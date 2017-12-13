using Petrovich.Business.Data;
using Petrovich.Business.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Core.Performance;

namespace Petrovich.Business.PerformanceCounters
{
    internal class ClientPerformanceCounter : PerformanceCounterBase, IClientDataSource
    {
        private readonly IClientDataSource innerDataSource;

        public ClientPerformanceCounter(IClientDataSource dataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            innerDataSource = dataSource;
        }

        public Task<ClientModelCollection> ListAsync(string filter)
        {
            using (new PerformanceMonitor(EventSource.ListClients, new { filter }))
            {
                return innerDataSource.ListAsync(filter);
            }
        }

        public Task<ClientModel> FindAsync(Guid clientId)
        {
            using (new PerformanceMonitor(EventSource.FindClient, new { clientId }))
            {
                return innerDataSource.FindAsync(clientId);
            }
        }
    }
}
