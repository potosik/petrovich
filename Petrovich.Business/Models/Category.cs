using Petrovich.Business.Models.Base;
using System;

namespace Petrovich.Business.Models
{
    public class Category : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public int InventoryPart { get; set; }

        public Guid BranchId { get; set; }
        public string BranchTitle { get; set; }
        public string BranchInventoryPart { get; set; }
    }
}
