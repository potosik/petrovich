using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models.Base;
using Petrovich.Core.Extensions;
using Petrovich.Core;
using System.ComponentModel.DataAnnotations;

namespace Petrovich.Web.Models
{
    public class ChangeTrackableViewModel : IChangeTrackableViewModel
    {
        public ChangeTrackableViewModel()
        {
        }

        public ChangeTrackableViewModel(IChangeTrackableEntityModel entity)
        {
            Guard.NotNullArgument(entity, nameof(entity));

            MapChangeTrackingFields(entity);
        }
        
        [DisplayFormat(DataFormatString = "{0:" + Constants.Format.DateTimeFormat + "}")]
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:" + Constants.Format.DateTimeFormat + "}")]
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }

        public void MapChangeTrackingFields(IChangeTrackableEntityModel entity)
        {
            Created = entity.Created.ToLocalTimeIfHasValue();
            CreatedBy = entity.CreatedBy;
            Modified = entity.Modified.ToLocalTimeIfHasValue();
            ModifiedBy = entity.ModifiedBy;
        }
    }
}