using System;

namespace Petrovich.Context.Entities.Base
{
    public interface IChangeTrackableEntity
    {
        DateTime? Created { get; set; }
        string CreatedBy { get; set; }
        DateTime? Modified { get; set; }
        string ModifiedBy { get; set; }
    }
}
