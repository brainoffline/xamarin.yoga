using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class AndroidNewsFeed
    {
        [TestMethod]
        public void android_news_feed()
        {
            YogaNode root_child0;
            YogaNode root_child0_child0;
            YogaNode root_child0_child0_child0;
            YogaNode root_child0_child0_child0_child0;
            YogaNode root_child0_child0_child0_child0_child0;
            YogaNode root_child0_child0_child0_child0_child1;
            YogaNode root_child0_child0_child0_child0_child1_child0;
            YogaNode root_child0_child0_child0_child0_child0_child0;
            YogaNode root_child0_child0_child0_child0_child1_child1;
            YogaNode root_child0_child0_child1;
            YogaNode root_child0_child0_child1_child0;
            YogaNode root_child0_child0_child1_child0_child0;
            YogaNode root_child0_child0_child1_child0_child0_child0;
            YogaNode root_child0_child0_child1_child0_child1;
            YogaNode root_child0_child0_child1_child0_child1_child0;
            YogaNode root_child0_child0_child1_child0_child1_child1;
            var root = new YogaNode
            {
                AlignContent = AlignType.Stretch, Width = 1080,
                Children =
                {
                    (root_child0 = new YogaNode
                    {
                        Children =
                        {
                            (root_child0_child0 = new YogaNode
                            {
                                AlignContent = AlignType.Stretch,
                                Children =
                                {
                                    (root_child0_child0_child0 = new YogaNode
                                    {
                                        AlignContent = AlignType.Stretch,
                                        Children =
                                        {
                                            (root_child0_child0_child0_child0 = new YogaNode
                                            {
                                                    FlexDirection = FlexDirectionType.Row,
                                                    AlignContent = AlignType.Stretch,
                                                    AlignItems = AlignType.FlexStart,
                                                    Margin = {Start = 36, Top = 24},
                                                Children =
                                                {
                                                    (root_child0_child0_child0_child0_child0 = new YogaNode
                                                    {
                                                            FlexDirection = FlexDirectionType.Row,
                                                            AlignContent = AlignType.Stretch,
                                                        Children =
                                                        {
                                                            (root_child0_child0_child0_child0_child0_child0 = new YogaNode
                                                            {
                                                                AlignContent = AlignType.Stretch, Width = 120, Height = 120,

                                                            })
                                                        }
                                                    }),
                                                    (root_child0_child0_child0_child0_child1 = new YogaNode
                                                    {
                                                        
                                                        
                                                            AlignContent = AlignType.Stretch,
                                                            FlexShrink = 1,
                                                            Margin = {Right = 36},
                                                            Padding = new Edges(36, 21, 36, 18)
                                                        ,
                                                        Children =
                                                        {
                                                            (root_child0_child0_child0_child0_child1_child0 = new YogaNode
                                                            {
                                                                
                                                                
                                                                    FlexDirection = FlexDirectionType.Row,
                                                                    AlignContent = AlignType.Stretch,
                                                                    FlexShrink = 1
                                                                
                                                            }),
                                                            (root_child0_child0_child0_child0_child1_child1 = new YogaNode
                                                            {
                                                                AlignContent = AlignType.Stretch, FlexShrink = 1
                                                            })
                                                        }
                                                    })
                                                }
                                            })
                                        }
                                    }),
                                    (root_child0_child0_child1 = new YogaNode
                                    {
                                        AlignContent = AlignType.Stretch,
                                        Children =
                                        {
                                            (root_child0_child0_child1_child0 = new YogaNode
                                            {
                                                
                                                
                                                    FlexDirection = FlexDirectionType.Row,
                                                    AlignContent = AlignType.Stretch,
                                                    AlignItems = AlignType.FlexStart,
                                                    Margin = {Start = 174, Top = 24},
                                                Children =
                                                {
                                                    (root_child0_child0_child1_child0_child0 = new YogaNode
                                                    {
                                                        
                                                        
                                                            FlexDirection = FlexDirectionType.Row,
                                                            AlignContent = AlignType.Stretch
                                                        ,
                                                        Children =
                                                        {
                                                            (root_child0_child0_child1_child0_child0_child0 = new YogaNode
                                                            {
                                                                AlignContent = AlignType.Stretch, Width = 72, Height = 72
                                                            })
                                                        }
                                                    }),
                                                    (root_child0_child0_child1_child0_child1 = new YogaNode
                                                    {
                                                        
                                                        
                                                            AlignContent = AlignType.Stretch,
                                                            FlexShrink = 1,
                                                            Margin = {Right = 36},
                                                            Padding = new Edges(36, 21, 36, 18)
                                                        ,
                                                        Children =
                                                        {
                                                            (root_child0_child0_child1_child0_child1_child0 = new YogaNode
                                                            {
                                                                FlexDirection = FlexDirectionType.Row,
                                                                AlignContent = AlignType.Stretch,
                                                                FlexShrink = 1
                                                            }),
                                                            (root_child0_child0_child1_child0_child1_child1 = new YogaNode
                                                            {
                                                                AlignContent = AlignType.Stretch, FlexShrink = 1
                                                            })
                                                        }
                                                    })
                                                }
                                            })
                                        }
                                    })
                                }
                            })
                        }
                    })
                }
            };

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1080, root.Layout.Width);
            Assert.AreEqual(240,  root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0.Layout.Width);
            Assert.AreEqual(240,  root_child0.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0_child0.Layout.Width);
            Assert.AreEqual(240,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(144,  root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(36,   root_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(24,   root_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(1044, root_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(120,  root_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(120, root_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(120, root_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(120, root_child0_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(120, root_child0_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(120, root_child0_child0_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0_child0_child0_child0_child1.Layout.Width);
            Assert.AreEqual(39,  root_child0_child0_child0_child0_child1.Layout.Height);

            Assert.AreEqual(36, root_child0_child0_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(21, root_child0_child0_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0_child0_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0_child0_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(36, root_child0_child0_child0_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(21, root_child0_child0_child0_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0_child0_child0_child0_child1_child1.Layout.Width);
            Assert.AreEqual(0,  root_child0_child0_child0_child0_child1_child1.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(144,  root_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0_child0_child1.Layout.Width);
            Assert.AreEqual(96,   root_child0_child0_child1.Layout.Height);

            Assert.AreEqual(174, root_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(24,  root_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(906, root_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child1_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0_child1_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0_child1_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child1_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0_child1_child0_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0_child1_child0_child0_child0.Layout.Height);

            Assert.AreEqual(72, root_child0_child0_child1_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child1.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0_child1_child0_child1.Layout.Width);
            Assert.AreEqual(39, root_child0_child0_child1_child0_child1.Layout.Height);

            Assert.AreEqual(36, root_child0_child0_child1_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(21, root_child0_child0_child1_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child1_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child1_child0.Layout.Height);

            Assert.AreEqual(36, root_child0_child0_child1_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(21, root_child0_child0_child1_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child1_child1.Layout.Width);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child1_child1.Layout.Height);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.RTL);

            Assert.AreEqual(0,    root.Layout.Position.Left);
            Assert.AreEqual(0,    root.Layout.Position.Top);
            Assert.AreEqual(1080, root.Layout.Width);
            Assert.AreEqual(240,  root.Layout.Height);

            Assert.AreEqual(0,    root_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0.Layout.Width);
            Assert.AreEqual(240,  root_child0.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0_child0.Layout.Width);
            Assert.AreEqual(240,  root_child0_child0.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,    root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(144,  root_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(24,   root_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(1044, root_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(120,  root_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(924, root_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(120, root_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(120, root_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(120, root_child0_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(120, root_child0_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(816, root_child0_child0_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0_child0_child0_child0_child1.Layout.Width);
            Assert.AreEqual(39,  root_child0_child0_child0_child0_child1.Layout.Height);

            Assert.AreEqual(36, root_child0_child0_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(21, root_child0_child0_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0_child0_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0_child0_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(36, root_child0_child0_child0_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(21, root_child0_child0_child0_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0_child0_child0_child0_child1_child1.Layout.Width);
            Assert.AreEqual(0,  root_child0_child0_child0_child0_child1_child1.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(144,  root_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0_child0_child1.Layout.Width);
            Assert.AreEqual(96,   root_child0_child0_child1.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(24,  root_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(906, root_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(834, root_child0_child0_child1_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child1_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0_child0_child1_child0_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0_child0_child1_child0_child0.Layout.Height);

            Assert.AreEqual(0,  root_child0_child0_child1_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72, root_child0_child0_child1_child0_child0_child0.Layout.Width);
            Assert.AreEqual(72, root_child0_child0_child1_child0_child0_child0.Layout.Height);

            Assert.AreEqual(726, root_child0_child0_child1_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child1_child0_child1.Layout.Position.Top);
            Assert.AreEqual(72,  root_child0_child0_child1_child0_child1.Layout.Width);
            Assert.AreEqual(39,  root_child0_child0_child1_child0_child1.Layout.Height);

            Assert.AreEqual(36, root_child0_child0_child1_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(21, root_child0_child0_child1_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child1_child0.Layout.Width);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child1_child0.Layout.Height);

            Assert.AreEqual(36, root_child0_child0_child1_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(21, root_child0_child0_child1_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child1_child1.Layout.Width);
            Assert.AreEqual(0,  root_child0_child0_child1_child0_child1_child1.Layout.Height);
        }
    }
}
