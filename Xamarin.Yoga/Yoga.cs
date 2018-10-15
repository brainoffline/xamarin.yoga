using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming
#pragma warning disable 414
#pragma warning disable 169

namespace Xamarin.Yoga
{
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;
    using static YGConst;

    public delegate YGSize YGMeasureFunc(
        YGNodeRef     node,
        float         width,
        YGMeasureMode widthMode,
        float         height,
        YGMeasureMode heightMode);

    public delegate float YGBaselineFunc(YGNodeRef node, float width, float height);

    public delegate void YGDirtiedFunc(YGNodeRef node);

    public delegate void YGPrintFunc(YGNodeRef node);

    public delegate void YGLogger(
        YGConfigRef     config,
        YGNodeRef       node,
        YGLogLevel      level,
        string          format,
        params object[] args);

    public static partial class YGGlobal
    {
        public static void YGDefaultLog(
            YGConfigRef     config,
            YGNodeRef       node,
            YGLogLevel      level,
            string          format,
            params object[] args)
        {
            //YG_UNUSED(config);
            //YG_UNUSED(node);
            switch (level)
            {
            case YGLogLevel.Error:
            case YGLogLevel.Fatal:
                Console.Error.WriteLine(format, args);
                return;

            case YGLogLevel.Warn:
            case YGLogLevel.Info:
            case YGLogLevel.Debug:
            case YGLogLevel.Verbose:
            default:
                Console.WriteLine(format, args);
                return;
            }
        }

        public static YGBaselineFunc YGNodeGetBaselineFunc(YGNodeRef node)
        {
            return node.getBaseline();
        }

        public static void YGNodeSetBaselineFunc(YGNodeRef node, YGBaselineFunc baselineFunc)
        {
            node.setBaseLineFunc(baselineFunc);
        }

        public static YGDirtiedFunc YGNodeGetDirtiedFunc(YGNodeRef node)
        {
            return node.getDirtied();
        }

        public static void YGNodeSetPrintFunc(YGNodeRef node, YGPrintFunc printFunc)
        {
            node.setPrintFunc(printFunc);
        }

        public static bool YGNodeGetHasNewLayout(YGNodeRef node)
        {
            return node.getHasNewLayout();
        }

        public static void YGConfigSetPrintTreeFlag(YGConfigRef config, bool enabled)
        {
            config.printTree = enabled;
        }

        public static void YGNodeSetHasNewLayout(YGNodeRef node, bool hasNewLayout)
        {
            node.setHasNewLayout(hasNewLayout);
        }

        public static YGNodeType YGNodeGetNodeType(YGNodeRef node)
        {
            return node.getNodeType();
        }

        public static void YGNodeSetNodeType(YGNodeRef node, YGNodeType nodeType)
        {
            node.setNodeType(nodeType);
        }

        public static bool YGNodeIsDirty(YGNodeRef node)
        {
            return node.IsDirty;
        }

        public static void YGNodeMarkDirtyAndPropogateToDescendants(YGNodeRef node)
        {
            node.markDirtyAndPropogateDownwards();
        }

        public static int gNodeInstanceCount   = 0;

        // WIN_EXPORT 
        public static YGNodeRef YGNodeNewWithConfig(YGConfigRef config)
        {
            var node = new YGNode(config);
            gNodeInstanceCount++;

            if (config.UseWebDefaults)
            {
                node.setStyleFlexDirection(YGFlexDirection.Row);
                node.setStyleAlignContent(YGAlign.Stretch);
            }
            return node;
        }

        public static YGNodeRef YGNodeNew()
        {
            return YGNodeNewWithConfig( YGConfig.DefaultConfig );
        }

        public static YGNodeRef YGNodeClone(YGNodeRef oldNode)
        {
            var node = new YGNode(oldNode);
            gNodeInstanceCount++;
            node.setOwner(null);
            return node;
        }

        private static YGNodeRef YGNodeDeepClone(YGNodeRef oldNode)
        {
            var       node      = YGNodeClone(oldNode);
            var       vec       = new YGVector(oldNode.getChildren().Count);
            YGNodeRef childNode = null;
            foreach (var item in oldNode.getChildren())
            {
                childNode = YGNodeDeepClone(item);
                childNode.setOwner(node);
                vec.Add(childNode);
            }

            node.setChildren(vec);

            if (oldNode.Config != null)
                node.Config = new YGConfig(oldNode.Config);

            return node;
        }

        public static void YGNodeFree(YGNodeRef node)
        {
            var owner = node.getOwner();
            if (owner != null)
            {
                owner.removeChild(node);
                node.setOwner(null);
            }

            var childCount = YGNodeGetChildCount(node);
            for (var i = 0; i < childCount; i++)
            {
                var child = YGNodeGetChild(node, i);
                child.setOwner(null);
            }

            node.ClearChildren();
            //delete node;
            gNodeInstanceCount--;
        }

        internal static void YGConfigFreeRecursive(YGNodeRef root)
        {
            // Delete configs recursively for childrens
            foreach (var child in root.getChildren())
                YGConfigFreeRecursive(child);
        }

        public static void YGNodeFreeRecursive(YGNodeRef root)
        {
            while (YGNodeGetChildCount(root) > 0)
            {
                var child = YGNodeGetChild(root, 0);
                if (child.getOwner() != root) break;

                YGNodeRemoveChild(root, child);
                YGNodeFreeRecursive(child);
            }

            YGNodeFree(root);
        }

        public static void YGNodeReset(YGNodeRef node)
        {
            YGAssertWithNode(
                node,
                YGNodeGetChildCount(node) == 0,
                "Cannot reset a node which still has children attached");
            YGAssertWithNode(
                node,
                node.getOwner() == null,
                "Cannot reset a node still attached to a owner");

            node.ClearChildren();

            var config = node.Config;
            node = new YGNode(config);
            if (config.UseWebDefaults)
            {
                node.setStyleFlexDirection(YGFlexDirection.Row);
                node.setStyleAlignContent(YGAlign.Stretch);
            }
        }

        public static int YGNodeGetInstanceCount()
        {
            return gNodeInstanceCount;
        }

        public static void YGNodeInsertChild(
            YGNodeRef node,
            YGNodeRef child,
            int       index)
        {
            YGAssertWithNode(
                node,
                child.getOwner() == null,
                "Child already has a owner, it must be removed first.");

            YGAssertWithNode(
                node,
                node.MeasureFunc == null,
                "Cannot add child: Nodes with measure functions cannot have children.");

            node.cloneChildrenIfNeeded();
            node.insertChild(child, index);
            var owner = child.getOwner() != null ? null : node;
            child.setOwner(owner);
            node.markDirtyAndPropogate();
        }

        public static void YGNodeInsertSharedChild(
            YGNodeRef node,
            YGNodeRef child,
            int       index)
        {
            YGAssertWithNode(
                node,
                node.MeasureFunc == null,
                "Cannot add child: Nodes with measure functions cannot have children.");

            node.insertChild(child, index);
            child.setOwner(null);
            node.markDirtyAndPropogate();
        }

        public static void YGNodeRemoveChild(YGNodeRef owner, YGNodeRef excludedChild)
        {
            // This algorithm is a forked variant from cloneChildrenIfNeeded in YGNode
            // that excludes a child.
            var childCount = YGNodeGetChildCount(owner);

            if (childCount == 0) return;

            var firstChild = YGNodeGetChild(owner, 0);
            if (firstChild.getOwner() == owner)
            {
                // If the first child has this node as its owner, we assume that it is
                // already unique. We can now try to delete a child in this list.
                if (owner.removeChild(excludedChild))
                {
                    excludedChild.Layout = (new YGNode().Layout); // layout is no longer valid
                    excludedChild.setOwner(null);
                    owner.markDirtyAndPropogate();
                }

                return;
            }

            // Otherwise we have to clone the node list except for the child we're trying
            // to delete. We don't want to simply clone all children, because then the
            // host will need to free the clone of the child that was just deleted.
            var nextInsertIndex = 0;
            for (var i = 0; i < childCount; i++)
            {
                var oldChild = owner.getChild(i);
                if (excludedChild == oldChild)
                {
                    // Ignore the deleted child. Don't reset its layout or owner since it is
                    // still valid in the other owner. However, since this owner has now
                    // changed, we need to mark it as dirty.
                    owner.markDirtyAndPropogate();
                    continue;
                }

                YGNodeRef newChild = YGNodeClone(oldChild);

                owner.replaceChild(newChild, nextInsertIndex);
                newChild.setOwner(owner);

                nextInsertIndex++;
            }

            while (nextInsertIndex < childCount)
            {
                owner.removeChild(nextInsertIndex);
                nextInsertIndex++;
            }
        }

        public static void YGNodeRemoveAllChildren(YGNodeRef owner)
        {
            var childCount = YGNodeGetChildCount(owner);
            if (childCount == 0) return;

            var firstChild = YGNodeGetChild(owner, 0);
            if (firstChild.getOwner() == owner)
            {
                // If the first child has this node as its owner, we assume that this child
                // set is unique.
                for (var i = 0; i < childCount; i++)
                {
                    var oldChild = YGNodeGetChild(owner, i);
                    oldChild.Layout = new YGNode().Layout; // layout is no longer valid
                    oldChild.setOwner(null);
                }

                owner.ClearChildren();
                owner.markDirtyAndPropogate();
                return;
            }

            // Otherwise, we are not the owner of the child set. We don't have to do
            // anything to clear it.
            owner.setChildren(new YGVector());
            owner.markDirtyAndPropogate();
        }

        internal static void YGNodeSetChildrenInternal(
            YGNodeRef       owner,
            List<YGNodeRef> children)
        {
            if (owner == null) return;

            if (children.Count == 0)
            {
                if (YGNodeGetChildCount(owner) > 0)
                {
                    foreach (var child in owner.getChildren())
                    {
                        child.Layout = new YGLayout();
                        child.setOwner(null);
                    }

                    owner.setChildren(new YGVector());
                    owner.markDirtyAndPropogate();
                }
            }
            else
            {
                if (YGNodeGetChildCount(owner) > 0)
                    foreach (var oldChild in owner.getChildren())
                        // Our new children may have nodes in common with the old children. We
                        // don't reset these common nodes.
                        if (!children.Contains(oldChild))
                        {
                            oldChild.Layout = new YGLayout();
                            oldChild.setOwner(null);
                        }

                owner.setChildren(children);
                foreach (var child in children) child.setOwner(owner);

                owner.markDirtyAndPropogate();
            }
        }

        public static void YGNodeSetChildren(
            YGNodeRef   owner,
            YGNodeRef[] c,
            int         count)
        {
            var children = c.ToList().Take(count).ToList();
            YGNodeSetChildrenInternal(owner, children);
        }

        public static void YGNodeSetChildren(
            YGNodeRef       owner,
            List<YGNodeRef> children)
        {
            YGNodeSetChildrenInternal(owner, children);
        }

        public static YGNodeRef YGNodeGetChild(YGNodeRef node, int index)
        {
            if (index < node.getChildren().Count) return node.getChild(index);

            return null;
        }

        public static int YGNodeGetChildCount(YGNodeRef node)
        {
            return node.getChildren().Count;
        }

        public static YGNodeRef YGNodeGetOwner(YGNodeRef node)
        {
            return node.getOwner();
        }

        public static YGNodeRef YGNodeGetParent(YGNodeRef node)
        {
            return node.getOwner();
        }

        public static void YGNodeMarkDirty(YGNodeRef node)
        {
            YGAssertWithNode(
                node,
                node.MeasureFunc != null,
                "Only leaf nodes with custom measure functions should manually mark themselves as dirty");

            node.markDirtyAndPropogate();
        }

        public static void YGNodeCopyStyle(YGNodeRef dstNode, YGNodeRef srcNode)
        {
            if (dstNode.Style != srcNode.Style)
            {
                dstNode.Style = srcNode.Style;
                dstNode.markDirtyAndPropogate();
            }
        }

        public static float YGNodeStyleGetFlexGrow(YGNodeRef node)
        {
            return node.Style.flexGrow ?? kDefaultFlexGrow;
        }

        public static float YGNodeStyleGetFlexShrink(YGNodeRef node)
        {
            return node.Style.flexShrink ??
                (node.Config.UseWebDefaults ? kWebDefaultFlexShrink : kDefaultFlexShrink);
        }

        public static void YGNodeStyleSetDirection(
            YGNodeRef   node,
            YGDirection direction)
        {
            if (node.Style.direction != direction)
            {
                node.Style.direction = direction;
                node.markDirtyAndPropogate();
            }
        }

        public static YGDirection YGNodeStyleGetDirection(YGNodeRef node)
        {
            return node.Style.direction;
        }

        public static void YGNodeStyleSetFlexDirection(
            YGNodeRef       node,
            YGFlexDirection flexDirection)
        {
            if (node.Style.flexDirection != flexDirection)
            {
                node.Style.flexDirection = flexDirection;
                node.markDirtyAndPropogate();
            }
        }

        public static YGFlexDirection YGNodeStyleGetFlexDirection(YGNodeRef node)
        {
            return node.Style.flexDirection;
        }

        public static void YGNodeStyleSetJustifyContent(
            YGNodeRef node,
            YGJustify justifyContent)
        {
            if (node.Style.justifyContent != justifyContent)
            {
                node.Style.justifyContent = justifyContent;
                node.markDirtyAndPropogate();
            }
        }

        public static YGJustify YGNodeStyleGetJustifyContent(YGNodeRef node)
        {
            return node.Style.justifyContent;
        }

        public static void YGNodeStyleSetAlignContent(
            YGNodeRef node,
            YGAlign   alignContent)
        {
            if (node.Style.alignContent != alignContent)
            {
                node.Style.alignContent = alignContent;
                node.markDirtyAndPropogate();
            }
        }

        public static YGAlign YGNodeStyleGetAlignContent(YGNodeRef node)
        {
            return node.Style.alignContent;
        }

        public static void YGNodeStyleSetAlignItems(YGNodeRef node, YGAlign alignItems)
        {
            if (node.Style.alignItems != alignItems)
            {
                node.Style.alignItems = alignItems;
                node.markDirtyAndPropogate();
            }
        }

        public static YGAlign YGNodeStyleGetAlignItems(YGNodeRef node)
        {
            return node.Style.alignItems;
        }

        public static void YGNodeStyleSetAlignSelf(YGNodeRef node, YGAlign alignSelf)
        {
            if (node.Style.alignSelf != alignSelf)
            {
                node.Style.alignSelf = alignSelf;
                node.markDirtyAndPropogate();
            }
        }

        public static YGAlign YGNodeStyleGetAlignSelf(YGNodeRef node)
        {
            return node.Style.alignSelf;
        }

        public static void YGNodeStyleSetPositionType(
            YGNodeRef      node,
            YGPositionType positionType)
        {
            if (node.Style.positionType != positionType)
            {
                node.Style.positionType = positionType;
                node.markDirtyAndPropogate();
            }
        }

        public static YGPositionType YGNodeStyleGetPositionType(YGNodeRef node)
        {
            return node.Style.positionType;
        }

        public static void YGNodeStyleSetFlexWrap(YGNodeRef node, YGWrap flexWrap)
        {
            if (node.Style.flexWrap != flexWrap)
            {
                node.Style.flexWrap = flexWrap;
                node.markDirtyAndPropogate();
            }
        }

        public static YGWrap YGNodeStyleGetFlexWrap(YGNodeRef node)
        {
            return node.Style.flexWrap;
        }

        public static void YGNodeStyleSetOverflow(YGNodeRef node, YGOverflow overflow)
        {
            if (node.Style.overflow != overflow)
            {
                node.Style.overflow = overflow;
                node.markDirtyAndPropogate();
            }
        }

        public static YGOverflow YGNodeStyleGetOverflow(YGNodeRef node)
        {
            return node.Style.overflow;
        }

        public static void YGNodeStyleSetDisplay(YGNodeRef node, YGDisplay display)
        {
            if (node.Style.display != display)
            {
                node.Style.display = display;
                node.markDirtyAndPropogate();
            }
        }

        public static YGDisplay YGNodeStyleGetDisplay(YGNodeRef node)
        {
            return node.Style.display;
        }

        // TODO(T26792433): Change the API to accept float?.
        public static void YGNodeStyleSetFlex(YGNodeRef node, float flex)
        {
            if (node.Style.flex != flex)
            {
                node.Style.flex = flex.HasValue() ? flex : (float?)null;
                node.markDirtyAndPropogate();
            }
        }

        // TODO(T26792433): Change the API to accept float?.
        public static float YGNodeStyleGetFlex(YGNodeRef node)
        {
            return node.Style.flex ?? YGUndefined;
        }

        // TODO(T26792433): Change the API to accept float?.
        public static void YGNodeStyleSetFlexGrow(YGNodeRef node, float flexGrow)
        {
            if (node.Style.flexGrow != flexGrow)
            {
                node.Style.flexGrow = flexGrow.HasValue() ? flexGrow : (float?)null;
                node.markDirtyAndPropogate();
            }
        }

