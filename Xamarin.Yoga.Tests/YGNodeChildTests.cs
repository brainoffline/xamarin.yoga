using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YogaConst;
    
    
    

    [TestClass]
    public class YGNodeChildTests
    {
        [TestMethod]
        public void reset_layout_when_child_removed()
        {
             YGNode root = new YGNode();

             YGNode root_child0 = new YGNode();
            root_child0.Style.Width = 100;
            root_child0.Style.Height = 100;
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
