using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCrawler.Globals.Extensions;

namespace WebCrawler.Globals.Tests.Extensions
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void GetLast_TakeThreeEmptyString_EmptyString()
        {
            var text = string.Empty;

            var result = text.GetLast(3);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetLast_TakeThreeLongString_LastThreeChars()
        {
            var text = "ABCDEFGHIJK";

            var result = text.GetLast(3);

            Assert.AreEqual("IJK", result);
        }

        [TestMethod]
        public void GetLast_TakeThreeTwoCharsString_EntireString()
        {
            var text = "AB";

            var result = text.GetLast(3);

            Assert.AreEqual("AB", result);
        }

        [TestMethod]
        public void GetLast_TakeThreeThreeCharsString_EntireString()
        {
            var text = "ABC";

            var result = text.GetLast(3);

            Assert.AreEqual("ABC", result);
        }

        [TestMethod]
        public void ToBoolSafe_True_True()
        {
            var text = "true";

            var result = text.ToBoolSafe();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToBoolSafe_Flase_False()
        {
            var text = "false";

            var result = text.ToBoolSafe();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToBoolSafe_NotBool_DefaultValue()
        {
            var text = "NotBool";

            var result = text.ToBoolSafe();

            Assert.IsFalse(result);
        }
    }
}
