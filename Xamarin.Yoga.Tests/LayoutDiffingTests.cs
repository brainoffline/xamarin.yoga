using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class LayoutDiffingTests
    {
        [TestMethod]
        public void assert_layout_trees_are_same()
        {
            var config = new YogaConfig();
            var root1  = new YogaNode(config);
            root1.Width  = 500;
            root1.Height = 500;

            var root1_child0 = new YogaNode(config);
            root1_child0.AlignItems = AlignType.FlexStart;
            root1.Children.Add(root1_child0);

            var root1_child0_child0 = new YogaNode(config);
            root1_child0_child0.FlexGrow   = 1;
            root1_child0_child0.FlexShrink = 1;
            root1_child0.Children.Add(root1_child0_child0);

            var root1_child0_child0_child0 = new YogaNode(config);
            root1_child0_child0_child0.FlexGrow   = 1;
            root1_child0_child0_child0.FlexShrink = 1;
            root1_child0_child0.Children.Add(root1_child0_child0_child0);

            root1.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            var root2 = new YogaNode(config);
            root2.Width  = 500;
            root2.Height = 500;

            var root2_child0 = new YogaNode(config);
            root2_child0.AlignItems = AlignType.FlexStart;
            root2.Children.Add(root2_child0);

            var root2_child0_child0 = new YogaNode(config);
            root2_child0_child0.FlexGrow   = 1;
            root2_child0_child0.FlexShrink = 1;
            root2_child0.Children.Add(root2_child0_child0);

            var root2_child0_child0_child0 = new YogaNode(config);
            root2_child0_child0_child0.FlexGrow   = 1;
            root2_child0_child0_child0.FlexShrink = 1;
            root2_child0_child0.Children.Add(root2_child0_child0_child0);

            root2.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.IsTrue(root1.IsLayoutTreeEqualToNode(root2));
            //Assert.IsTrue(root1.isLayoutTreeEqualToNode(*root2));

            root2.AlignItems = AlignType.FlexEnd;

            root2.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.IsFalse(root1.IsLayoutTreeEqualToNode(root2));
            //Assert.IsFalse(root1.isLayoutTreeEqualToNode(*root2));
        }
    }
}
