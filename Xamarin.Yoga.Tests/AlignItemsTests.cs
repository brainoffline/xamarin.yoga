using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class AlignItemsTests
    {
        [TestMethod]
        public void align_baseline()
        {
            YogaNode root_child0, root_child1;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 50}),
                    (root_child1 = new YogaNode {Width = 50, Height = 20})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
            YogaNode root_child0, root_child1, root_child1_child0;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 50}),
                    (root_child1 = new YogaNode
                    {
                        Width = 50, Height = 20,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 50, Height = 10})
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_baseline_child_margin()
        {
            YogaNode root_child0, root_child1, root_child1_child0;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        Margin = new Edges(5, 5, 5, 5),
                        Width  = 50,
                        Height = 50
                    }),
                    (root_child1 = new YogaNode
                    {
                        Width = 50, Height = 20,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode
                            {
                                Margin = new Edges(1, 1, 1, 1),
                                Width  = 50, Height = 10
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_baseline_child_multiline()
        {
            YogaNode root_child0,
                root_child1,
                root_child1_child0,
                root_child1_child1,
                root_child1_child2,
                root_child1_child3;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 60}),
                    (root_child1 = new YogaNode
                    {
                        FlexDirection = FlexDirectionType.Row,
                        FlexWrap      = WrapType.Wrap,
                        Width         = 50,
                        Height        = 25,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 25, Height = 20}),
                            (root_child1_child1 = new YogaNode {Width = 25, Height = 10}),
                            (root_child1_child2 = new YogaNode {Width = 25, Height = 20}),
                            (root_child1_child3 = new YogaNode {Width = 25, Height = 10})
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_baseline_child_multiline_no_override_on_secondline()
        {
            YogaNode root_child0,        root_child1;
            YogaNode root_child1_child0, root_child1_child1, root_child1_child2, root_child1_child3;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 60}),
                    (root_child1 = new YogaNode
                    {
                        FlexDirection = FlexDirectionType.Row,
                        FlexWrap      = WrapType.Wrap,
                        Width         = 50,
                        Height        = 25,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 25, Height = 20}),
                            (root_child1_child1 = new YogaNode {Width = 25, Height = 10}),
                            (root_child1_child2 = new YogaNode {Width = 25, Height = 20}),
                            (root_child1_child3 = new YogaNode
                            {
                                AlignSelf = AlignType.Baseline, Width = 25, Height = 10
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
            YogaNode root_child0,
                root_child1,
                root_child1_child0,
                root_child1_child1,
                root_child1_child2,
                root_child1_child3;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 60}),
                    (root_child1 = new YogaNode
                    {
                        FlexDirection = FlexDirectionType.Row,
                        FlexWrap      = WrapType.Wrap,
                        Width         = 50,
                        Height        = 25,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 25, Height = 20}),
                            (root_child1_child1 = new YogaNode
                            {
                                AlignSelf = AlignType.Baseline, Width = 25, Height = 10
                            }),
                            (root_child1_child2 = new YogaNode {Width = 25, Height = 20}),
                            (root_child1_child3 = new YogaNode
                            {
                                AlignSelf = AlignType.Baseline, Width = 25, Height = 10
                            })
                        }
                    })
                }
            };


            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_baseline_child_padding()
        {
            YogaNode root_child0;
            YogaNode root_child1;
            YogaNode root_child1_child0;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Padding       = new Edges(5, 5, 5, 5),
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 50}),
                    (root_child1 = new YogaNode
                    {
                        Padding = new Edges(5, 5, 5, 5), Width = 50, Height = 20,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 50, Height = 10})
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_baseline_child_top()
        {
            YogaNode root_child0, root_child1, root_child1_child0;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        Width    = 50,
                        Height   = 50,
                        Position = {Top = 10}
                    }),
                    (root_child1 = new YogaNode
                    {
                        Width = 50, Height = 20,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 50, Height = 10})
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
            YogaNode root_child0, root_child1, root_child1_child0;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 50}),
                    (root_child1 = new YogaNode
                    {
                        Width = 50, Height = 20, Position = {Top = 5},
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 50, Height = 10})
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_baseline_column()
        {
            YogaNode root_child0, root_child1;
            var root = new YogaNode
            {
                AlignItems = AlignType.Baseline,
                Width      = 100,
                Height     = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 50}),
                    (root_child1 = new YogaNode {Width = 50, Height = 20})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_baseline_double_nested_child()
        {
            YogaNode root_child0,        root_child1;
            YogaNode root_child0_child0, root_child1_child0;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        Width = 50, Height = 50,
                        Children =
                        {
                            (root_child0_child0 = new YogaNode {Width = 50, Height = 20})
                        }
                    }),
                    (root_child1 = new YogaNode
                    {
                        Width = 50, Height = 20,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 50, Height = 15})
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_baseline_multiline()
        {
            YogaNode root_child0;
            YogaNode root_child1;
            YogaNode root_child1_child0;
            YogaNode root_child2;
            YogaNode root_child2_child0;
            YogaNode root_child3;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                FlexWrap      = WrapType.Wrap,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 50}),
                    (root_child1 = new YogaNode
                    {
                        Width = 50, Height = 20,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 50, Height = 10})
                        }
                    }),
                    (root_child2 = new YogaNode
                    {
                        Width = 50, Height = 20,
                        Children =
                        {
                            (root_child2_child0 = new YogaNode {Width = 50, Height = 10})
                        }
                    }),
                    (root_child3 = new YogaNode {Width = 50, Height = 50})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
            YogaNode root_child0;
            YogaNode root_child1;
            YogaNode root_child1_child0;
            YogaNode root_child2;
            YogaNode root_child2_child0;
            YogaNode root_child3;
            var root = new YogaNode
            {
                AlignItems = AlignType.Baseline,
                FlexWrap   = WrapType.Wrap,
                Width      = 100,
                Height     = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 50}),
                    (root_child1 = new YogaNode
                    {
                        Width = 30, Height = 50,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 20, Height = 20})
                        }
                    }),
                    (root_child2 = new YogaNode
                    {
                        Width = 40, Height = 70,
                        Children =
                        {
                            (root_child2_child0 = new YogaNode {Width = 10, Height = 10})
                        }
                    }),
                    (root_child3 = new YogaNode {Width = 50, Height = 20})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
            YogaNode root_child0;
            YogaNode root_child1;
            YogaNode root_child1_child0;
            YogaNode root_child2;
            YogaNode root_child2_child0;
            YogaNode root_child3;
            var root = new YogaNode
            {
                AlignItems = AlignType.Baseline,
                FlexWrap   = WrapType.Wrap,
                Width      = 100,
                Height     = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 50}),
                    (root_child1 = new YogaNode
                    {
                        Width = 30, Height = 50,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 20, Height = 20})
                        }
                    }),
                    (root_child2 = new YogaNode
                    {
                        Width = 40, Height = 70,
                        Children =
                        {
                            (root_child2_child0 = new YogaNode {Width = 10, Height = 10})
                        }
                    }),
                    (root_child3 = new YogaNode {Width = 50, Height = 20})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
            YogaNode root_child0;
            YogaNode root_child1;
            YogaNode root_child1_child0;
            YogaNode root_child2;
            YogaNode root_child2_child0;
            YogaNode root_child3;
            var root = new YogaNode
            {
                FlexDirection = FlexDirectionType.Row,
                AlignItems    = AlignType.Baseline,
                FlexWrap      = WrapType.Wrap,
                Width         = 100,
                Height        = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 50, Height = 50}),
                    (root_child1 = new YogaNode
                    {
                        Width = 50, Height = 50,
                        Children =
                        {
                            (root_child1_child0 = new YogaNode {Width = 50, Height = 10})
                        }
                    }),
                    (root_child2 = new YogaNode
                    {
                        Width = 50, Height = 20,
                        Children =
                        {
                            (root_child2_child0 = new YogaNode {Width = 50, Height = 10})
                        }
                    }),
                    (root_child3 = new YogaNode {Width = 50, Height = 20})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_center_should_size_based_on_content()
        {
            YogaNode root_child0;
            YogaNode root_child0_child0;
            YogaNode root_child0_child0_child0;
            var root = new YogaNode
            {
                AlignItems = AlignType.Center,
                Margin     = {Top = 20},
                Width      = 100,
                Height     = 100,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        JustifyContent = JustifyType.Center, FlexShrink = 1,
                        Children =
                        {
                            (root_child0_child0 = new YogaNode
                            {
                                FlexGrow = 1, FlexShrink = 1,
                                Children =
                                {
                                    (root_child0_child0_child0 = new YogaNode
                                    {
                                        Width = 20, Height = 20
                                    })
                                }
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_flex_start_with_shrinking_children()
        {
            YogaNode root_child0;
            YogaNode root_child0_child0;
            YogaNode root_child0_child0_child0;
            var root = new YogaNode
            {
                Width = 500, Height = 500,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        AlignItems = AlignType.FlexStart,
                        Children =
                        {
                            (root_child0_child0 = new YogaNode
                            {
                                FlexGrow   = 1,
                                FlexShrink = 1,
                                Children =
                                {
                                    (root_child0_child0_child0 = new YogaNode
                                    {
                                        FlexGrow = 1, FlexShrink = 1
                                    })
                                }
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_flex_start_with_shrinking_children_with_stretch()
        {
            YogaNode root_child0;
            YogaNode root_child0_child0;
            YogaNode root_child0_child0_child0;
            var root = new YogaNode
            {
                Width = 500, Height = 500,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        AlignItems = AlignType.FlexStart,
                        Children =
                        {
                            (root_child0_child0 = new YogaNode
                            {
                                FlexGrow = 1, FlexShrink = 1,
                                Children =
                                {
                                    (root_child0_child0_child0 = new YogaNode
                                    {
                                        FlexGrow = 1, FlexShrink = 1
                                    })
                                }
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
            YogaNode root_child0;
            YogaNode root_child0_child0;
            YogaNode root_child0_child0_child0;
            var root = new YogaNode
            {
                Width = 500, Height = 500,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        Children =
                        {
                            (root_child0_child0 = new YogaNode
                            {
                                FlexGrow = 1, FlexShrink = 1,
                                Children =
                                {
                                    (root_child0_child0_child0 = new YogaNode
                                    {
                                        FlexGrow = 1, FlexShrink = 1
                                    })
                                }
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_items_center()
        {
            YogaNode root_child0;
            var root = new YogaNode
            {
                AlignItems = AlignType.Center,
                Width      = 100,
                Height     = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 10, Height = 10})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(45, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_items_center_child_with_margin_bigger_than_parent()
        {
            YogaNode root_child0;
            YogaNode root_child0_child0;
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                AlignItems     = AlignType.Center,
                Width          = 52,
                Height         = 52,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        AlignItems = AlignType.Center,
                        Children =
                        {
                            (root_child0_child0 = new YogaNode
                            {
                                Width  = 52,
                                Height = 52,
                                Margin = {Left = 10, Right = 10}
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
            YogaNode root_child0;
            YogaNode root_child0_child0;
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                AlignItems     = AlignType.Center,
                Width          = 52,
                Height         = 52,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        AlignItems = AlignType.Center,
                        Children =
                        {
                            (root_child0_child0 = new YogaNode {Width = 72, Height = 72})
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_items_flex_end()
        {
            YogaNode root_child0;
            var root = new YogaNode
            {
                AlignItems = AlignType.FlexEnd,
                Width      = 100,
                Height     = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 10, Height = 10})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(90, root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_items_flex_end_child_with_margin_bigger_than_parent()
        {
            YogaNode root_child0;
            YogaNode root_child0_child0;
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                AlignItems     = AlignType.Center,
                Width          = 52,
                Height         = 52,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        AlignItems = AlignType.FlexEnd,
                        Children =
                        {
                            (root_child0_child0 = new YogaNode
                            {
                                Width  = 52,
                                Height = 52,
                                Margin = {Left = 10, Right = 10}
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_items_flex_end_child_without_margin_bigger_than_parent()
        {
            YogaNode root_child0;
            YogaNode root_child0_child0;
            var root = new YogaNode
            {
                JustifyContent = JustifyType.Center,
                AlignItems     = AlignType.Center,
                Width          = 52,
                Height         = 52,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        AlignItems = AlignType.FlexEnd,
                        Children =
                        {
                            (root_child0_child0 = new YogaNode {Width = 72, Height = 72})
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_items_flex_start()
        {
            YogaNode root_child0;
            var root = new YogaNode
            {
                AlignItems = AlignType.FlexStart,
                Width      = 100,
                Height     = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Width = 10, Height = 10})
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0,   root.Layout.Position.Top);
            Assert.AreEqual(100, root.Layout.Width);
            Assert.AreEqual(100, root.Layout.Height);

            Assert.AreEqual(0,  root_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0.Layout.Position.Top);
            Assert.AreEqual(10, root_child0.Layout.Width);
            Assert.AreEqual(10, root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_items_stretch()
        {
            YogaNode root_child0;
            var root = new YogaNode
            {
                Width = 100, Height = 100,
                Children =
                {
                    (root_child0 = new YogaNode {Height = 10})
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
            Assert.AreEqual(10,  root_child0.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
        public void align_strech_should_size_based_on_parent()
        {
            YogaNode root_child0;
            YogaNode root_child0_child0;
            YogaNode root_child0_child0_child0;
            var root = new YogaNode
            {
                Width  = 100,
                Height = 100,
                Margin = {Top = 20},
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        JustifyContent = JustifyType.Center, FlexShrink = 1,
                        Children =
                        {
                            (root_child0_child0 = new YogaNode
                            {
                                FlexGrow = 1, FlexShrink = 1,
                                Children =
                                {
                                    (root_child0_child0_child0 = new YogaNode
                                    {
                                        Width = 20, Height = 20
                                    })
                                }
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

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
    }
}
