using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class PersistenceTests
    {
        [TestMethod]
        public void cloning_and_freeing()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 100;
            root.Style.Height = 100;
            var root_child0 = new YogaNode(config);
            root.Children.Add(root_child0);
            var root_child1 = new YogaNode(config);
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            var root2 = new YogaNode(root);

            // Freeing the original root should be safe as long as we don't free its
            // children.

            root2.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);
        }

        [TestMethod]
        public void cloning_shared_root()
        {
            YogaNode root_child0, root_child1;
            var root = new YogaNode
            {
                Style = {Width = 100, Height = 100},
                Children =
                {
                    (root_child0 = new YogaNode {Style = {FlexGrow = 1, FlexBasis = 50}}),
                    (root_child1 = new YogaNode {Style = {FlexGrow = 1}})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(75,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            var root2 = new YogaNode(root) {Style = {Width = 100}};

            Assert.AreEqual(2, root2.Children.Count);

            // The children should have referential equality at this point.
            Assert.AreEqual(root_child0, root2.Children[0]);
            Assert.AreEqual(root_child1, root2.Children[1]);

            root2.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(2, root2.Children.Count);

            // Re-layout with no changed input should result in referential equality.
            Assert.AreEqual(root_child0, root2.Children[0]);
            Assert.AreEqual(root_child1, root2.Children[1]);

            root2.Style.Width  = 150;
            root2.Style.Height = 200;

            root2.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(2, root2.Children.Count);

            // Re-layout with changed input should result in cloned children.
            var root2_child0 = root2.Children[0];
            var root2_child1 = root2.Children[1];
            Assert.AreNotEqual(root_child0, root2_child0);
            Assert.AreNotEqual(root_child1, root2_child1);

            // Everything in the root should remain unchanged.
            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(75,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(75,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(25,  root_child1.Layout.Height);

            // The new root now has new layout.
            Assert.AreEqual(0,   root2.Layout.Position.Left);
            Assert.AreEqual(0,   root2.Layout.Position.Top);
            Assert.AreEqual(150, root2.Layout.Width);
            Assert.AreEqual(200, root2.Layout.Height);

            Assert.AreEqual(0,   root2_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root2_child0.Layout.Position.Top);
            Assert.AreEqual(150, root2_child0.Layout.Width);
            Assert.AreEqual(125, root2_child0.Layout.Height);

            Assert.AreEqual(0,   root2_child1.Layout.Position.Left);
            Assert.AreEqual(125, root2_child1.Layout.Position.Top);
            Assert.AreEqual(150, root2_child1.Layout.Width);
            Assert.AreEqual(75,  root2_child1.Layout.Height);
        }

        [TestMethod]
        public void cloning_two_levels()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            root.Style.Width  = 100;
            root.Style.Height = 100;

            var root_child0 = new YogaNode(config);
            root_child0.Style.FlexGrow  = 1;
            root_child0.Style.FlexBasis = 15;
            root.Children.Add(root_child0);

            var root_child1 = new YogaNode(config);
            root_child1.Style.FlexGrow = 1;
            root.Children.Insert(1, root_child1);

            var root_child1_0 = new YogaNode(config);
            root_child1_0.Style.FlexBasis = 10;
            root_child1_0.Style.FlexGrow  = 1;
            root_child1.Children.Add(root_child1_0);

            var root_child1_1 = new YogaNode(config);
            root_child1_1.Style.FlexBasis = 25;
            root_child1.Children.Insert(1, root_child1_1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(40, root_child0.Layout.Height);
            Assert.AreEqual(60, root_child1.Layout.Height);
            Assert.AreEqual(35, root_child1_0.Layout.Height);
            Assert.AreEqual(25, root_child1_1.Layout.Height);

            var root2_child0 = new YogaNode(root_child0);
            var root2_child1 = new YogaNode(root_child1);
            var root2        = new YogaNode(root);

            root2_child0.Style.FlexGrow  = 0;
            root2_child0.Style.FlexBasis = 40;

            root2.ClearChildren();
            root2.Children.Add(root2_child0);
            root2.Children.Insert(1, root2_child1);
            Assert.AreEqual(2, root2.Children.Count);

            root2.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            // Original root is unchanged
            Assert.AreEqual(40, root_child0.Layout.Height);
            Assert.AreEqual(60, root_child1.Layout.Height);
            Assert.AreEqual(35, root_child1_0.Layout.Height);
            Assert.AreEqual(25, root_child1_1.Layout.Height);

            // New root has new layout at the top
            Assert.AreEqual(40, root2_child0.Layout.Height);
            Assert.AreEqual(60, root2_child1.Layout.Height);

            // The deeper children are untouched.
            Assert.AreEqual(root2_child1.Children[0], root_child1_0);
            Assert.AreEqual(root2_child1.Children[1], root_child1_1);
        }

        [Ignore("Clone works differently in .Net implementation")]
        [TestMethod]
        public void mutating_children_of_a_clone_clones()
        {
            var config = new YogaConfig();

            var root = new YogaNode(config);
            Assert.AreEqual(0, root.Children.Count);

            var root2 = new YogaNode(root);
            Assert.AreEqual(0, root2.Children.Count);

            var root2_child0 = new YogaNode(config);
            root2.Children.Add(root2_child0);

            Assert.AreEqual(0, root.Children.Count);
            Assert.AreEqual(1, root2.Children.Count);

            var root3 = new YogaNode(root2);
            Assert.AreEqual(1,                 root2.Children.Count);
            Assert.AreEqual(1,                 root3.Children.Count);
            Assert.AreEqual(root2.Children[0], root3.Children[0]);

            var root3_child1 = new YogaNode(config);
            root3.Children.Insert(1, root3_child1);
            Assert.AreEqual(1,            root2.Children.Count);
            Assert.AreEqual(2,            root3.Children.Count);
            Assert.AreEqual(root3_child1, root3.Children[1]);
            Assert.AreNotEqual(root2.Children[0], root3.Children[0]);

            var root4 = new YogaNode(root3);
            Assert.AreEqual(root3_child1, root4.Children[1]);

            root4.Children.Remove(root3_child1);
            Assert.AreEqual(2, root3.Children.Count);
            Assert.AreEqual(1, root4.Children.Count);
            Assert.AreNotEqual(root3.Children[0], root4.Children[0]);
        }
    }
}
