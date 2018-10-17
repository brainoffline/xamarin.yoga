using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGHadOverflowTests
    {
        YGNode   root;
        YGConfig config;

        [TestInitialize]
        public void YogaTest_HadOverflowTests_Init()
        {
            config = new YGConfig();
            root   = new YGNode(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 100);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Column);
            YGNodeStyleSetFlexWrap(root, YGWrap.NoWrap);
        }

        [TestCleanup]
        public void YogaTest_HadOverflowTests_Cleanup()
        {
            YGNodeFreeRecursive(root);
            
        }


        [TestMethod]
        public void children_overflow_no_wrap_and_no_flex_children()
        {
            YGNode child0 = new YGNode(config);
            YGNodeStyleSetWidth(child0, 80);
            YGNodeStyleSetHeight(child0, 40);
            YGNodeStyleSetMargin(child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(child0, YGEdge.Bottom, 15);
            root.InsertChild(child0, 0);
            YGNode child1 = new YGNode(config);
            YGNodeStyleSetWidth(child1, 80);
            YGNodeStyleSetHeight(child1, 40);
            YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
            root.InsertChild(child1, 1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void spacing_overflow_no_wrap_and_no_flex_children()
        {
            YGNode child0 = new YGNode(config);
            YGNodeStyleSetWidth(child0, 80);
            YGNodeStyleSetHeight(child0, 40);
            YGNodeStyleSetMargin(child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
            root.InsertChild(child0, 0);
            YGNode child1 = new YGNode(config);
            YGNodeStyleSetWidth(child1, 80);
            YGNodeStyleSetHeight(child1, 40);
            YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
            root.InsertChild(child1, 1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void no_overflow_no_wrap_and_flex_children()
        {
            YGNode child0 = new YGNode(config);
            YGNodeStyleSetWidth(child0, 80);
            YGNodeStyleSetHeight(child0, 40);
            YGNodeStyleSetMargin(child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
            root.InsertChild(child0, 0);
            YGNode child1 = new YGNode(config);
            YGNodeStyleSetWidth(child1, 80);
            YGNodeStyleSetHeight(child1, 40);
            YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
            YGNodeStyleSetFlexShrink(child1, 1);
            root.InsertChild(child1, 1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsFalse(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void hadOverflow_gets_reset_if_not_logger_valid()
        {
            YGNode child0 = new YGNode(config);
            YGNodeStyleSetWidth(child0, 80);
            YGNodeStyleSetHeight(child0, 40);
            YGNodeStyleSetMargin(child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
            root.InsertChild(child0, 0);
            YGNode child1 = new YGNode(config);
            YGNodeStyleSetWidth(child1, 80);
            YGNodeStyleSetHeight(child1, 40);
            YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
            root.InsertChild(child1, 1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);

            YGNodeStyleSetFlexShrink(child1, 1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsFalse(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void spacing_overflow_in_nested_nodes()
        {
            YGNode child0 = new YGNode(config);
            YGNodeStyleSetWidth(child0, 80);
            YGNodeStyleSetHeight(child0, 40);
            YGNodeStyleSetMargin(child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
            root.InsertChild(child0, 0);
            YGNode child1 = new YGNode(config);
            YGNodeStyleSetWidth(child1, 80);
            YGNodeStyleSetHeight(child1, 40);
            root.InsertChild(child1, 1);
            YGNode child1_1 = new YGNode(config);
            YGNodeStyleSetWidth(child1_1, 80);
            YGNodeStyleSetHeight(child1_1, 40);
            YGNodeStyleSetMargin(child1_1, YGEdge.Bottom, 5);
            child1.InsertChild(child1_1, 0);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }
    }
}
