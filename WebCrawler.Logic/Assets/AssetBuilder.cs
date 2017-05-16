using System.Collections.Generic;
using WebCrawler.Globals.Models;

namespace WebCrawler.Logic.Assets
{
    public class AssetBuilder : IAssetBuilder
    {
        public IEnumerable<AssetModel> GetDefaultAssets()
        {
            return new List<AssetModel>
            {
                new AssetModel
                {
                   XPathExpression = XPathExpression.IMG_ASSET,
                   Attribute = "src",
                   DefaultValue = "#"
                },
                new AssetModel
                {
                   XPathExpression = XPathExpression.CSS_ASSET,
                   Attribute = "href",
                   DefaultValue = "#"
                },new AssetModel
                {
                   XPathExpression = XPathExpression.SCRIPT_ASSET,
                   Attribute = "src",
                   DefaultValue = "#"
                }
            };
        }
    }
}
