using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    
    using static YogaConst;
    
    
    

    [TestClass]
    public class YGRoundingMeasureFuncTests
    {
        static SizeF _measureFloor(YogaNode node,
            float                             width,
            MeasureMode                     widthMode,
            float                             height,
            MeasureMode                     heightMode)
        {
            return new SizeF(width = 10.2f, height = 10.2f);
        }

        static SizeF _measureCeil(YogaNode node,
            float                            width,
            MeasureMode                    widthMode,
            float                            height,
            MeasureMode                    heightMode)
        {
            return new SizeF(width = 10.5f, height = 10.5f);
        }

        static SizeF _measureFractial(YogaNode node,
            float                                width,
            MeasureMode                        widthMode,
            float                                height,
            MeasureMode                        heightMode)
        {
            return new SizeF(width = 0.5f, height = 0.5f);
        }

        [TestMethod]
        public void rounding_feature_with_custom_measure_func_floor()
        {
            YogaConfig config = new YogaConfig();
            YogaNode root_child0;
            YogaNode root = new YogaNode(config)
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

            config.PointScaleFactor = (1.0f / 3.0f);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(12.0f, root_child0.Layout.Width);
            Assert.AreEqual(12.0f, root_child0.Layout.Height);
        }

        [TestMethod]
        public void rounding_feature_with_custom_measure_func_ceil()
        {
            YogaConfig config      = new YogaConfig();
            YogaNode   root        = new YogaNode(config);
            YogaNode   root_child0 = new YogaNode(config);

            root_child0.MeasureFunc = _measureCeil;
            root.Children.Add(root_child0);
            config.PointScaleFactor = 1.0f;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(11, root_child0.Layout.Width);
            Assert.AreEqual(11, root_child0.Layout.Height);

            
        }

        [TestMethod]
        public void rounding_feature_with_custom_measure_and_fractial_matching_scale()
        {
            YogaConfig config = new YogaConfig();
            YogaNode   root   = new YogaNode(config);

            YogaNode root_child0 = new YogaNode(config);
            root_child0.Style.Position.Left = 73.625f;
            root_child0.MeasureFunc = _measureFractial;
            root.Children.Add(root_child0);

            config.PointScaleFactor = 2.0f;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0.5f,  root_child0.Layout.Width);
            Assert.AreEqual(0.5f,  root_child0.Layout.Height);
            Assert.AreEqual(73.5f, root_child0.Layout.Position.Left);

            
        }
    }
}
