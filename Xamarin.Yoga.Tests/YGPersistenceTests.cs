using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;

    [TestClass]
    public class YGPersistenceTests
    {
        [TestMethod]
        public void cloning_shared_root()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root.InsertChild(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            YGNode root2 = new YGNode(root);
            root2.Style.Width = 100;

            Assert.AreEqual(2, root2.Children.Count);
            // The children should have referential equality at this point.
            Assert.AreEqual(root_child0, root2.Children[0]);
            Assert.AreEqual(root_child1, root2.Children[1]);

            YGNodeCalculateLayout(root2, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(2, root2.Children.Count);
            // Relayout with no changed input should result in referential equality.
            Assert.AreEqual(root_child0, root2.Children[0]);
            Assert.AreEqual(root_child1, root2.Children[1]);

            root2.Style.Width = 150;
            root2.Style.Height = 200;
            YGNodeCalculateLayout(root2, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(2, root2.Children.Count);
            // Relayout with changed input should result in cloned children.
            YGNode root2_child0 = root2.Children[0];
            YGNode root2_child1 = root2.Children[1];
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

        [Ignore("Clone works differently in .Net implementation")]
        [TestMethod]
        public void mutating_children_of_a_clone_clones()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            Assert.AreEqual(0, root.Children.Count);

            YGNode root2 = new YGNode(root);
            Assert.AreEqual(0, root2.Children.Count);

            YGNode root2_child0 = new YGNode(config);
            root2.InsertChild(root2_child0);

            Assert.AreEqual(0, root.Children.Count);
            Assert.AreEqual(1, root2.Children.Count);

            YGNode root3 = new YGNode(root2);
            Assert.AreEqual(1,                 root2.Children.Count);
            Assert.AreEqual(1,                 root3.Children.Count);
            Assert.AreEqual(root2.Children[0], root3.Children[0]);

            YGNode root3_child1 = new YGNode(config);
            root3.InsertChild(1, root3_child1);
            Assert.AreEqual(1,            root2.Children.Count);
            Assert.AreEqual(2,            root3.Children.Count);
            Assert.AreEqual(root3_child1, root3.Children[1]);
            Assert.AreNotEqual(root2.Children[0], root3.Children[0]);

            YGNode root4 = new YGNode(root3);
            Assert.AreEqual(root3_child1, root4.Children[1]);

            root4.RemoveChild(root3_child1);
            Assert.AreEqual(2, root3.Children.Count);
            Assert.AreEqual(1, root4.Children.Count);
            Assert.AreNotEqual(root3.Children[0], root4.Children[0]);
        }

        [TestMethod]
        public void cloning_two_levels()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 15;
            root.InsertChild(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root.InsertChild(1, root_child1);

            YGNode root_child1_0 = new YGNode(config);
            root_child1_0.Style.FlexBasis = 10;
            root_child1_0.Style.FlexGrow = 1;
            root_child1.InsertChild(root_child1_0);

            YGNode root_child1_1 = new YGNode(config);
            root_child1_1.Style.FlexBasis = 25;
            root_child1.InsertChild(1, root_child1_1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(40, root_child0.Layout.Height);
            Assert.AreEqual(60, root_child1.Layout.Height);
            Assert.AreEqual(35, root_child1_0.Layout.Height);
            Assert.AreEqual(25, root_child1_1.Layout.Height);

            YGNode root2_child0 = new YGNode(root_child0);
            YGNode root2_child1 = new YGNode(root_child1);
            YGNode root2        = new YGNode(root);

            root2_child0.Style.FlexGrow = 0;
            root2_child0.Style.FlexBasis = 40;

            root2.ClearChildren();
            root2.InsertChild(root2_child0);
            root2.InsertChild(1, root2_child1);
            Assert.AreEqual(2, root2.Children.Count);

            YGNodeCalculateLayout(root2, float.NaN, float.NaN, YGDirection.LTR);

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

        [TestMethod]
        public void cloning_and_freeing()
        {
            YGConfig config = new YGConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 100;
            YGNode root_child0 = new YGNode(config);
            root.InsertChild(root_child0);
            YGNode root_child1 = new YGNode(config);
            root.InsertChild(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            YGNode root2 = new YGNode(root);

            // Freeing the original root should be safe as long as we don't free its
            // children.

            YGNodeCalculateLayout(root2, float.NaN, float.NaN, YGDirection.LTR);
        }
    }
}
