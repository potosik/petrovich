using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class GroupModelCollection : List<GroupModel>
    {
        public int TotalCount { get; set; }

        public GroupModelCollection()
        {
        }

        public GroupModelCollection(IEnumerable<GroupModel> groups)
            : base(groups)
        {
        }
    }
}
