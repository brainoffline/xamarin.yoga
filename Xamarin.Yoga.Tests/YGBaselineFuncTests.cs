using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGBaselineFuncTests
    {
        private static float _baseline(YGNode node, float width, float height)
        {
            float baseline = (float) node.Context;
            return baseline;
        }

        [TestMethod]
        public void align_baseline_customer_func()
        {
            YGNode root = new YGNode();
            root.Style.FlexDirection = YGFlexDirection.Row;
            root.Style.AlignItems = YGAlign.Baseline;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode();
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.InsertChild(1, root_child1);

            float  baselineValue      = 10;
            YGNode root_child1_child0 = new YGNode();
            root_child1_child0.Context = baselineValue;
            root_child1_child0.Style.Width = 50;
            root_child1_child0.setBaseLineFunc(_baseline);
            root_child1_child0.Style.Height = 20;
            root_child1.InsertChild(root_child1_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);
        }
    }
}
