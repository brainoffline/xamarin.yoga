using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YogaConst;


    [TestClass]
    public class YGFlexWrapTests
    {
        [TestMethod]
        public void wrap_column()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexWrap = WrapType.Wrap;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30;
            root_child0.Style.Height = 30;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 30;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 30;
            root_child2.Style.Height = 30;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 30;
            root_child3.Style.Height = 30;
            root.Children.Insert(3, root_child3);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(60,  root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(60, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,  root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(60,  root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(30, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(30, root_child2.Layout.Position.Left);
            Assert.AreEqual(60, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,  root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);
        }

        [TestMethod]
        public void wrap_row()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.FlexWrap = WrapType.Wrap;
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30;
            root_child0.Style.Height = 30;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 30;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 30;
            root_child2.Style.Height = 30;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 30;
            root_child3.Style.Height = 30;
            root.Children.Insert(3, root_child3);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);
        }

        [TestMethod]
        public void wrap_row_align_items_flex_end()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignItems = YGAlign.FlexEnd;
            root.Style.FlexWrap = WrapType.Wrap;
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30;
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 30;
            root_child2.Style.Height = 30;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 30;
            root_child3.Style.Height = 30;
            root.Children.Insert(3, root_child3);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);
        }

        [TestMethod]
        public void wrap_row_align_items_center()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignItems = YGAlign.Center;
            root.Style.FlexWrap = WrapType.Wrap;
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30;
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 30;
            root_child2.Style.Height = 30;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 30;
            root_child3.Style.Height = 30;
            root.Children.Insert(3, root_child3);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);
        }

        [TestMethod]
        public void flex_wrap_children_with_min_main_overriding_flex_basis()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.FlexWrap = WrapType.Wrap;
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexBasis = 50;
            root_child0.Style.MinWidth = 55;
            root_child0.Style.Height = 50;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexBasis = 50;
            root_child1.Style.MinWidth = 55;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(55, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(55, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(55, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(45, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(55, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_wrap_wrap_to_child_height()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0.Style.AlignItems = YGAlign.FlexStart;
            root_child0.Style.FlexWrap = WrapType.Wrap;
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Width = 100;
            root_child0.Children.Add(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.Width = 100;
            root_child0_child0_child0.Style.Height = 100;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 100;
            root_child1.Style.Height = 100;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(100, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(100, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_wrap_align_stretch_fits_one_row()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.FlexWrap = WrapType.Wrap;
            root.Style.Width = 150;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void wrap_reverse_row_align_content_flex_start()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.FlexWrap = WrapType.WrapReverse;
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30;
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 30;
            root_child2.Style.Height = 30;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 30;
            root_child3.Style.Height = 40;
            root.Children.Insert(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 30;
            root_child4.Style.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_row_align_content_center()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignContent = YGAlign.Center;
            root.Style.FlexWrap = WrapType.WrapReverse;
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30;
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 30;
            root_child2.Style.Height = 30;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 30;
            root_child3.Style.Height = 40;
            root.Children.Insert(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 30;
            root_child4.Style.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_row_single_line_different_size()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.FlexWrap = WrapType.WrapReverse;
            root.Style.Width = 300;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30;
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 30;
            root_child2.Style.Height = 30;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 30;
            root_child3.Style.Height = 40;
            root.Children.Insert(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 30;
            root_child4.Style.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(300, root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(40, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(90, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(120, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(30,  root_child4.Layout.Width);
            Assert.AreEqual(50,  root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(300, root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(270, root_child0.Layout.Position.Left);
            Assert.AreEqual(40,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(240, root_child1.Layout.Position.Left);
            Assert.AreEqual(30,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30,  root_child1.Layout.Width);
            Assert.AreEqual(20,  root_child1.Layout.Height);

            Assert.AreEqual(210, root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30,  root_child2.Layout.Width);
            Assert.AreEqual(30,  root_child2.Layout.Height);

            Assert.AreEqual(180, root_child3.Layout.Position.Left);
            Assert.AreEqual(10,  root_child3.Layout.Position.Top);
            Assert.AreEqual(30,  root_child3.Layout.Width);
            Assert.AreEqual(40,  root_child3.Layout.Height);

            Assert.AreEqual(150, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(30,  root_child4.Layout.Width);
            Assert.AreEqual(50,  root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_row_align_content_stretch()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.FlexWrap = WrapType.WrapReverse;
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30;
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 30;
            root_child2.Style.Height = 30;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 30;
            root_child3.Style.Height = 40;
            root.Children.Insert(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 30;
            root_child4.Style.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_row_align_content_space_around()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignContent = YGAlign.SpaceAround;
            root.Style.FlexWrap = WrapType.WrapReverse;
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30;
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 30;
            root_child2.Style.Height = 30;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 30;
            root_child3.Style.Height = 40;
            root.Children.Insert(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 30;
            root_child4.Style.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_column_fixed_size()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.Center;
            root.Style.FlexWrap = WrapType.WrapReverse;
            root.Style.Width = 200;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30;
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 30;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 30;
            root_child2.Style.Height = 30;
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode(config);
            root_child3.Style.Width = 30;
            root_child3.Style.Height = 40;
            root.Children.Insert(3, root_child3);

            YGNode root_child4 = new YGNode(config);
            root_child4.Style.Width = 30;
            root_child4.Style.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(170, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(30,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(170, root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30,  root_child1.Layout.Width);
            Assert.AreEqual(20,  root_child1.Layout.Height);

            Assert.AreEqual(170, root_child2.Layout.Position.Left);
            Assert.AreEqual(30,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30,  root_child2.Layout.Width);
            Assert.AreEqual(30,  root_child2.Layout.Height);

            Assert.AreEqual(170, root_child3.Layout.Position.Left);
            Assert.AreEqual(60,  root_child3.Layout.Position.Top);
            Assert.AreEqual(30,  root_child3.Layout.Width);
            Assert.AreEqual(40,  root_child3.Layout.Height);

            Assert.AreEqual(140, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(30,  root_child4.Layout.Width);
            Assert.AreEqual(50,  root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(30, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(60, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrapped_row_within_align_items_center()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.Center;
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0.Style.FlexWrap = WrapType.Wrap;
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Width = 150;
            root_child0_child0.Style.Height = 80;
            root_child0.Children.Add(root_child0_child0);

            YGNode root_child0_child1 = new YGNode(config);
            root_child0_child1.Style.Width = 80;
            root_child0_child1.Style.Height = 80;
            root_child0.Children.Add(root_child0_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(120, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80,  root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0_child1.Layout.Width);
            Assert.AreEqual(80,  root_child0_child1.Layout.Height);
        }

        [TestMethod]
        public void wrapped_row_within_align_items_flex_start()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0.Style.FlexWrap = WrapType.Wrap;
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Width = 150;
            root_child0_child0.Style.Height = 80;
            root_child0.Children.Add(root_child0_child0);

            YGNode root_child0_child1 = new YGNode(config);
            root_child0_child1.Style.Width = 80;
            root_child0_child1.Style.Height = 80;
            root_child0.Children.Insert(1, root_child0_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(120, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80,  root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0_child1.Layout.Width);
            Assert.AreEqual(80,  root_child0_child1.Layout.Height);
        }

        [TestMethod]
        public void wrapped_row_within_align_items_flex_end()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignItems = YGAlign.FlexEnd;
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0.Style.FlexWrap = WrapType.Wrap;
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Width = 150;
            root_child0_child0.Style.Height = 80;
            root_child0.Children.Add(root_child0_child0);

            YGNode root_child0_child1 = new YGNode(config);
            root_child0_child1.Style.Width = 80;
            root_child0_child1.Style.Height = 80;
            root_child0.Children.Insert(1, root_child0_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(120, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80,  root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0_child1.Layout.Width);
            Assert.AreEqual(80,  root_child0_child1.Layout.Height);
        }

        [TestMethod]
        public void wrapped_column_max_height()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.AlignContent = YGAlign.Center;
            root.Style.AlignItems = YGAlign.Center;
            root.Style.FlexWrap = WrapType.Wrap;
            root.Style.Width = 700;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 100;
            root_child0.Style.Height = 500;
            root_child0.Style.MaxHeight = 200;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Margin = new Edges(20, 20, 20, 20);
            root_child1.Style.Width = 200;
            root_child1.Style.Height = 200;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 100;
            root_child2.Style.Height = 100;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(250, root_child0.Layout.Position.Left);
            Assert.AreEqual(30,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(200, root_child1.Layout.Position.Left);
            Assert.AreEqual(250, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(200, root_child1.Layout.Height);

            Assert.AreEqual(420, root_child2.Layout.Position.Left);
            Assert.AreEqual(200, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(350, root_child0.Layout.Position.Left);
            Assert.AreEqual(30,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(300, root_child1.Layout.Position.Left);
            Assert.AreEqual(250, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(200, root_child1.Layout.Height);

            Assert.AreEqual(180, root_child2.Layout.Position.Left);
            Assert.AreEqual(200, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void wrapped_column_max_height_flex()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.AlignContent = YGAlign.Center;
            root.Style.AlignItems = YGAlign.Center;
            root.Style.FlexWrap = WrapType.Wrap;
            root.Style.Width = 700;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexShrink = 1;
            root_child0.Style.FlexBasis = 0.Percent();
            root_child0.Style.Width = 100;
            root_child0.Style.Height = 500;
            root_child0.Style.MaxHeight = 200;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.FlexShrink = 1;
            root_child1.Style.FlexBasis = 0.Percent();
            root_child1.Style.Margin = new Edges(20, 20, 20, 20);
            root_child1.Style.Width = 200;
            root_child1.Style.Width = 200;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 100;
            root_child2.Style.Width = 100;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(300, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(180, root_child0.Layout.Height);

            Assert.AreEqual(250, root_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(180, root_child1.Layout.Height);

            Assert.AreEqual(300, root_child2.Layout.Position.Left);
            Assert.AreEqual(400, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(300, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(180, root_child0.Layout.Height);

            Assert.AreEqual(250, root_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(180, root_child1.Layout.Height);

            Assert.AreEqual(300, root_child2.Layout.Position.Left);
            Assert.AreEqual(400, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void wrap_nodes_with_content_sizing_overflowing_margin()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 500;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0.Style.FlexWrap = WrapType.Wrap;
            root_child0.Style.Width = 85;
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0.Children.Add(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.Width = 40;
            root_child0_child0_child0.Style.Height = 40;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            YGNode root_child0_child1 = new YGNode(config);
            root_child0_child1.Style.Margin.Right = 10;
            root_child0.Children.Insert(1, root_child0_child1);

            YGNode root_child0_child1_child0 = new YGNode(config);
            root_child0_child1_child0.Style.Width = 40;
            root_child0_child1_child0.Style.Height = 40;
            root_child0_child1.Children.Add(root_child0_child1_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(85, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(415, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(85,  root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);

            Assert.AreEqual(45, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(35, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void wrap_nodes_with_content_sizing_margin_cross()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 500;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0.Style.FlexWrap = WrapType.Wrap;
            root_child0.Style.Width = 70;
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0.Children.Add(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.Width = 40;
            root_child0_child0_child0.Style.Height = 40;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            YGNode root_child0_child1 = new YGNode(config);
            root_child0_child1.Style.Margin.Top = 10;
            root_child0.Children.Insert(1, root_child0_child1);

            YGNode root_child0_child1_child0 = new YGNode(config);
            root_child0_child1_child0.Style.Width = 40;
            root_child0_child1_child0.Style.Height = 40;
            root_child0_child1.Children.Add(root_child0_child1_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(70, root_child0.Layout.Width);
            Assert.AreEqual(90, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(430, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(70,  root_child0.Layout.Width);
            Assert.AreEqual(90,  root_child0.Layout.Height);

            Assert.AreEqual(30, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(30, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);
        }
    }
}
