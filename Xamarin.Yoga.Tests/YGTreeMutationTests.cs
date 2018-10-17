using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Yoga.Tests.Utils;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;


    [TestClass]
    public class YGTreeMutationTests
    {
        [TestMethod]
        public void set_children_adds_children_to_parent()
        {
            YGNode root        = new YGNode();
            YGNode root_child0 = new YGNode();
            YGNode root_child1 = new YGNode();

            root.SetChildren(new List<YGNode> {root_child0, root_child1});

            List<YGNode> children         = root.Children;
            List<YGNode> expectedChildren = new List<YGNode> {root_child0, root_child1};
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNode> owners = new List<YGNode>
            {
                YGNodeGetOwner(root_child0),
                YGNodeGetOwner(root_child1)
            };

            List<YGNode> expectedOwners = new List<YGNode>
            {
                root, root
            };
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void set_children_to_empty_removes_old_children()
        {
            YGNode root        = new YGNode();
            YGNode root_child0 = new YGNode();
            YGNode root_child1 = new YGNode();

            root.SetChildren(new List<YGNode> {root_child0, root_child1});
            root.SetChildren(new List<YGNode>());

            List<YGNode> children         = root.Children;
            List<YGNode> expectedChildren = new List<YGNode>();

            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNode> owners = new List<YGNode>
            {
                YGNodeGetOwner(root_child0), YGNodeGetOwner(root_child1)
            };
            List<YGNode> expectedOwners = new List<YGNode>
            {
                null, null
            };
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void set_children_replaces_non_common_children()
        {
            YGNode root        = new YGNode();
            YGNode root_child0 = new YGNode();
            YGNode root_child1 = new YGNode();

            root.SetChildren(new List<YGNode> {root_child0, root_child1});

            YGNode root_child2 = new YGNode();
            YGNode root_child3 = new YGNode();

            root.SetChildren(new List<YGNode> {root_child2, root_child3});

            List<YGNode> children         = root.Children;
            List<YGNode> expectedChildren = new List<YGNode> {root_child2, root_child3};
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNode> owners = new List<YGNode>
            {
                YGNodeGetOwner(root_child0),
                YGNodeGetOwner(root_child1)
            };
            List<YGNode> expectedOwners = new List<YGNode> {null, null};
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));

            YGNodeFreeRecursive(root);
            YGNodeFree(root_child0);
            YGNodeFree(root_child1);
        }

        [TestMethod]
        public void set_children_keeps_and_reorders_common_children()
        {
            YGNode root        = new YGNode();
            YGNode root_child0 = new YGNode();
            YGNode root_child1 = new YGNode();
            YGNode root_child2 = new YGNode();
            YGNode root_child3 = new YGNode();

            root_child0.Name = "Child0";
            root_child1.Name = "Child1";
            root_child2.Name = "Child2";
            root_child3.Name = "Child3";

            root.SetChildren(
                new List<YGNode>
                {
                    root_child0, root_child1, root_child2
                });

            root.SetChildren(
                new List<YGNode>
                {
                    root_child2, root_child1, root_child3
                });

            List<YGNode> children = root.Children;
            List<YGNode> expectedChildren = new List<YGNode>
            {
                root_child2, root_child1, root_child3
            };
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNode> owners = new List<YGNode>
            {
                YGNodeGetOwner(root_child0),
                YGNodeGetOwner(root_child1),
                YGNodeGetOwner(root_child2),
                YGNodeGetOwner(root_child3)
            };

            List<YGNode> expectedOwners = new List<YGNode> {null, root, root, root};
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));

            YGNodeFreeRecursive(root);
            YGNodeFree(root_child0);
        }
    }
}
