using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class AspectRatioTests
    {
        [TestMethod]
        public void aspect_ratio_absolute_layout_height_defined()
        {
            YogaNode root_child0;
            var root = new YogaNode
            {
                Width = 100, Height = 100,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        
                        
                            PositionType = PositionType.Absolute,
                            Position = {Left = 0, Top = 0},
                            Height = 50,
                            AspectRatio = 1
                        
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_absolute_layout_width_defined()
        {
            var root = new YogaNode {Width = 100, Height = 100};

            var root_child0 = new YogaNode {PositionType = PositionType.Absolute, Position = {Left = 0, Top = 0}, Width = 50, AspectRatio = 1};
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_align_stretch()
        {
            var root = new YogaNode();
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode();
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_allow_child_overflow_parent_size()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 50;
            root_child0.AspectRatio = 4;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(50,  root.Layout.Height);

            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_basis()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.FlexBasis   = 50;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_both_dimensions_defined_column()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Width       = 100;
            root_child0.Height      = 50;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_both_dimensions_defined_row()
        {
            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignItems    = AlignType.FlexStart;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.Width       = 100;
            root_child0.Height      = 50;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_cross_defined()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Width       = 50;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_defined_cross_with_main_margin()
        {
            var root = new YogaNode();
            root.AlignItems     = AlignType.Center;
            root.JustifyContent = JustifyType.Center;
            root.Width          = 100;
            root.Height         = 100;

            var root_child0 = new YogaNode();
            root_child0.Width         = 50;
            root_child0.AspectRatio   = 1;
            root_child0.Margin.Top    = 10;
            root_child0.Margin.Bottom = 10;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_defined_cross_with_margin()
        {
            var root = new YogaNode();
            root.AlignItems     = AlignType.Center;
            root.JustifyContent = JustifyType.Center;
            root.Width          = 100;
            root.Height         = 100;

            var root_child0 = new YogaNode();
            root_child0.Width        = 50;
            root_child0.AspectRatio  = 1;
            root_child0.Margin.Left  = 10;
            root_child0.Margin.Right = 10;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_defined_main_with_margin()
        {
            var root = new YogaNode();
            root.AlignItems     = AlignType.Center;
            root.JustifyContent = JustifyType.Center;
            root.Width          = 100;
            root.Height         = 100;

            var root_child0 = new YogaNode();
            root_child0.Height       = 50;
            root_child0.AspectRatio  = 1;
            root_child0.Margin.Left  = 10;
            root_child0.Margin.Right = 10;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_double_cross()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 50;
            root_child0.AspectRatio = 2;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_double_main()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Width       = 50;
            root_child0.AspectRatio = 0.5f;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_flex_grow()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 50;
            root_child0.FlexGrow    = 1;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_flex_shrink()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 150;
            root_child0.FlexShrink  = 1;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_flex_shrink_2()
        {
            var root = new YogaNode();
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 100.Percent();
            root_child0.FlexShrink  = 1;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode();
            root_child1.Height      = 100.Percent();
            root_child1.FlexShrink  = 1;
            root_child1.AspectRatio = 1;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_half_cross()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 100;
            root_child0.AspectRatio = 0.5f;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_half_main()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Width       = 100;
            root_child0.AspectRatio = 2;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_height_as_flex_basis()
        {
            var root = new YogaNode();
            root.AlignItems    = AlignType.FlexStart;
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 200;
            root.Height        = 200;

            var root_child0 = new YogaNode();
            root_child0.Height      = 50;
            root_child0.FlexGrow    = 1;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode();
            root_child1.Height      = 100;
            root_child1.FlexGrow    = 1;
            root_child1.AspectRatio = 1;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(75, root_child0.Layout.Width);
            Assert.AreEqual(75, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(125, root_child1.Layout.Width);
            Assert.AreEqual(125, root_child1.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_height_overrides_align_stretch_column()
        {
            var root = new YogaNode();
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 50;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_left_right_absolute()
        {
            var root = new YogaNode();
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode();
            root_child0.PositionType   = PositionType.Absolute;
            root_child0.Position.Left  = 10;
            root_child0.Position.Top   = 10;
            root_child0.Position.Right = 10;
            root_child0.AspectRatio    = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_main_defined()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 50;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_overrides_flex_grow_column()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 50;
            root_child0.FlexGrow    = 1;
            root_child0.AspectRatio = 2;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_overrides_flex_grow_row()
        {
            var root = new YogaNode();
            root.AlignItems    = AlignType.FlexStart;
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.Width       = 50;
            root_child0.FlexGrow    = 1;
            root_child0.AspectRatio = 0.5f;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(200, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_should_prefer_explicit_height()
        {
            var config = new YogaConfig {UseWebDefaults = true};

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Column;

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Column;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.FlexDirection = FlexDirectionType.Column;
            root_child0_child0.Height        = 100;
            root_child0_child0.AspectRatio   = 2;
            root_child0.Children.Add(root_child0_child0);

            root.Calc.CalculateLayout(100, 200, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(200, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_should_prefer_explicit_width()
        {
            var config = new YogaConfig {UseWebDefaults = true};

            var root = new YogaNode(config);
            root.FlexDirection = FlexDirectionType.Row;

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Row;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.FlexDirection = FlexDirectionType.Row;
            root_child0_child0.Width         = 100;
            root_child0_child0.AspectRatio   = 0.5f;
            root_child0.Children.Add(root_child0_child0);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(200, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_should_prefer_flexed_dimension()
        {
            var config = new YogaConfig {UseWebDefaults = true};

            var root = new YogaNode(config);

            var root_child0 = new YogaNode(config);
            root_child0.FlexDirection = FlexDirectionType.Column;
            root_child0.AspectRatio   = 2;
            root_child0.FlexGrow      = 1;
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode(config);
            root_child0_child0.AspectRatio = 4;
            root_child0_child0.FlexGrow    = 1;
            root_child0.Children.Add(root_child0_child0);

            root.Calc.CalculateLayout(100, 100, DirectionType.LTR);

            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(200, root_child0_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_top_bottom_absolute()
        {
            var root = new YogaNode();
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode();
            root_child0.PositionType    = PositionType.Absolute;
            root_child0.Position.Left   = 10;
            root_child0.Position.Top    = 10;
            root_child0.Position.Bottom = 10;
            root_child0.AspectRatio     = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_as_flex_basis()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 200;
            root.Height     = 200;

            var root_child0 = new YogaNode();
            root_child0.Width       = 50;
            root_child0.FlexGrow    = 1;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode();
            root_child1.Width       = 100;
            root_child1.FlexGrow    = 1;
            root_child1.AspectRatio = 1;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(75, root_child0.Layout.Width);
            Assert.AreEqual(75, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(125, root_child1.Layout.Width);
            Assert.AreEqual(125, root_child1.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_height_flex_grow_column()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 200;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Width       = 50;
            root_child0.Height      = 50;
            root_child0.FlexGrow    = 1;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_height_flex_grow_row()
        {
            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignItems    = AlignType.FlexStart;
            root.Width         = 100;
            root.Height        = 200;

            var root_child0 = new YogaNode();
            root_child0.Width       = 50;
            root_child0.Height      = 50;
            root_child0.FlexGrow    = 1;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_width_overrides_align_stretch_row()
        {
            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.Width       = 50;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_max_cross_defined()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 50;
            root_child0.MaxWidth    = 40;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_max_main_defined()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Width       = 50;
            root_child0.MaxHeight   = 40;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_measure_func()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.MeasureFunc       = _measure;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_min_cross_defined()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Height      = 30;
            root_child0.MinWidth    = 40;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(30, root_child0.Layout.Height);
        }

        [TestMethod]
        public void aspect_ratio_with_min_main_defined()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Width       = 30;
            root_child0.MinHeight   = 40;
            root_child0.AspectRatio = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(40, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        private static SizeF _measure(YogaNode node,
            float                              width,
            MeasureMode                        widthMode,
            float                              height,
            MeasureMode                        heightMode)
        {
            return new SizeF(
                width = widthMode   == MeasureMode.Exactly ? width : 50,
                height = heightMode == MeasureMode.Exactly ? height : 50
            );
        }
    }
}
