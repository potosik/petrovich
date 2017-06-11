using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? ToLocalTimeIfHasValue(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToLocalTime() : dateTime;
        }
    }
}
