using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGAlignItemsTests
    {
        [TestMethod]
        public void align_items_stretch()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Height = 10;
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_items_center()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.Center;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 10;
            root_child0.Style.Height = 10;
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
        public void align_items_flex_start()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 10;
            root_child0.Style.Height = 10;
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
        public void align_items_flex_end()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.FlexEnd;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 10;
            root_child0.Style.Height = 10;
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
        public void align_baseline()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.InsertChild(1, root_child1);
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
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

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
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 50;
            root_child1_child0.Style.Height = 10;
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

        [TestMethod]
        public void align_baseline_child_multiline()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 60;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexDirection = YGFlexDirection.Row;
            root_child1.Style.FlexWrap = YGWrap.Wrap;
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 25;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 25;
            root_child1_child0.Style.Height = 20;
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child1_child1 = new YGNode(config);
            root_child1_child1.Style.Width = 25;
            root_child1_child1.Style.Height = 10;
            root_child1.InsertChild(1, root_child1_child1);

            YGNode root_child1_child2 = new YGNode(config);
            root_child1_child2.Style.Width = 25;
            root_child1_child2.Style.Height = 20;
            root_child1.InsertChild(2, root_child1_child2);

            YGNode root_child1_child3 = new YGNode(config);
            root_child1_child3.Style.Width = 25;
            root_child1_child3.Style.Height = 10;
            root_child1.InsertChild(3, root_child1_child3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(25, root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(25, root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(0,  root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_multiline_override()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 60;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexDirection = YGFlexDirection.Row;
            root_child1.Style.FlexWrap = YGWrap.Wrap;
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 25;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 25;
            root_child1_child0.Style.Height = 20;
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child1_child1 = new YGNode(config);
            root_child1_child1.Style.AlignSelf = YGAlign.Baseline;
            root_child1_child1.Style.Width = 25;
            root_child1_child1.Style.Height = 10;
            root_child1.InsertChild(1, root_child1_child1);

            YGNode root_child1_child2 = new YGNode(config);
            root_child1_child2.Style.Width = 25;
            root_child1_child2.Style.Height = 20;
            root_child1.InsertChild(2, root_child1_child2);

            YGNode root_child1_child3 = new YGNode(config);
            root_child1_child3.Style.AlignSelf = YGAlign.Baseline;
            root_child1_child3.Style.Width = 25;
            root_child1_child3.Style.Height = 10;
            root_child1.InsertChild(3, root_child1_child3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(25, root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(25, root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(0,  root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_multiline_no_override_on_secondline()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 60;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexDirection = YGFlexDirection.Row;
            root_child1.Style.FlexWrap = YGWrap.Wrap;
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 25;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 25;
            root_child1_child0.Style.Height = 20;
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child1_child1 = new YGNode(config);
            root_child1_child1.Style.Width = 25;
            root_child1_child1.Style.Height = 10;
            root_child1.InsertChild(1, root_child1_child1);

            YGNode root_child1_child2 = new YGNode(config);
            root_child1_child2.Style.Width = 25;
            root_child1_child2.Style.Height = 20;
            root_child1.InsertChild(2, root_child1_child2);

            YGNode root_child1_child3 = new YGNode(config);
            root_child1_child3.Style.AlignSelf = YGAlign.Baseline;
            root_child1_child3.Style.Width = 25;
            root_child1_child3.Style.Height = 10;
            root_child1.InsertChild(3, root_child1_child3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(25, root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(25, root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(0,  root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_top()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Position.Top = 10;
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 50;
            root_child1_child0.Style.Height = 10;
            root_child1.InsertChild(root_child1_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
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
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
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

        [TestMethod]
        public void align_baseline_child_top2()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Position.Top = 5;
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 50;
            root_child1_child0.Style.Height = 10;
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
            Assert.AreEqual(45, root_child1.Layout.Position.Top);
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
            Assert.AreEqual(45, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_double_nested_child()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Width = 50;
            root_child0_child0.Style.Height = 20;
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 50;
            root_child1_child0.Style.Height = 15;
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

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(15, root_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(15, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_column()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_margin()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config)
            {
                Style =
                {
                    FlexDirection = YGFlexDirection.Row,
                    AlignItems = YGAlign.Baseline,
                    Width = 100,
                    Height = 100
                }
            };

            YGNode root_child0 = new YGNode(config)
            {
                Style =
                {
                    Margin = new Edges(5, 5, 5, 5),
                    Width = 50,
                    Height = 50
                }
            };
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config) {Style = {Width = 50, Height = 20}};
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config)
            {
                Style =
                {
                    Margin = new Edges(1, 1, 1, 1),
                    Width = 50, Height = 10
                }
            };
            root_child1.InsertChild(root_child1_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(5,  root_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(60, root_child1.Layout.Position.Left);
            Assert.AreEqual(44, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(1,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(1,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(-10, root_child1.Layout.Position.Left);
            Assert.AreEqual(44,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(20,  root_child1.Layout.Height);

            Assert.AreEqual(-1, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(1,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_padding()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Padding = new Edges(5,5,5,5);
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Padding = new Edges(5, 5, 5, 5);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 50;
            root_child1_child0.Style.Height = 10;
            root_child1.InsertChild(root_child1_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(5,  root_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(55, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(5,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(-5, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(-5, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_multiline()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 50;
            root_child1_child0.Style.Height = 10;
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root_child2.Style.Height = 20;
            root.InsertChild(2, root_child2);

            YGNode root_child2_child0 = new YGNode(config);
            root_child2_child0.Style.Width = 50;
            root_child2_child0.Style.Height = 10;
            root_child2.InsertChild(root_child2_child0);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root_child3.Style.Height = 50;
            root.InsertChild(3, root_child3);
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

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(100, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(60, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

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

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(100, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(60, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_multiline_column()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 20;
            root_child1_child0.Style.Height = 20;
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 40;
            root_child2.Style.Height = 70;
            root.InsertChild(2, root_child2);

            YGNode root_child2_child0 = new YGNode(config);
            root_child2_child0.Style.Width = 10;
            root_child2_child0.Style.Height = 10;
            root_child2.InsertChild(root_child2_child0);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root_child3.Style.Height = 20;
            root.InsertChild(3, root_child3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(40, root_child2.Layout.Width);
            Assert.AreEqual(70, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(70, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(70, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(40, root_child2.Layout.Width);
            Assert.AreEqual(70, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(70, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_multiline_column2()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 20;
            root_child1_child0.Style.Height = 20;
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 40;
            root_child2.Style.Height = 70;
            root.InsertChild(2, root_child2);

            YGNode root_child2_child0 = new YGNode(config);
            root_child2_child0.Style.Width = 10;
            root_child2_child0.Style.Height = 10;
            root_child2.InsertChild(root_child2_child0);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root_child3.Style.Height = 20;
            root.InsertChild(3, root_child3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(40, root_child2.Layout.Width);
            Assert.AreEqual(70, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(70, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(70, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(40, root_child2.Layout.Width);
            Assert.AreEqual(70, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(70, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_multiline_row_and_column()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 50;
            root_child1_child0.Style.Height = 10;
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root_child2.Style.Height = 20;
            root.InsertChild(2, root_child2);

            YGNode root_child2_child0 = new YGNode(config);
            root_child2_child0.Style.Width = 50;
            root_child2_child0.Style.Height = 10;
            root_child2.InsertChild(root_child2_child0);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root_child3.Style.Height = 20;
            root.InsertChild(3, root_child3);
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
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(100, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(90, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

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
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(100, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(90, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);
        }

        [TestMethod]
        public void align_items_center_child_with_margin_bigger_than_parent()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = YGJustify.Center;
            root.Style.AlignItems = YGAlign.Center;
            root.Style.Width = 52;
            root.Style.Height = 52;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.AlignItems = YGAlign.Center;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Margin.Left = 10;
            root_child0_child0.Style.Margin.Right = 10;
            root_child0_child0.Style.Width = 52;
            root_child0_child0.Style.Height = 52;
            root_child0.InsertChild(root_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(52,  root_child0.Layout.Height);

            Assert.AreEqual(10, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(52, root_child0_child0.Layout.Width);
            Assert.AreEqual(52, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(52,  root_child0.Layout.Height);

            Assert.AreEqual(10, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(52, root_child0_child0.Layout.Width);
            Assert.AreEqual(52, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_items_flex_end_child_with_margin_bigger_than_parent()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = YGJustify.Center;
            root.Style.AlignItems = YGAlign.Center;
            root.Style.Width = 52;
            root.Style.Height = 52;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.AlignItems = YGAlign.FlexEnd;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Margin.Left = 10;
            root_child0_child0.Style.Margin.Right = 10;
            root_child0_child0.Style.Width = 52;
            root_child0_child0.Style.Height = 52;
            root_child0.InsertChild(root_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(52,  root_child0.Layout.Height);

            Assert.AreEqual(10, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(52, root_child0_child0.Layout.Width);
            Assert.AreEqual(52, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(52,  root_child0.Layout.Height);

            Assert.AreEqual(10, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(52, root_child0_child0.Layout.Width);
            Assert.AreEqual(52, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_items_center_child_without_margin_bigger_than_parent()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = YGJustify.Center;
            root.Style.AlignItems = YGAlign.Center;
            root.Style.Width = 52;
            root.Style.Height = 52;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.AlignItems = YGAlign.Center;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Width = 72;
            root_child0_child0.Style.Height = 72;
            root_child0.InsertChild(root_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_items_flex_end_child_without_margin_bigger_than_parent()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = YGJustify.Center;
            root.Style.AlignItems = YGAlign.Center;
            root.Style.Width = 52;
            root.Style.Height = 52;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.AlignItems = YGAlign.FlexEnd;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Width = 72;
            root_child0_child0.Style.Height = 72;
            root_child0.InsertChild(root_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_center_should_size_based_on_content()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.Center;
            root.Style.Margin.Top = 20;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.JustifyContent = YGJustify.Center;
            root_child0.Style.FlexShrink = 1;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexGrow = 1;
            root_child0_child0.Style.FlexShrink = 1;
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.Width = 20;
            root_child0_child0_child0.Style.Height = 20;
            root_child0_child0.InsertChild(root_child0_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(20,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(40, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(20,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(40, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_strech_should_size_based_on_parent()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Margin.Top = 20;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.JustifyContent = YGJustify.Center;
            root_child0.Style.FlexShrink = 1;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexGrow = 1;
            root_child0_child0.Style.FlexShrink = 1;
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.Width = 20;
            root_child0_child0_child0.Style.Height = 20;
            root_child0_child0.InsertChild(root_child0_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(20,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(20,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(20,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(20,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(20,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(20,  root_child0_child0.Layout.Height);

            Assert.AreEqual(80, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_flex_start_with_shrinking_children()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 500;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.AlignItems = YGAlign.FlexStart;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexGrow = 1;
            root_child0_child0.Style.FlexShrink = 1;
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.FlexGrow = 1;
            root_child0_child0_child0.Style.FlexShrink = 1;
            root_child0_child0.InsertChild(root_child0_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(500, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_flex_start_with_stretching_children()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 500;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode(config);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexGrow = 1;
            root_child0_child0.Style.FlexShrink = 1;
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.FlexGrow = 1;
            root_child0_child0_child0.Style.FlexShrink = 1;
            root_child0_child0.InsertChild(root_child0_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_flex_start_with_shrinking_children_with_stretch()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 500;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.AlignItems = YGAlign.FlexStart;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexGrow = 1;
            root_child0_child0.Style.FlexShrink = 1;
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.FlexGrow = 1;
            root_child0_child0_child0.Style.FlexShrink = 1;
            root_child0_child0.InsertChild(root_child0_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(500, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Height);
        }
    }
}
