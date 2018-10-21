using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGAlignBaselineTests
    {
        private static float _baselineFunc(YGNode node, float width, float height)
        {
            return height / 2;
        }

        private static YGSize _measure1(
            YGNode     node,
            float         width,
            YGMeasureMode widthMode,
            float         height,
            YGMeasureMode heightMode)
        {
            return new YGSize(width = 42, height = 50);
        }

        private static YGSize _measure2(
            YGNode     node,
            float         width,
            YGMeasureMode widthMode,
            float         height,
            YGMeasureMode heightMode)
        {
            return new YGSize(width = 279, height = 126);
        }

        // Test case for bug in T32999822
        [TestMethod]
        public void align_baseline_parent_ht_not_specified()
        {
            var config = new YGConfig();

            var root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignContent = YGAlign.Stretch;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 340;
            root.Style.MaxHeight = 170;
            root.Style.MinHeight = 0;

            var root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow =  0;
            root_child0.Style.FlexShrink = 1;
            root_child0.MeasureFunc = _measure1;
            root.InsertChild(root_child0);

            var root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow =  0;
            root_child1.Style.FlexShrink =  1;
            root_child1.MeasureFunc = _measure2;
            root.InsertChild(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(340, root.Layout.Width);
            Assert.AreEqual(126, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(42, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);
            Assert.AreEqual(76, root_child0.Layout.Position.Top);

            Assert.AreEqual(42,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(279, root_child1.Layout.Width);
            Assert.AreEqual(126, root_child1.Layout.Height);

            
        }

        [TestMethod]
        public void align_baseline_with_no_parent_ht()
        {
            var config = new YGConfig();

            var root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 150;

            var root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            var root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 40;

            root_child1.setBaseLineFunc(_baselineFunc);
            root.InsertChild(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(70,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(40, root_child1.Layout.Height);

            
        }

        [TestMethod]
        public void align_baseline_with_no_baseline_func_and_no_parent_ht()
        {
            var config = new YGConfig();

            var root = new YGNode(config);
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 150;

            var root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 80;
            root.InsertChild(root_child0);

            var root_child1 = new YGNode(config);
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 50;
            root.InsertChild(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(80,  root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            

            
        }
    }
}
