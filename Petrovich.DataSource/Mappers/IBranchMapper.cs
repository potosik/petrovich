using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System.Collections.Generic;

namespace Petrovich.DataSource.Mappers
{
    public interface IBranchMapper
    {
        BranchModel ToBranchModel(Branch branch);
        BranchModelCollection ToBranchModelCollection(IEnumerable<Branch> branches);
        Branch ToContextBranch(BranchModel branchModel);
    }
}
