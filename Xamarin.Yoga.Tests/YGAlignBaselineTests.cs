﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGAlignBaselineTests
    {
        private static float _baselineFunc(YGNode node, float width, float height)
        {
            return height / 2;
        }

        private static YGSize _measure1(
            YGNode     node,
            float         width,
            YGMeasureMode widthMode,
            float         height,
            YGMeasureMode heightMode)
        {
            return new YGSize(width = 42, height = 50);
        }

        private static YGSize _measure2(
            YGNode     node,
            float         width,
            YGMeasureMode widthMode,
            float         height,
            YGMeasureMode heightMode)
        {
            return new YGSize(width = 279, height = 126);
        }

        // Test case for bug in T32999822
        [TestMethod]
        public void align_baseline_parent_ht_not_specified()
        {
            var config = new YGConfig();

            var root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetAlignItems(root, YGAlign.Baseline);
            YGNodeStyleSetWidth(root, 340);
            YGNodeStyleSetMaxHeight(root, 170);
            YGNodeStyleSetMinHeight(root, 0);

            var root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 0);
            YGNodeStyleSetFlexShrink(root_child0, 1);
            root_child0.MeasureFunc = _measure1;
            root.InsertChild(root_child0, 0);

            var root_child1 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child1, 0);
            YGNodeStyleSetFlexShrink(root_child1, 1);
            root_child1.MeasureFunc = _measure2;
            root.InsertChild(root_child1, 1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void align_baseline_with_no_parent_ht()
        {
            var config = new YGConfig();

            var root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root, YGAlign.Baseline);
            YGNodeStyleSetWidth(root, 150);

            var root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 50);
            root.InsertChild(root_child0, 0);

            var root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 40);

            root_child1.setBaseLineFunc(_baselineFunc);
            root.InsertChild(root_child1, 1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void align_baseline_with_no_baseline_func_and_no_parent_ht()
        {
            var config = new YGConfig();

            var root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root, YGAlign.Baseline);
            YGNodeStyleSetWidth(root, 150);

            var root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 80);
            root.InsertChild(root_child0, 0);

            var root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 50);
            root.InsertChild(root_child1, 1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeFreeRecursive(root);

            
        }
    }
}
