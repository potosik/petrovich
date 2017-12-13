namespace Petrovich.Business.Exceptions
{
    public enum ErrorCode
    {
        Unknown = 0,
        DatabaseInternalError = 1,

        DublicateBranchInventoryPart = 400001,
        BranchInventoryPartChanged = 400002,
        ProductInventoryPartChanged = 400003,
        NoBranchCategoriesSlots = 400004,
        CategoryInventoryPartChanged = 400005,
        ChildCategoriesExists = 400006,
        ChildGroupsExists = 400007,
        NoCategoryProductsSlots = 400008,
        ChildProductsExists = 400009,
        InvalidImageFormat = 400010,
        GroupInventoryPartChanged = 400011,
        NoCategoryGroupsSlots = 400012,
        NoGroupProductsSlots = 400013,

        LogNotFound = 404001,
        UserNotFound = 404002,
        BranchNotFound = 404003,
        CategoryNotFound = 404004,
        GroupNotFound = 404005,
        ProductNotFound = 404006,
        FullImageNotFound = 404007,
        ClientNotFound = 404008,
    }

}
