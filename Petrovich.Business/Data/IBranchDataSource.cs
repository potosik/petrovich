using System.Collections.Generic;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using System;

namespace Petrovich.Business.Data
{
    public interface IBranchDataSource
    {
        Task<BranchCollection> ListAsync();
        Task<Branch> FindByInventoryPartAsync(string inventoryPart);
        Task<Branch> CreateAsync(Branch branch);
        Task<Branch> FindAsync(Guid id);
        Task<Branch> UpdateAsync(Branch branch);
        Task DeleteAsync(Branch branch);
    }
}
