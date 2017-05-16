using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using WebCrawler.Globals.Extensions;
using WebCrawler.Globals.Models;

namespace WebCrawler.Logic.Processors
{
    public class HtmlProcessor : IHtmlProcessor
    {
        private const string LINK_ATTRIBUTE = "href";
        private const string LINK_DEFAULT_VALUE = "#";

        public IEnumerable<AssetModel> AssetList { get; set; }
        public IEnumerable<string> BlacklistedExtensions { get; set; }

        public HtmlProcessor()
        {
            AssetList = new List<AssetModel>();
            BlacklistedExtensions = new List<string>();
        }

        public ResultModel GetAssets(string url, HtmlDocument document)
        {
            var assets = new List<string>();
            if (document == null)
                return null;
            foreach (var asset in AssetList)
            {
                assets.AddRange(
                   document.DocumentNode.SafeSelectNodes(asset.XPathExpression).Select(link => link.GetAttributeValue(asset.Attribute, asset.DefaultValue)));
            }
            return new ResultModel
            {
                url = url,
                assets = assets
            };
        }

        public IEnumerable<string> GetLinks(HtmlDocument document)
        {
            var links = new List<string>();
            if (document == null)
                return links;
            links.AddRange(document.DocumentNode.SafeSelectNodes(XPathExpression.A_ASSET)
                  .Select(link => link.GetAttributeValue(LINK_ATTRIBUTE, LINK_DEFAULT_VALUE))
                  .Where(asset => !CheckIfResource(asset)));
            return links;
        }

        public bool CheckIfResource(string link)
        {
            return BlacklistedExtensions.Contains(link.GetLast(4).ToUpper());
        }
    }
}
