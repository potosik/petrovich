﻿using Petrovich.Context.Entities.Base;
using Petrovich.Core;
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
        public string Description { get; set; }

        [Range(Constants.ProductInventoryPartMinValue, Constants.ProductInventoryPartMaxValue, ErrorMessageResourceName = "Product_InventoryPart_Range_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
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
