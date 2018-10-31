using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class EdgeTests
    {
        [TestMethod]
        public void all_overridden()
        {
            YogaNode root_child0;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Column, Width = 100, Height = 100,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        
                        
                            FlexGrow = 1,
                            Margin =
                            {
                                Left   = 10,
                                Top    = 10,
                                Right  = 10,
                                Bottom = 10,
                                All    = 20
                            }
                        
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);
            Assert.AreEqual(10, root_child0.Layout.Position.Bottom);
        }

        [TestMethod]
        public void end_overrides()
        {
            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.FlexGrow     = 1;
            root_child0.Margin.End   = 10;
            root_child0.Margin.Left  = 20;
            root_child0.Margin.Right = 20;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Right);
        }

        [TestMethod]
        public void horizontal_overridden()
        {
            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.FlexGrow          = 1;
            root_child0.Margin.Horizontal = 10;
            root_child0.Margin.Left       = 20;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);
        }

        [TestMethod]
        public void horizontal_overrides_all()
        {
            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Column;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.FlexGrow          = 1;
            root_child0.Margin.Horizontal = 10;
            root_child0.Margin.All        = 20;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);
            Assert.AreEqual(20, root_child0.Layout.Position.Bottom);
        }

        [TestMethod]
        public void start_overrides()
        {
            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Row;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.FlexGrow     = 1;
            root_child0.Margin.Start = 10;
            root_child0.Margin.Left  = 20;
            root_child0.Margin.Right = 20;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(20, root_child0.Layout.Position.Right);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Right);
        }

        [TestMethod]
        public void vertical_overridden()
        {
            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Column;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.FlexGrow        = 1;
            root_child0.Margin.Vertical = 10;
            root_child0.Margin.Top      = 20;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Position.Bottom);
        }

        [TestMethod]
        public void vertical_overrides_all()
        {
            var root = new YogaNode();
            root.FlexDirection = FlexDirectionType.Column;
            root.Width         = 100;
            root.Height        = 100;

            var root_child0 = new YogaNode();
            root_child0.FlexGrow        = 1;
            root_child0.Margin.Vertical = 10;
            root_child0.Margin.All      = 20;
            root.Children.Add(root_child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(20, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Position.Right);
            Assert.AreEqual(10, root_child0.Layout.Position.Bottom);
        }
    }
}
