using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Yoga.Tests.Utils;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    
    
    

    [TestClass]
    public class YGTraversalTests
    {
        [TestMethod]
        public void pre_order_traversal()
        {
            YGNode root                = new YGNode();
            YGNode root_child0         = new YGNode();
            YGNode root_child1         = new YGNode();
            YGNode root_child0_child0  = new YGNode();

            root.SetChildren(new [] { root_child0, root_child1}.ToList());
            root_child0.InsertChild(root_child0_child0);

            List<YGNode> visited = new List<YGNode>();
            YGTraversePreOrder(root, node => visited.Add(node));

            List<YGNode> expected = new[]{
                root,
                root_child0,
                root_child0_child0,
                root_child1
            }.ToList();

            var areEqual = TestHelper.AreEqual(expected, visited);
            Assert.IsTrue(areEqual);

            
        }
    }
}
