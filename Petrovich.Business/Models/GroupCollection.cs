using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class GroupCollection : List<Group>
    {
        public GroupCollection()
        {
        }

        public GroupCollection(IEnumerable<Group> groups)
            : base(groups)
        {
        }
    }
}
