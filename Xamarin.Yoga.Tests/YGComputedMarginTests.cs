using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    
    
    

    [TestClass]
    public class YGComputedMarginTests
    {
        [TestMethod]
        public void computed_layout_margin()
        {
            var root = new YGNode();
            root.Style.Width = 100;
            root.Style.Height = 100;
            root.Style.Margin.Start = 10;

            YGNodeCalculateLayout(root, 100, 100, DirectionType.LTR);

            Assert.AreEqual(10, root.LayoutGetMargin(EdgeType.Left));
            Assert.AreEqual(0,  root.LayoutGetMargin(EdgeType.Right));

            YGNodeCalculateLayout(root, 100, 100, DirectionType.RTL);

            Assert.AreEqual(0,  root.LayoutGetMargin(EdgeType.Left));
            Assert.AreEqual(10, root.LayoutGetMargin(EdgeType.Right));

            
        }

    }
}
