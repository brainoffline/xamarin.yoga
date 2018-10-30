using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    
    using static YogaConst;


    [TestClass]
    public class YGAspectRatioTests
    {
        private static SizeF _measure(YogaNode node,
            float                             width,
            MeasureMode                     widthMode,
            float                             height,
            MeasureMode                     heightMode)
        {
            return new SizeF(
                width = widthMode   == MeasureMode.Exactly ? width : 50,
                height = heightMode == MeasureMode.Exactly ? height : 50
            );
        }

        [TestMethod]
        public void aspect_ratio_cross_defined()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_main_defined()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 50;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_both_dimensions_defined_row()
        {
            YogaNode root = new YogaNode();
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 100;
            root_child0.Style.Height = 50;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_both_dimensions_defined_column()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 100;
            root_child0.Style.Height = 50;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_align_stretch()
        {
            YogaNode root = new YogaNode();
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_flex_grow()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 50;
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_flex_shrink()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 150;
            root_child0.Style.FlexShrink = 1;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_flex_shrink_2()
        {
            YogaNode root = new YogaNode();
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 100.Percent();
            root_child0.Style.FlexShrink = 1;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            YogaNode root_child1 = new YogaNode();
            root_child1.Style.Height = 100.Percent();
            root_child1.Style.FlexShrink = 1;
            root_child1.Style.AspectRatio = 1;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_basis()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.FlexBasis = 50;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_absolute_layout_width_defined()
        {
            YogaNode root = new YogaNode();
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.PositionType = PositionType.Absolute;
            root_child0.Style.Position.Left = 0;
            root_child0.Style.Position.Top =  0;
            root_child0.Style.Width = 50;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_absolute_layout_height_defined()
        {
            YogaNode root = new YogaNode();
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.PositionType = PositionType.Absolute;
            root_child0.Style.Position.Left = 0;
            root_child0.Style.Position.Top =  0;
            root_child0.Style.Height = 50;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_max_cross_defined()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 50;
            root_child0.Style.MaxWidth = 40;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_max_main_defined()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.MaxHeight = 40;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_min_cross_defined()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 30;
            root_child0.Style.MinWidth = 40;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_min_main_defined()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 30;
            root_child0.Style.MinHeight = 40;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_double_cross()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 50;
            root_child0.Style.AspectRatio = 2;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_half_cross()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 100;
            root_child0.Style.AspectRatio = 0.5f;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_double_main()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.AspectRatio = 0.5f;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_half_main()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 100;
            root_child0.Style.AspectRatio = 2;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_measure_func()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.MeasureFunc = _measure;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_height_flex_grow_row()
        {
            YogaNode root = new YogaNode();
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 200;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_height_flex_grow_column()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 200;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_height_as_flex_basis()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 200;
            root.Style.Height = 200;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 50;
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            YogaNode root_child1 = new YogaNode();
            root_child1.Style.Height = 100;
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.AspectRatio = 1;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(75, root_child0.Layout.Width);
            Assert.AreEqual(75, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(125, root_child1.Layout.Width);
            Assert.AreEqual(125, root_child1.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_as_flex_basis()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 200;
            root.Style.Height = 200;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            YogaNode root_child1 = new YogaNode();
            root_child1.Style.Width = 100;
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.AspectRatio = 1;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(75, root_child0.Layout.Width);
            Assert.AreEqual(75, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(125, root_child1.Layout.Width);
            Assert.AreEqual(125, root_child1.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_overrides_flex_grow_row()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.AspectRatio = 0.5f;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_overrides_flex_grow_column()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 50;
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.AspectRatio = 2;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_left_right_absolute()
        {
            YogaNode root = new YogaNode();
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.PositionType = PositionType.Absolute;
            root_child0.Style.Position.Left = 10;
            root_child0.Style.Position.Top = 10;
            root_child0.Style.Position.Right = 10;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_top_bottom_absolute()
        {
            YogaNode root = new YogaNode();
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.PositionType = PositionType.Absolute;
            root_child0.Style.Position.Left = 10;
            root_child0.Style.Position.Top = 10;
            root_child0.Style.Position.Bottom = 10;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_overrides_align_stretch_row()
        {
            YogaNode root = new YogaNode();
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_height_overrides_align_stretch_column()
        {
            YogaNode root = new YogaNode();
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 50;
            root_child0.Style.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_allow_child_overflow_parent_size()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 50;
            root_child0.Style.AspectRatio = 4;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_defined_main_with_margin()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.Center;
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Height = 50;
            root_child0.Style.AspectRatio = 1;
            root_child0.Style.Margin.Left =  10;
            root_child0.Style.Margin.Right = 10;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_defined_cross_with_margin()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.Center;
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.AspectRatio = 1;
            root_child0.Style.Margin.Left =  10;
            root_child0.Style.Margin.Right = 10;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_defined_cross_with_main_margin()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.Center;
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.AspectRatio = 1;
            root_child0.Style.Margin.Top =    10;
            root_child0.Style.Margin.Bottom = 10;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_should_prefer_explicit_height()
        {
            YogaConfig config = new YogaConfig {UseWebDefaults = true};

            YogaNode root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Column;

            YogaNode root_child0 = new YogaNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Column;
            root.Children.Add(root_child0);

            YogaNode root_child0_child0 = new YogaNode(config);
            root_child0_child0.Style.FlexDirection = FlexDirectionType.Column;
            root_child0_child0.Style.Height = 100;
            root_child0_child0.Style.AspectRatio = 2;
            root_child0.Children.Add(root_child0_child0);

            root.Calc.CalculateLayout(100, 200, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(200, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_should_prefer_explicit_width()
        {
            YogaConfig config = new YogaConfig {UseWebDefaults = true};

            YogaNode root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;

            YogaNode root_child0 = new YogaNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Row;
            root.Children.Add(root_child0);

            YogaNode root_child0_child0 = new YogaNode(config);
            root_child0_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0_child0.Style.Width = 100;
            root_child0_child0.Style.AspectRatio = 0.5f;
            root_child0.Children.Add(root_child0_child0);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(200, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_should_prefer_flexed_dimension()
        {
            YogaConfig config = new YogaConfig {UseWebDefaults = true};

            YogaNode root = new YogaNode(config);

            YogaNode root_child0 = new YogaNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Column;
            root_child0.Style.AspectRatio = 2;
            root_child0.Style.FlexGrow = 1;
            root.Children.Add(root_child0);

            YogaNode root_child0_child0 = new YogaNode(config);
            root_child0_child0.Style.AspectRatio = 4;
            root_child0_child0.Style.FlexGrow = 1;
            root_child0.Children.Add(root_child0_child0);

            root.Calc.CalculateLayout(100, 100, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(200, root_child0_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0_child0.Layout.Height);
        }
    }
}
