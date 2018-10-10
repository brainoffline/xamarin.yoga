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
    public class YGBaselineFuncTests
    {
        private static float _baseline(YGNodeRef node, float width, float height)
        {
            float baseline = (float)node.getContext();
            return baseline;
        }

        [TestMethod]
        public void align_baseline_customer_func()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root, YGAlign.Baseline);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0 = YGNodeNew();
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNew();
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

            float     baselineValue      = 10;
            YGNodeRef root_child1_child0 = YGNodeNew();
            root_child1_child0.setContext(baselineValue);
            YGNodeStyleSetWidth(root_child1_child0, 50);
            root_child1_child0.setBaseLineFunc(_baseline);
            YGNodeStyleSetHeight(root_child1_child0, 20);
            YGNodeInsertChild(root_child1, root_child1_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(50, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(40, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(20, YGNodeLayoutGetHeight(root_child1));

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child1_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child1_child0));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root_child1_child0));
            Assert.AreEqual(20, YGNodeLayoutGetHeight(root_child1_child0));
        }
    }
}
