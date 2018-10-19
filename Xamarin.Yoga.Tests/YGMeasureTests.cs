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
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            int measureCount = 0;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = measureCount;
            root_child0.MeasureFunc = _measure;
            root_child0.StyleSetFlexGrow(1);
            root_child0.StyleSetFlexShrink(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, measureCount);
        }

        [TestMethod]
        public void measure_absolute_child_with_no_constraints()
        {
            YGNode root = new YGNode();

            YGNode root_child0 = new YGNode();
            root.InsertChild(root_child0);

            int measureCount = 0;

            YGNode root_child0_child0 = new YGNode();
            root_child0_child0.StyleSetPositionType(YGPositionType.Absolute);
            root_child0_child0.Context     = measureCount;
            root_child0_child0.MeasureFunc = _measure;
            root_child0.InsertChild(root_child0_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void dont_measure_when_min_equals_max()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            int measureCount = 0;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = measureCount;
            root_child0.MeasureFunc = _measure;
            YGNodeStyleSetMinWidth(root_child0, 10);
            YGNodeStyleSetMaxWidth(root_child0, 10);
            YGNodeStyleSetMinHeight(root_child0, 10);
            YGNodeStyleSetMaxHeight(root_child0, 10);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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
            root.StyleSetAlignItems(YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            int measureCount = 0;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = measureCount;
            root_child0.MeasureFunc = _measure;
            YGNodeStyleSetMinWidthPercent(root_child0, 10);
            YGNodeStyleSetMaxWidthPercent(root_child0, 10);
            YGNodeStyleSetMinHeightPercent(root_child0, 10);
            YGNodeStyleSetMaxHeightPercent(root_child0, 10);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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
            root.StyleSetDimensions(500, 500);

            YGNode root_child0 = new YGNode
            {
                Context     = 0,
                MeasureFunc = _measure
            };
            root_child0.StyleSetMarginAuto(YGEdge.Left);
            root.InsertChild(root_child0);

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
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetDimensions(100, 100);

            int measureCount = 0;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = measureCount;
            root_child0.MeasureFunc = _measure;
            YGNodeStyleSetMinWidthPercent(root_child0, 10);
            YGNodeStyleSetMaxWidthPercent(root_child0, 10);
            YGNodeStyleSetMinHeight(root_child0, 10);
            YGNodeStyleSetMaxHeight(root_child0, 10);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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
            root.StyleSetAlignItems(YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            int measureCount = 0;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = measureCount;
            root_child0.MeasureFunc = _measure;
            YGNodeStyleSetMinWidth(root_child0, 10);
            YGNodeStyleSetMaxWidth(root_child0, 10);
            YGNodeStyleSetMinHeightPercent(root_child0, 10);
            YGNodeStyleSetMaxHeightPercent(root_child0, 10);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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
            YGNodeStyleSetWidth(root, 100);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetAlignSelf(YGAlign.FlexStart);
            root_child0.MeasureFunc = _simulate_wrapping_text;

            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(68, root_child0.Layout.Width);
            Assert.AreEqual(16, root_child0.Layout.Height);
        }

        [TestMethod]
        public void measure_not_enough_size_should_wrap()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetWidth(root, 55);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetAlignSelf(YGAlign.FlexStart);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(32, root_child0.Layout.Height);
        }

        [TestMethod]
        public void measure_zero_space_should_grow()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetHeight(root, 200);
            root.StyleSetFlexDirection( YGFlexDirection.Column);
            root.StyleSetFlexGrow(0);

            int measureCount = 0;

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetFlexDirection(YGFlexDirection.Column);
            YGNodeStyleSetPadding(root_child0, YGEdge.All, 100);
            root_child0.Context     = measureCount;
            root_child0.MeasureFunc = _measure;

            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, 282, float.NaN, YGDirection.LTR);

            Assert.AreEqual(282, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
        }

        [TestMethod]
        public void measure_flex_direction_row_and_padding()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            YGNodeStyleSetPadding(root, YGEdge.Left,   25);
            YGNodeStyleSetPadding(root, YGEdge.Top,    25);
            YGNodeStyleSetPadding(root, YGEdge.Right,  25);
            YGNodeStyleSetPadding(root, YGEdge.Bottom, 25);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 50);

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            //  YGNodeSetMeasureFunc(root_child0, _simulate_wrapping_text);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 5);
            YGNodeStyleSetHeight(root_child1, 5);
            root.InsertChild(1, root_child1);
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
            root.StyleSetMargin(YGEdge.Top, 20);
            YGNodeStyleSetPadding(root, YGEdge.All, 25);
            root.StyleSetDimensions(50, 50);

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetDimensions(5,5);
            root.InsertChild(1, root_child1);
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
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            root.StyleSetMargin(YGEdge.Top, 20);
            root.StyleSetDimensions(50, 50);

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetDimensions(5, 5);
            root.InsertChild(1, root_child1);
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
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            root.StyleSetMargin(YGEdge.Top, 20);
            root.StyleSetDimensions(50, 50);
            root.StyleSetAlignItems(YGAlign.FlexStart);

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            YGNodeStyleSetWidth(root_child1, 5);
            YGNodeStyleSetHeight(root_child1, 5);
            root.InsertChild(1, root_child1);
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
            root.StyleSetMargin(YGEdge.Top, 20);
            YGNodeStyleSetPadding(root, YGEdge.All, 25);
            root.StyleSetDimensions(50, 50);

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root_child0.StyleSetDimensions(10, 10);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetDimensions(5, 5);
            root.InsertChild(1, root_child1);
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
            root.StyleSetMargin(YGEdge.Top, 20);
            YGNodeStyleSetPadding(root, YGEdge.All, 25);
            root.StyleSetDimensions(50, 50);

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root_child0.StyleSetFlexShrink(1);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetDimensions(5, 5);
            root.InsertChild(1, root_child1);
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
            root.StyleSetMargin(YGEdge.Top, 20);
            root.StyleSetDimensions(50, 50);

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _simulate_wrapping_text;
            root_child0.StyleSetFlexShrink(1);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.StyleSetDimensions(5, 5);
            root.InsertChild(1, root_child1);
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
  ASSERT_DEATH(root.InsertChild(root_child0, 0), "Cannot add child.*");
  YGNodeFree(root_child0);
  
}

