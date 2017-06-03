using Petrovich.Business.Models.Base;
using System;

namespace Petrovich.Business.Models
{
    public class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public int InventoryPart { get; set; }

        //public int CategoryId { get; set; }
        //public Category Category { get; set; }

        //public int? GroupId { get; set; }
        //public Group Group { get; set; }
    }
}
