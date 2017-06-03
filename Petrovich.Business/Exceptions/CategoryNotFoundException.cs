using System;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class CategoryNotFoundException : BusinessException
    {
        public CategoryNotFoundException(Guid id)
            : base(FormatErrorMessage(ErrorCode.CategoryNotFound, id))
        {
        }
    }
}
