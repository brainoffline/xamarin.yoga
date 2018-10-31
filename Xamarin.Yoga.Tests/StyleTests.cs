using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class StyleTests
    {
        [TestMethod]
        public void copy_style_modified()
        {
            var node0 = new YogaNode();
            Assert.IsFalse(node0.IsDirty);
            Assert.AreEqual(FlexDirectionType.Column, node0.FlexDirection);
            Assert.IsFalse(node0.MaxHeight.Unit != ValueUnit.Undefined);

            var node1 = new YogaNode();
            node1.FlexDirection = FlexDirectionType.Row;
            node1.MaxHeight     = 10;

            node0.SetStyle(node1);
            Assert.IsTrue(node0.IsDirty);
            Assert.AreEqual(FlexDirectionType.Row, node0.FlexDirection);
            Assert.AreEqual(10,                    node0.MaxHeight.Number);
        }

        [TestMethod]
        public void copy_style_modified_same()
        {
            var node0 = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row, MaxHeight = 10
            };

            node0.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
            Assert.IsFalse(node0.IsDirty);

            var node1 = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row, MaxHeight = 10
            };

            node0.SetStyle(node1);

            Assert.IsFalse(node0.IsDirty);
        }

        [TestMethod]
        public void copy_style_same()
        {
            var node0 = new YogaNode();
            var node1 = new YogaNode();
            Assert.IsFalse(node0.IsDirty);

            node0.SetStyle(node1);
            Assert.IsFalse(node0.IsDirty);
        }

        [TestMethod]
        public void initialise_flexShrink_flexGrow()
        {
            var node0 = new YogaNode();
            node0.FlexShrink = 1;
            Assert.AreEqual(1, node0.FlexShrink);

            node0.FlexShrink = float.NaN;
            node0.FlexGrow   = 3;
            Assert.AreEqual(0, node0.FlexShrink);
            Assert.AreEqual(3, node0.FlexGrow);

            node0.FlexGrow   = float.NaN;
            node0.FlexShrink = 3;
            Assert.AreEqual(null, node0.FlexGrow);
            Assert.AreEqual(3,    node0.FlexShrink);
        }
    }
}
