using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class NoBranchCategoriesSlotsException : BusinessException
    {
        public NoBranchCategoriesSlotsException(Guid id)
            : base(FormatErrorMessage(ErrorCode.NoBranchCategoriesSlots, id))
        {
        }
    }
}
