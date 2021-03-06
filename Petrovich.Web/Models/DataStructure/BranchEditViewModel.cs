﻿using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;
using Petrovich.Core;

namespace Petrovich.Web.Models.DataStructure
{
    public class BranchEditViewModel : ChangeTrackableViewModel
    {
        public BranchEditViewModel()
        {
        }

        public BranchEditViewModel(IChangeTrackableEntityModel entity)
            : base(entity)
        {
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public Guid BranchId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
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
        
        private string _inventoryPart;

        public static BranchEditViewModel Create(BranchModel branch)
        {
            Guard.NotNullArgument(branch, nameof(branch));

            return new BranchEditViewModel(branch)
            {
                BranchId = branch.BranchId,
                Title = branch.Title,
                InventoryPart = branch.InventoryPart,
            };
        }
    }
}