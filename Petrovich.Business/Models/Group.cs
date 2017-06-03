using Petrovich.Business.Models.Base;
using System;

namespace Petrovich.Business.Models
{
    public class Group : BaseEntity
    {
        public Guid GroupId { get; set; }
        public string Title { get; set; }
    }
}
