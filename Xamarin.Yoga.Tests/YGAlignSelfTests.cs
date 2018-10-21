using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGAlignSelfTests
    {
        [TestMethod]
        public void align_self_center()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetAlignSelf(YGAlign.Center);
            root_child0.StyleSetWidth(10);
            root_child0.StyleSetHeight(10);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_self_flex_end()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetAlignSelf(YGAlign.FlexEnd);
            root_child0.StyleSetWidth(10);
            root_child0.StyleSetHeight(10);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_self_flex_start()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetAlignSelf(YGAlign.FlexStart);
            root_child0.StyleSetWidth(10);
            root_child0.StyleSetHeight(10);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_self_flex_end_override_flex_start()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetAlignSelf(YGAlign.FlexEnd);
            root_child0.StyleSetWidth(10);
            root_child0.StyleSetHeight(10);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_self_baseline()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetAlignSelf(YGAlign.Baseline);
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetHeight(50);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetAlignSelf(YGAlign.Baseline);
            root_child1.StyleSetWidth(50);
            root_child1.StyleSetHeight(20);
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.StyleSetWidth(50);
            root_child1_child0.StyleSetHeight(10);
            root_child1.InsertChild(root_child1_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);
        }
    }
}
