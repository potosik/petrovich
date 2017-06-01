using System;

namespace Petrovich.Business.Models.Base
{
    public interface IChangeTrackableEntity
    {
        DateTime? Created { get; set; }
        string CreatedBy { get; set; }
        DateTime? Modified { get; set; }
        string ModifiedBy { get; set; }
    }
}
