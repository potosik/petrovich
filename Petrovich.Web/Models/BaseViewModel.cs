using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models.Base;
using Petrovich.Core.Extensions;

namespace Petrovich.Web.Models
{
    public class BaseViewModel : IChangeTrackableViewModel
    {
        public BaseViewModel()
        {
        }

        public BaseViewModel(IChangeTrackableEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            MapChangeTrackingFields(entity);
        }

        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }

        public void MapChangeTrackingFields(IChangeTrackableEntity entity)
        {
            Created = entity.Created.ToLocalTimeIfHasValue();
            CreatedBy = entity.CreatedBy;
            Modified = entity.Modified.ToLocalTimeIfHasValue();
            ModifiedBy = entity.ModifiedBy;
        }
    }
}