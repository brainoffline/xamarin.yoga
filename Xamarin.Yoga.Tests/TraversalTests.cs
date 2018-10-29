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
            YogaNode root_child0;
            YogaNode root_child0_child0;
            YogaNode root_child1;
            var root = new YogaNode
            {
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        Children =
                        {
                            (root_child0_child0 = new YogaNode())
                        }
                    }),
                    (root_child1 = new YogaNode())
                }
            };

            var visited = new List<YogaNode>();
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
