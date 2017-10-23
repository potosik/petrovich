using System.Threading.Tasks;
using Petrovich.Context.Entities;
using System;
using System.Collections.Generic;

namespace Petrovich.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IList<int>> ListUsedInventoryPartsAsync(Guid branchId);
        Task<bool> IsExistsForBranchAsync(Guid branchId);
        Task<IList<Category>> ListByBranchIdAsync(Guid branchId);
    }
}
