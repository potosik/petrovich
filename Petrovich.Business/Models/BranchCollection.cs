using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class BranchCollection : List<Branch>
    {
        public int TotalCount { get; set; }

        public BranchCollection()
        {
        }

        public BranchCollection(IEnumerable<Branch> branches)
            : base(branches)
        {
        }
    }
}
