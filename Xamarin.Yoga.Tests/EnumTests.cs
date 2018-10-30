using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class EnumTests
    {
        [TestMethod]
        public void ConvertEnumToString()
        {
            var result = AlignType.Auto.ToDescription();

            Assert.AreEqual("auto", result);
        }
    }
}
