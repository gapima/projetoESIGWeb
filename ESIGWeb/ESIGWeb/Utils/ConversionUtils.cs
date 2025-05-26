using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESIGWeb.Utils
{
    public static class ConversionUtils
    {
        public static int ToIntSafe(string value, int defaultValue = 0)
        {
            return int.TryParse(value, out var result) ? result : defaultValue;
        }

        public static DateTime? ToDateTimeSafe(string value)
        {
            return DateTime.TryParse(value, out var result) ? result : (DateTime?)null;
        }

        public static decimal ToDecimalSafe(string value)
        {
            if (decimal.TryParse(value, out decimal d)) return d;
            return 0;
        }
    }
}
