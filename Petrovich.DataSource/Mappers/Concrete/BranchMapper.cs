using System.Collections.Generic;
using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System.Linq;
using System;
using Petrovich.Core;

namespace Petrovich.DataSource.Mappers.Concrete
{
    public class BranchMapper : IBranchMapper
    {
        public BranchModel ToBranchModel(Branch branch)
        {
            if (branch == null)
            {
                return null;
            }

            return new BranchModel()
            {
                BranchId = branch.BranchId,
                Title = branch.Title,
                InventoryPart = branch.InventoryPart,
                
                Created = branch.Created,
                CreatedBy = branch.CreatedBy,
                Modified = branch.Modified,
                ModifiedBy = branch.ModifiedBy,
            };
        }

        public BranchModelCollection ToBranchModelCollection(IEnumerable<Branch> branches)
        {
            return new BranchModelCollection(branches.Select(item => ToBranchModel(item)));
        }

        public Branch ToContextBranch(BranchModel branchModel)
        {
            Guard.NotNullArgument(branchModel, nameof(branchModel));

            return new Branch()
            {
                BranchId = branchModel.BranchId,
                Title = branchModel.Title,
                InventoryPart = branchModel.InventoryPart,

                Created = branchModel.Created,
                CreatedBy = branchModel.CreatedBy,
                Modified = branchModel.Modified,
                ModifiedBy = branchModel.ModifiedBy,
            };
        }
    }
}
