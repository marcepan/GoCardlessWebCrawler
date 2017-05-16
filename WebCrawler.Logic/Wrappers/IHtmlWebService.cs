using HtmlAgilityPack;

namespace WebCrawler.Logic.Services
{
    public interface IHtmlWebService
    {
        HtmlDocument Load(string url);
    }
}
