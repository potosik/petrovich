using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Core.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToDoubleValueString(this double value)
        {
            return value.ToString(new NumberFormatInfo() { NumberDecimalSeparator = "." });
        }
    }
}
 