using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    [TestClass]
    public class DefaultValuesTests
    {
        [TestMethod]
        public void assert_default_values()
        {
            var root = new YogaNode();

            Assert.AreEqual(0, root.Children.Count);
            //Assert.AreEqual(null, root.Children[1]);

            Assert.AreEqual(DirectionType.Inherit,     root.Direction);
            Assert.AreEqual(FlexDirectionType.Column,  root.FlexDirection);
            Assert.AreEqual(JustifyType.FlexStart,     root.JustifyContent);
            Assert.AreEqual(AlignType.FlexStart,       root.AlignContent);
            Assert.AreEqual(AlignType.Stretch,         root.AlignItems);
            Assert.AreEqual(AlignType.Auto,            root.AlignSelf);
            Assert.AreEqual(PositionType.Relative,     root.PositionType);
            Assert.AreEqual(WrapType.NoWrap,           root.FlexWrap);
            Assert.AreEqual(OverflowType.Visible,      root.Overflow);
            Assert.AreEqual(null,                      root.FlexGrow);
            Assert.AreEqual(0,                         root.FlexShrink);
            Assert.AreEqual(root.FlexBasis.Unit, ValueUnit.Auto);

            Assert.AreEqual(root.Position.Left.Unit,   ValueUnit.Undefined);
            Assert.AreEqual(root.Position.Top.Unit,    ValueUnit.Undefined);
            Assert.AreEqual(root.Position.Right.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Position.Bottom.Unit, ValueUnit.Undefined);
            Assert.AreEqual(root.Position.Start.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Position.End.Unit,    ValueUnit.Undefined);

            Assert.AreEqual(root.Margin.Left.Unit,   ValueUnit.Undefined);
            Assert.AreEqual(root.Margin.Top.Unit,    ValueUnit.Undefined);
            Assert.AreEqual(root.Margin.Right.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Margin.Bottom.Unit, ValueUnit.Undefined);
            Assert.AreEqual(root.Margin.Start.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Margin.End.Unit,    ValueUnit.Undefined);

            Assert.AreEqual(root.Padding.Left.Unit,   ValueUnit.Undefined);
            Assert.AreEqual(root.Padding.Top.Unit,    ValueUnit.Undefined);
            Assert.AreEqual(root.Padding.Right.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Padding.Bottom.Unit, ValueUnit.Undefined);
            Assert.AreEqual(root.Padding.Start.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.Padding.End.Unit,    ValueUnit.Undefined);

            Assert.IsTrue(root.Border.Left.IsNaN());
            Assert.IsTrue(root.Border.Top.IsNaN());
            Assert.IsTrue(root.Border.Right.IsNaN());
            Assert.IsTrue(root.Border.Bottom.IsNaN());
            Assert.IsTrue(root.Border.Start.IsNaN());
            Assert.IsTrue(root.Border.End.IsNaN());

            Assert.AreEqual(root.Width.Unit,     ValueUnit.Auto);
            Assert.AreEqual(root.Height.Unit,    ValueUnit.Auto);
            Assert.AreEqual(root.MinWidth.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.MinHeight.Unit, ValueUnit.Undefined);
            Assert.AreEqual(root.MaxWidth.Unit,  ValueUnit.Undefined);
            Assert.AreEqual(root.MaxHeight.Unit, ValueUnit.Undefined);

            Assert.AreEqual(0, root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(0, root.Layout.Position.Right);
            Assert.AreEqual(0, root.Layout.Position.Bottom);

            Assert.AreEqual(0, root.Layout.GetMargin(EdgeType.Left));
            Assert.AreEqual(0, root.Layout.GetMargin(EdgeType.Top));
            Assert.AreEqual(0, root.Layout.GetMargin(EdgeType.Right));
            Assert.AreEqual(0, root.Layout.GetMargin(EdgeType.Bottom));

            Assert.AreEqual(0, root.Layout.GetPadding(EdgeType.Left));
            Assert.AreEqual(0, root.Layout.GetPadding(EdgeType.Top));
            Assert.AreEqual(0, root.Layout.GetPadding(EdgeType.Right));
            Assert.AreEqual(0, root.Layout.GetPadding(EdgeType.Bottom));

            Assert.AreEqual(0, root.Layout.GetBorder(EdgeType.Left));
            Assert.AreEqual(0, root.Layout.GetBorder(EdgeType.Top));
            Assert.AreEqual(0, root.Layout.GetBorder(EdgeType.Right));
            Assert.AreEqual(0, root.Layout.GetBorder(EdgeType.Bottom));

            Assert.IsTrue(root.Layout.Width.IsNaN());
            Assert.IsTrue(root.Layout.Height.IsNaN());
            Assert.AreEqual(DirectionType.Inherit, root.Layout.Direction);
        }

        [TestMethod]
        public void assert_webdefault_values()
        {
            var config = new YogaConfig {UseWebDefaults = true};
            var root   = new YogaNode(config);

            Assert.AreEqual(FlexDirectionType.Row, root.FlexDirection);
            Assert.AreEqual(AlignType.Stretch,     root.AlignContent);
            Assert.AreEqual(1.0f,                  root.FlexShrink);
        }

        [TestMethod]
        public void assert_webdefault_values_reset()
        {
            var config = new YogaConfig {UseWebDefaults = true};

            var root = new YogaNode(config);

            Assert.AreEqual(FlexDirectionType.Row, root.FlexDirection);
            Assert.AreEqual(AlignType.Stretch,     root.AlignContent);
            Assert.AreEqual(1.0f,                  root.FlexShrink);
        }
    }
}
