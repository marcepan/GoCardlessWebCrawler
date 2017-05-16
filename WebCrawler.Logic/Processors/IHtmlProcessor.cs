using System.Collections.Generic;
using HtmlAgilityPack;
using WebCrawler.Globals.Models;

namespace WebCrawler.Logic.Processors
{
    public interface IHtmlProcessor
    {
        IEnumerable<AssetModel> AssetList { get; set; }
        IEnumerable<string> BlacklistedExtensions { get; set; }
        IEnumerable<string> GetLinks(HtmlDocument document);
        ResultModel GetAssets(string url, HtmlDocument document);
        bool CheckIfResource(string link);
    }
}
