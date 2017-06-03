using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Business.Exceptions;
using Petrovich.Business.Logging;

namespace Petrovich.Business.Services
{
    public class DataStructureService : BaseService, IDataStructureService
    {
        private readonly IBranchDataSource branchDataSource;
        private readonly ICategoryDataSource categoryDataSource;
        private readonly IGroupDataSource groupDataSource;

        public DataStructureService(IBranchDataSource branchDataSource, ICategoryDataSource categoryDataSource, 
            IGroupDataSource groupDataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            this.branchDataSource = branchDataSource ?? throw new ArgumentNullException(nameof(branchDataSource));
            this.categoryDataSource = categoryDataSource ?? throw new ArgumentNullException(nameof(categoryDataSource));
            this.groupDataSource = groupDataSource ?? throw new ArgumentNullException(nameof(groupDataSource));
        }

        public async Task<BranchCollection> ListBranchesAsync()
        {
            await logger.LogNoneAsync("ListBranchesAsync: listing all branches.");
            return await branchDataSource.ListBranchesAsync();
        }

        public async Task<Branch> CreateBranchAsync(Branch branch)
        {
            if (branch == null)
            {
                await logger.LogInformationAsync("CreateBranchAsync: branch parameter is null.");
                throw new ArgumentNullException(nameof(branch));
            }

            await logger.LogNoneAsync($"CreateBranchAsync: trying to get branch by inventory part ({branch.InventoryPart}).");
            var branchByInventoryPart = await branchDataSource.FindByInventoryPartAsync(branch.InventoryPart);
            if (branchByInventoryPart != null)
            {
                await logger.LogInformationAsync($"CreateBranchAsync: branch with same inventory part found ({branch.InventoryPart}).");
                throw new DuplicateBranchInventoryPartException(branch.InventoryPart, branchByInventoryPart.BranchId);
            }

            await logger.LogNoneAsync("CreateBranchAsync: creating new branch.");
            return await branchDataSource.CreateBranchAsync(branch);
        }

        public async Task<Branch> FindBranchAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"FindBranchAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"FindBranchAsync: trying to get branch by id ({id}).");
            var branch = await branchDataSource.FindByIdAsync(id);
            if (branch == null)
            {
                await logger.LogInformationAsync($"FindBranchAsync: branch not found - {id}.");
                throw new BranchNotFoundException(id);
            }

            return branch;
        }

        public async Task<Branch> UpdateBranchAsync(Branch branch)
        {
            if (branch.BranchId == Guid.Empty)
            {
                await logger.LogInformationAsync($"UpdateBranchAsync: branchId is {branch.BranchId}.");
                throw new ArgumentOutOfRangeException(nameof(branch.BranchId));
            }

            await logger.LogNoneAsync($"UpdateBranchAsync: trying to get branch by id ({branch.BranchId}).");
            var dbBranch = await branchDataSource.FindByIdAsync(branch.BranchId);
            if (dbBranch == null)
            {
                await logger.LogInformationAsync($"UpdateBranchAsync: branch not found - {branch.BranchId}.");
                throw new BranchNotFoundException(branch.BranchId);
            }

            if (branch.InventoryPart != dbBranch.InventoryPart)
            {
                await logger.LogInformationAsync($"UpdateBranchAsync: branch inventory part changed - {branch.InventoryPart} / {dbBranch.InventoryPart}.");
                throw new BranchInventoryPartChangedException(branch.BranchId);
            }

            await logger.LogNoneAsync("UpdateBranchAsync: updating branch.");
            return await branchDataSource.UpdateAsync(branch);
        }

        public async Task DeleteBranchAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"DeleteBranchAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"DeleteBranchAsync: trying to get branch by id ({id}).");
            var branch = await branchDataSource.FindByIdAsync(id);
            if (branch == null)
            {
                await logger.LogInformationAsync($"DeleteBranchAsync: branch not found - {id}.");
                throw new BranchNotFoundException(id);
            }

            await logger.LogNoneAsync("DeleteBranchAsync: deleting branch.");
            await branchDataSource.DeleteAsync(branch);
        }
    }
}
