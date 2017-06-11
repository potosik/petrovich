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
        public void ListGroups(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.ListGroupsEventId, elapsed.ToString(), "IGroupDataSource.ListAsync", message);
        }

        public void CreateGroup(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.CreateGroupEventId, elapsed.ToString(), "IGroupDataSource.CreateAsync", message);
        }

        public void FindGroupById(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.FindGroupByIdEventId, elapsed.ToString(), "IGroupDataSource.FindAsync", message);
        }

        public void UpdateGroup(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.UpdateGroupEventId, elapsed.ToString(), "IGroupDataSource.UpdateAsync", message);
        }

        public void DeleteGroup(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.DeleteGroupEventId, elapsed.ToString(), "IGroupDataSource.DeleteAsync", message);
        }

        public void IsExistsGroupsForCategory(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.IsExistsGroupsForCategoryEventId, elapsed.ToString(), "IGroupDataSource.IsExistsForCategoryAsync", message);
        }

        public void ListGroupsByCategoryId(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.ListGroupsByCategoryIdEventId, elapsed.ToString(), "IGroupDataSource.ListByCategoryIdAsync", message);
        }
    }
}
