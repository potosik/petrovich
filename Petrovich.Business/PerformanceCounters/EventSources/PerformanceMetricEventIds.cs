using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.PerformanceCounters.EventSources
{
    internal static class PerformanceMetricEventIds
    {
        // branches #11
        public const int ListBranchesEventId = 11001;
        public const int FindBranchByInventoryPartEventId = 11002;
        public const int CreateBranchEventId = 11003;
        public const int FindBranchByIdEventId = 11004;
        public const int UpdateBranchEventId = 11005;
        public const int DeleteBranchEventId = 11006;
        public const int ListAllBranchesEventId = 11007;

        // categories #12
        public const int ListCategoriesEventId = 12001;
        public const int FindCategoryByInventoryPartEventId = 12002;
        public const int CreateCategoryEventId = 12003;
        public const int GetNewInventoryNumberForCategoryEventId = 12004;
        public const int FindCategoryByIdEventId = 12005;
        public const int UpdateCategoryEventId = 12006;
        public const int DeleteCategoryEventId = 12007;
        public const int IsExistsCategoriesForBranchEventId = 12008;
        public const int ListCategoriesByBranchIdEventId = 12009;
        public const int ListAllCategoriesEventId = 12010;

        // groups #13
        public const int ListGroupsEventId = 13001;
        public const int CreateGroupEventId = 13002;
        public const int FindGroupByIdEventId = 13003;
        public const int UpdateGroupEventId = 13004;
        public const int DeleteGroupEventId = 13005;
        public const int IsExistsGroupsForCategoryEventId = 13006;
        public const int ListGroupsByCategoryIdEventId = 13007;
        public const int GetNewInventoryNumberForGroupEventId = 13008;

        // products #14
        public const int ListProductsEventId = 14001;
        public const int CreateProductEventId = 14002;
        public const int FindProductByIdEventId = 14003;
        public const int UpdateProductEventId = 14004;
        public const int DeleteProductEventId = 14005;
        public const int IsExistsProductsForCategoryEventId = 14006;
        public const int IsExistsProductsForGroupEventId = 14007;
        public const int GetNewInventoryNumberForProductByCategoryEventId = 14008;
        public const int ProductSearchFastEventId = 14009;
        public const int ListProductsByCategoryIdEventId = 14010;
        public const int ListProductsByGroupIdEventId = 14011;
        public const int ListProductsByIdsEventId = 14012;
        public const int GetNewInventoryNumberForProductByGroupEventId = 14013;

        // fullimage #15
        public const int CreateFullImageEventId = 15001;
        public const int UpdateOrCreateFullImageEventId = 15002;
        public const int FindFullImageEventId = 15003;
    }
}
