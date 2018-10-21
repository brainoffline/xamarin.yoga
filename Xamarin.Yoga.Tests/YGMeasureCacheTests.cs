using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGMeasureCacheTests
    {
        static YGSize _measureMax(YGNode node,
            float                        width,
            YGMeasureMode                widthMode,
            float                        height,
            YGMeasureMode                heightMode)
        {
            int measureCount = (int) node.Context;
            node.Context = ++measureCount;

            return new YGSize(
                width = widthMode   == YGMeasureMode.Undefined ? 10 : width,
                height = heightMode == YGMeasureMode.Undefined ? 10 : height
            );
        }

        static YGSize _measureMin(YGNode node,
            float                        width,
            YGMeasureMode                widthMode,
            float                        height,
            YGMeasureMode                heightMode)
        {
            int measureCount = (int) node.Context;
            node.Context = ++measureCount;
            return new YGSize(
                width = widthMode   == YGMeasureMode.Undefined || (widthMode  == YGMeasureMode.AtMost && width  > 10) ? 10 : width,
                height = heightMode == YGMeasureMode.Undefined || (heightMode == YGMeasureMode.AtMost && height > 10) ? 10 : height
            );
        }

        static YGSize _measure_84_49(YGNode node,
            float                           width,
            YGMeasureMode                   widthMode,
            float                           height,
            YGMeasureMode                   heightMode)
        {
            int measureCount = (int) node.Context;
            node.Context = ++measureCount;

            return new YGSize(
                width = 84f,
                height = 49f
            );
        }

        [TestMethod]
        public void measure_once_single_flexible_child()
        {
            YGNode root = new YGNode();
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            root.StyleSetAlignItems(YGAlign.FlexStart);
            root.StyleSetWidth(100);
            root.StyleSetHeight(100);

            YGNode root_child0  = new YGNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measureMax;
            root_child0.StyleSetFlexGrow(1);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            int measureCount = (int) root_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void remeasure_with_same_exact_width_larger_than_needed_height()
        {
            YGNode root = new YGNode();

            YGNode root_child0  = new YGNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measureMin;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);
            YGNodeCalculateLayout(root, 100, 50,  YGDirection.LTR);

            int measureCount = (int)root_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void remeasure_with_same_atmost_width_larger_than_needed_height()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);

            YGNode root_child0  = new YGNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measureMin;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);
            YGNodeCalculateLayout(root, 100, 50,  YGDirection.LTR);

            int measureCount = (int)root_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void remeasure_with_computed_width_larger_than_needed_height()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);

            YGNode root_child0  = new YGNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measureMin;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);
            root.StyleSetAlignItems(YGAlign.Stretch);
            YGNodeCalculateLayout(root, 10, 50, YGDirection.LTR);

            int measureCount = (int)root_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void remeasure_with_atmost_computed_width_undefined_height()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);

            YGNode root_child0  = new YGNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measureMin;
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, 100, float.NaN, YGDirection.LTR);
            YGNodeCalculateLayout(root, 10,  float.NaN, YGDirection.LTR);

            int measureCount = (int)root_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void remeasure_with_already_measured_value_smaller_but_still_float_equal()
        {
            YGNode root = new YGNode();
            root.StyleSetWidth(288f);
            root.StyleSetHeight(288f);
            root.StyleSetFlexDirection(YGFlexDirection.Row);

            YGNode root_child0 = new YGNode();
            root_child0.StyleSetPadding(YGEdge.All, 2.88f);
            root_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode();
            root_child0_child0.Context     = 0;
            root_child0_child0.MeasureFunc = _measure_84_49;
            root_child0.InsertChild(root_child0_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            int measureCount = (int)root_child0_child0.Context;
            Assert.AreEqual(1, measureCount);
        }
    }
}
