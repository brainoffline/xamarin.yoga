using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGInfiniteHeightTests
    {
        // This test isn't correct from the Flexbox standard standpoint,
        // because percentages are calculated with parent constraints.
        // However, we need to make sure we fail gracefully in this case, not returning NaN
        [TestMethod]
        public void percent_absolute_position_infinite_height()
        {
             YGConfig config = new YGConfig();

             YGNode root = new YGNode(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 300);

             YGNode root_child0 = new YGNode(config);
            YGNodeStyleSetWidth(root_child0, 300);
            YGNodeStyleSetHeight(root_child0, 300);
            root.InsertChild(root_child0, 0);

             YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetPositionType(root_child1, YGPositionType.Absolute);
            YGNodeStyleSetPositionPercent(root_child1, YGEdge.Left, 20);
            YGNodeStyleSetPositionPercent(root_child1, YGEdge.Top,  20);
            YGNodeStyleSetWidthPercent(root_child1, 20);
            YGNodeStyleSetHeightPercent(root_child1, 20);
            root.InsertChild(root_child1, 1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(300, root.Layout.Width);
            Assert.AreEqual(300, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(300, root_child0.Layout.Width);
            Assert.AreEqual(300, root_child0.Layout.Height);

            Assert.AreEqual(60, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(60, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

    }
}
