using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGFlexWrapTests
    {
        [TestMethod]
        public void wrap_column()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 30);
            YGNodeStyleSetHeight(root_child0, 30);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 30);
            YGNodeStyleSetHeight(root_child1, 30);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 30);
            YGNodeStyleSetHeight(root_child2, 30);
            YGNodeInsertChild(root, root_child2, 2);

             YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 30);
            YGNodeStyleSetHeight(root_child3, 30);
            YGNodeInsertChild(root, root_child3, 3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(60, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0, root_child2.Layout.Position.Left);
            Assert.AreEqual(60, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(0, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(60, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(30, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(30, root_child2.Layout.Position.Left);
            Assert.AreEqual(60, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0, root_child3.Layout.Position.Left);
            Assert.AreEqual(0, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void wrap_row()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 30);
            YGNodeStyleSetHeight(root_child0, 30);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 30);
            YGNodeStyleSetHeight(root_child1, 30);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 30);
            YGNodeStyleSetHeight(root_child2, 30);
            YGNodeInsertChild(root, root_child2, 2);

             YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 30);
            YGNodeStyleSetHeight(root_child3, 30);
            YGNodeInsertChild(root, root_child3, 3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(0, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60, root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void wrap_row_align_items_flex_end()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexEnd);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 30);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 30);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 30);
            YGNodeStyleSetHeight(root_child2, 30);
            YGNodeInsertChild(root, root_child2, 2);

             YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 30);
            YGNodeStyleSetHeight(root_child3, 30);
            YGNodeInsertChild(root, root_child3, 3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(0, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60, root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void wrap_row_align_items_center()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 30);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 30);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 30);
            YGNodeStyleSetHeight(root_child2, 30);
            YGNodeInsertChild(root, root_child2, 2);

             YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 30);
            YGNodeStyleSetHeight(root_child3, 30);
            YGNodeInsertChild(root, root_child3, 3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(5, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(0, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60, root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(5, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void flex_wrap_children_with_min_main_overriding_flex_basis()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexBasis(root_child0, 50);
            YGNodeStyleSetMinWidth(root_child0, 55);
            YGNodeStyleSetHeight(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexBasis(root_child1, 50);
            YGNodeStyleSetMinWidth(root_child1, 55);
            YGNodeStyleSetHeight(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(55, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(55, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(55, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(45, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(55, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void flex_wrap_wrap_to_child_height()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root_child0, YGAlign.FlexStart);
            YGNodeStyleSetFlexWrap(root_child0, YGWrap.Wrap);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0, 100);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNode root_child0_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0_child0, 100);
            YGNodeStyleSetHeight(root_child0_child0_child0, 100);
            YGNodeInsertChild(root_child0_child0, root_child0_child0_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 100);
            YGNodeStyleSetHeight(root_child1, 100);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(100, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(100, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void flex_wrap_align_stretch_fits_one_row()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
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
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrap_reverse_row_align_content_flex_start()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root, YGWrap.WrapReverse);
            YGNodeStyleSetWidth(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 30);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 30);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 30);
            YGNodeStyleSetHeight(root_child2, 30);
            YGNodeInsertChild(root, root_child2, 2);

             YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 30);
            YGNodeStyleSetHeight(root_child3, 40);
            YGNodeInsertChild(root, root_child3, 3);

             YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 30);
            YGNodeStyleSetHeight(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80, root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrap_reverse_row_align_content_center()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Center);
            YGNodeStyleSetFlexWrap(root, YGWrap.WrapReverse);
            YGNodeStyleSetWidth(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 30);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 30);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 30);
            YGNodeStyleSetHeight(root_child2, 30);
            YGNodeInsertChild(root, root_child2, 2);

             YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 30);
            YGNodeStyleSetHeight(root_child3, 40);
            YGNodeInsertChild(root, root_child3, 3);

             YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 30);
            YGNodeStyleSetHeight(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80, root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrap_reverse_row_single_line_different_size()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root, YGWrap.WrapReverse);
            YGNodeStyleSetWidth(root, 300);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 30);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 30);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 30);
            YGNodeStyleSetHeight(root_child2, 30);
            YGNodeInsertChild(root, root_child2, 2);

             YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 30);
            YGNodeStyleSetHeight(root_child3, 40);
            YGNodeInsertChild(root, root_child3, 3);

             YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 30);
            YGNodeStyleSetHeight(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(300, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(40, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(90, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(120, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(300, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(270, root_child0.Layout.Position.Left);
            Assert.AreEqual(40, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(240, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(210, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(180, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(150, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrap_reverse_row_align_content_stretch()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.WrapReverse);
            YGNodeStyleSetWidth(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 30);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 30);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 30);
            YGNodeStyleSetHeight(root_child2, 30);
            YGNodeInsertChild(root, root_child2, 2);

             YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 30);
            YGNodeStyleSetHeight(root_child3, 40);
            YGNodeInsertChild(root, root_child3, 3);

             YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 30);
            YGNodeStyleSetHeight(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80, root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrap_reverse_row_align_content_space_around()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.SpaceAround);
            YGNodeStyleSetFlexWrap(root, YGWrap.WrapReverse);
            YGNodeStyleSetWidth(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 30);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 30);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 30);
            YGNodeStyleSetHeight(root_child2, 30);
            YGNodeInsertChild(root, root_child2, 2);

             YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 30);
            YGNodeStyleSetHeight(root_child3, 40);
            YGNodeInsertChild(root, root_child3, 3);

             YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 30);
            YGNodeStyleSetHeight(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80, root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrap_reverse_column_fixed_size()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetFlexWrap(root, YGWrap.WrapReverse);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 30);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 30);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 30);
            YGNodeStyleSetHeight(root_child2, 30);
            YGNodeInsertChild(root, root_child2, 2);

             YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 30);
            YGNodeStyleSetHeight(root_child3, 40);
            YGNodeInsertChild(root, root_child3, 3);

             YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 30);
            YGNodeStyleSetHeight(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(170, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(170, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(170, root_child2.Layout.Position.Left);
            Assert.AreEqual(30, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(170, root_child3.Layout.Position.Left);
            Assert.AreEqual(60, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(140, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0, root_child2.Layout.Position.Left);
            Assert.AreEqual(30, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0, root_child3.Layout.Position.Left);
            Assert.AreEqual(60, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0, root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrapped_row_within_align_items_center()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root_child0, YGWrap.Wrap);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0, 150);
            YGNodeStyleSetHeight(root_child0_child0, 80);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNode root_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child1, 80);
            YGNodeStyleSetHeight(root_child0_child1, 80);
            YGNodeInsertChild(root_child0, root_child0_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80, root_child0_child0.Layout.Height);

            Assert.AreEqual(120, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrapped_row_within_align_items_flex_start()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root_child0, YGWrap.Wrap);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0, 150);
            YGNodeStyleSetHeight(root_child0_child0, 80);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNode root_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child1, 80);
            YGNodeStyleSetHeight(root_child0_child1, 80);
            YGNodeInsertChild(root_child0, root_child0_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80, root_child0_child0.Layout.Height);

            Assert.AreEqual(120, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrapped_row_within_align_items_flex_end()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexEnd);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root_child0, YGWrap.Wrap);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0, 150);
            YGNodeStyleSetHeight(root_child0_child0, 80);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNode root_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child1, 80);
            YGNodeStyleSetHeight(root_child0_child1, 80);
            YGNodeInsertChild(root_child0, root_child0_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80, root_child0_child0.Layout.Height);

            Assert.AreEqual(120, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrapped_column_max_height()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetAlignContent(root, YGAlign.Center);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 700);
            YGNodeStyleSetHeight(root, 500);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 100);
            YGNodeStyleSetHeight(root_child0, 500);
            YGNodeStyleSetMaxHeight(root_child0, 200);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root_child1, YGEdge.Left, 20);
            YGNodeStyleSetMargin(root_child1, YGEdge.Top, 20);
            YGNodeStyleSetMargin(root_child1, YGEdge.Right, 20);
            YGNodeStyleSetMargin(root_child1, YGEdge.Bottom, 20);
            YGNodeStyleSetWidth(root_child1, 200);
            YGNodeStyleSetHeight(root_child1, 200);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 100);
            YGNodeStyleSetHeight(root_child2, 100);
            YGNodeInsertChild(root, root_child2, 2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(250, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(200, root_child1.Layout.Position.Left);
            Assert.AreEqual(250, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(200, root_child1.Layout.Height);

            Assert.AreEqual(420, root_child2.Layout.Position.Left);
            Assert.AreEqual(200, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(350, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(300, root_child1.Layout.Position.Left);
            Assert.AreEqual(250, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(200, root_child1.Layout.Height);

            Assert.AreEqual(180, root_child2.Layout.Position.Left);
            Assert.AreEqual(200, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrapped_column_max_height_flex()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetAlignContent(root, YGAlign.Center);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 700);
            YGNodeStyleSetHeight(root, 500);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexShrink(root_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0, 0);
            YGNodeStyleSetWidth(root_child0, 100);
            YGNodeStyleSetHeight(root_child0, 500);
            YGNodeStyleSetMaxHeight(root_child0, 200);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetFlexShrink(root_child1, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1, 0);
            YGNodeStyleSetMargin(root_child1, YGEdge.Left, 20);
            YGNodeStyleSetMargin(root_child1, YGEdge.Top, 20);
            YGNodeStyleSetMargin(root_child1, YGEdge.Right, 20);
            YGNodeStyleSetMargin(root_child1, YGEdge.Bottom, 20);
            YGNodeStyleSetWidth(root_child1, 200);
            YGNodeStyleSetHeight(root_child1, 200);
            YGNodeInsertChild(root, root_child1, 1);

             YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 100);
            YGNodeStyleSetHeight(root_child2, 100);
            YGNodeInsertChild(root, root_child2, 2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(300, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(180, root_child0.Layout.Height);

            Assert.AreEqual(250, root_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(180, root_child1.Layout.Height);

            Assert.AreEqual(300, root_child2.Layout.Position.Left);
            Assert.AreEqual(400, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(300, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(180, root_child0.Layout.Height);

            Assert.AreEqual(250, root_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(180, root_child1.Layout.Height);

            Assert.AreEqual(300, root_child2.Layout.Position.Left);
            Assert.AreEqual(400, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrap_nodes_with_content_sizing_overflowing_margin()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 500);
            YGNodeStyleSetHeight(root, 500);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root_child0, YGWrap.Wrap);
            YGNodeStyleSetWidth(root_child0, 85);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNode root_child0_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0_child0, 40);
            YGNodeStyleSetHeight(root_child0_child0_child0, 40);
            YGNodeInsertChild(root_child0_child0, root_child0_child0_child0, 0);

             YGNode root_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root_child0_child1, YGEdge.Right, 10);
            YGNodeInsertChild(root_child0, root_child0_child1, 1);

             YGNode root_child0_child1_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child1_child0, 40);
            YGNodeStyleSetHeight(root_child0_child1_child0, 40);
            YGNodeInsertChild(root_child0_child1, root_child0_child1_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(85, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0, root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(415, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(85, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(45, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(35, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0, root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod] public void wrap_nodes_with_content_sizing_margin_cross()
        {
             YGConfig config = new YGConfig();

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 500);
            YGNodeStyleSetHeight(root, 500);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root_child0, YGWrap.Wrap);
            YGNodeStyleSetWidth(root_child0, 70);
            YGNodeInsertChild(root, root_child0, 0);

             YGNode root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNode root_child0_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0_child0, 40);
            YGNodeStyleSetHeight(root_child0_child0_child0, 40);
            YGNodeInsertChild(root_child0_child0, root_child0_child0_child0, 0);

             YGNode root_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root_child0_child1, YGEdge.Top, 10);
            YGNodeInsertChild(root_child0, root_child0_child1, 1);

             YGNode root_child0_child1_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child1_child0, 40);
            YGNodeStyleSetHeight(root_child0_child1_child0, 40);
            YGNodeInsertChild(root_child0_child1, root_child0_child1_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(70, root_child0.Layout.Width);
            Assert.AreEqual(90, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0, root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(430, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(70, root_child0.Layout.Width);
            Assert.AreEqual(90, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(30, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0, root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

    }
}
