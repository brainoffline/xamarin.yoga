using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Yoga.Extensions;
// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class YGEnumTests
    {

        [TestMethod]
        public void ConvertEnumToString()
        {
            var result = YGAlign.Auto.ToDescription();

            Assert.AreEqual("auto", result);
        }
    }
}
