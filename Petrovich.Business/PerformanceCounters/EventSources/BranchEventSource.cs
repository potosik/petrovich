using Newtonsoft.Json;
using Petrovich.Core;
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
        public void ListBranches(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.ListBranchesEventId, elapsed.ToString(), "IBranchDataSource.ListAsync", message);
        }
        
        public void FindBranchByInventoryPart(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.FindBranchByInventoryPartEventId, elapsed.ToString(), "IBranchDataSource.FindByInventoryPartAsync", message);
        }
        
        public void CreateBranch(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.CreateBranchEventId, elapsed.ToString(), "IBranchDataSource.CreateAsync", message);
        }

        public void FindBranchById(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.FindBranchByIdEventId, elapsed.ToString(), "IBranchDataSource.FindAsync", message);
        }
        
        public void UpdateBranch(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.UpdateBranchEventId, elapsed.ToString(), "IBranchDataSource.UpdateAsync", message);
        }
        
        public void DeleteBranch(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.DeleteBranchEventId, elapsed.ToString(), "IBranchDataSource.DeleteAsync", message);
        }

        public void ListAllBranchesAsync(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.ListAllBranchesEventId, elapsed.ToString(), "IBranchDataSource.ListAllAsync", message);
        }

        internal void ProductSearchFast(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.ProductSearchFastEventId, elapsed.ToString(), "IBranchDataSource.SearchFastAsync", message);
        }
    }
}
