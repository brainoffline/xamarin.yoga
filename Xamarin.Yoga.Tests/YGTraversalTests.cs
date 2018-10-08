using System;
using System.Collections.Generic;
using System.Linq;
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
    public class YGTraversalTests
    {
        [TestMethod]
        public void pre_order_traversal()
        {
            YGNodeRef root                = YGNodeNew();
            YGNodeRef root_child0         = YGNodeNew();
            YGNodeRef root_child1         = YGNodeNew();
            YGNodeRef root_child0_child0  = YGNodeNew();

            YGNodeSetChildren(root, new [] { root_child0, root_child1}.ToList());
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

            List<YGNodeRef> visited = new YGVector();
            YGTraversePreOrder(root, node => visited.Add(node));

            List<YGNodeRef> expected = new[]{
                root,
                root_child0,
                root_child0_child0,
                root_child1
            }.ToList();

            var areEqual = TestHelper.AreEqual(expected, visited);
            Assert.IsTrue(areEqual);

            YGNodeFreeRecursive(root);
        }
    }
}
