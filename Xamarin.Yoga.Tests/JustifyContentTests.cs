using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class JustifyContentTests
    {
        [TestMethod]
        public void justify_content_column_center()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.Center;
            root.Width          = 102;
            root.Height         = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Height = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(36,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(56,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(36,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(56,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_flex_end()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.FlexEnd;
            root.Width          = 102;
            root.Height         = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Height = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(72,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(82,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(72,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(82,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_flex_start()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Width  = 102;
            root.Height = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Height = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(10,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_min_height_and_margin()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.Center;
            root.Margin.Top     = 100;
            root.MinHeight      = 50;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 20;
            root_child0.Height = 20;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_space_around()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.SpaceAround;
            root.Width          = 102;
            root.Height         = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Height = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(12,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(80,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(12,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(80,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_space_between()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.SpaceBetween;
            root.Width          = 102;
            root.Height         = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Height = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(92,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_column_space_evenly()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.SpaceEvenly;
            root.Width          = 102;
            root.Height         = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Height = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(18,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(74,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(18,  root_child0.Layout.Position.Top);
            Assert.AreEqual(102, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(46,  root_child1.Layout.Position.Top);
            Assert.AreEqual(102, root_child1.Layout.Width);
            Assert.AreEqual(10,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(74,  root_child2.Layout.Position.Top);
            Assert.AreEqual(102, root_child2.Layout.Width);
            Assert.AreEqual(10,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_colunn_max_height_and_margin()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.JustifyContent = JustifyType.Center;
            root.Margin.Top     = 100;
            root.Height         = 100;
            root.MaxHeight      = 80;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 20;
            root_child0.Height = 20;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(100, root.Layout.Position.Top);
            Assert.AreEqual(20,  root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_min_width_with_padding_child_width_greater_than_parent()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignContent = AlignType.Stretch;
            root.Width        = 1000;
            root.Height       = 1584;

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Row;
            root_child0.AlignContent  = AlignType.Stretch;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.FlexDirection      = FlexDirectionType.Row;
            root_child0_child0.JustifyContent     = JustifyType.Center;
            root_child0_child0.AlignContent       = AlignType.Stretch;
            root_child0_child0.MinWidth           = 400;
            root_child0_child0.Padding.Horizontal = 100;
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child0_child0 = new YogaNode(config);
            root_child0_child0_child0.FlexDirection = FlexDirectionType.Row;
            root_child0_child0_child0.AlignContent  = AlignType.Stretch;
            root_child0_child0_child0.Width         = 300;
            root_child0_child0_child0.Height        = 100;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1000, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1000, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(100, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(300, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1000, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1000, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(500, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(100, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(300, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_min_width_with_padding_child_width_lower_than_parent()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.AlignContent = AlignType.Stretch;
            root.Width        = 1080;
            root.Height       = 1584;

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Row;
            root_child0.AlignContent  = AlignType.Stretch;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.FlexDirection      = FlexDirectionType.Row;
            root_child0_child0.JustifyContent     = JustifyType.Center;
            root_child0_child0.AlignContent       = AlignType.Stretch;
            root_child0_child0.MinWidth           = 400;
            root_child0_child0.Padding.Horizontal = 100;
            root_child0.Children.Add(root_child0_child0);

            var root_child0_child0_child0 = new YogaNode(config);
            root_child0_child0_child0.FlexDirection = FlexDirectionType.Row;
            root_child0_child0_child0.AlignContent  = AlignType.Stretch;
            root_child0_child0_child0.Width         = 199;
            root_child0_child0_child0.Height        = 100;
            root_child0_child0.Children.Add(root_child0_child0_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1080, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(400, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(101, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(199, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1080, root.Layout.Width);
            Assert.AreEqual(1584, root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0.Layout.Width);
            Assert.AreEqual(100,  root_child0.Layout.Height);

            Assert.AreEqual(680, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(400, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            Assert.AreEqual(101, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(199, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0_child0.Layout.Height);
            //
        }

        [TestMethod]
        public void justify_content_row_center()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection  = FlexDirectionType.Row;
            root.JustifyContent = JustifyType.Center;
            root.Width          = 102;
            root.Height         = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Width = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(36,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(56,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(56,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(36,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_flex_end()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection  = FlexDirectionType.Row;
            root.JustifyContent = JustifyType.FlexEnd;
            root.Width          = 102;
            root.Height         = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Width = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(72,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(82,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(92,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(20,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(10,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_flex_start()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 102;
            root.Height        = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Width = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(10,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(20,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(92,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(82,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(72,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_max_width_and_margin()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection  = FlexDirectionType.Row;
            root.JustifyContent = JustifyType.Center;
            root.Margin.Left    = 100;
            root.Width          = 100;
            root.MaxWidth       = 80;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 20;
            root_child0.Height = 20;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(80,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(30, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(80,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(30, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_min_width_and_margin()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection  = FlexDirectionType.Row;
            root.JustifyContent = JustifyType.Center;
            root.Margin.Left    = 100;
            root.MinWidth       = 50;

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 20;
            root_child0.Height = 20;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(50,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(15, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(100, root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(50,  root.Layout.Width);
            Assert.AreEqual(20,  root.Layout.Height);

            Assert.AreEqual(15, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_space_around()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection  = FlexDirectionType.Row;
            root.JustifyContent = JustifyType.SpaceAround;
            root.Width          = 102;
            root.Height         = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Width = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(12,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(80,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(80,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(12,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_space_between()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection  = FlexDirectionType.Row;
            root.JustifyContent = JustifyType.SpaceBetween;
            root.Width          = 102;
            root.Height         = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Width = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Width = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Width = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(92,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(92,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(102, root_child0.Layout.Height);

            Assert.AreEqual(46,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(10,  root_child1.Layout.Width);
            Assert.AreEqual(102, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(102, root_child2.Layout.Height);
        }

        [TestMethod]
        public void justify_content_row_space_evenly()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.FlexDirection  = FlexDirectionType.Row;
            root.JustifyContent = JustifyType.SpaceEvenly;
            root.Width          = 102;
            root.Height         = 102;

            var root_child0 = new YogaNode(config);
            root_child0.Height = 10;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Height = 10;
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode(config);
            root_child2.Height = 10;
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(26, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(51, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(0,  root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(77, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(0,  root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(102, root.Layout.Width);
            Assert.AreEqual(102, root.Layout.Height);

            Assert.AreEqual(77, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(51, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(0,  root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(26, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(0,  root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);
        }
    }
}
