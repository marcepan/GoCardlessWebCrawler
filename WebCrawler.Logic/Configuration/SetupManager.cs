using System.Collections.Generic;
using System.Configuration;
using RobotsTxt;
using WebCrawler.Globals.Extensions;
using WebCrawler.Globals.Models;
using WebCrawler.Logic.Assets;
using WebCrawler.Logic.Services;

namespace WebCrawler.Logic.Configuration
{
    public class SetupManager : ISetupManager
    {
        private readonly IAssetBuilder _assetBuilder;
        private readonly IWebClientService _webClientService;

        public SetupManager(IAssetBuilder assetBuilder, IWebClientService webClientService)
        {
            _assetBuilder = assetBuilder;
            _webClientService = webClientService;
        }

        public SetupManager() : this(new AssetBuilder(), new WebClientService()) { }

        public string GetDefaultUrl()
        {
            return ConfigurationManager.AppSettings["DefaultUrl"];
        }

        public bool UseRobotsTxt()
        {
            return ConfigurationManager.AppSettings["PlayNice"].ToBoolSafe();
        }

        public IEnumerable<string> GetBlockedExtensions()
        {
            return ConfigurationManager.AppSettings["BlackedExtensions"].Split(',');
        }

        public IEnumerable<AssetModel> GetAssets()
        {
            return _assetBuilder.GetDefaultAssets();
        }

        public Robots ReadRobotsTxt(string path)
        {
            return Robots.Load(_webClientService.DownloadString($"{path}/robots.txt"));
        }
    }
}
