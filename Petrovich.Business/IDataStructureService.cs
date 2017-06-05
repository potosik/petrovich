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
        Task<BranchCollection> ListBranchesAsync();
        Task<Branch> CreateBranchAsync(Branch branch);
        Task<Branch> FindBranchAsync(Guid id);
        Task<Branch> UpdateBranchAsync(Branch branch);
        Task DeleteBranchAsync(Guid id);

        Task<CategoryCollection> ListCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> FindCategoryAsync(Guid id);
        Task<Category> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid id);
        Task<CategoryCollection> ListCategoriesByBranchIdAsync(Guid branchId);

        Task<GroupCollection> ListGroupsAsync();
        Task<Group> CreateGroupAsync(Group group);
        Task<Group> FindGroupAsync(Guid id);
        Task<Group> UpdateGroupAsync(Group group);
        Task DeleteGroupAsync(Guid id);
        Task<GroupCollection> ListGroupsByCategoryIdAsync(Guid categoryId);
    }
}
