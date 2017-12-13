using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Petrovich.Business.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.PerformanceCounters.EventSources
{
    internal sealed partial class PerformanceEventSource
    {
        public void ListClients(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.ListClientsEventId, elapsed.ToString(), "IClientDataSource.ListAsync", message);
        }

        public void FindClient(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.FindClientByIdEventId, elapsed.ToString(), "IClientDataSource.FindAsync", message);
        }
    }
}
