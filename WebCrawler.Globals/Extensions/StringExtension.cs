using System;

namespace WebCrawler.Globals.Extensions
{
    public static class StringExtension
    {
        public static string GetLast(this string source, int tailLength)
        {
            return tailLength >= source.Length ? source : source.Substring(source.Length - tailLength);
        }

        public static bool ToBoolSafe(this string source, bool defValue = false)
        {
            try
            {
                return Convert.ToBoolean(source);
            }
            catch (Exception)
            {
                return defValue;
            }
        }
    }
}
