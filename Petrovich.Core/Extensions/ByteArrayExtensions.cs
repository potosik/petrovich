using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToBase64String(this byte[] array)
        {
            return array == null ? null : Convert.ToBase64String(array);
        }
    }
}
