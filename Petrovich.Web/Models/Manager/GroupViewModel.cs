using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;

namespace Petrovich.Web.Models.Manager
{
    public class GroupViewModel
    {
        public Guid GroupId { get; set; }
        public string Title { get; set; }

        public static GroupViewModel Create(Group item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return new GroupViewModel()
            {
                GroupId = item.GroupId,
                Title = item.Title,
            };
        }
    }
}