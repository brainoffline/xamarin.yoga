﻿using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class AlignBaselineTests
    {
        // Test case for bug in T32999822
        [TestMethod]
        public void align_baseline_parent_ht_not_specified()
        {
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignContent  = AlignType.Stretch,
                AlignItems    = AlignType.Baseline,
                Width         = 340,
                MaxHeight     = 170,
                MinHeight     = 0
            };

            var root_child0 = new YogaNode
            {
                FlexGrow    = 0, FlexShrink = 1,
                MeasureFunc = _measure1
            };
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode
            {
                FlexGrow    = 0, FlexShrink = 1,
                MeasureFunc = _measure2
            };
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(340, root.Layout.Width);
            Assert.AreEqual(126, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(42, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
            Assert.AreEqual(76, root_child0.Layout.Position.Top);

            Assert.AreEqual(42,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(279, root_child1.Layout.Width);
            Assert.AreEqual(126, root_child1.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_with_no_baseline_func_and_no_parent_ht()
        {
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 150
            };

            var root_child0 = new YogaNode
            {
                Width = 50, Height = 80
            };
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode {Width = 50, Height = 50};
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_with_no_parent_ht()
        {
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 150
            };

            var root_child0 = new YogaNode {Width = 50, Height = 50};
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode {Width = 50, Height = 40};

            root_child1.BaselineFunc = _baselineFunc;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(70,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(40, root_child1.Layout.Height);
        }

        private static float _baselineFunc(YogaNode node, float width, float height)
        {
            return height / 2;
        }

        [SuppressMessage("ReSharper", "RedundantAssignment")]
        private static SizeF _measure1(
            YogaNode    node,
            float       width,
            MeasureMode widthMode,
            float       height,
            MeasureMode heightMode)
        {
            return new SizeF(width = 42, height = 50);
        }

        [SuppressMessage("ReSharper", "RedundantAssignment")]
        private static SizeF _measure2(
            YogaNode    node,
            float       width,
            MeasureMode widthMode,
            float       height,
            MeasureMode heightMode)
        {
            return new SizeF(width = 279, height = 126);
        }
    }
}
