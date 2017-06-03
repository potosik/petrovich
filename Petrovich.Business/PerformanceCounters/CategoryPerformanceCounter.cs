using Petrovich.Business.Data;
using System;
using Petrovich.Business.Models;
using System.Threading.Tasks;

namespace Petrovich.Business.PerformanceCounters
{
    public class CategoryPerformanceCounter : ICategoryDataSource
    {
        private readonly ICategoryDataSource innerDataSource;

        public CategoryPerformanceCounter(ICategoryDataSource dataSource)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<CategoryCollection> ListAsync()
        {
            return await innerDataSource.ListAsync();
        }

        public async Task<Category> FindByInventoryPartAsync(int inventoryPart, Guid branchId)
        {
            return await innerDataSource.FindByInventoryPartAsync(inventoryPart, branchId);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            return await innerDataSource.CreateAsync(category);
        }

        public async Task<int?> GetNewInventoryNumberAsync(Guid branchId)
        {
            return await innerDataSource.GetNewInventoryNumberAsync(branchId);
        }

        public async Task<Category> FindAsync(Guid id)
        {
            return await innerDataSource.FindAsync(id);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            return await innerDataSource.UpdateAsync(category);
        }

        public async Task DeleteAsync(Category category)
        {
            await innerDataSource.DeleteAsync(category);
        }

        public async Task<bool> IsExistsForBranchIdAsync(Guid branchId)
        {
            return await innerDataSource.IsExistsForBranchIdAsync(branchId);
        }
    }
}
