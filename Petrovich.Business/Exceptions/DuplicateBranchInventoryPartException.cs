using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class DuplicateBranchInventoryPartException : BusinessException
    {
        public DuplicateBranchInventoryPartException(string inventoryPart, Guid existingBranchId)
            : base(FormatErrorMessage(ErrorCode.DublicateBranchInventoryPart, inventoryPart, existingBranchId))
        {
        }
    }
}
