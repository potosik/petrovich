using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class BranchInventoryPartChangedException : BusinessException
    {
        public BranchInventoryPartChangedException(Guid id)
            : base(FormatErrorMessage(ErrorCode.BranchInventoryPartChanged, id))
        {
        }
    }
}
