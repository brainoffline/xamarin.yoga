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
    public class YGNodeChildTests
    {
        [TestMethod]
        public void reset_layout_when_child_removed()
        {
             YGNodeRef root = YGNodeNew();

             YGNodeRef root_child0 = YGNodeNew();
            YGNodeStyleSetWidth(root_child0, 100);
            YGNodeStyleSetHeight(root_child0, 100);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            YGNodeRemoveChild(root, root_child0);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.IsTrue(YGFloatIsUndefined(YGNodeLayoutGetWidth(root_child0)));
            Assert.IsTrue(YGFloatIsUndefined(YGNodeLayoutGetHeight(root_child0)));

            YGNodeFreeRecursive(root);
        }

    }
}
