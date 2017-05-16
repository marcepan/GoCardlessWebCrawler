using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using RobotsTxt;
using WebCrawler.Globals.Helpers;
using WebCrawler.Globals.Models;
using WebCrawler.Logic.Configuration;
using WebCrawler.Logic.Processors;
using WebCrawler.Logic.Services;

namespace WebCrawler.Logic.Scrapers
{
    public class Scraper : IScraper
    {
        private readonly IHtmlWebService _htmlWebService;
        private readonly ISetupManager _setupManager;
        private readonly IHtmlProcessor _htmlProcessor;
        private readonly IList<string> _processedList;
        private readonly Stack<string> _toProcessList;
        private ILinkProcessor _linkProcessor;
        private Robots _robots;
        private string _masterDomain;
        private string _absolutePath;
        private Uri _defaultUri;

        public bool IsInitialized { get; set; }

        public Scraper(IHtmlWebService htmlWebService, ISetupManager setupManager, IHtmlProcessor htmlProcessor)
        {
            _htmlWebService = htmlWebService;
            _setupManager = setupManager;
            _htmlProcessor = htmlProcessor;

            _robots = new Robots(string.Empty);
            _processedList = new List<string>();
            _toProcessList = new Stack<string>();
        }

        public Scraper() : this(new HtmlWebService(), new SetupManager(), new HtmlProcessor()) { }

        public void Initialize(string url, IEnumerable<AssetModel> assets, IEnumerable<string> blacklistedExtensions, bool usesRobotsTxt = true)
        {
            _defaultUri = UrlHelper.CreatUri(url);
            _masterDomain = UrlHelper.GetDomain(_defaultUri);
            _absolutePath = Regex.Match(_defaultUri.AbsoluteUri, RegexExpression.GET_BASE_URL).Value;
            _linkProcessor = new LinkProcessor(_robots, _processedList, _toProcessList, _masterDomain, _absolutePath);

            if (usesRobotsTxt)
                _robots = _setupManager.ReadRobotsTxt(_absolutePath);
            _htmlProcessor.AssetList = assets;
            _htmlProcessor.BlacklistedExtensions = blacklistedExtensions;

            if (_robots.IsPathAllowed(Crawler.USER_AGENT, _defaultUri.AbsolutePath))
                _toProcessList.Push(_defaultUri.AbsoluteUri);

            IsInitialized = true;
        }

        public IDictionary<string, ResultModel> Proceed()
        {
            if (!IsInitialized)
                throw new Exception("Crawler not initialized!");

            var results = new Dictionary<string, ResultModel>();
            while (_toProcessList.Count > 0)
            {
                var address = _toProcessList.Pop();
                _processedList.Add(address);
                var doc = _htmlWebService.Load(address);
                var result = _htmlProcessor.GetAssets(address, doc);
                if (result != null)
                    results.Add(result.url, result);
                var links = _htmlProcessor.GetLinks(doc);
                _linkProcessor.ProcessLinks(links);
            }
            return results;
        }
    }
}
