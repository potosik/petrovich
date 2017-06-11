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
        public void ListCategories(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.ListCategoriesEventId, elapsed.ToString(), "ICategoryDataSource.ListAsync", message);
        }

        public void FindCategoryByInventoryPart(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.FindCategoryByInventoryPartEventId, elapsed.ToString(), "ICategoryDataSource.FindByInventoryPartAsync", message);
        }

        public void CreateCategory(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.CreateCategoryEventId, elapsed.ToString(), "ICategoryDataSource.CreateAsync", message);
        }

        public void GetNewInventoryNumberForCategory(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.GetNewInventoryNumberForCategoryEventId, elapsed.ToString(), "ICategoryDataSource.GetNewInventoryNumberAsync", message);
        }

        public void FindCategoryById(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.FindCategoryByIdEventId, elapsed.ToString(), "ICategoryDataSource.FindAsync", message);
        }

        public void UpdateCategory(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.UpdateCategoryEventId, elapsed.ToString(), "ICategoryDataSource.UpdateAsync", message);
        }

        public void DeleteCategory(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.DeleteCategoryEventId, elapsed.ToString(), "ICategoryDataSource.DeleteAsync", message);
        }

        public void IsExistsCategoriesForBranch(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.IsExistsCategoriesForBranchEventId, elapsed.ToString(), "ICategoryDataSource.IsExistsForBranchAsync", message);
        }

        public void ListCategoriesByBranchId(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.ListCategoriesByBranchIdEventId, elapsed.ToString(), "ICategoryDataSource.ListByBranchIdAsync", message);
        }
    }
}
