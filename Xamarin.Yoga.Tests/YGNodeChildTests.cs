using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGNodeChildTests
    {
        [TestMethod]
        public void reset_layout_when_child_removed()
        {
             YGNode root = new YGNode();

             YGNode root_child0 = new YGNode();
            root_child0.StyleSetWidth(100);
            root_child0.StyleSetHeight(100);
            root.InsertChild(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            root.RemoveChild(root_child0);

            Assert.AreEqual(0, root_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0.Layout.Position.Top);
            Assert.IsTrue(root_child0.Layout.Width.IsNaN());
            Assert.IsTrue(root_child0.Layout.Height.IsNaN());

            
        }

    }
}
