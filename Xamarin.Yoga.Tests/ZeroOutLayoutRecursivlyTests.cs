﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class ZeroOutLayoutRecursivlyTests
    {
        [TestMethod]
        public void zero_out_layout()
        {
            YogaNode child;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row, Width = 200, Height = 200,
                Children =
                {
                    (child = new YogaNode
                    {
                        Width = 100, Height = 100, Margin = {Top = 10}, Padding = {Top = 10}
                    })
                }
            };

            root.Calc.CalculateLayout(100, 100, DirectionType.LTR);

            Assert.AreEqual(10, child.Layout.GetMargin(EdgeType.Top));
            Assert.AreEqual(10, child.Layout.GetPadding(EdgeType.Top));

            child.Display = DisplayType.None;

            root.Calc.CalculateLayout(100, 100, DirectionType.LTR);

            Assert.AreEqual(0, child.Layout.GetMargin(EdgeType.Top));
            Assert.AreEqual(0, child.Layout.GetPadding(EdgeType.Top));
        }
    }
}
