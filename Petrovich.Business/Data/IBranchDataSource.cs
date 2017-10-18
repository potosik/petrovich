using System.Collections.Generic;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using System;

namespace Petrovich.Business.Data
{
    public interface IBranchDataSource
    {
        Task<BranchModelCollection> ListAsync(int pageIndex, int pageSize);
        Task<BranchModel> FindByInventoryPartAsync(string inventoryPart);
        Task<BranchModel> CreateAsync(BranchModel branch);
        Task<BranchModel> FindAsync(Guid id);
        Task<BranchModel> UpdateAsync(BranchModel branch);
        Task DeleteAsync(BranchModel branch);
        Task<BranchModelCollection> ListAllAsync();
    }
}
