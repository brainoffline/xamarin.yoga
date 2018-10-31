using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class AbsolutePositionTests
    {
        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center()
        {
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                AlignItems     = AlignType.Center,
                FlexGrow       = 1,
                Width          = 110,
                Height         = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Width        = 60, Height = 40
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_bottom_position()
        {
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                AlignItems     = AlignType.Center,
                FlexGrow       = 1,
                Width          = 110,
                Height         = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Bottom = 10},
                Width        = 60, Height = 40
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_left_position()
        {
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                AlignItems     = AlignType.Center,
                FlexGrow       = 1,
                Width          = 110,
                Height         = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Left = 5},
                Width        = 60, Height = 40
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(5,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(5,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_right_position()
        {
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                AlignItems     = AlignType.Center,
                FlexGrow       = 1,
                Width          = 110,
                Height         = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Right = 5},
                Width        = 60, Height = 40
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_top_position()
        {
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                AlignItems     = AlignType.Center,
                FlexGrow       = 1,
                Width          = 110,
                Height         = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Top = 10},
                Width        = 60, Height = 40
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_flex_end()
        {
            var root = new YogaNode
            {
                JustifyContent = JustifyType.FlexEnd,
                AlignItems     = AlignType.FlexEnd,
                FlexGrow       = 1,
                Width          = 110,
                Height         = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Width        = 60, Height = 40
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(60, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(60, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_center()
        {
            var root = new YogaNode
            {
                AlignItems = AlignType.Center,
                FlexGrow   = 1,
                Width      = 110, Height = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Width        = 60, Height = 40
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_center_on_child_only()
        {
            var root = new YogaNode
            {
                FlexGrow = 1, Width = 110, Height = 100
            };

            var root_child0 = new YogaNode
            {
                AlignSelf    = AlignType.Center,
                PositionType = PositionType.Absolute,
                Width        = 60, Height = 40
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_column_container()
        {
            var root = new YogaNode
            {
                FlexWrap = WrapType.WrapReverse,
                Width    = 100, Height = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Width        = 20, Height = 20
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_column_container_flex_end()
        {
            var root = new YogaNode
            {
                FlexWrap = WrapType.WrapReverse, Width = 100, Height = 100
            };
            var root_child0 = new YogaNode
            {
                AlignSelf    = AlignType.FlexEnd,
                PositionType = PositionType.Absolute,
                Width        = 20, Height = 20
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_row_container()
        {
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                FlexWrap      = WrapType.WrapReverse,
                Width         = 100,
                Height        = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Width        = 20, Height = 20
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_row_container_flex_end()
        {
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                FlexWrap      = WrapType.WrapReverse,
                Width         = 100,
                Height        = 100
            };

            var root_child0 = new YogaNode
            {
                AlignSelf    = AlignType.FlexEnd,
                PositionType = PositionType.Absolute,
                Width        = 20, Height = 20
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_justify_content_center()
        {
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                FlexGrow       = 1,
                Width          = 110, Height = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Width        = 60, Height = 40
            };
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_percentage_bottom_based_on_parent_height()
        {
            var root = new YogaNode {Width = 100, Height = 200};

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Top = 50.Percent()},
                Width        = 10,
                Height       = 10
            };
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Bottom = 50.Percent()},
                Width        = 10,
                Height       = 10
            };
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Top = 10.Percent(), Bottom = 10.Percent()},
                Width        = 10
            };
            root.Children.Insert(2, root_child2);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(100, root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(90, root_child1.Layout.Position.Top);
            Assert.AreEqual(10, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(160, root_child2.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(90,  root_child0.Layout.Position.Left);
            Assert.AreEqual(100, root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(90, root_child1.Layout.Position.Left);
            Assert.AreEqual(90, root_child1.Layout.Position.Top);
            Assert.AreEqual(10, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(90,  root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(160, root_child2.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_start_top_end_bottom()
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
                        Position     = new Edges(10, 10, 10, 10)
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_width_height_end_bottom()
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
                        Position     = {End = 10, Bottom = 10},
                        Width        = 10,
                        Height       = 10
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_width_height_start_top()
        {
            YogaNode root_child0;
            var root = new YogaNode
            {
                Width  = 100,
                Height = 100,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        PositionType = PositionType.Absolute,
                        Width        = 10,
                        Height       = 10,
                        Position     = {Start = 10, Top = 10}
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_width_height_start_top_end_bottom()
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
                        Position     = new Edges(10, 10, 10, 10),
                        Width        = 10,
                        Height       = 10
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_within_border()
        {
            var root = new YogaNode
            {
                Margin  = new Edges(10, 10, 10, 10),
                Padding = new Edges(10, 10, 10, 10),
                Border  = new Edges(10, 10, 10, 10),
                Width   = 100,
                Height  = 100
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Left = 0, Top = 0},
                Width        = 50,
                Height       = 50
            };
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Right = 0, Bottom = 0},
                Width        = 50,
                Height       = 50
            };
            root.Children.Insert(1, root_child1);

            var root_child2 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Left = 0, Top = 0},
                Margin       = new Edges(10, 10, 10, 10),
                Width        = 50,
                Height       = 50
            };
            root.Children.Insert(2, root_child2);

            var root_child3 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Right = 0, Bottom = 0},
                Margin       = new Edges(10, 10, 10, 10),
                Width        = 50,
                Height       = 50
            };
            root.Children.Insert(3, root_child3);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(10,  root.Layout.Position.Left);
            Assert.AreEqual(10,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(20, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(10,  root.Layout.Position.Left);
            Assert.AreEqual(10,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(20, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);
        }

        [TestMethod]
        public void do_not_clamp_height_of_absolute_node_to_height_of_its_overflow_hidden_parent()
        {
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                Overflow      = OverflowType.Hidden,
                Width         = 50, Height = 50
            };

            var root_child0 = new YogaNode
            {
                PositionType = PositionType.Absolute,
                Position     = {Start = 0, Top = 0}
            };
            root.Children.Add(root_child0);

            var root_child0_child0 = new YogaNode
            {
                Width = 100, Height = 100
            };
            root_child0.Children.Add(root_child0_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(-50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void position_root_with_rtl_should_position_withoutdirection()
        {
            var root = new YogaNode
            {
                Width    = 52, Height = 52,
                Position = {Left = 72}
            };
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(72, root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(72, root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);
        }
    }
}
