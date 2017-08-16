using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;

namespace Petrovich.Web.Models.DataStructure
{
    public class GroupEditViewModel : ChangeTrackableViewModel
    {
        public GroupEditViewModel()
        {
        }

        public GroupEditViewModel(IChangeTrackableEntity entity)
            : base(entity)
        {
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public Guid GroupId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Категория")]
        public Guid CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        public static GroupEditViewModel Create(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            return new GroupEditViewModel(group)
            {
                GroupId = group.GroupId,
                Title = group.Title,
                CategoryId = group.CategoryId,

                CategoryTitle = group.CategoryTitle,
            };
        }
    }
}