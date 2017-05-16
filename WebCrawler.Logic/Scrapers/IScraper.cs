using System.Collections.Generic;
using WebCrawler.Globals.Models;

namespace WebCrawler.Logic.Scrapers
{
    public interface IScraper
    {
        void Initialize(string url, IEnumerable<AssetModel> assets, IEnumerable<string> blacklistedExtensions, bool usesRobotsTxt = true);
        IDictionary<string, ResultModel> Proceed();
    }
}
