using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.Manager
{
    public class BranchViewModel
    {
        public Guid BranchId { get; set; }
        public string Title { get; set; }
        public string InventoryNumbers { get; set; }

        public static BranchViewModel Create(BranchModel branch)
        {
            if (branch == null)
            {
                throw new ArgumentNullException(nameof(branch));
            }

            return new BranchViewModel()
            {
                BranchId = branch.BranchId,
                Title = branch.Title,
                InventoryNumbers = $"{branch.InventoryPart}*",
            };
        }
    }
}