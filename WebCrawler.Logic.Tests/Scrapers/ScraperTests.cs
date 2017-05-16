using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RobotsTxt;
using WebCrawler.Globals.Models;
using WebCrawler.Logic.Configuration;
using WebCrawler.Logic.Processors;
using WebCrawler.Logic.Scrapers;
using WebCrawler.Logic.Services;

namespace WebCrawler.Logic.Tests.Scrapers
{
    [TestClass]
    public class ScraperTests
    {
        private Scraper _scraper;

        private Mock<IHtmlWebService> _htmlWebService;
        private Mock<ISetupManager> _setupManager;
        private Mock<IHtmlProcessor> _htmlProcessor;

        private IEnumerable<AssetModel> _assets;
        private IEnumerable<string> _blacklistedExtensions;
        private string _url = "http://GoCardless.com/";
        private bool _usesRobotsTxt = false;
        private Robots _robots;
        private ResultModel _result;

        [TestInitialize]
        public void Setup()
        {
            _htmlWebService = new Mock<IHtmlWebService>();
            _setupManager = new Mock<ISetupManager>();
            _htmlProcessor = new Mock<IHtmlProcessor>();
            _robots = new Robots(string.Empty);
            _result = new ResultModel
            {
                url = _url,
                assets = new List<string>()
            };

            _scraper = new Scraper(_htmlWebService.Object, _setupManager.Object, _htmlProcessor.Object);
        }

        [TestMethod]
        public void Initialize_ProperDataAndNotUsingRobotTxt_InitializedWithoutReadingRobotsTxt()
        {
            var read = false;
            _setupManager.Setup(s => s.ReadRobotsTxt(It.IsAny<string>())).Returns(_robots).Callback(() => read = true);

            _scraper.Initialize(_url, _assets, _blacklistedExtensions, _usesRobotsTxt);

            Assert.IsTrue(_scraper.IsInitialized);
            Assert.IsFalse(read);
        }

        [TestMethod]
        public void Initialize_ProperDataAndUsingRobotTxt_InitializedWithReadingRobotsTxt()
        {
            var read = false;
            _setupManager.Setup(s => s.ReadRobotsTxt(It.IsAny<string>())).Returns(_robots).Callback(() => read = true);
            _usesRobotsTxt = true;

            _scraper.Initialize(_url, _assets, _blacklistedExtensions, _usesRobotsTxt);

            Assert.IsTrue(_scraper.IsInitialized);
            Assert.IsTrue(read);
        }

        [TestMethod]
        public void Proceed_NotInitialized_Exception()
        {
            _scraper.IsInitialized = false;
            try
            {
                _scraper.Proceed();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Crawler not initialized!", ex.Message);
            }
        }

        [TestMethod]
        public void Proceed_Initialized_ResultContainsElement()
        {
            _htmlProcessor.Setup(s => s.GetAssets(It.IsAny<string>(), It.IsAny<HtmlDocument>())).Returns(_result);

            _scraper.Initialize(_url, _assets, _blacklistedExtensions, _usesRobotsTxt);

            var result = _scraper.Proceed();

            Assert.IsTrue(result.ContainsKey(_result.url));
        }
    }
}
