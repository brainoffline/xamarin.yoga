﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class MarginTests
    {
        [TestMethod]
        public void margin_and_flex_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 100;
            root.Style.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.FlexGrow      = 1;
            root_child0.Style.Margin.Top    = 10;
            root_child0.Style.Margin.Bottom = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_and_flex_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width         = 100;
            root.Style.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.FlexGrow     = 1;
            root_child0.Style.Margin.Start = 10;
            root_child0.Style.Margin.End   = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_and_stretch_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 100;
            root.Style.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.FlexGrow     = 1;
            root_child0.Style.Margin.Start = 10;
            root_child0.Style.Margin.End   = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_and_stretch_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width         = 100;
            root.Style.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.FlexGrow      = 1;
            root_child0.Style.Margin.Top    = 10;
            root_child0.Style.Margin.Bottom = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_bottom()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.AlignItems = AlignType.Center;
            root.Style.Width      = 200;
            root.Style.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Bottom = Value.Auto;
            root_child0.Style.Width         = 50;
            root_child0.Style.Height        = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_bottom_and_top()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.AlignItems = AlignType.Center;
            root.Style.Width      = 200;
            root.Style.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Top    = Value.Auto;
            root_child0.Style.Margin.Bottom = Value.Auto;
            root_child0.Style.Width         = 50;
            root_child0.Style.Height        = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_bottom_and_top_justify_center()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Width          = 200;
            root.Style.Height         = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Top    = Value.Auto;
            root_child0.Style.Margin.Bottom = Value.Auto;
            root_child0.Style.Width         = 50;
            root_child0.Style.Height        = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(50,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.AlignItems = AlignType.Center;
            root.Style.Width      = 200;
            root.Style.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Left = Value.Auto;
            root_child0.Style.Width       = 50;
            root_child0.Style.Height      = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_and_right()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 200;
            root.Style.Height = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Left  = Value.Auto;
            root_child0.Style.Margin.Right = Value.Auto;
            root_child0.Style.Width        = 50;
            root_child0.Style.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(50,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_and_right_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignItems    = AlignType.Center;
            root.Style.Width         = 200;
            root.Style.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Left  = Value.Auto;
            root_child0.Style.Margin.Right = Value.Auto;
            root_child0.Style.Width        = 50;
            root_child0.Style.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(75, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(75,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(75, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_and_right_column_and_center()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.AlignItems = AlignType.Center;
            root.Style.Width      = 200;
            root.Style.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Left  = Value.Auto;
            root_child0.Style.Margin.Right = Value.Auto;
            root_child0.Style.Width        = 50;
            root_child0.Style.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_and_right_strech()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width         = 200;
            root.Style.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Left  = Value.Auto;
            root_child0.Style.Margin.Right = Value.Auto;
            root_child0.Style.Width        = 50;
            root_child0.Style.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_child_bigger_than_parent()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Width          = 52;
            root.Style.Height         = 52;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Left = Value.Auto;
            root_child0.Style.Width       = 72;
            root_child0.Style.Height      = 72;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-20, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_fix_right_child_bigger_than_parent()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Width          = 52;
            root.Style.Height         = 52;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Left  = Value.Auto;
            root_child0.Style.Margin.Right = 10;
            root_child0.Style.Width        = 72;
            root_child0.Style.Height       = 72;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-30, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_right_child_bigger_than_parent()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Width          = 52;
            root.Style.Height         = 52;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Left  = Value.Auto;
            root_child0.Style.Margin.Right = Value.Auto;
            root_child0.Style.Width        = 72;
            root_child0.Style.Height       = 72;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-20, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_stretching_child()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.AlignItems = AlignType.Center;
            root.Style.Width      = 200;
            root.Style.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.FlexGrow    = 1;
            root_child0.Style.FlexShrink  = 1;
            root_child0.Style.FlexBasis   = 0.Percent();
            root_child0.Style.Margin.Left = Value.Auto;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(200, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0.Layout.Width);
            Assert.AreEqual(150, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(200, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0.Layout.Width);
            Assert.AreEqual(150, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_mutiple_children_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.AlignItems = AlignType.Center;
            root.Style.Width      = 200;
            root.Style.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Top = Value.Auto;
            root_child0.Style.Width      = 50;
            root_child0.Style.Height     = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Margin.Top = Value.Auto;
            root_child1.Style.Width      = 50;
            root_child1.Style.Height     = 50;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Style.Width  = 50;
            root_child2.Style.Height = 50;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(25, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(100, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            Assert.AreEqual(75,  root_child2.Layout.Position.Left);
            Assert.AreEqual(150, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(25, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(100, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            Assert.AreEqual(75,  root_child2.Layout.Position.Left);
            Assert.AreEqual(150, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_mutiple_children_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignItems    = AlignType.Center;
            root.Style.Width         = 200;
            root.Style.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Right = Value.Auto;
            root_child0.Style.Width        = 50;
            root_child0.Style.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Margin.Right = Value.Auto;
            root_child1.Style.Width        = 50;
            root_child1.Style.Height       = 50;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Style.Width  = 50;
            root_child2.Style.Height = 50;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(75, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(75, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(150, root_child2.Layout.Position.Left);
            Assert.AreEqual(75,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(125, root_child0.Layout.Position.Left);
            Assert.AreEqual(75,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(75, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(75, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_right()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.AlignItems = AlignType.Center;
            root.Style.Width      = 200;
            root.Style.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Right = Value.Auto;
            root_child0.Style.Width        = 50;
            root_child0.Style.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_start_and_end()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 200;
            root.Style.Height = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Start = Value.Auto;
            root_child0.Style.Margin.End   = Value.Auto;
            root_child0.Style.Width        = 50;
            root_child0.Style.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(50,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_start_and_end_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignItems    = AlignType.Center;
            root.Style.Width         = 200;
            root.Style.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Start = Value.Auto;
            root_child0.Style.Margin.End   = Value.Auto;
            root_child0.Style.Width        = 50;
            root_child0.Style.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(75, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(75,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(75, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_top()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.AlignItems = AlignType.Center;
            root.Style.Width      = 200;
            root.Style.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Top = Value.Auto;
            root_child0.Style.Width      = 50;
            root_child0.Style.Height     = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75,  root_child0.Layout.Position.Left);
            Assert.AreEqual(100, root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75,  root_child0.Layout.Position.Left);
            Assert.AreEqual(100, root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_top_and_bottom_strech()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 200;
            root.Style.Height = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Top    = Value.Auto;
            root_child0.Style.Margin.Bottom = Value.Auto;
            root_child0.Style.Width         = 50;
            root_child0.Style.Height        = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(50,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_top_stretching_child()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.AlignItems = AlignType.Center;
            root.Style.Width      = 200;
            root.Style.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Style.FlexGrow   = 1;
            root_child0.Style.FlexShrink = 1;
            root_child0.Style.FlexBasis  = 0.Percent();
            root_child0.Style.Margin.Top = Value.Auto;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0.Layout.Width);
            Assert.AreEqual(150, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0.Layout.Width);
            Assert.AreEqual(150, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_bottom()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.JustifyContent = JustifyType.FlexEnd;
            root.Style.Width          = 100;
            root.Style.Height         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Bottom = 10;
            root_child0.Style.Height        = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(80,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(80,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_end()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.FlexDirection  = FlexDirectionType.Row;
            root.Style.JustifyContent = JustifyType.FlexEnd;
            root.Style.Width          = 100;
            root.Style.Height         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.End = 10;
            root_child0.Style.Width      = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_fix_left_auto_right_child_bigger_than_parent()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Width          = 52;
            root.Style.Height         = 52;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Left  = 10;
            root_child0.Style.Margin.Right = Value.Auto;
            root_child0.Style.Width        = 72;
            root_child0.Style.Height       = 72;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-20, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_should_not_be_part_of_max_height()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 250;
            root.Style.Height = 250;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Top = 20;
            root_child0.Style.Width      = 100;
            root_child0.Style.Height     = 100;
            root_child0.Style.MaxHeight  = 100;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(250, root.Layout.Width);
            Assert.AreEqual(250, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(20,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(250, root.Layout.Width);
            Assert.AreEqual(250, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(20,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_should_not_be_part_of_max_width()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 250;
            root.Style.Height = 250;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Left = 20;
            root_child0.Style.Width       = 100;
            root_child0.Style.Height      = 100;
            root_child0.Style.MaxWidth    = 100;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(250, root.Layout.Width);
            Assert.AreEqual(250, root.Layout.Height);

            Assert.AreEqual(20,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(250, root.Layout.Width);
            Assert.AreEqual(250, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_start()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width         = 100;
            root.Style.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Start = 10;
            root_child0.Style.Width        = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_top()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 100;
            root.Style.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Margin.Top = 10;
            root_child0.Style.Height     = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_with_sibling_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 100;
            root.Style.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.FlexGrow      = 1;
            root_child0.Style.Margin.Bottom = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.FlexGrow = 1;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(45,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(55,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(45,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(45,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(55,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(45,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_with_sibling_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width         = 100;
            root.Style.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.FlexGrow   = 1;
            root_child0.Style.Margin.End = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.FlexGrow = 1;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(45,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(55,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(45,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(55,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(45,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(45,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }
    }
}