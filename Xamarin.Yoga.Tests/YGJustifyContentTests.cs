using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YogaConst;


    [TestClass]
    public class YGJustifyContentTests
    {
        [TestMethod]
        public void justify_content_row_flex_start()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(10,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(20,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(92,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(82,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(72,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_flex_end()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.JustifyContent = JustifyType.FlexEnd;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(72,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(82,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(92,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(20,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(10,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_center()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(36,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(56,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(56,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(36,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_space_between()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.JustifyContent = JustifyType.SpaceBetween;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(92,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(92,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_space_around()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.JustifyContent = JustifyType.SpaceAround;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(12,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(80,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(80,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(12,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_flex_start()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_flex_end()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = JustifyType.FlexEnd;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(72,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(82,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(72,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(82,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_center()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(36,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(56,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(36,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(56,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_space_between()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = JustifyType.SpaceBetween;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_space_around()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = JustifyType.SpaceAround;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(12,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(80,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(12,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(80,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_min_width_and_margin()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Margin.Left = 100;
            root.Style.MinWidth = 50;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 20;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(50,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(15, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(50,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(15, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_min_width_with_padding_child_width_greater_than_parent()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.Width = 1000;
            root.Style.Height = 1584;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0.Style.AlignContent = YGAlign.Stretch;
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0_child0.Style.JustifyContent = JustifyType.Center;
            root_child0_child0.Style.AlignContent = YGAlign.Stretch;
            root_child0_child0.Style.MinWidth = 400;
            root_child0_child0.Style.Padding.Horizontal = 100;
            root_child0.Children.Add(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0_child0_child0.Style.AlignContent = YGAlign.Stretch;
            root_child0_child0_child0.Style.Width = 300;
            root_child0_child0_child0.Style.Height = 100;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1000, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1000, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(100, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(300, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1000, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1000, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(500, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(100, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(300, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_min_width_with_padding_child_width_lower_than_parent()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.Width = 1080;
            root.Style.Height = 1584;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0.Style.AlignContent = YGAlign.Stretch;
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0_child0.Style.JustifyContent = JustifyType.Center;
            root_child0_child0.Style.AlignContent = YGAlign.Stretch;
            root_child0_child0.Style.MinWidth = 400;
            root_child0_child0.Style.Padding.Horizontal = 100;
            root_child0.Children.Add(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.FlexDirection = FlexDirectionType.Row;
            root_child0_child0_child0.Style.AlignContent = YGAlign.Stretch;
            root_child0_child0_child0.Style.Width = 199;
            root_child0_child0_child0.Style.Height = 100;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1080, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(400, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(101, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(199, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1080, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(680, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(400, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(101, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(199, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);
            //
        }

        [TestMethod]
        public void justify_content_row_max_width_and_margin()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Margin.Left = 100;
            root.Style.Width = 100;
            root.Style.MaxWidth = 80;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 20;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(80,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(30, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(80,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(30, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_min_height_and_margin()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Margin.Top = 100;
            root.Style.MinHeight = 50;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 20;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_colunn_max_height_and_margin()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = JustifyType.Center;
            root.Style.Margin.Top = 100;
            root.Style.Height = 100;
            root.Style.MaxHeight = 80;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 20;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_space_evenly()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.JustifyContent = JustifyType.SpaceEvenly;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(18,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(74,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(18,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(74,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_space_evenly()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.JustifyContent = JustifyType.SpaceEvenly;
            root.Style.Width = 102;
            root.Style.Height = 102;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(26, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(51, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(0,  root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(77, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(0,  root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(77, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(51, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(0,  root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(26, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(0,  root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);
        }
    }
}
