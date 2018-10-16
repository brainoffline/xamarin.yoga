using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Yoga.Tests.Utils;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    
    
    

    [TestClass]
    public class YGTreeMutationTests
    {
        static List<YGNode> getChildren(YGNode node)
        {
            var             count    = YGNodeGetChildCount(node);
            List<YGNode> children = new List<YGNode>(count);
            for (int i = 0; i < count; i++)
            {
                children.Add(YGNodeGetChild(node, i));
            }

            return children;
        }

        [TestMethod]
        public void set_children_adds_children_to_parent()
        {
            YGNode root        = YGNodeNew();
            YGNode root_child0 = YGNodeNew();
            YGNode root_child1 = YGNodeNew();

            YGNodeSetChildren(root,  new List<YGNode>{root_child0, root_child1});

            List<YGNode> children         = getChildren(root);
            List<YGNode> expectedChildren = new List<YGNode>{root_child0, root_child1};
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNode> owners = new List<YGNode>{
                    YGNodeGetOwner(root_child0),
                    YGNodeGetOwner(root_child1)};

            List<YGNode> expectedOwners = new List<YGNode>{
                root, root
            };
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void set_children_to_empty_removes_old_children()
        {
            YGNode root         = YGNodeNew();
            YGNode root_child0  = YGNodeNew();
            YGNode root_child1  = YGNodeNew();

            YGNodeSetChildren(root, new List<YGNode> {
                root_child0, root_child1
            });
            YGNodeSetChildren(root, new List<YGNode>(){ });

            List<YGNode> children         = getChildren(root);
            List<YGNode> expectedChildren = new List<YGNode>();
            ;
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNode> owners = new List<YGNode>{
                YGNodeGetOwner(root_child0), YGNodeGetOwner(root_child1)
            };
            List<YGNode> expectedOwners = new List<YGNode>{
                null, null
            };
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void set_children_replaces_non_common_children()
        {
            YGNode root         = YGNodeNew();
            YGNode root_child0  = YGNodeNew();
            YGNode root_child1  = YGNodeNew();

            YGNodeSetChildren(root, new List<YGNode> {
                root_child0, root_child1
            });

            YGNode root_child2  = YGNodeNew();
            YGNode root_child3  = YGNodeNew();

            YGNodeSetChildren(root, new List<YGNode> {
                root_child2, root_child3
            });

            List<YGNode> children         = getChildren(root);
            List<YGNode> expectedChildren = new List<YGNode>{
                root_child2, root_child3
            };
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNode> owners = new List<YGNode>{
                YGNodeGetOwner(root_child0), YGNodeGetOwner(root_child1)
            };
            List<YGNode> expectedOwners = new List<YGNode>{
                null, null};
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));

            YGNodeFreeRecursive(root);
            YGNodeFree(root_child0);
            YGNodeFree(root_child1);
        }

        [TestMethod]
        public void set_children_keeps_and_reorders_common_children()
        {
            YGNode root         = YGNodeNew();
            YGNode root_child0  = YGNodeNew();
            YGNode root_child1  = YGNodeNew();
            YGNode root_child2  = YGNodeNew();
            YGNode root_child3  = YGNodeNew();

            root_child0.Name = "Child0";
            root_child1.Name = "Child1";
            root_child2.Name = "Child2";
            root_child3.Name = "Child3";

            YGNodeSetChildren(root, new List<YGNode> {
                root_child0, root_child1, root_child2
            });

            YGNodeSetChildren(root, new List<YGNode> {
                root_child2, root_child1, root_child3
            });

            List<YGNode> children         = getChildren(root);
            List<YGNode> expectedChildren = new List<YGNode>{
                root_child2, root_child1, root_child3
            };
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNode> owners = new List<YGNode>{
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
