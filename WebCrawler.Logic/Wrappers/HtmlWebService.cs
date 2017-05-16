using System.Net;
using HtmlAgilityPack;

namespace WebCrawler.Logic.Services
{
    public class HtmlWebService : IHtmlWebService
    {
        private readonly HtmlWeb _htmlWeb;

        public HtmlWebService()
        {
            _htmlWeb = new HtmlWeb();
        }

        public HtmlDocument Load(string url)
        {
            try
            {
                return _htmlWeb.Load(url);
            }
            catch (WebException)
            {
                return null;
            }
        }
    }
}
