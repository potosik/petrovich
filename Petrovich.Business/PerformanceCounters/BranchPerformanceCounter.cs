using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petrovich.Business.Data;
using Petrovich.Business.Models;
using Petrovich.Core.Performance;
using Petrovich.Business.PerformanceCounters.EventSources;
using Petrovich.Business.Logging;

namespace Petrovich.Business.PerformanceCounters
{
    internal class BranchPerformanceCounter : PerformanceCounterBase, IBranchDataSource
    {
        private readonly IBranchDataSource innerDataSource;

        public BranchPerformanceCounter(IBranchDataSource dataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<BranchCollection> ListAsync(int pageIndex, int pageSize)
        {
            using (new PerformanceMonitor(EventSource.ListBranches, new { pageIndex, pageSize }))
            {
                return await innerDataSource.ListAsync(pageIndex, pageSize);
            }
        }

        public async Task<Branch> FindByInventoryPartAsync(string inventoryPart)
        {
            using (new PerformanceMonitor(EventSource.FindBranchByInventoryPart, new { inventoryPart }))
            {
                return await innerDataSource.FindByInventoryPartAsync(inventoryPart);
            }
        }

        public async Task<Branch> CreateAsync(Branch branch)
        {
            using (new PerformanceMonitor(EventSource.CreateBranch, new { branch }))
            {
                return await innerDataSource.CreateAsync(branch);
            }
        }

        public async Task<Branch> FindAsync(Guid id)
        {
            using (new PerformanceMonitor(EventSource.FindBranchById, new { id }))
            {
                return await innerDataSource.FindAsync(id);
            }
        }

        public async Task<Branch> UpdateAsync(Branch branch)
        {
            using (new PerformanceMonitor(EventSource.UpdateBranch, new { branch }))
            {
                return await innerDataSource.UpdateAsync(branch);
            }
        }

        public async Task DeleteAsync(Branch branch)
        {
            using (new PerformanceMonitor(EventSource.DeleteBranch, new { branch }))
            {
                await innerDataSource.DeleteAsync(branch);
            }
        }

        public async Task<BranchCollection> ListAllAsync()
        {
            using (new PerformanceMonitor(EventSource.ListAllBranchesAsync))
            {
                return await innerDataSource.ListAllAsync();
            }
        }
    }
}
