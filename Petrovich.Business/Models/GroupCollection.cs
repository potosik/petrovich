using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class GroupCollection : List<Group>
    {
        public int TotalCount { get; set; }

        public GroupCollection()
        {
        }

        public GroupCollection(IEnumerable<Group> groups)
            : base(groups)
        {
        }
    }
}