TEST(YogaDeathTest, cannot_add_nonnull_measure_func_to_non_leaf_node) {
   YGNode root = new YGNode();
   YGNode root_child0 = new YGNode();
  root.InsertChild(root_child0);
  ASSERT_DEATH(root.MeasureFunc = _measure, "Cannot set measure function.*");
  
}

#endif

        [TestMethod]
        public void can_nullify_measure_func_on_any_node()
        {
            YGNode root = new YGNode();
            root.InsertChild(new YGNode());
            root.MeasureFunc = null;
            Assert.IsTrue(root.MeasureFunc == null);
        }

        [TestMethod]
        public void cant_call_negative_measure()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Column);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 10);

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _measure_assert_negative;
            root_child0.StyleSetMargin(YGEdge.Top, 20);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
        }

        [TestMethod]
        public void cant_call_negative_measure_horizontal()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 10);
            YGNodeStyleSetHeight(root, 20);

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _measure_assert_negative;
            root_child0.StyleSetMargin(YGEdge.Start, 20);
            root.InsertChild(root_child0);

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
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            root.StyleSetJustifyContent( YGJustify.SpaceBetween);
            root.StyleSetAlignItems(YGAlign.Center);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 80);

            YGNode root_child0 = new YGNode(config);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.MeasureFunc = _measure_90_10;
            YGNodeStyleSetMaxWidthPercent(root_child1, 50);
            YGNodeStyleSetPaddingPercent(root_child1, YGEdge.Top, 50);
            root.InsertChild(1, root_child1);

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
