using System;
using System.Collections.Generic;
using System.Linq;

namespace Petrovich.Core.Utils
{
    public static class EnumUtils
    {
        public static IEnumerable<TEnumType> GetValues<TEnumType>()
        {
            return Enum.GetValues(typeof(TEnumType)).Cast<TEnumType>();
        }

        public static IEnumerable<string> GetValuesStrings<TEnumType>()
        {
            return Enum.GetValues(typeof(TEnumType)).Cast<TEnumType>().Select(item => item.ToString());
        }
    }
}
