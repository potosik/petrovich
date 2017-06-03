using Petrovich.Context.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petrovich.Context.Entities
{
    public class Product : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }

        public string Title { get; set; }

        [Range(1, 99999, ErrorMessageResourceName = "Product_InventoryPart_Range_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int InventoryPart { get; set; }

        [Index]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Index]
        public Guid? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
    }
}
