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
            return await branchDataSource.ListAsync();
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
            return await branchDataSource.CreateAsync(branch);
        }

        public async Task<Branch> FindBranchAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"FindBranchAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"FindBranchAsync: trying to get branch by id ({id}).");
            var branch = await branchDataSource.FindAsync(id);
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
            var dbBranch = await branchDataSource.FindAsync(branch.BranchId);
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
            var branch = await branchDataSource.FindAsync(id);
            if (branch == null)
            {
                await logger.LogInformationAsync($"DeleteBranchAsync: branch not found - {id}.");
                throw new BranchNotFoundException(id);
            }

            await logger.LogNoneAsync($"DeleteBranchAsync: check if child categories exiests for branch {id}.");
            var categories = await categoryDataSource.IsExistsForBranchIdAsync(id);
            if (categories)
            {
                await logger.LogInformationAsync($"DeleteBranchAsync: branch '{id}' could not be deleted - child categories exiests for branch.");
                throw new ChildCategoriesExistsException(id);
            }

            await logger.LogNoneAsync("DeleteBranchAsync: deleting branch.");
            await branchDataSource.DeleteAsync(branch);
        }

        public async Task<CategoryCollection> ListCategoriesAsync()
        {
            await logger.LogNoneAsync("ListCategoriesAsync: listing all categories.");
            return await categoryDataSource.ListAsync();
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            if (category == null)
            {
                await logger.LogInformationAsync("CreateCategoryAsync: category parameter is null.");
                throw new ArgumentNullException(nameof(category));
            }

            await logger.LogNoneAsync($"CreateCategoryAsync: trying to get branch {category.BranchId}.");
            var branch = await branchDataSource.FindAsync(category.BranchId);
            if (branch == null)
            {
                await logger.LogInformationAsync($"CreateCategoryAsync: branch not found - {category.BranchId}.");
                throw new BranchNotFoundException(category.BranchId);
            }

            await logger.LogNoneAsync($"CreateCategoryAsync: trying to get category by inventory part ({category.InventoryPart}).");
            var categoryByInventoryPart = await categoryDataSource.FindByInventoryPartAsync(category.InventoryPart, category.BranchId);
            if (categoryByInventoryPart != null)
            {
                await logger.LogInformationAsync($"CreateCategoryAsync: category with same inventory part found ({category.InventoryPart}). Branch: {category.BranchId}.");
                throw new DuplicateCategoryInventoryPartException(category.InventoryPart, category.BranchId, categoryByInventoryPart.CategoryId);
            }

            await logger.LogNoneAsync($"CreateCategoryAsync: trying to get max inventory number for category {category.BranchId}.");
            var inventoryPartValue = await categoryDataSource.GetNewInventoryNumberAsync(category.BranchId);
            if (!inventoryPartValue.HasValue)
            {
                await logger.LogNoneAsync($"CreateCategoryAsync: there are no empty inventory numbers for branch {category.BranchId}.");
                throw new NoBranchCategoriesSlotsException(category.BranchId);
            }

            category.InventoryPart = inventoryPartValue.Value;
            await logger.LogNoneAsync("CreateCategoryAsync: creating new category.");
            return await categoryDataSource.CreateAsync(category);
        }

        public async Task<Category> FindCategoryAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"FindCategoryAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"FindCategoryAsync: trying to get category by id ({id}).");
            var category = await categoryDataSource.FindAsync(id);
            if (category == null)
            {
                await logger.LogInformationAsync($"FindCategoryAsync: category not found - {id}.");
                throw new CategoryNotFoundException(id);
            }

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            if (category.CategoryId == Guid.Empty)
            {
                await logger.LogInformationAsync($"UpdateCategoryAsync: categoryId is {category.CategoryId}.");
                throw new ArgumentOutOfRangeException(nameof(category.CategoryId));
            }

            await logger.LogNoneAsync($"UpdateCategoryAsync: trying to get branch by id ({category.BranchId}).");
            var dbBranch = await branchDataSource.FindAsync(category.BranchId);
            if (dbBranch == null)
            {
                await logger.LogInformationAsync($"UpdateCategoryAsync: branch not found - {category.BranchId}.");
                throw new BranchNotFoundException(category.BranchId);
            }

            await logger.LogNoneAsync($"UpdateCategoryAsync: trying to get category by id ({category.CategoryId}).");
            var dbCategory = await categoryDataSource.FindAsync(category.CategoryId);
            if (dbCategory == null)
            {
                await logger.LogInformationAsync($"UpdateCategoryAsync: category not found - {category.CategoryId}.");
                throw new CategoryNotFoundException(category.CategoryId);
            }

            if (category.InventoryPart != dbCategory.InventoryPart)
            {
                await logger.LogInformationAsync($"UpdateCategoryAsync: category inventory part changed - {category.InventoryPart} / {dbCategory.InventoryPart}.");
                throw new CategoryInventoryPartChangedException(category.CategoryId);
            }

            await logger.LogNoneAsync("UpdateCategoryAsync: updating category.");
            return await categoryDataSource.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"DeleteCategoryAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"DeleteCategoryAsync: trying to get category by id ({id}).");
            var category = await categoryDataSource.FindAsync(id);
            if (category == null)
            {
                await logger.LogInformationAsync($"DeleteCategoryAsync: category not found - {id}.");
                throw new CategoryNotFoundException(id);
            }

            await logger.LogNoneAsync("DeleteCategoryAsync: deleting category.");
            await categoryDataSource.DeleteAsync(category);
        }
    }
}
