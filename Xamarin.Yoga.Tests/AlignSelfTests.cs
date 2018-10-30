using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class AlignSelfTests
    {
        [TestMethod]
        public void align_self_baseline()
        {
            YogaNode root_child0;
            YogaNode root_child1;
            YogaNode root_child1_child0;
            var root = new YogaNode
            {
                Style = {FlexDirection = FlexDirectionType.Row, Width = 100, Height = 100},
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        Style = {AlignSelf = AlignType.Baseline, Width = 50, Height = 50}
                    }),
                    (root_child1 = new YogaNode
                    {
                        Style = {AlignSelf = AlignType.Baseline, Width = 50, Height = 20},
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Style = {Width = 50, Height = 10}})
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_self_center()
        {
            var config = new YogaConfig();

            YogaNode root_child0;
            var root = new YogaNode(config)
            {
                Style = {Width = 100, Height = 100},
                Children =
                {
                    (root_child0 = new YogaNode(config) {Style = {AlignSelf = AlignType.Center, Width = 10, Height = 10}})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_self_flex_end()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 100;
            root.Style.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.AlignSelf = AlignType.FlexEnd;
            root_child0.Style.Width     = 10;
            root_child0.Style.Height    = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_self_flex_end_override_flex_start()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width      = 100;
            root.Style.Height     = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.AlignSelf = AlignType.FlexEnd;
            root_child0.Style.Width     = 10;
            root_child0.Style.Height    = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_self_flex_start()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 100;
            root.Style.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.AlignSelf = AlignType.FlexStart;
            root_child0.Style.Width     = 10;
            root_child0.Style.Height    = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }
    }
}
