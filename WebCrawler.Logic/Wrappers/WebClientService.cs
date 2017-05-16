using System.Net;

namespace WebCrawler.Logic.Services
{
    public class WebClientService : IWebClientService
    {
        private readonly WebClient _webClient;

        public WebClientService()
        {
            _webClient = new WebClient();
        }

        public string DownloadString(string url)
        {
            try
            {
                return _webClient.DownloadString(url);
            }
            catch (WebException)
            {
                return string.Empty;
            }
        }
    }
}
