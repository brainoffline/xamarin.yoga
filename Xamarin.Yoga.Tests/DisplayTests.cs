using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class DisplayTests
    {
        [TestMethod]
        public void display_none()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow = 1;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1;
            root_child1.Display  = DisplayType.None;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(0, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(0, root_child1.Layout.Height);
        }

        [TestMethod]
        public void display_none_fixed_size()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow = 1;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width   = 20;
            root_child1.Height  = 20;
            root_child1.Display = DisplayType.None;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(0, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(0, root_child1.Layout.Height);
        }

        [TestMethod]
        public void display_none_with_child()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow   = 1;
            root_child0.FlexShrink = 1;
            root_child0.FlexBasis  = 0.Percent();
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow   = 1;
            root_child1.FlexShrink = 1;
            root_child1.FlexBasis  = 0.Percent();
            root_child1.Display    = DisplayType.None;
            root.Children.Insert(1, root_child1);

            var root_child1_child0 = new YogaNode(config);
            root_child1_child0.FlexGrow   = 1;
            root_child1_child0.FlexShrink = 1;
            root_child1_child0.FlexBasis  = 0.Percent();
            root_child1_child0.Width      = 20;
            root_child1_child0.MinWidth   = 0;
            root_child1_child0.MinHeight  = 0;
            root_child1.Children.Add(root_child1_child0);

            var root_child2 = new YogaNode(config);
            root_child2.FlexGrow   = 1;
            root_child2.FlexShrink = 1;
            root_child2.FlexBasis  = 0.Percent();
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(0, root_child1.Layout.Height);

            Assert.AreEqual(0, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child1_child0.Layout.Width);
            Assert.AreEqual(0, root_child1_child0.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(0, root_child1.Layout.Height);

            Assert.AreEqual(0, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child1_child0.Layout.Width);
            Assert.AreEqual(0, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void display_none_with_margin()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Margin  = new Edges(10, 10, 10, 10);
            root_child0.Width   = 20;
            root_child0.Width   = 20;
            root_child0.Display = DisplayType.None;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(0, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0.Layout.Width);
            Assert.AreEqual(0, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void display_none_with_position()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow = 1;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow     = 1;
            root_child1.Position.Top = 10;
            root_child1.Display      = DisplayType.None;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(0, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0, root_child1.Layout.Position.Left);
            Assert.AreEqual(0, root_child1.Layout.Position.Top);
            Assert.AreEqual(0, root_child1.Layout.Width);
            Assert.AreEqual(0, root_child1.Layout.Height);
        }
    }
}
