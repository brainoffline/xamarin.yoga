using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGJustifyContentTests
    {
        [TestMethod]
        public void justify_content_row_flex_start()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 102);
            YGNodeStyleSetHeight(root, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetWidth(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(10,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(20,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(92,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(82,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(72,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_flex_end()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetJustifyContent(YGJustify.FlexEnd);
            YGNodeStyleSetWidth(root, 102);
            YGNodeStyleSetHeight(root, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetWidth(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(72,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(82,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(92,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(20,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(10,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_center()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetJustifyContent(YGJustify.Center);
            YGNodeStyleSetWidth(root, 102);
            YGNodeStyleSetHeight(root, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetWidth(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(36,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(56,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(56,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(36,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_space_between()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetJustifyContent(YGJustify.SpaceBetween);
            YGNodeStyleSetWidth(root, 102);
            YGNodeStyleSetHeight(root, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetWidth(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(92,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(92,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_space_around()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetJustifyContent(YGJustify.SpaceAround);
            YGNodeStyleSetWidth(root, 102);
            YGNodeStyleSetHeight(root, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetWidth(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(12,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(80,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(80,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(12,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_flex_start()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 102);
            YGNodeStyleSetHeight(root, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetHeight(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetHeight(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_flex_end()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetJustifyContent(YGJustify.FlexEnd);
            YGNodeStyleSetWidth(root, 102);
            YGNodeStyleSetHeight(root, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetHeight(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetHeight(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(72,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(82,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(72,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(82,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_center()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetJustifyContent(YGJustify.Center);
            YGNodeStyleSetWidth(root, 102);
            YGNodeStyleSetHeight(root, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetHeight(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetHeight(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(36,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(56,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(36,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(56,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_space_between()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetJustifyContent(YGJustify.SpaceBetween);
            YGNodeStyleSetWidth(root, 102);
            YGNodeStyleSetHeight(root, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetHeight(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetHeight(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_space_around()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetJustifyContent(YGJustify.SpaceAround);
            YGNodeStyleSetWidth(root, 102);
            YGNodeStyleSetHeight(root, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetHeight(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetHeight(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(12,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(80,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(12,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(80,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_min_width_and_margin()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetJustifyContent(YGJustify.Center);
            root.StyleSetMargin(YGEdge.Left, 100);
            YGNodeStyleSetMinWidth(root, 50);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 20);
            YGNodeStyleSetHeight(root_child0, 20);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(50,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(15, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(50,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(15, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_min_width_with_padding_child_width_greater_than_parent()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetAlignContent(YGAlign.Stretch);
            YGNodeStyleSetWidth(root, 1000);
            YGNodeStyleSetHeight(root, 1584);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0.StyleSetAlignContent(YGAlign.Stretch);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0.StyleSetJustifyContent(YGJustify.Center);
            root_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            YGNodeStyleSetMinWidth(root_child0_child0, 400);
            root_child0_child0.StyleSetPadding(YGEdge.Horizontal, 100);
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child0.StyleSetDimensions(300, 100);
            root_child0_child0.InsertChild(root_child0_child0_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1000, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1000, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(100, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(300, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1000, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1000, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(500, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(100, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(300, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_min_width_with_padding_child_width_lower_than_parent()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetAlignContent(YGAlign.Stretch);
            YGNodeStyleSetWidth(root, 1080);
            YGNodeStyleSetHeight(root, 1584);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0.StyleSetAlignContent(YGAlign.Stretch);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0.StyleSetJustifyContent(YGJustify.Center);
            root_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            YGNodeStyleSetMinWidth(root_child0_child0, 400);
            root_child0_child0.StyleSetPadding(YGEdge.Horizontal, 100);
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            YGNodeStyleSetWidth(root_child0_child0_child0, 199);
            YGNodeStyleSetHeight(root_child0_child0_child0, 100);
            root_child0_child0.InsertChild(root_child0_child0_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1080, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(400, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(101, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(199, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1080, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(680, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(400, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(101, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(199, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);
            //
        }

        [TestMethod]
        public void justify_content_row_max_width_and_margin()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetJustifyContent(YGJustify.Center);
            root.StyleSetMargin(YGEdge.Left, 100);
            root.StyleSetDimensions(100, 80);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetDimensions(20, 20);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(80,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(30, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(80,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(30, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_min_height_and_margin()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetJustifyContent(YGJustify.Center);
            root.StyleSetMargin(YGEdge.Top, 100);
            YGNodeStyleSetMinHeight(root, 50);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetDimensions(20, 20);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_colunn_max_height_and_margin()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetJustifyContent(YGJustify.Center);
            root.StyleSetMargin(YGEdge.Top, 100);
            YGNodeStyleSetHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 80);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetDimensions(20, 20);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_space_evenly()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetJustifyContent(YGJustify.SpaceEvenly);
            root.StyleSetDimensions(102, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetHeight(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetHeight(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(18,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(74,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(18,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(74,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_space_evenly()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetJustifyContent(YGJustify.SpaceEvenly);
            root.StyleSetDimensions(102, 102);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetHeight(root_child0, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetHeight(root_child2, 10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(26, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(51, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(0,  root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(77, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(0,  root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(77, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(51, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(0,  root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(26, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(0,  root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);
        }
    }
}
