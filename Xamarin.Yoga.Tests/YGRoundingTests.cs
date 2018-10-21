using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGRoundingTests
    {
        [TestMethod]
        public void rounding_flex_basis_flex_grow_row_width_of_100()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(33,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(33,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(34,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(67,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(33,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(67,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(33,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(33,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(34,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(33,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_flex_basis_flex_grow_row_prime_number_width()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            root.StyleSetWidth(113);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1);
            root.InsertChild(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.StyleSetFlexGrow(1);
            root.InsertChild(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.StyleSetFlexGrow(1);
            root.InsertChild(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(113, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(23,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(23,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(22,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(45,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(23,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(68,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(22,  root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(90,  root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(23,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(113, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(23,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(68,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(22,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(45,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(23,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(23,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(22,  root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(23,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);
        }

        [TestMethod]
        public void rounding_flex_basis_flex_shrink_row()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            root.StyleSetWidth(101);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexShrink(1);
            root_child0.StyleSetFlexBasis(100);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexBasis(25);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexBasis(25);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(101, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(51,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(51,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(25,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(76,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(25,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(101, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(51,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(25,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(25,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(25,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_flex_basis_overrides_main_size()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetWidth(100);
            root.StyleSetHeight(113);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasis(50);
            root_child0.StyleSetHeight(20);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1);
            root_child2.StyleSetHeight(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_total_fractial()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetWidth(87.4f);
            root.StyleSetHeight(113.4f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(0.7f);
            root_child0.StyleSetFlexBasis(50.3f);
            root_child0.StyleSetHeight(20.3f);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1.6f);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1.1f);
            root_child2.StyleSetHeight(10.7f);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(87,  root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(87, root_child0.Layout.Width);
            Assert.AreEqual(59, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(59, root_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(89, root_child2.Layout.Position.Top);
            Assert.AreEqual(87, root_child2.Layout.Width);
            Assert.AreEqual(24, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(87,  root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(87, root_child0.Layout.Width);
            Assert.AreEqual(59, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(59, root_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(89, root_child2.Layout.Position.Top);
            Assert.AreEqual(87, root_child2.Layout.Width);
            Assert.AreEqual(24, root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_total_fractial_nested()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetWidth(87.4f);
            root.StyleSetHeight(113.4f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(0.7f);
            root_child0.StyleSetFlexBasis(50.3f);
            root_child0.StyleSetHeight(20.3f);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetFlexGrow(1);
            root_child0_child0.StyleSetFlexBasis(0.3f);
            root_child0_child0.StyleSetPosition(YGEdge.Bottom, 13.3f);
            root_child0_child0.StyleSetHeight(9.9f);
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child1 = new YGNode(config);
            root_child0_child1.StyleSetFlexGrow(4);
            root_child0_child1.StyleSetFlexBasis(0.3f);
            root_child0_child1.StyleSetPosition(YGEdge.Top, 13.3f);
            root_child0_child1.StyleSetHeight(1.1f);
            root_child0.InsertChild(1, root_child0_child1);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1.6f);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1.1f);
            root_child2.StyleSetHeight(10.7f);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(87,  root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(87, root_child0.Layout.Width);
            Assert.AreEqual(59, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(-13, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(87,  root_child0_child0.Layout.Width);
            Assert.AreEqual(12,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(25, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child0_child1.Layout.Width);
            Assert.AreEqual(47, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(59, root_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(89, root_child2.Layout.Position.Top);
            Assert.AreEqual(87, root_child2.Layout.Width);
            Assert.AreEqual(24, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(87,  root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(87, root_child0.Layout.Width);
            Assert.AreEqual(59, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(-13, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(87,  root_child0_child0.Layout.Width);
            Assert.AreEqual(12,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(25, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child0_child1.Layout.Width);
            Assert.AreEqual(47, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(59, root_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(89, root_child2.Layout.Position.Top);
            Assert.AreEqual(87, root_child2.Layout.Width);
            Assert.AreEqual(24, root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_fractial_input_1()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetWidth(100);
            root.StyleSetHeight(113.4f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasis(50);
            root_child0.StyleSetHeight(20);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1);
            root_child2.StyleSetHeight(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_fractial_input_2()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetWidth(100);
            root.StyleSetHeight(113.6f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasis(50);
            root_child0.StyleSetHeight(20);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1);
            root_child2.StyleSetHeight(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(114, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(65,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(65,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(24,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(25,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(114, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(65,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(65,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(24,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(25,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_fractial_input_3()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetPosition(YGEdge.Top, 0.3f);
            root.StyleSetWidth(100);
            root.StyleSetHeight(113.4f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasis(50);
            root_child0.StyleSetHeight(20);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1);
            root_child2.StyleSetHeight(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(114, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(65,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(24,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(25,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(114, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(65,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(24,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(25,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_fractial_input_4()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetPosition(YGEdge.Top, 0.7f);
            root.StyleSetWidth(100);
            root.StyleSetHeight(113.4f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasis(50);
            root_child0.StyleSetHeight(20);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1);
            root_child2.StyleSetHeight(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(1,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(1,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_inner_node_controversy_horizontal()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            root.StyleSetWidth(320);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetHeight(10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.StyleSetFlexGrow(1);
            root_child1_child0.StyleSetHeight(10);
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1);
            root_child2.StyleSetHeight(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(320, root.Layout.Width);
            Assert.AreEqual(10,  root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(107, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(107, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(106, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(106, root_child1_child0.Layout.Width);
            Assert.AreEqual(10,  root_child1_child0.Layout.Height);

            Assert.AreEqual(213, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(107, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(320, root.Layout.Width);
            Assert.AreEqual(10,  root.Layout.Height);

            Assert.AreEqual(213, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(107, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(107, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(106, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(106, root_child1_child0.Layout.Width);
            Assert.AreEqual(10,  root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(107, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_inner_node_controversy_vertical()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetHeight(320);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetWidth(10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetWidth(10);
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.StyleSetFlexGrow(1);
            root_child1_child0.StyleSetWidth(10);
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1);
            root_child2.StyleSetWidth(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(10,  root.Layout.Width);
            Assert.AreEqual(320, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(107, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(107, root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(106, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1_child0.Layout.Width);
            Assert.AreEqual(106, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(213, root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(107, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(10,  root.Layout.Width);
            Assert.AreEqual(320, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(107, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(107, root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(106, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1_child0.Layout.Width);
            Assert.AreEqual(106, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(213, root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(107, root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_inner_node_controversy_combined()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            root.StyleSetWidth(640);
            root.StyleSetHeight(320);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetHeightPercent(100);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetHeightPercent(100);
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.StyleSetFlexGrow(1);
            root_child1_child0.StyleSetWidthPercent( 100);
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child1_child1 = new YGNode(config);
            root_child1_child1.StyleSetFlexGrow(1);
            root_child1_child1.StyleSetWidthPercent( 100);
            root_child1.InsertChild(1, root_child1_child1);

            YGNode root_child1_child1_child0 = new YGNode(config);
            root_child1_child1_child0.StyleSetFlexGrow(1);
            root_child1_child1_child0.StyleSetWidthPercent(100);
            root_child1_child1.InsertChild(root_child1_child1_child0);

            YGNode root_child1_child2 = new YGNode(config);
            root_child1_child2.StyleSetFlexGrow(1);
            root_child1_child2.StyleSetWidthPercent(100);
            root_child1.InsertChild(2, root_child1_child2);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetFlexGrow(1);
            root_child2.StyleSetHeightPercent(100);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(640, root.Layout.Width);
            Assert.AreEqual(320, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(213, root_child0.Layout.Width);
            Assert.AreEqual(320, root_child0.Layout.Height);

            Assert.AreEqual(213, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(214, root_child1.Layout.Width);
            Assert.AreEqual(320, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child0.Layout.Width);
            Assert.AreEqual(107, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(107, root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child1.Layout.Width);
            Assert.AreEqual(106, root_child1_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child1_child0.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child1_child0.Layout.Width);
            Assert.AreEqual(106, root_child1_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(213, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child2.Layout.Width);
            Assert.AreEqual(107, root_child1_child2.Layout.Height);

            Assert.AreEqual(427, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(213, root_child2.Layout.Width);
            Assert.AreEqual(320, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(640, root.Layout.Width);
            Assert.AreEqual(320, root.Layout.Height);

            Assert.AreEqual(427, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(213, root_child0.Layout.Width);
            Assert.AreEqual(320, root_child0.Layout.Height);

            Assert.AreEqual(213, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(214, root_child1.Layout.Width);
            Assert.AreEqual(320, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child0.Layout.Width);
            Assert.AreEqual(107, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(107, root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child1.Layout.Width);
            Assert.AreEqual(106, root_child1_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child1_child0.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child1_child0.Layout.Width);
            Assert.AreEqual(106, root_child1_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(213, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child2.Layout.Width);
            Assert.AreEqual(107, root_child1_child2.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(213, root_child2.Layout.Width);
            Assert.AreEqual(320, root_child2.Layout.Height);
        }
    }
}
