using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;


    [TestClass]
    public class YGDefaultValuesTests
    {
        [TestMethod]
        public void assert_default_values()
        {
            YGNode root = new YGNode();

            Assert.AreEqual(0, root.Children.Count);
            //Assert.AreEqual(null, root.Children[1]);

            Assert.AreEqual(YGDirection.Inherit,       root.Style.Direction);
            Assert.AreEqual(YGFlexDirection.Column,    root.Style.FlexDirection);
            Assert.AreEqual(YGJustify.FlexStart,       root.Style.JustifyContent);
            Assert.AreEqual(YGAlign.FlexStart,         root.Style.AlignContent);
            Assert.AreEqual(YGAlign.Stretch,           root.Style.AlignItems);
            Assert.AreEqual(YGAlign.Auto,              root.Style.AlignSelf);
            Assert.AreEqual(YGPositionType.Relative,   root.Style.PositionType);
            Assert.AreEqual(YGWrap.NoWrap,             root.Style.FlexWrap);
            Assert.AreEqual(YGOverflow.Visible,        root.Style.Overflow);
            Assert.AreEqual(null,                      root.Style.FlexGrow);
            Assert.AreEqual(0,                         root.Style.FlexShrink);
            Assert.AreEqual(root.Style.FlexBasis.unit, YGUnit.Auto);

            Assert.AreEqual(root.StyleGetPosition(YGEdge.Left).unit,   YGUnit.Undefined);
            Assert.AreEqual(root.StyleGetPosition(YGEdge.Top).unit,    YGUnit.Undefined);
            Assert.AreEqual(root.StyleGetPosition(YGEdge.Right).unit,  YGUnit.Undefined);
            Assert.AreEqual(root.StyleGetPosition(YGEdge.Bottom).unit, YGUnit.Undefined);
            Assert.AreEqual(root.StyleGetPosition(YGEdge.Start).unit,  YGUnit.Undefined);
            Assert.AreEqual(root.StyleGetPosition(YGEdge.End).unit,    YGUnit.Undefined);

            Assert.AreEqual(root.StyleGetMargin(YGEdge.Left).unit,   YGUnit.Undefined);
            Assert.AreEqual(root.StyleGetMargin(YGEdge.Top).unit,    YGUnit.Undefined);
            Assert.AreEqual(root.StyleGetMargin(YGEdge.Right).unit,  YGUnit.Undefined);
            Assert.AreEqual(root.StyleGetMargin(YGEdge.Bottom).unit, YGUnit.Undefined);
            Assert.AreEqual(root.StyleGetMargin(YGEdge.Start).unit,  YGUnit.Undefined);
            Assert.AreEqual(root.StyleGetMargin(YGEdge.End).unit,    YGUnit.Undefined);

            Assert.AreEqual(YGNodeStyleGetPadding(root, YGEdge.Left).unit,   YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetPadding(root, YGEdge.Top).unit,    YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetPadding(root, YGEdge.Right).unit,  YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetPadding(root, YGEdge.Bottom).unit, YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetPadding(root, YGEdge.Start).unit,  YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetPadding(root, YGEdge.End).unit,    YGUnit.Undefined);

            Assert.IsTrue(YGNodeStyleGetBorder(root, YGEdge.Left).IsNaN());
            Assert.IsTrue(YGNodeStyleGetBorder(root, YGEdge.Top).IsNaN());
            Assert.IsTrue(YGNodeStyleGetBorder(root, YGEdge.Right).IsNaN());
            Assert.IsTrue(YGNodeStyleGetBorder(root, YGEdge.Bottom).IsNaN());
            Assert.IsTrue(YGNodeStyleGetBorder(root, YGEdge.Start).IsNaN());
            Assert.IsTrue(YGNodeStyleGetBorder(root, YGEdge.End).IsNaN());

            Assert.AreEqual(YGNodeStyleGetWidth(root).unit,       YGUnit.Auto);
            Assert.AreEqual(YGNodeStyleGetHeight(root).unit,      YGUnit.Auto);
            Assert.AreEqual(root.Style.MinDimensions.Width.unit,  YGUnit.Undefined);
            Assert.AreEqual(root.Style.MinDimensions.Height.unit, YGUnit.Undefined);
            Assert.AreEqual(root.Style.MaxDimensions.Width.unit,  YGUnit.Undefined);
            Assert.AreEqual(root.Style.MaxDimensions.Height.unit, YGUnit.Undefined);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Position.Right);
            Assert.AreEqual(0, root.Layout.Position.Bottom);

            Assert.AreEqual(0, YGNodeLayoutGetMargin(root, YGEdge.Left));
            Assert.AreEqual(0, YGNodeLayoutGetMargin(root, YGEdge.Top));
            Assert.AreEqual(0, YGNodeLayoutGetMargin(root, YGEdge.Right));
            Assert.AreEqual(0, YGNodeLayoutGetMargin(root, YGEdge.Bottom));

            Assert.AreEqual(0, YGNodeLayoutGetPadding(root, YGEdge.Left));
            Assert.AreEqual(0, YGNodeLayoutGetPadding(root, YGEdge.Top));
            Assert.AreEqual(0, YGNodeLayoutGetPadding(root, YGEdge.Right));
            Assert.AreEqual(0, YGNodeLayoutGetPadding(root, YGEdge.Bottom));

            Assert.AreEqual(0, YGNodeLayoutGetBorder(root, YGEdge.Left));
            Assert.AreEqual(0, YGNodeLayoutGetBorder(root, YGEdge.Top));
            Assert.AreEqual(0, YGNodeLayoutGetBorder(root, YGEdge.Right));
            Assert.AreEqual(0, YGNodeLayoutGetBorder(root, YGEdge.Bottom));

            Assert.IsTrue(root.Layout.Width.IsNaN());
            Assert.IsTrue(root.Layout.Height.IsNaN());
            Assert.AreEqual(YGDirection.Inherit, root.Layout.Direction);
        }

        [TestMethod]
        public void assert_webdefault_values()
        {
            var    config = new YGConfig {UseWebDefaults = true};
            YGNode root   = new YGNode(config);

            Assert.AreEqual(YGFlexDirection.Row, root.Style.FlexDirection);
            Assert.AreEqual(YGAlign.Stretch,     root.Style.AlignContent);
            Assert.AreEqual(1.0f,                YGNodeStyleGetFlexShrink(root));
        }

        [TestMethod]
        public void assert_webdefault_values_reset()
        {
            var config = new YGConfig {UseWebDefaults = true};

            YGNode root = new YGNode(config);

            Assert.AreEqual(YGFlexDirection.Row, root.Style.FlexDirection);
            Assert.AreEqual(YGAlign.Stretch,     root.Style.AlignContent);
            Assert.AreEqual(1.0f,                YGNodeStyleGetFlexShrink(root));
        }
    }
}
