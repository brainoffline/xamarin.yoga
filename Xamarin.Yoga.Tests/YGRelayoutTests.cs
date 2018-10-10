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
    public class YGRelayoutTests
    {
        [TestMethod]
        public void dont_cache_computed_flex_basis_between_layouts()
        {
             YGConfigRef config = YGConfigNew();
            YGConfigSetExperimentalFeatureEnabled(config, YGExperimentalFeature.WebFlexBasis, true);

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeightPercent(root, 100);
            YGNodeStyleSetWidthPercent(root, 100);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexBasisPercent(root_child0, 100);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, 100, YGUndefined, YGDirection.LTR);
            YGNodeCalculateLayout(root, 100, 100,         YGDirection.LTR);

            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void recalculate_resolvedDimonsion_onchange()
        {
             YGNodeRef root = YGNodeNew();

             YGNodeRef root_child0 = YGNodeNew();
            YGNodeStyleSetMinHeight(root_child0, 10);
            YGNodeStyleSetMaxHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);
            Assert.AreEqual(10, YGNodeLayoutGetHeight(root_child0));

            YGNodeStyleSetMinHeight(root_child0, YGUndefined);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);
        }

    }
}
