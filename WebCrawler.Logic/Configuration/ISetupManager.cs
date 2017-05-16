using System.Collections.Generic;
using RobotsTxt;
using WebCrawler.Globals.Models;

namespace WebCrawler.Logic.Configuration
{
    public interface ISetupManager
    {
        string GetDefaultUrl();
        bool UseRobotsTxt();
        IEnumerable<string> GetBlockedExtensions();
        IEnumerable<AssetModel> GetAssets();
        Robots ReadRobotsTxt(string path);
    }
}
