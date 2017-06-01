namespace Petrovich.Business.Exceptions
{
    public enum ErrorCode
    {
        Unknown = 0,
        DatabaseInternalError = 1,

        LogNotFound = 404001,
        UserNotFound = 404002,
    }

}
