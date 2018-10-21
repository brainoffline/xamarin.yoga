using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGAlignContentTests
    {
        [TestMethod]
        public void align_content_flex_start()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 130;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 10;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 10;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root_child2.Style.Height = 10;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root_child3.Style.Height = 10;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root_child4.Style.Height = 10;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(80, root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(80, root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_flex_start_without_height_on_children()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 10;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root_child3.Style.Height = 10;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(0,  root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(0,  root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_flex_start_with_flex()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 100;
            root.Style.Height = 120;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 0.Percent();
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.FlexBasis = 0.Percent();
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 10;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.FlexGrow = 1;
            root_child3.Style.FlexShrink = 1;
            root_child3.Style.FlexBasis = 0.Percent();
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(40, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(80, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(80, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(120, root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(0,   root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(40, root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(80, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(80, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(50,  root_child4.Layout.Position.Left);
            Assert.AreEqual(120, root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(0,   root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_flex_end()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignContent = YGAlign.FlexEnd;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 10;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 10;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root_child2.Style.Height = 10;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root_child3.Style.Height = 10;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root_child4.Style.Height = 10;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(40, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(40, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(0,  root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(0,  root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(0,   root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(0,   root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(0,   root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(0,   root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_spacebetween()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.SpaceBetween;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 130;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 10;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 10;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root_child2.Style.Height = 10;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root_child3.Style.Height = 10;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root_child4.Style.Height = 10;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(45, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(45, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(80, root_child2.Layout.Position.Left);
            Assert.AreEqual(45, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(45, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(80, root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_spacearound()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.SpaceAround;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 140;
            root.Style.Height = 120;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 10;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 10;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root_child2.Style.Height = 10;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root_child3.Style.Height = 10;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root_child4.Style.Height = 10;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(140, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(15, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(55, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(55, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(95, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(140, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(15, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(90, root_child2.Layout.Position.Left);
            Assert.AreEqual(55, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(40, root_child3.Layout.Position.Left);
            Assert.AreEqual(55, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(90, root_child4.Layout.Position.Left);
            Assert.AreEqual(95, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_children()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexGrow = 1;
            root_child0_child0.Style.FlexShrink = 1;
            root_child0_child0.Style.FlexBasis = 0.Percent();
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_flex()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.FlexShrink = 1;
            root_child1.Style.FlexBasis = 0.Percent();
            root_child1.Style.Width = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.FlexGrow = 1;
            root_child3.Style.FlexShrink = 1;
            root_child3.Style.FlexBasis = 0.Percent();
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(50,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_flex_no_shrink()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.FlexShrink = 1;
            root_child1.Style.FlexBasis = 0.Percent();
            root_child1.Style.Width = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.FlexGrow = 1;
            root_child3.Style.FlexBasis = 0.Percent();
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(50,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_margin()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Margin = new Edges(10, 10, 10, 10);
            root_child1.Style.Width = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Margin = new Edges(10, 10, 10, 10);
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            Assert.AreEqual(60, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(40, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(40, root_child2.Layout.Height);

            Assert.AreEqual(60, root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(80, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(20, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(40,  root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(40,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(40,  root_child2.Layout.Height);

            Assert.AreEqual(40, root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(80,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(20,  root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_padding()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Padding = new Edges(10, 10, 10, 10);
            root_child1.Style.Width = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Padding = new Edges(10, 10, 10, 10);
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_single_row()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_fixed_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 60;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(60, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(80,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(80, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(80, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(20, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(60, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(80, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(80,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(20,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(80, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(20, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_max_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.MaxHeight = 20;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_min_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.MinHeight = 80;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(90, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(90, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(90,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(90, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(90,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(90, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(90, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(90,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(10,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_column()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = YGWrap.Wrap;
            root.Style.Width = 100;
            root.Style.Height = 150;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexGrow = 1;
            root_child0_child0.Style.FlexShrink = 1;
            root_child0_child0.Style.FlexBasis = 0.Percent();
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.FlexShrink = 1;
            root_child1.Style.FlexBasis = 0.Percent();
            root_child1.Style.Height = 50;
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Height = 50;
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Height = 50;
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Height = 50;
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(150, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(0,   root_child3.Layout.Position.Left);
            Assert.AreEqual(100, root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(150, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(50,  root_child3.Layout.Position.Left);
            Assert.AreEqual(100, root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_is_not_overriding_align_items()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignContent = YGAlign.Stretch;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexDirection = YGFlexDirection.Row;
            root_child0.Style.AlignContent = YGAlign.Stretch;
            root_child0.Style.AlignItems = YGAlign.Center;
            root_child0.Style.Width = 100;
            root_child0.Style.Height = 100;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.AlignContent = YGAlign.Stretch;
            root_child0_child0.Style.Width = 10;
            root_child0_child0.Style.Height = 10;
            root_child0.InsertChild(root_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(45, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0_child0.Layout.Width);
            Assert.AreEqual(10, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(90, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(45, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0_child0.Layout.Width);
            Assert.AreEqual(10, root_child0_child0.Layout.Height);
        }
    }
}
