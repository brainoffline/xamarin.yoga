using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    [TestClass]
    public class YGZeroOutLayoutRecursivlyTests
    {
        [TestMethod]
        public void zero_out_layout()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

            YGNodeRef child = YGNodeNew();
            YGNodeInsertChild(root, child, 0);
            YGNodeStyleSetWidth(child, 100);
            YGNodeStyleSetHeight(child, 100);
            YGNodeStyleSetMargin(child, YGEdge.Top, 10);
            YGNodeStyleSetPadding(child, YGEdge.Top, 10);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);

            Assert.AreEqual(10, YGNodeLayoutGetMargin(child, YGEdge.Top));
            Assert.AreEqual(10, YGNodeLayoutGetPadding(child, YGEdge.Top));

            YGNodeStyleSetDisplay(child, YGDisplay.None);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetMargin(child, YGEdge.Top));
            Assert.AreEqual(0, YGNodeLayoutGetPadding(child, YGEdge.Top));

            YGNodeFreeRecursive(root);
        }

    }
}
