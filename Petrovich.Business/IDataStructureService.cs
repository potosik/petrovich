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
    }
}
