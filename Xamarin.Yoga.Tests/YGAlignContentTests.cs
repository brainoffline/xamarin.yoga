using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGAlignContentTests
    {
        [TestMethod]
        public void align_content_flex_start()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 130);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeStyleSetHeight(root_child2, 10);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeStyleSetHeight(root_child3, 10);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeStyleSetHeight(root_child4, 10);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(80, root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(80, root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_flex_start_without_height_on_children()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeStyleSetHeight(root_child3, 10);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(0,  root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(0,  root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_flex_start_with_flex()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 120);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0, 0);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1, 0);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child3, 1);
            YGNodeStyleSetFlexShrink(root_child3, 1);
            YGNodeStyleSetFlexBasisPercent(root_child3, 0);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(40, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(80, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(80, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(120, root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(0,   root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(40, root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(80, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(80, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(50,  root_child4.Layout.Position.Left);
            Assert.AreEqual(120, root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(0,   root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_flex_end()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(root, YGAlign.FlexEnd);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeStyleSetHeight(root_child2, 10);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeStyleSetHeight(root_child3, 10);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeStyleSetHeight(root_child4, 10);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(40, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(40, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(0,  root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(0,  root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(0,   root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(0,   root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(0,   root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(0,   root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_spacebetween()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.SpaceBetween);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 130);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeStyleSetHeight(root_child2, 10);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeStyleSetHeight(root_child3, 10);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeStyleSetHeight(root_child4, 10);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(45, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(45, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(80, root_child2.Layout.Position.Left);
            Assert.AreEqual(45, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(45, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(80, root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_spacearound()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.SpaceAround);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 140);
            YGNodeStyleSetHeight(root, 120);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeStyleSetHeight(root_child2, 10);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeStyleSetHeight(root_child3, 10);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeStyleSetHeight(root_child4, 10);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(140, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(15, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(55, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(55, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(95, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(140, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(15, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(90, root_child2.Layout.Position.Left);
            Assert.AreEqual(55, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(40, root_child3.Layout.Position.Left);
            Assert.AreEqual(55, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(90, root_child4.Layout.Position.Left);
            Assert.AreEqual(95, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_row()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_row_with_children()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetFlexShrink(root_child0_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0_child0, 0);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_row_with_flex()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetFlexShrink(root_child1, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1, 0);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child3, 1);
            YGNodeStyleSetFlexShrink(root_child3, 1);
            YGNodeStyleSetFlexBasisPercent(root_child3, 0);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(50,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_row_with_flex_no_shrink()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetFlexShrink(root_child1, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1, 0);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child3, 1);
            YGNodeStyleSetFlexBasisPercent(root_child3, 0);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(50,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_row_with_margin()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root_child1, YGEdge.Left,   10);
            YGNodeStyleSetMargin(root_child1, YGEdge.Top,    10);
            YGNodeStyleSetMargin(root_child1, YGEdge.Right,  10);
            YGNodeStyleSetMargin(root_child1, YGEdge.Bottom, 10);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root_child3, YGEdge.Left,   10);
            YGNodeStyleSetMargin(root_child3, YGEdge.Top,    10);
            YGNodeStyleSetMargin(root_child3, YGEdge.Right,  10);
            YGNodeStyleSetMargin(root_child3, YGEdge.Bottom, 10);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            Assert.AreEqual(60, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(40, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(40, root_child2.Layout.Height);

            Assert.AreEqual(60, root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(80, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(20, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(40,  root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(40,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(40,  root_child2.Layout.Height);

            Assert.AreEqual(40, root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(80,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(20,  root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_row_with_padding()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetPadding(root_child1, YGEdge.Left,   10);
            YGNodeStyleSetPadding(root_child1, YGEdge.Top,    10);
            YGNodeStyleSetPadding(root_child1, YGEdge.Right,  10);
            YGNodeStyleSetPadding(root_child1, YGEdge.Bottom, 10);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetPadding(root_child3, YGEdge.Left,   10);
            YGNodeStyleSetPadding(root_child3, YGEdge.Top,    10);
            YGNodeStyleSetPadding(root_child3, YGEdge.Right,  10);
            YGNodeStyleSetPadding(root_child3, YGEdge.Bottom, 10);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_row_with_single_row()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
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

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_row_with_fixed_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 60);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(60, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(80,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(80, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(80, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(20, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(60, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(80, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(80,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(20,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(80, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(20, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_row_with_max_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetMaxHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_row_with_min_height()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 150);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetMinHeight(root_child1, 80);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(90, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(90, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(90,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(90, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(90,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(90, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(90, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(90,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(10,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_column()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetFlexWrap(root, YGWrap.Wrap);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 150);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetFlexShrink(root_child0_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0_child0, 0);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

            YGNode root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetFlexShrink(root_child1, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1, 0);
            YGNodeStyleSetHeight(root_child1, 50);
            YGNodeInsertChild(root, root_child1, 1);

            YGNode root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child2, 50);
            YGNodeInsertChild(root, root_child2, 2);

            YGNode root_child3 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child3, 50);
            YGNodeInsertChild(root, root_child3, 3);

            YGNode root_child4 = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeight(root_child4, 50);
            YGNodeInsertChild(root, root_child4, 4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(150, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(0,   root_child3.Layout.Position.Left);
            Assert.AreEqual(100, root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(150, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(50,  root_child3.Layout.Position.Left);
            Assert.AreEqual(100, root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void align_content_stretch_is_not_overriding_align_items()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root_child0, YGAlign.Stretch);
            YGNodeStyleSetAlignItems(root_child0, YGAlign.Center);
            YGNodeStyleSetWidth(root_child0, 100);
            YGNodeStyleSetHeight(root_child0, 100);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(root_child0_child0, YGAlign.Stretch);
            YGNodeStyleSetWidth(root_child0_child0, 10);
            YGNodeStyleSetHeight(root_child0_child0, 10);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(45, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0_child0.Layout.Width);
            Assert.AreEqual(10, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(90, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(45, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0_child0.Layout.Width);
            Assert.AreEqual(10, root_child0_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }
    }
}
