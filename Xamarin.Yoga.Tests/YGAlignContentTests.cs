using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;


    [TestClass]
    public class YGAlignContentTests
    {
        [TestMethod]
        public void align_content_flex_start()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 130, Height = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(4, root_child4);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(80, root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(80, root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_flex_start_without_height_on_children()
        {
            var root = new YGNode {Style = {FlexWrap = WrapType.Wrap, Width = 100, Height = 100}};

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(0,  root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(10, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(10, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(20, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(0,  root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_flex_start_with_flex()
        {
            var root = new YGNode {Style = {FlexWrap = WrapType.Wrap, Width = 100, Height = 120}};

            var root_child0 = new YGNode {Style = {FlexGrow = 1, FlexBasis = 0.Percent(), Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode
            {
                Style = {FlexGrow = 1, FlexBasis = 0.Percent(), Width = 50, Height = 10}
            };
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode
            {
                Style = {FlexGrow = 1, FlexShrink = 1, FlexBasis = 0.Percent(), Width = 50}
            };
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(40, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(80, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(80, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(120, root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(0,   root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(40, root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(80, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(80, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(40, root_child3.Layout.Height);

            Assert.AreEqual(50,  root_child4.Layout.Position.Left);
            Assert.AreEqual(120, root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(0,   root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_flex_end()
        {
            var root = new YGNode
            {
                Style = {AlignContent = YGAlign.FlexEnd, FlexWrap = WrapType.Wrap, Width = 100, Height = 100}
            };

            var root_child0 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(40, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(40, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch()
        {
            var root = new YGNode
            {
                Style = {AlignContent = YGAlign.Stretch, FlexWrap = WrapType.Wrap, Width = 150, Height = 100}
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(0,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(0,  root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(0,  root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(0,   root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(0,   root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(0,   root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(0,   root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_spacebetween()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.SpaceBetween,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 130,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(45, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(45, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(130, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(30, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(80, root_child2.Layout.Position.Left);
            Assert.AreEqual(45, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(45, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(80, root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_spacearound()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.SpaceAround,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 140,
                    Height        = 120
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50, Height = 10}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(140, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(15, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(55, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(55, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(95, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(140, root.Layout.Width);
            Assert.AreEqual(120, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(15, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(15, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(90, root_child2.Layout.Position.Left);
            Assert.AreEqual(55, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(10, root_child2.Layout.Height);

            Assert.AreEqual(40, root_child3.Layout.Position.Left);
            Assert.AreEqual(55, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(90, root_child4.Layout.Position.Left);
            Assert.AreEqual(95, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 150,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_children()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 150,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child0_child0 = new YGNode
            {
                Style = {FlexGrow = 1, FlexShrink = 1, FlexBasis = 0.Percent()}
            };
            root_child0.Children.Add(root_child0_child0);

            var root_child1 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_flex()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 150,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode
            {
                Style = {FlexGrow = 1, FlexShrink = 1, FlexBasis = 0.Percent(), Width = 50}
            };
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode
            {
                Style = {FlexGrow = 1, FlexShrink = 1, FlexBasis = 0.Percent(), Width = 50}
            };
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(50,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_flex_no_shrink()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 150,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode
            {
                Style = {FlexGrow = 1, FlexShrink = 1, FlexBasis = 0.Percent(), Width = 50}
            };
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {FlexGrow = 1, FlexBasis = 0.Percent(), Width = 50}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(100, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(0,   root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(100, root_child2.Layout.Height);

            Assert.AreEqual(50,  root_child3.Layout.Position.Left);
            Assert.AreEqual(0,   root_child3.Layout.Position.Top);
            Assert.AreEqual(0,   root_child3.Layout.Width);
            Assert.AreEqual(100, root_child3.Layout.Height);

            Assert.AreEqual(0,   root_child4.Layout.Position.Left);
            Assert.AreEqual(0,   root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(100, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_margin()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 150,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Margin = new Edges(10, 10, 10, 10), Width = 50}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Margin = new Edges(10, 10, 10, 10), Width = 50}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            Assert.AreEqual(60, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(40, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(40, root_child2.Layout.Height);

            Assert.AreEqual(60, root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(80, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(20, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(40,  root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(10, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(40,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(40,  root_child2.Layout.Height);

            Assert.AreEqual(40, root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            Assert.AreEqual(100, root_child4.Layout.Position.Left);
            Assert.AreEqual(80,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50,  root_child4.Layout.Width);
            Assert.AreEqual(20,  root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_padding()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 150,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Padding = new Edges(10, 10, 10, 10), Width = 50}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Padding = new Edges(10, 10, 10, 10), Width = 50}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_single_row()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 150,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(50,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_fixed_height()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 150,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50, Height = 60}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(60, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(80,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(80, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(80, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(20, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(80,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(60, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(80, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(80,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(20,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(80, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(20, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_max_height()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 150,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50, MaxHeight = 20}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(50,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(50, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(50,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(50, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_row_with_min_height()
        {
            var root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 150,
                    Height        = 100
                }
            };

            var root_child0 = new YGNode {Style = {Width = 50}};
            root.Children.Add(root_child0);

            var root_child1 = new YGNode {Style = {Width = 50, MinHeight = 80}};
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Width = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(90, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(90, root_child1.Layout.Height);

            Assert.AreEqual(100, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,   root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(90,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(90, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(10, root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(150, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(100, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(90,  root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(90, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(90, root_child2.Layout.Height);

            Assert.AreEqual(100, root_child3.Layout.Position.Left);
            Assert.AreEqual(90,  root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(10,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(90, root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(10, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_column()
        {
            var root = new YGNode
            {
                Style =
                {
                    AlignContent = YGAlign.Stretch,
                    FlexWrap = WrapType.Wrap,
                    Width = 100,
                    Height = 150
                }
            };

            var root_child0 = new YGNode {Style = {Height = 50}};
            root.Children.Add(root_child0);

            var root_child0_child0 = new YGNode
            {
                Style = {FlexGrow = 1, FlexShrink = 1, FlexBasis = 0.Percent()}
            };
            root_child0.Children.Add(root_child0_child0);

            var root_child1 = new YGNode
            {
                Style = {FlexGrow = 1, FlexShrink = 1, FlexBasis = 0.Percent(), Height = 50}
            };
            root.Children.Insert(1, root_child1);

            var root_child2 = new YGNode {Style = {Height = 50}};
            root.Children.Insert(2, root_child2);

            var root_child3 = new YGNode {Style = {Height = 50}};
            root.Children.Insert(3, root_child3);

            var root_child4 = new YGNode {Style = {Height = 50}};
            root.Children.Insert(4, root_child4);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(150, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(0,   root_child3.Layout.Position.Left);
            Assert.AreEqual(100, root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(50, root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(150, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(50, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(50,  root_child3.Layout.Position.Left);
            Assert.AreEqual(100, root_child3.Layout.Position.Top);
            Assert.AreEqual(50,  root_child3.Layout.Width);
            Assert.AreEqual(50,  root_child3.Layout.Height);

            Assert.AreEqual(0,  root_child4.Layout.Position.Left);
            Assert.AreEqual(0,  root_child4.Layout.Position.Top);
            Assert.AreEqual(50, root_child4.Layout.Width);
            Assert.AreEqual(50, root_child4.Layout.Height);
        }

        [TestMethod]
        public void align_content_stretch_is_not_overriding_align_items()
        {
            var root = new YGNode {Style = {AlignContent = YGAlign.Stretch}};

            var root_child0 = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignContent  = YGAlign.Stretch,
                    AlignItems    = YGAlign.Center,
                    Width         = 100,
                    Height        = 100
                }
            };
            root.Children.Add(root_child0);

            var root_child0_child0 = new YGNode
            {
                Style = {AlignContent = YGAlign.Stretch, Width = 10, Height = 10}
            };
            root_child0.Children.Add(root_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(45, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0_child0.Layout.Width);
            Assert.AreEqual(10, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(90, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(45, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0_child0.Layout.Width);
            Assert.AreEqual(10, root_child0_child0.Layout.Height);
        }
    }
}
