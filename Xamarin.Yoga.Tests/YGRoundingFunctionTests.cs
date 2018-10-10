using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    [TestClass]
    public class YGRoundingFunctionTests
    {
        [TestMethod]
        public void rounding_value()
        {
            // Test that whole numbers are rounded to whole despite ceil/floor flags
            Assert.AreEqual(6.0, YGRoundValueToPixelGrid(6.000001f, 2.0f, false, false));
            Assert.AreEqual(6.0, YGRoundValueToPixelGrid(6.000001f, 2.0f, true,  false));
            Assert.AreEqual(6.0, YGRoundValueToPixelGrid(6.000001f, 2.0f, false, true));
            Assert.AreEqual(6.0, YGRoundValueToPixelGrid(5.999999f, 2.0f, false, false));
            Assert.AreEqual(6.0, YGRoundValueToPixelGrid(5.999999f, 2.0f, true,  false));
            Assert.AreEqual(6.0, YGRoundValueToPixelGrid(5.999999f, 2.0f, false, true));

            // Test that numbers with fraction are rounded correctly accounting for ceil/floor flags
            Assert.AreEqual(6.0, YGRoundValueToPixelGrid(6.01f, 2.0f, false, false));
            Assert.AreEqual(6.5, YGRoundValueToPixelGrid(6.01f, 2.0f, true,  false));
            Assert.AreEqual(6.0, YGRoundValueToPixelGrid(6.01f, 2.0f, false, true));
            Assert.AreEqual(6.0, YGRoundValueToPixelGrid(5.99f, 2.0f, false, false));
            Assert.AreEqual(6.0, YGRoundValueToPixelGrid(5.99f, 2.0f, true,  false));
            Assert.AreEqual(5.5, YGRoundValueToPixelGrid(5.99f, 2.0f, false, true));
        }

    }
}
