﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class PercentageTests
    {
        [TestMethod]
        public void percent_absolute_position()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 60f;
            root.Height = 50f;

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Row;
            root_child0.PositionType  = PositionType.Absolute;
            root_child0.Position.Left = 50.Percent();
            root_child0.Width         = 100.Percent();
            root_child0.Height        = 50f;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.Width = 100.Percent();
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child1 = new YogaNode(config);
            root_child0_child1.Width = 100.Percent();
            root_child0.Children.Insert(1, root_child0_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,  root.Layout.Position.Left);
            Assert.AreEqual(0f,  root.Layout.Position.Top);
            Assert.AreEqual(60f, root.Layout.Width);
            Assert.AreEqual(50f, root.Layout.Height);

            Assert.AreEqual(30f, root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60f, root_child0.Layout.Width);
            Assert.AreEqual(50f, root_child0.Layout.Height);

            Assert.AreEqual(0f,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(60f, root_child0_child0.Layout.Width);
            Assert.AreEqual(50f, root_child0_child0.Layout.Height);

            Assert.AreEqual(60f, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(60f, root_child0_child1.Layout.Width);
            Assert.AreEqual(50f, root_child0_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,  root.Layout.Position.Left);
            Assert.AreEqual(0f,  root.Layout.Position.Top);
            Assert.AreEqual(60f, root.Layout.Width);
            Assert.AreEqual(50f, root.Layout.Height);

            Assert.AreEqual(30f, root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60f, root_child0.Layout.Width);
            Assert.AreEqual(50f, root_child0.Layout.Height);

            Assert.AreEqual(0f,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(60f, root_child0_child0.Layout.Width);
            Assert.AreEqual(50f, root_child0_child0.Layout.Height);

            Assert.AreEqual(-60f, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(60f,  root_child0_child1.Layout.Width);
            Assert.AreEqual(50f,  root_child0_child1.Layout.Height);
        }

        [TestMethod]
        public void percent_within_flex_grow()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 350f;
            root.Height        = 100f;

            var root_child0 = new YogaNode(config);
            root_child0.Width = 100f;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1;
            root.Children.Insert(1, root_child1);

            var root_child1_child0 = new YogaNode(config);
            root_child1_child0.Width = 100.Percent();
            root_child1.Children.Add(root_child1_child0);

            var root_child2 = new YogaNode(config);
            root_child2.Width = 100f;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(350f, root.Layout.Width);
            Assert.AreEqual(100f, root.Layout.Height);

            Assert.AreEqual(0f,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100f, root_child0.Layout.Width);
            Assert.AreEqual(100f, root_child0.Layout.Height);

            Assert.AreEqual(100f, root_child1.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child1.Layout.Position.Top);
            Assert.AreEqual(150f, root_child1.Layout.Width);
            Assert.AreEqual(100f, root_child1.Layout.Height);

            Assert.AreEqual(0f,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(150f, root_child1_child0.Layout.Width);
            Assert.AreEqual(0f,   root_child1_child0.Layout.Height);

            Assert.AreEqual(250f, root_child2.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child2.Layout.Position.Top);
            Assert.AreEqual(100f, root_child2.Layout.Width);
            Assert.AreEqual(100f, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(350f, root.Layout.Width);
            Assert.AreEqual(100f, root.Layout.Height);

            Assert.AreEqual(250f, root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100f, root_child0.Layout.Width);
            Assert.AreEqual(100f, root_child0.Layout.Height);

            Assert.AreEqual(100f, root_child1.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child1.Layout.Position.Top);
            Assert.AreEqual(150f, root_child1.Layout.Width);
            Assert.AreEqual(100f, root_child1.Layout.Height);

            Assert.AreEqual(0f,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(150f, root_child1_child0.Layout.Width);
            Assert.AreEqual(0f,   root_child1_child0.Layout.Height);

            Assert.AreEqual(0f,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child2.Layout.Position.Top);
            Assert.AreEqual(100f, root_child2.Layout.Width);
            Assert.AreEqual(100f, root_child2.Layout.Height);
        }

        [TestMethod]
        public void percentage_absolute_position()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200f;
            root.Height = 100f;

            var root_child0 = new YogaNode(config);
            root_child0.PositionType  = PositionType.Absolute;
            root_child0.Position.Left = 30.Percent();
            root_child0.Position.Top  = 10.Percent();
            root_child0.Width         = 10f;
            root_child0.Height        = 10f;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(100f, root.Layout.Height);

            Assert.AreEqual(60f, root_child0.Layout.Position.Left);
            Assert.AreEqual(10f, root_child0.Layout.Position.Top);
            Assert.AreEqual(10f, root_child0.Layout.Width);
            Assert.AreEqual(10f, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(100f, root.Layout.Height);

            Assert.AreEqual(60f, root_child0.Layout.Position.Left);
            Assert.AreEqual(10f, root_child0.Layout.Position.Top);
            Assert.AreEqual(10f, root_child0.Layout.Width);
            Assert.AreEqual(10f, root_child0.Layout.Height);
        }

        [TestMethod]
        public void percentage_container_in_wrapping_container()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.Center;
            root.AlignItems     = AlignType.Center;
            root.Width          = 200f;
            root.Height         = 200f;

            var root_child0 = new YogaNode(config);
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.FlexDirection  = FlexDirectionType.Row;
            root_child0_child0.JustifyContent = JustifyType.Center;
            root_child0_child0.Width          = 100.Percent();
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child0_child0 = new YogaNode(config);
            root_child0_child0_child0.Width  = 50f;
            root_child0_child0_child0.Height = 50f;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            var root_child0_child0_child1 = new YogaNode(config);
            root_child0_child0_child1.Width  = 50f;
            root_child0_child0_child1.Height = 50f;
            root_child0_child0.Children.Insert(1, root_child0_child0_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(50f,  root_child0.Layout.Position.Left);
            Assert.AreEqual(75f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100f, root_child0.Layout.Width);
            Assert.AreEqual(50f,  root_child0.Layout.Height);

            Assert.AreEqual(0f,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100f, root_child0_child0.Layout.Width);
            Assert.AreEqual(50f,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0f,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50f, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(50f, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(50f, root_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(50f, root_child0_child0_child1.Layout.Width);
            Assert.AreEqual(50f, root_child0_child0_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(50f,  root_child0.Layout.Position.Left);
            Assert.AreEqual(75f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100f, root_child0.Layout.Width);
            Assert.AreEqual(50f,  root_child0.Layout.Height);

            Assert.AreEqual(0f,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100f, root_child0_child0.Layout.Width);
            Assert.AreEqual(50f,  root_child0_child0.Layout.Height);

            Assert.AreEqual(50f, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50f, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(50f, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0f,  root_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(50f, root_child0_child0_child1.Layout.Width);
            Assert.AreEqual(50f, root_child0_child0_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_flex_basis()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 200;
            root.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 50.Percent();
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow  = 1;
            root_child1.FlexBasis = 25.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(125, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(125, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(75,  root_child1.Layout.Width);
            Assert.AreEqual(200, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(125, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(75,  root_child1.Layout.Width);
            Assert.AreEqual(200, root_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_flex_basis_cross()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200;
            root.Height = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 50.Percent();
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow  = 1;
            root_child1.FlexBasis = 25.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(125, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(125, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(75,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(125, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(125, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(75,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_flex_basis_cross_max_height()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200;
            root.Height = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 10.Percent();
            root_child0.MaxHeight = 60.Percent();
            ;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow  = 4;
            root_child1.FlexBasis = 10.Percent();
            root_child1.MaxHeight = 20.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(120, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(120, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(40,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(120, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(120, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(40,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_flex_basis_cross_max_width()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200;
            root.Height = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 10.Percent();
            root_child0.MaxWidth  = 60.Percent();
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow  = 4;
            root_child1.FlexBasis = 15.Percent();
            root_child1.MaxWidth  = 20.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(120, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(50,  root_child1.Layout.Position.Top);
            Assert.AreEqual(40,  root_child1.Layout.Width);
            Assert.AreEqual(150, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(80,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(120, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(160, root_child1.Layout.Position.Left);
            Assert.AreEqual(50,  root_child1.Layout.Position.Top);
            Assert.AreEqual(40,  root_child1.Layout.Width);
            Assert.AreEqual(150, root_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_flex_basis_cross_min_height()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200;
            root.Height = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.MinHeight = 60.Percent();
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow  = 2;
            root_child1.MinHeight = 10.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(140, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(140, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(60,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(140, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(140, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(60,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_flex_basis_cross_min_width()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200f;
            root.Height = 200f;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 10.Percent();
            root_child0.MinWidth  = 60f.Percent();
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow  = 4;
            root_child1.FlexBasis = 15.Percent();
            root_child1.MinWidth  = 20f.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(0f,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200f, root_child0.Layout.Width);
            Assert.AreEqual(50f,  root_child0.Layout.Height);

            Assert.AreEqual(0f,   root_child1.Layout.Position.Left);
            Assert.AreEqual(50f,  root_child1.Layout.Position.Top);
            Assert.AreEqual(200f, root_child1.Layout.Width);
            Assert.AreEqual(150f, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(0f,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200f, root_child0.Layout.Width);
            Assert.AreEqual(50f,  root_child0.Layout.Height);

            Assert.AreEqual(0f,   root_child1.Layout.Position.Left);
            Assert.AreEqual(50f,  root_child1.Layout.Position.Top);
            Assert.AreEqual(200f, root_child1.Layout.Width);
            Assert.AreEqual(150f, root_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_flex_basis_main_max_height()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 200;
            root.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 10.Percent();
            root_child0.MaxHeight = 60.Percent();
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow  = 4;
            root_child1.FlexBasis = 10.Percent();
            root_child1.MaxHeight = 20.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(52,  root_child0.Layout.Width);
            Assert.AreEqual(120, root_child0.Layout.Height);

            Assert.AreEqual(52,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(148, root_child1.Layout.Width);
            Assert.AreEqual(40,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(148, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(52,  root_child0.Layout.Width);
            Assert.AreEqual(120, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(148, root_child1.Layout.Width);
            Assert.AreEqual(40,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_flex_basis_main_max_width()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 200;
            root.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 15.Percent();
            root_child0.MaxWidth  = 60.Percent();
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow  = 4;
            root_child1.FlexBasis = 10.Percent();
            root_child1.MaxWidth  = 20.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(120, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(120, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(40,  root_child1.Layout.Width);
            Assert.AreEqual(200, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(80,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(120, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(40,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(40,  root_child1.Layout.Width);
            Assert.AreEqual(200, root_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_flex_basis_main_min_width()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 200f;
            root.Height        = 200f;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 15.Percent();
            root_child0.MinWidth  = 60f.Percent();
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow  = 4;
            root_child1.FlexBasis = 10.Percent();
            root_child1.MinWidth  = 20f.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(0f,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(120f, root_child0.Layout.Width);
            Assert.AreEqual(200f, root_child0.Layout.Height);

            Assert.AreEqual(120f, root_child1.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child1.Layout.Position.Top);
            Assert.AreEqual(80f,  root_child1.Layout.Width);
            Assert.AreEqual(200f, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(80f,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(120f, root_child0.Layout.Width);
            Assert.AreEqual(200f, root_child0.Layout.Height);

            Assert.AreEqual(0f,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child1.Layout.Position.Top);
            Assert.AreEqual(80f,  root_child1.Layout.Width);
            Assert.AreEqual(200f, root_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_margin_should_calculate_based_only_on_width()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200f;
            root.Height = 100f;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow      = 1;
            root_child0.Margin.Left   = 10.Percent();
            root_child0.Margin.Top    = 10.Percent();
            root_child0.Margin.Right  = 10.Percent();
            root_child0.Margin.Bottom = 10.Percent();
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.Width  = 10;
            root_child0_child0.Height = 10;
            root_child0.Children.Add(root_child0_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(100f, root.Layout.Height);

            Assert.AreEqual(20f,  root_child0.Layout.Position.Left);
            Assert.AreEqual(20f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(160f, root_child0.Layout.Width);
            Assert.AreEqual(60f,  root_child0.Layout.Height);

            Assert.AreEqual(0f,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(10f, root_child0_child0.Layout.Width);
            Assert.AreEqual(10f, root_child0_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(100f, root.Layout.Height);

            Assert.AreEqual(20f,  root_child0.Layout.Position.Left);
            Assert.AreEqual(20f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(160f, root_child0.Layout.Width);
            Assert.AreEqual(60f,  root_child0.Layout.Height);

            Assert.AreEqual(150f, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(10f,  root_child0_child0.Layout.Width);
            Assert.AreEqual(10f,  root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void percentage_multiple_nested_with_padding_margin_and_percentage_values()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200f;
            root.Height = 200f;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 10.Percent();
            root_child0.Margin    = new Edges(5, 5, 5, 5);
            root_child0.Padding   = new Edges(3, 3, 3, 3);
            root_child0.MinWidth  = 60f.Percent();
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.Margin         = new Edges(5, 5, 5, 5);
            root_child0_child0.Padding.Left   = 3.Percent();
            root_child0_child0.Padding.Top    = 3.Percent();
            root_child0_child0.Padding.Right  = 3.Percent();
            root_child0_child0.Padding.Bottom = 3.Percent();
            root_child0_child0.Width          = 50.Percent();
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child0_child0 = new YogaNode(config);
            root_child0_child0_child0.Margin.Left   = 5.Percent();
            root_child0_child0_child0.Margin.Top    = 5.Percent();
            root_child0_child0_child0.Margin.Right  = 5.Percent();
            root_child0_child0_child0.Margin.Bottom = 5.Percent();
            root_child0_child0_child0.Padding       = new Edges(3, 3, 3, 3);
            root_child0_child0_child0.Width         = 45.Percent();
            root_child0_child0.Children.Add(root_child0_child0_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow  = 4;
            root_child1.FlexBasis = 15.Percent();
            root_child1.MinWidth  = 20.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(5f,   root_child0.Layout.Position.Left);
            Assert.AreEqual(5f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(190f, root_child0.Layout.Width);
            Assert.AreEqual(48f,  root_child0.Layout.Height);

            Assert.AreEqual(8f,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(8f,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(92f, root_child0_child0.Layout.Width);
            Assert.AreEqual(25f, root_child0_child0.Layout.Height);

            Assert.AreEqual(10f, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(10f, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(36f, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(6f,  root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0f,   root_child1.Layout.Position.Left);
            Assert.AreEqual(58f,  root_child1.Layout.Position.Top);
            Assert.AreEqual(200f, root_child1.Layout.Width);
            Assert.AreEqual(142f, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(5f,   root_child0.Layout.Position.Left);
            Assert.AreEqual(5f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(190f, root_child0.Layout.Width);
            Assert.AreEqual(48f,  root_child0.Layout.Height);

            Assert.AreEqual(90f, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(8f,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(92f, root_child0_child0.Layout.Width);
            Assert.AreEqual(25f, root_child0_child0.Layout.Height);

            Assert.AreEqual(46f, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(10f, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(36f, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(6f,  root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0f,   root_child1.Layout.Position.Left);
            Assert.AreEqual(58f,  root_child1.Layout.Position.Top);
            Assert.AreEqual(200f, root_child1.Layout.Width);
            Assert.AreEqual(142f, root_child1.Layout.Height);
        }

        [TestMethod]
        public void percentage_padding_should_calculate_based_only_on_width()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200f;
            root.Height = 100f;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow       = 1;
            root_child0.Padding.Left   = 10.Percent();
            root_child0.Padding.Top    = 10.Percent();
            root_child0.Padding.Right  = 10.Percent();
            root_child0.Padding.Bottom = 10.Percent();
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.Width  = 10f;
            root_child0_child0.Height = 10f;
            root_child0.Children.Add(root_child0_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(100f, root.Layout.Height);

            Assert.AreEqual(0f,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200f, root_child0.Layout.Width);
            Assert.AreEqual(100f, root_child0.Layout.Height);

            Assert.AreEqual(20f, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(20f, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(10f, root_child0_child0.Layout.Width);
            Assert.AreEqual(10f, root_child0_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(100f, root.Layout.Height);

            Assert.AreEqual(0f,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200f, root_child0.Layout.Width);
            Assert.AreEqual(100f, root_child0.Layout.Height);

            Assert.AreEqual(170f, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(20f,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(10f,  root_child0_child0.Layout.Width);
            Assert.AreEqual(10f,  root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void percentage_position_bottom_right()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 500f;
            root.Height        = 500f;

            var root_child0 = new YogaNode(config);
            root_child0.Position.Right  = 20.Percent();
            root_child0.Position.Bottom = 10.Percent();
            root_child0.Width           = 55.Percent();
            root_child0.Height          = 15.Percent();
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(500f, root.Layout.Width);
            Assert.AreEqual(500f, root.Layout.Height);

            Assert.AreEqual(-100f, root_child0.Layout.Position.Left);
            Assert.AreEqual(-50f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(275f,  root_child0.Layout.Width);
            Assert.AreEqual(75f,   root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(500f, root.Layout.Width);
            Assert.AreEqual(500f, root.Layout.Height);

            Assert.AreEqual(125f, root_child0.Layout.Position.Left);
            Assert.AreEqual(-50f, root_child0.Layout.Position.Top);
            Assert.AreEqual(275f, root_child0.Layout.Width);
            Assert.AreEqual(75f,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void percentage_position_left_top()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 400f;
            root.Height        = 400f;

            var root_child0 = new YogaNode(config);
            root_child0.Position.Left = 10.Percent();
            root_child0.Position.Top  = 20.Percent();
            root_child0.Width         = 45.Percent();
            root_child0.Height        = 55.Percent();
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(400f, root.Layout.Width);
            Assert.AreEqual(400f, root.Layout.Height);

            Assert.AreEqual(40f,  root_child0.Layout.Position.Left);
            Assert.AreEqual(80f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(180f, root_child0.Layout.Width);
            Assert.AreEqual(220f, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(400f, root.Layout.Width);
            Assert.AreEqual(400f, root.Layout.Height);

            Assert.AreEqual(260f, root_child0.Layout.Position.Left);
            Assert.AreEqual(80f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(180f, root_child0.Layout.Width);
            Assert.AreEqual(220f, root_child0.Layout.Height);
        }

        [TestMethod]
        public void percentage_width_height()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 200f;
            root.Height        = 200f;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30.Percent();
            root_child0.Height = 30.Percent();
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(0f,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60f, root_child0.Layout.Width);
            Assert.AreEqual(60f, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(140f, root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,   root_child0.Layout.Position.Top);
            Assert.AreEqual(60f,  root_child0.Layout.Width);
            Assert.AreEqual(60f,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void percentage_width_height_undefined_parent_size()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 50.Percent();
            root_child0.Height = 50.Percent();
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0f, root.Layout.Position.Left);
            Assert.AreEqual(0f, root.Layout.Position.Top);
            Assert.AreEqual(0f, root.Layout.Width);
            Assert.AreEqual(0f, root.Layout.Height);

            Assert.AreEqual(0f, root_child0.Layout.Position.Left);
            Assert.AreEqual(0f, root_child0.Layout.Position.Top);
            Assert.AreEqual(0f, root_child0.Layout.Width);
            Assert.AreEqual(0f, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0f, root.Layout.Position.Left);
            Assert.AreEqual(0f, root.Layout.Position.Top);
            Assert.AreEqual(0f, root.Layout.Width);
            Assert.AreEqual(0f, root.Layout.Height);

            Assert.AreEqual(0f, root_child0.Layout.Position.Left);
            Assert.AreEqual(0f, root_child0.Layout.Position.Top);
            Assert.AreEqual(0f, root_child0.Layout.Width);
            Assert.AreEqual(0f, root_child0.Layout.Height);
        }
    }
}