        // TODO(T26792433): Change the API to accept float?.
        public static void YGNodeStyleSetFlexShrink(YGNodeRef node, float flexShrink)
        {
            if (node.Style.flexShrink != flexShrink)
            {
                node.Style.flexShrink = flexShrink.HasValue() ? flexShrink : (float?)null;
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetFlexBasis(YGNodeRef node)
        {
            return node.Style.flexBasis;
        }

        public static void YGNodeStyleSetFlexBasis(YGNodeRef node, float flexBasis)
        {
            var value = YGValue.Sanitized(flexBasis, YGUnit.Point);

            if (!YGFloatsEqual(node.Style.flexBasis.value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.flexBasis.unit != value.unit)
            {
                node.Style.flexBasis = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetFlexBasisPercent(
            YGNodeRef node,
            float     flexBasisPercent)
        {
            if (!YGFloatsEqual(node.Style.flexBasis.value, flexBasisPercent) ||
                node.Style.flexBasis.unit  != YGUnit.Percent)
            {
                node.Style.flexBasis = flexBasisPercent.IsUndefined()
                    ? new YGValue(0,                YGUnit.Auto)
                    : new YGValue(flexBasisPercent, YGUnit.Percent);
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetFlexBasisAuto(YGNodeRef node)
        {
            if (node.Style.flexBasis.unit != YGUnit.Auto)
            {
                node.Style.flexBasis = new YGValue(0, YGUnit.Auto);
                node.markDirtyAndPropogate();
            }
        }

        //YG_NODE_STYLE_EDGE_PROPERTY_UNIT_IMPL(YGValue,      Position, position, position);
        public static void YGNodeStyleSetPosition(YGNodeRef node, YGEdge edge, float position)
        {
            var value = YGValue.Sanitized(position, YGUnit.Point);

            if (!YGFloatsEqual(node.Style.Position[edge].value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Position[edge].unit != value.unit)
            {
                node.Style.Position[edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetPositionPercent(
            YGNodeRef node, YGEdge edge, float position)
        {
            var value = YGValue.Sanitized(position, YGUnit.Percent);
            if (!YGFloatsEqual(node.Style.Position[edge].value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Position[edge].unit != value.unit)
            {
                node.Style.Position[edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetPosition(YGNodeRef node, YGEdge edge)
        {
            var value = node.Style.Position[edge];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto)
            {
                return (node.Style.Position[edge] = new YGValue(YGUndefined, value.unit));
            }

            return value;
        }

        // YG_NODE_STYLE_EDGE_PROPERTY_UNIT_IMPL(YGValue,      Margin,   margin,   margin);
        public static void YGNodeStyleSetMargin(YGNodeRef node, YGEdge edge, float margin)
        {
            var value = YGValue.Sanitized(margin, YGUnit.Point);

            if (!YGFloatsEqual(node.Style.Margin[edge].value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Margin[edge].unit != value.unit)
            {
                node.Style.Margin[edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetMarginPercent(
            YGNodeRef node,
            YGEdge    edge,
            float     margin)
        {
            var value = YGValue.Sanitized(margin, YGUnit.Percent);
            if (!YGFloatsEqual(node.Style.Margin[edge].value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Margin[edge].unit != value.unit)
            {
                node.Style.Margin[edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetMargin(YGNodeRef node, YGEdge edge)
        {
            var value = node.Style.Margin[edge];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto)
                return (node.Style.Margin[edge] = new YGValue(YGUndefined, value.unit));

            return value;
        }

        // YG_NODE_STYLE_EDGE_PROPERTY_UNIT_AUTO_IMPL(YGValue, Margin,   margin);
        public static void YGNodeStyleSetMarginAuto(YGNodeRef node, YGEdge edge)
        {
            if (node.Style.Margin[edge].unit != YGUnit.Auto)
            {
                node.Style.Margin[edge] = YGConst.YGValueAuto;
                node.markDirtyAndPropogate();
            }
        }

        //YG_NODE_STYLE_EDGE_PROPERTY_UNIT_IMPL(YGValue,      Padding,  padding, padding);
        public static void YGNodeStyleSetPadding(YGNodeRef node, YGEdge edge, float padding)
        {
            var value = YGValue.Sanitized(padding, YGUnit.Point);

            if (!YGFloatsEqual(node.Style.Padding[edge].value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Padding[edge].unit != value.unit)
            {
                node.Style.Padding[edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetPaddingPercent(
            YGNodeRef node, YGEdge edge, float padding)
        {
            var value = YGValue.Sanitized(padding, YGUnit.Percent);
            if (!YGFloatsEqual(node.Style.Padding[edge].value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Padding[edge].unit != value.unit)
            {
                node.Style.Padding[edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetPadding(YGNodeRef node, YGEdge edge)
        {
            var value = node.Style.Padding[edge];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto)
            {
                return (node.Style.Padding[edge] = new YGValue(YGUndefined, value.unit));
            }

            return value;
        }


        // TODO(T26792433): Change the API to accept float?.
        public static void YGNodeStyleSetBorder(
            YGNodeRef node,
            YGEdge    edge,
            float     border)
        {
            var value = YGValue.Sanitized(border,YGUnit.Point);
            if (!YGFloatsEqual(node.Style.Border[edge].value, value.value) &&
                value.unit                          != YGUnit.Undefined ||
                node.Style.Border[edge].unit != value.unit)
            {
                node.Style.Border[edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static float YGNodeStyleGetBorder(YGNodeRef node, YGEdge edge)
        {
            var value = node.Style.Border[edge];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto)
                return YGUndefined;
            return value.value;
        }

        // Yoga specific properties, not compatible with flexbox specification

        // YG_NODE_STYLE_PROPERTY_UNIT_AUTO_IMPL(YGValue,Width,width,dimensions[YGDimension.Width]);
        public static void YGNodeStyleSetWidth(YGNodeRef node, YGValue width)
        {
            var value = YGValue.Sanitized(width.value,YGUnit.Point);
            if (!YGFloatsEqual(node.Style.Dimensions.Width.value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Dimensions.Width.unit != value.unit)
            {
                node.Style.Dimensions.Width = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetWidthPercent(
            YGNodeRef node, YGValue width)
        {
            if (!YGFloatsEqual(node.Style.Dimensions.Width.value, YGFloatSanitize(width.value)) ||
                node.Style.Dimensions.Width.unit  != YGUnit.Percent)
            {
                node.Style.Dimensions.Width = width.IsUndefined()
                    ? new YGValue(0,           YGUnit.Auto)
                    : new YGValue(width.value, YGUnit.Percent);
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetWidthAuto(YGNodeRef node)
        {
            if (node.Style.Dimensions[YGDimension.Width].unit != YGUnit.Auto)
            {
                node.Style.Dimensions[YGDimension.Width] = new YGValue(0, YGUnit.Auto);
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetWidth(YGNodeRef node)
        {
            var value = node.Style.Dimensions[YGDimension.Width];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Undefined)
            {
                node.Style.Dimensions[YGDimension.Width] = value = new YGValue(YGUndefined, value.unit);
            }

            return value;
        }


        //YG_NODE_STYLE_PROPERTY_UNIT_AUTO_IMPL(YGValue,Height,height,dimensions[ YGDimension.Height]);
        public static void YGNodeStyleSetHeight(YGNodeRef node, YGValue height)
        {
            var value = YGValue.Sanitized(height.value, YGUnit.Point);
            if (!YGFloatsEqual(node.Style.Dimensions.Height.value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Dimensions.Height.unit != value.unit)
            {
                node.Style.Dimensions.Height = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetHeightPercent(YGNodeRef node, YGValue height)
        {
            if (!YGFloatsEqual(node.Style.Dimensions.Height.value, YGFloatSanitize(height.value)) ||
                node.Style.Dimensions.Height.unit  != YGUnit.Percent)
            {
                node.Style.Dimensions.Height = height.IsUndefined()
                    ? new YGValue(0,            YGUnit.Auto)
                    : new YGValue(height.value, YGUnit.Percent);
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetHeightAuto(YGNodeRef node)
        {
            if (node.Style.Dimensions.Height.unit != YGUnit.Auto)
            {
                node.Style.Dimensions.Height = new YGValue(0, YGUnit.Auto);
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetHeight(YGNodeRef node)
        {
            var value = node.Style.Dimensions.Height;
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Undefined)
                node.Style.Dimensions.Height = value = new YGValue(YGUndefined, value.unit);
            return value;
        }


        internal static void YGNodeStyleSetDimensions(
            YGNodeRef   node,
            Dimensions  dimensions,
            YGDimension dim,
            float       value,
            YGUnit      unit)
        {
            var newValue = YGValue.Sanitized(value, unit);
            var current  = dimensions[dim];
            if (current != newValue)
            {
                dimensions[dim] = newValue;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetMinWidth(YGNodeRef node, float minWidth)
        {
            YGNodeStyleSetDimensions(node, node.Style.MinDimensions, YGDimension.Width, minWidth, YGUnit.Point);
        }

        public static void YGNodeStyleSetMinWidthPercent(YGNodeRef node, float minWidth)
        {
            YGNodeStyleSetDimensions(node, node.Style.MinDimensions, YGDimension.Width, minWidth, YGUnit.Percent);
        }

        public static void YGNodeStyleSetMinHeight(YGNodeRef node, float minHeight)
        {
            YGNodeStyleSetDimensions(node, node.Style.MinDimensions, YGDimension.Height, minHeight, YGUnit.Point);
        }

        public static void YGNodeStyleSetMinHeightPercent(YGNodeRef node, float minHeight)
        {
            YGNodeStyleSetDimensions(node, node.Style.MinDimensions, YGDimension.Height, minHeight, YGUnit.Percent);
        }

        public static void YGNodeStyleSetMaxWidth(YGNodeRef node, float maxWidth)
        {
            YGNodeStyleSetDimensions(node, node.Style.MaxDimensions, YGDimension.Width, maxWidth, YGUnit.Point);
        }

        public static void YGNodeStyleSetMaxWidthPercent(YGNodeRef node, float maxWidth)
        {
            YGNodeStyleSetDimensions(node, node.Style.MaxDimensions, YGDimension.Width, maxWidth, YGUnit.Percent);
        }

        public static void YGNodeStyleSetMaxHeight(YGNodeRef node, float maxHeight)
        {
            YGNodeStyleSetDimensions(node, node.Style.MaxDimensions, YGDimension.Height, maxHeight, YGUnit.Point);
        }

        public static void YGNodeStyleSetMaxHeightPercent(YGNodeRef node, float maxHeight)
        {
            YGNodeStyleSetDimensions(node, node.Style.MaxDimensions, YGDimension.Height, maxHeight, YGUnit.Percent);
        }

        //YG_NODE_LAYOUT_PROPERTY_IMPL(float, Left, position[YGEdge.Left]);
        //YG_NODE_LAYOUT_PROPERTY_IMPL(float,       Top,         position[   YGEdge.Top]);
        //YG_NODE_LAYOUT_PROPERTY_IMPL(float,       Right,       position[   YGEdge.Right]);
        //YG_NODE_LAYOUT_PROPERTY_IMPL(float,       Bottom,      position[   YGEdge.Bottom]);

        //YG_NODE_LAYOUT_PROPERTY_IMPL(float,       Width,       dimensions[ YGDimension.Width]);
        //YG_NODE_LAYOUT_PROPERTY_IMPL(float,       Height,      dimensions[ YGDimension.Height]);
        //YG_NODE_LAYOUT_PROPERTY_IMPL(YGDirection, Direction,   direction);
        //YG_NODE_LAYOUT_PROPERTY_IMPL(bool,        HadOverflow, hadOverflow);

        //YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(float, Margin,  margin);
        public static float YGNodeLayoutGetMargin(YGNodeRef node, YGEdge edge)
        {
            YGAssertWithNode(
                node,
                edge <= YGEdge.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case YGEdge.Left when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.margin[(int) YGEdge.End];
            case YGEdge.Left:
                return node.Layout.margin[(int) YGEdge.Start];
            case YGEdge.Right when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.margin[(int) YGEdge.Start];
            case YGEdge.Right:
                return node.Layout.margin[(int) YGEdge.End];
            }

            return node.Layout.margin[(int) edge];
        }


        // YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(float, Border,  border);
        public static float YGNodeLayoutGetBorder(YGNodeRef node, YGEdge edge)
        {
            YGAssertWithNode(
                node,
                edge <= YGEdge.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case YGEdge.Left when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.border[(int) YGEdge.End];
            case YGEdge.Left:
                return node.Layout.border[(int) YGEdge.Start];
            case YGEdge.Right when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.border[(int) YGEdge.Start];
            case YGEdge.Right:
                return node.Layout.border[(int) YGEdge.End];
            }

            return node.Layout.border[(int) edge];
        }

        // YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(float, Padding, padding);
        public static float YGNodeLayoutGetPadding(YGNodeRef node, YGEdge edge)
        {
            YGAssertWithNode(
                node,
                edge <= YGEdge.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case YGEdge.Left when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.padding[(int) YGEdge.End];
            case YGEdge.Left:
                return node.Layout.padding[(int) YGEdge.Start];
            case YGEdge.Right when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.padding[(int) YGEdge.Start];
            case YGEdge.Right:
                return node.Layout.padding[(int) YGEdge.End];
            }

            return node.Layout.padding[(int) edge];
        }

        public static int gCurrentGenerationCount = 0;

        internal static void YGNodePrintInternal(
            YGNodeRef      node,
            YGPrintOptions options)
        {
            var sb = YGNodeToString(null, node, options, 0);

            YGLog(node, YGLogLevel.Debug, sb.ToString());
        }

        public static void YGNodePrint(YGNodeRef node, YGPrintOptions options)
        {
            YGNodePrintInternal(node, options);
        }

        public static YGEdge[] leading  = {YGEdge.Top, YGEdge.Bottom, YGEdge.Left, YGEdge.Right};
        public static YGEdge[] trailing = {YGEdge.Bottom, YGEdge.Top, YGEdge.Right, YGEdge.Left};

        internal static YGEdge[]      pos = {YGEdge.Top, YGEdge.Bottom, YGEdge.Left, YGEdge.Right};
        internal static YGDimension[] dim = {YGDimension.Height, YGDimension.Height, YGDimension.Width, YGDimension.Width};

        internal static float YGNodePaddingAndBorderForAxis(
            YGNodeRef       node,
            YGFlexDirection axis,
            float           widthSize)
        {
            return YGUnwrapFloatOptional(
                node.getLeadingPaddingAndBorder(axis, widthSize) +
                node.getTrailingPaddingAndBorder(axis, widthSize));
        }

        internal static YGAlign YGNodeAlignItem(
            YGNodeRef node,
            YGNodeRef child)
        {
            var align = child.Style.alignSelf == YGAlign.Auto
                ? node.Style.alignItems
                : child.Style.alignSelf;
            if (align == YGAlign.Baseline &&
                YGFlexDirectionIsColumn(node.Style.flexDirection))
                return YGAlign.FlexStart;

            return align;
        }

        internal static float YGBaseline(YGNodeRef node)
        {
            if (node.getBaseline() != null)
            {
                var baseline = node.getBaseline()(
                    node,
                    node.Layout.MeasuredWidth,
                    node.Layout.MeasuredHeight);
                YGAssertWithNode(
                    node,
                    baseline.HasValue(),
                    "Expect custom baseline function to not return NaN");
                return baseline;
            }

            YGNodeRef baselineChild = null;
            var       childCount    = YGNodeGetChildCount(node);
            for (var i = 0; i < childCount; i++)
            {
                var child = YGNodeGetChild(node, i);
                if (child.getLineIndex() > 0) break;

                if (child.Style.positionType == YGPositionType.Absolute) continue;

                if (YGNodeAlignItem(node, child) == YGAlign.Baseline)
                {
                    baselineChild = child;
                    break;
                }

                if (baselineChild == null) baselineChild = child;
            }

            if (baselineChild == null) return node.Layout.MeasuredHeight;

            var childBaseline = YGBaseline(baselineChild);
            return childBaseline + baselineChild.Layout.Position.Top;
        }

        internal static bool YGIsBaselineLayout(YGNodeRef node)
        {
            if (YGFlexDirectionIsColumn(node.Style.flexDirection)) return false;

            if (node.Style.alignItems == YGAlign.Baseline) return true;

            var childCount = YGNodeGetChildCount(node);
            for (var i = 0; i < childCount; i++)
            {
                var child = YGNodeGetChild(node, i);
                if (child.Style.positionType == YGPositionType.Relative &&
                    child.Style.alignSelf    == YGAlign.Baseline)
                    return true;
            }

            return false;
        }

        // inline
        internal static float YGNodeDimWithMargin(
            YGNodeRef       node,
            YGFlexDirection axis,
            float           widthSize)
        {
            return node.Layout.GetMeasuredDimension(dim[(int) axis]) +
                YGUnwrapFloatOptional(
                    node.getLeadingMargin(axis, widthSize) +
                    node.getTrailingMargin(axis, widthSize));
        }

        // inline
        internal static bool YGNodeIsStyleDimDefined(
            YGNodeRef       node,
            YGFlexDirection axis,
            float           ownerSize)
        {
            var isUndefined = node.getResolvedDimension(dim[(int) axis]).IsUndefined();
            return !(
                node.getResolvedDimension(dim[(int) axis]).unit == YGUnit.Auto      ||
                node.getResolvedDimension(dim[(int) axis]).unit == YGUnit.Undefined ||
                node.getResolvedDimension(dim[(int) axis]).unit == YGUnit.Point &&
                !isUndefined                                                    && 
                node.getResolvedDimension(dim[(int) axis]).value < 0.0f ||
                node.getResolvedDimension(dim[(int) axis]).unit == YGUnit.Percent &&
                !isUndefined                                                      &&
                (node.getResolvedDimension(dim[(int) axis]).value < 0.0f || ownerSize.IsUndefined()));
        }

        // inline
        internal static bool YGNodeIsLayoutDimDefined(
            YGNodeRef       node,
            YGFlexDirection axis)
        {
            var value = node.Layout.GetMeasuredDimension(dim[(int) axis]);
            return value.HasValue() && value >= 0.0f;
        }

        internal static float? YGNodeBoundAxisWithinMinAndMax(
            YGNodeRef       node,
            YGFlexDirection axis,
            float           value,
            float           axisSize)
        {
            float? min = null;
            float? max = null;

            if (YGFlexDirectionIsColumn(axis))
            {
                min = YGResolveValue(
                    node.Style.MinDimensions[YGDimension.Height],
                    axisSize);
                max = YGResolveValue(
                    node.Style.MaxDimensions[YGDimension.Height],
                    axisSize);
            }
            else if (YGFlexDirectionIsRow(axis))
            {
                min = YGResolveValue(
                    node.Style.MinDimensions[YGDimension.Width],
                    axisSize);
                max = YGResolveValue(
                    node.Style.MaxDimensions[YGDimension.Width],
                    axisSize);
            }

            if (max.HasValue && max >= 0 && value > max)
                return max;

            if (min.HasValue && min >= 0 && value < min)
                return min;

            return value;
        }

        // Like YGNodeBoundAxisWithinMinAndMax but also ensures that the value doesn't
        // go below the padding and border amount.
        // inline
        internal static float YGNodeBoundAxis(
            YGNodeRef       node,
            YGFlexDirection axis,
            float           value,
            float           axisSize,
            float           widthSize)
        {
            return YGFloatMax(
                YGUnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(node, axis, value, axisSize)),
                YGNodePaddingAndBorderForAxis(node, axis, widthSize));
        }

        internal static void YGNodeSetChildTrailingPosition(
            YGNodeRef       node,
            YGNodeRef       child,
            YGFlexDirection axis)
        {
            var size = child.Layout.GetMeasuredDimension(dim[(int) axis]);
            child.Layout.Position[trailing[(int)axis]] = 
                node.Layout.GetMeasuredDimension(dim[(int) axis]) - size -
                child.Layout.Position[pos[(int) axis]];
        }

        internal static void YGConstrainMaxSizeForMode(
            YGNodeRef         node,
            YGFlexDirection   axis,
            float             ownerAxisSize,
            float             ownerWidth,
            ref YGMeasureMode mode,
            ref float         size)
        {
            var maxSize =
                YGResolveValue(node.Style.MaxDimensions[dim[(int) axis]], ownerAxisSize) +
                node.getMarginForAxis(axis, ownerWidth);
            switch (mode)
            {
            case YGMeasureMode.Exactly:
            case YGMeasureMode.AtMost:
                if (maxSize.HasValue && size > maxSize)
                    size = maxSize.Value;
                break;
            case YGMeasureMode.Undefined:
                if (maxSize.HasValue)
                {
                    mode = YGMeasureMode.AtMost;
                    size = maxSize.Value;
                }

                break;
            }
        }

        internal static void YGNodeComputeFlexBasisForChild(
            YGNodeRef     node,
            YGNodeRef     child,
            float         width,
            YGMeasureMode widthMode,
            float         height,
            float         ownerWidth,
            float         ownerHeight,
            YGMeasureMode heightMode,
            YGDirection   direction,
            YGConfigRef   config)
        {
            var mainAxis          = YGResolveFlexDirection(node.Style.flexDirection, direction);
            var isMainAxisRow     = YGFlexDirectionIsRow(mainAxis);
            var mainAxisSize      = isMainAxisRow ? width : height;
            var mainAxisownerSize = isMainAxisRow ? ownerWidth : ownerHeight;

            float         childWidth;
            float         childHeight;
            YGMeasureMode childWidthMeasureMode;
            YGMeasureMode childHeightMeasureMode;

            var resolvedFlexBasis = YGResolveValue(child.resolveFlexBasisPtr(), mainAxisownerSize);
            var isRowStyleDimDefined =
                YGNodeIsStyleDimDefined(child, YGFlexDirection.Row, ownerWidth);
            var isColumnStyleDimDefined =
                YGNodeIsStyleDimDefined(child, YGFlexDirection.Column, ownerHeight);

            if (resolvedFlexBasis.HasValue && mainAxisSize.HasValue())
            {
                if (child.Layout.ComputedFlexBasis.IsUndefined() ||
                    child.Config.ExperimentalFeatures.HasFlag(YGExperimentalFeatures.WebFlexBasis) &&
                    child.Layout.ComputedFlexBasisGeneration != gCurrentGenerationCount)
                {
                    var paddingAndBorder = new float?(YGNodePaddingAndBorderForAxis(child, mainAxis, ownerWidth));
                    child.Layout.ComputedFlexBasis = FloatOptionalMax(resolvedFlexBasis, paddingAndBorder);
                }
            }
            else if (isMainAxisRow && isRowStyleDimDefined)
            {
                // The width is definite, so use that as the flex basis.
                var paddingAndBorder = new float?(
                    YGNodePaddingAndBorderForAxis(child, YGFlexDirection.Row, ownerWidth));

                child.Layout.ComputedFlexBasis =
                    FloatOptionalMax(
                        YGResolveValue(child.getResolvedDimension(YGDimension.Width), ownerWidth),
                        paddingAndBorder);
            }
            else if (!isMainAxisRow && isColumnStyleDimDefined)
            {
                // The height is definite, so use that as the flex basis.
                var paddingAndBorder = new float?(
                    YGNodePaddingAndBorderForAxis(
                        child,
                        YGFlexDirection.Column,
                        ownerWidth));
                child.Layout.ComputedFlexBasis =
                    FloatOptionalMax(
                        YGResolveValue(child.getResolvedDimension(YGDimension.Height), ownerHeight),
                        paddingAndBorder);
            }
            else
            {
                // Compute the flex basis and hypothetical main size (i.e. the clamped
                // flex basis).
                childWidth             = YGUndefined;
                childHeight            = YGUndefined;
                childWidthMeasureMode  = YGMeasureMode.Undefined;
                childHeightMeasureMode = YGMeasureMode.Undefined;

                var marginRow = YGUnwrapFloatOptional(
                    child.getMarginForAxis(YGFlexDirection.Row, ownerWidth));
                var marginColumn = YGUnwrapFloatOptional(
                    child.getMarginForAxis(YGFlexDirection.Column, ownerWidth));

                if (isRowStyleDimDefined)
                {
                    childWidth =
                        YGUnwrapFloatOptional(
                            YGResolveValue(
                                child.getResolvedDimension(YGDimension.Width),
                                ownerWidth)) +
                        marginRow;
                    childWidthMeasureMode = YGMeasureMode.Exactly;
                }

                if (isColumnStyleDimDefined)
                {
                    childHeight =
                        YGUnwrapFloatOptional(
                            YGResolveValue(
                                child.getResolvedDimension(YGDimension.Height),
                                ownerHeight)) +
                        marginColumn;
                    childHeightMeasureMode = YGMeasureMode.Exactly;
                }

                // The W3C spec doesn't say anything about the 'overflow' property,
                // but all major browsers appear to implement the following logic.
                if (!isMainAxisRow && node.Style.overflow == YGOverflow.Scroll ||
                    node.Style.overflow != YGOverflow.Scroll)
                    if (childWidth.IsUndefined() && width.HasValue())
                    {
                        childWidth            = width;
                        childWidthMeasureMode = YGMeasureMode.AtMost;
                    }

                if (isMainAxisRow && node.Style.overflow == YGOverflow.Scroll ||
                    node.Style.overflow != YGOverflow.Scroll)
                    if (childHeight.IsUndefined() && height.HasValue())
                    {
                        childHeight            = height;
                        childHeightMeasureMode = YGMeasureMode.AtMost;
                    }

                if (child.Style.AspectRatio.HasValue)
                {
                    if (!isMainAxisRow && childWidthMeasureMode == YGMeasureMode.Exactly)
                    {
                        childHeight = marginColumn +
                            (childWidth - marginRow) / child.Style.AspectRatio.Value;
                        childHeightMeasureMode = YGMeasureMode.Exactly;
                    }
                    else if (
                        isMainAxisRow && childHeightMeasureMode == YGMeasureMode.Exactly)
                    {
                        childWidth = marginRow +
                            (childHeight - marginColumn) *
                            child.Style.AspectRatio.Value;
                        childWidthMeasureMode = YGMeasureMode.Exactly;
                    }
                }

                // If child has no defined size in the cross axis and is set to stretch,
                // set the cross
                // axis to be measured exactly with the available inner width

                var hasExactWidth = width.HasValue() && widthMode == YGMeasureMode.Exactly;
                var childWidthStretch =
                    YGNodeAlignItem(node, child) == YGAlign.Stretch &&
                    childWidthMeasureMode        != YGMeasureMode.Exactly;
                if (!isMainAxisRow && !isRowStyleDimDefined && hasExactWidth &&
                    childWidthStretch)
                {
                    childWidth            = width;
                    childWidthMeasureMode = YGMeasureMode.Exactly;
                    if (child.Style.AspectRatio.HasValue)
                    {
                        childHeight = (childWidth - marginRow) / child.Style.AspectRatio.Value;
                        childHeightMeasureMode = YGMeasureMode.Exactly;
                    }
                }

                var hasExactHeight = height.HasValue() && heightMode == YGMeasureMode.Exactly;
                var childHeightStretch =
                    YGNodeAlignItem(node, child) == YGAlign.Stretch &&
                    childHeightMeasureMode       != YGMeasureMode.Exactly;
                if (isMainAxisRow && !isColumnStyleDimDefined && hasExactHeight &&
                    childHeightStretch)
                {
                    childHeight            = height;
                    childHeightMeasureMode = YGMeasureMode.Exactly;

                    if (child.Style.AspectRatio.HasValue)
                    {
                        childWidth = (childHeight - marginColumn) *
                            child.Style.AspectRatio.Value;
                        childWidthMeasureMode = YGMeasureMode.Exactly;
                    }
                }

                YGConstrainMaxSizeForMode(
                    child,
                    YGFlexDirection.Row,
                    ownerWidth,
                    ownerWidth,
                    ref childWidthMeasureMode,
                    ref childWidth);
                YGConstrainMaxSizeForMode(
                    child,
                    YGFlexDirection.Column,
                    ownerHeight,
                    ownerWidth,
                    ref childHeightMeasureMode,
                    ref childHeight);

                // Measure the child
                YGLayoutNodeInternal(
                    child,
                    childWidth,
                    childHeight,
                    direction,
                    childWidthMeasureMode,
                    childHeightMeasureMode,
                    ownerWidth,
                    ownerHeight,
                    false,
                    "measure",
                    config);

                child.Layout.ComputedFlexBasis =
                    new float?(
                        YGFloatMax(
                            child.Layout.GetMeasuredDimension(dim[(int) mainAxis]),
                            YGNodePaddingAndBorderForAxis(child, mainAxis, ownerWidth)));
            }

            child.Layout.ComputedFlexBasisGeneration = gCurrentGenerationCount;
        }

        internal static void YGNodeAbsoluteLayoutChild(
            YGNodeRef     node,
            YGNodeRef     child,
            float         width,
            YGMeasureMode widthMode,
            float         height,
            YGDirection   direction,
            YGConfigRef   config)
        {
            var mainAxis      = YGResolveFlexDirection(node.Style.flexDirection, direction);
            var crossAxis     = YGFlexDirectionCross(mainAxis, direction);
            var isMainAxisRow = YGFlexDirectionIsRow(mainAxis);

            var childWidth             = YGUndefined;
            var childHeight            = YGUndefined;
            var childWidthMeasureMode  = YGMeasureMode.Undefined;
            var childHeightMeasureMode = YGMeasureMode.Undefined;

            var marginRow =
                YGUnwrapFloatOptional(child.getMarginForAxis(YGFlexDirection.Row, width));
            var marginColumn = YGUnwrapFloatOptional(
                child.getMarginForAxis(YGFlexDirection.Column, width));

            if (YGNodeIsStyleDimDefined(child, YGFlexDirection.Row, width))
            {
                childWidth = YGUnwrapFloatOptional(
                        YGResolveValue(
                            child.getResolvedDimension(YGDimension.Width),
                            width)) +
                    marginRow;
            }
            else
            {
                // If the child doesn't have a specified width, compute the width based
                // on the left/right
                // offsets if they're defined.
                if (child.isLeadingPositionDefined(YGFlexDirection.Row) &&
                    child.isTrailingPosDefined(YGFlexDirection.Row))
                {
                    childWidth = node.Layout.MeasuredWidth -
                        (node.getLeadingBorder(YGFlexDirection.Row) +
                            node.getTrailingBorder(YGFlexDirection.Row)) -
                        YGUnwrapFloatOptional(
                            child.getLeadingPosition(YGFlexDirection.Row, width) +
                            child.getTrailingPosition(YGFlexDirection.Row, width));
                    childWidth =
                        YGNodeBoundAxis(child, YGFlexDirection.Row, childWidth, width, width);
                }
            }

            if (YGNodeIsStyleDimDefined(child, YGFlexDirection.Column, height))
            {
                childHeight = YGUnwrapFloatOptional(
                        YGResolveValue(
                            child.getResolvedDimension(YGDimension.Height),
                            height)) +
                    marginColumn;
            }
            else
            {
                // If the child doesn't have a specified height, compute the height
                // based on the top/bottom
                // offsets if they're defined.
                if (child.isLeadingPositionDefined(YGFlexDirection.Column) &&
                    child.isTrailingPosDefined(YGFlexDirection.Column))
                {
                    childHeight =
                        node.Layout.MeasuredHeight -
                        (node.getLeadingBorder(YGFlexDirection.Column) +
                            node.getTrailingBorder(YGFlexDirection.Column)) -
                        YGUnwrapFloatOptional(
                            child.getLeadingPosition(YGFlexDirection.Column, height) +
                            child.getTrailingPosition(YGFlexDirection.Column, height));
                    childHeight = YGNodeBoundAxis(
                        child,
                        YGFlexDirection.Column,
                        childHeight,
                        height,
                        width);
                }
            }

            // Exactly one dimension needs to be defined for us to be able to do aspect
            // ratio calculation. One dimension being the anchor and the other being
            // flexible.
            if (childWidth.IsUndefined() ^ childHeight.IsUndefined())
                if (child.Style.AspectRatio.HasValue)
                {
                    if (childWidth.IsUndefined())
                        childWidth = marginRow + (childHeight - marginColumn) * child.Style.AspectRatio.Value;
                    else if (childHeight.IsUndefined())
                        childHeight = marginColumn + (childWidth - marginRow) / child.Style.AspectRatio.Value;
                }

            // If we're still missing one or the other dimension, measure the content.
            if (childWidth.IsUndefined() || childHeight.IsUndefined())
            {
                childWidthMeasureMode = childWidth.IsUndefined() ? YGMeasureMode.Undefined : YGMeasureMode.Exactly;
                childHeightMeasureMode = childHeight.IsUndefined() ? YGMeasureMode.Undefined : YGMeasureMode.Exactly;

                // If the size of the owner is defined then try to constrain the absolute
                // child to that size as well. This allows text within the absolute child to
                // wrap to the size of its owner. This is the same behavior as many browsers
                // implement.
                if (!isMainAxisRow                       && childWidth.IsUndefined() &&
                    widthMode != YGMeasureMode.Undefined && width.HasValue()     &&
                    width     > 0)
                {
                    childWidth            = width;
                    childWidthMeasureMode = YGMeasureMode.AtMost;
                }

                YGLayoutNodeInternal(
                    child,
                    childWidth,
                    childHeight,
                    direction,
                    childWidthMeasureMode,
                    childHeightMeasureMode,
                    childWidth,
                    childHeight,
                    false,
                    "abs-measure",
                    config);
                childWidth = child.Layout.MeasuredWidth +
                    YGUnwrapFloatOptional(
                        child.getMarginForAxis(YGFlexDirection.Row, width));
                childHeight = child.Layout.MeasuredHeight +
                    YGUnwrapFloatOptional(
                        child.getMarginForAxis(YGFlexDirection.Column, width));
            }

            YGLayoutNodeInternal(
                child,
                childWidth,
                childHeight,
                direction,
                YGMeasureMode.Exactly,
                YGMeasureMode.Exactly,
                childWidth,
                childHeight,
                true,
                "abs-layout",
                config);

            if (child.isTrailingPosDefined(mainAxis) &&
                !child.isLeadingPositionDefined(mainAxis))
                child.Layout.Position[leading[(int) mainAxis]] =
                    node.Layout.GetMeasuredDimension(dim[(int) mainAxis])           -
                    child.Layout.GetMeasuredDimension(dim[(int) mainAxis])          -
                    node.getTrailingBorder(mainAxis)                                -
                    YGUnwrapFloatOptional(child.getTrailingMargin(mainAxis, width)) -
                    YGUnwrapFloatOptional(
                        child.getTrailingPosition(
                            mainAxis,
                            isMainAxisRow ? width : height));
            else if (
                !child.isLeadingPositionDefined(mainAxis) &&
                node.Style.justifyContent == YGJustify.Center)
                child.Layout.Position[leading[(int)mainAxis]] = 
                    (node.Layout.GetMeasuredDimension(dim[(int) mainAxis]) -
                        child.Layout.GetMeasuredDimension(dim[(int) mainAxis])) / 2.0f;
            else if (
                !child.isLeadingPositionDefined(mainAxis) &&
                node.Style.justifyContent == YGJustify.FlexEnd)
                child.Layout.Position[leading[(int)mainAxis]] = 
                    node.Layout.GetMeasuredDimension(dim[(int) mainAxis]) -
                    child.Layout.GetMeasuredDimension(dim[(int) mainAxis]);

            if (child.isTrailingPosDefined(crossAxis) &&
                !child.isLeadingPositionDefined(crossAxis))
                child.Layout.Position[leading[(int)crossAxis]] = 
                    node.Layout.GetMeasuredDimension(dim[(int) crossAxis])           -
                    child.Layout.GetMeasuredDimension(dim[(int) crossAxis])          -
                    node.getTrailingBorder(crossAxis)                                -
                    YGUnwrapFloatOptional(child.getTrailingMargin(crossAxis, width)) -
                    YGUnwrapFloatOptional(
                        child.getTrailingPosition(
                            crossAxis,
                            isMainAxisRow ? height : width));
            else if (
                !child.isLeadingPositionDefined(crossAxis) &&
                YGNodeAlignItem(node, child) == YGAlign.Center)
                child.Layout.Position[leading[(int)crossAxis]] = 
                    (node.Layout.GetMeasuredDimension(dim[(int) crossAxis]) -
                        child.Layout.GetMeasuredDimension(dim[(int) crossAxis])) /
                    2.0f;
                    
            else if (
                !child.isLeadingPositionDefined(crossAxis) &&
                (YGNodeAlignItem(node, child) == YGAlign.FlexEnd) ^
                (node.Style.flexWrap          == YGWrap.WrapReverse))
                child.Layout.Position[leading[(int)crossAxis]] =
                    node.Layout.GetMeasuredDimension(dim[(int) crossAxis]) -
                    child.Layout.GetMeasuredDimension(dim[(int) crossAxis]);
                    
        }

        internal static void YGNodeWithMeasureFuncSetMeasuredDimensions(
            YGNodeRef     node,
            float         availableWidth,
            float         availableHeight,
            YGMeasureMode widthMeasureMode,
            YGMeasureMode heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight)
        {
            YGAssertWithNode(
                node,
                node.MeasureFunc != null,
                "Expected node to have custom measure function");

            var paddingAndBorderAxisRow    = YGNodePaddingAndBorderForAxis(node, YGFlexDirection.Row,    availableWidth);
            var paddingAndBorderAxisColumn = YGNodePaddingAndBorderForAxis(node, YGFlexDirection.Column, availableWidth);
            var marginAxisRow              = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Row,    availableWidth));
            var marginAxisColumn           = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Column, availableWidth));

            // We want to make sure we don't call measure with negative size
            var innerWidth = availableWidth.IsUndefined()
                ? availableWidth
                : YGFloatMax(0, availableWidth - marginAxisRow - paddingAndBorderAxisRow);
            var innerHeight = availableHeight.IsUndefined()
                ? availableHeight
                : YGFloatMax(0, availableHeight - marginAxisColumn - paddingAndBorderAxisColumn);

            if (widthMeasureMode  == YGMeasureMode.Exactly &&
                heightMeasureMode == YGMeasureMode.Exactly)
            {
                // Don't bother sizing the text if both dimensions are already defined.
                node.Layout.SetMeasuredDimension(
                    YGDimension.Width,
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Row,
                        availableWidth - marginAxisRow,
                        ownerWidth,
                        ownerWidth)
                );
                node.Layout.SetMeasuredDimension(
                    YGDimension.Height,
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Column,
                        availableHeight - marginAxisColumn,
                        ownerHeight,
                        ownerWidth)
                );
            }
            else
            {
                // Measure the text under the current constraints.
                var measuredSize = node.MeasureFunc(node, innerWidth, widthMeasureMode, innerHeight, heightMeasureMode);

                node.Layout.SetMeasuredDimension(
                    YGDimension.Width,
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Row,
                        widthMeasureMode == YGMeasureMode.Undefined ||
                        widthMeasureMode == YGMeasureMode.AtMost
                            ? measuredSize.Width + paddingAndBorderAxisRow
                            : availableWidth     - marginAxisRow,
                        ownerWidth,
                        ownerWidth)
                );

                node.Layout.SetMeasuredDimension(
                    YGDimension.Height,
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Column,
                        heightMeasureMode == YGMeasureMode.Undefined ||
                        heightMeasureMode == YGMeasureMode.AtMost
                            ? measuredSize.Height + paddingAndBorderAxisColumn
                            : availableHeight     - marginAxisColumn,
                        ownerHeight,
                        ownerWidth)
                );
            }
        }

        // For nodes with no children, use the available values if they were provided,
        // or the minimum size as indicated by the padding and border sizes.
        internal static void YGNodeEmptyContainerSetMeasuredDimensions(
            YGNodeRef     node,
            float         availableWidth,
            float         availableHeight,
            YGMeasureMode widthMeasureMode,
            YGMeasureMode heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight)
        {
            var paddingAndBorderAxisRow    = YGNodePaddingAndBorderForAxis(node, YGFlexDirection.Row,    ownerWidth);
            var paddingAndBorderAxisColumn = YGNodePaddingAndBorderForAxis(node, YGFlexDirection.Column, ownerWidth);
            var marginAxisRow              = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Row,    ownerWidth));
            var marginAxisColumn           = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Column, ownerWidth));

            node.Layout.SetMeasuredDimension(
                YGDimension.Width,
                YGNodeBoundAxis(
                    node,
                    YGFlexDirection.Row,
                    widthMeasureMode == YGMeasureMode.Undefined ||
                    widthMeasureMode == YGMeasureMode.AtMost
                        ? paddingAndBorderAxisRow
                        : availableWidth - marginAxisRow,
                    ownerWidth,
                    ownerWidth)
            );

            node.Layout.SetMeasuredDimension(
                YGDimension.Height,
                YGNodeBoundAxis(
                    node,
                    YGFlexDirection.Column,
                    heightMeasureMode == YGMeasureMode.Undefined ||
                    heightMeasureMode == YGMeasureMode.AtMost
                        ? paddingAndBorderAxisColumn
                        : availableHeight - marginAxisColumn,
                    ownerHeight,
                    ownerWidth)
            );
        }

        internal static bool YGNodeFixedSizeSetMeasuredDimensions(
            YGNodeRef     node,
            float         availableWidth,
            float         availableHeight,
            YGMeasureMode widthMeasureMode,
            YGMeasureMode heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight)
        {
            if (availableWidth.HasValue()      &&
                widthMeasureMode == YGMeasureMode.AtMost && availableWidth <= 0.0f ||
                availableHeight.HasValue()      &&
                heightMeasureMode == YGMeasureMode.AtMost && availableHeight <= 0.0f ||
                widthMeasureMode  == YGMeasureMode.Exactly &&
                heightMeasureMode == YGMeasureMode.Exactly)
            {
                var marginAxisColumn = YGUnwrapFloatOptional(
                    node.getMarginForAxis(YGFlexDirection.Column, ownerWidth));
                var marginAxisRow = YGUnwrapFloatOptional(
                    node.getMarginForAxis(YGFlexDirection.Row, ownerWidth));

                node.Layout.SetMeasuredDimension(
                    YGDimension.Width,
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Row,
                        availableWidth.IsUndefined() ||
                        widthMeasureMode == YGMeasureMode.AtMost &&
                        availableWidth   < 0.0f
                            ? 0.0f
                            : availableWidth - marginAxisRow,
                        ownerWidth,
                        ownerWidth)
                );

                node.Layout.SetMeasuredDimension(
                    YGDimension.Height,
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Column,
                        availableHeight.IsUndefined() ||
                        heightMeasureMode == YGMeasureMode.AtMost &&
                        availableHeight   < 0.0f
                            ? 0.0f
                            : availableHeight - marginAxisColumn,
                        ownerHeight,
                        ownerWidth)
                );
                return true;
            }

            return false;
        }

        private static void YGZeroOutLayoutRecursivly(YGNodeRef node)
        {
            node.Layout = new YGLayout();
            node.setHasNewLayout(true);

            node.cloneChildrenIfNeeded();
            var childCount = YGNodeGetChildCount(node);
            for (var i = 0; i < childCount; i++)
            {
                var child = node.getChild(i);
                YGZeroOutLayoutRecursivly(child);
            }
        }

        internal static float YGNodeCalculateAvailableInnerDim(
            YGNodeRef       node,
            YGFlexDirection axis,
            float           availableDim,
            float           ownerDim)
        {
            var direction =
                YGFlexDirectionIsRow(axis) ? YGFlexDirection.Row : YGFlexDirection.Column;
            var dimension =
                YGFlexDirectionIsRow(axis) ? YGDimension.Width : YGDimension.Height;

            var margin           = YGUnwrapFloatOptional(node.getMarginForAxis(direction, ownerDim));
            var paddingAndBorder = YGNodePaddingAndBorderForAxis(node, direction, ownerDim);

            var availableInnerDim = availableDim - margin - paddingAndBorder;
            // Max dimension overrides predefined dimension value; Min dimension in turn
            // overrides both of the above
            if (availableInnerDim.HasValue())
            {
                // We want to make sure our available height does not violate min and max
                // constraints
                var minDimensionOptional =
                    YGResolveValue(node.Style.MinDimensions[dimension], ownerDim);
                var minInnerDim = minDimensionOptional.IsUndefined()
                    ? 0.0f
                    : minDimensionOptional.Value - paddingAndBorder;

                var maxDimensionOptional =
                    YGResolveValue(node.Style.MaxDimensions[dimension], ownerDim);

                var maxInnerDim = maxDimensionOptional.IsUndefined()
                    ? float.MaxValue
                    : maxDimensionOptional.Value - paddingAndBorder;
                availableInnerDim =
                    YGFloatMax(YGFloatMin(availableInnerDim, maxInnerDim), minInnerDim);
            }

            return availableInnerDim;
        }

        private static void YGNodeComputeFlexBasisForChildren(
            YGNodeRef       node,
            float           availableInnerWidth,
            float           availableInnerHeight,
            YGMeasureMode   widthMeasureMode,
            YGMeasureMode   heightMeasureMode,
            YGDirection     direction,
            YGFlexDirection mainAxis,
            YGConfigRef     config,
            bool            performLayout,
            float           totalOuterFlexBasis)
        {
            YGNodeRef singleFlexChild = null;
            var       children        = node.getChildren();
            var measureModeMainDim =
                YGFlexDirectionIsRow(mainAxis) ? widthMeasureMode : heightMeasureMode;
            // If there is only one child with flexGrow + flexShrink it means we can set
            // the computedFlexBasis to 0 instead of measuring and shrinking / flexing the
            // child to exactly match the remaining space
            if (measureModeMainDim == YGMeasureMode.Exactly)
                foreach (var child in children)
                    if (child.isNodeFlexible())
                    {
                        if (singleFlexChild != null                        ||
                            YGFloatsEqual(child.resolveFlexGrow(),   0.0f) ||
                            YGFloatsEqual(child.resolveFlexShrink(), 0.0f))
                        {
                            // There is already a flexible child, or this flexible child doesn't
                            // have flexGrow and flexShrink, abort
                            singleFlexChild = null;
                            break;
                        }
                        else
                        {
                            singleFlexChild = child;
                        }
                    }

            foreach (var child in children)
            {
                child.resolveDimension();
                if (child.Style.display == YGDisplay.None)
                {
                    YGZeroOutLayoutRecursivly(child);
                    child.setHasNewLayout(true);
                    child.IsDirty = false;
                    continue;
                }

                if (performLayout)
                {
                    // Set the initial position (relative to the owner).
                    var childDirection = child.resolveDirection(direction);
                    var mainDim = YGFlexDirectionIsRow(mainAxis)
                        ? availableInnerWidth
                        : availableInnerHeight;
                    var crossDim = YGFlexDirectionIsRow(mainAxis)
                        ? availableInnerHeight
                        : availableInnerWidth;
                    child.setPosition(
                        childDirection,
                        mainDim,
                        crossDim,
                        availableInnerWidth);
                }

                if (child.Style.positionType == YGPositionType.Absolute) continue;

                if (child == singleFlexChild)
                {
                    child.Layout.ComputedFlexBasisGeneration = gCurrentGenerationCount;
                    child.Layout.ComputedFlexBasis = 0f;
                }
                else
                {
                    YGNodeComputeFlexBasisForChild(
                        node,
                        child,
                        availableInnerWidth,
                        widthMeasureMode,
                        availableInnerHeight,
                        availableInnerWidth,
                        availableInnerHeight,
                        heightMeasureMode,
                        direction,
                        config);
                }

                totalOuterFlexBasis += YGUnwrapFloatOptional(
                    child.Layout.ComputedFlexBasis +
                    child.getMarginForAxis(mainAxis, availableInnerWidth));
            }
        }

        // This function assumes that all the children of node have their
        // computedFlexBasis properly computed(To do this use
        // YGNodeComputeFlexBasisForChildren function).
        // This function calculates YGCollectFlexItemsRowMeasurement
        internal static YGCollectFlexItemsRowValues YGCalculateCollectFlexItemsRowValues(
            YGNodeRef   node,
            YGDirection ownerDirection,
            float       mainAxisownerSize,
            float       availableInnerWidth,
            float       availableInnerMainDim,
            int         startOfLineIndex,
            int         lineCount)
        {
            var flexAlgoRowMeasurement = new YGCollectFlexItemsRowValues
            {
                relativeChildren = new YGVector(node.getChildren().Count)
            };

            float sizeConsumedOnCurrentLineIncludingMinConstraint = 0;
            var   mainAxis                                        = YGResolveFlexDirection(node.Style.flexDirection, node.resolveDirection(ownerDirection));
            var   isNodeFlexWrap                                  = node.Style.flexWrap != YGWrap.NoWrap;

            // Add items to the current line until it's full or we run out of items.
            var endOfLineIndex = startOfLineIndex;
            for (; endOfLineIndex < node.getChildren().Count; endOfLineIndex++)
            {
                var child = node.getChild(endOfLineIndex);
                if (child.Style.display      == YGDisplay.None ||
                    child.Style.positionType == YGPositionType.Absolute)
                    continue;

                child.setLineIndex(lineCount);
                var childMarginMainAxis = YGUnwrapFloatOptional(child.getMarginForAxis(mainAxis, availableInnerWidth));
                var flexBasisWithMinAndMaxConstraints = YGUnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        child,
                        mainAxis,
                        YGUnwrapFloatOptional(child.Layout.ComputedFlexBasis),
                        mainAxisownerSize));

                // If this is a multi-line flow and this item pushes us over the
                // available size, we've
                // hit the end of the current line. Break out of the loop and lay out
                // the current line.
                if (sizeConsumedOnCurrentLineIncludingMinConstraint +
                    flexBasisWithMinAndMaxConstraints               + childMarginMainAxis >
                    availableInnerMainDim &&
                    isNodeFlexWrap        && flexAlgoRowMeasurement.itemsOnLine > 0)
                    break;

                sizeConsumedOnCurrentLineIncludingMinConstraint +=
                    flexBasisWithMinAndMaxConstraints + childMarginMainAxis;
                flexAlgoRowMeasurement.sizeConsumedOnCurrentLine +=
                    flexBasisWithMinAndMaxConstraints + childMarginMainAxis;
                flexAlgoRowMeasurement.itemsOnLine++;

                if (child.isNodeFlexible())
                {
                    flexAlgoRowMeasurement.totalFlexGrowFactors += child.resolveFlexGrow();

                    // Unlike the grow factor, the shrink factor is scaled relative to the
                    // child dimension.
                    flexAlgoRowMeasurement.totalFlexShrinkScaledFactors +=
                        -child.resolveFlexShrink() *
                        YGUnwrapFloatOptional(child.Layout.ComputedFlexBasis);
                }

                flexAlgoRowMeasurement.relativeChildren.Add(child);
            }

            // The total flex factor needs to be floored to 1.
            if (flexAlgoRowMeasurement.totalFlexGrowFactors > 0 &&
                flexAlgoRowMeasurement.totalFlexGrowFactors < 1)
                flexAlgoRowMeasurement.totalFlexGrowFactors = 1;

            // The total flex shrink factor needs to be floored to 1.
            if (flexAlgoRowMeasurement.totalFlexShrinkScaledFactors > 0 &&
                flexAlgoRowMeasurement.totalFlexShrinkScaledFactors < 1)
                flexAlgoRowMeasurement.totalFlexShrinkScaledFactors = 1;

            flexAlgoRowMeasurement.endOfLineIndex = endOfLineIndex;
            return flexAlgoRowMeasurement;
        }

        // It distributes the free space to the flexible items and ensures that the size
        // of the flex items abide the min and max constraints. At the end of this
        // function the child nodes would have proper size. Prior using this function
        // please ensure that YGDistributeFreeSpaceFirstPass is called.
        internal static float YGDistributeFreeSpaceSecondPass(
            YGCollectFlexItemsRowValues collectedFlexItemsValues,
            YGNodeRef                   node,
            YGFlexDirection             mainAxis,
            YGFlexDirection             crossAxis,
            float                       mainAxisownerSize,
            float                       availableInnerMainDim,
            float                       availableInnerCrossDim,
            float                       availableInnerWidth,
            float                       availableInnerHeight,
            bool                        flexBasisOverflows,
            YGMeasureMode               measureModeCrossDim,
            bool                        performLayout,
            YGConfigRef                 config)
        {
            float childFlexBasis         = 0;
            float flexShrinkScaledFactor = 0;
            float flexGrowFactor         = 0;
            float deltaFreeSpace         = 0;
            var   isMainAxisRow          = YGFlexDirectionIsRow(mainAxis);
            var   isNodeFlexWrap         = node.Style.flexWrap != YGWrap.NoWrap;

            foreach (var currentRelativeChild in collectedFlexItemsValues.relativeChildren)
            {
                childFlexBasis = YGUnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        currentRelativeChild,
                        mainAxis,
                        YGUnwrapFloatOptional(
                            currentRelativeChild.Layout.ComputedFlexBasis),
                        mainAxisownerSize));
                var updatedMainSize = childFlexBasis;

                if (collectedFlexItemsValues.remainingFreeSpace.HasValue() &&
                    collectedFlexItemsValues.remainingFreeSpace < 0)
                {
                    flexShrinkScaledFactor =
                        -currentRelativeChild.resolveFlexShrink() * childFlexBasis;
                    // Is this child able to shrink?
                    if (flexShrinkScaledFactor != 0f)
                    {
                        float childSize;

                        if (collectedFlexItemsValues.totalFlexShrinkScaledFactors.HasValue() &&
                            collectedFlexItemsValues.totalFlexShrinkScaledFactors == 0f)
                            childSize = childFlexBasis + flexShrinkScaledFactor;
                        else
                            childSize = childFlexBasis +
                                collectedFlexItemsValues.remainingFreeSpace /
                                collectedFlexItemsValues.totalFlexShrinkScaledFactors *
                                flexShrinkScaledFactor;

                        updatedMainSize = YGNodeBoundAxis(
                            currentRelativeChild,
                            mainAxis,
                            childSize,
                            availableInnerMainDim,
                            availableInnerWidth);
                    }
                }
                else if (
                    collectedFlexItemsValues.remainingFreeSpace.HasValue() &&
                    collectedFlexItemsValues.remainingFreeSpace > 0)
                {
                    flexGrowFactor = currentRelativeChild.resolveFlexGrow();

                    // Is this child able to grow?
                    if (flexGrowFactor.HasValue() && flexGrowFactor != 0)
                        updatedMainSize = YGNodeBoundAxis(
                            currentRelativeChild,
                            mainAxis,
                            childFlexBasis +
                            collectedFlexItemsValues.remainingFreeSpace /
                            collectedFlexItemsValues.totalFlexGrowFactors *
                            flexGrowFactor,
                            availableInnerMainDim,
                            availableInnerWidth);
                }

                deltaFreeSpace += updatedMainSize - childFlexBasis;

                var marginMain  = YGUnwrapFloatOptional(currentRelativeChild.getMarginForAxis(mainAxis,  availableInnerWidth));
                var marginCross = YGUnwrapFloatOptional(currentRelativeChild.getMarginForAxis(crossAxis, availableInnerWidth));

                float         childCrossSize;
                var           childMainSize = updatedMainSize + marginMain;
                YGMeasureMode childCrossMeasureMode;
                var           childMainMeasureMode = YGMeasureMode.Exactly;

                if (currentRelativeChild.Style.AspectRatio.HasValue)
                {
                    childCrossSize = isMainAxisRow
                        ? (childMainSize - marginMain) / currentRelativeChild.Style.AspectRatio.Value
                        : (childMainSize - marginMain) * currentRelativeChild.Style.AspectRatio.Value;
                    childCrossMeasureMode = YGMeasureMode.Exactly;

                    childCrossSize += marginCross;
                }
                else if (
                    availableInnerCrossDim.HasValue() &&
                    !YGNodeIsStyleDimDefined(currentRelativeChild,crossAxis,                        availableInnerCrossDim)                                    &&
                    measureModeCrossDim == YGMeasureMode.Exactly                   &&
                    !(isNodeFlexWrap && flexBasisOverflows)                        &&
                    YGNodeAlignItem(node, currentRelativeChild) == YGAlign.Stretch &&
                    currentRelativeChild.marginLeadingValue(crossAxis).unit !=
                    YGUnit.Auto &&
                    currentRelativeChild.marginTrailingValue(crossAxis).unit !=
                    YGUnit.Auto)
                {
                    childCrossSize        = availableInnerCrossDim;
                    childCrossMeasureMode = YGMeasureMode.Exactly;
                }
                else if (!YGNodeIsStyleDimDefined(
                    currentRelativeChild,
                    crossAxis,
                    availableInnerCrossDim))
                {
                    childCrossSize = availableInnerCrossDim;
                    childCrossMeasureMode = childCrossSize.IsUndefined()
                        ? YGMeasureMode.Undefined
                        : YGMeasureMode.AtMost;
                }
                else
                {
                    childCrossSize =
                        YGUnwrapFloatOptional(
                            YGResolveValue(
                                currentRelativeChild.getResolvedDimension(dim[(int) crossAxis]),
                                availableInnerCrossDim)) +
                        marginCross;
                    var isLoosePercentageMeasurement =
                        currentRelativeChild.getResolvedDimension(dim[(int) crossAxis]).unit ==
                        YGUnit.Percent &&
                        measureModeCrossDim != YGMeasureMode.Exactly;
                    childCrossMeasureMode =
                        childCrossSize.IsUndefined() || isLoosePercentageMeasurement
                            ? YGMeasureMode.Undefined
                            : YGMeasureMode.Exactly;
                }

                YGConstrainMaxSizeForMode(
                    currentRelativeChild,
                    mainAxis,
                    availableInnerMainDim,
                    availableInnerWidth,
                    ref childMainMeasureMode,
                    ref childMainSize);
                YGConstrainMaxSizeForMode(
                    currentRelativeChild,
                    crossAxis,
                    availableInnerCrossDim,
                    availableInnerWidth,
                    ref childCrossMeasureMode,
                    ref childCrossSize);

                var requiresStretchLayout =
                    !YGNodeIsStyleDimDefined(
                        currentRelativeChild,
                        crossAxis,
                        availableInnerCrossDim)                                    &&
                    YGNodeAlignItem(node, currentRelativeChild) == YGAlign.Stretch &&
                    currentRelativeChild.marginLeadingValue(crossAxis).unit !=
                    YGUnit.Auto &&
                    currentRelativeChild.marginTrailingValue(crossAxis).unit != YGUnit.Auto;

                var childWidth  = isMainAxisRow ? childMainSize : childCrossSize;
                var childHeight = !isMainAxisRow ? childMainSize : childCrossSize;

                var childWidthMeasureMode  = isMainAxisRow ? childMainMeasureMode : childCrossMeasureMode;
                var childHeightMeasureMode = !isMainAxisRow ? childMainMeasureMode : childCrossMeasureMode;

                // Recursively call the layout algorithm for this child with the updated
                // main size.
                YGLayoutNodeInternal(
                    currentRelativeChild,
                    childWidth,
                    childHeight,
                    node.Layout.Direction,
                    childWidthMeasureMode,
                    childHeightMeasureMode,
                    availableInnerWidth,
                    availableInnerHeight,
                    performLayout && !requiresStretchLayout,
                    "flex",
                    config);
                node.Layout.HadOverflow = node.Layout.HadOverflow | currentRelativeChild.Layout.HadOverflow;
            }

            return deltaFreeSpace;
        }

        // It distributes the free space to the flexible items.For those flexible items
        // whose min and max constraints are triggered, those flex item's clamped size
        // is removed from the remaingfreespace.
        internal static void YGDistributeFreeSpaceFirstPass(
            YGCollectFlexItemsRowValues collectedFlexItemsValues,
            YGFlexDirection             mainAxis,
            float                       mainAxisownerSize,
            float                       availableInnerMainDim,
            float                       availableInnerWidth)
        {
            float flexShrinkScaledFactor = 0;
            float flexGrowFactor         = 0;
            float baseMainSize           = 0;
            float boundMainSize          = 0;
            float deltaFreeSpace         = 0;

            foreach (var currentRelativeChild in collectedFlexItemsValues.relativeChildren)
            {
                var childFlexBasis = YGUnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        currentRelativeChild,
                        mainAxis,
                        YGUnwrapFloatOptional(
                            currentRelativeChild.Layout.ComputedFlexBasis),
                        mainAxisownerSize));

                if (collectedFlexItemsValues.remainingFreeSpace < 0)
                {
                    flexShrinkScaledFactor =
                        -currentRelativeChild.resolveFlexShrink() * childFlexBasis;

                    // Is this child able to shrink?
                    if (flexShrinkScaledFactor.HasValue() && flexShrinkScaledFactor != 0)
                    {
                        baseMainSize = childFlexBasis +
                            collectedFlexItemsValues.remainingFreeSpace /
                            collectedFlexItemsValues.totalFlexShrinkScaledFactors *
                            flexShrinkScaledFactor;
                        boundMainSize = YGNodeBoundAxis(
                            currentRelativeChild,
                            mainAxis,
                            baseMainSize,
                            availableInnerMainDim,
                            availableInnerWidth);
                        if (baseMainSize.HasValue()  &&
                            boundMainSize.HasValue() &&
                            baseMainSize != boundMainSize)
                        {
                            // By excluding this item's size and flex factor from remaining,
                            // this item's
                            // min/max constraints should also trigger in the second pass
                            // resulting in the
                            // item's size calculation being identical in the first and second
                            // passes.
                            deltaFreeSpace += boundMainSize - childFlexBasis;
                            collectedFlexItemsValues.totalFlexShrinkScaledFactors -=
                                flexShrinkScaledFactor;
                        }
                    }
                }
                else if (
                    collectedFlexItemsValues.remainingFreeSpace.HasValue() &&
                    collectedFlexItemsValues.remainingFreeSpace > 0)
                {
                    flexGrowFactor = currentRelativeChild.resolveFlexGrow();

                    // Is this child able to grow?
                    if (flexGrowFactor.HasValue() && flexGrowFactor != 0)
                    {
                        baseMainSize = childFlexBasis +
                            collectedFlexItemsValues.remainingFreeSpace /
                            collectedFlexItemsValues.totalFlexGrowFactors * flexGrowFactor;
                        boundMainSize = YGNodeBoundAxis(
                            currentRelativeChild,
                            mainAxis,
                            baseMainSize,
                            availableInnerMainDim,
                            availableInnerWidth);

                        if (baseMainSize.HasValue()  &&
                            boundMainSize.HasValue() &&
                            baseMainSize != boundMainSize)
                        {
                            // By excluding this item's size and flex factor from remaining,
                            // this item's
                            // min/max constraints should also trigger in the second pass
                            // resulting in the
                            // item's size calculation being identical in the first and second
                            // passes.
                            deltaFreeSpace                                += boundMainSize - childFlexBasis;
                            collectedFlexItemsValues.totalFlexGrowFactors -= flexGrowFactor;
                        }
                    }
                }
            }

            collectedFlexItemsValues.remainingFreeSpace -= deltaFreeSpace;
        }

        // Do two passes over the flex items to figure out how to distribute the
        // remaining space.
        // The first pass finds the items whose min/max constraints trigger,
        // freezes them at those
        // sizes, and excludes those sizes from the remaining space. The second
        // pass sets the size
        // of each flexible item. It distributes the remaining space amongst the
        // items whose min/max
        // constraints didn't trigger in pass 1. For the other items, it sets
        // their sizes by forcing
        // their min/max constraints to trigger again.
        //
        // This two pass approach for resolving min/max constraints deviates from
        // the spec. The
        // spec (https://www.w3.org/TR/YG-flexbox-1/#resolve-flexible-lengths)
        // describes a process
        // that needs to be repeated a variable number of times. The algorithm
        // implemented here
        // won't handle all cases but it was simpler to implement and it mitigates
        // performance
        // concerns because we know exactly how many passes it'll do.
        //
        // At the end of this function the child nodes would have the proper size
        // assigned to them.
        //
        private static void YGResolveFlexibleLength(
            YGNodeRef                   node,
            YGCollectFlexItemsRowValues collectedFlexItemsValues,
            YGFlexDirection             mainAxis,
            YGFlexDirection             crossAxis,
            float                       mainAxisownerSize,
            float                       availableInnerMainDim,
            float                       availableInnerCrossDim,
            float                       availableInnerWidth,
            float                       availableInnerHeight,
            bool                        flexBasisOverflows,
            YGMeasureMode               measureModeCrossDim,
            bool                        performLayout,
            YGConfigRef                 config)
        {
            var originalFreeSpace = collectedFlexItemsValues.remainingFreeSpace;
            // First pass: detect the flex items whose min/max constraints trigger
            YGDistributeFreeSpaceFirstPass(
                collectedFlexItemsValues,
                mainAxis,
                mainAxisownerSize,
                availableInnerMainDim,
                availableInnerWidth);

            // Second pass: resolve the sizes of the flexible items
            var distributedFreeSpace = YGDistributeFreeSpaceSecondPass(
                collectedFlexItemsValues,
                node,
                mainAxis,
                crossAxis,
                mainAxisownerSize,
                availableInnerMainDim,
                availableInnerCrossDim,
                availableInnerWidth,
                availableInnerHeight,
                flexBasisOverflows,
                measureModeCrossDim,
                performLayout,
                config);

            collectedFlexItemsValues.remainingFreeSpace =
                originalFreeSpace - distributedFreeSpace;
        }

        internal static void YGJustifyMainAxis(
            YGNodeRef                   node,
            YGCollectFlexItemsRowValues collectedFlexItemsValues,
            int                         startOfLineIndex,
            YGFlexDirection             mainAxis,
            YGFlexDirection             crossAxis,
            YGMeasureMode               measureModeMainDim,
            YGMeasureMode               measureModeCrossDim,
            float                       mainAxisownerSize,
            float                       ownerWidth,
            float                       availableInnerMainDim,
            float                       availableInnerCrossDim,
            float                       availableInnerWidth,
            bool                        performLayout)
        {
            var style                        = node.Style;
            var leadingPaddingAndBorderMain  = YGUnwrapFloatOptional(node.getLeadingPaddingAndBorder(mainAxis, ownerWidth));
            var trailingPaddingAndBorderMain = YGUnwrapFloatOptional(node.getTrailingPaddingAndBorder(mainAxis, ownerWidth));
            // If we are using "at most" rules in the main axis, make sure that
            // remainingFreeSpace is 0 when min main dimension is not given
            if (measureModeMainDim                          == YGMeasureMode.AtMost &&
                collectedFlexItemsValues.remainingFreeSpace > 0)
            {
                if (style.MinDimensions[dim[(int) mainAxis]].unit != YGUnit.Undefined &&
                    YGResolveValue(style.MinDimensions[dim[(int) mainAxis]], mainAxisownerSize).HasValue)
                {
                    // This condition makes sure that if the size of main dimension(after
                    // considering child nodes main dim, leading and trailing padding etc)
                    // falls below min dimension, then the remainingFreeSpace is reassigned
                    // considering the min dimension

                    // `minAvailableMainDim` denotes minimum available space in which child
                    // can be laid out, it will exclude space consumed by padding and border.
                    var minAvailableMainDim = YGUnwrapFloatOptional(
                            YGResolveValue(style.MinDimensions[dim[(int) mainAxis]],
                                mainAxisownerSize)) - leadingPaddingAndBorderMain - trailingPaddingAndBorderMain;
                    var occupiedSpaceByChildNodes = availableInnerMainDim - collectedFlexItemsValues.remainingFreeSpace;
                    collectedFlexItemsValues.remainingFreeSpace =
                        YGFloatMax(0, minAvailableMainDim - occupiedSpaceByChildNodes);
                }
                else
                {
                    collectedFlexItemsValues.remainingFreeSpace = 0;
                }
            }

            var numberOfAutoMarginsOnCurrentLine = 0;
            for (var i = startOfLineIndex;
                i < collectedFlexItemsValues.endOfLineIndex;
                i++)
            {
                var child = node.getChild(i);
                if (child.Style.positionType == YGPositionType.Relative)
                {
                    if (child.marginLeadingValue(mainAxis).unit == YGUnit.Auto) numberOfAutoMarginsOnCurrentLine++;

                    if (child.marginTrailingValue(mainAxis).unit == YGUnit.Auto) numberOfAutoMarginsOnCurrentLine++;
                }
            }

            // In order to position the elements in the main axis, we have two
            // controls. The space between the beginning and the first element
            // and the space between each two elements.
            float leadingMainDim = 0;
            float betweenMainDim = 0;
            var   justifyContent = node.Style.justifyContent;

            if (numberOfAutoMarginsOnCurrentLine == 0)
                switch (justifyContent)
                {
                case YGJustify.Center:
                    leadingMainDim = collectedFlexItemsValues.remainingFreeSpace / 2;
                    break;
                case YGJustify.FlexEnd:
                    leadingMainDim = collectedFlexItemsValues.remainingFreeSpace;
                    break;
                case YGJustify.SpaceBetween:
                    if (collectedFlexItemsValues.itemsOnLine > 1)
                        betweenMainDim =
                            YGFloatMax(collectedFlexItemsValues.remainingFreeSpace, 0) /
                            (collectedFlexItemsValues.itemsOnLine - 1);
                    else
                        betweenMainDim = 0;

                    break;
                case YGJustify.SpaceEvenly:
                    // Space is distributed evenly across all elements
                    betweenMainDim = collectedFlexItemsValues.remainingFreeSpace /
                        (collectedFlexItemsValues.itemsOnLine + 1);
                    leadingMainDim = betweenMainDim;
                    break;
                case YGJustify.SpaceAround:
                    // Space on the edges is half of the space between elements
                    betweenMainDim = collectedFlexItemsValues.remainingFreeSpace /
                        collectedFlexItemsValues.itemsOnLine;
                    leadingMainDim = betweenMainDim / 2;
                    break;
                case YGJustify.FlexStart:
                    break;
                }

            collectedFlexItemsValues.mainDim =
                leadingPaddingAndBorderMain + leadingMainDim;
            collectedFlexItemsValues.crossDim = 0;

            float maxAscentForCurrentLine  = 0;
            float maxDescentForCurrentLine = 0;
            var   isNodeBaselineLayout     = YGIsBaselineLayout(node);
            for (var i = startOfLineIndex;
                i < collectedFlexItemsValues.endOfLineIndex;
                i++)
            {
                var child       = node.getChild(i);
                var childStyle  = child.Style;
                var childLayout = child.Layout;
                if (childStyle.display == YGDisplay.None) continue;

                if (childStyle.positionType == YGPositionType.Absolute &&
                    child.isLeadingPositionDefined(mainAxis))
                {
                    if (performLayout)
                        child.Layout.Position[pos[(int)mainAxis]] = 
                            YGUnwrapFloatOptional(
                                child.getLeadingPosition(mainAxis, availableInnerMainDim)) +
                            node.getLeadingBorder(mainAxis)                                +
                            YGUnwrapFloatOptional(
                                child.getLeadingMargin(mainAxis, availableInnerWidth));
                }
                else
                {
                    // Now that we placed the element, we need to update the variables.
                    // We need to do that only for relative elements. Absolute elements
                    // do not take part in that phase.
                    if (childStyle.positionType == YGPositionType.Relative)
                    {
                        if (child.marginLeadingValue(mainAxis).unit == YGUnit.Auto)
                            collectedFlexItemsValues.mainDim +=
                                collectedFlexItemsValues.remainingFreeSpace /
                                numberOfAutoMarginsOnCurrentLine;

                        if (performLayout)
                            child.Layout.Position[pos[(int)mainAxis]] = 
                                childLayout.Position[ pos[(int) mainAxis]] +
                                collectedFlexItemsValues.mainDim;

                        if (child.marginTrailingValue(mainAxis).unit == YGUnit.Auto)
                            collectedFlexItemsValues.mainDim +=
                                collectedFlexItemsValues.remainingFreeSpace /
                                numberOfAutoMarginsOnCurrentLine;

                        var canSkipFlex =
                            !performLayout && measureModeCrossDim == YGMeasureMode.Exactly;
                        if (canSkipFlex)
                        {
                            // If we skipped the flex step, then we can't rely on the
                            // measuredDims because
                            // they weren't computed. This means we can't call
                            // YGNodeDimWithMargin.
                            collectedFlexItemsValues.mainDim += betweenMainDim +
                                YGUnwrapFloatOptional(
                                    child.getMarginForAxis(
                                        mainAxis,
                                        availableInnerWidth)) +
                                YGUnwrapFloatOptional(childLayout.ComputedFlexBasis);
                            collectedFlexItemsValues.crossDim = availableInnerCrossDim;
                        }
                        else
                        {
                            // The main dimension is the sum of all the elements dimension plus
                            // the spacing.
                            collectedFlexItemsValues.mainDim += betweenMainDim +
                                YGNodeDimWithMargin(child, mainAxis, availableInnerWidth);

                            if (isNodeBaselineLayout)
                            {
                                // If the child is baseline aligned then the cross dimension is
                                // calculated by adding maxAscent and maxDescent from the baseline.
                                var ascent = YGBaseline(child) +
                                    YGUnwrapFloatOptional(
                                        child.getLeadingMargin(
                                            YGFlexDirection.Column,
                                            availableInnerWidth));
                                var descent =
                                    child.Layout.MeasuredHeight +
                                    YGUnwrapFloatOptional(
                                        child.getMarginForAxis(
                                            YGFlexDirection.Column,
                                            availableInnerWidth)) -
                                    ascent;

                                maxAscentForCurrentLine =
                                    YGFloatMax(maxAscentForCurrentLine, ascent);
                                maxDescentForCurrentLine =
                                    YGFloatMax(maxDescentForCurrentLine, descent);
                            }
                            else
                            {
                                // The cross dimension is the max of the elements dimension since
                                // there can only be one element in that cross dimension in the case
                                // when the items are not baseline aligned
                                collectedFlexItemsValues.crossDim = YGFloatMax(
                                    collectedFlexItemsValues.crossDim,
                                    YGNodeDimWithMargin(child, crossAxis, availableInnerWidth));
                            }
                        }
                    }
                    else if (performLayout)
                    {
                        child.Layout.Position[pos[(int)mainAxis]] = 
                            childLayout.Position[pos[(int) mainAxis]] +
                            node.getLeadingBorder(mainAxis)                 + leadingMainDim;
                    }
                }
            }

            collectedFlexItemsValues.mainDim += trailingPaddingAndBorderMain;

            if (isNodeBaselineLayout)
                collectedFlexItemsValues.crossDim =
                    maxAscentForCurrentLine + maxDescentForCurrentLine;
        }

        //
        // This is the main routine that implements a subset of the flexbox layout
        // algorithm
        // described in the W3C YG documentation: https://www.w3.org/TR/YG3-flexbox/.
        //
        // Limitations of this algorithm, compared to the full standard:
        //  * Display property is always assumed to be 'flex' except for Text nodes,
        //  which
        //    are assumed to be 'inline-flex'.
        //  * The 'zIndex' property (or any form of z ordering) is not supported. Nodes
        //  are
        //    stacked in document order.
        //  * The 'order' property is not supported. The order of flex items is always
        //  defined
        //    by document order.
        //  * The 'visibility' property is always assumed to be 'visible'. Values of
        //  'collapse'
        //    and 'hidden' are not supported.
        //  * There is no support for forced breaks.
        //  * It does not support vertical inline directions (top-to-bottom or
        //  bottom-to-top text).
        //
        // Deviations from standard:
        //  * Section 4.5 of the spec indicates that all flex items have a default
        //  minimum
        //    main size. For text blocks, for example, this is the width of the widest
        //    word.
        //    Calculating the minimum width is expensive, so we forego it and assume a
        //    default
        //    minimum main size of 0.
        //  * Min/Max sizes in the main axis are not honored when resolving flexible
        //  lengths.
        //  * The spec indicates that the default value for 'flexDirection' is 'row',
        //  but
        //    the algorithm below assumes a default of 'column'.
        //
        // Input parameters:
        //    - node: current node to be sized and layed out
        //    - availableWidth & availableHeight: available size to be used for sizing
        //    the node
        //      or YGUndefined if the size is not available; interpretation depends on
        //      layout
        //      flags
        //    - ownerDirection: the inline (text) direction within the owner
        //    (left-to-right or
        //      right-to-left)
        //    - widthMeasureMode: indicates the sizing rules for the width (see below
        //    for explanation)
        //    - heightMeasureMode: indicates the sizing rules for the height (see below
        //    for explanation)
        //    - performLayout: specifies whether the caller is interested in just the
        //    dimensions
        //      of the node or it requires the entire node and its subtree to be layed
        //      out
        //      (with final positions)
        //
        // Details:
        //    This routine is called recursively to lay out subtrees of flexbox
        //    elements. It uses the
        //    information in node.style, which is treated as a read-only input. It is
        //    responsible for
        //    setting the layout.direction and layout.measuredDimensions fields for the
        //    input node as well
        //    as the layout.position and layout.lineIndex fields for its child nodes.
        //    The
        //    layout.measuredDimensions field includes any border or padding for the
        //    node but does
        //    not include margins.
        //
        //    The spec describes four different layout modes: "fill available", "max
        //    content", "min
        //    content",
        //    and "fit content". Of these, we don't use "min content" because we don't
        //    support default
        //    minimum main sizes (see above for details). Each of our measure modes maps
        //    to a layout mode
        //    from the spec (https://www.w3.org/TR/YG3-sizing/#terms):
        //      - YGMeasureMode.Undefined: max content
        //      - YGMeasureMode.Exactly: fill available
        //      - YGMeasureMode.AtMost: fit content
        //
        //    When calling YGNodelayoutImpl and YGLayoutNodeInternal, if the caller
        //    passes an available size of undefined then it must also pass a measure
        //    mode of YGMeasureMode.Undefined in that dimension.
        //
        internal static void YGNodelayoutImpl(
            YGNodeRef     node,
            float         availableWidth,
            float         availableHeight,
            YGDirection   ownerDirection,
            YGMeasureMode widthMeasureMode,
            YGMeasureMode heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight,
            bool          performLayout,
            YGConfigRef   config)
        {
            YGAssertWithNode(
                node,
                availableWidth.IsUndefined()
                    ? widthMeasureMode == YGMeasureMode.Undefined
                    : true,
                "availableWidth is indefinite so widthMeasureMode must be YGMeasureMode.Undefined");
            YGAssertWithNode(
                node,
                availableHeight.IsUndefined()
                    ? heightMeasureMode == YGMeasureMode.Undefined
                    : true,
                "availableHeight is indefinite so heightMeasureMode must be YGMeasureMode.Undefined");

            // Set the resolved resolution in the node's layout.
            var direction = node.resolveDirection(ownerDirection);
            node.setLayoutDirection(direction);

            var flexRowDirection    = YGResolveFlexDirection(YGFlexDirection.Row,    direction);
            var flexColumnDirection = YGResolveFlexDirection(YGFlexDirection.Column, direction);

            node.setLayoutMargin(
                YGUnwrapFloatOptional(
                    node.getLeadingMargin(flexRowDirection, ownerWidth)),
                YGEdge.Start);
            node.setLayoutMargin(
                YGUnwrapFloatOptional(
                    node.getTrailingMargin(flexRowDirection, ownerWidth)),
                YGEdge.End);
            node.setLayoutMargin(
                YGUnwrapFloatOptional(
                    node.getLeadingMargin(flexColumnDirection, ownerWidth)),
                YGEdge.Top);
            node.setLayoutMargin(
                YGUnwrapFloatOptional(
                    node.getTrailingMargin(flexColumnDirection, ownerWidth)),
                YGEdge.Bottom);

            node.setLayoutBorder(node.getLeadingBorder(flexRowDirection),    YGEdge.Start);
            node.setLayoutBorder(node.getTrailingBorder(flexRowDirection),   YGEdge.End);
            node.setLayoutBorder(node.getLeadingBorder(flexColumnDirection), YGEdge.Top);
            node.setLayoutBorder(
                node.getTrailingBorder(flexColumnDirection),
                YGEdge.Bottom);

            node.setLayoutPadding(
                YGUnwrapFloatOptional(
                    node.getLeadingPadding(flexRowDirection, ownerWidth)),
                YGEdge.Start);
            node.setLayoutPadding(
                YGUnwrapFloatOptional(
                    node.getTrailingPadding(flexRowDirection, ownerWidth)),
                YGEdge.End);
            node.setLayoutPadding(
                YGUnwrapFloatOptional(
                    node.getLeadingPadding(flexColumnDirection, ownerWidth)),
                YGEdge.Top);
            node.setLayoutPadding(
                YGUnwrapFloatOptional(
                    node.getTrailingPadding(flexColumnDirection, ownerWidth)),
                YGEdge.Bottom);

            if (node.MeasureFunc != null)
            {
                YGNodeWithMeasureFuncSetMeasuredDimensions(
                    node,
                    availableWidth,
                    availableHeight,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight);
                return;
            }

            var childCount = YGNodeGetChildCount(node);
            if (childCount == 0)
            {
                YGNodeEmptyContainerSetMeasuredDimensions(
                    node,
                    availableWidth,
                    availableHeight,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight);
                return;
            }

            // If we're not being asked to perform a full layout we can skip the algorithm
            // if we already know the size
            if (!performLayout &&
                YGNodeFixedSizeSetMeasuredDimensions(
                    node,
                    availableWidth,
                    availableHeight,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight))
                return;

            // At this point we know we're going to perform work. Ensure that each child
            // has a mutable copy.
            node.cloneChildrenIfNeeded();
            // Reset layout flags, as they could have changed.
            node.Layout.HadOverflow = false;

            // STEP 1: CALCULATE VALUES FOR REMAINDER OF ALGORITHM
            var mainAxis       = YGResolveFlexDirection(node.Style.flexDirection, direction);
            var crossAxis      = YGFlexDirectionCross(mainAxis, direction);
            var isMainAxisRow  = YGFlexDirectionIsRow(mainAxis);
            var isNodeFlexWrap = node.Style.flexWrap != YGWrap.NoWrap;

            var mainAxisownerSize  = isMainAxisRow ? ownerWidth : ownerHeight;
            var crossAxisownerSize = isMainAxisRow ? ownerHeight : ownerWidth;

            var leadingPaddingAndBorderCross = YGUnwrapFloatOptional(node.getLeadingPaddingAndBorder(crossAxis, ownerWidth));
            var paddingAndBorderAxisMain     = YGNodePaddingAndBorderForAxis(node, mainAxis,  ownerWidth);
            var paddingAndBorderAxisCross    = YGNodePaddingAndBorderForAxis(node, crossAxis, ownerWidth);

            var measureModeMainDim  = isMainAxisRow ? widthMeasureMode : heightMeasureMode;
            var measureModeCrossDim = isMainAxisRow ? heightMeasureMode : widthMeasureMode;

            var paddingAndBorderAxisRow    = isMainAxisRow ? paddingAndBorderAxisMain : paddingAndBorderAxisCross;
            var paddingAndBorderAxisColumn = isMainAxisRow ? paddingAndBorderAxisCross : paddingAndBorderAxisMain;

            var marginAxisRow    = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Row,    ownerWidth));
            var marginAxisColumn = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Column, ownerWidth));

            var minInnerWidth = YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.Style.MinDimensions[YGDimension.Width],
                        ownerWidth)) -
                paddingAndBorderAxisRow;
            var maxInnerWidth =
                YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.Style.MaxDimensions[YGDimension.Width],
                        ownerWidth)) -
                paddingAndBorderAxisRow;
            var minInnerHeight =
                YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.Style.MinDimensions[YGDimension.Height],
                        ownerHeight)) -
                paddingAndBorderAxisColumn;
            var maxInnerHeight =
                YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.Style.MaxDimensions[YGDimension.Height],
                        ownerHeight)) -
                paddingAndBorderAxisColumn;

            var minInnerMainDim = isMainAxisRow ? minInnerWidth : minInnerHeight;
            var maxInnerMainDim = isMainAxisRow ? maxInnerWidth : maxInnerHeight;

            // STEP 2: DETERMINE AVAILABLE SIZE IN MAIN AND CROSS DIRECTIONS

            var availableInnerWidth = YGNodeCalculateAvailableInnerDim(
                node,
                YGFlexDirection.Row,
                availableWidth,
                ownerWidth);
            var availableInnerHeight = YGNodeCalculateAvailableInnerDim(
                node,
                YGFlexDirection.Column,
                availableHeight,
                ownerHeight);

            var availableInnerMainDim  = isMainAxisRow ? availableInnerWidth : availableInnerHeight;
            var availableInnerCrossDim = isMainAxisRow ? availableInnerHeight : availableInnerWidth;

            float totalOuterFlexBasis = 0;

            // STEP 3: DETERMINE FLEX BASIS FOR EACH ITEM

            YGNodeComputeFlexBasisForChildren(
                node,
                availableInnerWidth,
                availableInnerHeight,
                widthMeasureMode,
                heightMeasureMode,
                direction,
                mainAxis,
                config,
                performLayout,
                totalOuterFlexBasis);

            var flexBasisOverflows = measureModeMainDim == YGMeasureMode.Undefined
                ? false
                : totalOuterFlexBasis > availableInnerMainDim;
            if (isNodeFlexWrap && flexBasisOverflows &&
                measureModeMainDim == YGMeasureMode.AtMost)
                measureModeMainDim = YGMeasureMode.Exactly;
            // STEP 4: COLLECT FLEX ITEMS INTO FLEX LINES

            // Indexes of children that represent the first and last items in the line.
            var startOfLineIndex = 0;
            var endOfLineIndex   = 0;

            // Number of lines.
            var lineCount = 0;

            // Accumulated cross dimensions of all lines so far.
            float totalLineCrossDim = 0;

            // Max main dimension of all the lines.
            float                       maxLineMainDim = 0;
            YGCollectFlexItemsRowValues collectedFlexItemsValues;
            for (;
                endOfLineIndex < childCount;
                lineCount++, startOfLineIndex = endOfLineIndex)
            {
                collectedFlexItemsValues = YGCalculateCollectFlexItemsRowValues(
                    node,
                    ownerDirection,
                    mainAxisownerSize,
                    availableInnerWidth,
                    availableInnerMainDim,
                    startOfLineIndex,
                    lineCount);
                endOfLineIndex = collectedFlexItemsValues.endOfLineIndex;

                // If we don't need to measure the cross axis, we can skip the entire flex
                // step.
                var canSkipFlex = !performLayout && measureModeCrossDim == YGMeasureMode.Exactly;

                // STEP 5: RESOLVING FLEXIBLE LENGTHS ON MAIN AXIS
                // Calculate the remaining available space that needs to be allocated.
                // If the main dimension size isn't known, it is computed based on
                // the line length, so there's no more space left to distribute.

                var sizeBasedOnContent = false;
                // If we don't measure with exact main dimension we want to ensure we don't
                // violate min and max
                if (measureModeMainDim != YGMeasureMode.Exactly)
                {
                    if (minInnerMainDim.HasValue() &&
                        collectedFlexItemsValues.sizeConsumedOnCurrentLine <
                        minInnerMainDim)
                    {
                        availableInnerMainDim = minInnerMainDim;
                    }
                    else if (
                        maxInnerMainDim.HasValue() &&
                        collectedFlexItemsValues.sizeConsumedOnCurrentLine >
                        maxInnerMainDim)
                    {
                        availableInnerMainDim = maxInnerMainDim;
                    }
                    else
                    {
                        if ((collectedFlexItemsValues.totalFlexGrowFactors.IsUndefined() &&
                                collectedFlexItemsValues.totalFlexGrowFactors == 0 || node.resolveFlexGrow().IsUndefined() &&
                                node.resolveFlexGrow() == 0f))
                            availableInnerMainDim = collectedFlexItemsValues.sizeConsumedOnCurrentLine;

                        sizeBasedOnContent = true;
                    }
                }

                if (!sizeBasedOnContent && availableInnerMainDim.HasValue())
                    collectedFlexItemsValues.remainingFreeSpace = availableInnerMainDim -
                        collectedFlexItemsValues.sizeConsumedOnCurrentLine;
                else if (collectedFlexItemsValues.sizeConsumedOnCurrentLine < 0)
                    collectedFlexItemsValues.remainingFreeSpace =
                        -collectedFlexItemsValues.sizeConsumedOnCurrentLine;

                if (!canSkipFlex)
                    YGResolveFlexibleLength(
                        node,
                        collectedFlexItemsValues,
                        mainAxis,
                        crossAxis,
                        mainAxisownerSize,
                        availableInnerMainDim,
                        availableInnerCrossDim,
                        availableInnerWidth,
                        availableInnerHeight,
                        flexBasisOverflows,
                        measureModeCrossDim,
                        performLayout,
                        config);

                node.Layout.HadOverflow = node.Layout.HadOverflow | (collectedFlexItemsValues.remainingFreeSpace < 0);

                // STEP 6: MAIN-AXIS JUSTIFICATION & CROSS-AXIS SIZE DETERMINATION

                // At this point, all the children have their dimensions set in the main
                // axis.
                // Their dimensions are also set in the cross axis with the exception of
                // items
                // that are aligned "stretch". We need to compute these stretch values and
                // set the final positions.

                YGJustifyMainAxis(
                    node,
                    collectedFlexItemsValues,
                    startOfLineIndex,
                    mainAxis,
                    crossAxis,
                    measureModeMainDim,
                    measureModeCrossDim,
                    mainAxisownerSize,
                    ownerWidth,
                    availableInnerMainDim,
                    availableInnerCrossDim,
                    availableInnerWidth,
                    performLayout);

                var containerCrossAxis = availableInnerCrossDim;
                if (measureModeCrossDim == YGMeasureMode.Undefined ||
                    measureModeCrossDim == YGMeasureMode.AtMost)
                    containerCrossAxis =
                        YGNodeBoundAxis(
                            node,
                            crossAxis,
                            collectedFlexItemsValues.crossDim + paddingAndBorderAxisCross,
                            crossAxisownerSize,
                            ownerWidth) -
                        paddingAndBorderAxisCross;

                // If there's no flex wrap, the cross dimension is defined by the container.
                if (!isNodeFlexWrap && measureModeCrossDim == YGMeasureMode.Exactly) collectedFlexItemsValues.crossDim = availableInnerCrossDim;

                // Clamp to the min/max size specified on the container.
                collectedFlexItemsValues.crossDim =
                    YGNodeBoundAxis(
                        node,
                        crossAxis,
                        collectedFlexItemsValues.crossDim + paddingAndBorderAxisCross,
                        crossAxisownerSize,
                        ownerWidth) -
                    paddingAndBorderAxisCross;

                // STEP 7: CROSS-AXIS ALIGNMENT
                // We can skip child alignment if we're just measuring the container.
                if (performLayout)
                    for (var i = startOfLineIndex; i < endOfLineIndex; i++)
                    {
                        var child = node.getChild(i);
                        if (child.Style.display == YGDisplay.None) continue;

                        if (child.Style.positionType == YGPositionType.Absolute)
                        {
                            // If the child is absolutely positioned and has a
                            // top/left/bottom/right set, override
                            // all the previously computed positions to set it correctly.
                            var isChildLeadingPosDefined =
                                child.isLeadingPositionDefined(crossAxis);
                            if (isChildLeadingPosDefined)
                                child.Layout.Position[pos[(int)crossAxis]] = 
                                    YGUnwrapFloatOptional(
                                        child.getLeadingPosition(
                                            crossAxis,
                                            availableInnerCrossDim)) +
                                    node.getLeadingBorder(crossAxis) +
                                    YGUnwrapFloatOptional(
                                        child.getLeadingMargin(
                                            crossAxis,
                                            availableInnerWidth));

                            // If leading position is not defined or calculations result in Nan,
                            // default to border + margin
                            if (!isChildLeadingPosDefined ||
                                child.Layout.Position[ pos[(int) crossAxis]].IsUndefined())
                                child.Layout.Position[pos[(int)crossAxis]] = 
                                    node.getLeadingBorder(crossAxis) + YGUnwrapFloatOptional( child.getLeadingMargin( crossAxis, availableInnerWidth));
                        }
                        else
                        {
                            var leadingCrossDim = leadingPaddingAndBorderCross;

                            // For a relative children, we're either using alignItems (owner) or
                            // alignSelf (child) in order to determine the position in the cross
                            // axis
                            var alignItem = YGNodeAlignItem(node, child);

                            // If the child uses align stretch, we need to lay it out one more
                            // time, this time
                            // forcing the cross-axis size to be the computed cross size for the
                            // current line.
                            if (alignItem                                 == YGAlign.Stretch &&
                                child.marginLeadingValue(crossAxis).unit  != YGUnit.Auto     &&
                                child.marginTrailingValue(crossAxis).unit != YGUnit.Auto)
                            {
                                // If the child defines a definite size for its cross axis, there's
                                // no need to stretch.
                                if (!YGNodeIsStyleDimDefined(
                                    child,
                                    crossAxis,
                                    availableInnerCrossDim))
                                {
                                    var childMainSize = child.Layout.GetMeasuredDimension(dim[(int) mainAxis]);
                                    var childCrossSize =
                                        child.Style.AspectRatio.HasValue
                                            ? YGUnwrapFloatOptional(
                                                child.getMarginForAxis( crossAxis, availableInnerWidth)) +
                                            (isMainAxisRow
                                                ? childMainSize / child.Style.AspectRatio.Value
                                                : childMainSize * child.Style.AspectRatio.Value)
                                            : collectedFlexItemsValues.crossDim;

                                    childMainSize += YGUnwrapFloatOptional(
                                        child.getMarginForAxis(mainAxis, availableInnerWidth));

                                    var childMainMeasureMode  = YGMeasureMode.Exactly;
                                    var childCrossMeasureMode = YGMeasureMode.Exactly;
                                    YGConstrainMaxSizeForMode(
                                        child,
                                        mainAxis,
                                        availableInnerMainDim,
                                        availableInnerWidth,
                                        ref childMainMeasureMode,
                                        ref childMainSize);
                                    YGConstrainMaxSizeForMode(
                                        child,
                                        crossAxis,
                                        availableInnerCrossDim,
                                        availableInnerWidth,
                                        ref childCrossMeasureMode,
                                        ref childCrossSize);

                                    var childWidth  = isMainAxisRow ? childMainSize : childCrossSize;
                                    var childHeight = !isMainAxisRow ? childMainSize : childCrossSize;

                                    var childWidthMeasureMode =
                                        childWidth.IsUndefined() ? YGMeasureMode.Undefined : YGMeasureMode.Exactly;
                                    var childHeightMeasureMode =
                                        childHeight.IsUndefined() ? YGMeasureMode.Undefined : YGMeasureMode.Exactly;

                                    YGLayoutNodeInternal(
                                        child,
                                        childWidth,
                                        childHeight,
                                        direction,
                                        childWidthMeasureMode,
                                        childHeightMeasureMode,
                                        availableInnerWidth,
                                        availableInnerHeight,
                                        true,
                                        "stretch",
                                        config);
                                }
                            }
                            else
                            {
                                var remainingCrossDim = containerCrossAxis - YGNodeDimWithMargin(child, crossAxis, availableInnerWidth);

                                if (child.marginLeadingValue(crossAxis).unit  == YGUnit.Auto &&
                                    child.marginTrailingValue(crossAxis).unit == YGUnit.Auto)
                                {
                                    leadingCrossDim += YGFloatMax(0.0f, remainingCrossDim / 2);
                                }
                                else if (
                                    child.marginTrailingValue(crossAxis).unit == YGUnit.Auto)
                                {
                                    // No-Op
                                }
                                else if (
                                    child.marginLeadingValue(crossAxis).unit == YGUnit.Auto)
                                {
                                    leadingCrossDim += YGFloatMax(0.0f, remainingCrossDim);
                                }
                                else if (alignItem == YGAlign.FlexStart)
                                {
                                    // No-Op
                                }
                                else if (alignItem == YGAlign.Center)
                                {
                                    leadingCrossDim += remainingCrossDim / 2;
                                }
                                else
                                {
                                    leadingCrossDim += remainingCrossDim;
                                }
                            }

                            // And we apply the position
                            child.Layout.Position[pos[(int)crossAxis]] = 
                                child.Layout.Position[ pos[(int) crossAxis]] + totalLineCrossDim +
                                leadingCrossDim;
                        }
                    }

                totalLineCrossDim += collectedFlexItemsValues.crossDim;
                maxLineMainDim =
                    YGFloatMax(maxLineMainDim, collectedFlexItemsValues.mainDim);
            }

            // STEP 8: MULTI-LINE CONTENT ALIGNMENT
            // currentLead stores the size of the cross dim
            if (performLayout && (lineCount > 1 || YGIsBaselineLayout(node)))
            {
                float crossDimLead = 0;
                var   currentLead  = leadingPaddingAndBorderCross;
                if (availableInnerCrossDim.HasValue())
                {
                    var remainingAlignContentDim = availableInnerCrossDim - totalLineCrossDim;
                    switch (node.Style.alignContent)
                    {
                    case YGAlign.FlexEnd:
                        currentLead += remainingAlignContentDim;
                        break;
                    case YGAlign.Center:
                        currentLead += remainingAlignContentDim / 2;
                        break;
                    case YGAlign.Stretch:
                        if (availableInnerCrossDim > totalLineCrossDim) crossDimLead = remainingAlignContentDim / lineCount;

                        break;
                    case YGAlign.SpaceAround:
                        if (availableInnerCrossDim > totalLineCrossDim)
                        {
                            currentLead += remainingAlignContentDim / (2 * lineCount);
                            if (lineCount > 1) crossDimLead = remainingAlignContentDim / lineCount;
                        }
                        else
                        {
                            currentLead += remainingAlignContentDim / 2;
                        }

                        break;
                    case YGAlign.SpaceBetween:
                        if (availableInnerCrossDim > totalLineCrossDim && lineCount > 1) crossDimLead = remainingAlignContentDim / (lineCount - 1);

                        break;
                    case YGAlign.Auto:
                    case YGAlign.FlexStart:
                    case YGAlign.Baseline:
                        break;
                    }
                }

                var endIndex = 0;
                for (var i = 0; i < lineCount; i++)
                {
                    var startIndex = endIndex;
                    int ii;

                    // compute the line's height and find the endIndex
                    float lineHeight               = 0;
                    float maxAscentForCurrentLine  = 0;
                    float maxDescentForCurrentLine = 0;
                    for (ii = startIndex; ii < childCount; ii++)
                    {
                        var child = node.getChild(ii);
                        if (child.Style.display == YGDisplay.None) continue;

                        if (child.Style.positionType == YGPositionType.Relative)
                        {
                            if (child.getLineIndex() != i) break;

                            if (YGNodeIsLayoutDimDefined(child, crossAxis))
                                lineHeight = YGFloatMax(
                                    lineHeight,
                                    child.Layout.GetMeasuredDimension(dim[(int) crossAxis]) +
                                    YGUnwrapFloatOptional(
                                        child.getMarginForAxis(
                                            crossAxis,
                                            availableInnerWidth)));

                            if (YGNodeAlignItem(node, child) == YGAlign.Baseline)
                            {
                                var ascent = YGBaseline(child) +
                                    YGUnwrapFloatOptional(
                                        child.getLeadingMargin(
                                            YGFlexDirection.Column,
                                            availableInnerWidth));
                                var descent =
                                    child.Layout.MeasuredHeight +
                                    YGUnwrapFloatOptional(
                                        child.getMarginForAxis(
                                            YGFlexDirection.Column,
                                            availableInnerWidth)) -
                                    ascent;
                                maxAscentForCurrentLine =
                                    YGFloatMax(maxAscentForCurrentLine, ascent);
                                maxDescentForCurrentLine =
                                    YGFloatMax(maxDescentForCurrentLine, descent);
                                lineHeight = YGFloatMax(
                                    lineHeight,
                                    maxAscentForCurrentLine + maxDescentForCurrentLine);
                            }
                        }
                    }

                    endIndex   =  ii;
                    lineHeight += crossDimLead;

                    if (performLayout)
                        for (ii = startIndex; ii < endIndex; ii++)
                        {
                            var child = node.getChild(ii);
                            if (child.Style.display == YGDisplay.None) continue;

                            if (child.Style.positionType == YGPositionType.Relative)
                                switch (YGNodeAlignItem(node, child))
                                {
                                case YGAlign.FlexStart:
                                {
                                    child.Layout.Position[pos[(int)crossAxis]] = 
                                        currentLead +
                                        YGUnwrapFloatOptional(child.getLeadingMargin(crossAxis, availableInnerWidth));
                                    break;
                                }
                                case YGAlign.FlexEnd:
                                {
                                    child.Layout.Position[pos[(int)crossAxis]] = 
                                        currentLead + lineHeight -
                                        YGUnwrapFloatOptional(
                                            child.getTrailingMargin(
                                                crossAxis,
                                                availableInnerWidth)) -
                                        child.Layout.GetMeasuredDimension(dim[(int) crossAxis]);
                                    break;
                                }
                                case YGAlign.Center:
                                {
                                    var childHeight =
                                        child.Layout.GetMeasuredDimension(dim[(int) crossAxis]);

                                    child.Layout.Position[pos[(int)crossAxis]] = 
                                        currentLead + (lineHeight - childHeight) / 2;
                                    break;
                                }
                                case YGAlign.Stretch:
                                {
                                    child.Layout.Position[pos[(int)crossAxis]] = 
                                        currentLead +
                                        YGUnwrapFloatOptional(child.getLeadingMargin(crossAxis, availableInnerWidth));

                                    // Remeasure child with the line height as it as been only
                                    // measured with the owners height yet.
                                    if (!YGNodeIsStyleDimDefined(
                                        child,
                                        crossAxis,
                                        availableInnerCrossDim))
                                    {
                                        var childWidth = isMainAxisRow
                                            ? child.Layout.MeasuredWidth +
                                            YGUnwrapFloatOptional(
                                                child.getMarginForAxis(
                                                    mainAxis,
                                                    availableInnerWidth))
                                            : lineHeight;

                                        var childHeight = !isMainAxisRow
                                            ? child.Layout.MeasuredHeight +
                                            YGUnwrapFloatOptional(
                                                child.getMarginForAxis(
                                                    crossAxis,
                                                    availableInnerWidth))
                                            : lineHeight;

                                        if (!(YGFloatsEqual(
                                                childWidth,
                                                child.Layout.MeasuredWidth) &&
                                            YGFloatsEqual(
                                                childHeight,
                                                child.Layout.MeasuredHeight)))
                                            YGLayoutNodeInternal(
                                                child,
                                                childWidth,
                                                childHeight,
                                                direction,
                                                YGMeasureMode.Exactly,
                                                YGMeasureMode.Exactly,
                                                availableInnerWidth,
                                                availableInnerHeight,
                                                true,
                                                "multiline-stretch",
                                                config);
                                    }

                                    break;
                                }
                                case YGAlign.Baseline:
                                {
                                    child.Layout.Position[YGEdge.Top] = 
                                        currentLead + maxAscentForCurrentLine - YGBaseline(child) +
                                        YGUnwrapFloatOptional(
                                            child.getLeadingPosition(
                                                YGFlexDirection.Column,
                                                availableInnerCrossDim));

                                    break;
                                }
                                case YGAlign.Auto:
                                case YGAlign.SpaceBetween:
                                case YGAlign.SpaceAround:
                                    break;
                                }
                        }

                    currentLead += lineHeight;
                }
            }

            // STEP 9: COMPUTING FINAL DIMENSIONS

            node.Layout.SetMeasuredDimension(
                YGDimension.Width,
                YGNodeBoundAxis(
                    node,
                    YGFlexDirection.Row,
                    availableWidth - marginAxisRow,
                    ownerWidth,
                    ownerWidth)
            );

            node.Layout.SetMeasuredDimension(
                YGDimension.Height,
                YGNodeBoundAxis(
                    node,
                    YGFlexDirection.Column,
                    availableHeight - marginAxisColumn,
                    ownerHeight,
                    ownerWidth)
            );

            // If the user didn't specify a width or height for the node, set the
            // dimensions based on the children.
            if (measureModeMainDim == YGMeasureMode.Undefined ||
                node.Style.overflow != YGOverflow.Scroll &&
                measureModeMainDim  == YGMeasureMode.AtMost)
                node.Layout.SetMeasuredDimension(
                    dim[(int) mainAxis],
                    YGNodeBoundAxis(
                        node,
                        mainAxis,
                        maxLineMainDim,
                        mainAxisownerSize,
                        ownerWidth)
                );
            else if (
                measureModeMainDim  == YGMeasureMode.AtMost &&
                node.Style.overflow == YGOverflow.Scroll)
                node.Layout.SetMeasuredDimension(
                    dim[(int) mainAxis],
                    YGFloatMax(
                        YGFloatMin(
                            availableInnerMainDim + paddingAndBorderAxisMain,
                            YGUnwrapFloatOptional(
                                YGNodeBoundAxisWithinMinAndMax(
                                    node,
                                    mainAxis,
                                    maxLineMainDim,
                                    mainAxisownerSize))),
                        paddingAndBorderAxisMain)
                );

            if (measureModeCrossDim == YGMeasureMode.Undefined ||
                node.Style.overflow != YGOverflow.Scroll &&
                measureModeCrossDim == YGMeasureMode.AtMost)
                node.Layout.SetMeasuredDimension(
                    dim[(int) crossAxis],
                    YGNodeBoundAxis(
                        node,
                        crossAxis,
                        totalLineCrossDim + paddingAndBorderAxisCross,
                        crossAxisownerSize,
                        ownerWidth)
                );
            else if (
                measureModeCrossDim == YGMeasureMode.AtMost &&
                node.Style.overflow == YGOverflow.Scroll)
                node.Layout.SetMeasuredDimension(
                    dim[(int) crossAxis],
                    YGFloatMax(
                        YGFloatMin(
                            availableInnerCrossDim + paddingAndBorderAxisCross,
                            YGUnwrapFloatOptional(
                                YGNodeBoundAxisWithinMinAndMax(
                                    node,
                                    crossAxis,
                                    totalLineCrossDim + paddingAndBorderAxisCross,
                                    crossAxisownerSize))),
                        paddingAndBorderAxisCross)
                );

            // As we only wrapped in normal direction yet, we need to reverse the
            // positions on wrap-reverse.
            if (performLayout && node.Style.flexWrap == YGWrap.WrapReverse)
                for (var i = 0; i < childCount; i++)
                {
                    var child = YGNodeGetChild(node, i);
                    if (child.Style.positionType == YGPositionType.Relative)
                        child.Layout.Position[pos[(int)crossAxis]] = 
                            node.Layout.GetMeasuredDimension(dim[(int) crossAxis]) -
                            child.Layout.Position[pos[(int) crossAxis]]      -
                            child.Layout.GetMeasuredDimension(dim[(int) crossAxis]);
                }

            if (performLayout)
            {
                // STEP 10: SIZING AND POSITIONING ABSOLUTE CHILDREN
                foreach (var child in node.getChildren())
                {
                    if (child.Style.positionType != YGPositionType.Absolute) continue;

                    YGNodeAbsoluteLayoutChild(
                        node,
                        child,
                        availableInnerWidth,
                        isMainAxisRow ? measureModeMainDim : measureModeCrossDim,
                        availableInnerHeight,
                        direction,
                        config);
                }

                // STEP 11: SETTING TRAILING POSITIONS FOR CHILDREN
                var needsMainTrailingPos = mainAxis == YGFlexDirection.RowReverse ||
                    mainAxis                        == YGFlexDirection.ColumnReverse;
                var needsCrossTrailingPos = crossAxis == YGFlexDirection.RowReverse ||
                    crossAxis                         == YGFlexDirection.ColumnReverse;

                // Set trailing position if necessary.
                if (needsMainTrailingPos || needsCrossTrailingPos)
                    for (var i = 0; i < childCount; i++)
                    {
                        var child = node.getChild(i);
                        if (child.Style.display == YGDisplay.None) continue;

                        if (needsMainTrailingPos) YGNodeSetChildTrailingPosition(node, child, mainAxis);

                        if (needsCrossTrailingPos) YGNodeSetChildTrailingPosition(node, child, crossAxis);
                    }
            }
        }

        internal static int  gDepth        = 0;
        internal static bool gPrintChanges = false;
        internal static bool gPrintSkips   = false;

        internal static string spacer = "                                                            ";

        internal static string YGSpacer(int level)
        {
            var spacerLen = spacer.Length;
            if (level > spacerLen)
                return spacer;
            return spacer.Substring(spacerLen - level);
        }

        internal static string YGMeasureModeName(
            YGMeasureMode mode,
            bool          performLayout)
        {
            string[] kMeasureModeNames = {"UNDEFINED", "EXACTLY", "AT_MOST"};
            string[] kLayoutModeNames  = {"LAY_UNDEFINED", "LAY_EXACTLY", "LAY_AT_MOST"};

            if ((int) mode >= YGMeasureModeCount) return "";

            return performLayout ? kLayoutModeNames[(int) mode] : kMeasureModeNames[(int) mode];
        }

        // inline
        internal static bool YGMeasureModeSizeIsExactAndMatchesOldMeasuredSize(
            YGMeasureMode sizeMode,
            float         size,
            float         lastComputedSize)
        {
            return sizeMode == YGMeasureMode.Exactly &&
                YGFloatsEqual(size, lastComputedSize);
        }

        // inline
        internal static bool YGMeasureModeOldSizeIsUnspecifiedAndStillFits(
            YGMeasureMode sizeMode,
            float         size,
            YGMeasureMode lastSizeMode,
            float         lastComputedSize)
        {
            return sizeMode  == YGMeasureMode.AtMost    &&
                lastSizeMode == YGMeasureMode.Undefined &&
                (size >= lastComputedSize || YGFloatsEqual(size, lastComputedSize));
        }

        // inline
        internal static bool YGMeasureModeNewMeasureSizeIsStricterAndStillValid(
            YGMeasureMode sizeMode,
            float         size,
            YGMeasureMode lastSizeMode,
            float         lastSize,
            float         lastComputedSize)
        {
            return lastSizeMode == YGMeasureMode.AtMost &&
                sizeMode        == YGMeasureMode.AtMost && lastSize.HasValue()         &&
                size.HasValue()               && lastComputedSize.HasValue() &&
                lastSize > size                         &&
                (lastComputedSize <= size || YGFloatsEqual(size, lastComputedSize));
        }

        public static float YGRoundValueToPixelGrid(
            float value,
            float pointScaleFactor,
            bool  forceCeil,
            bool  forceFloor)
        {
            var scaledValue = value       * pointScaleFactor;
            var fractial    = scaledValue % 1.0f;
            if (YGFloatsEqual(fractial, 0))
                scaledValue = scaledValue - fractial;
            else if (YGFloatsEqual(fractial, 1.0f))
                scaledValue = scaledValue - fractial + 1.0f;
            else if (forceCeil)
                scaledValue = scaledValue - fractial + 1.0f;
            else if (forceFloor)
                scaledValue = scaledValue - fractial;
            else
                scaledValue = scaledValue - fractial +
                    (fractial.HasValue() &&
                        (fractial > 0.5f || YGFloatsEqual(fractial, 0.5f))
                            ? 1.0f
                            : 0.0f);

            return scaledValue.IsUndefined() || pointScaleFactor.IsUndefined()
                    ? YGUndefined
                    : scaledValue / pointScaleFactor;
        }

        public static bool YGNodeCanUseCachedMeasurement(
            YGMeasureMode widthMode,
            float         width,
            YGMeasureMode heightMode,
            float         height,
            YGMeasureMode lastWidthMode,
            float         lastWidth,
            YGMeasureMode lastHeightMode,
            float         lastHeight,
            float         lastComputedWidth,
            float         lastComputedHeight,
            float         marginRow,
            float         marginColumn,
            YGConfigRef   config)
        {
            if (lastComputedHeight.HasValue() && lastComputedHeight < 0 ||
                lastComputedWidth.HasValue()  && lastComputedWidth  < 0)
                return false;

            var useRoundedComparison =
                config != null && config.pointScaleFactor != 0;
            var effectiveWidth = useRoundedComparison
                ? YGRoundValueToPixelGrid(width, config.pointScaleFactor, false, false)
                : width;
            var effectiveHeight = useRoundedComparison
                ? YGRoundValueToPixelGrid(height, config.pointScaleFactor, false, false)
                : height;
            var effectiveLastWidth = useRoundedComparison
                ? YGRoundValueToPixelGrid(
                    lastWidth,
                    config.pointScaleFactor,
                    false,
                    false)
                : lastWidth;
            var effectiveLastHeight = useRoundedComparison
                ? YGRoundValueToPixelGrid(
                    lastHeight,
                    config.pointScaleFactor,
                    false,
                    false)
                : lastHeight;

            var hasSameWidthSpec = lastWidthMode == widthMode &&
                YGFloatsEqual(effectiveLastWidth, effectiveWidth);
            var hasSameHeightSpec = lastHeightMode == heightMode &&
                YGFloatsEqual(effectiveLastHeight, effectiveHeight);

            var widthIsCompatible =
                hasSameWidthSpec ||
                YGMeasureModeSizeIsExactAndMatchesOldMeasuredSize(
                    widthMode,
                    width - marginRow,
                    lastComputedWidth) ||
                YGMeasureModeOldSizeIsUnspecifiedAndStillFits(
                    widthMode,
                    width - marginRow,
                    lastWidthMode,
                    lastComputedWidth) ||
                YGMeasureModeNewMeasureSizeIsStricterAndStillValid(
                    widthMode,
                    width - marginRow,
                    lastWidthMode,
                    lastWidth,
                    lastComputedWidth);

            var heightIsCompatible =
                hasSameHeightSpec ||
                YGMeasureModeSizeIsExactAndMatchesOldMeasuredSize(
                    heightMode,
                    height - marginColumn,
                    lastComputedHeight) ||
                YGMeasureModeOldSizeIsUnspecifiedAndStillFits(
                    heightMode,
                    height - marginColumn,
                    lastHeightMode,
                    lastComputedHeight) ||
                YGMeasureModeNewMeasureSizeIsStricterAndStillValid(
                    heightMode,
                    height - marginColumn,
                    lastHeightMode,
                    lastHeight,
                    lastComputedHeight);

            return widthIsCompatible && heightIsCompatible;
        }

        //
        // This is a wrapper around the YGNodelayoutImpl function. It determines
        // whether the layout request is redundant and can be skipped.
        //
        // Parameters:
        //  Input parameters are the same as YGNodelayoutImpl (see above)
        //  Return parameter is true if layout was performed, false if skipped
        //
        public static bool YGLayoutNodeInternal(
            YGNodeRef     node,
            float         availableWidth,
            float         availableHeight,
            YGDirection   ownerDirection,
            YGMeasureMode widthMeasureMode,
            YGMeasureMode heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight,
            bool          performLayout,
            string        reason,
            YGConfigRef   config)
        {
            var layout = node.Layout;

            gDepth++;

            var needToVisitNode =
                node.IsDirty && layout.GenerationCount != gCurrentGenerationCount ||
                layout.LastOwnerDirection != ownerDirection;

            if (needToVisitNode)
                layout.InvalidateCache();

            YGCachedMeasurement cachedResults = null;

            // Determine whether the results are already cached. We maintain a separate
            // cache for layouts and measurements. A layout operation modifies the
            // positions
            // and dimensions for nodes in the subtree. The algorithm assumes that each
            // node
            // gets layed out a maximum of one time per tree layout, but multiple
            // measurements
            // may be required to resolve all of the flex dimensions.
            // We handle nodes with measure functions specially here because they are the
            // most
            // expensive to measure, so it's worth avoiding redundant measurements if at
            // all possible.
            if (node.MeasureFunc != null)
            {
                var marginAxisRow = YGUnwrapFloatOptional(
                    node.getMarginForAxis(YGFlexDirection.Row, ownerWidth));
                var marginAxisColumn = YGUnwrapFloatOptional(
                    node.getMarginForAxis(YGFlexDirection.Column, ownerWidth));

                // First, try to use the layout cache.
                if (YGNodeCanUseCachedMeasurement(
                    widthMeasureMode,
                    availableWidth,
                    heightMeasureMode,
                    availableHeight,
                    layout.CachedLayout.widthMeasureMode,
                    layout.CachedLayout.availableWidth,
                    layout.CachedLayout.heightMeasureMode,
                    layout.CachedLayout.availableHeight,
                    layout.CachedLayout.computedWidth,
                    layout.CachedLayout.computedHeight,
                    marginAxisRow,
                    marginAxisColumn,
                    config))
                    cachedResults = layout.CachedLayout;
                else
                    for (var i = 0; i < layout.NextCachedMeasurementsIndex; i++)
                        if (YGNodeCanUseCachedMeasurement(
                            widthMeasureMode,
                            availableWidth,
                            heightMeasureMode,
                            availableHeight,
                            layout.CachedMeasurements[i].widthMeasureMode,
                            layout.CachedMeasurements[i].availableWidth,
                            layout.CachedMeasurements[i].heightMeasureMode,
                            layout.CachedMeasurements[i].availableHeight,
                            layout.CachedMeasurements[i].computedWidth,
                            layout.CachedMeasurements[i].computedHeight,
                            marginAxisRow,
                            marginAxisColumn,
                            config))
                        {
                            cachedResults = layout.CachedMeasurements[i];
                            break;
                        }
            }
            else if (performLayout)
            {
                if (YGFloatsEqual(layout.CachedLayout.availableWidth,  availableWidth)  &&
                    YGFloatsEqual(layout.CachedLayout.availableHeight, availableHeight) &&
                    layout.CachedLayout.widthMeasureMode  == widthMeasureMode           &&
                    layout.CachedLayout.heightMeasureMode == heightMeasureMode)
                    cachedResults = layout.CachedLayout;
            }
            else
            {
                for (var i = 0; i < layout.NextCachedMeasurementsIndex; i++)
                    if (YGFloatsEqual(
                            layout.CachedMeasurements[i].availableWidth,
                            availableWidth) &&
                        YGFloatsEqual(
                            layout.CachedMeasurements[i].availableHeight,
                            availableHeight)                                              &&
                        layout.CachedMeasurements[i].widthMeasureMode == widthMeasureMode &&
                        layout.CachedMeasurements[i].heightMeasureMode ==
                        heightMeasureMode)
                    {
                        cachedResults = layout.CachedMeasurements[i];
                        break;
                    }
            }

            if (!needToVisitNode && cachedResults != null)
            {
                layout.SetMeasuredDimension(YGDimension.Width,  cachedResults.computedWidth);
                layout.SetMeasuredDimension(YGDimension.Height, cachedResults.computedHeight);

                if (gPrintChanges && gPrintSkips)
                {
                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        $"{YGSpacer(gDepth)}{gDepth}.[[skipped] ");

                    node.getPrintFunc()?.Invoke(node);

                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        "wm: {YGMeasureModeName(widthMeasureMode,  performLayout)}, hm: {YGMeasureModeName(heightMeasureMode, performLayout)}, aw: {availableWidth} ah: {availableHeight} => d: ({cachedResults.computedWidth}, {cachedResults.computedHeight}) {reason}\n");
                }
            }
            else
            {
                if (gPrintChanges)
                {
                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        $"{YGSpacer(gDepth)}{gDepth}.{(needToVisitNode ? " * " : "")}");

                    node.getPrintFunc()?.Invoke(node);

                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        $"wm: {YGMeasureModeName(widthMeasureMode, performLayout)}, hm: {YGMeasureModeName(heightMeasureMode, performLayout)}, aw: {availableWidth} ah: {availableHeight} {reason}\n");
                }

                YGNodelayoutImpl(
                    node,
                    availableWidth,
                    availableHeight,
                    ownerDirection,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight,
                    performLayout,
                    config);

                if (gPrintChanges)
                {
                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        $"{YGSpacer(gDepth)}{gDepth}.]{(needToVisitNode ? "*" : "")}");

                    node.getPrintFunc()?.Invoke(node);

                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        "wm: {YGMeasureModeName(widthMeasureMode,  performLayout)}, hm: {YGMeasureModeName(heightMeasureMode, performLayout)}, d: ({layout.measuredDimensions[YGDimension.Width]}, {layout.measuredDimensions[YGDimension.Height]}) {reason}\n");
                }

                layout.LastOwnerDirection = ownerDirection;

                if (cachedResults == null)
                {
                    if (layout.CachedMeasurementFull)
                    {
                        if (gPrintChanges)
                            YGLog(node, YGLogLevel.Verbose, "Out of cache entries!\n");

                        layout.ResetNextCachedMeasurement();
                    }

                    YGCachedMeasurement newCacheEntry;
                    if (performLayout)
                    {
                        // Use the single layout cache entry.
                        newCacheEntry = layout.CachedLayout;
                    }
                    else
                    {
                        // Allocate a new measurement cache entry.
                        newCacheEntry = layout.GetNextCachedMeasurement();
                    }

                    newCacheEntry.availableWidth    = availableWidth;
                    newCacheEntry.availableHeight   = availableHeight;
                    newCacheEntry.widthMeasureMode  = widthMeasureMode;
                    newCacheEntry.heightMeasureMode = heightMeasureMode;
                    newCacheEntry.computedWidth     = layout.MeasuredWidth;
                    newCacheEntry.computedHeight    = layout.MeasuredHeight;
                }
            }

            if (performLayout)
            {
                node.Layout.SetDimension(YGDimension.Width,  node.Layout.MeasuredWidth);
                node.Layout.SetDimension(YGDimension.Height, node.Layout.MeasuredHeight);

                node.setHasNewLayout(true);
                node.IsDirty = false;
            }

            gDepth--;
            layout.GenerationCount = gCurrentGenerationCount;
            return needToVisitNode || cachedResults == null;
        }

        public static void YGConfigSetPointScaleFactor(
            YGConfigRef config,
            float       pixelsInPoint)
        {
            YGAssertWithConfig(
                config,
                pixelsInPoint >= 0.0f,
                "Scale factor should not be less than zero");

            // We store points for Pixel as we will use it for rounding
            if (pixelsInPoint == 0.0f)
                config.pointScaleFactor = 0.0f;
            else
                config.pointScaleFactor = pixelsInPoint;
        }

        internal static void YGRoundToPixelGrid(
            YGNodeRef node,
            float     pointScaleFactor,
            float     absoluteLeft,
            float     absoluteTop)
        {
            if (pointScaleFactor == 0.0f) return;

            var nodeLeft = node.Layout.Position.Left;
            var nodeTop  = node.Layout.Position.Top;

            var nodeWidth  = node.Layout.Width;
            var nodeHeight = node.Layout.Height;

            var absoluteNodeLeft = absoluteLeft + nodeLeft;
            var absoluteNodeTop  = absoluteTop  + nodeTop;

            var absoluteNodeRight  = absoluteNodeLeft + nodeWidth;
            var absoluteNodeBottom = absoluteNodeTop  + nodeHeight;

            // If a node has a custom measure function we never want to round down its
            // size as this could lead to unwanted text truncation.
            var textRounding = node.getNodeType() == YGNodeType.Text;

            node.Layout.Position[YGEdge.Left] = 
                YGRoundValueToPixelGrid(nodeLeft, pointScaleFactor, false, textRounding);

            node.Layout.Position[YGEdge.Top] = 
                YGRoundValueToPixelGrid(nodeTop, pointScaleFactor, false, textRounding);

            // We multiply dimension by scale factor and if the result is close to the
            // whole number, we don't have any fraction To verify if the result is close
            // to whole number we want to check both floor and ceil numbers
            var hasFractionalWidth =
                !YGFloatsEqual(nodeWidth * pointScaleFactor % 1.0f, 0) &&
                !YGFloatsEqual(nodeWidth * pointScaleFactor % 1.0f, 1.0f);
            var hasFractionalHeight =
                !YGFloatsEqual(nodeHeight * pointScaleFactor % 1.0f, 0f) &&
                !YGFloatsEqual(nodeHeight * pointScaleFactor % 1.0f, 1f);

            node.Layout.SetDimension(
                YGDimension.Width,
                YGRoundValueToPixelGrid(
                    absoluteNodeRight,
                    pointScaleFactor,
                    textRounding && hasFractionalWidth,
                    textRounding && !hasFractionalWidth) -
                YGRoundValueToPixelGrid(
                    absoluteNodeLeft,
                    pointScaleFactor,
                    false,
                    textRounding)
                );

            node.Layout.SetDimension(
                YGDimension.Height,
                YGRoundValueToPixelGrid(
                    absoluteNodeBottom,
                    pointScaleFactor,
                    textRounding && hasFractionalHeight,
                    textRounding && !hasFractionalHeight) -
                YGRoundValueToPixelGrid(
                    absoluteNodeTop,
                    pointScaleFactor,
                    false,
                    textRounding)
                );

            var childCount = YGNodeGetChildCount(node);
            for (var i = 0; i < childCount; i++)
                YGRoundToPixelGrid(
                    YGNodeGetChild(node, i),
                    pointScaleFactor,
                    absoluteNodeLeft,
                    absoluteNodeTop);
        }

        public static void YGNodeCalculateLayout(
            YGNodeRef   node,
            float       ownerWidth,
            float       ownerHeight,
            YGDirection ownerDirection)
        {
            // Increment the generation count. This will force the recursive routine to
            // visit
            // all dirty nodes at least once. Subsequent visits will be skipped if the
            // input
            // parameters don't change.
            gCurrentGenerationCount++;
            node.resolveDimension();
            var width            = YGUndefined;
            var widthMeasureMode = YGMeasureMode.Undefined;
            if (YGNodeIsStyleDimDefined(node, YGFlexDirection.Row, ownerWidth))
            {
                width = YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.getResolvedDimension(dim[(int) YGFlexDirection.Row]),
                        ownerWidth) +
                    node.getMarginForAxis(YGFlexDirection.Row, ownerWidth));
                widthMeasureMode = YGMeasureMode.Exactly;
            }
            else if (!YGResolveValue(
                    node.Style.MaxDimensions[YGDimension.Width],
                    ownerWidth).IsUndefined())
            {
                width = YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.Style.MaxDimensions[YGDimension.Width],
                        ownerWidth));
                widthMeasureMode = YGMeasureMode.AtMost;
            }
            else
            {
                width = ownerWidth;
                widthMeasureMode = width.IsUndefined()
                    ? YGMeasureMode.Undefined
                    : YGMeasureMode.Exactly;
            }

            var height            = YGUndefined;
            var heightMeasureMode = YGMeasureMode.Undefined;
            if (YGNodeIsStyleDimDefined(node, YGFlexDirection.Column, ownerHeight))
            {
                height = YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.getResolvedDimension(dim[(int) YGFlexDirection.Column]),
                        ownerHeight) +
                    node.getMarginForAxis(YGFlexDirection.Column, ownerWidth));
                heightMeasureMode = YGMeasureMode.Exactly;
            }
            else if (!YGResolveValue(
                    node.Style.MaxDimensions[YGDimension.Height],
                    ownerHeight).IsUndefined())
            {
                height = YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.Style.MaxDimensions[YGDimension.Height],
                        ownerHeight));
                heightMeasureMode = YGMeasureMode.AtMost;
            }
            else
            {
                height = ownerHeight;
                heightMeasureMode = height.IsUndefined()
                    ? YGMeasureMode.Undefined
                    : YGMeasureMode.Exactly;
            }

            if (YGLayoutNodeInternal(
                node,
                width,
                height,
                ownerDirection,
                widthMeasureMode,
                heightMeasureMode,
                ownerWidth,
                ownerHeight,
                true,
                "initial",
                node.Config))
            {
                node.setPosition(
                    node.Layout.Direction,
                    ownerWidth,
                    ownerHeight,
                    ownerWidth);
                YGRoundToPixelGrid(node, node.Config.pointScaleFactor, 0.0f, 0.0f);

                if (node.Config.printTree)
                    YGNodePrint(
                        node,
                        YGPrintOptions.Layout | YGPrintOptions.Children | YGPrintOptions.Style);
            }
        }

        public static void YGConfigSetLogger(YGConfigRef config, YGLogger logger)
        {
            config.logger = logger ?? YGDefaultLog;
        }

        internal static void YGVLog(
            YGConfigRef config,
            YGNodeRef   node,
            YGLogLevel  level,
            string      format, params object[] args)
        {
            var logConfig = config ?? YGConfig.DefaultConfig;
            logConfig.logger(logConfig, node, level, format, args);

            if (level == YGLogLevel.Fatal) throw new SystemException();
        }

        public static void YGLogWithConfig(YGConfigRef config, YGLogLevel level, string message)
        {
            YGVLog(config, null, level, message);
        }

        public static void YGLog(YGNodeRef node, YGLogLevel level, string message)
        {
            YGVLog(
                node == null ? null : node.Config,
                node,
                level,
                message);
        }

        public static void YGAssert(bool condition, string message)
        {
            if (!condition) YGLog(null, YGLogLevel.Fatal, $"{message}\n");
        }

        public static void YGAssertWithNode(
            YGNodeRef node,
            bool      condition,
            string    message)
        {
            if (!condition) YGLog(node, YGLogLevel.Fatal, $"{message}\n");
        }

        public static void YGAssertWithConfig(
            YGConfigRef config,
            bool        condition,
            string      message)
        {
            if (!condition)
                YGLogWithConfig(config, YGLogLevel.Fatal, "{message}\n");
        }

        internal static void YGTraverseChildrenPreOrder(
            YGVector          children,
            Action<YGNodeRef> f)
        {
            foreach (var node in children)
            {
                f(node);
                YGTraverseChildrenPreOrder(node.getChildren(), f);
            }
        }

        public static void YGTraversePreOrder(
            YGNodeRef         node,
            Action<YGNodeRef> f)
        {
            if (node == null) return;

            f(node);
            YGTraverseChildrenPreOrder(node.getChildren(), f);
        }
    }
}
