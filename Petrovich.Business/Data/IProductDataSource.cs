using Petrovich.Business.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Petrovich.Business.Data
{
    public interface IProductDataSource
    {
        Task<ProductModelCollection> ListAsync(int pageIndex, int pageSize);
        Task<ProductModel> CreateAsync(ProductModel product);
        Task<ProductModel> FindAsync(Guid id);
        Task<ProductModel> UpdateAsync(ProductModel product);
        Task DeleteAsync(ProductModel product);
        Task<bool> IsExistsForCategoryAsync(Guid categoryId);
        Task<bool> IsExistsForGroupAsync(Guid groupId);
        Task<int?> GetNewInventoryNumberInCategoryAsync(Guid categoryId);
        Task<int?> GetNewInventoryNumberInGroupAsync(Guid groupId);
        Task<ProductModelCollection> SearchFastAsync(string query, int count);
        Task<ProductModelCollection> ListByCategoryIdAsync(Guid categoryId);
        Task<ProductModelCollection> ListByGroupIdAsync(Guid groupId);
        Task<ProductModelCollection> ListAsync(IEnumerable<Guid> productIds);
    }
}
