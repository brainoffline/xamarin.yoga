﻿using System;
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
    public class YGPersistenceTests
    {
        [TestMethod]
        public void cloning_shared_root()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(75,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            YGNodeRef root2 = YGNodeClone(root);
            YGNodeStyleSetWidth(root2, 100);

            Assert.AreEqual(2, YGNodeGetChildCount(root2));
            // The children should have referential equality at this point.
            Assert.AreEqual(root_child0, YGNodeGetChild(root2, 0));
            Assert.AreEqual(root_child1, YGNodeGetChild(root2, 1));

            YGNodeCalculateLayout(root2, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(2, YGNodeGetChildCount(root2));
            // Relayout with no changed input should result in referential equality.
            Assert.AreEqual(root_child0, YGNodeGetChild(root2, 0));
            Assert.AreEqual(root_child1, YGNodeGetChild(root2, 1));

            YGNodeStyleSetWidth(root2, 150);
            YGNodeStyleSetHeight(root2, 200);
            YGNodeCalculateLayout(root2, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(2, YGNodeGetChildCount(root2));
            // Relayout with changed input should result in cloned children.
            YGNodeRef root2_child0 = YGNodeGetChild(root2, 0);
            YGNodeRef root2_child1 = YGNodeGetChild(root2, 1);
            Assert.AreNotEqual(root_child0, root2_child0);
            Assert.AreNotEqual(root_child1, root2_child1);

            // Everything in the root should remain unchanged.
            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(75,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            // The new root now has new layout.
            Assert.AreEqual(0,   root2.Layout.Position.Left);
            Assert.AreEqual(0,   root2.Layout.Position.Top);
            Assert.AreEqual(150, root2.Layout.Width);
            Assert.AreEqual(200, root2.Layout.Height);

            Assert.AreEqual(0,   root2_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root2_child0.Layout.Position.Top);
            Assert.AreEqual(150, root2_child0.Layout.Width);
            Assert.AreEqual(125, root2_child0.Layout.Height);

            Assert.AreEqual(0,   root2_child1.Layout.Position.Left);
            Assert.AreEqual(125, root2_child1.Layout.Position.Top);
            Assert.AreEqual(150, root2_child1.Layout.Width);
            Assert.AreEqual(75,  root2_child1.Layout.Height);

            YGNodeFreeRecursive(root2);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [Ignore("Clone works differently in .Net implementation")]
        [TestMethod]
        public void mutating_children_of_a_clone_clones()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            Assert.AreEqual(0, YGNodeGetChildCount(root));

            YGNodeRef root2 = YGNodeClone(root);
            Assert.AreEqual(0, YGNodeGetChildCount(root2));

            YGNodeRef root2_child0 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root2, root2_child0, 0);

            Assert.AreEqual(0, YGNodeGetChildCount(root));
            Assert.AreEqual(1, YGNodeGetChildCount(root2));

            YGNodeRef root3 = YGNodeClone(root2);
            Assert.AreEqual(1,                        YGNodeGetChildCount(root2));
            Assert.AreEqual(1,                        YGNodeGetChildCount(root3));
            Assert.AreEqual(YGNodeGetChild(root2, 0), YGNodeGetChild(root3, 0));

            YGNodeRef root3_child1 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root3, root3_child1, 1);
            Assert.AreEqual(1,            YGNodeGetChildCount(root2));
            Assert.AreEqual(2,            YGNodeGetChildCount(root3));
            Assert.AreEqual(root3_child1, YGNodeGetChild(root3, 1));
            Assert.AreNotEqual(YGNodeGetChild(root2,            0), YGNodeGetChild(root3, 0));

            YGNodeRef root4 = YGNodeClone(root3);
            Assert.AreEqual(root3_child1, YGNodeGetChild(root4, 1));

            YGNodeRemoveChild(root4, root3_child1);
            Assert.AreEqual(2, YGNodeGetChildCount(root3));
            Assert.AreEqual(1, YGNodeGetChildCount(root4));
            Assert.AreNotEqual(YGNodeGetChild(root3, 0), YGNodeGetChild(root4, 0));

            YGNodeFreeRecursive(root4);
            YGNodeFreeRecursive(root3);
            YGNodeFreeRecursive(root2);
            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void cloning_two_levels()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 15);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeInsertChild(root, root_child1, 1);

            YGNodeRef root_child1_0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexBasis(root_child1_0, 10);
            YGNodeStyleSetFlexGrow(root_child1_0, 1);
            YGNodeInsertChild(root_child1, root_child1_0, 0);

            YGNodeRef root_child1_1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexBasis(root_child1_1, 25);
            YGNodeInsertChild(root_child1, root_child1_1, 1);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(40, root_child0.Layout.Height);
            Assert.AreEqual(60, root_child1.Layout.Height);
            Assert.AreEqual(35, root_child1_0.Layout.Height);
            Assert.AreEqual(25, root_child1_1.Layout.Height);

            YGNodeRef root2_child0 = YGNodeClone(root_child0);
            YGNodeRef root2_child1 = YGNodeClone(root_child1);
            YGNodeRef root2        = YGNodeClone(root);

            YGNodeStyleSetFlexGrow(root2_child0, 0);
            YGNodeStyleSetFlexBasis(root2_child0, 40);

            YGNodeRemoveAllChildren(root2);
            YGNodeInsertChild(root2, root2_child0, 0);
            YGNodeInsertChild(root2, root2_child1, 1);
            Assert.AreEqual(2, YGNodeGetChildCount(root2));

            YGNodeCalculateLayout(root2, YGUndefined, YGUndefined, YGDirection.LTR);

            // Original root is unchanged
            Assert.AreEqual(40, root_child0.Layout.Height);
            Assert.AreEqual(60, root_child1.Layout.Height);
            Assert.AreEqual(35, root_child1_0.Layout.Height);
            Assert.AreEqual(25, root_child1_1.Layout.Height);

            // New root has new layout at the top
            Assert.AreEqual(40, root2_child0.Layout.Height);
            Assert.AreEqual(60, root2_child1.Layout.Height);

            // The deeper children are untouched.
            Assert.AreEqual(YGNodeGetChild(root2_child1, 0), root_child1_0);
            Assert.AreEqual(YGNodeGetChild(root2_child1, 1), root_child1_1);

            YGNodeFreeRecursive(root2);
            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void cloning_and_freeing()
        {
            int initialInstanceCount = YGNodeGetInstanceCount();

            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);
            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root, root_child1, 1);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            YGNodeRef root2 = YGNodeClone(root);

            // Freeing the original root should be safe as long as we don't free its
            // children.
            YGNodeFree(root);

            YGNodeCalculateLayout(root2, YGUndefined, YGUndefined, YGDirection.LTR);

            YGNodeFreeRecursive(root2);

            YGNodeFree(root_child0);
            YGNodeFree(root_child1);

            YGConfigFree(config);

            Assert.AreEqual(initialInstanceCount, YGNodeGetInstanceCount());
        }
    }
}