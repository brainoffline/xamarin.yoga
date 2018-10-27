using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YogaConst;
    
    
    

    [TestClass]
    public class YGLayoutDiffingTests
    {
        [TestMethod]
        public void assert_layout_trees_are_same()
        {
            var       config = new YogaConfig();
            YGNode root1  = new YGNode(config);
            root1.Style.Width = 500;
            root1.Style.Height = 500;

            YGNode root1_child0 = new YGNode(config);
            root1_child0.Style.AlignItems = YGAlign.FlexStart;
            root1.Children.Add(root1_child0);

            YGNode root1_child0_child0 = new YGNode(config);
            root1_child0_child0.Style.FlexGrow =  1;
            root1_child0_child0.Style.FlexShrink =  1;
            root1_child0.Children.Add(root1_child0_child0);

            YGNode root1_child0_child0_child0 = new YGNode(config);
            root1_child0_child0_child0.Style.FlexGrow = 1;
            root1_child0_child0_child0.Style.FlexShrink = 1;
            root1_child0_child0.Children.Add(root1_child0_child0_child0);

            root1.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            YGNode root2 = new YGNode(config);
            root2.Style.Width = 500;
            root2.Style.Height = 500;

            YGNode root2_child0 = new YGNode(config);
            root2_child0.Style.AlignItems = YGAlign.FlexStart;
            root2.Children.Add(root2_child0);

            YGNode root2_child0_child0 = new YGNode(config);
            root2_child0_child0.Style.FlexGrow = 1;
            root2_child0_child0.Style.FlexShrink = 1;
            root2_child0.Children.Add(root2_child0_child0);

            YGNode root2_child0_child0_child0 = new YGNode(config);
            root2_child0_child0_child0.Style.FlexGrow = 1;
            root2_child0_child0_child0.Style.FlexShrink = 1;
            root2_child0_child0.Children.Add(root2_child0_child0_child0);

            root2.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.IsTrue(root1.IsLayoutTreeEqualToNode(root2));
            //Assert.IsTrue(root1.isLayoutTreeEqualToNode(*root2));

            root2.Style.AlignItems = YGAlign.FlexEnd;

            root2.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.IsFalse(root1.IsLayoutTreeEqualToNode(root2));
            //Assert.IsFalse(root1.isLayoutTreeEqualToNode(*root2));
        }
    }
}
