using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business
{
    public interface IDataStructureService
    {
        Task<BranchModelCollection> ListBranchesAsync(int pageIndex, int pageSize);
        Task<BranchModel> CreateBranchAsync(BranchModel branch);
        Task<BranchModel> FindBranchAsync(Guid id);
        Task<BranchModel> UpdateBranchAsync(BranchModel branch);
        Task DeleteBranchAsync(Guid id);
        Task<BranchModelCollection> ListAllBranchesAsync();

        Task<CategoryModelCollection> ListCategoriesAsync(int pageIndex, int pageSize);
        Task<CategoryModel> CreateCategoryAsync(CategoryModel category);
        Task<CategoryModel> FindCategoryAsync(Guid id);
        Task<CategoryModel> UpdateCategoryAsync(CategoryModel category);
        Task DeleteCategoryAsync(Guid id);
        Task<CategoryModelCollection> ListCategoriesByBranchIdAsync(Guid branchId);
        Task<CategoryModelCollection> ListAllCategoriesAsync();

        Task<GroupModelCollection> ListGroupsAsync(int pageIndex, int pageSize);
        Task<GroupModel> CreateGroupAsync(GroupModel group);
        Task<GroupModel> FindGroupAsync(Guid id);
        Task<GroupModel> UpdateGroupAsync(GroupModel group);
        Task DeleteGroupAsync(Guid id);
        Task<GroupModelCollection> ListGroupsByCategoryIdAsync(Guid categoryId);
    }
}
