using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Repositories.Mappers;
using System.Data.Entity.Core;
using Petrovich.Business.Exceptions;

namespace Petrovich.Repositories.DataSources
{
    public class BranchDataSource : IBranchDataSource
    {
        private readonly IBranchRepository branchRepository;
        private readonly IBranchMapper branchMapper;

        public BranchDataSource(IBranchRepository branchRepository, IBranchMapper branchMapper)
        {
            this.branchRepository = branchRepository ?? throw new ArgumentNullException(nameof(branchRepository));
            this.branchMapper = branchMapper ?? throw new ArgumentNullException(nameof(branchMapper));
        }

        public async Task<BranchCollection> ListBranchesAsync()
        {
            try
            {
                var branches = await branchRepository.ListAllAsync();
                return branchMapper.ToBusinessEntityCollection(branches);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Branch> FindByInventoryPartAsync(string inventoryPart)
        {
            try
            {
                var branch = await branchRepository.FindByInventoryPartAsync(inventoryPart);
                return branchMapper.ToBusinessEntity(branch);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Branch> CreateBranchAsync(Branch branch)
        {
            try
            {
                var contextBranch = branchMapper.ToContextEntity(branch);
                var newBranch = await branchRepository.CreateAsync(contextBranch);
                return branchMapper.ToBusinessEntity(newBranch);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Branch> FindByIdAsync(Guid id)
        {
            try
            {
                var branch = await branchRepository.FindAsync(id);
                return branchMapper.ToBusinessEntity(branch);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Branch> UpdateAsync(Branch branch)
        {
            try
            {
                var targetBranch = await branchRepository.FindAsync(branch.BranchId);

                targetBranch.Title = branch.Title;
                targetBranch.InventoryPart = branch.InventoryPart;

                await branchRepository.UpdateAsync(targetBranch);
                return branchMapper.ToBusinessEntity(targetBranch);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task DeleteAsync(Branch branch)
        {
            try
            {
                var targetBranch = await branchRepository.FindAsync(branch.BranchId);
                await branchRepository.DeleteAsync(targetBranch);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
