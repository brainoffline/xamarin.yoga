using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;

    [TestClass]
    public class AlignItemsTests
    {
        [TestMethod]
        public void align_items_stretch()
        {
            YGNode root_child0;
            YGNode root = new YGNode
            {
                Style = {Width = 100, Height = 100},
                Children =
                {
                    (root_child0 = new YGNode {Style = {Height = 10}})
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(10,  root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_items_center()
        {
            YGNode root_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    AlignItems = YGAlign.Center,
                    Width      = 100,
                    Height     = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 10, Height = 10}})
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_items_flex_start()
        {
            YGNode root_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    AlignItems = YGAlign.FlexStart,
                    Width      = 100,
                    Height     = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 10, Height = 10}})
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_items_flex_end()
        {
            YGNode root_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    AlignItems = YGAlign.FlexEnd,
                    Width      = 100,
                    Height     = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 10, Height = 10}})
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline()
        {
            YGNode root_child0, root_child1;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 50}}),
                    (root_child1 = new YGNode {Style = {Width = 50, Height = 20}})
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(30, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child()
        {
            YGNode root_child0, root_child1, root_child1_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 50}}),
                    (root_child1 = new YGNode
                    {
                        Style = {Width = 50, Height = 20},
                        Children =
                        {
                            ((root_child1_child0 = new YGNode {Style = {Width = 50, Height = 10}}))
                        }
                    }),
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_multiline()
        {
            YGNode root_child0,
                root_child1,
                root_child1_child0,
                root_child1_child1,
                root_child1_child2,
                root_child1_child3;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 60}}),
                    (root_child1 = new YGNode
                    {
                        Style =
                        {
                            FlexDirection = FlexDirectionType.Row,
                            FlexWrap      = WrapType.Wrap,
                            Width         = 50,
                            Height        = 25
                        },
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 25, Height = 20}}),
                            (root_child1_child1 = new YGNode {Style = {Width = 25, Height = 10}}),
                            (root_child1_child2 = new YGNode {Style = {Width = 25, Height = 20}}),
                            (root_child1_child3 = new YGNode {Style = {Width = 25, Height = 10}})
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(25, root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(25, root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(0,  root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_multiline_override()
        {
            YGNode root_child0,
                root_child1,
                root_child1_child0,
                root_child1_child1,
                root_child1_child2,
                root_child1_child3;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 60}}),
                    (root_child1 = new YGNode
                    {
                        Style =
                        {
                            FlexDirection = FlexDirectionType.Row,
                            FlexWrap      = WrapType.Wrap,
                            Width         = 50,
                            Height        = 25
                        },
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 25, Height = 20}}),
                            (root_child1_child1 = new YGNode
                            {
                                Style = {AlignSelf = YGAlign.Baseline, Width = 25, Height = 10}
                            }),
                            (root_child1_child2 = new YGNode {Style = {Width = 25, Height = 20}}),
                            (root_child1_child3 = new YGNode
                            {
                                Style = {AlignSelf = YGAlign.Baseline, Width = 25, Height = 10}
                            })
                        }
                    })
                }
            };


            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(25, root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(25, root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(0,  root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_multiline_no_override_on_secondline()
        {
            YGNode root_child0,        root_child1;
            YGNode root_child1_child0, root_child1_child1, root_child1_child2, root_child1_child3;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 60}}),
                    (root_child1 = new YGNode
                    {
                        Style =
                        {
                            FlexDirection = FlexDirectionType.Row,
                            FlexWrap      = WrapType.Wrap,
                            Width         = 50,
                            Height        = 25
                        },
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 25, Height = 20}}),
                            (root_child1_child1 = new YGNode {Style = {Width = 25, Height = 10}}),
                            (root_child1_child2 = new YGNode {Style = {Width = 25, Height = 20}}),
                            (root_child1_child3 = new YGNode
                            {
                                Style = {AlignSelf = YGAlign.Baseline, Width = 25, Height = 10}
                            })
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(25, root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(25, root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(60, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(25, root_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child1.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child1.Layout.Width);
            Assert.AreEqual(10, root_child1_child1.Layout.Height);

            Assert.AreEqual(25, root_child1_child2.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child2.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child2.Layout.Width);
            Assert.AreEqual(20, root_child1_child2.Layout.Height);

            Assert.AreEqual(0,  root_child1_child3.Layout.Position.Left);
            Assert.AreEqual(20, root_child1_child3.Layout.Position.Top);
            Assert.AreEqual(25, root_child1_child3.Layout.Width);
            Assert.AreEqual(10, root_child1_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_top()
        {
            YGNode root_child0, root_child1, root_child1_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style =
                        {
                            Width    = 50,
                            Height   = 50,
                            Position = {Top = 10}
                        }
                    }),
                    (root_child1 = new YGNode
                    {
                        Style = {Width = 50, Height = 20},
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 50, Height = 10}})
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(10, root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_top2()
        {
            YGNode root_child0, root_child1, root_child1_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 50}}),
                    (root_child1 = new YGNode
                    {
                        Style = {Width = 50, Height = 20, Position = {Top = 5}},
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 50, Height = 10}})
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(45, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(45, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_double_nested_child()
        {
            YGNode root_child0,        root_child1;
            YGNode root_child0_child0, root_child1_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style = {Width = 50, Height = 50},
                        Children =
                        {
                            (root_child0_child0 = new YGNode {Style = {Width = 50, Height = 20}})
                        }
                    }),
                    (root_child1 = new YGNode
                    {
                        Style = {Width = 50, Height = 20},
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 50, Height = 15}})
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(15, root_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(15, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_column()
        {
            YGNode root_child0, root_child1;
            YGNode root = new YGNode
            {
                Style =
                {
                    AlignItems = YGAlign.Baseline,
                    Width      = 100,
                    Height     = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 50}}),
                    (root_child1 = new YGNode {Style = {Width = 50, Height = 20}})
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_margin()
        {
            YGNode root_child0, root_child1, root_child1_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style =
                        {
                            Margin = new Edges(5, 5, 5, 5),
                            Width  = 50,
                            Height = 50
                        }
                    }),
                    (root_child1 = new YGNode
                    {
                        Style = {Width = 50, Height = 20},
                        Children =
                        {
                            (root_child1_child0 = new YGNode
                            {
                                Style =
                                {
                                    Margin = new Edges(1, 1, 1, 1),
                                    Width  = 50, Height = 10
                                }
                            })
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(5,  root_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(60, root_child1.Layout.Position.Left);
            Assert.AreEqual(44, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(1,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(1,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(-10, root_child1.Layout.Position.Left);
            Assert.AreEqual(44,  root_child1.Layout.Position.Top);
            Assert.AreEqual(50,  root_child1.Layout.Width);
            Assert.AreEqual(20,  root_child1.Layout.Height);

            Assert.AreEqual(-1, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(1,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_child_padding()
        {
            YGNode root_child0;
            YGNode root_child1;
            YGNode root_child1_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    Padding       = new Edges(5, 5, 5, 5),
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 50}}),
                    (root_child1 = new YGNode
                    {
                        Style = {Padding = new Edges(5, 5, 5, 5), Width = 50, Height = 20},
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 50, Height = 10}})
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(5,  root_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(55, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(5,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(-5, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(-5, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(5,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_multiline()
        {
            YGNode root_child0;
            YGNode root_child1;
            YGNode root_child1_child0;
            YGNode root_child2;
            YGNode root_child2_child0;
            YGNode root_child3;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 50}}),
                    (root_child1 = new YGNode
                    {
                        Style = {Width = 50, Height = 20},
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 50, Height = 10}})
                        }
                    }),
                    (root_child2 = new YGNode
                    {
                        Style = {Width = 50, Height = 20},
                        Children =
                        {
                            (root_child2_child0 = new YGNode {Style = {Width = 50, Height = 10}})
                        }
                    }),
                    (root_child3 = new YGNode {Style = {Width = 50, Height = 50}})
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(100, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(60, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(20, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(100, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(60, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(50, root_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_multiline_column()
        {
            YGNode root_child0;
            YGNode root_child1;
            YGNode root_child1_child0;
            YGNode root_child2;
            YGNode root_child2_child0;
            YGNode root_child3;
            YGNode root = new YGNode
            {
                Style =
                {
                    AlignItems = YGAlign.Baseline,
                    FlexWrap   = WrapType.Wrap,
                    Width      = 100,
                    Height     = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 50}}),
                    (root_child1 = new YGNode
                    {
                        Style = {Width = 30, Height = 50},
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 20, Height = 20}})
                        }
                    }),
                    (root_child2 = new YGNode
                    {
                        Style = {Width = 40, Height = 70},
                        Children =
                        {
                            (root_child2_child0 = new YGNode {Style = {Width = 10, Height = 10}})
                        }
                    }),
                    (root_child3 = new YGNode {Style = {Width = 50, Height = 20}})
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(40, root_child2.Layout.Width);
            Assert.AreEqual(70, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(70, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(70, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(40, root_child2.Layout.Width);
            Assert.AreEqual(70, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(70, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_multiline_column2()
        {
            YGNode root_child0;
            YGNode root_child1;
            YGNode root_child1_child0;
            YGNode root_child2;
            YGNode root_child2_child0;
            YGNode root_child3;
            YGNode root = new YGNode
            {
                Style =
                {
                    AlignItems = YGAlign.Baseline,
                    FlexWrap   = WrapType.Wrap,
                    Width      = 100,
                    Height     = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 50}}),
                    (root_child1 = new YGNode
                    {
                        Style = {Width = 30, Height = 50},
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 20, Height = 20}})
                        }
                    }),
                    (root_child2 = new YGNode
                    {
                        Style = {Width = 40, Height = 70},
                        Children =
                        {
                            (root_child2_child0 = new YGNode {Style = {Width = 10, Height = 10}})
                        }
                    }),
                    (root_child3 = new YGNode {Style = {Width = 50, Height = 20}})
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(50, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(40, root_child2.Layout.Width);
            Assert.AreEqual(70, root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(70, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(70, root_child1.Layout.Position.Left);
            Assert.AreEqual(50, root_child1.Layout.Position.Top);
            Assert.AreEqual(30, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(10, root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child1_child0.Layout.Width);
            Assert.AreEqual(20, root_child1_child0.Layout.Height);

            Assert.AreEqual(10, root_child2.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2.Layout.Position.Top);
            Assert.AreEqual(40, root_child2.Layout.Width);
            Assert.AreEqual(70, root_child2.Layout.Height);

            Assert.AreEqual(30, root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(70, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);
        }

        [TestMethod]
        public void align_baseline_multiline_row_and_column()
        {
            YGNode root_child0;
            YGNode root_child1;
            YGNode root_child1_child0;
            YGNode root_child2;
            YGNode root_child2_child0;
            YGNode root_child3;
            YGNode root = new YGNode
            {
                Style =
                {
                    FlexDirection = FlexDirectionType.Row,
                    AlignItems    = YGAlign.Baseline,
                    FlexWrap      = WrapType.Wrap,
                    Width         = 100,
                    Height        = 100
                },
                Children =
                {
                    (root_child0 = new YGNode {Style = {Width = 50, Height = 50}}),
                    (root_child1 = new YGNode
                    {
                        Style = {Width = 50, Height = 50},
                        Children =
                        {
                            (root_child1_child0 = new YGNode {Style = {Width = 50, Height = 10}})
                        }
                    }),
                    (root_child2 = new YGNode
                    {
                        Style = {Width = 50, Height = 20},
                        Children =
                        {
                            (root_child2_child0 = new YGNode {Style = {Width = 50, Height = 10}})
                        }
                    }),
                    (root_child3 = new YGNode {Style = {Width = 50, Height = 20}})
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(50, root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            Assert.AreEqual(0,   root_child2.Layout.Position.Left);
            Assert.AreEqual(100, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(50, root_child3.Layout.Position.Left);
            Assert.AreEqual(90, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(50, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child0.Layout.Width);
            Assert.AreEqual(50, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child1.Layout.Position.Left);
            Assert.AreEqual(40, root_child1.Layout.Position.Top);
            Assert.AreEqual(50, root_child1.Layout.Width);
            Assert.AreEqual(50, root_child1.Layout.Height);

            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child1_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child1_child0.Layout.Width);
            Assert.AreEqual(10, root_child1_child0.Layout.Height);

            Assert.AreEqual(50,  root_child2.Layout.Position.Left);
            Assert.AreEqual(100, root_child2.Layout.Position.Top);
            Assert.AreEqual(50,  root_child2.Layout.Width);
            Assert.AreEqual(20,  root_child2.Layout.Height);

            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child2_child0.Layout.Position.Top);
            Assert.AreEqual(50, root_child2_child0.Layout.Width);
            Assert.AreEqual(10, root_child2_child0.Layout.Height);

            Assert.AreEqual(0,  root_child3.Layout.Position.Left);
            Assert.AreEqual(90, root_child3.Layout.Position.Top);
            Assert.AreEqual(50, root_child3.Layout.Width);
            Assert.AreEqual(20, root_child3.Layout.Height);
        }

        [TestMethod]
        public void align_items_center_child_with_margin_bigger_than_parent()
        {
            YGNode root_child0;
            YGNode root_child0_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = JustifyType.Center,
                    AlignItems     = YGAlign.Center,
                    Width          = 52,
                    Height         = 52
                },
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style = {AlignItems = YGAlign.Center},
                        Children =
                        {
                            (root_child0_child0 = new YGNode
                            {
                                Style =
                                {
                                    Width  = 52,
                                    Height = 52,
                                    Margin = {Left = 10, Right = 10}
                                }
                            })
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(52,  root_child0.Layout.Height);

            Assert.AreEqual(10, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(52, root_child0_child0.Layout.Width);
            Assert.AreEqual(52, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(52,  root_child0.Layout.Height);

            Assert.AreEqual(10, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(52, root_child0_child0.Layout.Width);
            Assert.AreEqual(52, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_items_flex_end_child_with_margin_bigger_than_parent()
        {
            YGNode root_child0;
            YGNode root_child0_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = JustifyType.Center,
                    AlignItems     = YGAlign.Center,
                    Width          = 52,
                    Height         = 52
                },
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style = {AlignItems = YGAlign.FlexEnd},
                        Children =
                        {
                            (root_child0_child0 = new YGNode
                            {
                                Style =
                                {
                                    Width  = 52,
                                    Height = 52,
                                    Margin = {Left = 10, Right = 10}
                                }
                            })
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(52,  root_child0.Layout.Height);

            Assert.AreEqual(10, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(52, root_child0_child0.Layout.Width);
            Assert.AreEqual(52, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(52,  root_child0.Layout.Height);

            Assert.AreEqual(10, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(52, root_child0_child0.Layout.Width);
            Assert.AreEqual(52, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_items_center_child_without_margin_bigger_than_parent()
        {
            YGNode root_child0;
            YGNode root_child0_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = JustifyType.Center,
                    AlignItems     = YGAlign.Center,
                    Width          = 52,
                    Height         = 52
                },
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style = {AlignItems = YGAlign.Center},
                        Children =
                        {
                            (root_child0_child0 = new YGNode {Style = {Width = 72, Height = 72}})
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_items_flex_end_child_without_margin_bigger_than_parent()
        {
            YGNode root_child0;
            YGNode root_child0_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    JustifyContent = JustifyType.Center,
                    AlignItems     = YGAlign.Center,
                    Width          = 52,
                    Height         = 52
                },
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style = {AlignItems = YGAlign.FlexEnd},
                        Children =
                        {
                            (root_child0_child0 = new YGNode {Style = {Width = 72, Height = 72}})
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,  root.Layout.Position.Left);
            Assert.AreEqual(0,  root.Layout.Position.Top);
            Assert.AreEqual(52, root.Layout.Width);
            Assert.AreEqual(52, root.Layout.Height);

            Assert.AreEqual(-10, root_child0.Layout.Position.Left);
            Assert.AreEqual(-10, root_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_center_should_size_based_on_content()
        {
            YGNode root_child0;
            YGNode root_child0_child0;
            YGNode root_child0_child0_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    AlignItems = YGAlign.Center,
                    Margin     = {Top = 20},
                    Width      = 100,
                    Height     = 100
                },
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style = {JustifyContent = JustifyType.Center, FlexShrink = 1},
                        Children =
                        {
                            (root_child0_child0 = new YGNode
                            {
                                Style = {FlexGrow = 1, FlexShrink = 1},
                                Children =
                                {
                                    (root_child0_child0_child0 = new YGNode
                                    {
                                        Style = {Width = 20, Height = 20}
                                    })
                                }
                            })
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(20,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(40, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(20,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(40, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0.Layout.Width);
            Assert.AreEqual(20, root_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_strech_should_size_based_on_parent()
        {
            YGNode root_child0;
            YGNode root_child0_child0;
            YGNode root_child0_child0_child0;
            YGNode root = new YGNode
            {
                Style =
                {
                    Width  = 100,
                    Height = 100,
                    Margin = {Top = 20}
                },
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style = {JustifyContent = JustifyType.Center, FlexShrink = 1},
                        Children =
                        {
                            (root_child0_child0 = new YGNode
                            {
                                Style = {FlexGrow = 1, FlexShrink = 1},
                                Children =
                                {
                                    (root_child0_child0_child0 = new YGNode
                                    {
                                        Style = {Width = 20, Height = 20}
                                    })
                                }
                            })
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(20,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(20,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(20,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(20,  root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0.Layout.Width);
            Assert.AreEqual(20,  root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(100, root_child0_child0.Layout.Width);
            Assert.AreEqual(20,  root_child0_child0.Layout.Height);

            Assert.AreEqual(80, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(20, root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_flex_start_with_shrinking_children()
        {
            YGNode root_child0;
            YGNode root_child0_child0;
            YGNode root_child0_child0_child0;
            YGNode root = new YGNode
            {
                Style = {Width = 500, Height = 500},
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style = {AlignItems = YGAlign.FlexStart},
                        Children =
                        {
                            (root_child0_child0 = new YGNode
                            {
                                Style = {FlexGrow = 1, FlexShrink = 1},
                                Children =
                                {
                                    (root_child0_child0_child0 = new YGNode
                                    {
                                        Style = {FlexGrow = 1, FlexShrink = 1}
                                    })
                                }
                            })
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(500, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_flex_start_with_stretching_children()
        {
            YGNode root_child0;
            YGNode root_child0_child0;
            YGNode root_child0_child0_child0;
            YGNode root = new YGNode
            {
                Style = {Width = 500, Height = 500},
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Children =
                        {
                            (root_child0_child0 = new YGNode
                            {
                                Style = {FlexGrow = 1, FlexShrink = 1},
                                Children =
                                {
                                    (root_child0_child0_child0 = new YGNode
                                    {
                                        Style = {FlexGrow = 1, FlexShrink = 1}
                                    })
                                }
                            })
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Height);
        }

        [TestMethod]
        public void align_flex_start_with_shrinking_children_with_stretch()
        {
            YGNode root_child0;
            YGNode root_child0_child0;
            YGNode root_child0_child0_child0;
            YGNode root = new YGNode
            {
                Style = {Width = 500, Height = 500},
                Children =
                {
                    (root_child0 = new YGNode
                    {
                        Style = {AlignItems = YGAlign.FlexStart},
                        Children =
                        {
                            (root_child0_child0 = new YGNode
                            {
                                Style = {FlexGrow = 1, FlexShrink = 1},
                                Children =
                                {
                                    (root_child0_child0_child0 = new YGNode
                                    {
                                        Style = {FlexGrow = 1, FlexShrink = 1}
                                    })
                                }
                            })
                        }
                    })
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0.Layout.Height);

            Assert.AreEqual(500, root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0_child0.Layout.Width);
            Assert.AreEqual(0,   root_child0_child0.Layout.Height);

            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(0, root_child0_child0_child0.Layout.Height);
        }
    }
}
