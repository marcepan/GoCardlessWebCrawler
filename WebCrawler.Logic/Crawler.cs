using System.Collections.Generic;
using WebCrawler.Globals.Models;
using WebCrawler.Logic.Configuration;
using WebCrawler.Logic.Scrapers;

namespace WebCrawler.Logic
{
    public class Crawler
    {
        public static string USER_AGENT = "WebCrawlerGoCardlessTest";

        private readonly ISetupManager _setupManager;
        private readonly IScraper _scraper;

        public string BaseUrl { get; set; }
        public bool UsesRobotsTxt { get; set; }
        public IEnumerable<AssetModel> AssetList { get; set; }
        public IEnumerable<string> BlockedExtensions { get; set; }
        public IDictionary<string, ResultModel> Results;
        
        public Crawler(ISetupManager setupManager, IScraper scraper)
        {
            _setupManager = setupManager;
            _scraper = scraper;
        }

        public Crawler() : this(new SetupManager(), new Scraper()) { }

        public void Run()
        {
            AssetList = _setupManager.GetAssets();
            BaseUrl = _setupManager.GetDefaultUrl();
            BlockedExtensions = _setupManager.GetBlockedExtensions();
            UsesRobotsTxt = _setupManager.UseRobotsTxt();

            _scraper.Initialize(BaseUrl, AssetList, BlockedExtensions, UsesRobotsTxt);
            Results = _scraper.Proceed();
        }
    }
}
