using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.PerformanceCounters.EventSources
{
    internal sealed partial class PerformanceEventSource
    {
        public void CreateFullImageAsync(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.CreateFullImageEventId, elapsed.ToString(), "IFullImageDataSource.CreateAsync", message);
        }

        public void UpdateOrCreateFullImageAsync(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.UpdateOrCreateFullImageEventId, elapsed.ToString(), "IFullImageDataSource.UpdateOrCreateAsync", message);
        }

        public void FindFullImageAsync(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.FindFullImageEventId, elapsed.ToString(), "IFullImageDataSource.FindAsync", message);
        }
    }
}
