using Petrovich.Context.Entities.Base;
using Petrovich.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petrovich.Context.Entities
{
    public class Branch : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BranchId { get; set; }

        public string Title { get; set; }

        [StringLength(Constants.BranchInventoryPartLenght, MinimumLength = Constants.BranchInventoryPartLenght, ErrorMessageResourceName = "Branch_InventoryPart_StringLength_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string InventoryPart { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
