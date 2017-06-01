using Petrovich.Context.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petrovich.Context.Entities
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }

        public string Title { get; set; }

        [Range(1, 99, ErrorMessageResourceName = "Category_InventoryPart_Range_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int InventoryPart { get; set; }

        [Index]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
