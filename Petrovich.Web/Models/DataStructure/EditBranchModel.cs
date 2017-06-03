using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.DataStructure
{
    public class EditBranchModel
    {
        public Guid BranchId { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Часть инвентарного номера")]
        [StringLength(2, MinimumLength = 2, ErrorMessageResourceName = "Branch_InventoryPart_StringLength_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string InventoryPart
        {
            get
            {
                return _inventoryPart;
            }
            set
            {
                _inventoryPart = value.ToUpperInvariant();
            }
        }

        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }

        private string _inventoryPart;
    }
}