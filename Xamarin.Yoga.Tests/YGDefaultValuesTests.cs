using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    
    using static YogaConst;


    [TestClass]
    public class YGDefaultValuesTests
    {
        [TestMethod]
        public void assert_default_values()
        {
            YogaNode root = new YogaNode();

            Assert.AreEqual(0, root.Children.Count);
            //Assert.AreEqual(null, root.Children[1]);

            Assert.AreEqual(DirectionType.Inherit,       root.Style.Direction);
            Assert.AreEqual(FlexDirectionType.Column,    root.Style.FlexDirection);
            Assert.AreEqual(JustifyType.FlexStart,       root.Style.JustifyContent);
            Assert.AreEqual(AlignType.FlexStart,         root.Style.AlignContent);
            Assert.AreEqual(AlignType.Stretch,           root.Style.AlignItems);
            Assert.AreEqual(AlignType.Auto,              root.Style.AlignSelf);
            Assert.AreEqual(PositionType.Relative,   root.Style.PositionType);
            Assert.AreEqual(WrapType.NoWrap,             root.Style.FlexWrap);
            Assert.AreEqual(OverflowType.Visible,        root.Style.Overflow);
            Assert.AreEqual(null,                      root.Style.FlexGrow);
            Assert.AreEqual(0,                         root.Style.FlexShrink);
            Assert.AreEqual(root.Style.FlexBasis.Unit, ValueUnit.Auto);

            Assert.AreEqual(root.Style.Position.Left.Unit,   ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Position.Top.Unit,    ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Position.Right.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Position.Bottom.Unit, ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Position.Start.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Position.End.Unit,    ValueUnit.Undefined);

            Assert.AreEqual(root.Style.Margin.Left.Unit,   ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Margin.Top.Unit,    ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Margin.Right.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Margin.Bottom.Unit, ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Margin.Start.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Margin.End.Unit,    ValueUnit.Undefined);

            Assert.AreEqual(root.Style.Padding.Left.Unit,   ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Padding.Top.Unit,    ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Padding.Right.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Padding.Bottom.Unit, ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Padding.Start.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Style.Padding.End.Unit,    ValueUnit.Undefined);

            Assert.IsTrue(root.Style.Border.Left.IsNaN());
            Assert.IsTrue(root.Style.Border.Top.IsNaN());
            Assert.IsTrue(root.Style.Border.Right.IsNaN());
            Assert.IsTrue(root.Style.Border.Bottom.IsNaN());
            Assert.IsTrue(root.Style.Border.Start.IsNaN());
            Assert.IsTrue(root.Style.Border.End.IsNaN());

            Assert.AreEqual(root.Style.Width.Unit,  ValueUnit.Auto);
            Assert.AreEqual(root.Style.Height.Unit, ValueUnit.Auto);
            Assert.AreEqual(root.Style.MinWidth.Unit,   ValueUnit.Undefined);
            Assert.AreEqual(root.Style.MinHeight.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Style.MaxWidth.Unit,   ValueUnit.Undefined);
            Assert.AreEqual(root.Style.MaxHeight.Unit,  ValueUnit.Undefined);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Position.Right);
            Assert.AreEqual(0, root.Layout.Position.Bottom);

            Assert.AreEqual(0, root.Layout.GetMargin(EdgeType.Left));
            Assert.AreEqual(0, root.Layout.GetMargin(EdgeType.Top));
            Assert.AreEqual(0, root.Layout.GetMargin(EdgeType.Right));
            Assert.AreEqual(0, root.Layout.GetMargin(EdgeType.Bottom));

            Assert.AreEqual(0, root.Layout.YGNodeLayoutGetPadding(EdgeType.Left));
            Assert.AreEqual(0, root.Layout.YGNodeLayoutGetPadding(EdgeType.Top));
            Assert.AreEqual(0, root.Layout.YGNodeLayoutGetPadding(EdgeType.Right));
            Assert.AreEqual(0, root.Layout.YGNodeLayoutGetPadding(EdgeType.Bottom));

            Assert.AreEqual(0, root.Layout.YGNodeLayoutGetBorder(EdgeType.Left));
            Assert.AreEqual(0, root.Layout.YGNodeLayoutGetBorder(EdgeType.Top));
            Assert.AreEqual(0, root.Layout.YGNodeLayoutGetBorder(EdgeType.Right));
            Assert.AreEqual(0, root.Layout.YGNodeLayoutGetBorder(EdgeType.Bottom));

            Assert.IsTrue(root.Layout.Width.IsNaN());
            Assert.IsTrue(root.Layout.Height.IsNaN());
            Assert.AreEqual(DirectionType.Inherit, root.Layout.Direction);
        }

        [TestMethod]
        public void assert_webdefault_values()
        {
            var    config = new YogaConfig {UseWebDefaults = true};
            YogaNode root   = new YogaNode(config);

            Assert.AreEqual(FlexDirectionType.Row, root.Style.FlexDirection);
            Assert.AreEqual(AlignType.Stretch,     root.Style.AlignContent);
            Assert.AreEqual(1.0f,                root.Style.FlexShrink);
        }

        [TestMethod]
        public void assert_webdefault_values_reset()
        {
            var config = new YogaConfig {UseWebDefaults = true};

            YogaNode root = new YogaNode(config);

            Assert.AreEqual(FlexDirectionType.Row, root.Style.FlexDirection);
            Assert.AreEqual(AlignType.Stretch,     root.Style.AlignContent);
            Assert.AreEqual(1.0f,                root.Style.FlexShrink);
        }
    }
}
