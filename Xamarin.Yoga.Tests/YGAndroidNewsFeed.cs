using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGAndroidNewsFeed
    {
        [TestMethod]
        public void android_news_feed()
        {
            YGConfig config = new YGConfig();

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(root, YGAlign.Stretch);
            YGNodeStyleSetWidth(root, 1080);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root, root_child0, 0);

            YGNode root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(root_child0_child0, YGAlign.Stretch);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

            YGNode root_child0_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(root_child0_child0_child0, YGAlign.Stretch);
            YGNodeInsertChild(root_child0_child0, root_child0_child0_child0, 0);

            YGNode root_child0_child0_child0_child0 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(
                root_child0_child0_child0_child0,
                YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root_child0_child0_child0_child0, YGAlign.Stretch);
            YGNodeStyleSetAlignItems(root_child0_child0_child0_child0, YGAlign.FlexStart);
            YGNodeStyleSetMargin(root_child0_child0_child0_child0, YGEdge.Start, 36);
            YGNodeStyleSetMargin(root_child0_child0_child0_child0, YGEdge.Top,   24);
            YGNodeInsertChild(
                root_child0_child0_child0,
                root_child0_child0_child0_child0,
                0);

            YGNode root_child0_child0_child0_child0_child0 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(
                root_child0_child0_child0_child0_child0,
                YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(
                root_child0_child0_child0_child0_child0,
                YGAlign.Stretch);
            YGNodeInsertChild(
                root_child0_child0_child0_child0,
                root_child0_child0_child0_child0_child0,
                0);

            YGNode root_child0_child0_child0_child0_child0_child0 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(
                root_child0_child0_child0_child0_child0_child0,
                YGAlign.Stretch);
            YGNodeStyleSetWidth(root_child0_child0_child0_child0_child0_child0, 120);
            YGNodeStyleSetHeight(root_child0_child0_child0_child0_child0_child0, 120);
            YGNodeInsertChild(
                root_child0_child0_child0_child0_child0,
                root_child0_child0_child0_child0_child0_child0,
                0);

            YGNode root_child0_child0_child0_child0_child1 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(
                root_child0_child0_child0_child0_child1,
                YGAlign.Stretch);
            YGNodeStyleSetFlexShrink(root_child0_child0_child0_child0_child1, 1);
            YGNodeStyleSetMargin(
                root_child0_child0_child0_child0_child1,
                YGEdge.Right,
                36);
            YGNodeStyleSetPadding(
                root_child0_child0_child0_child0_child1,
                YGEdge.Left,
                36);
            YGNodeStyleSetPadding(root_child0_child0_child0_child0_child1, YGEdge.Top, 21);
            YGNodeStyleSetPadding(
                root_child0_child0_child0_child0_child1,
                YGEdge.Right,
                36);
            YGNodeStyleSetPadding(
                root_child0_child0_child0_child0_child1,
                YGEdge.Bottom,
                18);
            YGNodeInsertChild(
                root_child0_child0_child0_child0,
                root_child0_child0_child0_child0_child1,
                1);

            YGNode root_child0_child0_child0_child0_child1_child0 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(
                root_child0_child0_child0_child0_child1_child0,
                YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(
                root_child0_child0_child0_child0_child1_child0,
                YGAlign.Stretch);
            YGNodeStyleSetFlexShrink(root_child0_child0_child0_child0_child1_child0, 1);
            YGNodeInsertChild(
                root_child0_child0_child0_child0_child1,
                root_child0_child0_child0_child0_child1_child0,
                0);

            YGNode root_child0_child0_child0_child0_child1_child1 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(
                root_child0_child0_child0_child0_child1_child1,
                YGAlign.Stretch);
            YGNodeStyleSetFlexShrink(root_child0_child0_child0_child0_child1_child1, 1);
            YGNodeInsertChild(
                root_child0_child0_child0_child0_child1,
                root_child0_child0_child0_child0_child1_child1,
                1);

            YGNode root_child0_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(root_child0_child0_child1, YGAlign.Stretch);
            YGNodeInsertChild(root_child0_child0, root_child0_child0_child1, 1);

            YGNode root_child0_child0_child1_child0 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(
                root_child0_child0_child1_child0,
                YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(root_child0_child0_child1_child0, YGAlign.Stretch);
            YGNodeStyleSetAlignItems(root_child0_child0_child1_child0, YGAlign.FlexStart);
            YGNodeStyleSetMargin(root_child0_child0_child1_child0, YGEdge.Start, 174);
            YGNodeStyleSetMargin(root_child0_child0_child1_child0, YGEdge.Top,   24);
            YGNodeInsertChild(
                root_child0_child0_child1,
                root_child0_child0_child1_child0,
                0);

            YGNode root_child0_child0_child1_child0_child0 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(
                root_child0_child0_child1_child0_child0,
                YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(
                root_child0_child0_child1_child0_child0,
                YGAlign.Stretch);
            YGNodeInsertChild(
                root_child0_child0_child1_child0,
                root_child0_child0_child1_child0_child0,
                0);

            YGNode root_child0_child0_child1_child0_child0_child0 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(
                root_child0_child0_child1_child0_child0_child0,
                YGAlign.Stretch);
            YGNodeStyleSetWidth(root_child0_child0_child1_child0_child0_child0, 72);
            YGNodeStyleSetHeight(root_child0_child0_child1_child0_child0_child0, 72);
            YGNodeInsertChild(
                root_child0_child0_child1_child0_child0,
                root_child0_child0_child1_child0_child0_child0,
                0);

            YGNode root_child0_child0_child1_child0_child1 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(
                root_child0_child0_child1_child0_child1,
                YGAlign.Stretch);
            YGNodeStyleSetFlexShrink(root_child0_child0_child1_child0_child1, 1);
            YGNodeStyleSetMargin(
                root_child0_child0_child1_child0_child1,
                YGEdge.Right,
                36);
            YGNodeStyleSetPadding(
                root_child0_child0_child1_child0_child1,
                YGEdge.Left,
                36);
            YGNodeStyleSetPadding(root_child0_child0_child1_child0_child1, YGEdge.Top, 21);
            YGNodeStyleSetPadding(
                root_child0_child0_child1_child0_child1,
                YGEdge.Right,
                36);
            YGNodeStyleSetPadding(
                root_child0_child0_child1_child0_child1,
                YGEdge.Bottom,
                18);
            YGNodeInsertChild(
                root_child0_child0_child1_child0,
                root_child0_child0_child1_child0_child1,
                1);

            YGNode root_child0_child0_child1_child0_child1_child0 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(
                root_child0_child0_child1_child0_child1_child0,
                YGFlexDirection.Row);
            YGNodeStyleSetAlignContent(
                root_child0_child0_child1_child0_child1_child0,
                YGAlign.Stretch);
            YGNodeStyleSetFlexShrink(root_child0_child0_child1_child0_child1_child0, 1);
            YGNodeInsertChild(
                root_child0_child0_child1_child0_child1,
                root_child0_child0_child1_child0_child1_child0,
                0);

            YGNode root_child0_child0_child1_child0_child1_child1 =
                YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignContent(
                root_child0_child0_child1_child0_child1_child1,
                YGAlign.Stretch);
            YGNodeStyleSetFlexShrink(root_child0_child0_child1_child0_child1_child1, 1);
            YGNodeInsertChild(
                root_child0_child0_child1_child0_child1,
                root_child0_child0_child1_child0_child1_child1,
                1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

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

            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(
                120,
                root_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(
                120,
                root_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(
                120,
                root_child0_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(
                120,
                root_child0_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(
                120,
                root_child0_child0_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(
                72,
                root_child0_child0_child0_child0_child1.Layout.Width);
            Assert.AreEqual(
                39,
                root_child0_child0_child0_child0_child1.Layout.Height);

            Assert.AreEqual(
                36,
                root_child0_child0_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(
                21,
                root_child0_child0_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(
                36,
                root_child0_child0_child0_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(
                21,
                root_child0_child0_child0_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child1_child1.Layout.Width);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child1_child1.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(144,  root_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0_child0_child1.Layout.Width);
            Assert.AreEqual(96,   root_child0_child0_child1.Layout.Height);

            Assert.AreEqual(174, root_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(24,  root_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(906, root_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child0.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child0.Layout.Position.Top);
            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child0.Layout.Width);
            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child0.Layout.Height);

            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child0_child0.Layout.Width);
            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child0_child0.Layout.Height);

            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child1.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child1.Layout.Position.Top);
            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child1.Layout.Width);
            Assert.AreEqual(
                39,
                root_child0_child0_child1_child0_child1.Layout.Height);

            Assert.AreEqual(
                36,
                root_child0_child0_child1_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(
                21,
                root_child0_child0_child1_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child1_child0.Layout.Width);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child1_child0.Layout.Height);

            Assert.AreEqual(
                36,
                root_child0_child0_child1_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(
                21,
                root_child0_child0_child1_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child1_child1.Layout.Width);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child1_child1.Layout.Height);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.RTL);

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

            Assert.AreEqual(
                924,
                root_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(
                120,
                root_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(
                120,
                root_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(
                120,
                root_child0_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(
                120,
                root_child0_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(
                816,
                root_child0_child0_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(
                72,
                root_child0_child0_child0_child0_child1.Layout.Width);
            Assert.AreEqual(
                39,
                root_child0_child0_child0_child0_child1.Layout.Height);

            Assert.AreEqual(
                36,
                root_child0_child0_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(
                21,
                root_child0_child0_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(
                36,
                root_child0_child0_child0_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(
                21,
                root_child0_child0_child0_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child1_child1.Layout.Width);
            Assert.AreEqual(
                0,
                root_child0_child0_child0_child0_child1_child1.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(144,  root_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0_child0_child1.Layout.Width);
            Assert.AreEqual(96,   root_child0_child0_child1.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(24,  root_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(906, root_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(
                834,
                root_child0_child0_child1_child0_child0.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child0.Layout.Position.Top);
            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child0.Layout.Width);
            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child0.Layout.Height);

            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child0_child0.Layout.Width);
            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child0_child0.Layout.Height);

            Assert.AreEqual(
                726,
                root_child0_child0_child1_child0_child1.Layout.Position.Left);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child1.Layout.Position.Top);
            Assert.AreEqual(
                72,
                root_child0_child0_child1_child0_child1.Layout.Width);
            Assert.AreEqual(
                39,
                root_child0_child0_child1_child0_child1.Layout.Height);

            Assert.AreEqual(
                36,
                root_child0_child0_child1_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(
                21,
                root_child0_child0_child1_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child1_child0.Layout.Width);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child1_child0.Layout.Height);

            Assert.AreEqual(
                36,
                root_child0_child0_child1_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(
                21,
                root_child0_child0_child1_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child1_child1.Layout.Width);
            Assert.AreEqual(
                0,
                root_child0_child0_child1_child0_child1_child1.Layout.Height);

            YGNodeFreeRecursive(root);

            
        }
    }
}
