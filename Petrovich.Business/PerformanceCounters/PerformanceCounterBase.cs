using Petrovich.Business.Logging;
using Petrovich.Business.PerformanceCounters.EventSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.PerformanceCounters
{
    internal class PerformanceCounterBase
    {
        protected PerformanceEventSource EventSource { get; private set; }

        public PerformanceCounterBase(ILoggingService loggingService)
        {
            EventSource = new PerformanceEventSource(loggingService);
        }
    }
}
