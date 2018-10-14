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
    public class YGMinMaxDimensionTests
    {
        [TestMethod]
        public void max_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetMaxWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void max_height()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 10);
            YGNodeStyleSetMaxHeight(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void min_height()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMinHeight(root_child0, 60);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void min_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMinWidth(root_child0, 60);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(80, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(20, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(20, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void justify_content_min_max()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 200);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 60);
            YGNodeStyleSetHeight(root_child0, 60);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(40, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void align_items_min_max()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetMinWidth(root, 100);
            YGNodeStyleSetMaxWidth(root, 200);
            YGNodeStyleSetHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 60);
            YGNodeStyleSetHeight(root_child0, 60);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void justify_content_overflow_min_max()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 110);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);

             YGNodeRef root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeStyleSetHeight(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(110, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(-20, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0, root_child2.Layout.Position.Left);
            Assert.AreEqual(80, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(110, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(-20, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0, root_child2.Layout.Position.Left);
            Assert.AreEqual(80, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_to_min()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 500);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexShrink(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_in_at_most_container()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0_child0, 0);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(0, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(0, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_child()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 0);
            YGNodeStyleSetHeight(root_child0, 100);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_within_constrained_min_max_column()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 200);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_within_max_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetMaxWidth(root_child0, 100);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetHeight(root_child0_child0, 20);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_within_constrained_max_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetMaxWidth(root_child0, 300);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetHeight(root_child0_child0, 20);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_root_ignored()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root, 1);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 500);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 200);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child1, 100);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(300, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(300, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_root_minimized()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 500);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMinHeight(root_child0, 100);
            YGNodeStyleSetMaxHeight(root_child0, 500);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0_child0, 200);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNodeRef root_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child0_child1, 100);
            YGNodeInsertChild(root_child0, root_child0_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(300, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(300, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(200, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(300, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(300, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(200, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_height_maximized()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 500);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMinHeight(root_child0, 100);
            YGNodeStyleSetMaxHeight(root_child0, 500);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0_child0, 200);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNodeRef root_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child0_child1, 100);
            YGNodeInsertChild(root_child0, root_child0_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(500, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(400, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(400, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(500, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(400, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(400, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_within_constrained_min_row()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetMinWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_within_constrained_min_column()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetMinHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_within_constrained_max_row()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetMaxWidth(root_child0, 100);
            YGNodeStyleSetHeight(root_child0, 100);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexShrink(root_child0_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0_child0, 100);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNodeRef root_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child1, 50);
            YGNodeInsertChild(root_child0, root_child0_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void flex_grow_within_constrained_max_column()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetMaxHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexShrink(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 100);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void child_min_max_width_flexing()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 120);
            YGNodeStyleSetHeight(root, 50);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 0);
            YGNodeStyleSetMinWidth(root_child0, 60);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1, 50);
            YGNodeStyleSetMaxWidth(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(120, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(20, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(120, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(20, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void min_width_overrides_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetMinWidth(root, 100);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(0, root.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(0, root.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void max_width_overrides_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetMaxWidth(root, 100);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(0, root.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(0, root.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void min_height_overrides_height()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root, 50);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void max_height_overrides_height()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root, 200);
            YGNodeStyleSetMaxHeight(root, 100);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void min_max_percent_no_width_height()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetMinWidthPercent(root_child0, 10);
            YGNodeStyleSetMaxWidthPercent(root_child0, 10);
            YGNodeStyleSetMinHeightPercent(root_child0, 10);
            YGNodeStyleSetMaxHeightPercent(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

    }
}
