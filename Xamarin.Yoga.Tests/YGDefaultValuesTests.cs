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

            Assert.AreEqual(root.Style.Position.Left.unit,   YGUnit.Undefined);
            Assert.AreEqual(root.Style.Position.Top.unit,    YGUnit.Undefined);
            Assert.AreEqual(root.Style.Position.Right.unit,  YGUnit.Undefined);
            Assert.AreEqual(root.Style.Position.Bottom.unit, YGUnit.Undefined);
            Assert.AreEqual(root.Style.Position.Start.unit,  YGUnit.Undefined);
            Assert.AreEqual(root.Style.Position.End.unit,    YGUnit.Undefined);

            Assert.AreEqual(root.Style.Margin.Left.unit,   YGUnit.Undefined);
            Assert.AreEqual(root.Style.Margin.Top.unit,    YGUnit.Undefined);
            Assert.AreEqual(root.Style.Margin.Right.unit,  YGUnit.Undefined);
            Assert.AreEqual(root.Style.Margin.Bottom.unit, YGUnit.Undefined);
            Assert.AreEqual(root.Style.Margin.Start.unit,  YGUnit.Undefined);
            Assert.AreEqual(root.Style.Margin.End.unit,    YGUnit.Undefined);

            Assert.AreEqual(root.Style.Padding.Left.unit,   YGUnit.Undefined);
            Assert.AreEqual(root.Style.Padding.Top.unit,    YGUnit.Undefined);
            Assert.AreEqual(root.Style.Padding.Right.unit,  YGUnit.Undefined);
            Assert.AreEqual(root.Style.Padding.Bottom.unit, YGUnit.Undefined);
            Assert.AreEqual(root.Style.Padding.Start.unit,  YGUnit.Undefined);
            Assert.AreEqual(root.Style.Padding.End.unit,    YGUnit.Undefined);

            Assert.IsTrue(root.Style.Border.Left.IsNaN());
            Assert.IsTrue(root.Style.Border.Top.IsNaN());
            Assert.IsTrue(root.Style.Border.Right.IsNaN());
            Assert.IsTrue(root.Style.Border.Bottom.IsNaN());
            Assert.IsTrue(root.Style.Border.Start.IsNaN());
            Assert.IsTrue(root.Style.Border.End.IsNaN());

            Assert.AreEqual(root.Style.Width.unit,  YGUnit.Auto);
            Assert.AreEqual(root.Style.Height.unit, YGUnit.Auto);
            Assert.AreEqual(root.Style.MinWidth.unit,   YGUnit.Undefined);
            Assert.AreEqual(root.Style.MinHeight.unit,  YGUnit.Undefined);
            Assert.AreEqual(root.Style.MaxWidth.unit,   YGUnit.Undefined);
            Assert.AreEqual(root.Style.MaxHeight.unit,  YGUnit.Undefined);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Position.Right);
            Assert.AreEqual(0, root.Layout.Position.Bottom);

            Assert.AreEqual(0, root.LayoutGetMargin(YGEdge.Left));
            Assert.AreEqual(0, root.LayoutGetMargin(YGEdge.Top));
            Assert.AreEqual(0, root.LayoutGetMargin(YGEdge.Right));
            Assert.AreEqual(0, root.LayoutGetMargin(YGEdge.Bottom));

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
            Assert.AreEqual(1.0f,                root.Style.FlexShrink);
        }

        [TestMethod]
        public void assert_webdefault_values_reset()
        {
            var config = new YGConfig {UseWebDefaults = true};

            YGNode root = new YGNode(config);

            Assert.AreEqual(YGFlexDirection.Row, root.Style.FlexDirection);
            Assert.AreEqual(YGAlign.Stretch,     root.Style.AlignContent);
            Assert.AreEqual(1.0f,                root.Style.FlexShrink);
        }
    }
}
