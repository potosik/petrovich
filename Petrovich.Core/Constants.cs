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

        public const int ProductInventoryPartMinValue = 1;
        public const int ProductInventoryPartMaxValue = 99999;
        public const int ProductInventoryPartMaxCount = 99999;
        public const string ProductInventoryPartStringFormat = "D5";

        public static class Base64Images
        {
            public const int BigWidth = 1024;
            public const int BigHeight = 1024;
            public const int DefaultWidth = 256;
            public const int DefaultHeight = 256;
            public const int SmallWidth = 64;
            public const int SmallHeight = 64;
        }
    }
}
