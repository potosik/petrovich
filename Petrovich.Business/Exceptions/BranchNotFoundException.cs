using System;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class BranchNotFoundException : BusinessException
    {
        public BranchNotFoundException(Guid id)
            : base(FormatErrorMessage(ErrorCode.BranchNotFound, id))
        {
        }
    }
}
