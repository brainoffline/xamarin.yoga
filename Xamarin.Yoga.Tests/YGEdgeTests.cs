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
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, YGEdge.Start, 10);
            YGNodeStyleSetMargin(root_child0, YGEdge.Left,  20);
            YGNodeStyleSetMargin(root_child0, YGEdge.Right, 20);
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Right);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void end_overrides()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, YGEdge.End,   10);
            YGNodeStyleSetMargin(root_child0, YGEdge.Left,  20);
            YGNodeStyleSetMargin(root_child0, YGEdge.Right, 20);
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Right);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void horizontal_overridden()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, YGEdge.Horizontal, 10);
            YGNodeStyleSetMargin(root_child0, YGEdge.Left,       20);
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void vertical_overridden()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Column);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, YGEdge.Vertical, 10);
            YGNodeStyleSetMargin(root_child0, YGEdge.Top,      20);
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Position.Bottom);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void horizontal_overrides_all()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Column);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, YGEdge.Horizontal, 10);
            YGNodeStyleSetMargin(root_child0, YGEdge.All,        20);
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);
            Assert.AreEqual(20, root_child0.Layout.Position.Bottom);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void vertical_overrides_all()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Column);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, YGEdge.Vertical, 10);
            YGNodeStyleSetMargin(root_child0, YGEdge.All,      20);
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Position.Right);
            Assert.AreEqual(10, root_child0.Layout.Position.Bottom);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void all_overridden()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Column);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, YGEdge.Left,   10);
            YGNodeStyleSetMargin(root_child0, YGEdge.Top,    10);
            YGNodeStyleSetMargin(root_child0, YGEdge.Right,  10);
            YGNodeStyleSetMargin(root_child0, YGEdge.Bottom, 10);
            YGNodeStyleSetMargin(root_child0, YGEdge.All,    20);
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);
            Assert.AreEqual(10, root_child0.Layout.Position.Bottom);

            YGNodeFreeRecursive(root);
        }
    }
}
