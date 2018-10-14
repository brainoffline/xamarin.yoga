using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Yoga.Tests.Utils;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    [TestClass]
    public class YGTreeMutationTests
    {
        static List<YGNodeRef> getChildren(YGNodeRef node)
        {
            var             count    = YGNodeGetChildCount(node);
            List<YGNodeRef> children = new YGVector(count);
            for (int i = 0; i < count; i++)
            {
                children.Add(YGNodeGetChild(node, i));
            }

            return children;
        }

        [TestMethod]
        public void set_children_adds_children_to_parent()
        {
            YGNodeRef root        = YGNodeNew();
            YGNodeRef root_child0 = YGNodeNew();
            YGNodeRef root_child1 = YGNodeNew();

            YGNodeSetChildren(root,  new YGVector{root_child0, root_child1});

            List<YGNodeRef> children         = getChildren(root);
            List<YGNodeRef> expectedChildren = new YGVector{root_child0, root_child1};
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNodeRef> owners = new YGVector{
                    YGNodeGetOwner(root_child0),
                    YGNodeGetOwner(root_child1)};

            List<YGNodeRef> expectedOwners = new YGVector{
                root, root
            };
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void set_children_to_empty_removes_old_children()
        {
            YGNodeRef root         = YGNodeNew();
            YGNodeRef root_child0  = YGNodeNew();
            YGNodeRef root_child1  = YGNodeNew();

            YGNodeSetChildren(root, new YGVector {
                root_child0, root_child1
            });
            YGNodeSetChildren(root, new YGVector(){ });

            List<YGNodeRef> children         = getChildren(root);
            List<YGNodeRef> expectedChildren = new YGVector();
            ;
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNodeRef> owners = new YGVector{
                YGNodeGetOwner(root_child0), YGNodeGetOwner(root_child1)
            };
            List<YGNodeRef> expectedOwners = new YGVector{
                null, null
            };
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void set_children_replaces_non_common_children()
        {
            YGNodeRef root         = YGNodeNew();
            YGNodeRef root_child0  = YGNodeNew();
            YGNodeRef root_child1  = YGNodeNew();

            YGNodeSetChildren(root, new YGVector {
                root_child0, root_child1
            });

            YGNodeRef root_child2  = YGNodeNew();
            YGNodeRef root_child3  = YGNodeNew();

            YGNodeSetChildren(root, new YGVector {
                root_child2, root_child3
            });

            List<YGNodeRef> children         = getChildren(root);
            List<YGNodeRef> expectedChildren = new YGVector{
                root_child2, root_child3
            };
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNodeRef> owners = new YGVector{
                YGNodeGetOwner(root_child0), YGNodeGetOwner(root_child1)
            };
            List<YGNodeRef> expectedOwners = new YGVector{
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
            YGNodeRef root         = YGNodeNew();
            YGNodeRef root_child0  = YGNodeNew();
            YGNodeRef root_child1  = YGNodeNew();
            YGNodeRef root_child2  = YGNodeNew();
            YGNodeRef root_child3  = YGNodeNew();

            root_child0.Name = "Child0";
            root_child1.Name = "Child1";
            root_child2.Name = "Child2";
            root_child3.Name = "Child3";

            YGNodeSetChildren(root, new YGVector {
                root_child0, root_child1, root_child2
            });

            YGNodeSetChildren(root, new YGVector {
                root_child2, root_child1, root_child3
            });

            List<YGNodeRef> children         = getChildren(root);
            List<YGNodeRef> expectedChildren = new YGVector{
                root_child2, root_child1, root_child3
            };
            Assert.IsTrue(
                TestHelper.AreEqual(children, expectedChildren));

            List<YGNodeRef> owners = new YGVector{
                YGNodeGetOwner(root_child0),
                YGNodeGetOwner(root_child1),
                YGNodeGetOwner(root_child2),
                YGNodeGetOwner(root_child3)
            };

            List<YGNodeRef> expectedOwners = new YGVector {null, root, root, root};
            Assert.IsTrue(
                TestHelper.AreEqual(owners, expectedOwners));

            YGNodeFreeRecursive(root);
            YGNodeFree(root_child0);
        }
    }
}
