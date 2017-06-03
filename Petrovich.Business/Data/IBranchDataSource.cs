using System.Collections.Generic;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using System;

namespace Petrovich.Business.Data
{
    public interface IBranchDataSource
    {
        Task<BranchCollection> ListBranchesAsync();
        Task<Branch> FindByInventoryPartAsync(string inventoryPart);
        Task<Branch> CreateBranchAsync(Branch branch);
        Task<Branch> FindByIdAsync(Guid id);
        Task<Branch> UpdateAsync(Branch branch);
        Task DeleteAsync(Branch branch);
    }
}
