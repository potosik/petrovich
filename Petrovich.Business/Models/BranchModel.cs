using Petrovich.Business.Models.Base;
using System;

namespace Petrovich.Business.Models
{
    public class BranchModel : BaseEntityModel
    {
        public Guid BranchId { get; set; }
        public string Title { get; set; }
        public string InventoryPart { get; set; }
    }
}
