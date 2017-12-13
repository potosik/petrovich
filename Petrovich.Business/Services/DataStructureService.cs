using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Business.Exceptions;
using Petrovich.Business.Logging;
using Petrovich.Core;

namespace Petrovich.Business.Services
{
    public class DataStructureService : BaseService, IDataStructureService
    {
        private readonly IBranchDataSource branchDataSource;
        private readonly ICategoryDataSource categoryDataSource;
        private readonly IGroupDataSource groupDataSource;

        private readonly IProductDataSource productDataSource;

        public DataStructureService(IBranchDataSource branchDataSource, ICategoryDataSource categoryDataSource, 
            IGroupDataSource groupDataSource, IProductDataSource productDataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            this.branchDataSource = branchDataSource;
            this.categoryDataSource = categoryDataSource;
            this.groupDataSource = groupDataSource;
            this.productDataSource = productDataSource;
        }

        public async Task<BranchModelCollection> ListBranchesAsync(int pageIndex, int pageSize)
        {
            await logger.LogNoneAsync($"DataStructureService.ListBranchesAsync: listing branches (pageIndex: {pageIndex} pageSize: {pageSize}).");
            return await branchDataSource.ListAsync(pageIndex, pageSize);
        }

        public async Task<BranchModel> CreateBranchAsync(BranchModel branch)
        {
            Guard.NotNullArgument(branch, nameof(branch));

            await logger.LogNoneAsync($"DataStructureService.CreateBranchAsync: trying to get branch by inventory part ({branch.InventoryPart}).");
            var branchByInventoryPart = await branchDataSource.FindByInventoryPartAsync(branch.InventoryPart);
            if (branchByInventoryPart != null)
            {
                await logger.LogInformationAsync($"DataStructureService.CreateBranchAsync: branch with same inventory part found ({branch.InventoryPart}).");
                throw new DuplicateBranchInventoryPartException(branch.InventoryPart, branchByInventoryPart.BranchId);
            }

            await logger.LogNoneAsync("DataStructureService.CreateBranchAsync: creating new branch.");
            return await branchDataSource.CreateAsync(branch);
        }

        public async Task<BranchModel> FindBranchAsync(Guid id)
        {
            Guard.ValidateIdentifier(id, nameof(id));

            await logger.LogNoneAsync($"DataStructureService.FindBranchAsync: trying to get branch by id ({id}).");
            var branch = await branchDataSource.FindAsync(id);
            if (branch == null)
            {
                await logger.LogInformationAsync($"DataStructureService.FindBranchAsync: branch not found - {id}.");
                throw new BranchNotFoundException(id);
            }

            return branch;
        }

        public async Task<BranchModel> UpdateBranchAsync(BranchModel branch)
        {
            Guard.ValidateIdentifier(branch.BranchId, nameof(branch.BranchId));

            await logger.LogNoneAsync($"DataStructureService.UpdateBranchAsync: trying to get branch by id ({branch.BranchId}).");
            var dbBranch = await branchDataSource.FindAsync(branch.BranchId);
            if (dbBranch == null)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateBranchAsync: branch not found - {branch.BranchId}.");
                throw new BranchNotFoundException(branch.BranchId);
            }

            if (branch.InventoryPart != dbBranch.InventoryPart)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateBranchAsync: branch inventory part changed - {branch.InventoryPart} / {dbBranch.InventoryPart}.");
                throw new BranchInventoryPartChangedException(branch.BranchId);
            }

