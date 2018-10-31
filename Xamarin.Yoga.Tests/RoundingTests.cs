using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class RoundingTests
    {
        [TestMethod]
        public void rounding_flex_basis_flex_grow_row_prime_number_width()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 113;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow = 1;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.FlexGrow = 1;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.FlexGrow = 1;
            root.Children.Insert(3, root_child3);

            var root_child4 = new YogaNode(config);
            root_child4.FlexGrow = 1;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(113, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(23,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(23,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(22,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(45,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(23,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(68,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(22,  root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(90,  root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(23,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(113, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(23,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(68,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(22,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(45,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(23,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(23,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(22,  root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(23,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);
        }

        [TestMethod]
        public void rounding_flex_basis_flex_grow_row_width_of_100()
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
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.FlexGrow = 1;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(33,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(33,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(34,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(67,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(33,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(67,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(33,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(33,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(34,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(33,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_flex_basis_flex_shrink_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 101;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexShrink = 1;
            root_child0.FlexBasis  = 100;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexBasis = 25;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.FlexBasis = 25;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(101, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(51,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(51,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(25,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(76,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(25,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(101, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(51,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(25,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(25,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(25,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_flex_basis_overrides_main_size()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 113;

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
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_fractial_input_1()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 113.4f;

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
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_fractial_input_2()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 113.6f;

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
            Assert.AreEqual(114, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(65,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(65,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(24,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(25,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(114, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(65,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(65,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(24,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(25,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_fractial_input_3()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Position.Top = 0.3f;
            root.Width        = 100;
            root.Height       = 113.4f;

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
            Assert.AreEqual(114, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(65,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(24,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(25,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(114, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(65,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(24,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(25,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_fractial_input_4()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Position.Top = 0.7f;
            root.Width        = 100;
            root.Height       = 113.4f;

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
            Assert.AreEqual(1,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(1,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(64,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(64,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(89,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(24,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_inner_node_controversy_combined()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 640;
            root.Height        = 320;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow = 1;
            root_child0.Height   = 100.Percent();
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1;
            root_child1.Height   = 100.Percent();
            root.Children.Insert(1, root_child1);

            var root_child1_child0 = new YogaNode(config);
            root_child1_child0.FlexGrow = 1;
            root_child1_child0.Width    = 100.Percent();
            root_child1.Children.Add(root_child1_child0);

            var root_child1_child1 = new YogaNode(config);
            root_child1_child1.FlexGrow = 1;
            root_child1_child1.Width    = 100.Percent();
            root_child1.Children.Insert(1, root_child1_child1);

            var root_child1_child1_child0 = new YogaNode(config);
            root_child1_child1_child0.FlexGrow = 1;
            root_child1_child1_child0.Width    = 100.Percent();
            root_child1_child1.Children.Add(root_child1_child1_child0);

            var root_child1_child2 = new YogaNode(config);
            root_child1_child2.FlexGrow = 1;
            root_child1_child2.Width    = 100.Percent();
            root_child1.Children.Insert(2, root_child1_child2);

            var root_child2 = new YogaNode(config);
            root_child2.FlexGrow = 1;
            root_child2.Height   = 100.Percent();
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(640, root.Layout.Width);
            Assert.AreEqual(320, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(213, root_child0.Layout.Width);
            Assert.AreEqual(320, root_child0.Layout.Height);

            Assert.AreEqual(213, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(214, root_child1.Layout.Width);
            Assert.AreEqual(320, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child0.Layout.Width);
            Assert.AreEqual(107, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(107, root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child1.Layout.Width);
            Assert.AreEqual(106, root_child1_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child1_child0.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child1_child0.Layout.Width);
            Assert.AreEqual(106, root_child1_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(213, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child2.Layout.Width);
            Assert.AreEqual(107, root_child1_child2.Layout.Height);

            Assert.AreEqual(427, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(213, root_child2.Layout.Width);
            Assert.AreEqual(320, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(640, root.Layout.Width);
            Assert.AreEqual(320, root.Layout.Height);

            Assert.AreEqual(427, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(213, root_child0.Layout.Width);
            Assert.AreEqual(320, root_child0.Layout.Height);

            Assert.AreEqual(213, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(214, root_child1.Layout.Width);
            Assert.AreEqual(320, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child0.Layout.Width);
            Assert.AreEqual(107, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(107, root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child1.Layout.Width);
            Assert.AreEqual(106, root_child1_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child1_child0.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child1_child0.Layout.Width);
            Assert.AreEqual(106, root_child1_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(213, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(214, root_child1_child2.Layout.Width);
            Assert.AreEqual(107, root_child1_child2.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(213, root_child2.Layout.Width);
            Assert.AreEqual(320, root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_inner_node_controversy_horizontal()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 320;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow = 1;
            root_child0.Height   = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1;
            root_child1.Height   = 10;
            root.Children.Insert(1, root_child1);

            var root_child1_child0 = new YogaNode(config);
            root_child1_child0.FlexGrow = 1;
            root_child1_child0.Height   = 10;
            root_child1.Children.Add(root_child1_child0);

            var root_child2 = new YogaNode(config);
            root_child2.FlexGrow = 1;
            root_child2.Height   = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(320, root.Layout.Width);
            Assert.AreEqual(10,  root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(107, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(107, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(106, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(106, root_child1_child0.Layout.Width);
            Assert.AreEqual(10,  root_child1_child0.Layout.Height);

            Assert.AreEqual(213, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(107, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(320, root.Layout.Width);
            Assert.AreEqual(10,  root.Layout.Height);

            Assert.AreEqual(213, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(107, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(107, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(106, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(106, root_child1_child0.Layout.Width);
            Assert.AreEqual(10,  root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(107, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_inner_node_controversy_vertical()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Height = 320;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow = 1;
            root_child0.Width    = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1;
            root_child1.Width    = 10;
            root.Children.Insert(1, root_child1);

            var root_child1_child0 = new YogaNode(config);
            root_child1_child0.FlexGrow = 1;
            root_child1_child0.Width    = 10;
            root_child1.Children.Add(root_child1_child0);

            var root_child2 = new YogaNode(config);
            root_child2.FlexGrow = 1;
            root_child2.Width    = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(10,  root.Layout.Width);
            Assert.AreEqual(320, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(107, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(107, root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(106, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1_child0.Layout.Width);
            Assert.AreEqual(106, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(213, root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(107, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(10,  root.Layout.Width);
            Assert.AreEqual(320, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(107, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(107, root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(106, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1_child0.Layout.Width);
            Assert.AreEqual(106, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(213, root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(107, root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_total_fractial()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 87.4f;
            root.Height = 113.4f;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 0.7f;
            root_child0.FlexBasis = 50.3f;
            root_child0.Height    = 20.3f;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1.6f;
            root_child1.Height   = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.FlexGrow = 1.1f;
            root_child2.Height   = 10.7f;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(87,  root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(87, root_child0.Layout.Width);
            Assert.AreEqual(59, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(59, root_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(89, root_child2.Layout.Position.Top);
            Assert.AreEqual(87, root_child2.Layout.Width);
            Assert.AreEqual(24, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(87,  root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(87, root_child0.Layout.Width);
            Assert.AreEqual(59, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(59, root_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(89, root_child2.Layout.Position.Top);
            Assert.AreEqual(87, root_child2.Layout.Width);
            Assert.AreEqual(24, root_child2.Layout.Height);
        }

        [TestMethod]
        public void rounding_total_fractial_nested()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 87.4f;
            root.Height = 113.4f;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow  = 0.7f;
            root_child0.FlexBasis = 50.3f;
            root_child0.Height    = 20.3f;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.FlexGrow        = 1;
            root_child0_child0.FlexBasis       = 0.3f;
            root_child0_child0.Position.Bottom = 13.3f;
            root_child0_child0.Height          = 9.9f;
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child1 = new YogaNode(config);
            root_child0_child1.FlexGrow     = 4;
            root_child0_child1.FlexBasis    = 0.3f;
            root_child0_child1.Position.Top = 13.3f;
            root_child0_child1.Height       = 1.1f;
            root_child0.Children.Insert(1, root_child0_child1);

            var root_child1 = new YogaNode(config);
            root_child1.FlexGrow = 1.6f;
            root_child1.Height   = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.FlexGrow = 1.1f;
            root_child2.Height   = 10.7f;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(87,  root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(87, root_child0.Layout.Width);
            Assert.AreEqual(59, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(-13, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(87,  root_child0_child0.Layout.Width);
            Assert.AreEqual(12,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(25, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child0_child1.Layout.Width);
            Assert.AreEqual(47, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(59, root_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(89, root_child2.Layout.Position.Top);
            Assert.AreEqual(87, root_child2.Layout.Width);
            Assert.AreEqual(24, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(87,  root.Layout.Width);
            Assert.AreEqual(113, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(87, root_child0.Layout.Width);
            Assert.AreEqual(59, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(-13, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(87,  root_child0_child0.Layout.Width);
            Assert.AreEqual(12,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(25, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child0_child1.Layout.Width);
            Assert.AreEqual(47, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(59, root_child1.Layout.Position.Top);
            Assert.AreEqual(87, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(89, root_child2.Layout.Position.Top);
            Assert.AreEqual(87, root_child2.Layout.Width);
            Assert.AreEqual(24, root_child2.Layout.Height);
        }
    }
}
