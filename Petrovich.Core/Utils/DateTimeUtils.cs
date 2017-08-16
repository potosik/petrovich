using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core.Utils
{
    public static class DateTimeUtils
    {
        private const string NoPurchaseDateValue = "-";
        private const string PurchaseDateFormat = "MMMM yyyy";

        public static string CreatePurchaseDate(int? year, int? month)
        {
            if (!year.HasValue || !month.HasValue)
            {
                return NoPurchaseDateValue;
            }

            var dateTime = new DateTime(year.Value, month.Value, 1);
            return dateTime.ToString(PurchaseDateFormat);
        }
    }
}