            await logger.LogNoneAsync("DataStructureService.UpdateBranchAsync: updating branch.");
            return await branchDataSource.UpdateAsync(branch);
        }

        public async Task DeleteBranchAsync(Guid id)
        {
            Guard.ValidateIdentifier(id, nameof(id));

            await logger.LogNoneAsync($"DataStructureService.DeleteBranchAsync: trying to get branch by id ({id}).");
            var branch = await branchDataSource.FindAsync(id);
            if (branch == null)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteBranchAsync: branch not found - {id}.");
                throw new BranchNotFoundException(id);
            }

            await logger.LogNoneAsync($"DataStructureService.DeleteBranchAsync: check if child categories exists for branch {id}.");
            var categoriesExists = await categoryDataSource.IsExistsForBranchAsync(id);
            if (categoriesExists)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteBranchAsync: branch '{id}' could not be deleted - child categories exists for branch.");
                throw new ChildCategoriesExistsException(id);
            }

            await logger.LogNoneAsync("DataStructureService.DeleteBranchAsync: deleting branch.");
            await branchDataSource.DeleteAsync(branch);
        }

        public async Task<BranchModelCollection> ListAllBranchesAsync()
        {
            await logger.LogNoneAsync("DataStructureService.ListAllBranchesAsync: listing all branches.");
            return await branchDataSource.ListAllAsync();
        }

        public async Task<CategoryModelCollection> ListCategoriesAsync(int pageIndex, int pageSize)
        {
            await logger.LogNoneAsync($"DataStructureService.ListCategoriesAsync: listing categories (pageIndex: {pageIndex} pageSize: {pageSize}).");
            return await categoryDataSource.ListAsync(pageIndex, pageSize);
        }

        public async Task<CategoryModel> CreateCategoryAsync(CategoryModel category)
        {
            Guard.NotNullArgument(category, nameof(category));

            await logger.LogNoneAsync($"DataStructureService.CreateCategoryAsync: trying to get branch {category.BranchId}.");
            var branch = await branchDataSource.FindAsync(category.BranchId);
            if (branch == null)
            {
                await logger.LogInformationAsync($"DataStructureService.CreateCategoryAsync: branch not found - {category.BranchId}.");
                throw new BranchNotFoundException(category.BranchId);
            }
            
            await logger.LogNoneAsync($"DataStructureService.CreateCategoryAsync: trying to get inventory number for category for branch {category.BranchId}.");
            var inventoryPartValue = await categoryDataSource.GetNewInventoryNumberAsync(category.BranchId);
            if (!inventoryPartValue.HasValue)
            {
                await logger.LogNoneAsync($"DataStructureService.CreateCategoryAsync: there are no empty inventory numbers for branch {category.BranchId}.");
                throw new NoBranchCategoriesSlotsException(category.BranchId);
            }

            category.InventoryPart = inventoryPartValue.Value;

            await logger.LogNoneAsync("DataStructureService.CreateCategoryAsync: creating new category.");
            return await categoryDataSource.CreateAsync(category);
        }

        public async Task<CategoryModel> FindCategoryAsync(Guid id)
        {
            Guard.ValidateIdentifier(id, nameof(id));

            await logger.LogNoneAsync($"DataStructureService.FindCategoryAsync: trying to get category by id ({id}).");
            var category = await categoryDataSource.FindAsync(id);
            if (category == null)
            {
                await logger.LogInformationAsync($"DataStructureService.FindCategoryAsync: category not found - {id}.");
                throw new CategoryNotFoundException(id);
            }

            return category;
        }

        public async Task<CategoryModel> UpdateCategoryAsync(CategoryModel category)
        {
            Guard.ValidateIdentifier(category.CategoryId, nameof(category.CategoryId));

            await logger.LogNoneAsync($"DataStructureService.UpdateCategoryAsync: trying to get category by id ({category.CategoryId}).");
            var dbCategory = await categoryDataSource.FindAsync(category.CategoryId);
            if (dbCategory == null)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateCategoryAsync: category not found - {category.CategoryId}.");
                throw new CategoryNotFoundException(category.CategoryId);
            }

            await logger.LogNoneAsync($"DataStructureService.UpdateCategoryAsync: trying to get branch by id ({category.BranchId}).");
            var dbBranch = await branchDataSource.FindAsync(category.BranchId);
            if (dbBranch == null)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateCategoryAsync: branch not found - {category.BranchId}.");
                throw new BranchNotFoundException(category.BranchId);
            }

            if (category.InventoryPart != dbCategory.InventoryPart)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateCategoryAsync: category inventory part changed - {category.InventoryPart} / {dbCategory.InventoryPart}.");
                throw new CategoryInventoryPartChangedException(category.CategoryId);
            }

            await logger.LogNoneAsync("DataStructureService.UpdateCategoryAsync: updating category.");
            return await categoryDataSource.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            Guard.ValidateIdentifier(id, nameof(id));

            await logger.LogNoneAsync($"DataStructureService.DeleteCategoryAsync: trying to get category by id ({id}).");
            var category = await categoryDataSource.FindAsync(id);
            if (category == null)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteCategoryAsync: category not found - {id}.");
                throw new CategoryNotFoundException(id);
            }
            
            await logger.LogNoneAsync($"DataStructureService.DeleteCategoryAsync: check if child groups exists for category {id}.");
            var groupsExists = await groupDataSource.IsExistsForCategoryAsync(id);
            if (groupsExists)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteCategoryAsync: category '{id}' could not be deleted - child groups exists for category.");
                throw new ChildGroupsExistsException(id);
            }

            await logger.LogNoneAsync($"DataStructureService.DeleteCategoryAsync: check if child products exists for category {id}.");
            var productsExists = await productDataSource.IsExistsForCategoryAsync(id);
            if (productsExists)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteCategoryAsync: category '{id}' could not be deleted - child products exists for category.");
                throw new ChildProductsExistsException(id);
            }

            await logger.LogNoneAsync("DataStructureService.DeleteCategoryAsync: deleting category.");
            await categoryDataSource.DeleteAsync(category);
        }
        
        public async Task<CategoryModelCollection> ListCategoriesByBranchIdAsync(Guid branchId)
        {
            Guard.ValidateIdentifier(branchId, nameof(branchId));

            await logger.LogNoneAsync($"DataStructureService.ListCategoriesByBranchIdAsync: check if branch exist ({branchId}).");
            await FindBranchAsync(branchId);
            
            await logger.LogNoneAsync($"DataStructureService.ListCategoriesByBranchIdAsync: getting categories by branch id ({branchId}).");
            return await categoryDataSource.ListByBranchIdAsync(branchId);
        }

        public async Task<CategoryModelCollection> ListAllCategoriesAsync()
        {
            await logger.LogNoneAsync("DataStructureService.ListAllCategoriesAsync: listing all categories.");
            return await categoryDataSource.ListAllAsync();
        }

        public async Task<GroupModelCollection> ListGroupsAsync(int pageIndex, int pageSize)
        {
            await logger.LogNoneAsync($"DataStructureService.ListGroupAsync: listing groups (pageIndex: {pageIndex} pageSize: {pageSize}).");
            return await groupDataSource.ListAsync(pageIndex, pageSize);
        }

        public async Task<GroupModel> CreateGroupAsync(GroupModel group)
        {
            Guard.NotNullArgument(group, nameof(group));

            await logger.LogNoneAsync($"DataStructureService.CreateGroupAsync: trying to get category {group.CategoryId}.");
            var category = await categoryDataSource.FindAsync(group.CategoryId);
            if (category == null)
            {
                await logger.LogInformationAsync($"DataStructureService.CreateGroupAsync: category not found - {group.CategoryId}.");
                throw new CategoryNotFoundException(group.CategoryId);
            }

            await logger.LogNoneAsync($"DataStructureService.CreateGroupAsync: trying to get inventory number for group for category {category.CategoryId}.");
            var inventoryPartValue = await groupDataSource.GetNewInventoryNumberAsync(category.CategoryId);
            if (!inventoryPartValue.HasValue)
            {
                await logger.LogNoneAsync($"DataStructureService.CreateGroupAsync: there are no empty inventory numbers for category {category.CategoryId}.");
                throw new NoCategoryGroupsSlotsException(category.CategoryId);
            }

            group.InventoryPart = inventoryPartValue.Value;

            await logger.LogNoneAsync("DataStructureService.CreateGroupAsync: creating new group.");
            return await groupDataSource.CreateAsync(group);
        }

        public async Task<GroupModel> FindGroupAsync(Guid id)
        {
            Guard.ValidateIdentifier(id, nameof(id));

            await logger.LogNoneAsync($"DataStructureService.FindGroupAsync: trying to get group by id ({id}).");
            var group = await groupDataSource.FindAsync(id);
            if (group == null)
            {
                await logger.LogInformationAsync($"DataStructureService.FindGroupAsync: group not found - {id}.");
                throw new GroupNotFoundException(id);
            }

            return group;
        }

        public async Task<GroupModel> UpdateGroupAsync(GroupModel group)
        {
            Guard.ValidateIdentifier(group.GroupId, nameof(group.GroupId));

            await logger.LogNoneAsync($"DataStructureService.UpdateGroupAsync: trying to get group by id ({group.GroupId}).");
            var dbGroup = await groupDataSource.FindAsync(group.GroupId);
            if (dbGroup == null)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateGroupAsync: group not found - {group.GroupId}.");
                throw new GroupNotFoundException(group.GroupId);
            }

            await logger.LogNoneAsync($"DataStructureService.UpdateGroupAsync: trying to get category by id ({group.CategoryId}).");
            var dbCategory = await categoryDataSource.FindAsync(group.CategoryId);
            if (dbCategory == null)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateGroupAsync: category not found - {group.CategoryId}.");
                throw new CategoryNotFoundException(group.CategoryId);
            }

            if (group.InventoryPart != dbGroup.InventoryPart)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateGroupAsync: group inventory part changed - {group.InventoryPart} / {dbGroup.InventoryPart}.");
                throw new GroupInventoryPartChangedException(group.GroupId);
            }

            await logger.LogNoneAsync("DataStructureService.UpdateGroupAsync: updating group.");
            return await groupDataSource.UpdateAsync(group);
        }

        public async Task DeleteGroupAsync(Guid id)
        {
            Guard.ValidateIdentifier(id, nameof(id));

            await logger.LogNoneAsync($"DataStructureService.DeleteGroupAsync: trying to get group by id ({id}).");
            var group = await groupDataSource.FindAsync(id);
            if (group == null)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteGroupAsync: group not found - {id}.");
                throw new GroupNotFoundException(id);
            }

            await logger.LogNoneAsync($"DataStructureService.DeleteGroupAsync: check if child products exists for group {id}.");
            var productsExists = await productDataSource.IsExistsForGroupAsync(id);
            if (productsExists)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteGroupAsync: group '{id}' could not be deleted - child products exists for group.");
                throw new ChildProductsExistsException(id);
            }

            await logger.LogNoneAsync("DataStructureService.DeleteGroupAsync: deleting group.");
            await groupDataSource.DeleteAsync(group);
        }
        
        public async Task<GroupModelCollection> ListGroupsByCategoryIdAsync(Guid categoryId)
        {
            Guard.ValidateIdentifier(categoryId, nameof(categoryId));

            await logger.LogNoneAsync($"DataStructureService.ListGroupsByCategoryIdAsync: check if category exist ({categoryId}).");
            await FindCategoryAsync(categoryId);

            await logger.LogNoneAsync($"DataStructureService.ListGroupsByCategoryIdAsync: getting groups by category id ({categoryId}).");
            return await groupDataSource.ListByCategoryIdAsync(categoryId);
        }
    }
}
