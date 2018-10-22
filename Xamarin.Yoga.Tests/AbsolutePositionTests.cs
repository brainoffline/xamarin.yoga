﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;

    [TestClass]
    public class AbsolutePositionTests
    {
        [TestMethod]
        public void absolute_layout_width_height_start_top()
        {
            YGNode root_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    Width  = 100,
                    Height = 100
                },
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style =
                        {
                            PositionType = YGPositionType.Absolute,
                            Width        = 10,
                            Height       = 10,
                            Position     = {Start = 10, Top = 10}
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_width_height_end_bottom()
        {
            YGNode root = new YGNode
            {
                Style = {Width = 100, Height = 100},
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {End = 10, Bottom = 10},
                    Width        = 10,
                    Height       = 10
                }
            };
            root.Children.Add(root_child0);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_start_top_end_bottom()
        {
            YGNode root = new YGNode {Style = {Width = 100, Height = 100}};

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = new Edges(10, 10, 10, 10)
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(80, root_child0.Layout.Width);
            Assert.AreEqual(80, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_width_height_start_top_end_bottom()
        {
            YGNode root = new YGNode {Style = {Width = 100, Height = 100}};

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = new Edges(10, 10, 10, 10),
                    Width        = 10,
                    Height       = 10
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void do_not_clamp_height_of_absolute_node_to_height_of_its_overflow_hidden_parent()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = YGFlexDirection.Row,
                    Overflow      = YGOverflow.Hidden,
                    Width         = 50, Height = 50
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Start = 0, Top = 0}
                }
            };
            root.Children.Add(root_child0);

            YGNode root_child0_child0 = new YGNode
            {
                Style = {Width = 100, Height = 100}
            };
            root_child0.Children.Add(root_child0_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(50, root.Layout.Width);
            Assert.AreEqual(50, root.Layout.Height);

            Assert.AreEqual(-50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(100, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(100, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_within_border()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    Margin  = new Edges(10, 10, 10, 10),
                    Padding = new Edges(10, 10, 10, 10),
                    Border  = new Edges(10, 10, 10, 10),
                    Width   = 100,
                    Height  = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Left = 0, Top = 0},
                    Width        = 50,
                    Height       = 50
                }
            };
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Right = 0, Bottom = 0},
                    Width        = 50,
                    Height       = 50
                }
            };
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Left = 0, Top = 0},
                    Margin       = new Edges(10, 10, 10, 10),
                    Width        = 50,
                    Height       = 50
                }
            };
            root.Children.Insert(2, root_child2);

            YGNode root_child3 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Right = 0, Bottom = 0},
                    Margin       = new Edges(10, 10, 10, 10),
                    Width        = 50,
                    Height       = 50
                }
            };
            root.Children.Insert(3, root_child3);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(10,  root.Layout.Position.Left);
            Assert.AreEqual(10,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(20, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(10,  root.Layout.Position.Left);
            Assert.AreEqual(10,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(10, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(40, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(20, root_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child2.Layout.Position.Top);
            Assert.AreEqual(50, root_child2.Layout.Width);
            Assert.AreEqual(50, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child3.Layout.Position.Left);
            Assert.AreEqual(30, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = YGJustify.Center,
                    AlignItems     = YGAlign.Center,
                    FlexGrow       = 1,
                    Width          = 110,
                    Height         = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Width        = 60, Height = 40
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_flex_end()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = YGJustify.FlexEnd,
                    AlignItems     = YGAlign.FlexEnd,
                    FlexGrow       = 1,
                    Width          = 110,
                    Height         = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Width        = 60, Height = 40
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(60, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(60, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_justify_content_center()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = YGJustify.Center,
                    FlexGrow       = 1,
                    Width          = 110, Height = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Width        = 60, Height = 40
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_center()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    AlignItems = YGAlign.Center,
                    FlexGrow   = 1,
                    Width      = 110, Height = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Width        = 60, Height = 40
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_center_on_child_only()
        {
            YGNode root = new YGNode
            {
                Style = {FlexGrow = 1, Width = 110, Height = 100}
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    AlignSelf    = YGAlign.Center,
                    PositionType = YGPositionType.Absolute,
                    Width        = 60, Height = 40
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_top_position()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = YGJustify.Center,
                    AlignItems     = YGAlign.Center,
                    FlexGrow       = 1,
                    Width          = 110,
                    Height         = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Top = 10},
                    Width        = 60, Height = 40
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_bottom_position()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = YGJustify.Center,
                    AlignItems     = YGAlign.Center,
                    FlexGrow       = 1,
                    Width          = 110,
                    Height         = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Bottom = 10},
                    Width        = 60, Height = 40
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(25, root_child0.Layout.Position.Left);
            Assert.AreEqual(50, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_left_position()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = YGJustify.Center,
                    AlignItems     = YGAlign.Center,
                    FlexGrow       = 1,
                    Width          = 110,
                    Height         = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Left = 5},
                    Width        = 60, Height = 40
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(5,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(5,  root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_align_items_and_justify_content_center_and_right_position()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = YGJustify.Center,
                    AlignItems     = YGAlign.Center,
                    FlexGrow       = 1,
                    Width          = 110,
                    Height         = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Right = 5},
                    Width        = 60, Height = 40
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(110, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(30, root_child0.Layout.Position.Top);
            Assert.AreEqual(60, root_child0.Layout.Width);
            Assert.AreEqual(40, root_child0.Layout.Height);
        }

        [TestMethod]
        public void position_root_with_rtl_should_position_withoutdirection()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    Width    = 52, Height = 52,
                    Position = {Left = 72}
                }
            };
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(72, root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(72, root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_percentage_bottom_based_on_parent_height()
        {
            YGNode root = new YGNode {Style = {Width = 100, Height = 200}};

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Top = 50.Percent()},
                    Width        = 10,
                    Height       = 10
                }
            };
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Bottom = 50.Percent()},
                    Width        = 10,
                    Height       = 10
                }
            };
            root.Children.Insert(1, root_child1);

            YGNode root_child2 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Position     = {Top = 10.Percent(), Bottom = 10.Percent()},
                    Width        = 10
                }
            };
            root.Children.Insert(2, root_child2);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(100, root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(90, root_child1.Layout.Position.Top);
            Assert.AreEqual(10, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(160, root_child2.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(200, root.Layout.Height);

            Assert.AreEqual(90,  root_child0.Layout.Position.Left);
            Assert.AreEqual(100, root_child0.Layout.Position.Top);
            Assert.AreEqual(10,  root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            Assert.AreEqual(90, root_child1.Layout.Position.Left);
            Assert.AreEqual(90, root_child1.Layout.Position.Top);
            Assert.AreEqual(10, root_child1.Layout.Width);
            Assert.AreEqual(10, root_child1.Layout.Height);

            Assert.AreEqual(90,  root_child2.Layout.Position.Left);
            Assert.AreEqual(20,  root_child2.Layout.Position.Top);
            Assert.AreEqual(10,  root_child2.Layout.Width);
            Assert.AreEqual(160, root_child2.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_column_container()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexWrap = YGWrap.WrapReverse,
                    Width    = 100, Height = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Width        = 20, Height = 20
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_row_container()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = YGFlexDirection.Row,
                    FlexWrap      = YGWrap.WrapReverse,
                    Width         = 100,
                    Height        = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    PositionType = YGPositionType.Absolute,
                    Width        = 20, Height = 20
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(80, root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_column_container_flex_end()
        {
            YGNode root = new YGNode {Style = {FlexWrap = YGWrap.WrapReverse, Width = 100, Height = 100}};

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    AlignSelf    = YGAlign.FlexEnd,
                    PositionType = YGPositionType.Absolute,
                    Width        = 20, Height = 20
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }

        [TestMethod]
        public void absolute_layout_in_wrap_reverse_row_container_flex_end()
        {
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = YGFlexDirection.Row,
                    FlexWrap      = YGWrap.WrapReverse,
                    Width         = 100,
                    Height        = 100
                }
            };

            YGNode root_child0 = new YGNode
            {
                Style =
                {
                    AlignSelf    = YGAlign.FlexEnd,
                    PositionType = YGPositionType.Absolute,
                    Width        = 20, Height = 20
                }
            };
            root.Children.Add(root_child0);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(80, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);
        }
    }
}