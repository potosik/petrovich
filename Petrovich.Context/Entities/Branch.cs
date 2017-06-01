using Petrovich.Context.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Petrovich.Context.Entities
{
    public class Branch : BaseEntity
    {
        [Key]
        public int BranchId { get; set; }

        public string Title { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessageResourceName = "Branch_InventoryPart_StringLength_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string InventoryPart { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
