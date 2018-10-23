using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YogaConst;


    [TestClass]
    public class YGRoundingTests
    {
        [TestMethod]
        public void rounding_flex_basis_flex_grow_row_width_of_100()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 113;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.FlexGrow = 1;
            root.Children.Insert(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.FlexGrow = 1;
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 101;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexShrink = 1;
            root_child0.Style.FlexBasis = 100;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexBasis = 25;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexBasis = 25;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 113;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 87.4f;
            root.Style.Height = 113.4f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 0.7f;
            root_child0.Style.FlexBasis = 50.3f;
            root_child0.Style.Height = 20.3f;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1.6f;
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1.1f;
            root_child2.Style.Height = 10.7f;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 87.4f;
            root.Style.Height = 113.4f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 0.7f;
            root_child0.Style.FlexBasis = 50.3f;
            root_child0.Style.Height = 20.3f;
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexGrow = 1;
            root_child0_child0.Style.FlexBasis = 0.3f;
            root_child0_child0.Style.Position.Bottom = 13.3f;
            root_child0_child0.Style.Height = 9.9f;
            root_child0.Children.Add(root_child0_child0);

            YGNode root_child0_child1 = new YGNode(config);
            root_child0_child1.Style.FlexGrow = 4;
            root_child0_child1.Style.FlexBasis = 0.3f;
            root_child0_child1.Style.Position.Top = 13.3f;
            root_child0_child1.Style.Height = 1.1f;
            root_child0.Children.Insert(1, root_child0_child1);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1.6f;
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1.1f;
            root_child2.Style.Height = 10.7f;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 113.4f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 113.6f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Position.Top = 0.3f;
            root.Style.Width = 100;
            root.Style.Height = 113.4f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Position.Top = 0.7f;
            root.Style.Width = 100;
            root.Style.Height = 113.4f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 320;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.FlexGrow = 1;
            root_child1_child0.Style.Height = 10;
            root_child1.Children.Add(root_child1_child0);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Height = 320;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Width = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.Width = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.FlexGrow = 1;
            root_child1_child0.Style.Width = 10;
            root_child1.Children.Add(root_child1_child0);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root_child2.Style.Width = 10;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 640;
            root.Style.Height = 320;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Height = 100.Percent();
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.Height = 100.Percent();
            root.Children.Insert(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.FlexGrow = 1;
            root_child1_child0.Style.Width = 100.Percent();
            root_child1.Children.Add(root_child1_child0);

            YGNode root_child1_child1 = new YGNode(config);
            root_child1_child1.Style.FlexGrow = 1;
            root_child1_child1.Style.Width = 100.Percent();
            root_child1.Children.Insert(1, root_child1_child1);

            YGNode root_child1_child1_child0 = new YGNode(config);
            root_child1_child1_child0.Style.FlexGrow = 1;
            root_child1_child1_child0.Style.Width = 100.Percent();
            root_child1_child1.Children.Add(root_child1_child1_child0);

            YGNode root_child1_child2 = new YGNode(config);
            root_child1_child2.Style.FlexGrow = 1;
            root_child1_child2.Style.Width = 100.Percent();
            root_child1.Children.Insert(2, root_child1_child2);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root_child2.Style.Height = 100.Percent();
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
