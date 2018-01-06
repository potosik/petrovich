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

        public Task<ClientModelCollection> ListAsync(string filter, int pageIndex, int pageSize)
        {
            using (new PerformanceMonitor(EventSource.ListClients, new { filter, pageIndex, pageSize }))
            {
                return innerDataSource.ListAsync(filter, pageIndex, pageSize);
            }
        }

        public Task<ClientModel> FindAsync(Guid clientId)
        {
            using (new PerformanceMonitor(EventSource.FindClient, new { clientId }))
            {
                return innerDataSource.FindAsync(clientId);
            }
        }

        public Task<ClientModel> FindAsync(string passportId)
        {
            using (new PerformanceMonitor(EventSource.FindClient, new { passportId }))
            {
                return innerDataSource.FindAsync(passportId);
            }
        }

        public Task<ClientModel> CreateAsync(ClientModel client)
        {
            using (new PerformanceMonitor(EventSource.CreateClient, new { client }))
            {
                return innerDataSource.CreateAsync(client);
            }
        }

        public Task<ClientModel> UpdateAsync(ClientModel client)
        {
            using (new PerformanceMonitor(EventSource.UpdateClient, new { client }))
            {
                return innerDataSource.UpdateAsync(client);
            }
        }
    }
}
