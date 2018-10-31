using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class BaselineFuncTests
    {
        [TestMethod]
        public void align_baseline_customer_func()
        {
            float baselineValue = 10;
            YogaNode root_child0;
            YogaNode root_child1;
            YogaNode root_child1_child0;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row, AlignItems = AlignType.Baseline, Width = 100, Height = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 50}),
                    (root_child1 = new YogaNode
                    {
                        Width = 50, Height = 20,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode
                            {
                                Context      = baselineValue,
                                Width = 50, Height = 20,
                                BaselineFunc = _baseline
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);
        }

        private static float _baseline(YogaNode node, float width, float height)
        {
            var baseline = (float) node.Context;
            return baseline;
        }
    }
}
