using Petrovich.Business.Models.Base;
using Petrovich.Business.Models.Enumerations;
using System;

namespace Petrovich.Business.Models
{
    public class GroupModel : BaseEntityModel, IPriceableEntityModel
    {
        public Guid GroupId { get; set; }
        public string Title { get; set; }

        public double? Price { get; set; }

        public int InventoryPart { get; set; }

        public string BranchInventoryPart { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int CategoryInventoryPart { get; set; }
    }
}
