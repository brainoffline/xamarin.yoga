using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YogaConst;


    [TestClass]
    public class YGFlexTests
    {
        [TestMethod]
        public void flex_basis_flex_grow_column()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

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

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

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
        }

        [TestMethod]
        public void flex_shrink_flex_grow_row()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 500;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 0;
            root_child0.Style.FlexShrink = 1;
            root_child0.Style.Width = 500;
            root_child0.Style.Height = 100;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 0;
            root_child1.Style.FlexShrink = 1;
            root_child1.Style.Width = 500;
            root_child1.Style.Height = 100;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(250, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(250, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(250, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_shrink_flex_grow_child_flex_shrink_other_child()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 500;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 0;
            root_child0.Style.FlexShrink = 1;
            root_child0.Style.Width = 500;
            root_child0.Style.Height = 100;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.FlexShrink = 1;
            root_child1.Style.Width = 500;
            root_child1.Style.Height = 100;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(250, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(250, root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(250, root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_basis_flex_grow_row()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(75,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(75,  root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(25,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(75,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(25,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_basis_flex_shrink_column()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexShrink = 1;
            root_child0.Style.FlexBasis = 100;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexBasis = 50;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(50,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(50,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(50,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(50,  root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_basis_flex_shrink_row()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.FlexDirection = FlexDirectionType.Row;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexShrink = 1;
            root_child0.Style.FlexBasis = 100;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexBasis = 50;
            root.Children.Insert(1, root_child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
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
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(50,  root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(100, root_child1.Layout.Height);
        }

        [TestMethod]
        public void flex_shrink_to_zero()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Height = 75;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 50;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexShrink = 1;
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 50;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.Width = 50;
            root_child2.Style.Height = 50;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(75, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(75, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(0,  root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child2.Layout.Position.Left);
            Assert.AreEqual(50, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);
        }

        [TestMethod]
        public void flex_basis_overrides_main_size()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 1;
            root_child0.Style.FlexBasis = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 1;
            root_child1.Style.Height = 10;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 1;
            root_child2.Style.Height = 10;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(60,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(60,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(20,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(80,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(60,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(60,  root_child1.Layout.Position.Top);
            Assert.AreEqual(100, root_child1.Layout.Width);
            Assert.AreEqual(20,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(80,  root_child2.Layout.Position.Top);
            Assert.AreEqual(100, root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);
        }

        [TestMethod]
        public void flex_grow_shrink_at_most()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode(config);
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.Style.FlexGrow = 1;
            root_child0_child0.Style.FlexShrink = 1;
            root_child0.Children.Add(root_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void flex_grow_less_than_factor_one()
        {
            YogaConfig config = new YogaConfig();

            YGNode root = new YGNode(config);
            root.Style.Width = 200;
            root.Style.Height = 500;

            YGNode root_child0 = new YGNode(config);
            root_child0.Style.FlexGrow = 0.2f;
            root_child0.Style.FlexBasis = 40;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode(config);
            root_child1.Style.FlexGrow = 0.2f;
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode(config);
            root_child2.Style.FlexGrow = 0.4f;
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(132, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(132, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(92,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(224, root_child2.Layout.Position.Top);
            Assert.AreEqual(200, root_child2.Layout.Width);
            Assert.AreEqual(184, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(200, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(200, root_child0.Layout.Width);
            Assert.AreEqual(132, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child1.Layout.Position.Left);
            Assert.AreEqual(132, root_child1.Layout.Position.Top);
            Assert.AreEqual(200, root_child1.Layout.Width);
            Assert.AreEqual(92,  root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(224, root_child2.Layout.Position.Top);
            Assert.AreEqual(200, root_child2.Layout.Width);
            Assert.AreEqual(184, root_child2.Layout.Height);
        }
    }
}
