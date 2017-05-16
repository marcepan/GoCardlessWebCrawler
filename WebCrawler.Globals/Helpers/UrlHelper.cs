using System;
using System.Text.RegularExpressions;
using WebCrawler.Globals.Models;

namespace WebCrawler.Globals.Helpers
{
    public static class UrlHelper
    {
        public static Uri CreatUri(string url)
        {
            return new Uri(new UriBuilder(url).Uri.ToString());
        }

        public static string GetDomain(Uri uri)
        {
            return Regex.Replace(uri.Host, RegexExpression.CLEAR_URL, string.Empty);
        }

        public static string RemoveBookmarkAndClear(string url)
        {
            var result = Regex.Replace(url, RegexExpression.REMOVE_BOOKMARK, string.Empty);
            result = Regex.Replace(result, RegexExpression.CLEAR_URL, string.Empty);
            return result;
        }
    }
}
