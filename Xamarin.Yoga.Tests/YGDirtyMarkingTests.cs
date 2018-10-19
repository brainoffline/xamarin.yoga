using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            root.StyleSetAlignItems(YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 20);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode();
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 20);
            root.InsertChild(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            YGNodeStyleSetWidth(root_child0, 20);

            Assert.IsTrue(root_child0.IsDirty);
            Assert.IsFalse(root_child1.IsDirty);
            Assert.IsTrue(root.IsDirty);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.IsFalse(root_child0.IsDirty);
            Assert.IsFalse(root_child1.IsDirty);
            Assert.IsFalse(root.IsDirty);
        }

        [TestMethod]
        public void dirty_propagation_only_if_prop_changed()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YGNode root_child0 = new YGNode();
            YGNodeStyleSetWidth(root_child0, 50);
            YGNodeStyleSetHeight(root_child0, 20);
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode();
            YGNodeStyleSetWidth(root_child1, 50);
            YGNodeStyleSetHeight(root_child1, 20);
            root.InsertChild(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            YGNodeStyleSetWidth(root_child0, 50);

            Assert.IsFalse(root_child0.IsDirty);
            Assert.IsFalse(root_child1.IsDirty);
            Assert.IsFalse(root.IsDirty);
        }

        [TestMethod]
        public void dirty_mark_all_children_as_dirty_when_display_changes()
        {
            YGNode root = new YGNode();
            root.StyleSetFlexDirection(YGFlexDirection.Row);
            YGNodeStyleSetHeight(root, 100);

            YGNode child0 = new YGNode();
            child0.StyleSetFlexGrow(1);
            YGNode child1 = new YGNode();
            child1.StyleSetFlexGrow(1);

            YGNode child1_child0        = new YGNode();
            YGNode child1_child0_child0 = new YGNode();
            child1_child0_child0.StyleSetDimensions(8, 16);

            child1_child0.InsertChild(child1_child0_child0);

            child1.InsertChild(child1_child0);
            root.InsertChild(child0);
            root.InsertChild(child1);

            child0.StyleSetDisplay(YGDisplay.Flex);
            child1.StyleSetDisplay(YGDisplay.None);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(0, child1_child0_child0.Layout.Width);
            Assert.AreEqual(0, child1_child0_child0.Layout.Height);

            child0.StyleSetDisplay(YGDisplay.None);
            child1.StyleSetDisplay(YGDisplay.Flex);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(8,  child1_child0_child0.Layout.Width);
            Assert.AreEqual(16, child1_child0_child0.Layout.Height);

            child0.StyleSetDisplay(YGDisplay.Flex);
            child1.StyleSetDisplay(YGDisplay.None);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(0, child1_child0_child0.Layout.Width);
            Assert.AreEqual(0, child1_child0_child0.Layout.Height);

            child0.StyleSetDisplay(YGDisplay.None);
            child1.StyleSetDisplay(YGDisplay.Flex);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            Assert.AreEqual(8,  child1_child0_child0.Layout.Width);
            Assert.AreEqual(16, child1_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void dirty_node_only_if_children_are_actually_removed()
        {
            YGNode root = new YGNode();
            root.StyleSetAlignItems(YGAlign.FlexStart);
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 50);

            YGNode child0 = new YGNode();
            YGNodeStyleSetWidth(child0, 50);
            YGNodeStyleSetHeight(child0, 25);
            root.InsertChild(child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            YGNode child1 = new YGNode();
            root.RemoveChild(child1);
            Assert.IsFalse(root.IsDirty);

            root.RemoveChild(child0);
            Assert.IsTrue(root.IsDirty);
        }

        [TestMethod]
        public void dirty_node_only_if_undefined_values_gets_set_to_undefined()
        {
            YGNode root = new YGNode();
            YGNodeStyleSetWidth(root, 50);
            YGNodeStyleSetHeight(root, 50);
            YGNodeStyleSetMinWidth(root, float.NaN);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.IsFalse(root.IsDirty);

            YGNodeStyleSetMinWidth(root, float.NaN);

            Assert.IsFalse(root.IsDirty);
        }
    }
}
