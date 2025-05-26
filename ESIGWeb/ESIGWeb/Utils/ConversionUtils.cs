

namespace ESIGWeb.Utils
{
    public static class ConversionUtils
    {
        public static int ToIntSafe(string value, int defaultValue = 0)
        {
            return int.TryParse(value, out var result) ? result : defaultValue;
        }
    }
}
