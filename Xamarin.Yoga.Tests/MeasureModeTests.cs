using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class MeasureModeTests
    {
        [TestMethod]
        public void at_most_cross_axis_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                constraintList[0].Width);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].WidthMode);
        }

        [TestMethod]
        public void at_most_cross_axis_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Row;
            root.AlignItems    = AlignType.FlexStart;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                constraintList[0].Height);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].HeightMode);

            //free(constraintList.constraints);
        }

        [TestMethod]
        public void at_most_main_axis_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            var root = new YogaNode();
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                constraintList[0].Height);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].HeightMode);

            ////free(constraintList.constraints);
        }

        [TestMethod]
        public void at_most_main_axis_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                constraintList[0].Width);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].WidthMode);

            //free(constraintList.constraints);
        }

        [TestMethod]
        public void exactly_measure_stretched_child_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            var root = new YogaNode();
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode();
            //  root_child0.setContext(&constraintList);
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            //  root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                 constraintList[0].Width);
            Assert.AreEqual(MeasureMode.Exactly, constraintList[0].WidthMode);

            ////free(constraintList.constraints);
        }

        [TestMethod]
        public void exactly_measure_stretched_child_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            //  root_child0.setContext(&constraintList);
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                 constraintList[0].Height);
            Assert.AreEqual(MeasureMode.Exactly, constraintList[0].HeightMode);

            ////free(constraintList.constraints);
        }

        [TestMethod]
        public void flex_child()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            var root = new YogaNode();
            root.Height = 100;

            var root_child0 = new YogaNode();
            root_child0.FlexGrow = 1;
            root_child0.Context        = constraintList;
            root_child0.MeasureFunc    = _measure;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(2, constraintList.Count);

            Assert.AreEqual(100,                constraintList[0].Height);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].HeightMode);

            Assert.AreEqual(100,                 constraintList[1].Height);
            Assert.AreEqual(MeasureMode.Exactly, constraintList[1].HeightMode);

            //free(constraintList.constraints);
        }

        [TestMethod]
        public void flex_child_with_flex_basis()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            var root = new YogaNode();
            root.Height = 100;

            var root_child0 = new YogaNode();
            root_child0.FlexGrow  = 1;
            root_child0.FlexBasis = 0;
            root_child0.Context         = constraintList;
            root_child0.MeasureFunc     = _measure;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                 constraintList[0].Height);
            Assert.AreEqual(MeasureMode.Exactly, constraintList[0].HeightMode);

            //free(constraintList.constraints);
        }

        [TestMethod]
        public void overflow_scroll_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Overflow   = OverflowType.Scroll;
            root.Height     = 100;
            root.Width      = 100;

            var root_child0 = new YogaNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                constraintList[0].Width);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].WidthMode);

            Assert.IsTrue(constraintList[0].Height.IsNaN());
            Assert.AreEqual(MeasureMode.Undefined, constraintList[0].HeightMode);

            //free(constraintList.constraints);
        }

        [TestMethod]
        public void overflow_scroll_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            var root = new YogaNode();
            root.AlignItems    = AlignType.FlexStart;
            root.FlexDirection = FlexDirectionType.Row;
            root.Overflow      = OverflowType.Scroll;
            root.Height        = 100;
            root.Width         = 100;

            var root_child0 = new YogaNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.IsTrue(constraintList[0].Width.IsNaN());
            Assert.AreEqual(MeasureMode.Undefined, constraintList[0].WidthMode);

            Assert.AreEqual(100,                constraintList[0].Height);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].HeightMode);

            //free(constraintList.constraints);
        }

        private static SizeF _measure(YogaNode node,
            float                              width,
            MeasureMode                        widthMode,
            float                              height,
            MeasureMode                        heightMode)
        {
            var constraintList = (List<_MeasureConstraint>) node.Context;
            var constraint = new _MeasureConstraint
            {
                Width      = width,
                WidthMode  = widthMode,
                Height     = height,
                HeightMode = heightMode
            };
            constraintList.Add(constraint);

            return new SizeF(
                width = widthMode   == MeasureMode.Undefined ? 10 : width,
                height = heightMode == MeasureMode.Undefined ? 10 : width
            );
        }

        private class _MeasureConstraint
        {
            public float       Height;
            public MeasureMode HeightMode;
            public float       Width;
            public MeasureMode WidthMode;
        }
    }
}
