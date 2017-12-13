using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;
using System.Web.Mvc;
using Petrovich.Core;

namespace Petrovich.Web.Models.DataStructure
{
    public class GroupEditViewModel : ChangeTrackableViewModel
    {
        public GroupEditViewModel()
        {
        }

        public GroupEditViewModel(IChangeTrackableEntityModel entity)
            : base(entity)
        {
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public Guid GroupId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Базовая цена проката (BYN)")]
        public double? BasePrice { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Часть инвентарного номера")]
        public int InventoryPart { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Категория")]
        public Guid CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        public static GroupEditViewModel Create(GroupModel group)
        {
            Guard.NotNullArgument(group, nameof(group));

            return new GroupEditViewModel(group)
            {
                GroupId = group.GroupId,
                Title = group.Title,
                BasePrice = group.Price,
                InventoryPart = group.InventoryPart,
                CategoryId = group.CategoryId,

                CategoryTitle = group.CategoryTitle,
            };
        }
    }
}