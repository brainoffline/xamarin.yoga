using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

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
