using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petrovich.Business.Data;
using Petrovich.Business.Models;

namespace Petrovich.Business.PerformanceCounters
{
    public class BranchPerformanceCounter : IBranchDataSource
    {
        private readonly IBranchDataSource innerDataSource;

        public BranchPerformanceCounter(IBranchDataSource dataSource)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<BranchCollection> ListBranchesAsync()
        {
            return await innerDataSource.ListBranchesAsync();
        }

        public async Task<Branch> FindByInventoryPartAsync(string inventoryPart)
        {
            return await innerDataSource.FindByInventoryPartAsync(inventoryPart);
        }

        public async Task<Branch> CreateBranchAsync(Branch branch)
        {
            return await innerDataSource.CreateBranchAsync(branch);
        }

        public async Task<Branch> FindByIdAsync(Guid id)
        {
            return await innerDataSource.FindByIdAsync(id);
        }

        public async Task<Branch> UpdateAsync(Branch branch)
        {
            return await innerDataSource.UpdateAsync(branch);
        }

        public async Task DeleteAsync(Branch branch)
        {
            await innerDataSource.DeleteAsync(branch);
        }
    }
}
