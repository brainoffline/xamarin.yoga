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
            float                           width,
            YGMeasureMode                   widthMode,
            float                           height,
            YGMeasureMode                   heightMode)
        {
            int measureCount = (int) node.Context;
            node.Context = ++measureCount;

            return new YGSize(
                width = widthMode   == YGMeasureMode.Undefined ? 10 : width,
                height = heightMode == YGMeasureMode.Undefined ? 10 : height
            );
        }

        static YGSize _measureMin(YGNode node,
            float                           width,
            YGMeasureMode                   widthMode,
            float                           height,
            YGMeasureMode                   heightMode)
        {
            int measureCount = (int) node.Context;
            node.Context = ++measureCount;
            return new YGSize(
                width = widthMode   == YGMeasureMode.Undefined || (widthMode  == YGMeasureMode.AtMost && width  > 10) ? 10 : width,
                height = heightMode == YGMeasureMode.Undefined || (heightMode == YGMeasureMode.AtMost && height > 10) ? 10 : height
            );
        }

        static YGSize _measure_84_49(YGNode node,
            float                              width,
            YGMeasureMode                      widthMode,
            float                              height,
            YGMeasureMode                      heightMode)
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
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0  = new YGNode();
            int       measureCount = 0;
            root_child0.Context = measureCount;
            root_child0.MeasureFunc = _measureMax;
            YGNodeStyleSetFlexGrow(root_child0, 1);
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void remeasure_with_same_exact_width_larger_than_needed_height()
        {
            YGNode root = new YGNode();

            YGNode root_child0  = new YGNode();
            int       measureCount = 0;
            root_child0.Context = measureCount;
            root_child0.MeasureFunc = _measureMin;
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);
            YGNodeCalculateLayout(root, 100, 50,  YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void remeasure_with_same_atmost_width_larger_than_needed_height()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);

            YGNode root_child0  = new YGNode();
            int       measureCount = 0;
            root_child0.Context = measureCount;
            root_child0.MeasureFunc = _measureMin;
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);
            YGNodeCalculateLayout(root, 100, 50,  YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void remeasure_with_computed_width_larger_than_needed_height()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);

            YGNode root_child0  = new YGNode();
            int       measureCount = 0;
            root_child0.Context = measureCount;
            root_child0.MeasureFunc = _measureMin;
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);
            YGNodeStyleSetAlignItems(root, YGAlign.Stretch);
            YGNodeCalculateLayout(root, 10, 50, YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void remeasure_with_atmost_computed_width_undefined_height()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);

            YGNode root_child0  = new YGNode();
            int       measureCount = 0;
            root_child0.Context = measureCount;
            root_child0.MeasureFunc = _measureMin;
            root.InsertChild(root_child0, 0);

            YGNodeCalculateLayout(root, 100, float.NaN, YGDirection.LTR);
            YGNodeCalculateLayout(root, 10,  float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void remeasure_with_already_measured_value_smaller_but_still_float_equal()
        {
            int measureCount = 0;

            YGNode root = new YGNode();
            YGNodeStyleSetWidth(root, 288f);
            YGNodeStyleSetHeight(root, 288f);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetPadding(root_child0, YGEdge.All, 2.88f);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            root.InsertChild(root_child0, 0);

            YGNode root_child0_child0 = new YGNode();
            root_child0_child0.Context = measureCount;
            root_child0_child0.MeasureFunc = _measure_84_49;
            root_child0.InsertChild(root_child0_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            YGNodeFreeRecursive(root);

            Assert.AreEqual(1, measureCount);
        }
    }
}
