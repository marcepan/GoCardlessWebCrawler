using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCrawler.Globals.Helpers;

namespace WebCrawler.Globals.Tests.Helpers
{
    [TestClass]
    public class UrlHelperTests
    {
        private const string Domain = "gocardless.com";
        private const string ClearedAddress = "gocardless.com/test/";

        [TestMethod]
        public void GetDomain_AddressWithWWW_ProperDomain()
        {
            var uri = UrlHelper.CreatUri("http://www.gocardless.com/");

            var domain = UrlHelper.GetDomain(uri);

            Assert.AreEqual(Domain, domain);
        }

        [TestMethod]
        public void GetDomain_AddressWithoutWWW_ProperDomain()
        {
            var uri = UrlHelper.CreatUri("http://gocardless.com/");

            var domain = UrlHelper.GetDomain(uri);

            Assert.AreEqual(Domain, domain);
        }

        [TestMethod]
        public void GetDomain_AddressWithSubpath_ProperDomain()
        {
            var uri = UrlHelper.CreatUri("http://gocardless.com/test/");

            var domain = UrlHelper.GetDomain(uri);

            Assert.AreEqual(Domain, domain);
        }

        [TestMethod]
        public void RemoveBookmarkAndClear_AddressWithWWW_ClearedAddress()
        {
            var url = "www.gocardless.com/test/";

            var address = UrlHelper.RemoveBookmarkAndClear(url);

            Assert.AreEqual(ClearedAddress, address);
        }

        [TestMethod]
        public void RemoveBookmarkAndClear_AddressWithBookmark_ClearedAddress()
        {
            var url = "gocardless.com/test/#bookmark";

            var address = UrlHelper.RemoveBookmarkAndClear(url);

            Assert.AreEqual(ClearedAddress, address);
        }

        [TestMethod]
        public void RemoveBookmarkAndClear_AddressWithWWWAndBookmark_ClearedAddress()
        {
            var url = "www.gocardless.com/test/#bookmark";

            var address = UrlHelper.RemoveBookmarkAndClear(url);

            Assert.AreEqual(ClearedAddress, address);
        }
    }
}
