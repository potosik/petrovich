using Petrovich.Context.Entities.Base;
using Petrovich.Context.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petrovich.Context.Entities
{
    public class Group : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GroupId { get; set; }

        public string Title { get; set; }

        public double? BasePrice { get; set; }
        public PriceType? PriceType { get; set; }

        [Index]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
