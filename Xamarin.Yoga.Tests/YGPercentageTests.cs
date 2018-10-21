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
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 200f;
            root.Style.Height = 200f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 30.Percent();
            root_child0.Style.Height = 30.Percent();
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
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 400f;
            root.Style.Height = 400f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Position.Left = 10.Percent();
            root_child0.Style.Position.Top = 20.Percent();
            root_child0.Style.Width = 45.Percent();
            root_child0.Style.Height = 55.Percent();
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
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 500f;
            root.Style.Height = 500f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Position.Right = 20.Percent();
            root_child0.Style.Position.Bottom = 10.Percent();
            root_child0.Style.Width = 55.Percent();
            root_child0.Style.Height = 15.Percent();
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
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50.Percent();
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.FlexBasis = 25.Percent();
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
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50.Percent();
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.FlexBasis = 25.Percent();
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
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.MinHeight = 60.Percent();
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 2;
            root_child1.Style.MinHeight = 10.Percent();
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
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 10.Percent();
            root_child0.Style.MaxHeight = 60.Percent();
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 4;
            root_child1.Style.FlexBasis = 10.Percent();
            root_child1.Style.MaxHeight = 20.Percent();
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
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 10.Percent();
            root_child0.Style.MaxHeight = 60.Percent();;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 4;
            root_child1.Style.FlexBasis = 10.Percent();
            root_child1.Style.MaxHeight = 20.Percent();
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
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 15.Percent();
            root_child0.Style.MaxWidth = 60.Percent();
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 4;
            root_child1.Style.FlexBasis = 10.Percent();
            root_child1.Style.MaxWidth = 20.Percent();
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
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 10.Percent();
            root_child0.Style.MaxWidth = 60.Percent();
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 4;
            root_child1.Style.FlexBasis = 15.Percent();
            root_child1.Style.MaxWidth = 20.Percent();
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
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 200f;
            root.Style.Height = 200f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 15.Percent();
            root_child0.Style.MinWidth = 60f.Percent();
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 4;
            root_child1.Style.FlexBasis = 10.Percent();
            root_child1.Style.MinWidth = 20f.Percent();
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
            root.Style.Width = 200f;
            root.Style.Height = 200f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 10.Percent();
            root_child0.Style.MinWidth = 60f.Percent();
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 4;
            root_child1.Style.FlexBasis = 15.Percent();
            root_child1.Style.MinWidth = 20f.Percent();
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
            root.Style.Width = 200f;
            root.Style.Height = 200f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 10.Percent();
            root_child0.Style.Margin = new Edges(5,5,5,5);
            root_child0.Style.Padding = new Edges(3,3,3,3);
            root_child0.Style.MinWidth = 60f.Percent();
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Margin = new Edges(5,5,5,5);
            root_child0_child0.Style.Padding.Left = 3.Percent();
            root_child0_child0.Style.Padding.Top  = 3.Percent();
            root_child0_child0.Style.Padding.Right= 3.Percent();
            root_child0_child0.Style.Padding.Bottom= 3.Percent();
            root_child0_child0.Style.Width = 50.Percent();
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.Margin.Left= 5.Percent();
            root_child0_child0_child0.Style.Margin.Top= 5.Percent();
            root_child0_child0_child0.Style.Margin.Right= 5.Percent();
            root_child0_child0_child0.Style.Margin.Bottom= 5.Percent();
            root_child0_child0_child0.Style.Padding = new Edges(3,3,3,3);
            root_child0_child0_child0.Style.Width = 45.Percent();
            root_child0_child0.InsertChild(root_child0_child0_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 4;
            root_child1.Style.FlexBasis = 15.Percent();
            root_child1.Style.MinWidth = 20.Percent();
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
            root.Style.Width = 200f;
            root.Style.Height = 100f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Margin.Left= 10.Percent();
            root_child0.Style.Margin.Top= 10.Percent();
            root_child0.Style.Margin.Right= 10.Percent();
            root_child0.Style.Margin.Bottom= 10.Percent();
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Width = 10;
            root_child0_child0.Style.Height = 10;
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
            root.Style.Width = 200f;
            root.Style.Height = 100f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Padding.Left= 10.Percent();
            root_child0.Style.Padding.Top=    10.Percent();
            root_child0.Style.Padding.Right=  10.Percent();
            root_child0.Style.Padding.Bottom= 10.Percent();
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Width = 10f;
            root_child0_child0.Style.Height = 10f;
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
            root.Style.Width = 200f;
            root.Style.Height = 100f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.PositionType = YGPositionType.Absolute;
            root_child0.Style.Position.Left = 30.Percent();
            root_child0.Style.Position.Top = 10.Percent();
            root_child0.Style.Width = 10f;
            root_child0.Style.Height = 10f;
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
            root_child0.Style.Width = 50.Percent();
            root_child0.Style.Height = 50.Percent();
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
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 350f;
            root.Style.Height = 100f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 100f;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root.InsertChild(1, root_child1);

            YGNode root_child1_child0 = new YGNode(config);
            root_child1_child0.Style.Width = 100.Percent();
            root_child1.InsertChild(root_child1_child0);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 100f;
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
            root.Style.JustifyContent = YGJustify.Center;
            root.Style.AlignItems = YGAlign.Center;
            root.Style.Width = 200f;
            root.Style.Height = 200f;

            YGNode root_child0 = new YGNode(config);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexDirection = YGFlexDirection.Row;
            root_child0_child0.Style.JustifyContent = YGJustify.Center;
            root_child0_child0.Style.Width = 100.Percent();
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.Style.Width = 50f;
            root_child0_child0_child0.Style.Height = 50f;
            root_child0_child0.InsertChild(root_child0_child0_child0);

            YGNode root_child0_child0_child1 = new YGNode(config);
            root_child0_child0_child1.Style.Width = 50f;
            root_child0_child0_child1.Style.Height = 50f;
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
            root.Style.Width = 60f;
            root.Style.Height = 50f;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexDirection = YGFlexDirection.Row;
            root_child0.Style.PositionType = YGPositionType.Absolute;
            root_child0.Style.Position.Left = 50.Percent();
            root_child0.Style.Width = 100.Percent();
            root_child0.Style.Height = 50f;
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.Width = 100.Percent();
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child1 = new YGNode(config);
            root_child0_child1.Style.Width = 100.Percent();
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
