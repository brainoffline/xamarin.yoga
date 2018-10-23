using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YogaConst;
    
    
    

    [TestClass]
    public class YGRoundingMeasureFuncTests
    {
        static SizeF _measureFloor(YGNode node,
            float                             width,
            MeasureMode                     widthMode,
            float                             height,
            MeasureMode                     heightMode)
        {
            return new SizeF(width = 10.2f, height = 10.2f);
        }

        static SizeF _measureCeil(YGNode node,
            float                            width,
            MeasureMode                    widthMode,
            float                            height,
            MeasureMode                    heightMode)
        {
            return new SizeF(width = 10.5f, height = 10.5f);
        }

        static SizeF _measureFractial(YGNode node,
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
            YGNode   root   = new YGNode(config);

            YGNode root_child0 = new YGNode(config);
            root_child0.MeasureFunc = _measureFloor;
            root.Children.Add(root_child0);

            YGConfigSetPointScaleFactor(config, 0.0f);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(10.2f, root_child0.Layout.Width);
            Assert.AreEqual(10.2f, root_child0.Layout.Height);

            YGConfigSetPointScaleFactor(config, 1.0f);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(11f, root_child0.Layout.Width);
            Assert.AreEqual(11f, root_child0.Layout.Height);

            YGConfigSetPointScaleFactor(config, 2.0f);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(10.5f, root_child0.Layout.Width);
            Assert.AreEqual(10.5f, root_child0.Layout.Height);

            YGConfigSetPointScaleFactor(config, 4.0f);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(10.25f, root_child0.Layout.Width);
            Assert.AreEqual(10.25f, root_child0.Layout.Height);

            YGConfigSetPointScaleFactor(config, 1.0f / 3.0f);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(12.0f, root_child0.Layout.Width);
            Assert.AreEqual(12.0f, root_child0.Layout.Height);

            
        }

        [TestMethod]
        public void rounding_feature_with_custom_measure_func_ceil()
        {
            YogaConfig config      = new YogaConfig();
            YGNode   root        = new YGNode(config);
            YGNode   root_child0 = new YGNode(config);

            root_child0.MeasureFunc = _measureCeil;
            root.Children.Add(root_child0);
            YGConfigSetPointScaleFactor(config, 1.0f);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(11, root_child0.Layout.Width);
            Assert.AreEqual(11, root_child0.Layout.Height);

            
        }

        [TestMethod]
        public void rounding_feature_with_custom_measure_and_fractial_matching_scale()
        {
            YogaConfig config = new YogaConfig();
            YGNode   root   = new YGNode(config);

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Position.Left = 73.625f;
            root_child0.MeasureFunc = _measureFractial;
            root.Children.Add(root_child0);

            YGConfigSetPointScaleFactor(config, 2.0f);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0.5f,  root_child0.Layout.Width);
            Assert.AreEqual(0.5f,  root_child0.Layout.Height);
            Assert.AreEqual(73.5f, root_child0.Layout.Position.Left);

            
        }
    }
}
