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

            YGNode root = new YGNode(config);
            root.StyleSetAlignContent(YGAlign.Stretch);
            YGNodeStyleSetWidth(root, 1080);

            YGNode root_child0 = new YGNode(config);
            root.InsertChild(root_child0);

            YGNode root_child0_child0 = new YGNode(config);
            root_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            root_child0.InsertChild(root_child0_child0);

            YGNode root_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0.InsertChild(root_child0_child0_child0);

            YGNode root_child0_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child0_child0.StyleSetAlignItems(YGAlign.FlexStart);
            root_child0_child0_child0_child0.StyleSetMargin(YGEdge.Start, 36);
            root_child0_child0_child0_child0.StyleSetMargin(YGEdge.Top,   24);
            root_child0_child0_child0.InsertChild(root_child0_child0_child0_child0);

            YGNode root_child0_child0_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0_child0_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0_child0_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child0_child0.InsertChild(root_child0_child0_child0_child0_child0);

            YGNode root_child0_child0_child0_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child0_child0_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            YGNodeStyleSetWidth(root_child0_child0_child0_child0_child0_child0, 120);
            YGNodeStyleSetHeight(root_child0_child0_child0_child0_child0_child0, 120);
            root_child0_child0_child0_child0_child0.InsertChild(root_child0_child0_child0_child0_child0_child0);

            YGNode root_child0_child0_child0_child0_child1 = new YGNode(config);
            root_child0_child0_child0_child0_child1.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child0_child0_child1.StyleSetFlexShrink(1);
            root_child0_child0_child0_child0_child1.StyleSetMargin(YGEdge.Right, 36);
            root_child0_child0_child0_child0_child1.StyleSetPadding(new Edges(36, 21, 36, 18));
            root_child0_child0_child0_child0.InsertChild(1, root_child0_child0_child0_child0_child1);

            YGNode root_child0_child0_child0_child0_child1_child0 = new YGNode(config);
            root_child0_child0_child0_child0_child1_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0_child0_child0_child1_child0.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child0_child0_child1_child0.StyleSetFlexShrink(1);
            root_child0_child0_child0_child0_child1.InsertChild(root_child0_child0_child0_child0_child1_child0);

            YGNode root_child0_child0_child0_child0_child1_child1 = new YGNode(config);
            root_child0_child0_child0_child0_child1_child1.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child0_child0_child1_child1.StyleSetFlexShrink(1);
            root_child0_child0_child0_child0_child1.InsertChild(1, root_child0_child0_child0_child0_child1_child1);

            YGNode root_child0_child0_child1 = new YGNode(config);
            root_child0_child0_child1.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0.InsertChild(1, root_child0_child0_child1);

            YGNode root_child0_child0_child1_child0 = new YGNode(config);
            root_child0_child0_child1_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0_child1_child0.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child1_child0.StyleSetAlignItems(YGAlign.FlexStart);
            root_child0_child0_child1_child0.StyleSetMargin(YGEdge.Start, 174);
            root_child0_child0_child1_child0.StyleSetMargin(YGEdge.Top,   24);
            root_child0_child0_child1.InsertChild(root_child0_child0_child1_child0);

            YGNode root_child0_child0_child1_child0_child0 = new YGNode(config);
            root_child0_child0_child1_child0_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0_child1_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child1_child0.InsertChild(root_child0_child0_child1_child0_child0);

            YGNode root_child0_child0_child1_child0_child0_child0 = new YGNode(config);
            root_child0_child0_child1_child0_child0_child0.StyleSetAlignContent(YGAlign.Stretch);
            YGNodeStyleSetWidth(root_child0_child0_child1_child0_child0_child0, 72);
            YGNodeStyleSetHeight(root_child0_child0_child1_child0_child0_child0, 72);
            root_child0_child0_child1_child0_child0.InsertChild(
                root_child0_child0_child1_child0_child0_child0);

            YGNode root_child0_child0_child1_child0_child1 = new YGNode(config);
            root_child0_child0_child1_child0_child1.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child1_child0_child1.StyleSetFlexShrink(1);
            root_child0_child0_child1_child0_child1.StyleSetMargin(YGEdge.Right, 36);
            root_child0_child0_child1_child0_child1.StyleSetPadding(new Edges(36, 21, 36, 18));

            root_child0_child0_child1_child0.InsertChild(1, root_child0_child0_child1_child0_child1);

            YGNode root_child0_child0_child1_child0_child1_child0 = new YGNode(config);
            root_child0_child0_child1_child0_child1_child0.StyleSetFlexDirection(YGFlexDirection.Row);
            root_child0_child0_child1_child0_child1_child0.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child1_child0_child1_child0.StyleSetFlexShrink(1);
            root_child0_child0_child1_child0_child1.InsertChild(root_child0_child0_child1_child0_child1_child0);

            YGNode root_child0_child0_child1_child0_child1_child1 = new YGNode(config);
            root_child0_child0_child1_child0_child1_child1.StyleSetAlignContent(YGAlign.Stretch);
            root_child0_child0_child1_child0_child1_child1.StyleSetFlexShrink(1);
            root_child0_child0_child1_child0_child1.InsertChild(1, root_child0_child0_child1_child0_child1_child1);
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

            Assert.AreEqual(0,root_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(120,root_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(120,root_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,root_child0_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(120,root_child0_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(120,root_child0_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(120,root_child0_child0_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(72,root_child0_child0_child0_child0_child1.Layout.Width);
            Assert.AreEqual(39,root_child0_child0_child0_child0_child1.Layout.Height);

            Assert.AreEqual(36,root_child0_child0_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(21,root_child0_child0_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(36,root_child0_child0_child0_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(21,root_child0_child0_child0_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child1_child1.Layout.Width);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child1_child1.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(144,  root_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0_child0_child1.Layout.Width);
            Assert.AreEqual(96,   root_child0_child0_child1.Layout.Height);

            Assert.AreEqual(174, root_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(24,  root_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(906, root_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(0,root_child0_child0_child1_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72,root_child0_child0_child1_child0_child0.Layout.Width);
            Assert.AreEqual(72,root_child0_child0_child1_child0_child0.Layout.Height);

            Assert.AreEqual(0,root_child0_child0_child1_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72,root_child0_child0_child1_child0_child0_child0.Layout.Width);
            Assert.AreEqual(72,root_child0_child0_child1_child0_child0_child0.Layout.Height);

            Assert.AreEqual(72,root_child0_child0_child1_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child1.Layout.Position.Top);
            Assert.AreEqual(72,root_child0_child0_child1_child0_child1.Layout.Width);
            Assert.AreEqual(39,root_child0_child0_child1_child0_child1.Layout.Height);

            Assert.AreEqual(36,root_child0_child0_child1_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(21,root_child0_child0_child1_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child1_child0.Layout.Width);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child1_child0.Layout.Height);

            Assert.AreEqual(36,root_child0_child0_child1_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(21,root_child0_child0_child1_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child1_child1.Layout.Width);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child1_child1.Layout.Height);

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

            Assert.AreEqual(924,root_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(120,root_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(120,root_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(0,root_child0_child0_child0_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(120,root_child0_child0_child0_child0_child0_child0.Layout.Width);
            Assert.AreEqual(120,root_child0_child0_child0_child0_child0_child0.Layout.Height);

            Assert.AreEqual(816,root_child0_child0_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(72,root_child0_child0_child0_child0_child1.Layout.Width);
            Assert.AreEqual(39,root_child0_child0_child0_child0_child1.Layout.Height);

            Assert.AreEqual(36,root_child0_child0_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(21,root_child0_child0_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(36,root_child0_child0_child0_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(21,root_child0_child0_child0_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child1_child1.Layout.Width);
            Assert.AreEqual(0,root_child0_child0_child0_child0_child1_child1.Layout.Height);

            Assert.AreEqual(0,    root_child0_child0_child1.Layout.Position.Left);
            Assert.AreEqual(144,  root_child0_child0_child1.Layout.Position.Top);
            Assert.AreEqual(1080, root_child0_child0_child1.Layout.Width);
            Assert.AreEqual(96,   root_child0_child0_child1.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(24,  root_child0_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(906, root_child0_child0_child1_child0.Layout.Width);
            Assert.AreEqual(72,  root_child0_child0_child1_child0.Layout.Height);

            Assert.AreEqual(834,root_child0_child0_child1_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72,root_child0_child0_child1_child0_child0.Layout.Width);
            Assert.AreEqual(72,root_child0_child0_child1_child0_child0.Layout.Height);

            Assert.AreEqual(0,root_child0_child0_child1_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(72,root_child0_child0_child1_child0_child0_child0.Layout.Width);
            Assert.AreEqual(72,root_child0_child0_child1_child0_child0_child0.Layout.Height);

            Assert.AreEqual(726,root_child0_child0_child1_child0_child1.Layout.Position.Left);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child1.Layout.Position.Top);
            Assert.AreEqual(72,root_child0_child0_child1_child0_child1.Layout.Width);
            Assert.AreEqual(39,root_child0_child0_child1_child0_child1.Layout.Height);

            Assert.AreEqual(36,root_child0_child0_child1_child0_child1_child0.Layout.Position.Left);
            Assert.AreEqual(21,root_child0_child0_child1_child0_child1_child0.Layout.Position.Top);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child1_child0.Layout.Width);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child1_child0.Layout.Height);

            Assert.AreEqual(36,root_child0_child0_child1_child0_child1_child1.Layout.Position.Left);
            Assert.AreEqual(21,root_child0_child0_child1_child0_child1_child1.Layout.Position.Top);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child1_child1.Layout.Width);
            Assert.AreEqual(0,root_child0_child0_child1_child0_child1_child1.Layout.Height);
        }
    }
}
