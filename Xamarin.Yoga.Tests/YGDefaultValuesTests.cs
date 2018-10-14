﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    [TestClass]
    public class YGDefaultValuesTests
    {
        [TestMethod]
        public void assert_default_values()
        {
            YGNodeRef root = YGNodeNew();

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

            Assert.IsTrue(YGFloatIsUndefined(YGNodeStyleGetBorder(root, YGEdge.Left)));
            Assert.IsTrue(YGFloatIsUndefined(YGNodeStyleGetBorder(root, YGEdge.Top)));
            Assert.IsTrue(YGFloatIsUndefined(YGNodeStyleGetBorder(root, YGEdge.Right)));
            Assert.IsTrue(YGFloatIsUndefined(YGNodeStyleGetBorder(root, YGEdge.Bottom)));
            Assert.IsTrue(YGFloatIsUndefined(YGNodeStyleGetBorder(root, YGEdge.Start)));
            Assert.IsTrue(YGFloatIsUndefined(YGNodeStyleGetBorder(root, YGEdge.End)));

            Assert.AreEqual(YGNodeStyleGetWidth(root).unit,     YGUnit.Auto);
            Assert.AreEqual(YGNodeStyleGetHeight(root).unit,    YGUnit.Auto);
            Assert.AreEqual(YGNodeStyleGetMinWidth(root).unit,  YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetMinHeight(root).unit, YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetMaxWidth(root).unit,  YGUnit.Undefined);
            Assert.AreEqual(YGNodeStyleGetMaxHeight(root).unit, YGUnit.Undefined);

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

            Assert.IsTrue(YGFloatIsUndefined(root.Layout.Width));
            Assert.IsTrue(YGFloatIsUndefined(root.Layout.Height));
            Assert.AreEqual(YGDirection.Inherit, root.Layout.Direction);

            YGNodeFreeRecursive(root);
        }

        [TestMethod]
        public void assert_webdefault_values()
        {
            var config = YGConfigNew();
            YGConfigSetUseWebDefaults(config, true);
            YGNodeRef root = YGNodeNewWithConfig(config);

            Assert.AreEqual(YGFlexDirection.Row, YGNodeStyleGetFlexDirection(root));
            Assert.AreEqual(YGAlign.Stretch,     YGNodeStyleGetAlignContent(root));
            Assert.AreEqual(1.0f, YGNodeStyleGetFlexShrink(root));

            YGNodeFreeRecursive(root);
            YGConfigFree(config);
        }

        [TestMethod]
        public void assert_webdefault_values_reset()
        {
            var config = YGConfigNew();
            YGConfigSetUseWebDefaults(config, true);
            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeReset(root);

            Assert.AreEqual(YGFlexDirection.Row, YGNodeStyleGetFlexDirection(root));
            Assert.AreEqual(YGAlign.Stretch,     YGNodeStyleGetAlignContent(root));
            Assert.AreEqual(1.0f, YGNodeStyleGetFlexShrink(root));

            YGNodeFreeRecursive(root);
            YGConfigFree(config);
        }

        [TestMethod]
        public void assert_legacy_stretch_behaviour()
        {
            var config = YGConfigNew();
            YGConfigSetUseLegacyStretchBehaviour(config, true);
            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 500);
            YGNodeStyleSetHeight(root, 500);

            YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetAlignItems(root_child0, YGAlign.FlexStart);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0_child0, 1);
            YGNodeStyleSetFlexShrink(root_child0_child0, 1);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

            YGNodeRef root_child0_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0_child0_child0, 1);
            YGNodeStyleSetFlexShrink(root_child0_child0_child0, 1);
            YGNodeInsertChild(root_child0_child0, root_child0_child0_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0,   root.Layout.Position.Left);
            Assert.AreEqual(0, root.Layout.Position.Top);
            Assert.AreEqual(500, root.Layout.Width);
            Assert.AreEqual(500, root.Layout.Height);

            Assert.AreEqual(0,   root_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0.Layout.Position.Top);
            Assert.AreEqual(500, root_child0.Layout.Width);
            Assert.AreEqual(500, root_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0_child0.Layout.Width);
            Assert.AreEqual(500, root_child0_child0.Layout.Height);

            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Left);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Position.Top);
            Assert.AreEqual(0,   root_child0_child0_child0.Layout.Width);
            Assert.AreEqual(500, root_child0_child0_child0.Layout.Height);

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }
    }
}