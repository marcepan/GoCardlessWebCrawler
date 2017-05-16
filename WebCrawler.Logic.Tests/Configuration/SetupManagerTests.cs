using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebCrawler.Globals.Extensions;
using WebCrawler.Globals.Models;
using WebCrawler.Logic.Assets;
using WebCrawler.Logic.Configuration;
using WebCrawler.Logic.Services;

namespace WebCrawler.Logic.Tests.Configuration
{
    [TestClass]
    public class SetupManagerTests
    {
        private SetupManager _setupManager;

        private Mock<IAssetBuilder> _assetBuilder;
        private Mock<IWebClientService> _webClientService;
        private IEnumerable<AssetModel> _assets;

        [TestInitialize]
        public void Setup()
        {
            _assets = new List<AssetModel>();
            _assetBuilder = new Mock<IAssetBuilder>();
            _assetBuilder.Setup(s => s.GetDefaultAssets()).Returns(_assets);

            _webClientService = new Mock<IWebClientService>();

            _setupManager = new SetupManager(_assetBuilder.Object, _webClientService.Object);
        }

        [TestMethod]
        public void GetDefaultUrl_AppConfigExists_DefaultUrl()
        {
            var result = _setupManager.GetDefaultUrl();

            Assert.AreEqual(ConfigurationManager.AppSettings["DefaultUrl"], result);
        }

        [TestMethod]
        public void UseRobotsTxt_AppConfigExists_PlayNice()
        {
            var result = _setupManager.UseRobotsTxt();

            Assert.AreEqual(ConfigurationManager.AppSettings["PlayNice"].ToBoolSafe(), result);
        }

        [TestMethod]
        public void GetBlockedExtensions_AppConfigExists_BlockedExtensions()
        {
            var result = _setupManager.GetBlockedExtensions();

            CollectionAssert.AreEqual(ConfigurationManager.AppSettings["BlackedExtensions"].Split(','), result.ToArray());
        }

        [TestMethod]
        public void GetAssets_AssetBuilderInitialized_Assets()
        {
            var result = _setupManager.GetAssets();

            Assert.AreEqual(_assets, result);
        }

        [TestMethod]
        public void ReadRobotsTxt_Path_RobotsTxtDownloaded()
        {
            var downloaded = false;
            _webClientService.Setup(s => s.DownloadString(It.IsAny<string>())).Callback(() => downloaded = true);

            _setupManager.ReadRobotsTxt("https://GoCardless.com/");

            Assert.IsTrue(downloaded);
        }
    }
}
