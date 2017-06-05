using Petrovich.Business.Data;
using System;
using Petrovich.Business.Models;
using System.Threading.Tasks;

namespace Petrovich.Business.PerformanceCounters
{
    public class ProductPerformanceCounter : IProductDataSource
    {
        private readonly IProductDataSource innerDataSource;

        public ProductPerformanceCounter(IProductDataSource dataSource)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<ProductCollection> ListAsync()
        {
            return await innerDataSource.ListAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await innerDataSource.CreateAsync(product);
        }

        public async Task<Product> FindAsync(Guid id)
        {
            return await innerDataSource.FindAsync(id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            return await innerDataSource.UpdateAsync(product);
        }

        public async Task DeleteAsync(Product product)
        {
            await innerDataSource.DeleteAsync(product);
        }

        public async Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            return await innerDataSource.IsExistsForCategoryAsync(categoryId);
        }

        public async Task<bool> IsExistsForGroupAsync(Guid groupId)
        {
            return await innerDataSource.IsExistsForGroupAsync(groupId);
        }

        public async Task<int?> GetNewInventoryNumberAsync(Guid categoryId)
        {
            return await innerDataSource.GetNewInventoryNumberAsync(categoryId);
        }
    }
}
