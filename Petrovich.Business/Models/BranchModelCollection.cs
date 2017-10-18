using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class BranchModelCollection : List<BranchModel>
    {
        public int TotalCount { get; set; }

        public BranchModelCollection()
        {
        }

        public BranchModelCollection(IEnumerable<BranchModel> branches)
            : base(branches)
        {
        }
    }
}
