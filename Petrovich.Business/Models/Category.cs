using Petrovich.Business.Models.Base;
using Petrovich.Business.Models.Enumerations;
using System;

namespace Petrovich.Business.Models
{
    public class Category : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public int InventoryPart { get; set; }
        public double? BasePrice { get; set; }
        public PriceType? PriceType { get; set; }

        public Guid BranchId { get; set; }
        public string BranchTitle { get; set; }
        public string BranchInventoryPart { get; set; }
    }
}
