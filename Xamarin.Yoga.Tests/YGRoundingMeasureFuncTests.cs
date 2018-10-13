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
    public class YGRoundingMeasureFuncTests
    {
        static YGSize _measureFloor(YGNodeRef node,
                            float width,
                            YGMeasureMode widthMode,
                            float height,
                            YGMeasureMode heightMode)
        {
            return new YGSize {width = 10.2f, height = 10.2f};
        }

        static YGSize _measureCeil(YGNodeRef node,
                                   float width,
                                   YGMeasureMode widthMode,
                                   float height,
                                   YGMeasureMode heightMode)
        {
            return new YGSize{width = 10.5f, height = 10.5f};
        }

        static YGSize _measureFractial(YGNodeRef node,
          float width,
          YGMeasureMode widthMode,
          float height,
          YGMeasureMode heightMode)
        {
            return new YGSize{width = 0.5f, height = 0.5f};
        }

        [TestMethod]
        public void rounding_feature_with_custom_measure_func_floor()
        {
             YGConfigRef config = YGConfigNew();
             YGNodeRef root = YGNodeNewWithConfig(config);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            root_child0.setMeasureFunc(_measureFloor);
            YGNodeInsertChild(root, root_child0, 0);

            YGConfigSetPointScaleFactor(config, 0.0f);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(10.2, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10.2, YGNodeLayoutGetHeight(root_child0));

            YGConfigSetPointScaleFactor(config, 1.0f);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(11, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(11, YGNodeLayoutGetHeight(root_child0));

            YGConfigSetPointScaleFactor(config, 2.0f);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(10.5, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10.5, YGNodeLayoutGetHeight(root_child0));

            YGConfigSetPointScaleFactor(config, 4.0f);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(10.25, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10.25, YGNodeLayoutGetHeight(root_child0));

            YGConfigSetPointScaleFactor(config, 1.0f / 3.0f);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(12.0, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(12.0, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void rounding_feature_with_custom_measure_func_ceil()
        {
            YGConfigRef config = YGConfigNew();
            YGNodeRef root = YGNodeNewWithConfig(config);

            YGNodePrint(root, YGPrintOptions.All);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);

            YGNodePrint(root_child0, YGPrintOptions.All);

            root_child0.setMeasureFunc(_measureCeil);

            YGNodePrint(root_child0, YGPrintOptions.All);

            YGNodeInsertChild(root, root_child0, 0);

            YGNodePrint(root, YGPrintOptions.All);

            YGConfigSetPointScaleFactor(config, 1.0f);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            YGNodePrint(root, YGPrintOptions.All);

            Assert.AreEqual(11, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(11, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void rounding_feature_with_custom_measure_and_fractial_matching_scale()
        {
             YGConfigRef config = YGConfigNew();
             YGNodeRef root = YGNodeNewWithConfig(config);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetPosition(root_child0, YGEdge.Left, 73.625f);
            root_child0.setMeasureFunc(_measureFractial);
            YGNodeInsertChild(root, root_child0, 0);

            YGConfigSetPointScaleFactor(config, 2.0f);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0.5, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(0.5, YGNodeLayoutGetHeight(root_child0));
            Assert.AreEqual(73.5, YGNodeLayoutGetLeft(root_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

    }
}
