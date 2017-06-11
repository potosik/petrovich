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
        private readonly ILoggingService logger;

        public PerformanceEventSource(ILoggingService loggingService)
        {
            logger = loggingService;
        }
        
        private static string BuildMessage(object args)
        {
            return args != null ? JsonConvert.SerializeObject(args) : "none";
        }
    }
}
