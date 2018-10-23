using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YogaConst;
    
    
    

    [TestClass]
    public class YGComputedPaddingTests
    {
        [TestMethod] public void computed_layout_padding()
        {
             YGNode root = new YGNode();
            root.Style.Width = 100;
            root.Style.Height = 100;
            root.Style.Padding.Start = 10.Percent();

            YGNodeCalculateLayout(root, 100, 100, DirectionType.LTR);

            Assert.AreEqual(10, YGNodeLayoutGetPadding(root, EdgeType.Left));
            Assert.AreEqual(0,  YGNodeLayoutGetPadding(root, EdgeType.Right));

            YGNodeCalculateLayout(root, 100, 100, DirectionType.RTL);

            Assert.AreEqual(0,  YGNodeLayoutGetPadding(root, EdgeType.Left));
            Assert.AreEqual(10, YGNodeLayoutGetPadding(root, EdgeType.Right));

            
        }

    }
}
