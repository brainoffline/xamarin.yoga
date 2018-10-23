﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;


    [TestClass]
    public class YGDirtyMarkingTests
    {
        [TestMethod]
        public void dirty_propagation()
        {
            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode();
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            root_child0.Style.Width = 20;

            Assert.IsTrue(root_child0.IsDirty);
            Assert.IsFalse(root_child1.IsDirty);
            Assert.IsTrue(root.IsDirty);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.IsFalse(root_child0.IsDirty);
            Assert.IsFalse(root_child1.IsDirty);
            Assert.IsFalse(root.IsDirty);
        }

        [TestMethod]
        public void dirty_propagation_only_if_prop_changed()
        {
            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode();
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            root_child0.Style.Width = 50;

            Assert.IsFalse(root_child0.IsDirty);
            Assert.IsFalse(root_child1.IsDirty);
            Assert.IsFalse(root.IsDirty);
        }

        [TestMethod]
        public void dirty_mark_all_children_as_dirty_when_display_changes()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    Height = 100
                }
            };

            YGNode child0 = new YGNode {Style = {FlexGrow = 1}};
            YGNode child1 = new YGNode {Style = {FlexGrow = 1}};

            YGNode child1_child0        = new YGNode();
            YGNode child1_child0_child0 = new YGNode
            {
                Style = {Width = 8, Height = 16}
            };

            child1_child0.Children.Add(child1_child0_child0);

            child1.Children.Add(child1_child0);
            root.Children.Add(child0);
            root.Children.Add(child1);

            child0.Style.Display = DisplayType.Flex;
            child1.Style.Display = DisplayType.None;

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0, child1_child0_child0.Layout.Width);
            Assert.AreEqual(0, child1_child0_child0.Layout.Height);

            child0.Style.Display = DisplayType.None;
            child1.Style.Display = DisplayType.Flex;
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(8,  child1_child0_child0.Layout.Width);
            Assert.AreEqual(16, child1_child0_child0.Layout.Height);

            child0.Style.Display = DisplayType.Flex;
            child1.Style.Display = DisplayType.None;
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(0, child1_child0_child0.Layout.Width);
            Assert.AreEqual(0, child1_child0_child0.Layout.Height);

            child0.Style.Display = DisplayType.None;
            child1.Style.Display = DisplayType.Flex;
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);
            Assert.AreEqual(8,  child1_child0_child0.Layout.Width);
            Assert.AreEqual(16, child1_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void dirty_node_only_if_children_are_actually_removed()
        {
            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 50;
            root.Style.Height = 50;

            YGNode child0 = new YGNode();
            child0.Style.Width = 50;
            child0.Style.Height = 25;
            root.Children.Add(child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            YGNode child1 = new YGNode();
            root.Children.Remove(child1);
            Assert.IsFalse(root.IsDirty);

            root.Children.Remove(child0);
            Assert.IsTrue(root.IsDirty);
        }

        [TestMethod]
        public void dirty_node_only_if_undefined_values_gets_set_to_undefined()
        {
            YGNode root = new YGNode();
            root.Style.Width = 50;
            root.Style.Height = 50;
            root.Style.MinWidth = float.NaN;

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.IsFalse(root.IsDirty);

            root.Style.MinWidth = float.NaN;

            Assert.IsFalse(root.IsDirty);
        }
    }
}
