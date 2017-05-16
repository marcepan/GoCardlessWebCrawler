using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCrawler.Globals.Models;
using WebCrawler.Logic.Processors;

namespace WebCrawler.Logic.Tests.Processors
{
    [TestClass]
    public class HtmlProcessorTests
    {
        private HtmlProcessor _htmlProcessor;
        private string _url = "http://GoCardless.com/";

        [TestInitialize]
        public void Setup()
        {
            _htmlProcessor = new HtmlProcessor { AssetList = new List<AssetModel>(), BlacklistedExtensions = new List<string> { ".JPG" } };
        }

        [TestMethod]
        public void GetAssets_HtmlDocumentIsNull_Null()
        {
            var result = _htmlProcessor.GetAssets(_url, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAssets_ExampleHtmlDocument_AssertWithProperUrl()
        {
            var result = _htmlProcessor.GetAssets(_url, new HtmlDocument());

            Assert.AreEqual(_url, result.url);
        }

        [TestMethod]
        public void GetLinks_ExampleHtmlDocument_EmptyList()
        {
            var result = _htmlProcessor.GetLinks(new HtmlDocument());

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void CheckIfResource_BlacklistedResourceInLink_True()
        {
            var result = _htmlProcessor.CheckIfResource("http://GoCardless.com/test.jpg");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckIfResource_NormalLink_false()
        {
            var result = _htmlProcessor.CheckIfResource("http://GoCardless.com/test");

            Assert.IsFalse(result);
        }
    }
}
