using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class DuplicateCategoryInventoryPartException : BusinessException
    {
        public DuplicateCategoryInventoryPartException(int inventoryPart, Guid branchId, Guid existingCategoryId)
            : base(FormatErrorMessage(ErrorCode.DublicateCategoryInventoryPart, inventoryPart, branchId, existingCategoryId))
        {
        }
    }
}
