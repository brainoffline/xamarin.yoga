﻿using System;
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
    public class YGDisplayTests
    {
        [TestMethod]
        public void display_none()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetDisplay(root_child1, YGDisplay.None);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void display_none_fixed_size()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 20);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeStyleSetDisplay(root_child1, YGDisplay.None);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void display_none_with_margin()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root_child0, YGEdge.Left,   10);
            YGNodeStyleSetMargin(root_child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(root_child0, YGEdge.Right,  10);
            YGNodeStyleSetMargin(root_child0, YGEdge.Bottom, 10);
            YGNodeStyleSetWidth(root_child0, 20);
            YGNodeStyleSetHeight(root_child0, 20);
            YGNodeStyleSetDisplay(root_child0, YGDisplay.None);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void display_none_with_child()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexShrink(root_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0, 0);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetFlexShrink(root_child1, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1, 0);
            YGNodeStyleSetDisplay(root_child1, YGDisplay.None);
            YGNodeInsertChild(root, root_child1, 1);

            YGNodeRef root_child1_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1_child0, 1);
            YGNodeStyleSetFlexShrink(root_child1_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1_child0, 0);
            YGNodeStyleSetWidth(root_child1_child0, 20);
            YGNodeStyleSetMinWidth(root_child1_child0, 0);
            YGNodeStyleSetMinHeight(root_child1_child0, 0);
            YGNodeInsertChild(root_child1, root_child1_child0, 0);

            YGNodeRef root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child2, 1);
            YGNodeStyleSetFlexShrink(root_child2, 1);
            YGNodeStyleSetFlexBasisPercent(root_child2, 0);
            YGNodeInsertChild(root, root_child2, 2);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(50,  YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child1));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1_child0));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child1_child0));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child1_child0));

            Assert.AreEqual(50,  YGNodeLayoutGetLeft(root_child2));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child2));
            Assert.AreEqual(50,  YGNodeLayoutGetWidth(root_child2));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child2));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(50,  YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(50,  YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child1));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1_child0));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child1_child0));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child1_child0));

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child2));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child2));
            Assert.AreEqual(50,  YGNodeLayoutGetWidth(root_child2));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child2));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void display_none_with_position()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetPosition(root_child1, YGEdge.Top, 10);
            YGNodeStyleSetDisplay(root_child1, YGDisplay.None);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }
    }
}
