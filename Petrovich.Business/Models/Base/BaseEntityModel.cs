using System;

namespace Petrovich.Business.Models.Base
{
    public class BaseEntityModel : IChangeTrackableEntityModel
    {
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
