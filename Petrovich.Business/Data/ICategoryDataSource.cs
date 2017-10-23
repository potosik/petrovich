using System.Threading.Tasks;
using Petrovich.Business.Models;
using System;

namespace Petrovich.Business.Data
{
    public interface ICategoryDataSource
    {
        Task<CategoryModelCollection> ListAsync(int pageIndex, int pageSize);
        Task<CategoryModel> CreateAsync(CategoryModel category);
        Task<int?> GetNewInventoryNumberAsync(Guid branchId);
        Task<CategoryModel> FindAsync(Guid id);
        Task<CategoryModel> UpdateAsync(CategoryModel category);
        Task DeleteAsync(CategoryModel category);
        Task<bool> IsExistsForBranchAsync(Guid branchId);
        Task<CategoryModelCollection> ListByBranchIdAsync(Guid branchId);
        Task<CategoryModelCollection> ListAllAsync();
    }
}
