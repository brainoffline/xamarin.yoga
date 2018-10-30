using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class ComputedPaddingTests
    {
        [TestMethod]
        public void computed_layout_padding()
        {
            var root = new YogaNode
            {
                Style = {Width = 100, Height = 100, Padding = {Start = 10.Percent()}}
            };

            root.Calc.CalculateLayout(100, 100, DirectionType.LTR);

            Assert.AreEqual(10, root.Layout.GetPadding(EdgeType.Left));
            Assert.AreEqual(0,  root.Layout.GetPadding(EdgeType.Right));

            root.Calc.CalculateLayout(100, 100, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.GetPadding(EdgeType.Left));
            Assert.AreEqual(10, root.Layout.GetPadding(EdgeType.Right));
        }
    }
}
