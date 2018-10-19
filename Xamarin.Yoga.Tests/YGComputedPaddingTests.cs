using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGComputedPaddingTests
    {
        [TestMethod] public void computed_layout_padding()
        {
             YGNode root = new YGNode();
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);
            YGNodeStyleSetPaddingPercent(root, YGEdge.Start, 10);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);

            Assert.AreEqual(10, YGNodeLayoutGetPadding(root, YGEdge.Left));
            Assert.AreEqual(0,  YGNodeLayoutGetPadding(root, YGEdge.Right));

            YGNodeCalculateLayout(root, 100, 100, YGDirection.RTL);

            Assert.AreEqual(0,  YGNodeLayoutGetPadding(root, YGEdge.Left));
            Assert.AreEqual(10, YGNodeLayoutGetPadding(root, YGEdge.Right));

            
        }

    }
}
