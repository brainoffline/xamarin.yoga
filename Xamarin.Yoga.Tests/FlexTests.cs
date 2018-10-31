using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class FlexTests
    {
        [TestMethod]
        public void flex_basis_flex_grow_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(75,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(75,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_basis_flex_grow_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(75,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(25,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(75,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(25,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_basis_flex_shrink_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexShrink = 1;
            root_child0.FlexBasis  = 100;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexBasis = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(50,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(50,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_basis_flex_shrink_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexShrink = 1;
            root_child0.FlexBasis  = 100;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexBasis = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_basis_overrides_main_size()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 50;
            root_child0.Height    = 20;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1;
            root_child1.Height   = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.FlexGrow = 1;
            root_child2.Height   = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(60,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(60,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(20,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(80,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(60,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(60,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(20,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(80,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void flex_grow_less_than_factor_one()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200;
            root.Height = 500;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 0.2f;
            root_child0.FlexBasis = 40;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 0.2f;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.FlexGrow = 0.4f;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(132, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(132, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(92,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(224, root_child2.Layout.Position.Top);
            Assert.AreEqual(200, root_child2.Layout.Width);
            Assert.AreEqual(184, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(132, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(132, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(92,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(224, root_child2.Layout.Position.Top);
            Assert.AreEqual(200, root_child2.Layout.Width);
            Assert.AreEqual(184, root_child2.Layout.Height);
        }

        [TestMethod]
        public void flex_grow_shrink_at_most()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode(config);
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.FlexGrow   = 1;
            root_child0_child0.FlexShrink = 1;
            root_child0.Children.Add(root_child0_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void flex_shrink_flex_grow_child_flex_shrink_other_child()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 500;
            root.Height        = 500;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow   = 0;
            root_child0.FlexShrink = 1;
            root_child0.Width      = 500;
            root_child0.Height     = 100;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow   = 1;
            root_child1.FlexShrink = 1;
            root_child1.Width      = 500;
            root_child1.Height     = 100;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(250, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(250, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(250, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_shrink_flex_grow_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 500;
            root.Height        = 500;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow   = 0;
            root_child0.FlexShrink = 1;
            root_child0.Width      = 500;
            root_child0.Height     = 100;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow   = 0;
            root_child1.FlexShrink = 1;
            root_child1.Width      = 500;
            root_child1.Height     = 100;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(250, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(250, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(250, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_shrink_to_zero()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Height = 75;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 50;
            root_child0.Height = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexShrink = 1;
            root_child1.Width      = 50;
            root_child1.Height     = 50;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 50;
            root_child2.Height = 50;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(75, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(75, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);
        }
    }
}
