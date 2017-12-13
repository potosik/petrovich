using Petrovich.Business.Data;
using System;
using Petrovich.Business.Models;
using System.Threading.Tasks;
using Petrovich.Business.Logging;
using Petrovich.Core.Performance;
using System.Collections.Generic;

namespace Petrovich.Business.PerformanceCounters
{
    internal class ProductPerformanceCounter : PerformanceCounterBase, IProductDataSource
    {
        private readonly IProductDataSource innerDataSource;

        public ProductPerformanceCounter(IProductDataSource dataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            innerDataSource = dataSource;
        }

        public Task<ProductModelCollection> ListAsync(string filter, int pageIndex, int pageSize)
        {
            using (new PerformanceMonitor(EventSource.ListProducts, new { filter, pageIndex, pageSize }))
            {
                return innerDataSource.ListAsync(filter, pageIndex, pageSize);
            }
        }

        public Task<ProductModel> CreateAsync(ProductModel product)
        {
            using (new PerformanceMonitor(EventSource.CreateProduct, new { product }))
            {
                return innerDataSource.CreateAsync(product);
            }
        }

        public Task<ProductModel> FindAsync(Guid id)
        {
            using (new PerformanceMonitor(EventSource.FindProductById, new { id }))
            {
                return innerDataSource.FindAsync(id);
            }
        }

        public Task<ProductModel> UpdateAsync(ProductModel product)
        {
            using (new PerformanceMonitor(EventSource.UpdateProduct, new { product }))
            {
                return innerDataSource.UpdateAsync(product);
            }
        }

        public Task DeleteAsync(ProductModel product)
        {
            using (new PerformanceMonitor(EventSource.DeleteProduct, new { product }))
            {
                return innerDataSource.DeleteAsync(product);
            }
        }

        public Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.IsExistsProductsForCategory, new { categoryId }))
            {
                return innerDataSource.IsExistsForCategoryAsync(categoryId);
            }
        }

        public Task<bool> IsExistsForGroupAsync(Guid groupId)
        {
            using (new PerformanceMonitor(EventSource.IsExistsProductsForGroup, new { groupId }))
            {
                return innerDataSource.IsExistsForGroupAsync(groupId);
            }
        }

        public Task<int?> GetNewInventoryNumberInCategoryAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.GetNewInventoryNumberForProductByCategory, new { categoryId }))
            {
                return innerDataSource.GetNewInventoryNumberInCategoryAsync(categoryId);
            }
        }

        public Task<int?> GetNewInventoryNumberInGroupAsync(Guid groupId)
        {
            using (new PerformanceMonitor(EventSource.GetNewInventoryNumberForProductByGroup, new { groupId }))
            {
                return innerDataSource.GetNewInventoryNumberInGroupAsync(groupId);
            }
        }

        public Task<ProductModelCollection> SearchFastAsync(string query, int count)
        {
            using (new PerformanceMonitor(EventSource.ProductSearchFast, new { query, count }))
            {
                return innerDataSource.SearchFastAsync(query, count);
            }
        }

        public Task<ProductModelCollection> ListByCategoryIdAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.ListProductsByCategoryId, new { categoryId }))
            {
                return innerDataSource.ListByCategoryIdAsync(categoryId);
            }
        }

        public Task<ProductModelCollection> ListByGroupIdAsync(Guid groupId)
        {
            using (new PerformanceMonitor(EventSource.ListProductsByGroupId, new { groupId }))
            {
                return innerDataSource.ListByGroupIdAsync(groupId);
            }
        }

        public Task<ProductModelCollection> ListAsync(IEnumerable<Guid> productIds)
        {
            using (new PerformanceMonitor(EventSource.ListProductsByIds, new { productIds }))
            {
                return innerDataSource.ListAsync(productIds);
            }
        }
    }
}
