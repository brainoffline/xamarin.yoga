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
    public class YGHadOverflowTests
    {
        YGNodeRef   root;
        YGConfigRef config;

        [TestInitialize]
        public void YogaTest_HadOverflowTests_Init()
        {
            config = YGConfigNew();
            root   = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 100);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Column);
            YGNodeStyleSetFlexWrap(root, YGWrap.NoWrap);
        }

        [TestCleanup]
        public void YogaTest_HadOverflowTests_Cleanup()
        {
            YGNodeFreeRecursive(root);
            YGConfigFree(config);
        }


        [TestMethod]
        public void children_overflow_no_wrap_and_no_flex_children()
        {
            YGNodeRef child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child0, 80);
            YGNodeStyleSetHeight(child0, 40);
            YGNodeStyleSetMargin(child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(child0, YGEdge.Bottom, 15);
            YGNodeInsertChild(root, child0, 0);
            YGNodeRef child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child1, 80);
            YGNodeStyleSetHeight(child1, 40);
            YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
            YGNodeInsertChild(root, child1, 1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(YGNodeLayoutGetHadOverflow(root));
        }

        [TestMethod]
        public void spacing_overflow_no_wrap_and_no_flex_children()
        {
            YGNodeRef child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child0, 80);
            YGNodeStyleSetHeight(child0, 40);
            YGNodeStyleSetMargin(child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
            YGNodeInsertChild(root, child0, 0);
            YGNodeRef child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child1, 80);
            YGNodeStyleSetHeight(child1, 40);
            YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
            YGNodeInsertChild(root, child1, 1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(YGNodeLayoutGetHadOverflow(root));
        }

        [TestMethod]
        public void no_overflow_no_wrap_and_flex_children()
        {
            YGNodeRef child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child0, 80);
            YGNodeStyleSetHeight(child0, 40);
            YGNodeStyleSetMargin(child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
            YGNodeInsertChild(root, child0, 0);
            YGNodeRef child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child1, 80);
            YGNodeStyleSetHeight(child1, 40);
            YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
            YGNodeStyleSetFlexShrink(child1, 1);
            YGNodeInsertChild(root, child1, 1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsFalse(YGNodeLayoutGetHadOverflow(root));
        }

        [TestMethod]
        public void hadOverflow_gets_reset_if_not_logger_valid()
        {
            YGNodeRef child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child0, 80);
            YGNodeStyleSetHeight(child0, 40);
            YGNodeStyleSetMargin(child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
            YGNodeInsertChild(root, child0, 0);
            YGNodeRef child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child1, 80);
            YGNodeStyleSetHeight(child1, 40);
            YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
            YGNodeInsertChild(root, child1, 1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(YGNodeLayoutGetHadOverflow(root));

            YGNodeStyleSetFlexShrink(child1, 1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsFalse(YGNodeLayoutGetHadOverflow(root));
        }

        [TestMethod]
        public void spacing_overflow_in_nested_nodes()
        {
            YGNodeRef child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child0, 80);
            YGNodeStyleSetHeight(child0, 40);
            YGNodeStyleSetMargin(child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
            YGNodeInsertChild(root, child0, 0);
            YGNodeRef child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child1, 80);
            YGNodeStyleSetHeight(child1, 40);
            YGNodeInsertChild(root, child1, 1);
            YGNodeRef child1_1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(child1_1, 80);
            YGNodeStyleSetHeight(child1_1, 40);
            YGNodeStyleSetMargin(child1_1, YGEdge.Bottom, 5);
            YGNodeInsertChild(child1, child1_1, 0);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(YGNodeLayoutGetHadOverflow(root));
        }
    }
}
