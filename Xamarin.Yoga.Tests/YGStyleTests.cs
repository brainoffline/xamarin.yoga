using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGStyleTests
    {
        [TestMethod]
        public void copy_style_same()
        {
            YGNode node0 = new YGNode();
            YGNode node1 = new YGNode();
            Assert.IsFalse(node0.IsDirty);

            node0.Style = node1.Style;
            Assert.IsFalse(node0.IsDirty);
        }

        [TestMethod]
        public void copy_style_modified()
        {
            YGNode node0 = new YGNode();
            Assert.IsFalse(node0.IsDirty);
            Assert.AreEqual(YGFlexDirection.Column, node0.Style.FlexDirection);
            Assert.IsFalse(node0.Style.MaxDimensions.Height.unit != YGUnit.Undefined);

            YGNode node1 = new YGNode();
            node1.StyleSetFlexDirection(YGFlexDirection.Row);
            YGNodeStyleSetMaxHeight(node1, 10);

            node0.Style = node1.Style;
            Assert.IsTrue(node0.IsDirty);
            Assert.AreEqual(YGFlexDirection.Row, node0.Style.FlexDirection);
            Assert.AreEqual(10, node0.Style.MaxDimensions.Height.value);
        }

        [TestMethod]
        public void copy_style_modified_same()
        {
            YGNode node0 = new YGNode();
            node0.StyleSetFlexDirection(YGFlexDirection.Row);
            YGNodeStyleSetMaxHeight(node0, 10);
            YGNodeCalculateLayout(node0, float.NaN, float.NaN, YGDirection.LTR);
            Assert.IsFalse(node0.IsDirty);

            YGNode node1 = new YGNode();
            node1.StyleSetFlexDirection(YGFlexDirection.Row);
            YGNodeStyleSetMaxHeight(node1, 10);

            node0.Style = node1.Style;
            Assert.IsFalse(node0.IsDirty);
        }

        [TestMethod]
        public void initialise_flexShrink_flexGrow()
        {
            YGNode node0 = new YGNode();
            node0.StyleSetFlexShrink(1);
            Assert.AreEqual(1, YGNodeStyleGetFlexShrink(node0));

            node0.StyleSetFlexShrink(float.NaN);
            node0.StyleSetFlexGrow(3);
            Assert.AreEqual(null,node0.Style.FlexShrink);
            Assert.AreEqual(3, node0.Style.FlexGrow);

            node0.StyleSetFlexGrow(float.NaN);
            node0.StyleSetFlexShrink(3);
            Assert.AreEqual(null, node0.Style.FlexGrow); 
            Assert.AreEqual(3, YGNodeStyleGetFlexShrink(node0));
        }

    }
}
