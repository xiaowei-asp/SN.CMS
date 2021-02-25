using System.Linq;

namespace SN.CMS.Common
{
    public static class StringExtension
    {
        public static string Underscore(this string value)
           => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value);
        }
    }
}
