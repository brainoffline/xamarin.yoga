using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class HadOverflowTests
    {
        private YogaNode   root;


        [TestMethod]
        public void children_overflow_no_wrap_and_no_flex_children()
        {
            var child0 = new YogaNode
            {
                Width = 80, Height = 40, Margin = {Top = 10, Bottom = 15}
            };
            root.Children.Add(child0);

            var child1 = new YogaNode
            {
                Width = 80, Height = 40, Margin = {Bottom = 5}
            };
            root.Children.Insert(1, child1);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void hadOverflow_gets_reset_if_not_logger_valid()
        {
            var child0 = new YogaNode
            {
                Width = 80, Height = 40, Margin = {Top = 10, Bottom = 10}
            };
            root.Children.Add(child0);

            var child1 = new YogaNode
            {
                Width = 80, Height = 40, Margin = {Bottom = 5}
            };
            root.Children.Insert(1, child1);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);

            child1.FlexShrink = 1;

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsFalse(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void no_overflow_no_wrap_and_flex_children()
        {
            var child0 = new YogaNode
            {
                Width = 80, Height = 40, Margin = {Top = 10, Bottom = 10}
            };
            root.Children.Add(child0);

            var child1 = new YogaNode
            {
                Width = 80, Height = 40, Margin = {Bottom = 5}, FlexShrink = 1
            };
            root.Children.Insert(1, child1);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsFalse(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void spacing_overflow_in_nested_nodes()
        {
            var child0 = new YogaNode
            {
                Width = 80, Height = 40, Margin = {Top = 10, Bottom = 10}
            };
            root.Children.Add(child0);

            var child1 = new YogaNode
            {
                Width = 80, Height = 40
            };
            root.Children.Insert(1, child1);

            var child1_1 = new YogaNode
            {
                Width = 80, Height = 40, Margin = {Bottom = 5}
            };
            child1.Children.Add(child1_1);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void spacing_overflow_no_wrap_and_no_flex_children()
        {
            var child0 = new YogaNode
            {
                Width = 80, Height = 40, Margin = {Top = 10, Bottom = 10}
            };
            root.Children.Add(child0);

            var child1 = new YogaNode
            {
                Width = 80, Height = 40, Margin = {Bottom = 5}
            };
            root.Children.Insert(1, child1);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }

        [TestCleanup]
        public void YogaTest_HadOverflowTests_Cleanup() { }

        [TestInitialize]
        public void YogaTest_HadOverflowTests_Init()
        {
            root = new YogaNode
            {
                
                
                    Width = 200,
                    Height = 100,
                    FlexDirection = FlexDirectionType.Column,
                    FlexWrap = WrapType.NoWrap
                
            };
        }
    }
}
