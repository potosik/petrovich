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
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<ProductModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            using (new PerformanceMonitor(EventSource.ListProducts, new { pageIndex, pageSize }))
            {
                return await innerDataSource.ListAsync(pageIndex, pageSize);
            }
        }

        public async Task<ProductModel> CreateAsync(ProductModel product)
        {
            using (new PerformanceMonitor(EventSource.CreateProduct, new { product }))
            {
                return await innerDataSource.CreateAsync(product);
            }
        }

        public async Task<ProductModel> FindAsync(Guid id)
        {
            using (new PerformanceMonitor(EventSource.FindProductById, new { id }))
            {
                return await innerDataSource.FindAsync(id);
            }
        }

        public async Task<ProductModel> UpdateAsync(ProductModel product)
        {
            using (new PerformanceMonitor(EventSource.UpdateProduct, new { product }))
            {
                return await innerDataSource.UpdateAsync(product);
            }
        }

        public async Task DeleteAsync(ProductModel product)
        {
            using (new PerformanceMonitor(EventSource.DeleteProduct, new { product }))
            {
                await innerDataSource.DeleteAsync(product);
            }
        }

        public async Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.IsExistsProductsForCategory, new { categoryId }))
            {
                return await innerDataSource.IsExistsForCategoryAsync(categoryId);
            }
        }

        public async Task<bool> IsExistsForGroupAsync(Guid groupId)
        {
            using (new PerformanceMonitor(EventSource.IsExistsProductsForGroup, new { groupId }))
            {
                return await innerDataSource.IsExistsForGroupAsync(groupId);
            }
        }

        public async Task<int?> GetNewInventoryNumberAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.GetNewInventoryNumberForProduct, new { categoryId }))
            {
                return await innerDataSource.GetNewInventoryNumberAsync(categoryId);
            }
        }

        public async Task<ProductModelCollection> SearchFastAsync(string query, int count)
        {
            using (new PerformanceMonitor(EventSource.ProductSearchFast, new { query, count }))
            {
                return await innerDataSource.SearchFastAsync(query, count);
            }
        }

        public async Task<ProductModelCollection> ListByCategoryIdAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.ListProductsByCategoryId, new { categoryId }))
            {
                return await innerDataSource.ListByCategoryIdAsync(categoryId);
            }
        }

        public async Task<ProductModelCollection> ListByGroupIdAsync(Guid groupId)
        {
            using (new PerformanceMonitor(EventSource.ListProductsByGroupId, new { groupId }))
            {
                return await innerDataSource.ListByGroupIdAsync(groupId);
            }
        }

        public async Task<ProductModelCollection> ListAsync(IEnumerable<Guid> productIds)
        {
            using (new PerformanceMonitor(EventSource.ListProductsByIds, new { productIds }))
            {
                return await innerDataSource.ListAsync(productIds);
            }
        }
    }
}
