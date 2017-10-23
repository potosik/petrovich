using Petrovich.Business.Data;
using System;
using Petrovich.Business.Models;
using System.Threading.Tasks;
using Petrovich.Business.Logging;
using Petrovich.Core.Performance;

namespace Petrovich.Business.PerformanceCounters
{
    internal class CategoryPerformanceCounter : PerformanceCounterBase, ICategoryDataSource
    {
        private readonly ICategoryDataSource innerDataSource;

        public CategoryPerformanceCounter(ICategoryDataSource dataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<CategoryModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            using (new PerformanceMonitor(EventSource.ListCategories, new { pageIndex, pageSize }))
            {
                return await innerDataSource.ListAsync(pageIndex, pageSize);
            }
        }
        
        public async Task<CategoryModel> CreateAsync(CategoryModel category)
        {
            using (new PerformanceMonitor(EventSource.CreateCategory, new { category }))
            {
                return await innerDataSource.CreateAsync(category);
            }
        }

        public async Task<int?> GetNewInventoryNumberAsync(Guid branchId)
        {
            using (new PerformanceMonitor(EventSource.GetNewInventoryNumberForCategory, new { branchId }))
            {
                return await innerDataSource.GetNewInventoryNumberAsync(branchId);
            }
        }

        public async Task<CategoryModel> FindAsync(Guid id)
        {
            using (new PerformanceMonitor(EventSource.FindCategoryById, new { id }))
            {
                return await innerDataSource.FindAsync(id);
            }
        }

        public async Task<CategoryModel> UpdateAsync(CategoryModel category)
        {
            using (new PerformanceMonitor(EventSource.UpdateCategory, new { category }))
            {
                return await innerDataSource.UpdateAsync(category);
            }
        }

        public async Task DeleteAsync(CategoryModel category)
        {
            using (new PerformanceMonitor(EventSource.DeleteCategory, new { category }))
            {
                await innerDataSource.DeleteAsync(category);
            }
        }

        public async Task<bool> IsExistsForBranchAsync(Guid branchId)
        {
            using (new PerformanceMonitor(EventSource.IsExistsCategoriesForBranch, new { branchId }))
            {
                return await innerDataSource.IsExistsForBranchAsync(branchId);
            }
        }

        public async Task<CategoryModelCollection> ListByBranchIdAsync(Guid branchId)
        {
            using (new PerformanceMonitor(EventSource.ListCategoriesByBranchId, new { branchId }))
            {
                return await innerDataSource.ListByBranchIdAsync(branchId);
            }
        }

        public async Task<CategoryModelCollection> ListAllAsync()
        {
            using (new PerformanceMonitor(EventSource.ListAllCategoriesAsync))
            {
                return await innerDataSource.ListAllAsync();
            }
        }
    }
}
