using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models
{
    public interface IChangeTrackableViewModel
    {
        void MapChangeTrackingFields(IChangeTrackableEntity entity);

        DateTime? Created { get; set; }
        string CreatedBy { get; set; }
        DateTime? Modified { get; set; }
        string ModifiedBy { get; set; }
    }
}