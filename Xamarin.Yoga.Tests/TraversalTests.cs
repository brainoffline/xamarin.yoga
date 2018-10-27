using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Yoga.Tests.Utils;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class TraversalTests
    {
        [TestMethod]
        public void pre_order_traversal()
        {
            YGNode root_child0;
            YGNode root_child0_child0;
            YGNode root_child1;
            var root = new YGNode
            {
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Children =
                        {
                            (root_child0_child0 = new YGNode())
                        }
                    }),
                    (root_child1 = new YGNode())
                }
            };

            var visited = new List<YGNode>();
            root.Traverse(node => visited.Add(node));

            var expected = new[]
            {
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
