using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class RelayoutTests
    {
        [TestMethod]
        public void dont_cache_computed_flex_basis_between_layouts()
        {
            var config = new YogaConfig();
            config.ExperimentalFeatures |= ExperimentalFeatures.WebFlexBasis;

            var root = new YogaNode(config);
            root.Width  = 100.Percent();
            root.Height = 100.Percent();

            var root_child0 = new YogaNode(config);
            root_child0.FlexBasis = 100.Percent();
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(100, float.NaN, DirectionType.LTR);
            root.Calc.CalculateLayout(100, 100,       DirectionType.LTR);

            Assert.AreEqual(100, root_child0.Layout.Height);
        }

        [TestMethod]
        public void recalculate_resolvedDimension_onchange()
        {
            var root = new YogaNode();

            var root_child0 = new YogaNode();
            root_child0.MinHeight = 10;
            root_child0.MaxHeight = 10;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root_child0.MinHeight = float.NaN;
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0, root_child0.Layout.Height);
        }
    }
}
