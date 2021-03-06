﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class NodeChildTests
    {
        [TestMethod]
        public void reset_layout_when_child_removed()
        {
            var root = new YogaNode();

            var root_child0 = new YogaNode();
            root_child0.Width  = 100;
            root_child0.Height = 100;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.Children.Remove(root_child0);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.IsTrue(root_child0.Layout.Width.IsNaN());
            Assert.IsTrue(root_child0.Layout.Height.IsNaN());
        }
    }
}
