namespace Petrovich.Business.Exceptions
{
    public enum ErrorCode
    {
        Unknown = 0,
        DatabaseInternalError = 1,

        DublicateBranchInventoryPart = 400001,
        BranchInventoryPartChanged = 400002,
        DublicateCategoryInventoryPart = 400003,
        NoBranchCategoriesSlots = 400004,
        CategoryInventoryPartChanged = 400005,
        ChildCategoriesExists = 400006,

        LogNotFound = 404001,
        UserNotFound = 404002,
        BranchNotFound = 404003,
        CategoryNotFound = 404004,
    }

}
