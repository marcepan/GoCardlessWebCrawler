using System.Collections.Generic;

namespace WebCrawler.Logic.Processors
{
    public interface ILinkProcessor
    {
        IList<string> ProcessedList { get; set; }
        Stack<string> ToProcessList { get; set; }
        void ProcessLinks(IEnumerable<string> links);
    }
}
