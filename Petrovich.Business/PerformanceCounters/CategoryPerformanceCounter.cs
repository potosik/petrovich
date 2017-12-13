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
            innerDataSource = dataSource;
        }

        public Task<CategoryModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            using (new PerformanceMonitor(EventSource.ListCategories, new { pageIndex, pageSize }))
            {
                return innerDataSource.ListAsync(pageIndex, pageSize);
            }
        }
        
        public Task<CategoryModel> CreateAsync(CategoryModel category)
        {
            using (new PerformanceMonitor(EventSource.CreateCategory, new { category }))
            {
                return innerDataSource.CreateAsync(category);
            }
        }

        public Task<int?> GetNewInventoryNumberAsync(Guid branchId)
        {
            using (new PerformanceMonitor(EventSource.GetNewInventoryNumberForCategory, new { branchId }))
            {
                return innerDataSource.GetNewInventoryNumberAsync(branchId);
            }
        }

        public Task<CategoryModel> FindAsync(Guid id)
        {
            using (new PerformanceMonitor(EventSource.FindCategoryById, new { id }))
            {
                return innerDataSource.FindAsync(id);
            }
        }

        public Task<CategoryModel> UpdateAsync(CategoryModel category)
        {
            using (new PerformanceMonitor(EventSource.UpdateCategory, new { category }))
            {
                return innerDataSource.UpdateAsync(category);
            }
        }

        public Task DeleteAsync(CategoryModel category)
        {
            using (new PerformanceMonitor(EventSource.DeleteCategory, new { category }))
            {
                return innerDataSource.DeleteAsync(category);
            }
        }

        public Task<bool> IsExistsForBranchAsync(Guid branchId)
        {
            using (new PerformanceMonitor(EventSource.IsExistsCategoriesForBranch, new { branchId }))
            {
                return innerDataSource.IsExistsForBranchAsync(branchId);
            }
        }

        public Task<CategoryModelCollection> ListByBranchIdAsync(Guid branchId)
        {
            using (new PerformanceMonitor(EventSource.ListCategoriesByBranchId, new { branchId }))
            {
                return innerDataSource.ListByBranchIdAsync(branchId);
            }
        }

        public Task<CategoryModelCollection> ListAllAsync()
        {
            using (new PerformanceMonitor(EventSource.ListAllCategoriesAsync))
            {
                return innerDataSource.ListAllAsync();
            }
        }
    }
}
