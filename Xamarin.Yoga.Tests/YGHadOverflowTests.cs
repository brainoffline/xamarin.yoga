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
            root.StyleSetWidth(200);
            root.StyleSetHeight(100);
            root.StyleSetFlexDirection(YGFlexDirection.Column);
            root.StyleSetFlexWrap(YGWrap.NoWrap);
        }

        [TestCleanup]
        public void YogaTest_HadOverflowTests_Cleanup() { }


        [TestMethod]
        public void children_overflow_no_wrap_and_no_flex_children()
        {
            YGNode child0 = new YGNode(config);
            child0.StyleSetWidth(80);
            child0.StyleSetHeight(40);
            child0.StyleSetMargin(YGEdge.Top,    10);
            child0.StyleSetMargin(YGEdge.Bottom, 15);
            root.InsertChild(child0);
            YGNode child1 = new YGNode(config);
            child1.StyleSetWidth(80);
            child1.StyleSetHeight(40);
            child1.StyleSetMargin(YGEdge.Bottom, 5);
            root.InsertChild(1, child1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void spacing_overflow_no_wrap_and_no_flex_children()
        {
            YGNode child0 = new YGNode(config);
            child0.StyleSetWidth(80);
            child0.StyleSetHeight(40);
            child0.StyleSetMargin(YGEdge.Top,    10);
            child0.StyleSetMargin(YGEdge.Bottom, 10);
            root.InsertChild(child0);
            YGNode child1 = new YGNode(config);
            child1.StyleSetWidth(80);
            child1.StyleSetHeight(40);
            child1.StyleSetMargin(YGEdge.Bottom, 5);
            root.InsertChild(1, child1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void no_overflow_no_wrap_and_flex_children()
        {
            YGNode child0 = new YGNode(config);
            child0.StyleSetWidth(80);
            child0.StyleSetHeight(40);
            child0.StyleSetMargin(YGEdge.Top,    10);
            child0.StyleSetMargin(YGEdge.Bottom, 10);
            root.InsertChild(child0);
            YGNode child1 = new YGNode(config);
            child1.StyleSetWidth(80);
            child1.StyleSetHeight(40);
            child1.StyleSetMargin(YGEdge.Bottom, 5);
            child1.StyleSetFlexShrink(1);
            root.InsertChild(1, child1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsFalse(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void hadOverflow_gets_reset_if_not_logger_valid()
        {
            YGNode child0 = new YGNode(config);
            child0.StyleSetDimensions(80, 40);
            child0.StyleSetMargin(YGEdge.Top,    10);
            child0.StyleSetMargin(YGEdge.Bottom, 10);
            root.InsertChild(child0);
            YGNode child1 = new YGNode(config);
            child1.StyleSetDimensions(80, 40);
            child1.StyleSetMargin(YGEdge.Bottom, 5);
            root.InsertChild(1, child1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);

            child1.StyleSetFlexShrink(1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsFalse(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void spacing_overflow_in_nested_nodes()
        {
            YGNode child0 = new YGNode(config);
            child0.StyleSetDimensions(80, 40);
            child0.StyleSetMargin(YGEdge.Top,    10);
            child0.StyleSetMargin(YGEdge.Bottom, 10);
            root.InsertChild(child0);
            YGNode child1 = new YGNode(config);
            child1.StyleSetDimensions(80, 40);
            root.InsertChild(1, child1);
            YGNode child1_1 = new YGNode(config);
            child1_1.StyleSetDimensions(80, 40);
            child1_1.StyleSetMargin(YGEdge.Bottom, 5);
            child1.InsertChild(child1_1);

            YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }
    }
}
