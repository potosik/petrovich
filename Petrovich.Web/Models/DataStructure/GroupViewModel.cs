using Petrovich.Business.Models;
using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.DataStructure
{
    public class GroupViewModel : BaseViewModel
    {
        public GroupViewModel()
        {
        }

        public GroupViewModel(IChangeTrackableEntity entity)
            : base(entity)
        {
        }

        public Guid GroupId { get; set; }
        public string Title { get; set; }
        public string CategoryTitle { get; set; }

        public static GroupViewModel Create(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            return new GroupViewModel(group)
            {
                GroupId = group.GroupId,
                Title = group.Title,
                CategoryTitle = group.CategoryTitle,
            };
        }
    }
}