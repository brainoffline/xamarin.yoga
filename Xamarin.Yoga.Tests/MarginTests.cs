using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class MarginTests
    {
        [TestMethod]
        public void margin_and_flex_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow      = 1;
            root_child0.Margin.Top    = 10;
            root_child0.Margin.Bottom = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_and_flex_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow     = 1;
            root_child0.Margin.Start = 10;
            root_child0.Margin.End   = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_and_stretch_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow     = 1;
            root_child0.Margin.Start = 10;
            root_child0.Margin.End   = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(80,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_and_stretch_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow      = 1;
            root_child0.Margin.Top    = 10;
            root_child0.Margin.Bottom = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_bottom()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Bottom = Value.Auto;
            root_child0.Width         = 50;
            root_child0.Height        = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_bottom_and_top()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Top    = Value.Auto;
            root_child0.Margin.Bottom = Value.Auto;
            root_child0.Width         = 50;
            root_child0.Height        = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_bottom_and_top_justify_center()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.Center;
            root.Width          = 200;
            root.Height         = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Top    = Value.Auto;
            root_child0.Margin.Bottom = Value.Auto;
            root_child0.Width         = 50;
            root_child0.Height        = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(50,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Left = Value.Auto;
            root_child0.Width       = 50;
            root_child0.Height      = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_and_right()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200;
            root.Height = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Left  = Value.Auto;
            root_child0.Margin.Right = Value.Auto;
            root_child0.Width        = 50;
            root_child0.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(50,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_and_right_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignItems    = AlignType.Center;
            root.Width         = 200;
            root.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Left  = Value.Auto;
            root_child0.Margin.Right = Value.Auto;
            root_child0.Width        = 50;
            root_child0.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(75, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(75,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(75, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_and_right_column_and_center()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Left  = Value.Auto;
            root_child0.Margin.Right = Value.Auto;
            root_child0.Width        = 50;
            root_child0.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_and_right_strech()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 200;
            root.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Left  = Value.Auto;
            root_child0.Margin.Right = Value.Auto;
            root_child0.Width        = 50;
            root_child0.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_child_bigger_than_parent()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.Center;
            root.Width          = 52;
            root.Height         = 52;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Left = Value.Auto;
            root_child0.Width       = 72;
            root_child0.Height      = 72;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-20, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_fix_right_child_bigger_than_parent()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.Center;
            root.Width          = 52;
            root.Height         = 52;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Left  = Value.Auto;
            root_child0.Margin.Right = 10;
            root_child0.Width        = 72;
            root_child0.Height       = 72;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-30, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_right_child_bigger_than_parent()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.Center;
            root.Width          = 52;
            root.Height         = 52;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Left  = Value.Auto;
            root_child0.Margin.Right = Value.Auto;
            root_child0.Width        = 72;
            root_child0.Height       = 72;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-20, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_left_stretching_child()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow    = 1;
            root_child0.FlexShrink  = 1;
            root_child0.FlexBasis   = 0.Percent();
            root_child0.Margin.Left = Value.Auto;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(200, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0.Layout.Width);
            Assert.AreEqual(150, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(200, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0.Layout.Width);
            Assert.AreEqual(150, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_mutiple_children_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Top = Value.Auto;
            root_child0.Width      = 50;
            root_child0.Height     = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Margin.Top = Value.Auto;
            root_child1.Width      = 50;
            root_child1.Height     = 50;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 50;
            root_child2.Height = 50;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(25, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(100, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            Assert.AreEqual(75,  root_child2.Layout.Position.Left);
            Assert.AreEqual(150, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(25, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(100, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            Assert.AreEqual(75,  root_child2.Layout.Position.Left);
            Assert.AreEqual(150, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_mutiple_children_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignItems    = AlignType.Center;
            root.Width         = 200;
            root.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Right = Value.Auto;
            root_child0.Width        = 50;
            root_child0.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Margin.Right = Value.Auto;
            root_child1.Width        = 50;
            root_child1.Height       = 50;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width  = 50;
            root_child2.Height = 50;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(75, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(75, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(150, root_child2.Layout.Position.Left);
            Assert.AreEqual(75,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(125, root_child0.Layout.Position.Left);
            Assert.AreEqual(75,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(75, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(75, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_right()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Right = Value.Auto;
            root_child0.Width        = 50;
            root_child0.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_start_and_end()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200;
            root.Height = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Start = Value.Auto;
            root_child0.Margin.End   = Value.Auto;
            root_child0.Width        = 50;
            root_child0.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(50,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_start_and_end_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignItems    = AlignType.Center;
            root.Width         = 200;
            root.Height        = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Start = Value.Auto;
            root_child0.Margin.End   = Value.Auto;
            root_child0.Width        = 50;
            root_child0.Height       = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(75, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(75,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(75, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_top()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Top = Value.Auto;
            root_child0.Width      = 50;
            root_child0.Height     = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75,  root_child0.Layout.Position.Left);
            Assert.AreEqual(100, root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(75,  root_child0.Layout.Position.Left);
            Assert.AreEqual(100, root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_top_and_bottom_strech()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 200;
            root.Height = 200;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Top    = Value.Auto;
            root_child0.Margin.Bottom = Value.Auto;
            root_child0.Width         = 50;
            root_child0.Height        = 50;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(50,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(150, root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_auto_top_stretching_child()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignItems = AlignType.Center;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow   = 1;
            root_child0.FlexShrink = 1;
            root_child0.FlexBasis  = 0.Percent();
            root_child0.Margin.Top = Value.Auto;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width  = 50;
            root_child1.Height = 50;
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0.Layout.Width);
            Assert.AreEqual(150, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0.Layout.Width);
            Assert.AreEqual(150, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(150, root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_bottom()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.FlexEnd;
            root.Width          = 100;
            root.Height         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Bottom = 10;
            root_child0.Height        = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(80,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(80,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_end()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection  = FlexDirectionType.Row;
            root.JustifyContent = JustifyType.FlexEnd;
            root.Width          = 100;
            root.Height         = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.End = 10;
            root_child0.Width      = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_fix_left_auto_right_child_bigger_than_parent()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.Center;
            root.Width          = 52;
            root.Height         = 52;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Left  = 10;
            root_child0.Margin.Right = Value.Auto;
            root_child0.Width        = 72;
            root_child0.Height       = 72;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-20, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_should_not_be_part_of_max_height()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 250;
            root.Height = 250;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Top = 20;
            root_child0.Width      = 100;
            root_child0.Height     = 100;
            root_child0.MaxHeight  = 100;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(250, root.Layout.Width);
            Assert.AreEqual(250, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(20,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(250, root.Layout.Width);
            Assert.AreEqual(250, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(20,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_should_not_be_part_of_max_width()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 250;
            root.Height = 250;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Left = 20;
            root_child0.Width       = 100;
            root_child0.Height      = 100;
            root_child0.MaxWidth    = 100;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(250, root.Layout.Width);
            Assert.AreEqual(250, root.Layout.Height);

            Assert.AreEqual(20,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(250, root.Layout.Width);
            Assert.AreEqual(250, root.Layout.Height);

            Assert.AreEqual(150, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_start()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Start = 10;
            root_child0.Width        = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_top()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Margin.Top = 10;
            root_child0.Height     = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(10,  root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void margin_with_sibling_column()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow      = 1;
            root_child0.Margin.Bottom = 10;
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
            Assert.AreEqual(45,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(55,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(45,  root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(45,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(55,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(45,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void margin_with_sibling_row()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow   = 1;
            root_child0.Margin.End = 10;
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
            Assert.AreEqual(45,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(55,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(45,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(55,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(45,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(45,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }
    }
}
