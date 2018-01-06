using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core
{
    public static class Constants
    {
        public const int BranchInventoryPartLenght = 2;

        public const int CategoryInventoryPartMinValue = 1;
        public const int CategoryInventoryPartMaxValue = 99;
        public const int CategoryInventoryPartMaxCount = 99;
        public const string CategoryInventoryPartStringFormat = "D2";

        public const int GroupInventoryPartMinValue = 1;
        public const int GroupInventoryPartMaxValue = 99;
        public const int GroupInventoryPartMaxCount = 99;
        public const string GroupInventoryPartStringFormat = "D2";

        public const int ProductInventoryPartMinValue = 1;
        public const int ProductInventoryPartMaxValue = 999;
        public const int ProductInventoryPartMaxCount = 999;
        public const string ProductInventoryPartStringFormat = "D3";

        public const string PriceValueStringFormat = "N2";

        public static class Base64Images
        {
            public const int BigWidth = 1024;
            public const int BigHeight = 1024;
            public const int DefaultWidth = 256;
            public const int DefaultHeight = 256;
            public const int SmallWidth = 64;
            public const int SmallHeight = 64;
        }

        public static class Validation
        {
            public static int UnassignedId = 0;
            public static Guid UnassignedGuidId = Guid.Empty;
        }

        public static class Format
        {
            public const string DateFormat = "dd MMMM yyyy";
            public const string TimeFormat = "HH:mm";
            public const string DateTimeFormat = "dd MMMM yyyy HH:mm";
        }
    }
}
