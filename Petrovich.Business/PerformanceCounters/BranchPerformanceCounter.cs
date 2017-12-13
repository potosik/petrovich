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
            innerDataSource = dataSource;
        }

        public Task<BranchModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            using (new PerformanceMonitor(EventSource.ListBranches, new { pageIndex, pageSize }))
            {
                return innerDataSource.ListAsync(pageIndex, pageSize);
            }
        }

        public Task<BranchModel> FindByInventoryPartAsync(string inventoryPart)
        {
            using (new PerformanceMonitor(EventSource.FindBranchByInventoryPart, new { inventoryPart }))
            {
                return innerDataSource.FindByInventoryPartAsync(inventoryPart);
            }
        }

        public Task<BranchModel> CreateAsync(BranchModel branch)
        {
            using (new PerformanceMonitor(EventSource.CreateBranch, new { branch }))
            {
                return innerDataSource.CreateAsync(branch);
            }
        }

        public Task<BranchModel> FindAsync(Guid id)
        {
            using (new PerformanceMonitor(EventSource.FindBranchById, new { id }))
            {
                return innerDataSource.FindAsync(id);
            }
        }

        public Task<BranchModel> UpdateAsync(BranchModel branch)
        {
            using (new PerformanceMonitor(EventSource.UpdateBranch, new { branch }))
            {
                return innerDataSource.UpdateAsync(branch);
            }
        }

        public Task DeleteAsync(BranchModel branch)
        {
            using (new PerformanceMonitor(EventSource.DeleteBranch, new { branch }))
            {
                return innerDataSource.DeleteAsync(branch);
            }
        }

        public Task<BranchModelCollection> ListAllAsync()
        {
            using (new PerformanceMonitor(EventSource.ListAllBranchesAsync))
            {
                return innerDataSource.ListAllAsync();
            }
        }
    }
}
