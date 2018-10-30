using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class ComputedMarginTests
    {
        [TestMethod]
        public void computed_layout_margin()
        {
            var root = new YogaNode
            {
                Style =
                {
                    Width = 100,
                    Height = 100,
                    Margin = {Start = 10}
                }
            };

            root.Calc.CalculateLayout(100, 100, DirectionType.LTR);

            Assert.AreEqual(10, root.Layout.GetMargin(EdgeType.Left));
            Assert.AreEqual(0,  root.Layout.GetMargin(EdgeType.Right));

            root.Calc.CalculateLayout(100, 100, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.GetMargin(EdgeType.Left));
            Assert.AreEqual(10, root.Layout.GetMargin(EdgeType.Right));
        }
    }
}
