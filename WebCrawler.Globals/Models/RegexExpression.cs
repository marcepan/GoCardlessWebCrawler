namespace WebCrawler.Globals.Models
{
    public static class RegexExpression
    {
        public const string CLEAR_URL = @"(?<=\/\/)www\.|^www.";
        public const string REMOVE_BOOKMARK = @"#.*$";
        public const string GET_BASE_URL = @"^.+?[^\/:](?=[?\/]|$)";
    }
}
