using System;
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
    public class YGMeasureCacheTests
    {
        static YGSize _measureMax(YGNodeRef node,
            float                           width,
            YGMeasureMode                   widthMode,
            float                           height,
            YGMeasureMode                   heightMode)
        {
            int measureCount = (int) node.getContext();
            node.setContext(++measureCount);

            return new YGSize(
                width = widthMode   == YGMeasureMode.Undefined ? 10 : width,
                height = heightMode == YGMeasureMode.Undefined ? 10 : height
            );
        }

        static YGSize _measureMin(YGNodeRef node,
            float                           width,
            YGMeasureMode                   widthMode,
            float                           height,
            YGMeasureMode                   heightMode)
        {
            int measureCount = (int) node.getContext();
            node.setContext(++measureCount);
            return new YGSize(
                width = widthMode   == YGMeasureMode.Undefined || (widthMode  == YGMeasureMode.AtMost && width  > 10) ? 10 : width,
                height = heightMode == YGMeasureMode.Undefined || (heightMode == YGMeasureMode.AtMost && height > 10) ? 10 : height
            );
        }

        static YGSize _measure_84_49(YGNodeRef node,
            float                              width,
            YGMeasureMode                      widthMode,
            float                              height,
            YGMeasureMode                      heightMode)
        {
            int measureCount = (int) node.getContext();
            node.setContext(++measureCount);

            return new YGSize(
                width = 84f,
                height = 49f
            );
        }

        [TestMethod]
        public void measure_once_single_flexible_child()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNodeRef root_child0  = YGNodeNew();
            int       measureCount = 0;
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measureMax);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void remeasure_with_same_exact_width_larger_than_needed_height()
        {
            YGNodeRef root = YGNodeNew();

            YGNodeRef root_child0  = YGNodeNew();
            int       measureCount = 0;
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measureMin);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);
            YGNodeCalculateLayout(root, 100, 50,  YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void remeasure_with_same_atmost_width_larger_than_needed_height()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);

            YGNodeRef root_child0  = YGNodeNew();
            int       measureCount = 0;
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measureMin);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);
            YGNodeCalculateLayout(root, 100, 50,  YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void remeasure_with_computed_width_larger_than_needed_height()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);

            YGNodeRef root_child0  = YGNodeNew();
            int       measureCount = 0;
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measureMin);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);
            YGNodeStyleSetAlignItems(root, YGAlign.Stretch);
            YGNodeCalculateLayout(root, 10, 50, YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void remeasure_with_atmost_computed_width_undefined_height()
        {
            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);

            YGNodeRef root_child0  = YGNodeNew();
            int       measureCount = 0;
            root_child0.setContext(measureCount);
            root_child0.setMeasureFunc(_measureMin);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, 100, YGUndefined, YGDirection.LTR);
            YGNodeCalculateLayout(root, 10,  YGUndefined, YGDirection.LTR);

            Assert.AreEqual(1, measureCount);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void remeasure_with_already_measured_value_smaller_but_still_float_equal()
        {
            int measureCount = 0;

            YGNodeRef root = YGNodeNew();
            YGNodeStyleSetWidth(root, 288f);
            YGNodeStyleSetHeight(root, 288f);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);

            YGNodeRef root_child0 = YGNodeNew();
            YGNodeStyleSetPadding(root_child0, YGEdge.All, 2.88f);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child0_child0 = YGNodeNew();
            root_child0_child0.setContext(measureCount);
            root_child0_child0.setMeasureFunc(_measure_84_49);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            YGNodeFreeRecursive(root);

            Assert.AreEqual(1, measureCount);
        }
    }
}
