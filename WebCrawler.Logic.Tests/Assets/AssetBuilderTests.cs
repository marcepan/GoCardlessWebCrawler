using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCrawler.Globals.Models;
using WebCrawler.Logic.Assets;

namespace WebCrawler.Logic.Tests.Assets
{
    [TestClass]
    public class AssetBuilderTests
    {
        private AssetBuilder _assetBuilder;
        [TestInitialize]
        public void Setup()
        {
            _assetBuilder = new AssetBuilder();
        }

        [TestMethod]
        public void GetDefaultAssets_DefaultConfig_ContainsDefaultAssets()
        {
            var result = _assetBuilder.GetDefaultAssets();

            var assetModels = result as IList<AssetModel> ?? result.ToList();

            Assert.IsNotNull(assetModels.FirstOrDefault(a => a.XPathExpression == XPathExpression.IMG_ASSET));
            Assert.IsNotNull(assetModels.FirstOrDefault(a => a.XPathExpression == XPathExpression.CSS_ASSET));
            Assert.IsNotNull(assetModels.FirstOrDefault(a => a.XPathExpression == XPathExpression.SCRIPT_ASSET));
        }
    }
}
