using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class DirtyMarkingTests
    {
        [TestMethod]
        public void dirty_mark_all_children_as_dirty_when_display_changes()
        {
            YogaNode child0;
            YogaNode child1;
            YogaNode child1_child0;
            YogaNode child1_child0_child0;
            var root = new YogaNode
            {
                Name = "Root",
                
                
                    FlexDirection = FlexDirectionType.Row,
                    Height        = 100
                ,
                Children =
                {
                    (child1 = new YogaNode
                    {
                        Name  = "Child1",
                        FlexGrow = 1,
                        Display = DisplayType.None,
                        Children =
                        {
                            (child1_child0 = new YogaNode
                            {
                                Name = "child1_child0",
                                Children =
                                {
                                    (child1_child0_child0 = new YogaNode {Name = "child1_child0_child0", Width = 8, Height = 16})
                                }
                            })
                        }
                    }),
                    (child0 = new YogaNode {FlexGrow = 1, Display = DisplayType.Flex})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0, child1_child0_child0.Layout.Width);
            Assert.AreEqual(0, child1_child0_child0.Layout.Height);

            child0.Display = DisplayType.None;
            child1.Display = DisplayType.Flex;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(8,  child1_child0_child0.Layout.Width);
            Assert.AreEqual(16, child1_child0_child0.Layout.Height);

            child0.Display = DisplayType.Flex;
            child1.Display = DisplayType.None;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0, child1_child0_child0.Layout.Width);
            Assert.AreEqual(0, child1_child0_child0.Layout.Height);

            child0.Display = DisplayType.None;
            child1.Display = DisplayType.Flex;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(8,  child1_child0_child0.Layout.Width);
            Assert.AreEqual(16, child1_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void dirty_node_only_if_children_are_actually_removed()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 50;
            root.Height     = 50;

            var child0 = new YogaNode();
            child0.Width  = 50;
            child0.Height = 25;
            root.Children.Add(child0);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            var child1 = new YogaNode();
            root.Children.Remove(child1);
            Assert.IsFalse(root.IsDirty);

            root.Children.Remove(child0);
            Assert.IsTrue(root.IsDirty);
        }

        [TestMethod]
        public void dirty_node_only_if_undefined_values_gets_set_to_undefined()
        {
            var root = new YogaNode();
            root.Width    = 50;
            root.Height   = 50;
            root.MinWidth = float.NaN;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.IsFalse(root.IsDirty);

            root.MinWidth = float.NaN;

            Assert.IsFalse(root.IsDirty);
        }

        [TestMethod]
        public void dirty_propagation()
        {
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Width  = 50;
            root_child0.Height = 20;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode();
            root_child1.Width  = 50;
            root_child1.Height = 20;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            root_child0.Width = 20;

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
            var root = new YogaNode();
            root.AlignItems = AlignType.FlexStart;
            root.Width      = 100;
            root.Height     = 100;

            var root_child0 = new YogaNode();
            root_child0.Width  = 50;
            root_child0.Height = 20;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode();
            root_child1.Width  = 50;
            root_child1.Height = 20;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            root_child0.Width = 50;

            Assert.IsFalse(root_child0.IsDirty);
            Assert.IsFalse(root_child1.IsDirty);
            Assert.IsFalse(root.IsDirty);
        }
    }
}
