using System.Collections.Generic;
using WebCrawler.Globals.Models;

namespace WebCrawler.Logic.Assets
{
    public interface IAssetBuilder
    {
        IEnumerable<AssetModel> GetDefaultAssets();
    }
}
