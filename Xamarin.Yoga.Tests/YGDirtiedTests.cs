using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGDirtiedTests
    {
        [TestMethod]
        public void dirtied()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            int dirtiedCount = 0;
            root.Context = dirtiedCount ;
            root.setDirtiedFunc(n => { dirtiedCount++; });

            Assert.AreEqual(0, dirtiedCount);

            // `_dirtied` MUST be called in case of explicit dirtying.
            root.IsDirty = true;
            Assert.AreEqual(1, dirtiedCount);

            // `_dirtied` MUST be called ONCE.
            root.IsDirty = true;
            Assert.AreEqual(1, dirtiedCount);
        }

        [TestMethod]
        public void dirtied_propagation()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 20);
            root.InsertChild(root_child0, 0);

            YGNode root_child1 = new YGNode();
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 20);
            root.InsertChild(root_child1, 1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            int dirtiedCount = 0;
            root.Context = dirtiedCount;
            root.setDirtiedFunc(n => { dirtiedCount++; });

            Assert.AreEqual(0, dirtiedCount);

            // `_dirtied` MUST be called for the first time.
            root_child0.markDirtyAndPropogate();
            Assert.AreEqual(1, dirtiedCount);

            // `_dirtied` must NOT be called for the second time.
            root_child0.markDirtyAndPropogate();
            Assert.AreEqual(1, dirtiedCount);
        }

        [TestMethod]
        public void dirtied_hierarchy()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 20);
            root.InsertChild(root_child0, 0);

            YGNode root_child1 = new YGNode();
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 20);
            root.InsertChild(root_child1, 1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            int dirtiedCount = 0;
            root_child0.Context = dirtiedCount;
            root_child0.setDirtiedFunc(n => { dirtiedCount++; });

            Assert.AreEqual(0, dirtiedCount);

            // `_dirtied` must NOT be called for descendants.
            root.markDirtyAndPropogate();
            Assert.AreEqual(0, dirtiedCount);

            // `_dirtied` must NOT be called for the sibling node.
            root_child1.markDirtyAndPropogate();
            Assert.AreEqual(0, dirtiedCount);

            // `_dirtied` MUST be called in case of explicit dirtying.
            root_child0.markDirtyAndPropogate();
            Assert.AreEqual(1, dirtiedCount);
        }
    }
}
