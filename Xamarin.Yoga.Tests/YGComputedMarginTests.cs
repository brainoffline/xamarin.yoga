using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    [TestClass]
    public class YGComputedMarginTests
    {
        [TestMethod]
        public void computed_layout_margin()
        {
            var root = YGNodeNew();
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);
            YGNodeStyleSetMarginPercent(root, YGEdge.Start, 10);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);

            Assert.AreEqual(10, YGNodeLayoutGetMargin(root, YGEdge.Left));
            Assert.AreEqual(0,  YGNodeLayoutGetMargin(root, YGEdge.Right));

            YGNodeCalculateLayout(root, 100, 100, YGDirection.RTL);

            Assert.AreEqual(0,  YGNodeLayoutGetMargin(root, YGEdge.Left));
            Assert.AreEqual(10, YGNodeLayoutGetMargin(root, YGEdge.Right));

            YGNodeFreeRecursive(root);
        }

    }
}
