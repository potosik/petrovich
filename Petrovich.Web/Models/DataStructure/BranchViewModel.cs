using Petrovich.Business.Models;
using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.DataStructure
{
    public class BranchViewModel : BaseViewModel
    {
        public BranchViewModel()
        {
        }

        public BranchViewModel(IChangeTrackableEntity entity)
            : base(entity)
        {
        }

        public Guid BranchId { get; set; }
        public string Title { get; set; }
        public string InventoryPart { get; set; }

        public static BranchViewModel Create(Branch branch)
        {
            if (branch == null)
            {
                throw new ArgumentNullException(nameof(branch));
            }

            return new BranchViewModel(branch)
            {
                BranchId = branch.BranchId,
                Title = branch.Title,
                InventoryPart = branch.InventoryPart,
            };
        }
    }
}