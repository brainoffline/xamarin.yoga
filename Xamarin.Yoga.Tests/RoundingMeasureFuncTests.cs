using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class RoundingMeasureFuncTests
    {
        [TestMethod]
        public void rounding_feature_with_custom_measure_and_fractional_matching_scale()
        {
            var config = new YogaConfig();
            var root   = new YogaNode(config);

            var root_child0 = new YogaNode(config);
            root_child0.Position.Left = 73.625f;
            root_child0.MeasureFunc         = _measureFractional;
            root.Children.Add(root_child0);

            config.PointScaleFactor = 2.0f;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0.5f,  root_child0.Layout.Width);
            Assert.AreEqual(0.5f,  root_child0.Layout.Height);
            Assert.AreEqual(73.5f, root_child0.Layout.Position.Left);
        }

        [TestMethod]
        public void rounding_feature_with_custom_measure_func_ceil()
        {
            var config      = new YogaConfig();
            var root        = new YogaNode(config);
            var root_child0 = new YogaNode(config);

            root_child0.MeasureFunc = _measureCeil;
            root.Children.Add(root_child0);
            config.PointScaleFactor = 1.0f;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(11, root_child0.Layout.Width);
            Assert.AreEqual(11, root_child0.Layout.Height);
        }

        [TestMethod]
        public void rounding_feature_with_custom_measure_func_floor()
        {
            var      config = new YogaConfig();
            YogaNode root_child0;
            var root = new YogaNode(config)
            {
                Children =
                {
                    (root_child0 = new YogaNode(config) {MeasureFunc = _measureFloor})
                }
            };

            config.PointScaleFactor = 0.0f;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(10.2f, root_child0.Layout.Width);
            Assert.AreEqual(10.2f, root_child0.Layout.Height);

            config.PointScaleFactor = 1.0f;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(11f, root_child0.Layout.Width);
            Assert.AreEqual(11f, root_child0.Layout.Height);

            config.PointScaleFactor = 2.0f;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(10.5f, root_child0.Layout.Width);
            Assert.AreEqual(10.5f, root_child0.Layout.Height);

            config.PointScaleFactor = 4.0f;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(10.25f, root_child0.Layout.Width);
            Assert.AreEqual(10.25f, root_child0.Layout.Height);

            config.PointScaleFactor = 1.0f / 3.0f;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(12.0f, root_child0.Layout.Width);
            Assert.AreEqual(12.0f, root_child0.Layout.Height);
        }

        private static SizeF _measureCeil(YogaNode node,
            float                                  width,
            MeasureMode                            widthMode,
            float                                  height,
            MeasureMode                            heightMode)
        {
            return new SizeF(width = 10.5f, height = 10.5f);
        }

        private static SizeF _measureFloor(YogaNode node,
            float                                   width,
            MeasureMode                             widthMode,
            float                                   height,
            MeasureMode                             heightMode)
        {
            return new SizeF(width = 10.2f, height = 10.2f);
        }

        private static SizeF _measureFractional(YogaNode node,
            float                                        width,
            MeasureMode                                  widthMode,
            float                                        height,
            MeasureMode                                  heightMode)
        {
            return new SizeF(width = 0.5f, height = 0.5f);
        }
    }
}
