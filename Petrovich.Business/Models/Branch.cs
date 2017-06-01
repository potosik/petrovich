using Petrovich.Business.Models.Base;

namespace Petrovich.Business.Models
{
    public class Branch : BaseEntity
    {
        public int BranchId { get; set; }
        public string Title { get; set; }
        public string InventoryPart { get; set; }

        //public CategoryCollection Categories { get; set; }
    }
}
