using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    [TestClass]
    public class YGDirtiedTests
    {
        [TestMethod]
        public void dirtied()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            int dirtiedCount = 0;
            root.setContext(dirtiedCount);
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
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0 = YGNodeNew();
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNew();
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            int dirtiedCount = 0;
            root.setContext(dirtiedCount);
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
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0 = YGNodeNew();
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNew();
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            int dirtiedCount = 0;
            root_child0.setContext(dirtiedCount);
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
