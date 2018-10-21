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

             YGNode root = new YGNode(config);
            root.StyleSetWidthPercent(100);
            root.StyleSetHeightPercent(100);

             YGNode root_child0 = new YGNode(config);
            root_child0.StyleSetFlexBasisPercent(100);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, 100, float.NaN, YGDirection.LTR);
            YGNodeCalculateLayout(root, 100, 100,         YGDirection.LTR);

            Assert.AreEqual(100, root_child0.Layout.Height);

            

            
        }

        [TestMethod]
        public void recalculate_resolvedDimonsion_onchange()
        {
             YGNode root = new YGNode();

             YGNode root_child0 = new YGNode();
            root_child0.StyleSetMinHeight(10);
            root_child0.StyleSetMaxHeight(10);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root_child0.StyleSetMinHeight(float.NaN);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0, root_child0.Layout.Height);

            
        }

    }
}
