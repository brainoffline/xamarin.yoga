using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class BorderTests
    {
        [TestMethod]
        public void border_center_child()
        {
            var config = new YogaConfig();

            YogaNode root_child0;
            var root = new YogaNode(config)
            {
                
                
                    JustifyContent = JustifyType.Center,
                    AlignItems     = AlignType.Center,
                    Border         = {Start = 10, End = 20, Bottom = 20},
                    Width          = 100,
                    Height         = 100
                ,
                Children =
                {
                    (root_child0 = new YogaNode(config) {Width = 10, Height = 10})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(40, root_child0.Layout.Position.Left);
            Assert.AreEqual(35, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(35, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void border_container_match_child()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Border = new Edges(10, 10, 10, 10);

            var root_child0 = new YogaNode(config);
            root_child0.Width  = 10;
            root_child0.Height = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(30, root.Layout.Width);
            Assert.AreEqual(30, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(30, root.Layout.Width);
            Assert.AreEqual(30, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void border_flex_child()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Border = new Edges(10, 10, 10, 10);
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.FlexGrow = 1;
            root_child0.Width    = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);
        }

        [TestMethod]
        public void border_no_size()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Border = new Edges(10, 10, 10, 10);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(20, root.Layout.Width);
            Assert.AreEqual(20, root.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(20, root.Layout.Width);
            Assert.AreEqual(20, root.Layout.Height);
        }

        [TestMethod]
        public void border_stretch_child()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Border = new Edges(10, 10, 10, 10);
            root.Width  = 100;
            root.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Height = 10;
            root.Children.Add(root_child0);
            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }
    }
}
