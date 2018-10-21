using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGEdgeTests
    {
        [TestMethod]
        public void start_overrides()
        {
            YGNode root = new YGNode();
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Margin.Start = 10;
            root_child0.Style.Margin.Left =  20;
            root_child0.Style.Margin.Right = 20;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Right);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);
        }

        [TestMethod]
        public void end_overrides()
        {
            YGNode root = new YGNode();
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Margin.End =   10;
            root_child0.Style.Margin.Left =  20;
            root_child0.Style.Margin.Right = 20;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Right);
        }

        [TestMethod]
        public void horizontal_overridden()
        {
            YGNode root = new YGNode();
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Margin.Horizontal = 10;
            root_child0.Style.Margin.Left =       20;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);
        }

        [TestMethod]
        public void vertical_overridden()
        {
            YGNode root = new YGNode();
            root.Style.FlexDirection = YGFlexDirection.Column;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Margin.Vertical = 10;
            root_child0.Style.Margin.Top =      20;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Position.Bottom);
        }

        [TestMethod]
        public void horizontal_overrides_all()
        {
            YGNode root = new YGNode();
            root.Style.FlexDirection = YGFlexDirection.Column;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Margin.Horizontal = 10;
            root_child0.Style.Margin.All =        20;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);
            Assert.AreEqual(20, root_child0.Layout.Position.Bottom);
        }

        [TestMethod]
        public void vertical_overrides_all()
        {
            YGNode root = new YGNode();
            root.Style.FlexDirection = YGFlexDirection.Column;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Margin.Vertical = 10;
            root_child0.Style.Margin.All =      20;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Position.Right);
            Assert.AreEqual(10, root_child0.Layout.Position.Bottom);
        }

        [TestMethod]
        public void all_overridden()
        {
            YGNode root = new YGNode();
            root.Style.FlexDirection = YGFlexDirection.Column;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.Margin.Left =   10;
            root_child0.Style.Margin.Top =    10;
            root_child0.Style.Margin.Right =  10;
            root_child0.Style.Margin.Bottom = 10;
            root_child0.Style.Margin.All =    20;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);
            Assert.AreEqual(10, root_child0.Layout.Position.Bottom);
        }
    }
}
