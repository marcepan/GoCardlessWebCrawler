using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebCrawler.Globals.Models;
using WebCrawler.Logic.Configuration;
using WebCrawler.Logic.Scrapers;

namespace WebCrawler.Logic.Tests
{
    [TestClass]
    public class CrawlerTests
    {
        private Crawler _crawler;
        private Mock<ISetupManager> _setupManagerMock;
        private Mock<IScraper> _scraperMock;

        private IDictionary<string, ResultModel> _results;
        private IEnumerable<AssetModel> _assetList;
        private IEnumerable<string> _blockedExtensions;
        private string _uri = "http://GoCardless.com/";
        private bool _usesRobotsTxt = false;

        [TestInitialize]
        public void Setup()
        {
            _assetList = new List<AssetModel>();
            _blockedExtensions = new List<string>();
            _results = new Dictionary<string, ResultModel>();

            _setupManagerMock = new Mock<ISetupManager>();
            _setupManagerMock.Setup(s => s.GetAssets()).Returns(_assetList);
            _setupManagerMock.Setup(s => s.GetBlockedExtensions()).Returns(_blockedExtensions);
            _setupManagerMock.Setup(s => s.GetDefaultUrl()).Returns(_uri);
            _setupManagerMock.Setup(s => s.UseRobotsTxt()).Returns(_usesRobotsTxt);

            _scraperMock = new Mock<IScraper>();
            _scraperMock.Setup(s => s.Proceed()).Returns(_results);

            _crawler = new Crawler(_setupManagerMock.Object, _scraperMock.Object);
        }

        [TestMethod]
        public void Run_AllData_ScraperInitialized()
        {
            var initialized = false;
            _scraperMock.Setup(s => s.Initialize(_uri, _assetList, _blockedExtensions, _usesRobotsTxt)).Callback(() => initialized = true);

            _crawler.Run();

            Assert.IsTrue(initialized);
        }

        [TestMethod]
        public void Run_AllData_ResultReturned()
        {
            _crawler.Run();

            Assert.AreEqual(_results, _crawler.Results);
        }
    }
}
