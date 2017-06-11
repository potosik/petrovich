﻿using Microsoft.Practices.Unity;
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
        public void ListProducts(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.ListProductsEventId, elapsed.ToString(), "IProductDataSource.ListAsync", message);
        }

        public void CreateProduct(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.CreateProductEventId, elapsed.ToString(), "IProductDataSource.CreateAsync", message);
        }

        public void FindProductById(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.FindProductByIdEventId, elapsed.ToString(), "IProductDataSource.FindAsync", message);
        }

        public void UpdateProduct(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.UpdateProductEventId, elapsed.ToString(), "IProductDataSource.UpdateAsync", message);
        }

        public void DeleteProduct(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.DeleteProductEventId, elapsed.ToString(), "IProductDataSource.DeleteAsync", message);
        }

        public void IsExistsProductsForCategory(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.IsExistsProductsForCategoryEventId, elapsed.ToString(), "IProductDataSource.IsExistsForCategoryAsync", message);
        }

        public void IsExistsProductsForGroup(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.IsExistsProductsForGroupEventId, elapsed.ToString(), "IProductDataSource.IsExistsForGroupAsync", message);
        }

        public void GetNewInventoryNumberForProduct(TimeSpan elapsed, object arguments)
        {
            var message = BuildMessage(arguments);
            logger.LogPerformanceMetrics(PerformanceMetricEventIds.GetNewInventoryNumberForProductEventId, elapsed.ToString(), "IProductDataSource.GetNewInventoryNumberAsync", message);
        }
    }
}