using System.Threading.Tasks;
using Petrovich.Business.Models;
using System;

namespace Petrovich.Business.Data
{
    public interface ICategoryDataSource
    {
        Task<CategoryCollection> ListAsync(int pageIndex, int pageSize);
        Task<Category> FindByInventoryPartAsync(int inventoryPart, Guid branchId);
        Task<Category> CreateAsync(Category category);
        Task<int?> GetNewInventoryNumberAsync(Guid branchId);
        Task<Category> FindAsync(Guid id);
        Task<Category> UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> IsExistsForBranchAsync(Guid branchId);
        Task<CategoryCollection> ListByBranchIdAsync(Guid branchId);
        Task<CategoryCollection> ListAllAsync();
    }
}
