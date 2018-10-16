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

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child2, 10);
            YGNodeInsertChild(root, root_child2, 2);
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

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_direction_row_no_width()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 10);
            YGNodeInsertChild(root, root_child2, 2);
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

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_direction_column()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child2, 10);
            YGNodeInsertChild(root, root_child2, 2);
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

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_direction_row()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 10);
            YGNodeInsertChild(root, root_child2, 2);
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

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_direction_column_reverse()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.ColumnReverse);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child2, 10);
            YGNodeInsertChild(root, root_child2, 2);
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

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_direction_row_reverse()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.RowReverse);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 10);
            YGNodeInsertChild(root, root_child2, 2);
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

            YGNodeFreeRecursive(root);

            
        }
    }
}
