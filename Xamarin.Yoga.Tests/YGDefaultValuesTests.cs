﻿using System;
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
            YGNode root = YGNodeNew();

            Assert.AreEqual(0,    YGNodeGetChildCount(root));
            Assert.AreEqual(null, YGNodeGetChild(root, 1));

            Assert.AreEqual(YGDirection.Inherit,     YGNodeStyleGetDirection(root));
            Assert.AreEqual(YGFlexDirection.Column,  YGNodeStyleGetFlexDirection(root));
            Assert.AreEqual(YGJustify.FlexStart,     YGNodeStyleGetJustifyContent(root));
            Assert.AreEqual(YGAlign.FlexStart,       YGNodeStyleGetAlignContent(root));
            Assert.AreEqual(YGAlign.Stretch,         YGNodeStyleGetAlignItems(root));
            Assert.AreEqual(YGAlign.Auto,            YGNodeStyleGetAlignSelf(root));
            Assert.AreEqual(YGPositionType.Relative, YGNodeStyleGetPositionType(root));
            Assert.AreEqual(YGWrap.NoWrap,           YGNodeStyleGetFlexWrap(root));
            Assert.AreEqual(YGOverflow.Visible,      YGNodeStyleGetOverflow(root));
            Assert.AreEqual(0, YGNodeStyleGetFlexGrow(root));
            Assert.AreEqual(0, YGNodeStyleGetFlexShrink(root));
            Assert.AreEqual(YGNodeStyleGetFlexBasis(root).unit, YGUnit.Auto);

            Assert.AreEqual(YGNodeStyleGetPosition(root, YGEdge.Left).unit,   YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetPosition(root, YGEdge.Top).unit,    YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetPosition(root, YGEdge.Right).unit,  YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetPosition(root, YGEdge.Bottom).unit, YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetPosition(root, YGEdge.Start).unit,  YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetPosition(root, YGEdge.End).unit,    YGUnit.Undefined);

            Assert.AreEqual(YGNodeStyleGetMargin(root, YGEdge.Left).unit,   YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetMargin(root, YGEdge.Top).unit,    YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetMargin(root, YGEdge.Right).unit,  YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetMargin(root, YGEdge.Bottom).unit, YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetMargin(root, YGEdge.Start).unit,  YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetMargin(root, YGEdge.End).unit,    YGUnit.Undefined);

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

            Assert.AreEqual(YGNodeStyleGetWidth(root).unit,     YGUnit.Auto);
            Assert.AreEqual(YGNodeStyleGetHeight(root).unit,    YGUnit.Auto);
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

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void assert_webdefault_values()
        {
            var config = new YGConfig {UseWebDefaults = true};
            YGNode root = YGNodeNewWithConfig(config);

            Assert.AreEqual(YGFlexDirection.Row, YGNodeStyleGetFlexDirection(root));
            Assert.AreEqual(YGAlign.Stretch,     YGNodeStyleGetAlignContent(root));
            Assert.AreEqual(1.0f, YGNodeStyleGetFlexShrink(root));

            YGNodeFreeRecursive(root);
            
        }

        [TestMethod]
        public void assert_webdefault_values_reset()
        {
            var config = new YGConfig { UseWebDefaults = true };

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeReset(root);

            Assert.AreEqual(YGFlexDirection.Row, YGNodeStyleGetFlexDirection(root));
            Assert.AreEqual(YGAlign.Stretch,     YGNodeStyleGetAlignContent(root));
            Assert.AreEqual(1.0f, YGNodeStyleGetFlexShrink(root));

            YGNodeFreeRecursive(root);
        }
    }
}
