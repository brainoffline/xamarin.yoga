using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGMeasureTests
    {
        private static YGSize _measure(
            YGNode        node,
            float         width,
            YGMeasureMode widthMode,
            float         height,
            YGMeasureMode heightMode)
        {
            int measureCount = (int) node.Context;
            node.Context = ++measureCount;

            return new YGSize(width = 10, height = 10);
        }

        static YGSize _simulate_wrapping_text(YGNode node,
            float                                    width,
            YGMeasureMode                            widthMode,
            float                                    height,
            YGMeasureMode                            heightMode)
        {
            if (widthMode == YGMeasureMode.Undefined || width >= 68)
            {
                return new YGSize(width = 68, height = 16);
            }

            return new YGSize(width = 50, height = 32);
        }

        static YGSize _measure_assert_negative(YGNode node,
            float                                     width,
            YGMeasureMode                             widthMode,
            float                                     height,
            YGMeasureMode                             heightMode)
        {
            Assert.IsTrue(width  >= 0);
            Assert.IsTrue(height >= 0);

            return new YGSize(width = 0, height = 0);
        }

        [TestMethod]
        public void dont_measure_single_grow_shrink_child()
        {
            YGNode root = new YGNode();
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode
            {
                Context = 0,
                MeasureFunc = _measure
            };
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexShrink = 1;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            var measureCount = (int) root_child0.Context;
            Assert.AreEqual(0, measureCount);
        }

        [TestMethod]
        public void measure_absolute_child_with_no_constraints()
        {
            YGNode root = new YGNode();

            YGNode root_child0 = new YGNode();
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode();
            root_child0_child0.Style.PositionType = YGPositionType.Absolute;
            root_child0_child0.Context     = 0;
            root_child0_child0.MeasureFunc = _measure;
            root_child0.Children.Add(root_child0_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            var measureCount = (int)root_child0_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void dont_measure_when_min_equals_max()
        {
            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measure;
            root_child0.Style.MinWidth = 10;
            root_child0.Style.MaxWidth = 10;
            root_child0.Style.MinHeight = 10;
            root_child0.Style.MaxHeight = 10;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            var measureCount = (int)root_child0.Context;
            Assert.AreEqual(0,  measureCount);
            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void dont_measure_when_min_equals_max_percentages()
        {
            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measure;
            root_child0.Style.MinWidth = 10.Percent();
            root_child0.Style.MaxWidth = 10.Percent();
            root_child0.Style.MinHeight = 10.Percent();
            root_child0.Style.MaxHeight = 10.Percent();
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            var measureCount = (int)root_child0.Context;
            Assert.AreEqual(0,  measureCount);
            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }


        [TestMethod]
        public void measure_nodes_with_margin_auto_and_stretch()
        {
            YGNode root = new YGNode();
            root.Style.Width = 500;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode
            {
                Context     = 0,
                MeasureFunc = _measure
            };
            root_child0.Style.Margin.Left = YGValue.Auto;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(490, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void dont_measure_when_min_equals_max_mixed_width_percent()
        {
            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width  = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measure;
            root_child0.Style.MinWidth = 10.Percent();
            root_child0.Style.MaxWidth = 10.Percent();
            root_child0.Style.MinHeight = 10;
            root_child0.Style.MaxHeight = 10;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            var measureCount = (int)root_child0.Context;
            Assert.AreEqual(0,  measureCount);
            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void dont_measure_when_min_equals_max_mixed_height_percent()
        {
            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measure;
            root_child0.Style.MinWidth = 10;
            root_child0.Style.MaxWidth = 10;
            root_child0.Style.MinHeight = 10.Percent();
            root_child0.Style.MaxHeight = 10.Percent();
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            var measureCount = (int)root_child0.Context;
            Assert.AreEqual(0,  measureCount);
            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void measure_enough_size_should_be_in_single_line()
        {
            YGNode root = new YGNode();
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.AlignSelf = YGAlign.FlexStart;
            root_child0.MeasureFunc = _simulate_wrapping_text;

            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(68, root_child0.Layout.Width);
            Assert.AreEqual(16, root_child0.Layout.Height);
        }

        [TestMethod]
        public void measure_not_enough_size_should_wrap()
        {
            YGNode root = new YGNode();
            root.Style.Width = 55;

            YGNode root_child0 = new YGNode();
            root_child0.Style.AlignSelf = YGAlign.FlexStart;
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(32, root_child0.Layout.Height);
        }

        [TestMethod]
        public void measure_zero_space_should_grow()
        {
            YGNode root = new YGNode();
            root.Style.Height = 200;
            root.Style.FlexDirection =  YGFlexDirection.Column;
            root.Style.FlexGrow = 0;

            YGNode root_child0 = new YGNode();
            root_child0.Style.FlexDirection = YGFlexDirection.Column;
            root_child0.Style.Padding.All = 100;
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measure;

            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, 282, float.NaN, YGDirection.LTR);

            Assert.AreEqual(282, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
        }

        [TestMethod]
        public void measure_flex_direction_row_and_padding()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Padding = new Edges(25,25,25,25);
            root.Style.Width = 50;
            root.Style.Height = 50;

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            //  YGNodeSetMeasureFunc(root_child0, _simulate_wrapping_text);
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 5;
            root_child1.Style.Height = 5;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(25, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(75, root_child1.Layout.Position.Left);
            Assert.AreEqual(25, root_child1.Layout.Position.Top);
            Assert.AreEqual(5,  root_child1.Layout.Width);
            Assert.AreEqual(5,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void measure_flex_direction_column_and_padding()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Margin.Top = 20;
            root.Style.Padding.All = 25;
            root.Style.Width  = 50;
            root.Style.Height = 50;

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 5;
            root_child1.Style.Height = 5;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(20, root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(25, root_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0.Layout.Width);
            Assert.AreEqual(32, root_child0.Layout.Height);

            Assert.AreEqual(25, root_child1.Layout.Position.Left);
            Assert.AreEqual(57, root_child1.Layout.Position.Top);
            Assert.AreEqual(5,  root_child1.Layout.Width);
            Assert.AreEqual(5,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void measure_flex_direction_row_no_padding()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Margin.Top = 20;
            root.Style.Width  = 50;
            root.Style.Height = 50;

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 5;
            root_child1.Style.Height = 5;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(20, root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(5,  root_child1.Layout.Width);
            Assert.AreEqual(5,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void measure_flex_direction_row_no_padding_align_items_flexstart()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Margin.Top = 20;
            root.Style.Width  = 50;
            root.Style.Height = 50;
            root.Style.AlignItems = YGAlign.FlexStart;

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 5;
            root_child1.Style.Height = 5;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(20, root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(32, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(5,  root_child1.Layout.Width);
            Assert.AreEqual(5,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void measure_with_fixed_size()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Margin.Top = 20;
            root.Style.Padding.All = 25;
            root.Style.Width  = 50;
            root.Style.Height = 50;

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root_child0.Style.Width = 10;
            root_child0.Style.Height = 10;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 5;
            root_child1.Style.Height = 5;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(20, root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(25, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(25, root_child1.Layout.Position.Left);
            Assert.AreEqual(35, root_child1.Layout.Position.Top);
            Assert.AreEqual(5,  root_child1.Layout.Width);
            Assert.AreEqual(5,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void measure_with_flex_shrink()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Margin.Top = 20;
            root.Style.Padding.All = 25;
            root.Style.Width  = 50;
            root.Style.Height = 50;

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root_child0.Style.FlexShrink = 1;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 5;
            root_child1.Style.Height = 5;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(20, root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(25, root_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(25, root_child1.Layout.Position.Left);
            Assert.AreEqual(25, root_child1.Layout.Position.Top);
            Assert.AreEqual(5,  root_child1.Layout.Width);
            Assert.AreEqual(5,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void measure_no_padding()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Margin.Top = 20;
            root.Style.Width  = 50;
            root.Style.Height = 50;

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root_child0.Style.FlexShrink = 1;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.Width = 5;
            root_child1.Style.Height = 5;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(20, root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(32, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(32, root_child1.Layout.Position.Top);
            Assert.AreEqual(5,  root_child1.Layout.Width);
            Assert.AreEqual(5,  root_child1.Layout.Height);
        }

#if GTEST_HAS_DEATH_TEST
TEST(YogaDeathTest, cannot_add_child_to_node_with_measure_func) {
   YGNode root = new YGNode();
  root.MeasureFunc = _measure;

   YGNode root_child0 = new YGNode();
  ASSERT_DEATH(root.Children.Add(root_child0, 0), "Cannot add child.*");
  YGNodeFree(root_child0);
  
}

TEST(YogaDeathTest, cannot_add_nonnull_measure_func_to_non_leaf_node) {
   YGNode root = new YGNode();
   YGNode root_child0 = new YGNode();
  root.Children.Add(root_child0);
  ASSERT_DEATH(root.MeasureFunc = _measure, "Cannot set measure function.*");
  
}

#endif

        [TestMethod]
        public void can_nullify_measure_func_on_any_node()
        {
            YGNode root = new YGNode();
            root.Children.Add(new YGNode());
            root.MeasureFunc = null;
            Assert.IsTrue(root.MeasureFunc == null);
        }

        [TestMethod]
        public void cant_call_negative_measure()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection =  YGFlexDirection.Column;
            root.Style.Width = 50;
            root.Style.Height = 10;

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _measure_assert_negative;
            root_child0.Style.Margin.Top = 20;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
        }

        [TestMethod]
        public void cant_call_negative_measure_horizontal()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.Width = 10;
            root.Style.Height = 20;

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _measure_assert_negative;
            root_child0.Style.Margin.Start = 20;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
        }

        static YGSize _measure_90_10(YGNode node,
            float                           width,
            YGMeasureMode                   widthMode,
            float                           height,
            YGMeasureMode                   heightMode)
        {
            return new YGSize(width = 90, height = 10);
        }

        [TestMethod]
        public void percent_with_text_node()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.JustifyContent =  YGJustify.SpaceBetween;
            root.Style.AlignItems = YGAlign.Center;
            root.Style.Width = 100;
            root.Style.Height = 80;

            YGNode root_child0 = new YGNode(config);
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.MeasureFunc = _measure_90_10;
            root_child1.Style.MaxWidth = 50.Percent();
            root_child1.Style.Padding.Top = 50.Percent();
            root.Children.Insert(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(40, root_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(15, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);
        }
    }
}
