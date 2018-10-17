using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;

    [TestClass]
    public class YGAbsolutePositionTests
    {
        [TestMethod]
        public void absolute_layout_width_height_start_top()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child0, YGEdge.Start, 10);
            YGNodeStyleSetPosition(root_child0, YGEdge.Top,   10);
            root_child0.StyleSetDimensions(10, 10);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_width_height_end_bottom()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child0, YGEdge.End,    10);
            YGNodeStyleSetPosition(root_child0, YGEdge.Bottom, 10);
            root_child0.StyleSetDimensions(10, 10);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_start_top_end_bottom()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child0, YGEdge.Start,  10);
            YGNodeStyleSetPosition(root_child0, YGEdge.Top,    10);
            YGNodeStyleSetPosition(root_child0, YGEdge.End,    10);
            YGNodeStyleSetPosition(root_child0, YGEdge.Bottom, 10);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_width_height_start_top_end_bottom()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child0, YGEdge.Start,  10);
            YGNodeStyleSetPosition(root_child0, YGEdge.Top,    10);
            YGNodeStyleSetPosition(root_child0, YGEdge.End,    10);
            YGNodeStyleSetPosition(root_child0, YGEdge.Bottom, 10);
            root_child0.StyleSetDimensions(10, 10);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void do_not_clamp_height_of_absolute_node_to_height_of_its_overflow_hidden_parent()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetOverflow(root, YGOverflow.Hidden);
            root.StyleSetDimensions(50, 50);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child0, YGEdge.Start, 0);
            YGNodeStyleSetPosition(root_child0, YGEdge.Top,   0);
            root.InsertChild(root_child0, 0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetDimensions(100, 100);
            root_child0.InsertChild(root_child0_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(-50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_within_border()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetMargin(new Edges(10,  10, 10, 10));
            root.StyleSetPadding(new Edges(10, 10, 10, 10));
            root.StyleSetBorder(new Edges(10,  10, 10, 10));
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child0, YGEdge.Left, 0);
            YGNodeStyleSetPosition(root_child0, YGEdge.Top,  0);
            root_child0.StyleSetDimensions(50, 50);
            root.InsertChild(root_child0, 0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child1, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child1, YGEdge.Right,  0);
            YGNodeStyleSetPosition(root_child1, YGEdge.Bottom, 0);
            root_child1.StyleSetDimensions(50, 50);
            root.InsertChild(root_child1, 1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child2, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child2, YGEdge.Left, 0);
            YGNodeStyleSetPosition(root_child2, YGEdge.Top,  0);
            YGNodeStyleSetMargin(root_child2, YGEdge.Left,   10);
            YGNodeStyleSetMargin(root_child2, YGEdge.Top,    10);
            YGNodeStyleSetMargin(root_child2, YGEdge.Right,  10);
            YGNodeStyleSetMargin(root_child2, YGEdge.Bottom, 10);
            root_child2.StyleSetDimensions(50, 50);
            root.InsertChild(root_child2, 2);

            YGNode root_child3 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child3, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child3, YGEdge.Right,  0);
            YGNodeStyleSetPosition(root_child3, YGEdge.Bottom, 0);
            YGNodeStyleSetMargin(root_child3, YGEdge.Left,   10);
            YGNodeStyleSetMargin(root_child3, YGEdge.Top,    10);
            YGNodeStyleSetMargin(root_child3, YGEdge.Right,  10);
            YGNodeStyleSetMargin(root_child3, YGEdge.Bottom, 10);
            root_child3.StyleSetDimensions(50, 50);
            root.InsertChild(root_child3, 3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(10,  root.Layout.Position.Left);
            Assert.AreEqual(10,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(20, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(10,  root.Layout.Position.Left);
            Assert.AreEqual(10,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(20, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetFlexGrow(root, 1);
            root.StyleSetDimensions(110, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            root_child0.StyleSetDimensions(60, 40);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_flex_end()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.FlexEnd);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexEnd);
            YGNodeStyleSetFlexGrow(root, 1);
            root.StyleSetDimensions(110, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            root_child0.StyleSetDimensions(60, 40);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(60, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(60, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_justify_content_center()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetFlexGrow(root, 1);
            root.StyleSetDimensions(110, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            root_child0.StyleSetDimensions(60, 40);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_align_items_center()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetFlexGrow(root, 1);
            root.StyleSetDimensions(110, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            root_child0.StyleSetDimensions(60, 40);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_align_items_center_on_child_only()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetFlexGrow(root, 1);
            root.StyleSetDimensions(110, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetAlignSelf(root_child0, YGAlign.Center);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            root_child0.StyleSetDimensions(60, 40);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_top_position()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetFlexGrow(root, 1);
            root.StyleSetDimensions(110, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child0, YGEdge.Top, 10);
            root_child0.StyleSetDimensions(60, 40);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_bottom_position()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetFlexGrow(root, 1);
            root.StyleSetDimensions(110, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child0, YGEdge.Bottom, 10);
            root_child0.StyleSetDimensions(60, 40);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_left_position()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetFlexGrow(root, 1);
            root.StyleSetDimensions(110, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child0, YGEdge.Left, 5);
            root_child0.StyleSetDimensions(60, 40);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(5,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(5,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_right_position()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetFlexGrow(root, 1);
            root.StyleSetDimensions(110, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPosition(root_child0, YGEdge.Right, 5);
            root_child0.StyleSetDimensions(60, 40);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void position_root_with_rtl_should_position_withoutdirection()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetPosition(root, YGEdge.Left, 72);
            root.StyleSetDimensions(52, 52);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(72, root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(72, root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_percentage_bottom_based_on_parent_height()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetDimensions(100, 200);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPositionPercent(root_child0, YGEdge.Top, 50);
            root_child0.StyleSetDimensions(10, 10);
            root.InsertChild(root_child0, 0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child1, YGPositionType.Absolute);
            YGNodeStyleSetPositionPercent(root_child1, YGEdge.Bottom, 50);
            root_child1.StyleSetDimensions(10, 10);
            root.InsertChild(root_child1, 1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child2, YGPositionType.Absolute);
            YGNodeStyleSetPositionPercent(root_child2, YGEdge.Top,    10);
            YGNodeStyleSetPositionPercent(root_child2, YGEdge.Bottom, 10);
            YGNodeStyleSetWidth(root_child2, 10);
            root.InsertChild(root_child2, 2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(100, root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(90, root_child1.Layout.Position.Top);
            Assert.AreEqual(10, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(160, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(90,  root_child0.Layout.Position.Left);
            Assert.AreEqual(100, root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(90, root_child1.Layout.Position.Left);
            Assert.AreEqual(90, root_child1.Layout.Position.Top);
            Assert.AreEqual(10, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(90,  root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(160, root_child2.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_column_container()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetFlexWrap(root, YGWrap.WrapReverse);
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            root_child0.StyleSetDimensions(20, 20);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_row_container()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root, YGWrap.WrapReverse);
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            root_child0.StyleSetDimensions(20, 20);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_column_container_flex_end()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetFlexWrap(root, YGWrap.WrapReverse);
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetAlignSelf(root_child0, YGAlign.FlexEnd);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            root_child0.StyleSetDimensions(20, 20);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_row_container_flex_end()
        {
            var config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root, YGWrap.WrapReverse);
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetAlignSelf(root_child0, YGAlign.FlexEnd);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            root_child0.StyleSetDimensions(20, 20);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }
    }
}
