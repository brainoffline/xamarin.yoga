﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    [TestClass]
    public class YGMeasureTests
    {
        private static YGSize _measure(YGNodeRef node,
            float                                width,
            YGMeasureMode                        widthMode,
            float                                height,
            YGMeasureMode                        heightMode)
        {
            int measureCount = (int) node.getContext();
            node.setContext(++measureCount);

            return new YGSize(
                width = 10,
                height = 10
            );
        }

        static YGSize _simulate_wrapping_text(YGNodeRef node,
            float                                       width,
            YGMeasureMode                               widthMode,
            float                                       height,
            YGMeasureMode                               heightMode)
        {
            if (widthMode == YGMeasureMode.Undefined || width >= 68)
            {
                return new YGSize(width = 68, height = 16);
            }

            return new YGSize(width = 50, height = 32);
        }

        static YGSize _measure_assert_negative(YGNodeRef node,
            float                                        width,
            YGMeasureMode                                widthMode,
            float                                        height,
            YGMeasureMode                                heightMode)
        {
            Assert.IsTrue(width  >= 0);
            Assert.IsTrue(height >= 0);

            return new YGSize(width = 0, height = 0);
        }

        [TestMethod]
        public void dont_measure_single_grow_shrink_child()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            int measureCount = 0;

            YGNodeRef root_child0 = YGNodeNew();
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measure);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexShrink(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void measure_absolute_child_with_no_constraints()
        {
            YGNodeRef root = YGNodeNew();

            YGNodeRef root_child0 = YGNodeNew();
            YGNodeInsertChild(root, root_child0, 0);

            int measureCount = 0;

            YGNodeRef root_child0_child0 = YGNodeNew();
            YGNodeStyleSetPositionType(root_child0_child0, YGPositionType.Absolute);
            root_child0_child0.setContext(measureCount);
            root_child0_child0.setMeasureFunc(_measure);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void dont_measure_when_min_equals_max()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            int measureCount = 0;

            YGNodeRef root_child0 = YGNodeNew();
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measure);
            YGNodeStyleSetMinWidth(root_child0, 10);
            YGNodeStyleSetMaxWidth(root_child0, 10);
            YGNodeStyleSetMinHeight(root_child0, 10);
            YGNodeStyleSetMaxHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  measureCount);
            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void dont_measure_when_min_equals_max_percentages()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            int measureCount = 0;

            YGNodeRef root_child0 = YGNodeNew();
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measure);
            YGNodeStyleSetMinWidthPercent(root_child0, 10);
            YGNodeStyleSetMaxWidthPercent(root_child0, 10);
            YGNodeStyleSetMinHeightPercent(root_child0, 10);
            YGNodeStyleSetMaxHeightPercent(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  measureCount);
            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);
        }


        [TestMethod]
        public void measure_nodes_with_margin_auto_and_stretch()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetWidth(root, 500);
            YGNodeStyleSetHeight(root, 500);

            YGNodeRef root_child0 = YGNodeNew();
            root_child0.setMeasureFunc(_measure);
            YGNodeStyleSetMarginAuto(root_child0, YGEdge.Left);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(490, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10,  YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10,  YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void dont_measure_when_min_equals_max_mixed_width_percent()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            int measureCount = 0;

            YGNodeRef root_child0 = YGNodeNew();
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measure);
            YGNodeStyleSetMinWidthPercent(root_child0, 10);
            YGNodeStyleSetMaxWidthPercent(root_child0, 10);
            YGNodeStyleSetMinHeight(root_child0, 10);
            YGNodeStyleSetMaxHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  measureCount);
            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void dont_measure_when_min_equals_max_mixed_height_percent()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            int measureCount = 0;

            YGNodeRef root_child0 = YGNodeNew();
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measure);
            YGNodeStyleSetMinWidth(root_child0, 10);
            YGNodeStyleSetMaxWidth(root_child0, 10);
            YGNodeStyleSetMinHeightPercent(root_child0, 10);
            YGNodeStyleSetMaxHeightPercent(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  measureCount);
            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void measure_enough_size_should_be_in_single_line()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetWidth(root, 100);

            YGNodeRef root_child0 = YGNodeNew();
            YGNodeStyleSetAlignSelf(root_child0, YGAlign.FlexStart);
            root_child0.setMeasureFunc(_simulate_wrapping_text);

            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(68, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(16, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void measure_not_enough_size_should_wrap()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetWidth(root, 55);

            YGNodeRef root_child0 = YGNodeNew();
            YGNodeStyleSetAlignSelf(root_child0, YGAlign.FlexStart);
            //  YGNodeSetMeasureFunc(root_child0, _simulate_wrapping_text);
            root_child0.setMeasureFunc(_simulate_wrapping_text);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(50, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(32, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void measure_zero_space_should_grow()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetHeight(root, 200);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Column);
            YGNodeStyleSetFlexGrow(root, 0);

            int measureCount = 0;

            YGNodeRef root_child0 = YGNodeNew();
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Column);
            YGNodeStyleSetPadding(root_child0, YGEdge.All, 100);
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measure);

            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, 282, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(282, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root_child0));

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void measure_flex_direction_row_and_padding()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetPadding(root, YGEdge.Left,   25);
            YGNodeStyleSetPadding(root, YGEdge.Top,    25);
            YGNodeStyleSetPadding(root, YGEdge.Right,  25);
            YGNodeStyleSetPadding(root, YGEdge.Bottom, 25);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 50);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            root_child0.setMeasureFunc(_simulate_wrapping_text);
            //  YGNodeSetMeasureFunc(root_child0, _simulate_wrapping_text);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 5);
            YGNodeStyleSetHeight(root_child1, 5);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(25, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(25, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(75, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(25, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void measure_flex_direction_column_and_padding()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root, YGEdge.Top, 20);
            YGNodeStyleSetPadding(root, YGEdge.All, 25);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 50);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            root_child0.setMeasureFunc(_simulate_wrapping_text);
            //  YGNodeSetMeasureFunc(root_child0, _simulate_wrapping_text);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 5);
            YGNodeStyleSetHeight(root_child1, 5);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root));
            Assert.AreEqual(20, YGNodeLayoutGetTop(root));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(25, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(25, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(32, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(25, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(57, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void measure_flex_direction_row_no_padding()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetMargin(root, YGEdge.Top, 20);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 50);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            //  YGNodeSetMeasureFunc(root_child0, _simulate_wrapping_text);
            root_child0.setMeasureFunc(_simulate_wrapping_text);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 5);
            YGNodeStyleSetHeight(root_child1, 5);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root));
            Assert.AreEqual(20, YGNodeLayoutGetTop(root));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(50, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void measure_flex_direction_row_no_padding_align_items_flexstart()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetMargin(root, YGEdge.Top, 20);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 50);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            root_child0.setMeasureFunc(_simulate_wrapping_text);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 5);
            YGNodeStyleSetHeight(root_child1, 5);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root));
            Assert.AreEqual(20, YGNodeLayoutGetTop(root));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(32, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(50, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void measure_with_fixed_size()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root, YGEdge.Top, 20);
            YGNodeStyleSetPadding(root, YGEdge.All, 25);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 50);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            root_child0.setMeasureFunc(_simulate_wrapping_text);
            YGNodeStyleSetWidth(root_child0, 10);
            YGNodeStyleSetHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 5);
            YGNodeStyleSetHeight(root_child1, 5);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root));
            Assert.AreEqual(20, YGNodeLayoutGetTop(root));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(25, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(25, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(25, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(35, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void measure_with_flex_shrink()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root, YGEdge.Top, 20);
            YGNodeStyleSetPadding(root, YGEdge.All, 25);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 50);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            root_child0.setMeasureFunc(_simulate_wrapping_text);
            YGNodeStyleSetFlexShrink(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 5);
            YGNodeStyleSetHeight(root_child1, 5);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root));
            Assert.AreEqual(20, YGNodeLayoutGetTop(root));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(25, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(25, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(25, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(25, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void measure_no_padding()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root, YGEdge.Top, 20);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 50);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            root_child0.setMeasureFunc(_simulate_wrapping_text);
            YGNodeStyleSetFlexShrink(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child1, 5);
            YGNodeStyleSetHeight(root_child1, 5);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root));
            Assert.AreEqual(20, YGNodeLayoutGetTop(root));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(32, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(32, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(5,  YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

#if GTEST_HAS_DEATH_TEST
TEST(YogaDeathTest, cannot_add_child_to_node_with_measure_func) {
   YGNodeRef root = YGNodeNew();
  root.setMeasureFunc(_measure);

   YGNodeRef root_child0 = YGNodeNew();
  ASSERT_DEATH(YGNodeInsertChild(root, root_child0, 0), "Cannot add child.*");
  YGNodeFree(root_child0);
  YGNodeFreeRecursive(root);
}

TEST(YogaDeathTest, cannot_add_nonnull_measure_func_to_non_leaf_node) {
   YGNodeRef root = YGNodeNew();
   YGNodeRef root_child0 = YGNodeNew();
  YGNodeInsertChild(root, root_child0, 0);
  ASSERT_DEATH(root.setMeasureFunc(_measure), "Cannot set measure function.*");
  YGNodeFreeRecursive(root);
}

#endif

        [TestMethod]
        public void can_nullify_measure_func_on_any_node()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeInsertChild(root, YGNodeNew(), 0);
            root.setMeasureFunc(null);
            Assert.IsTrue(root.getMeasure() == null);
            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void cant_call_negative_measure()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Column);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 10);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            root_child0.setMeasureFunc(_measure_assert_negative);
            YGNodeStyleSetMargin(root_child0, YGEdge.Top, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            YGNodeFreeRecursive(root);
            YGConfigFree(config);
        }

        [TestMethod]
        public void cant_call_negative_measure_horizontal()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 10);
            YGNodeStyleSetHeight(root, 20);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            root_child0.setMeasureFunc(_measure_assert_negative);
            YGNodeStyleSetMargin(root_child0, YGEdge.Start, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            YGNodeFreeRecursive(root);
            YGConfigFree(config);
        }

        static YGSize _measure_90_10(YGNodeRef node,
            float                              width,
            YGMeasureMode                      widthMode,
            float                              height,
            YGMeasureMode                      heightMode)
        {
            return new YGSize(width = 90, height = 10);
        }

        [TestMethod]
        public void percent_with_text_node()
        {
            YGConfigRef config = YGConfigNew();

            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetJustifyContent(root, YGJustify.SpaceBetween);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 80);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            root_child1.setMeasureFunc(_measure_90_10);
            YGNodeStyleSetMaxWidthPercent(root_child1, 50);
            YGNodeStyleSetPaddingPercent(root_child1, YGEdge.Top, 50);
            YGNodeInsertChild(root, root_child1, 1);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,   YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0,   YGNodeLayoutGetTop(root));
            Assert.AreEqual(100, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(80,  YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0,  YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(40, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(0,  YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(50, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(15, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(50, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }
    }
}
