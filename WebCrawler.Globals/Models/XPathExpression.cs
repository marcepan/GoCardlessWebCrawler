namespace WebCrawler.Globals.Models
{
    public static class XPathExpression
    {
        public const string IMG_ASSET = "//img[@src]";
        public const string SCRIPT_ASSET = "//script[@src]";
        public const string CSS_ASSET = "//link[substring(@href,string-length(@href) -string-length('.css') +1) = '.css']";

        public const string A_ASSET = "//a[@href and not(contains(@href, 'mailto')) and not(starts-with(@href, '#'))]";
    }
}
