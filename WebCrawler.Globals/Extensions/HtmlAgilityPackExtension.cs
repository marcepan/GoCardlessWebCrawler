namespace WebCrawler.Globals.Extensions
{
    public static class HtmlAgilityPackExtension
    {
        public static HtmlAgilityPack.HtmlNodeCollection SafeSelectNodes(this HtmlAgilityPack.HtmlNode node, string selector)
        {
            return node.SelectNodes(selector) ?? new HtmlAgilityPack.HtmlNodeCollection(node);
        }
    }
}
