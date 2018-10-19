﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGPercentageTests
    {
        [TestMethod]
        public void percentage_width_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 200f);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidthPercent(root_child0, 30f);
            YGNodeStyleSetHeightPercent(root_child0, 30f);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(200f, root.Layout.Height);

            Assert.AreEqual(0f,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60f, root_child0.Layout.Width);
            Assert.AreEqual(60f, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_position_left_top()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 400f);
            YGNodeStyleSetHeight(root, 400f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetPositionPercent(YGEdge.Left, 10);
            root_child0.StyleSetPositionPercent(YGEdge.Top,  20);
            YGNodeStyleSetWidthPercent(root_child0, 45f);
            YGNodeStyleSetHeightPercent(root_child0, 55f);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(400f, root.Layout.Width);
            Assert.AreEqual(400f, root.Layout.Height);

            Assert.AreEqual(40f,  root_child0.Layout.Position.Left);
            Assert.AreEqual(80f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(180f, root_child0.Layout.Width);
            Assert.AreEqual(220f, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_position_bottom_right()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 500f);
            YGNodeStyleSetHeight(root, 500f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetPositionPercent(YGEdge.Right,  20f);
            root_child0.StyleSetPositionPercent(YGEdge.Bottom, 10f);
            YGNodeStyleSetWidthPercent(root_child0, 55f);
            YGNodeStyleSetHeightPercent(root_child0, 15f);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(500f, root.Layout.Width);
            Assert.AreEqual(500f, root.Layout.Height);

            Assert.AreEqual(-100f, root_child0.Layout.Position.Left);
            Assert.AreEqual(-50f,  root_child0.Layout.Position.Top);
            Assert.AreEqual(275f,  root_child0.Layout.Width);
            Assert.AreEqual(75f,   root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_flex_basis()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasisPercent(50);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetFlexBasisPercent(25);
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasisPercent(50);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1);
            root_child1.StyleSetFlexBasisPercent(25);
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_flex_basis_cross_min_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            YGNodeStyleSetMinHeightPercent(root_child0, 60);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(2);
            YGNodeStyleSetMinHeightPercent(root_child1, 10);
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_flex_basis_main_max_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasisPercent(10);
            YGNodeStyleSetMaxHeightPercent(root_child0, 60);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(4);
            root_child1.StyleSetFlexBasisPercent(10);
            YGNodeStyleSetMaxHeightPercent(root_child1, 20);
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_flex_basis_cross_max_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasisPercent(10);
            YGNodeStyleSetMaxHeightPercent(root_child0, 60);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(4);
            root_child1.StyleSetFlexBasisPercent(10);
            YGNodeStyleSetMaxHeightPercent(root_child1, 20);
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_flex_basis_main_max_width()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasisPercent(15);
            YGNodeStyleSetMaxWidthPercent(root_child0, 60);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(4);
            root_child1.StyleSetFlexBasisPercent(10);
            YGNodeStyleSetMaxWidthPercent(root_child1, 20);
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_flex_basis_cross_max_width()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasisPercent(10);
            YGNodeStyleSetMaxWidthPercent(root_child0, 60);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(4);
            root_child1.StyleSetFlexBasisPercent(15);
            YGNodeStyleSetMaxWidthPercent(root_child1, 20);
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_flex_basis_main_min_width()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 200f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1f);
            root_child0.StyleSetFlexBasisPercent(15f);
            YGNodeStyleSetMinWidthPercent(root_child0, 60f);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(4f);
            root_child1.StyleSetFlexBasisPercent(10f);
            YGNodeStyleSetMinWidthPercent(root_child1, 20f);
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_flex_basis_cross_min_width()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 200f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1f);
            root_child0.StyleSetFlexBasisPercent(10f);
            YGNodeStyleSetMinWidthPercent(root_child0, 60f);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(4f);
            root_child1.StyleSetFlexBasisPercent(15f);
            YGNodeStyleSetMinWidthPercent(root_child1, 20f);
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_multiple_nested_with_padding_margin_and_percentage_values()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 200f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexBasisPercent(10f);
            root_child0.StyleSetMargin(new Edges(5,5,5,5));
            root_child0.StyleSetPadding(new Edges(3,3,3,3));
            YGNodeStyleSetMinWidthPercent(root_child0, 60f);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetMargin(new Edges(5,5,5,5));
            root_child0_child0.StyleSetPaddingPercent(YGEdge.Left,   3f);
            root_child0_child0.StyleSetPaddingPercent(YGEdge.Top,    3f);
            root_child0_child0.StyleSetPaddingPercent(YGEdge.Right,  3f);
            root_child0_child0.StyleSetPaddingPercent(YGEdge.Bottom, 3f);
            YGNodeStyleSetWidthPercent(root_child0_child0, 50f);
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.StyleSetMargin(new Edges(5,5,5,5));
            root_child0_child0_child0.StyleSetPadding(new Edges(3,3,3,3));
            YGNodeStyleSetWidthPercent(root_child0_child0_child0, 45f);
            root_child0_child0.InsertChild(root_child0_child0_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(4f);
            root_child1.StyleSetFlexBasisPercent(15f);
            YGNodeStyleSetMinWidthPercent(root_child1, 20f);
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_margin_should_calculate_based_only_on_width()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 100f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetMargin(new Edges(10, 10, 10, 10));
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetDimensions(10, 10);
            root_child0.InsertChild(root_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_padding_should_calculate_based_only_on_width()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 100f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetPaddingPercent(YGEdge.Left,   10f);
            root_child0.StyleSetPaddingPercent(YGEdge.Top,    10f);
            root_child0.StyleSetPaddingPercent(YGEdge.Right,  10f);
            root_child0.StyleSetPaddingPercent(YGEdge.Bottom, 10f);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0_child0, 10f);
            YGNodeStyleSetHeight(root_child0_child0, 10f);
            root_child0.InsertChild(root_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_absolute_position()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 100f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetPositionType(YGPositionType.Absolute);
            root_child0.StyleSetPositionPercent(YGEdge.Left, 30f);
            root_child0.StyleSetPositionPercent(YGEdge.Top,  10f);
            YGNodeStyleSetWidth(root_child0, 10f);
            YGNodeStyleSetHeight(root_child0, 10f);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0f,   root.Layout.Position.Left);
            Assert.AreEqual(0f,   root.Layout.Position.Top);
            Assert.AreEqual(200f, root.Layout.Width);
            Assert.AreEqual(100f, root.Layout.Height);

            Assert.AreEqual(60f, root_child0.Layout.Position.Left);
            Assert.AreEqual(10f, root_child0.Layout.Position.Top);
            Assert.AreEqual(10f, root_child0.Layout.Width);
            Assert.AreEqual(10f, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_width_height_undefined_parent_size()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidthPercent(root_child0, 50f);
            YGNodeStyleSetHeightPercent(root_child0, 50f);
            root.InsertChild(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0f, root.Layout.Position.Left);
            Assert.AreEqual(0f, root.Layout.Position.Top);
            Assert.AreEqual(0f, root.Layout.Width);
            Assert.AreEqual(0f, root.Layout.Height);

            Assert.AreEqual(0f, root_child0.Layout.Position.Left);
            Assert.AreEqual(0f, root_child0.Layout.Position.Top);
            Assert.AreEqual(0f, root_child0.Layout.Width);
            Assert.AreEqual(0f, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0f, root.Layout.Position.Left);
            Assert.AreEqual(0f, root.Layout.Position.Top);
            Assert.AreEqual(0f, root.Layout.Width);
            Assert.AreEqual(0f, root.Layout.Height);

            Assert.AreEqual(0f, root_child0.Layout.Position.Left);
            Assert.AreEqual(0f, root_child0.Layout.Position.Top);
            Assert.AreEqual(0f, root_child0.Layout.Width);
            Assert.AreEqual(0f, root_child0.Layout.Height);
        }

        [TestMethod]
        public void percent_within_flex_grow()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 350f);
            YGNodeStyleSetHeight(root, 100f);

            YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 100f);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetFlexGrow(1f);
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            YGNodeStyleSetWidthPercent(root_child1_child0, 100f);
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetWidth(root_child2, 100f);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percentage_container_in_wrapping_container()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetJustifyContent( YGJustify.Center);
            root.StyleSetAlignItems(YGAlign.Center);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 200f);

            YGNode root_child0 = new YGNode(config);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0.StyleSetJustifyContent(YGJustify.Center);
            YGNodeStyleSetWidthPercent(root_child0_child0, 100f);
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0_child0_child0, 50f);
            YGNodeStyleSetHeight(root_child0_child0_child0, 50f);
            root_child0_child0.InsertChild(root_child0_child0_child0);

            YGNode root_child0_child0_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0_child0_child1, 50f);
            YGNodeStyleSetHeight(root_child0_child0_child1, 50f);
            root_child0_child0.InsertChild(1, root_child0_child0_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
        public void percent_absolute_position()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 60f);
            YGNodeStyleSetHeight(root, 50f);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0.StyleSetPositionType(YGPositionType.Absolute);
            root_child0.StyleSetPositionPercent(YGEdge.Left, 50);
            YGNodeStyleSetWidthPercent(root_child0, 100f);
            YGNodeStyleSetHeight(root_child0, 50f);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            YGNodeStyleSetWidthPercent(root_child0_child0, 100f);
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child1 = new YGNode(config);
            YGNodeStyleSetWidthPercent(root_child0_child1, 100f);
            root_child0.InsertChild(1, root_child0_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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
    }
}
