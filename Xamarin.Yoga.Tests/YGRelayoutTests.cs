using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YogaConst;
    
    
    

    [TestClass]
    public class YGRelayoutTests
    {
        [TestMethod]
        public void dont_cache_computed_flex_basis_between_layouts()
        {
            YogaConfig config = new YogaConfig();
            config.ExperimentalFeatures |= ExperimentalFeatures.WebFlexBasis;

             YGNode root = new YGNode(config);
            root.Style.Width = 100.Percent();
            root.Style.Height = 100.Percent();

             YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexBasis = 100.Percent();
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(100, float.NaN, DirectionType.LTR);
            root.Calc.CalculateLayout(100, 100,         DirectionType.LTR);

            Assert.AreEqual(100, root_child0.Layout.Height);

            

            
        }

        [TestMethod]
        public void recalculate_resolvedDimonsion_onchange()
        {
             YGNode root = new YGNode();

             YGNode root_child0 = new YGNode();
            root_child0.Style.MinHeight = 10;
            root_child0.Style.MaxHeight = 10;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root_child0.Style.MinHeight = float.NaN;
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0, root_child0.Layout.Height);

            
        }

    }
}
