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
            root.StyleSetFlexDirection( YGFlexDirection.Row);
            root.StyleSetWidth(200);
            root.StyleSetHeight(200);

            YGNode child = new YGNode();
            root.InsertChild(child);
            child.StyleSetDimensions(100, 100);
            child.StyleSetMargin(YGEdge.Top, 10);
            child.StyleSetPadding(YGEdge.Top, 10);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);

            Assert.AreEqual(10, YGNodeLayoutGetMargin(child, YGEdge.Top));
            Assert.AreEqual(10, YGNodeLayoutGetPadding(child, YGEdge.Top));

            child.StyleSetDisplay(YGDisplay.None);

            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetMargin(child, YGEdge.Top));
            Assert.AreEqual(0, YGNodeLayoutGetPadding(child, YGEdge.Top));

            
        }

    }
}
