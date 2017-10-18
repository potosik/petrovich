using Petrovich.Business.Models.Base;
using Petrovich.Business.Models.Enumerations;
using System;

namespace Petrovich.Business.Models
{
    public class Group : BaseEntity, IPriceableEntity
    {
        public Guid GroupId { get; set; }
        public string Title { get; set; }

        public double? Price { get; set; }
        public PriceType? PriceType { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
    }
}
