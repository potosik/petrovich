using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.DataSource.Mappers;
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
            this.branchRepository = branchRepository;
            this.branchMapper = branchMapper;
        }

        public async Task<BranchModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            try
            {
                var branches = await branchRepository.ListAsync(pageIndex, pageSize);
                var count = await branchRepository.ListCountAsync();
                var collection = branchMapper.ToBranchModelCollection(branches);
                collection.TotalCount = count;
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<BranchModel> FindByInventoryPartAsync(string inventoryPart)
        {
            try
            {
                var branch = await branchRepository.FindByInventoryPartAsync(inventoryPart);
                return branchMapper.ToBranchModel(branch);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<BranchModel> CreateAsync(BranchModel branch)
        {
            try
            {
                var contextBranch = branchMapper.ToContextBranch(branch);
                var newBranch = await branchRepository.CreateAsync(contextBranch);
                return branchMapper.ToBranchModel(newBranch);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<BranchModel> FindAsync(Guid id)
        {
            try
            {
                var branch = await branchRepository.FindAsync(id);
                return branchMapper.ToBranchModel(branch);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<BranchModel> UpdateAsync(BranchModel branch)
        {
            try
            {
                var targetBranch = await branchRepository.FindAsync(branch.BranchId);

                targetBranch.Title = branch.Title;
                targetBranch.InventoryPart = branch.InventoryPart;

                await branchRepository.UpdateAsync(targetBranch);
                return branchMapper.ToBranchModel(targetBranch);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task DeleteAsync(BranchModel branch)
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

        public async Task<BranchModelCollection> ListAllAsync()
        {
            try
            {
                var branches = await branchRepository.ListAllAsync();
                return branchMapper.ToBranchModelCollection(branches);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
