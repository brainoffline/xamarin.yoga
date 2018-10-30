using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class InfiniteHeightTests
    {
        // This test isn't correct from the Flexbox standard standpoint,
        // because percentages are calculated with parent constraints.
        // However, we need to make sure we fail gracefully in this case, not returning NaN
        [TestMethod]
        public void percent_absolute_position_infinite_height()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width         = 300;

            var root_child0 = new YogaNode(config);
            root_child0.Style.Width  = 300;
            root_child0.Style.Height = 300;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.PositionType  = PositionType.Absolute;
            root_child1.Style.Position.Left = 20.Percent();
            root_child1.Style.Position.Top  = 20.Percent();
            root_child1.Style.Width         = 20.Percent();
            root_child1.Style.Height        = 20.Percent();
            root.Children.Insert(1, root_child1);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(300, root.Layout.Width);
            Assert.AreEqual(300, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(300, root_child0.Layout.Width);
            Assert.AreEqual(300, root_child0.Layout.Height);

            Assert.AreEqual(60, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(60, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);
        }
    }
}
