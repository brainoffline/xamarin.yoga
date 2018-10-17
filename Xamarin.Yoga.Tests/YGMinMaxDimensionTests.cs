using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGMinMaxDimensionTests
    {
        [TestMethod]
        public void max_width()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetMaxWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 10);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void max_height()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 10);
            YGNodeStyleSetMaxHeight(root_child0, 50);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void min_height()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMinHeight(root_child0, 60);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            root.InsertChild(root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void min_width()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMinWidth(root_child0, 60);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            root.InsertChild(root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(80, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(20, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(20, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void justify_content_min_max()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 200);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 60);
            YGNodeStyleSetHeight(root_child0, 60);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(40, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_items_min_max()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetMinWidth(root, 100);
            YGNodeStyleSetMaxWidth(root, 200);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 60);
            YGNodeStyleSetHeight(root_child0, 60);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void justify_content_overflow_min_max()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 110);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 50);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 50);
            root.InsertChild(root_child1, 1);

             YGNode root_child2 = new YGNode(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeStyleSetHeight(root_child2, 50);
            root.InsertChild(root_child2, 2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(110, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(-20, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0, root_child2.Layout.Position.Left);
            Assert.AreEqual(80, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(110, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(-20, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0, root_child2.Layout.Position.Left);
            Assert.AreEqual(80, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_to_min()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 500);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexShrink(root_child0, 1);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 50);
            root.InsertChild(root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_in_at_most_container()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            root.InsertChild(root_child0, 0);

             YGNode root_child0_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0_child0, 0);
            root_child0.InsertChild(root_child0_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(0, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(0, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_child()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 0);
            YGNodeStyleSetHeight(root_child0, 100);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_within_constrained_min_max_column()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 200);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 50);
            root.InsertChild(root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_within_max_width()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetMaxWidth(root_child0, 100);
            root.InsertChild(root_child0, 0);

             YGNode root_child0_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetHeight(root_child0_child0, 20);
            root_child0.InsertChild(root_child0_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_within_constrained_max_width()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetMaxWidth(root_child0, 300);
            root.InsertChild(root_child0, 0);

             YGNode root_child0_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetHeight(root_child0_child0, 20);
            root_child0.InsertChild(root_child0_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_root_ignored()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetFlexGrow(root, 1);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 500);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 200);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 100);
            root.InsertChild(root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(300, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(300, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_root_minimized()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeStyleSetMaxHeight(root, 500);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMinHeight(root_child0, 100);
            YGNodeStyleSetMaxHeight(root_child0, 500);
            root.InsertChild(root_child0, 0);

             YGNode root_child0_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0_child0, 200);
            root_child0.InsertChild(root_child0_child0, 0);

             YGNode root_child0_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child0_child1, 100);
            root_child0.InsertChild(root_child0_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(300, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(300, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(200, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(300, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(300, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(200, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_height_maximized()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 500);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMinHeight(root_child0, 100);
            YGNodeStyleSetMaxHeight(root_child0, 500);
            root.InsertChild(root_child0, 0);

             YGNode root_child0_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0_child0, 200);
            root_child0.InsertChild(root_child0_child0, 0);

             YGNode root_child0_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child0_child1, 100);
            root_child0.InsertChild(root_child0_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(500, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(400, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(400, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(500, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(400, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(400, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_within_constrained_min_row()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetMinWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 50);
            root.InsertChild(root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_within_constrained_min_column()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetMinHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 50);
            root.InsertChild(root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_within_constrained_max_row()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetMaxWidth(root_child0, 100);
            YGNodeStyleSetHeight(root_child0, 100);
            root.InsertChild(root_child0, 0);

             YGNode root_child0_child0 = new YGNode(config);
            YGNodeStyleSetFlexShrink(root_child0_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0_child0, 100);
            root_child0.InsertChild(root_child0_child0, 0);

             YGNode root_child0_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0_child1, 50);
            root_child0.InsertChild(root_child0_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child1.Layout.Width);
            Assert.AreEqual(100, root_child0_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_grow_within_constrained_max_column()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetMaxHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexShrink(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 100);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetHeight(root_child1, 50);
            root.InsertChild(root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void child_min_max_width_flexing()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 120);
            YGNodeStyleSetHeight(root, 50);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 0);
            YGNodeStyleSetMinWidth(root_child0, 60);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1, 50);
            YGNodeStyleSetMaxWidth(root_child1, 20);
            root.InsertChild(root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(120, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(20, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(120, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(20, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void min_width_overrides_width()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetMinWidth(root, 100);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(0, root.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(0, root.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void max_width_overrides_width()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetMaxWidth(root, 100);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(0, root.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(0, root.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void min_height_overrides_height()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetHeight(root, 50);
            YGNodeStyleSetMinHeight(root, 100);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void max_height_overrides_height()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetHeight(root, 200);
            YGNodeStyleSetMaxHeight(root, 100);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void min_max_percent_no_width_height()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetMinWidthPercent(root_child0, 10);
            YGNodeStyleSetMaxWidthPercent(root_child0, 10);
            YGNodeStyleSetMinHeightPercent(root_child0, 10);
            YGNodeStyleSetMaxHeightPercent(root_child0, 10);
            root.InsertChild(root_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

    }
}
