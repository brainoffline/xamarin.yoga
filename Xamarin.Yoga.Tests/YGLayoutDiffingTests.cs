using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGLayoutDiffingTests
    {
        [TestMethod]
        public void assert_layout_trees_are_same()
        {
            var       config = new YGConfig();
            YGNode root1  = new YGNode(config);
            YGNodeStyleSetWidth(root1, 500);
            YGNodeStyleSetHeight(root1, 500);

            YGNode root1_child0 = new YGNode(config);
            root1_child0.StyleSetAlignItems(YGAlign.FlexStart);
            root1.InsertChild(root1_child0);

            YGNode root1_child0_child0 = new YGNode(config);
            root1_child0_child0.StyleSetFlexGrow( 1);
            root1_child0_child0.StyleSetFlexShrink( 1);
            root1_child0.InsertChild(root1_child0_child0);

            YGNode root1_child0_child0_child0 = new YGNode(config);
            root1_child0_child0_child0.StyleSetFlexGrow(1);
            root1_child0_child0_child0.StyleSetFlexShrink(1);
            root1_child0_child0.InsertChild(root1_child0_child0_child0);

            YGNodeCalculateLayout(root1, float.NaN, float.NaN, YGDirection.LTR);

            YGNode root2 = new YGNode(config);
            YGNodeStyleSetWidth(root2, 500);
            YGNodeStyleSetHeight(root2, 500);

            YGNode root2_child0 = new YGNode(config);
            root2_child0.StyleSetAlignItems(YGAlign.FlexStart);
            root2.InsertChild(root2_child0);

            YGNode root2_child0_child0 = new YGNode(config);
            root2_child0_child0.StyleSetFlexGrow(1);
            root2_child0_child0.StyleSetFlexShrink(1);
            root2_child0.InsertChild(root2_child0_child0);

            YGNode root2_child0_child0_child0 = new YGNode(config);
            root2_child0_child0_child0.StyleSetFlexGrow(1);
            root2_child0_child0_child0.StyleSetFlexShrink(1);
            root2_child0_child0.InsertChild(root2_child0_child0_child0);

            YGNodeCalculateLayout(root2, float.NaN, float.NaN, YGDirection.LTR);

            Assert.IsTrue(root1.isLayoutTreeEqualToNode(root2));
            //Assert.IsTrue(root1.isLayoutTreeEqualToNode(*root2));

            root2.StyleSetAlignItems(YGAlign.FlexEnd);

            YGNodeCalculateLayout(root2, float.NaN, float.NaN, YGDirection.LTR);

            Assert.IsFalse(root1.isLayoutTreeEqualToNode(root2));
            //Assert.IsFalse(root1.isLayoutTreeEqualToNode(*root2));
        }
    }
}
