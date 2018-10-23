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
    public class YGMeasureModeTests
    {
        public class _MeasureConstraint
        {
            public float         width;
            public MeasureMode widthMode;
            public float         height;
            public MeasureMode heightMode;
        };

        private static SizeF _measure(YGNode node,
            float                             width,
            MeasureMode                     widthMode,
            float                             height,
            MeasureMode                     heightMode)
        {
            var constraintList = (List<_MeasureConstraint>) node.Context;
            var constraint = new _MeasureConstraint
            {
                width      = width,
                widthMode  = widthMode,
                height     = height,
                heightMode = heightMode
            };
            constraintList.Add(constraint);

            return new SizeF(
                width = widthMode   == MeasureMode.Undefined ? 10 : width,
                height = heightMode == MeasureMode.Undefined ? 10 : width
            );
        }

        [TestMethod]
        public void exactly_measure_stretched_child_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = new YGNode();
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            //  root_child0.setContext(&constraintList);
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            //  root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                   constraintList[0].width);
            Assert.AreEqual(MeasureMode.Exactly, constraintList[0].widthMode);

            ////free(constraintList.constraints);
        }

        [TestMethod]
        public void exactly_measure_stretched_child_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = new YGNode();
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            //  root_child0.setContext(&constraintList);
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                   constraintList[0].height);
            Assert.AreEqual(MeasureMode.Exactly, constraintList[0].heightMode);

            ////free(constraintList.constraints);
        }

        [TestMethod]
        public void at_most_main_axis_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = new YGNode();
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].height);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].heightMode);

            ////free(constraintList.constraints);
        }

        [TestMethod]
        public void at_most_cross_axis_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].width);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].widthMode);

            ////free(constraintList.constraints);
        }

        [TestMethod]
        public void at_most_main_axis_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = new YGNode();
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].width);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].widthMode);

            //free(constraintList.constraints);
        }

        [TestMethod]
        public void at_most_cross_axis_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = new YGNode();
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].height);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].heightMode);

            //free(constraintList.constraints);
        }

        [TestMethod]
        public void flex_child()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = new YGNode();
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.FlexGrow = 1;
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(2, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].height);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].heightMode);

            Assert.AreEqual(100,                   constraintList[1].height);
            Assert.AreEqual(MeasureMode.Exactly, constraintList[1].heightMode);

            //free(constraintList.constraints);
        }

        [TestMethod]
        public void flex_child_with_flex_basis()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = new YGNode();
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 0;
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                   constraintList[0].height);
            Assert.AreEqual(MeasureMode.Exactly, constraintList[0].heightMode);

            //free(constraintList.constraints);
        }

        [TestMethod]
        public void overflow_scroll_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Overflow = OverflowType.Scroll;
            root.Style.Height = 100;
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].width);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].widthMode);

            Assert.IsTrue(constraintList[0].height.IsNaN());
            Assert.AreEqual(MeasureMode.Undefined, constraintList[0].heightMode);

            //free(constraintList.constraints);
        }

        [TestMethod]
        public void overflow_scroll_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Overflow = OverflowType.Scroll;
            root.Style.Height = 100;
            root.Style.Width = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Context     = constraintList;
            root_child0.MeasureFunc = _measure;
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.IsTrue(constraintList[0].width.IsNaN());
            Assert.AreEqual(MeasureMode.Undefined, constraintList[0].widthMode);

            Assert.AreEqual(100,                  constraintList[0].height);
            Assert.AreEqual(MeasureMode.AtMost, constraintList[0].heightMode);

            //free(constraintList.constraints);
        }
    }
}
