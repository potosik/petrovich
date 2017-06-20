using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core.Extensions
{
    public static class StringExtensions
    {
        public static byte[] FromBase64String(this string str)
        {
            return String.IsNullOrWhiteSpace(str) ? null : Convert.FromBase64CharArray(str.ToArray(), 0, str.Length);
        }
    }
}
