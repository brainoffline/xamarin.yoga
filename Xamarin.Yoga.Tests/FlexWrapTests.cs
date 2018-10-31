using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class FlexWrapTests
    {
        [TestMethod]
        public void flex_wrap_align_stretch_fits_one_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.FlexWrap      = WrapType.Wrap;
            root.Width         = 150;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Width = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
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
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_wrap_children_with_min_main_overriding_flex_basis()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.FlexWrap      = WrapType.Wrap;
            root.Width         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexBasis = 50;
            root_child0.MinWidth  = 55;
            root_child0.Height    = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.FlexBasis = 50;
            root_child1.MinWidth  = 55;
            root_child1.Height    = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(55, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(55, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(55, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(45, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(55, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_wrap_wrap_to_child_height()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Row;
            root_child0.AlignItems    = AlignType.FlexStart;
            root_child0.FlexWrap      = WrapType.Wrap;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.Width = 100;
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child0_child0 = new YogaNode(config);
            root_child0_child0_child0.Width  = 100;
            root_child0_child0_child0.Height = 100;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 100;
            root_child1.Height = 100;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(100, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(100, root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void wrap_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexWrap = WrapType.Wrap;
            root.Height   = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30;
            root_child0.Height = 30;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 30;
            root_child1.Height = 30;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 30;
            root_child2.Height = 30;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.Width  = 30;
            root_child3.Height = 30;
            root.Children.Insert(3, root_child3);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(60,  root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(60, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,  root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(60,  root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(30, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(30, root_child2.Layout.Position.Left);
            Assert.AreEqual(60, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,  root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);
        }

        [TestMethod]
        public void wrap_nodes_with_content_sizing_margin_cross()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 500;
            root.Height = 500;

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Row;
            root_child0.FlexWrap      = WrapType.Wrap;
            root_child0.Width         = 70;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child0_child0 = new YogaNode(config);
            root_child0_child0_child0.Width  = 40;
            root_child0_child0_child0.Height = 40;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            var root_child0_child1 = new YogaNode(config);
            root_child0_child1.Margin.Top = 10;
            root_child0.Children.Insert(1, root_child0_child1);

            var root_child0_child1_child0 = new YogaNode(config);
            root_child0_child1_child0.Width  = 40;
            root_child0_child1_child0.Height = 40;
            root_child0_child1.Children.Add(root_child0_child1_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(70, root_child0.Layout.Width);
            Assert.AreEqual(90, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(430, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(70,  root_child0.Layout.Width);
            Assert.AreEqual(90,  root_child0.Layout.Height);

            Assert.AreEqual(30, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(30, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void wrap_nodes_with_content_sizing_overflowing_margin()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 500;
            root.Height = 500;

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Row;
            root_child0.FlexWrap      = WrapType.Wrap;
            root_child0.Width         = 85;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child0_child0 = new YogaNode(config);
            root_child0_child0_child0.Width  = 40;
            root_child0_child0_child0.Height = 40;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            var root_child0_child1 = new YogaNode(config);
            root_child0_child1.Margin.Right = 10;
            root_child0.Children.Insert(1, root_child0_child1);

            var root_child0_child1_child0 = new YogaNode(config);
            root_child0_child1_child0.Width  = 40;
            root_child0_child1_child0.Height = 40;
            root_child0_child1.Children.Add(root_child0_child1_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(85, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(415, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(85,  root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);

            Assert.AreEqual(45, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(35, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1.Layout.Width);
            Assert.AreEqual(40, root_child0_child1.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Width);
            Assert.AreEqual(40, root_child0_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_column_fixed_size()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.FlexWrap   = WrapType.WrapReverse;
            root.Width      = 200;
            root.Height     = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30;
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 30;
            root_child1.Height = 20;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 30;
            root_child2.Height = 30;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.Width  = 30;
            root_child3.Height = 40;
            root.Children.Insert(3, root_child3);

            var root_child4 = new YogaNode(config);
            root_child4.Width  = 30;
            root_child4.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(170, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(30,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(170, root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30,  root_child1.Layout.Width);
            Assert.AreEqual(20,  root_child1.Layout.Height);

            Assert.AreEqual(170, root_child2.Layout.Position.Left);
            Assert.AreEqual(30,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30,  root_child2.Layout.Width);
            Assert.AreEqual(30,  root_child2.Layout.Height);

            Assert.AreEqual(170, root_child3.Layout.Position.Left);
            Assert.AreEqual(60,  root_child3.Layout.Position.Top);
            Assert.AreEqual(30,  root_child3.Layout.Width);
            Assert.AreEqual(40,  root_child3.Layout.Height);

            Assert.AreEqual(140, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(30,  root_child4.Layout.Width);
            Assert.AreEqual(50,  root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(30, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(60, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_row_align_content_center()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignContent  = AlignType.Center;
            root.FlexWrap      = WrapType.WrapReverse;
            root.Width         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30;
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 30;
            root_child1.Height = 20;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 30;
            root_child2.Height = 30;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.Width  = 30;
            root_child3.Height = 40;
            root.Children.Insert(3, root_child3);

            var root_child4 = new YogaNode(config);
            root_child4.Width  = 30;
            root_child4.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_row_align_content_flex_start()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.FlexWrap      = WrapType.WrapReverse;
            root.Width         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30;
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 30;
            root_child1.Height = 20;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 30;
            root_child2.Height = 30;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.Width  = 30;
            root_child3.Height = 40;
            root.Children.Insert(3, root_child3);

            var root_child4 = new YogaNode(config);
            root_child4.Width  = 30;
            root_child4.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_row_align_content_space_around()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignContent  = AlignType.SpaceAround;
            root.FlexWrap      = WrapType.WrapReverse;
            root.Width         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30;
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 30;
            root_child1.Height = 20;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 30;
            root_child2.Height = 30;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.Width  = 30;
            root_child3.Height = 40;
            root.Children.Insert(3, root_child3);

            var root_child4 = new YogaNode(config);
            root_child4.Width  = 30;
            root_child4.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_row_align_content_stretch()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignContent  = AlignType.Stretch;
            root.FlexWrap      = WrapType.WrapReverse;
            root.Width         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30;
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 30;
            root_child1.Height = 20;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 30;
            root_child2.Height = 30;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.Width  = 30;
            root_child3.Height = 40;
            root.Children.Insert(3, root_child3);

            var root_child4 = new YogaNode(config);
            root_child4.Width  = 30;
            root_child4.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(30, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(70, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(60, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(40, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(30, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_reverse_row_single_line_different_size()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.FlexWrap      = WrapType.WrapReverse;
            root.Width         = 300;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30;
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 30;
            root_child1.Height = 20;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 30;
            root_child2.Height = 30;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.Width  = 30;
            root_child3.Height = 40;
            root.Children.Insert(3, root_child3);

            var root_child4 = new YogaNode(config);
            root_child4.Width  = 30;
            root_child4.Height = 50;
            root.Children.Insert(4, root_child4);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(300, root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(40, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(90, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(120, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(30,  root_child4.Layout.Width);
            Assert.AreEqual(50,  root_child4.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(300, root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(270, root_child0.Layout.Position.Left);
            Assert.AreEqual(40,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(240, root_child1.Layout.Position.Left);
            Assert.AreEqual(30,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30,  root_child1.Layout.Width);
            Assert.AreEqual(20,  root_child1.Layout.Height);

            Assert.AreEqual(210, root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30,  root_child2.Layout.Width);
            Assert.AreEqual(30,  root_child2.Layout.Height);

            Assert.AreEqual(180, root_child3.Layout.Position.Left);
            Assert.AreEqual(10,  root_child3.Layout.Position.Top);
            Assert.AreEqual(30,  root_child3.Layout.Width);
            Assert.AreEqual(40,  root_child3.Layout.Height);

            Assert.AreEqual(150, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(30,  root_child4.Layout.Width);
            Assert.AreEqual(50,  root_child4.Layout.Height);
        }

        [TestMethod]
        public void wrap_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.FlexWrap      = WrapType.Wrap;
            root.Width         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30;
            root_child0.Height = 30;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 30;
            root_child1.Height = 30;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 30;
            root_child2.Height = 30;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.Width  = 30;
            root_child3.Height = 30;
            root.Children.Insert(3, root_child3);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(30, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);
        }

        [TestMethod]
        public void wrap_row_align_items_center()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignItems    = AlignType.Center;
            root.FlexWrap      = WrapType.Wrap;
            root.Width         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30;
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 30;
            root_child1.Height = 20;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 30;
            root_child2.Height = 30;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.Width  = 30;
            root_child3.Height = 30;
            root.Children.Insert(3, root_child3);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);
        }

        [TestMethod]
        public void wrap_row_align_items_flex_end()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignItems    = AlignType.FlexEnd;
            root.FlexWrap      = WrapType.Wrap;
            root.Width         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 30;
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 30;
            root_child1.Height = 20;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 30;
            root_child2.Height = 30;
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode(config);
            root_child3.Width  = 30;
            root_child3.Height = 30;
            root.Children.Insert(3, root_child3);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(60, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(60,  root.Layout.Height);

            Assert.AreEqual(70, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(30, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(30, root_child2.Layout.Width);
            Assert.AreEqual(30, root_child2.Layout.Height);

            Assert.AreEqual(70, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(30, root_child3.Layout.Width);
            Assert.AreEqual(30, root_child3.Layout.Height);
        }

        [TestMethod]
        public void wrapped_column_max_height()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.Center;
            root.AlignContent   = AlignType.Center;
            root.AlignItems     = AlignType.Center;
            root.FlexWrap       = WrapType.Wrap;
            root.Width          = 700;
            root.Height         = 500;

            var root_child0 = new YogaNode(config);
            root_child0.Width     = 100;
            root_child0.Height    = 500;
            root_child0.MaxHeight = 200;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Margin = new Edges(20, 20, 20, 20);
            root_child1.Width  = 200;
            root_child1.Height = 200;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 100;
            root_child2.Height = 100;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(250, root_child0.Layout.Position.Left);
            Assert.AreEqual(30,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(200, root_child1.Layout.Position.Left);
            Assert.AreEqual(250, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(200, root_child1.Layout.Height);

            Assert.AreEqual(420, root_child2.Layout.Position.Left);
            Assert.AreEqual(200, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(350, root_child0.Layout.Position.Left);
            Assert.AreEqual(30,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);

            Assert.AreEqual(300, root_child1.Layout.Position.Left);
            Assert.AreEqual(250, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(200, root_child1.Layout.Height);

            Assert.AreEqual(180, root_child2.Layout.Position.Left);
            Assert.AreEqual(200, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void wrapped_column_max_height_flex()
        {
            YogaNode root_child0;
            YogaNode root_child1;
            YogaNode root_child2;
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                AlignContent   = AlignType.Center,
                AlignItems     = AlignType.Center,
                FlexWrap       = WrapType.Wrap,
                Width          = 700,
                Height         = 500,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        FlexGrow   = 1,
                        FlexShrink = 1,
                        FlexBasis  = 0.Percent(),
                        Width      = 100,
                        Height     = 500,
                        MaxHeight  = 200
                    }),
                    (root_child1 = new YogaNode
                    {
                        FlexGrow   = 1,
                        FlexShrink = 1,
                        FlexBasis  = 0.Percent(),
                        Margin     = new Edges(20, 20, 20, 20),
                        Width      = 200
                    }),
                    (root_child2 = new YogaNode {Width = 100}),
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(300, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(180, root_child0.Layout.Height);

            Assert.AreEqual(250, root_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(180, root_child1.Layout.Height);

            Assert.AreEqual(300, root_child2.Layout.Position.Left);
            Assert.AreEqual(400, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(700, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(300, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(180, root_child0.Layout.Height);

            Assert.AreEqual(250, root_child1.Layout.Position.Left);
            Assert.AreEqual(200, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(180, root_child1.Layout.Height);

            Assert.AreEqual(300, root_child2.Layout.Position.Left);
            Assert.AreEqual(400, root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);
        }

        [TestMethod]
        public void wrapped_row_within_align_items_center()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Row;
            root_child0.FlexWrap      = WrapType.Wrap;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.Width  = 150;
            root_child0_child0.Height = 80;
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child1 = new YogaNode(config);
            root_child0_child1.Width  = 80;
            root_child0_child1.Height = 80;
            root_child0.Children.Add(root_child0_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(120, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80,  root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0_child1.Layout.Width);
            Assert.AreEqual(80,  root_child0_child1.Layout.Height);
        }

        [TestMethod]
        public void wrapped_row_within_align_items_flex_end()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.FlexEnd;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Row;
            root_child0.FlexWrap      = WrapType.Wrap;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.Width  = 150;
            root_child0_child0.Height = 80;
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child1 = new YogaNode(config);
            root_child0_child1.Width  = 80;
            root_child0_child1.Height = 80;
            root_child0.Children.Insert(1, root_child0_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(120, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80,  root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0_child1.Layout.Width);
            Assert.AreEqual(80,  root_child0_child1.Layout.Height);
        }

        [TestMethod]
        public void wrapped_row_within_align_items_flex_start()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Row;
            root_child0.FlexWrap      = WrapType.Wrap;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.Width  = 150;
            root_child0_child0.Height = 80;
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child1 = new YogaNode(config);
            root_child0_child1.Width  = 80;
            root_child0_child1.Height = 80;
            root_child0.Children.Insert(1, root_child0_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80, root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80, root_child0_child1.Layout.Width);
            Assert.AreEqual(80, root_child0_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(160, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(150, root_child0_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0_child0.Layout.Height);

            Assert.AreEqual(120, root_child0_child1.Layout.Position.Left);
            Assert.AreEqual(80,  root_child0_child1.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0_child1.Layout.Width);
            Assert.AreEqual(80,  root_child0_child1.Layout.Height);
        }
    }
}
