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
        public string InventoryNumbers { get; set; }

        public static GroupViewModel Create(GroupModel group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            return new GroupViewModel()
            {
                GroupId = group.GroupId,
                Title = group.Title,
                InventoryNumbers = $"{group.BranchInventoryPart}{group.CategoryInventoryPart.ToString("D2")}{group.InventoryPart.ToString("D2")}*",
            };
        }
    }
}