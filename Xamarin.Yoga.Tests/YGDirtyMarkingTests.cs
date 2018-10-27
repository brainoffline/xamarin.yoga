using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;


    [TestClass]
    public class YGDirtyMarkingTests
    {
        [TestMethod]
        public void dirty_mark_all_children_as_dirty_when_display_changes()
        {
            YGNode child0;
            YGNode child1;
            YGNode child1_child0;
            YGNode child1_child0_child0;
            var root = new YGNode
            {
                Name = "Root",
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    Height        = 100
                },
                Children =
                {
                    (child1 = new YGNode
                    {
                        Name  = "Child1",
                        Style = {FlexGrow = 1, Display = DisplayType.None},
                        Children =
                        {
                            (child1_child0 = new YGNode
                            {
                                Name = "child1_child0",
                                Children =
                                {
                                    (child1_child0_child0 = new YGNode {Name = "child1_child0_child0", Style = {Width = 8, Height = 16}})
                                }
                            })
                        }
                    }),
                    (child0 = new YGNode {Style = {FlexGrow = 1, Display = DisplayType.Flex}})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0, child1_child0_child0.Layout.Width);
            Assert.AreEqual(0, child1_child0_child0.Layout.Height);

            child0.Style.Display = DisplayType.None;
            child1.Style.Display = DisplayType.Flex;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(8,  child1_child0_child0.Layout.Width);
            Assert.AreEqual(16, child1_child0_child0.Layout.Height);

            child0.Style.Display = DisplayType.Flex;
            child1.Style.Display = DisplayType.None;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0, child1_child0_child0.Layout.Width);
            Assert.AreEqual(0, child1_child0_child0.Layout.Height);

            child0.Style.Display = DisplayType.None;
            child1.Style.Display = DisplayType.Flex;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(8,  child1_child0_child0.Layout.Width);
            Assert.AreEqual(16, child1_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void dirty_node_only_if_children_are_actually_removed()
        {
            var root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width      = 50;
            root.Style.Height     = 50;

            var child0 = new YGNode();
            child0.Style.Width  = 50;
            child0.Style.Height = 25;
            root.Children.Add(child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            var child1 = new YGNode();
            root.Children.Remove(child1);
            Assert.IsFalse(root.IsDirty);

            root.Children.Remove(child0);
            Assert.IsTrue(root.IsDirty);
        }

        [TestMethod]
        public void dirty_node_only_if_undefined_values_gets_set_to_undefined()
        {
            var root = new YGNode();
            root.Style.Width    = 50;
            root.Style.Height   = 50;
            root.Style.MinWidth = float.NaN;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.IsFalse(root.IsDirty);

            root.Style.MinWidth = float.NaN;

            Assert.IsFalse(root.IsDirty);
        }

        [TestMethod]
        public void dirty_propagation()
        {
            var root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width      = 100;
            root.Style.Height     = 100;

            var root_child0 = new YGNode();
            root_child0.Style.Width  = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            var root_child1 = new YGNode();
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            root_child0.Style.Width = 20;

            Assert.IsTrue(root_child0.IsDirty);
            Assert.IsFalse(root_child1.IsDirty);
            Assert.IsTrue(root.IsDirty);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.IsFalse(root_child0.IsDirty);
            Assert.IsFalse(root_child1.IsDirty);
            Assert.IsFalse(root.IsDirty);
        }

        [TestMethod]
        public void dirty_propagation_only_if_prop_changed()
        {
            var root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width      = 100;
            root.Style.Height     = 100;

            var root_child0 = new YGNode();
            root_child0.Style.Width  = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            var root_child1 = new YGNode();
            root_child1.Style.Width  = 50;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            root_child0.Style.Width = 50;

            Assert.IsFalse(root_child0.IsDirty);
            Assert.IsFalse(root_child1.IsDirty);
            Assert.IsFalse(root.IsDirty);
        }
    }
}
