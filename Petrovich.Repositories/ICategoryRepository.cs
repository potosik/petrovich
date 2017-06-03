using System.Threading.Tasks;
using Petrovich.Context.Entities;
using System;
using System.Collections.Generic;

namespace Petrovich.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> FindByInventoryPartAsync(int inventoryPart, Guid branchId);
        Task<IList<int>> ListUsedInventoryPartsAsync(Guid branchId);
        Task<bool> IsExistsForBranchAsync(Guid branchId);
    }
}
