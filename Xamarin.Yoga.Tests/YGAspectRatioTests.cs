﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGAspectRatioTests
    {
        private static YGSize _measure(YGNode node,
            float                             width,
            YGMeasureMode                     widthMode,
            float                             height,
            YGMeasureMode                     heightMode)
        {
            return new YGSize(
                width = widthMode   == YGMeasureMode.Exactly ? width : 50,
                height = heightMode == YGMeasureMode.Exactly ? height : 50
            );
        }

        [TestMethod]
        public void aspect_ratio_cross_defined()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_main_defined()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_both_dimensions_defined_row()
        {
            YGNode root = new YGNode();
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(100);
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_both_dimensions_defined_column()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(100);
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_align_stretch()
        {
            YGNode root = new YGNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_flex_grow()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_flex_shrink()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(150);
            root_child0.StyleSetFlexShrink(1);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_flex_shrink_2()
        {
            YGNode root = new YGNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeightPercent(100);
            root_child0.StyleSetFlexShrink(1);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode();
            root_child1.StyleSetHeightPercent(100);
            root_child1.StyleSetFlexShrink(1);
            root_child1.StyleSetAspectRatio(1);
            root.InsertChild(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetFlexBasis(50);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_absolute_layout_width_defined()
        {
            YGNode root = new YGNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetPositionType(YGPositionType.Absolute);
            root_child0.StyleSetPosition(YGEdge.Left, 0);
            root_child0.StyleSetPosition(YGEdge.Top,  0);
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_absolute_layout_height_defined()
        {
            YGNode root = new YGNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetPositionType(YGPositionType.Absolute);
            root_child0.StyleSetPosition(YGEdge.Left, 0);
            root_child0.StyleSetPosition(YGEdge.Top,  0);
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_max_cross_defined()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetMaxWidth(40);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_max_main_defined()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetMaxHeight(40);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_min_cross_defined()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(30);
            root_child0.StyleSetMinWidth(40);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_min_main_defined()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(30);
            root_child0.StyleSetMinHeight(40);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_double_cross()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetAspectRatio(2);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_half_cross()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(100);
            root_child0.StyleSetAspectRatio(0.5f);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_double_main()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetAspectRatio(0.5f);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_half_main()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(100);
            root_child0.StyleSetAspectRatio(2);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_measure_func()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.MeasureFunc = _measure;
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_height_flex_grow_row()
        {
            YGNode root = new YGNode();
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(200);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_height_flex_grow_column()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(200);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_height_as_flex_basis()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetWidth(200);
            root.StyleSetHeight(200);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode();
            root_child1.StyleSetHeight(100);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetAspectRatio(1);
            root.InsertChild(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(200);
            root.StyleSetHeight(200);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode();
            root_child1.StyleSetWidth(100);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetAspectRatio(1);
            root.InsertChild(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetAspectRatio(0.5f);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_overrides_flex_grow_column()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetAspectRatio(2);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_left_right_absolute()
        {
            YGNode root = new YGNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetPositionType(YGPositionType.Absolute);
            root_child0.StyleSetPosition(YGEdge.Left,  10);
            root_child0.StyleSetPosition(YGEdge.Top,   10);
            root_child0.StyleSetPosition(YGEdge.Right, 10);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_top_bottom_absolute()
        {
            YGNode root = new YGNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetPositionType(YGPositionType.Absolute);
            root_child0.StyleSetPosition(YGEdge.Left,   10);
            root_child0.StyleSetPosition(YGEdge.Top,    10);
            root_child0.StyleSetPosition(YGEdge.Bottom, 10);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_overrides_align_stretch_row()
        {
            YGNode root = new YGNode();
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_height_overrides_align_stretch_column()
        {
            YGNode root = new YGNode();
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetAspectRatio(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_allow_child_overflow_parent_size()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetAspectRatio(4);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_defined_main_with_margin()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.Center);
            root.StyleSetJustifyContent(YGJustify.Center);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetHeight(50);
            root_child0.StyleSetAspectRatio(1);
            root_child0.StyleSetMargin(YGEdge.Left,  10);
            root_child0.StyleSetMargin(YGEdge.Right, 10);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_defined_cross_with_margin()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.Center);
            root.StyleSetJustifyContent(YGJustify.Center);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetAspectRatio(1);
            root_child0.StyleSetMargin(YGEdge.Left,  10);
            root_child0.StyleSetMargin(YGEdge.Right, 10);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_defined_cross_with_main_margin()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.Center);
            root.StyleSetJustifyContent(YGJustify.Center);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(50);
            root_child0.StyleSetAspectRatio(1);
            root_child0.StyleSetMargin(YGEdge.Top,    10);
            root_child0.StyleSetMargin(YGEdge.Bottom, 10);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_should_prefer_explicit_height()
        {
            YGConfig config = new YGConfig {UseWebDefaults = true};

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Column);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexDirection(YGFlexDirection.Column);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetFlexDirection(YGFlexDirection.Column);
            root_child0_child0.StyleSetHeight(100);
            root_child0_child0.StyleSetAspectRatio(2);
            root_child0.InsertChild(root_child0_child0);

            YGNodeCalculateLayout(root, 100, 200, YGDirection.LTR);

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
            YGConfig config = new YGConfig {UseWebDefaults = true};

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0.StyleSetWidth(100);
            root_child0_child0.StyleSetAspectRatio(0.5f);
            root_child0.InsertChild(root_child0_child0);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

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
            YGConfig config = new YGConfig {UseWebDefaults = true};

            YGNode root = new YGNode(config);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexDirection(YGFlexDirection.Column);
            root_child0.StyleSetAspectRatio(2);
            root_child0.StyleSetFlexGrow(1);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetAspectRatio(4);
            root_child0_child0.StyleSetFlexGrow(1);
            root_child0.InsertChild(root_child0_child0);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(200, root_child0_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0_child0.Layout.Height);
        }
    }
}
