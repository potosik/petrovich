using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class BranchCollection : List<Branch>
    {
        public BranchCollection()
        {
        }

        public BranchCollection(IEnumerable<Branch> branches)
            : base(branches)
        {
        }
    }
}
