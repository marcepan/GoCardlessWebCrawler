using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsTxt;
using WebCrawler.Globals.Helpers;
using WebCrawler.Logic.Processors;

namespace WebCrawler.Logic.Tests.Processors
{
    [TestClass]
    public class LinkProcessorTests
    {
        private LinkProcessor _linkProcessor;
        private Robots _robots;
        private IList<string> _processedList;
        private Stack<string> _toProcessList;
        private string _masterDomain;
        private string _absolutePath;
        private IEnumerable<string> _links;

        [TestInitialize]
        public void Setup()
        {
            _robots = Robots.Load(string.Empty);
            _processedList = new List<string>
            {
                "http://gocardless.com/test2"
            };
            _toProcessList = new Stack<string>();
            _masterDomain = "gocardless.com";
            _absolutePath = "http://gocardless.com";
            _linkProcessor = new LinkProcessor(_robots, _processedList, _toProcessList, _masterDomain, _absolutePath);
        }

        [TestMethod]
        public void ProcessLinks_ThreeLinks_ThreeLinksOnProcessList()
        {
            _links = new List<string>
            {
                "http://gocardless.com/",
                "http://gocardless.com/test",
                "/test/next",
            };

            _linkProcessor.ProcessLinks(_links);

            Assert.AreEqual(_links.Count(), _linkProcessor.ToProcessList.Count);
        }

        [TestMethod]
        public void ProcessLinks_ThreeLinks_AllLinksWithProperDomain()
        {
            _links = new List<string>
            {
                "http://gocardless.com/",
                "http://gocardless.com/test",
                "/test/next",
            };

            _linkProcessor.ProcessLinks(_links);

            foreach (var link in _linkProcessor.ToProcessList)
            {
                Assert.AreEqual(_masterDomain, UrlHelper.GetDomain(UrlHelper.CreatUri(link)));
            }
        }

        [TestMethod]
        public void ProcessLinks_FourLinksWithOneAlreadyProcessed_ThreeLinksOnProcessList()
        {
            _links = new List<string>
            {
                "http://gocardless.com/",
                "http://gocardless.com/test",
                "http://gocardless.com/test2",
                "/test/next",
            };

            _linkProcessor.ProcessLinks(_links);

            Assert.AreEqual(_links.Count() - 1, _linkProcessor.ToProcessList.Count);
        }

        [TestMethod]
        public void ProcessLinks_FourLinksWithOneAlreadyOnToProcessList_FourLinksOnProcessList()
        {
            _toProcessList.Push("http://gocardless.com/test3");
            _links = new List<string>
            {
                "http://gocardless.com/",
                "http://gocardless.com/test",
                "http://gocardless.com/test3",
                "/test/next",
            };

            _linkProcessor.ProcessLinks(_links);

            Assert.AreEqual(_links.Count(), _linkProcessor.ToProcessList.Count);
        }

        [TestMethod]
        public void ProcessLinks_ThreeLinksButOnFromDifferentDomain_TwoLinksOnProcessList()
        {
            _links = new List<string>
            {
                "http://test.gocardless.com/",
                "http://gocardless.com/test",
                "/test/next",
            };

            _linkProcessor.ProcessLinks(_links);

            Assert.AreEqual(_links.Count() - 1, _linkProcessor.ToProcessList.Count);
        }
    }
}
