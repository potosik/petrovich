using Petrovich.Business.Models.Base;

namespace Petrovich.Business.Models
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public int InventoryPart { get; set; }

        //public int BranchId { get; set; }
        //public Branch Branch { get; set; }

        //public GroupCollection Groups { get; set; }
        //public ProductCollection Products { get; set; }
    }
}
