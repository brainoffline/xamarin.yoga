﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Yoga.Tests.Utils;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class TreeMutationTests
    {
        [TestMethod]
        public void set_children_adds_children_to_parent()
        {
            var root        = new YogaNode();
            var root_child0 = new YogaNode();
            var root_child1 = new YogaNode();

            root.SetChildren(new List<YogaNode> {root_child0, root_child1});

            var children         = root.Children;
            var expectedChildren = new List<YogaNode> {root_child0, root_child1};
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            var owners = new List<YogaNode>
            {
                root_child0.Owner,
                root_child1.Owner
            };

            var expectedOwners = new List<YogaNode>
            {
                root, root
            };
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));
        }

        [TestMethod]
        public void set_children_keeps_and_reorders_common_children()
        {
            var root        = new YogaNode();
            var root_child0 = new YogaNode();
            var root_child1 = new YogaNode();
            var root_child2 = new YogaNode();
            var root_child3 = new YogaNode();

            root_child0.Name = "Child0";
            root_child1.Name = "Child1";
            root_child2.Name = "Child2";
            root_child3.Name = "Child3";

            root.SetChildren(
                new List<YogaNode>
                {
                    root_child0, root_child1, root_child2
                });

            root.SetChildren(
                new List<YogaNode>
                {
                    root_child2, root_child1, root_child3
                });

            var children = root.Children;
            var expectedChildren = new List<YogaNode>
            {
                root_child2, root_child1, root_child3
            };
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            var owners = new List<YogaNode>
            {
                root_child0.Owner,
                root_child1.Owner,
                root_child2.Owner,
                root_child3.Owner
            };

            var expectedOwners = new List<YogaNode> {null, root, root, root};
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));
        }

        [TestMethod]
        public void set_children_replaces_non_common_children()
        {
            var root        = new YogaNode();
            var root_child0 = new YogaNode();
            var root_child1 = new YogaNode();

            root.SetChildren(new List<YogaNode> {root_child0, root_child1});

            var root_child2 = new YogaNode();
            var root_child3 = new YogaNode();

            root.SetChildren(new List<YogaNode> {root_child2, root_child3});

            var children         = root.Children;
            var expectedChildren = new List<YogaNode> {root_child2, root_child3};
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            var owners = new List<YogaNode>
            {
                root_child0.Owner,
                root_child1.Owner
            };
            var expectedOwners = new List<YogaNode> {null, null};
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));
        }

        [TestMethod]
        public void set_children_to_empty_removes_old_children()
        {
            var root        = new YogaNode();
            var root_child0 = new YogaNode();
            var root_child1 = new YogaNode();

            root.SetChildren(new List<YogaNode> {root_child0, root_child1});
            root.SetChildren(new List<YogaNode>());

            var children         = root.Children;
            var expectedChildren = new List<YogaNode>();

            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            var owners = new List<YogaNode>
            {
                root_child0.Owner, root_child1.Owner
            };
            var expectedOwners = new List<YogaNode>
            {
                null, null
            };
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));
        }
    }
}
