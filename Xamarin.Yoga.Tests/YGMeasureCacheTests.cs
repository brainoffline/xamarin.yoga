﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    
    using static YogaConst;


    [TestClass]
    public class YGMeasureCacheTests
    {
        static SizeF _measureMax(YogaNode node,
            float                        width,
            MeasureMode                widthMode,
            float                        height,
            MeasureMode                heightMode)
        {
            int measureCount = (int) node.Context;
            node.Context = ++measureCount;

            return new SizeF(
                width = widthMode   == MeasureMode.Undefined ? 10 : width,
                height = heightMode == MeasureMode.Undefined ? 10 : height
            );
        }

        static SizeF _measureMin(YogaNode node,
            float                        width,
            MeasureMode                widthMode,
            float                        height,
            MeasureMode                heightMode)
        {
            int measureCount = (int) node.Context;
            node.Context = ++measureCount;
            return new SizeF(
                width = widthMode   == MeasureMode.Undefined || (widthMode  == MeasureMode.AtMost && width  > 10) ? 10 : width,
                height = heightMode == MeasureMode.Undefined || (heightMode == MeasureMode.AtMost && height > 10) ? 10 : height
            );
        }

        static SizeF _measure_84_49(YogaNode node,
            float                           width,
            MeasureMode                   widthMode,
            float                           height,
            MeasureMode                   heightMode)
        {
            int measureCount = (int) node.Context;
            node.Context = ++measureCount;

            return new SizeF(width = 84f, height = 49f);
        }

        [TestMethod]
        public void measure_once_single_flexible_child()
        {
            YogaNode root = new YogaNode();
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0  = new YogaNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measureMax;
            root_child0.Style.FlexGrow = 1;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            int measureCount = (int) root_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void remeasure_with_same_exact_width_larger_than_needed_height()
        {
            YogaNode root = new YogaNode();

            YogaNode root_child0  = new YogaNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measureMin;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(100, 100, DirectionType.LTR);
            root.Calc.CalculateLayout(100, 50,  DirectionType.LTR);

            int measureCount = (int)root_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void remeasure_with_same_atmost_width_larger_than_needed_height()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;

            YogaNode root_child0  = new YogaNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measureMin;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(100, 100, DirectionType.LTR);
            root.Calc.CalculateLayout(100, 50,  DirectionType.LTR);

            int measureCount = (int)root_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void remeasure_with_computed_width_larger_than_needed_height()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;

            YogaNode root_child0  = new YogaNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measureMin;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(100, 100, DirectionType.LTR);
            root.Style.AlignItems = AlignType.Stretch;
            root.Calc.CalculateLayout(10, 50, DirectionType.LTR);

            int measureCount = (int)root_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void remeasure_with_atmost_computed_width_undefined_height()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;

            YogaNode root_child0  = new YogaNode();
            root_child0.Context     = 0;
            root_child0.MeasureFunc = _measureMin;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(100, float.NaN, DirectionType.LTR);
            root.Calc.CalculateLayout(10,  float.NaN, DirectionType.LTR);

            int measureCount = (int)root_child0.Context;
            Assert.AreEqual(1, measureCount);
        }

        [TestMethod]
        public void remeasure_with_already_measured_value_smaller_but_still_float_equal()
        {
            YogaNode root = new YogaNode();
            root.Style.Width = 288f;
            root.Style.Height = 288f;
            root.Style.FlexDirection = FlexDirectionType.Row;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Padding.All = 2.88f;
            root_child0.Style.FlexDirection = FlexDirectionType.Row;
            root.Children.Add(root_child0);

            YogaNode root_child0_child0 = new YogaNode();
            root_child0_child0.Context     = 0;
            root_child0_child0.MeasureFunc = _measure_84_49;
            root_child0.Children.Add(root_child0_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            int measureCount = (int)root_child0_child0.Context;
            Assert.AreEqual(1, measureCount);
        }
    }
}
