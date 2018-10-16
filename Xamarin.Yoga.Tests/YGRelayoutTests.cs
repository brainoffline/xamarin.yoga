using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGRelayoutTests
    {
        [TestMethod]
        public void dont_cache_computed_flex_basis_between_layouts()
        {
            YGConfig config = new YGConfig();
            config.ExperimentalFeatures |= YGExperimentalFeatures.WebFlexBasis;

             YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeightPercent(root, 100);
            YGNodeStyleSetWidthPercent(root, 100);

             YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexBasisPercent(root_child0, 100);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, 100, float.NaN, YGDirection.LTR);
            YGNodeCalculateLayout(root, 100, 100,         YGDirection.LTR);

            Assert.AreEqual(100, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }

        [TestMethod]
        public void recalculate_resolvedDimonsion_onchange()
        {
             YGNode root = YGNodeNew();

             YGNode root_child0 = YGNodeNew();
            YGNodeStyleSetMinHeight(root_child0, 10);
            YGNodeStyleSetMaxHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeStyleSetMinHeight(root_child0, float.NaN);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root_child0.Layout.Height);

            YGNodeFreeRecursive(root);
        }

    }
}
