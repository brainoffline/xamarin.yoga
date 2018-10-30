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
            Assert.AreEqual(FlexDirectionType.Column, node0.Style.FlexDirection);
            Assert.IsFalse(node0.Style.MaxHeight.Unit != ValueUnit.Undefined);

            var node1 = new YogaNode();
            node1.Style.FlexDirection = FlexDirectionType.Row;
            node1.Style.MaxHeight     = 10;

            node0.Style = node1.Style;
            Assert.IsTrue(node0.IsDirty);
            Assert.AreEqual(FlexDirectionType.Row, node0.Style.FlexDirection);
            Assert.AreEqual(10,                    node0.Style.MaxHeight.Number);
        }

        [TestMethod]
        public void copy_style_modified_same()
        {
            var node0 = new YogaNode();
            node0.Style.FlexDirection = FlexDirectionType.Row;
            node0.Style.MaxHeight     = 10;
            node0.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
            Assert.IsFalse(node0.IsDirty);

            var node1 = new YogaNode();
            node1.Style.FlexDirection = FlexDirectionType.Row;
            node1.Style.MaxHeight     = 10;

            node0.Style = node1.Style;
            Assert.IsFalse(node0.IsDirty);
        }

        [TestMethod]
        public void copy_style_same()
        {
            var node0 = new YogaNode();
            var node1 = new YogaNode();
            Assert.IsFalse(node0.IsDirty);

            node0.Style = node1.Style;
            Assert.IsFalse(node0.IsDirty);
        }

        [TestMethod]
        public void initialise_flexShrink_flexGrow()
        {
            var node0 = new YogaNode();
            node0.Style.FlexShrink = 1;
            Assert.AreEqual(1, node0.Style.FlexShrink);

            node0.Style.FlexShrink = float.NaN;
            node0.Style.FlexGrow   = 3;
            Assert.AreEqual(0, node0.Style.FlexShrink);
            Assert.AreEqual(3, node0.Style.FlexGrow);

            node0.Style.FlexGrow   = float.NaN;
            node0.Style.FlexShrink = 3;
            Assert.AreEqual(null, node0.Style.FlexGrow);
            Assert.AreEqual(3,    node0.Style.FlexShrink);
        }
    }
}
