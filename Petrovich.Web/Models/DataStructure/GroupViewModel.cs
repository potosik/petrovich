﻿using Petrovich.Business.Models;
using Petrovich.Business.Models.Base;
using Petrovich.Web.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.DataStructure
{
    public class GroupViewModel : ChangeTrackableViewModel
    {
        public GroupViewModel()
        {
        }

        public GroupViewModel(IChangeTrackableEntityModel entity)
            : base(entity)
        {
        }

        public Guid GroupId { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string CategoryTitle { get; set; }

        public static GroupViewModel Create(GroupModel group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            return new GroupViewModel(group)
            {
                GroupId = group.GroupId,
                Title = group.Title,
                Price = group.GetPriceInformation(),
                CategoryTitle = group.CategoryTitle,
            };
        }
    }
}