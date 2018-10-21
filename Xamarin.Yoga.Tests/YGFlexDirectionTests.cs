using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGFlexDirectionTests
    {
        [TestMethod]
        public void flex_direction_column_no_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetWidth(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetHeight(10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetHeight(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(30,  root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(30,  root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void flex_direction_row_no_width()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetWidth(10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetWidth(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetWidth(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(30,  root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(10,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(20,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(30,  root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(20,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(10,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void flex_direction_column()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetHeight(10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetHeight(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void flex_direction_row()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetWidth(10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetWidth(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetWidth(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(10,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(20,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(80,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(70,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void flex_direction_column_reverse()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.ColumnReverse);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetHeight(10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetHeight(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetHeight(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(90,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(80,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(70,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(90,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(80,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(70,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void flex_direction_row_reverse()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection(YGFlexDirection.RowReverse);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetWidth(10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetWidth(10);
            root.InsertChild(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.StyleSetWidth(10);
            root.InsertChild(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(80,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(70,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(10,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(20,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }
    }
}
