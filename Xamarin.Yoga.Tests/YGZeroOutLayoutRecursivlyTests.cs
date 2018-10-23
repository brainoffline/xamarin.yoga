using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    
    
    

    [TestClass]
    public class YGZeroOutLayoutRecursivlyTests
    {
        [TestMethod]
        public void zero_out_layout()
        {
            YGNode root = new YGNode();
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 200;
            root.Style.Height = 200;

            YGNode child = new YGNode();
            root.Children.Add(child);
            child.Style.Width = 100;
            child.Style.Height = 100;
            child.Style.Margin.Top = 10;
            child.Style.Padding.Top = 10;

            YGNodeCalculateLayout(root, 100, 100, DirectionType.LTR);

            Assert.AreEqual(10, child.LayoutGetMargin(EdgeType.Top));
            Assert.AreEqual(10, YGNodeLayoutGetPadding(child, EdgeType.Top));

            child.Style.Display = DisplayType.None;

            YGNodeCalculateLayout(root, 100, 100, DirectionType.LTR);

            Assert.AreEqual(0, child.LayoutGetMargin(EdgeType.Top));
            Assert.AreEqual(0, YGNodeLayoutGetPadding(child, EdgeType.Top));

            
        }

    }
}
