using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;
using System.Web.Mvc;

namespace Petrovich.Web.Models.DataStructure
{
    public class GroupEditViewModel : ChangeTrackableViewModel
    {
        public GroupEditViewModel()
        {
            PriceTypes = new List<SelectListItem>();
        }

        public GroupEditViewModel(IChangeTrackableEntityModel entity)
            : base(entity)
        {
            PriceTypes = new List<SelectListItem>();
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public Guid GroupId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Базовая цена проката (BYN)")]
        public double? BasePrice { get; set; }

        [Display(Name = "Ценовой срок")]
        public int? PriceType { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Категория")]
        public Guid CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        public List<SelectListItem> PriceTypes { get; set; }

        public static GroupEditViewModel Create(GroupModel group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            return new GroupEditViewModel(group)
            {
                GroupId = group.GroupId,
                Title = group.Title,
                BasePrice = group.Price,
                PriceType = (int?)group.PriceType,
                CategoryId = group.CategoryId,

                CategoryTitle = group.CategoryTitle,
            };
        }
    }
}