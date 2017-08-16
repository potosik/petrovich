using Petrovich.Business.Models;
using System;
using System.Threading.Tasks;

namespace Petrovich.Business.Data
{
    public interface IProductDataSource
    {
        Task<ProductCollection> ListAsync(int pageIndex, int pageSize);
        Task<Product> CreateAsync(Product product);
        Task<Product> FindAsync(Guid id);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<bool> IsExistsForCategoryAsync(Guid categoryId);
        Task<bool> IsExistsForGroupAsync(Guid groupId);
        Task<int?> GetNewInventoryNumberAsync(Guid categoryId);
        Task<ProductCollection> SearchFastAsync(string query, int count);
        Task<ProductCollection> ListByCategoryIdAsync(Guid categoryId);
        Task<ProductCollection> ListByGroupIdAsync(Guid groupId);
    }
}
