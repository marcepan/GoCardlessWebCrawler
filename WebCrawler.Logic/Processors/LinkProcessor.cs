using System.Collections.Generic;
using System.Linq;
using RobotsTxt;
using WebCrawler.Globals.Helpers;

namespace WebCrawler.Logic.Processors
{
    public class LinkProcessor : ILinkProcessor
    {
        private readonly Robots _robots;
        private readonly string _masterDomain;
        private readonly string _absolutePath;

        public IList<string> ProcessedList { get; set; }
        public Stack<string> ToProcessList { get; set; }

        public LinkProcessor(Robots robots, IList<string> processedList, Stack<string> toProcessList,
            string masterDomain, string absolutePath)
        {
            ProcessedList = processedList;
            ToProcessList = toProcessList;
            _masterDomain = masterDomain;
            _absolutePath = absolutePath;
            _robots = robots;
        }

        public void ProcessLinks(IEnumerable<string> links)
        {
            foreach (var link in links.Where(l => !string.IsNullOrEmpty(l)).Distinct())
            {
                var clearedLink = ClearLink(link);
                if (!_robots.IsPathAllowed(Crawler.USER_AGENT, clearedLink))
                    continue;
                if (UrlHelper.GetDomain(UrlHelper.CreatUri(clearedLink)) == _masterDomain
                      && !ProcessedList.Contains(clearedLink)
                      && !ToProcessList.Contains(clearedLink))
                {
                    ToProcessList.Push(clearedLink);
                }
            }
        }

        private string ClearLink(string link)
        {
            var clearedLink = link;
            if (clearedLink.StartsWith("/"))
                clearedLink = (_absolutePath.EndsWith("/") ? _absolutePath.Substring(0, _absolutePath.Length - 1) : _absolutePath) + clearedLink;
            return UrlHelper.RemoveBookmarkAndClear(clearedLink);
        }
    }
}
