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
            await logger.LogNoneAsync("DataStructureService.ListBranchesAsync: listing all branches.");
            return await branchDataSource.ListAsync();
        }

        public async Task<Branch> CreateBranchAsync(Branch branch)
        {
            if (branch == null)
            {
                await logger.LogInformationAsync("DataStructureService.CreateBranchAsync: branch parameter is null.");
                throw new ArgumentNullException(nameof(branch));
            }

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

        public async Task<Branch> FindBranchAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.FindBranchAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"DataStructureService.FindBranchAsync: trying to get branch by id ({id}).");
            var branch = await branchDataSource.FindAsync(id);
            if (branch == null)
            {
                await logger.LogInformationAsync($"DataStructureService.FindBranchAsync: branch not found - {id}.");
                throw new BranchNotFoundException(id);
            }

            return branch;
        }

        public async Task<Branch> UpdateBranchAsync(Branch branch)
        {
            if (branch.BranchId == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateBranchAsync: branchId is {branch.BranchId}.");
                throw new ArgumentOutOfRangeException(nameof(branch.BranchId));
            }

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
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteBranchAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"DataStructureService.DeleteBranchAsync: trying to get branch by id ({id}).");
            var branch = await branchDataSource.FindAsync(id);
            if (branch == null)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteBranchAsync: branch not found - {id}.");
                throw new BranchNotFoundException(id);
            }

            await logger.LogNoneAsync($"DataStructureService.DeleteBranchAsync: check if child categories exiests for branch {id}.");
            var categoriesExists = await categoryDataSource.IsExistsForBranchAsync(id);
            if (categoriesExists)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteBranchAsync: branch '{id}' could not be deleted - child categories exiests for branch.");
                throw new ChildCategoriesExistsException(id);
            }

            await logger.LogNoneAsync("DataStructureService.DeleteBranchAsync: deleting branch.");
            await branchDataSource.DeleteAsync(branch);
        }

        public async Task<CategoryCollection> ListCategoriesAsync()
        {
            await logger.LogNoneAsync("DataStructureService.ListCategoriesAsync: listing all categories.");
            return await categoryDataSource.ListAsync();
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            if (category == null)
            {
                await logger.LogInformationAsync("DataStructureService.CreateCategoryAsync: category parameter is null.");
                throw new ArgumentNullException(nameof(category));
            }

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

        public async Task<Category> FindCategoryAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.FindCategoryAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"DataStructureService.FindCategoryAsync: trying to get category by id ({id}).");
            var category = await categoryDataSource.FindAsync(id);
            if (category == null)
            {
                await logger.LogInformationAsync($"DataStructureService.FindCategoryAsync: category not found - {id}.");
                throw new CategoryNotFoundException(id);
            }

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            if (category.CategoryId == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateCategoryAsync: categoryId is {category.CategoryId}.");
                throw new ArgumentOutOfRangeException(nameof(category.CategoryId));
            }

            await logger.LogNoneAsync($"DataStructureService.UpdateCategoryAsync: trying to get branch by id ({category.BranchId}).");
            var dbBranch = await branchDataSource.FindAsync(category.BranchId);
            if (dbBranch == null)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateCategoryAsync: branch not found - {category.BranchId}.");
                throw new BranchNotFoundException(category.BranchId);
            }

            await logger.LogNoneAsync($"DataStructureService.UpdateCategoryAsync: trying to get category by id ({category.CategoryId}).");
            var dbCategory = await categoryDataSource.FindAsync(category.CategoryId);
            if (dbCategory == null)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateCategoryAsync: category not found - {category.CategoryId}.");
                throw new CategoryNotFoundException(category.CategoryId);
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
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteCategoryAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"DataStructureService.DeleteCategoryAsync: trying to get category by id ({id}).");
            var category = await categoryDataSource.FindAsync(id);
            if (category == null)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteCategoryAsync: category not found - {id}.");
                throw new CategoryNotFoundException(id);
            }
            
            await logger.LogNoneAsync($"DataStructureService.DeleteCategoryAsync: check if child groups exiests for category {id}.");
            var groupsExists = await groupDataSource.IsExistsForCategoryAsync(id);
            if (groupsExists)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteCategoryAsync: category '{id}' could not be deleted - child groups exiests for category.");
                throw new ChildGroupsExistsException(id);
            }

            await logger.LogNoneAsync("DataStructureService.DeleteCategoryAsync: deleting category.");
            await categoryDataSource.DeleteAsync(category);
        }

        public async Task<CategoryCollection> ListCategoriesByBranchIdAsync(Guid branchId)
        {
            if (branchId == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.ListCategoriesByBranchIdAsync: id parameter is {branchId}.");
                throw new ArgumentOutOfRangeException(nameof(branchId));
            }
            
            await logger.LogNoneAsync($"DataStructureService.ListCategoriesByBranchIdAsync: check if branch exist ({branchId}).");
            await FindBranchAsync(branchId);
            
            await logger.LogNoneAsync($"DataStructureService.ListCategoriesByBranchIdAsync: getting categories by branch id ({branchId}).");
            return await categoryDataSource.ListByBranchIdAsync(branchId);
        }

        public async Task<GroupCollection> ListGroupsAsync()
        {
            await logger.LogNoneAsync("DataStructureService.ListGroupAsync: listing all groups.");
            return await groupDataSource.ListAsync();
        }

        public async Task<Group> CreateGroupAsync(Group group)
        {
            if (group == null)
            {
                await logger.LogInformationAsync("DataStructureService.CreateGroupAsync: group parameter is null.");
                throw new ArgumentNullException(nameof(group));
            }

            await logger.LogNoneAsync($"DataStructureService.CreateGroupAsync: trying to get category {group.CategoryId}.");
            var category = await categoryDataSource.FindAsync(group.CategoryId);
            if (category == null)
            {
                await logger.LogInformationAsync($"DataStructureService.CreateGroupAsync: category not found - {group.CategoryId}.");
                throw new CategoryNotFoundException(group.CategoryId);
            }

            await logger.LogNoneAsync("DataStructureService.CreateGroupAsync: creating new group.");
            return await groupDataSource.CreateAsync(group);
        }

        public async Task<Group> FindGroupAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.FindGroupAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"DataStructureService.FindGroupAsync: trying to get group by id ({id}).");
            var group = await groupDataSource.FindAsync(id);
            if (group == null)
            {
                await logger.LogInformationAsync($"DataStructureService.FindGroupAsync: group not found - {id}.");
                throw new GroupNotFoundException(id);
            }

            return group;
        }

        public async Task<Group> UpdateGroupAsync(Group group)
        {
            if (group.GroupId == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.UpdateGroupAsync: groupId is {group.GroupId}.");
                throw new ArgumentOutOfRangeException(nameof(group.GroupId));
            }

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

            await logger.LogNoneAsync("DataStructureService.UpdateGroupAsync: updating group.");
            return await groupDataSource.UpdateAsync(group);
        }

        public async Task DeleteGroupAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteGroupAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"DataStructureService.DeleteGroupAsync: trying to get group by id ({id}).");
            var group = await groupDataSource.FindAsync(id);
            if (group == null)
            {
                await logger.LogInformationAsync($"DataStructureService.DeleteGroupAsync: group not found - {id}.");
                throw new GroupNotFoundException(id);
            }
            
            await logger.LogNoneAsync("DataStructureService.DeleteGroupAsync: deleting group.");
            await groupDataSource.DeleteAsync(group);
        }

        public async Task<GroupCollection> ListGroupsByCategoryIdAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                await logger.LogInformationAsync($"DataStructureService.ListGroupsByCategoryIdAsync: categoryId parameter is {categoryId}.");
                throw new ArgumentOutOfRangeException(nameof(categoryId));
            }
            
            await logger.LogNoneAsync($"DataStructureService.ListGroupsByCategoryIdAsync: check if category exist ({categoryId}).");
            await FindCategoryAsync(categoryId);

            await logger.LogNoneAsync($"DataStructureService.ListGroupsByCategoryIdAsync: getting groups by category id ({categoryId}).");
            return await groupDataSource.ListByCategoryIdAsync(categoryId);
        }
    }
}
