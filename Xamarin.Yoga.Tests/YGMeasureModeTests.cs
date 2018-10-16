using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGMeasureModeTests
    {
        public class _MeasureConstraint
        {
            public float         width;
            public YGMeasureMode widthMode;
            public float         height;
            public YGMeasureMode heightMode;
        };

        private static YGSize _measure(YGNode node,
            float                                width,
            YGMeasureMode                        widthMode,
            float                                height,
            YGMeasureMode                        heightMode)
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

            return new YGSize(
                width = widthMode   == YGMeasureMode.Undefined ? 10 : width,
                height = heightMode == YGMeasureMode.Undefined ? 10 : width
            );
        }

        [TestMethod]
        public void exactly_measure_stretched_child_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = YGNodeNew();
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNew();
            //  root_child0.setContext(&constraintList);
            root_child0.Context = constraintList;
            root_child0.MeasureFunc = _measure;
            //  root_child0.MeasureFunc = _measure;
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                   constraintList[0].width);
            Assert.AreEqual(YGMeasureMode.Exactly, constraintList[0].widthMode);

            ////free(constraintList.constraints);
            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void exactly_measure_stretched_child_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNew();
            //  root_child0.setContext(&constraintList);
            root_child0.Context = constraintList;
            root_child0.MeasureFunc = _measure;
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                   constraintList[0].height);
            Assert.AreEqual(YGMeasureMode.Exactly, constraintList[0].heightMode);

            ////free(constraintList.constraints);
            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void at_most_main_axis_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = YGNodeNew();
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNew();
            root_child0.Context = constraintList;
            root_child0.MeasureFunc = _measure;
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].height);
            Assert.AreEqual(YGMeasureMode.AtMost, constraintList[0].heightMode);

            ////free(constraintList.constraints);
            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void at_most_cross_axis_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNew();
            root_child0.Context = constraintList;
            root_child0.MeasureFunc = _measure;
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].width);
            Assert.AreEqual(YGMeasureMode.AtMost, constraintList[0].widthMode);

            ////free(constraintList.constraints);
            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void at_most_main_axis_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNew();
            root_child0.Context = constraintList;
            root_child0.MeasureFunc = _measure;
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].width);
            Assert.AreEqual(YGMeasureMode.AtMost, constraintList[0].widthMode);

            //free(constraintList.constraints);
            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void at_most_cross_axis_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNew();
            root_child0.Context = constraintList;
            root_child0.MeasureFunc = _measure;
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].height);
            Assert.AreEqual(YGMeasureMode.AtMost, constraintList[0].heightMode);

            //free(constraintList.constraints);
            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void flex_child()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = YGNodeNew();
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNew();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            root_child0.Context = constraintList;
            root_child0.MeasureFunc = _measure;
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(2, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].height);
            Assert.AreEqual(YGMeasureMode.AtMost, constraintList[0].heightMode);

            Assert.AreEqual(100,                   constraintList[1].height);
            Assert.AreEqual(YGMeasureMode.Exactly, constraintList[1].heightMode);

            //free(constraintList.constraints);
            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void flex_child_with_flex_basis()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = YGNodeNew();
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = YGNodeNew();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasis(root_child0, 0);
            root_child0.Context = constraintList;
            root_child0.MeasureFunc = _measure;
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                   constraintList[0].height);
            Assert.AreEqual(YGMeasureMode.Exactly, constraintList[0].heightMode);

            //free(constraintList.constraints);
            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void overflow_scroll_column()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetOverflow(root, YGOverflow.Scroll);
            YGNodeStyleSetHeight(root, 100);
            YGNodeStyleSetWidth(root, 100);

            YGNode root_child0 = YGNodeNew();
            root_child0.Context = constraintList;
            root_child0.MeasureFunc = _measure;
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.AreEqual(100,                  constraintList[0].width);
            Assert.AreEqual(YGMeasureMode.AtMost, constraintList[0].widthMode);

            Assert.IsTrue(constraintList[0].height.IsNaN());
            Assert.AreEqual(YGMeasureMode.Undefined, constraintList[0].heightMode);

            //free(constraintList.constraints);
            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void overflow_scroll_row()
        {
            var constraintList = new List<_MeasureConstraint>(10);

            YGNode root = YGNodeNew();
            YGNodeStyleSetAlignItems(root, YGAlign.FlexStart);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetOverflow(root, YGOverflow.Scroll);
            YGNodeStyleSetHeight(root, 100);
            YGNodeStyleSetWidth(root, 100);

            YGNode root_child0 = YGNodeNew();
            root_child0.Context = constraintList;
            root_child0.MeasureFunc = _measure;
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(1, constraintList.Count);

            Assert.IsTrue(constraintList[0].width.IsNaN());
            Assert.AreEqual(YGMeasureMode.Undefined, constraintList[0].widthMode);

            Assert.AreEqual(100,                  constraintList[0].height);
            Assert.AreEqual(YGMeasureMode.AtMost, constraintList[0].heightMode);

            //free(constraintList.constraints);
            YGNodeFreeRecursive(root);
        }
    }
}
