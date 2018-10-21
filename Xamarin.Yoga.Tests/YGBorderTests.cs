using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGBorderTests
    {
        [TestMethod]
        public void border_no_size()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetBorder(new Edges(10, 10,10,10));
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(20, root.Layout.Width);
            Assert.AreEqual(20, root.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(20, root.Layout.Width);
            Assert.AreEqual(20, root.Layout.Height);
        }

        [TestMethod]
        public void border_container_match_child()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetBorder(new Edges(10, 10, 10, 10));

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetDimensions(10, 10);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(30, root.Layout.Width);
            Assert.AreEqual(30, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(30, root.Layout.Width);
            Assert.AreEqual(30, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void border_flex_child()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetBorder(new Edges(10, 10, 10, 10));
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetWidth(10);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);
        }

        [TestMethod]
        public void border_stretch_child()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetBorder(new Edges(10, 10, 10, 10));
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetHeight(10);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void border_center_child()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetJustifyContent( YGJustify.Center);
            root.StyleSetAlignItems(YGAlign.Center);
            root.StyleSetBorder(YGEdge.Start,  10);
            root.StyleSetBorder(YGEdge.End,    20);
            root.StyleSetBorder(YGEdge.Bottom, 20);
            root.StyleSetDimensions(100, 100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetDimensions(10, 10);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(40, root_child0.Layout.Position.Left);
            Assert.AreEqual(35, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(35, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }
    }
}
