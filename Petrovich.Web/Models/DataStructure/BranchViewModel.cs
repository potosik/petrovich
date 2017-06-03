using Petrovich.Business.Models;
using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.DataStructure
{
    public class BranchViewModel : IChangeTrackableEntity
    {
        public Guid BranchId { get; set; }
        public string Title { get; set; }
        public string InventoryPart { get; set; }

        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }

        public static BranchViewModel Create(Branch branch)
        {
            if (branch == null)
            {
                throw new ArgumentNullException(nameof(branch));
            }

            return new BranchViewModel()
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
    }
}