using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class CategoryInventoryPartChangedException : BusinessException
    {
        public CategoryInventoryPartChangedException(Guid id)
            : base(FormatErrorMessage(ErrorCode.CategoryInventoryPartChanged, id))
        {
        }
    }
}
