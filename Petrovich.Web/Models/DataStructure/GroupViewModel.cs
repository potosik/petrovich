using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.DataStructure
{
    public class GroupViewModel
    {
        public Guid GroupId { get; set; }
        public string Title { get; set; }
        public string CategoryTitle { get; set; }

        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }

        public static GroupViewModel Create(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            return new GroupViewModel()
            {
                GroupId = group.GroupId,
                Title = group.Title,
                CategoryTitle = group.CategoryTitle,

                Created = group.Created,
                CreatedBy = group.CreatedBy,
                Modified = group.Modified,
                ModifiedBy = group.ModifiedBy,
            };
        }
    }
}