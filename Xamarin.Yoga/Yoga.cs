using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming
#pragma warning disable 414
#pragma warning disable 169

namespace Xamarin.Yoga
{
    using static YGConst;

    public delegate YGSize YGMeasureFunc(
        YGNode        node,
        float         width,
        YGMeasureMode widthMode,
        float         height,
        YGMeasureMode heightMode);

    public delegate float YGBaselineFunc(YGNode node, float width, float height);

    public delegate void YGDirtiedFunc(YGNode node);

    public delegate void YGPrintFunc(YGNode node);

    public delegate void YGLogger(
        YGConfig        config,
        YGNode          node,
        YGLogLevel      level,
        string          format,
        params object[] args);

    public static partial class YGGlobal
    {

        public static float YGNodeStyleGetFlexShrink(YGNode node)
        {
            return node.Style.FlexShrink ??
                (node.Config.UseWebDefaults ? kWebDefaultFlexShrink : kDefaultFlexShrink);
        }

        // YG_NODE_STYLE_EDGE_PROPERTY_UNIT_AUTO_IMPL(YGValue, Margin,   margin);
        //YG_NODE_STYLE_EDGE_PROPERTY_UNIT_IMPL(YGValue,      Padding,  padding, padding);
        public static void YGNodeStyleSetPadding(YGNode node, YGEdge edge, float padding)
        {
            var value = YGValue.Sanitized(padding, YGUnit.Point);

            if (!FloatEqual(node.Style.Padding[edge].value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Padding[edge].unit != value.unit)
            {
                node.Style.Padding[edge] = value;
                node.MarkDirtyAndPropagate();
            }
        }

        public static void YGNodeStyleSetPaddingPercent(
            YGNode node, YGEdge edge, float padding)
        {
            var value = YGValue.Sanitized(padding, YGUnit.Percent);
            if (!FloatEqual(node.Style.Padding[edge].value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Padding[edge].unit != value.unit)
            {
                node.Style.Padding[edge] = value;
                node.MarkDirtyAndPropagate();
            }
        }

        public static YGValue YGNodeStyleGetPadding(YGNode node, YGEdge edge)
        {
            var value = node.Style.Padding[edge];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto)
            {
                return (node.Style.Padding[edge] = new YGValue(float.NaN, value.unit));
            }

            return value;
        }


        // TODO(T26792433): Change the API to accept float?.
        public static void YGNodeStyleSetBorder(
            YGNode node,
            YGEdge edge,
            float  border)
        {
            var value = YGValue.Sanitized(border, YGUnit.Point);
            if (!FloatEqual(node.Style.Border[edge].value, value.value) &&
                value.unit != YGUnit.Undefined ||
                node.Style.Border[edge].unit != value.unit)
            {
                node.Style.Border[edge] = value;
                node.MarkDirtyAndPropagate();
            }
        }

        public static float YGNodeStyleGetBorder(YGNode node, YGEdge edge)
        {
            var value = node.Style.Border[edge];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto)
                return float.NaN;
            return value.value;
        }

        // Yoga specific properties, not compatible with flexbox specification

        // YG_NODE_STYLE_PROPERTY_UNIT_AUTO_IMPL(YGValue,Width,width,dimensions[YGDimension.Width]);
        public static void YGNodeStyleSetWidth(YGNode node, YGValue width)
        {
            var value = YGValue.Sanitized(width.value, YGUnit.Point);
            if (!FloatEqual(node.Style.Dimensions.Width.value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Dimensions.Width.unit != value.unit)
            {
                node.Style.Dimensions.Width = value;
                node.MarkDirtyAndPropagate();
            }
        }

        public static void YGNodeStyleSetWidthPercent(
            YGNode node, YGValue width)
        {
            if (!FloatEqual(node.Style.Dimensions.Width.value, YGFloatSanitize(width.value)) ||
                node.Style.Dimensions.Width.unit != YGUnit.Percent)
            {
                node.Style.Dimensions.Width = width.IsNaN()
                    ? new YGValue(0,           YGUnit.Auto)
                    : new YGValue(width.value, YGUnit.Percent);
                node.MarkDirtyAndPropagate();
            }
        }

        public static void YGNodeStyleSetWidthAuto(YGNode node)
        {
            if (node.Style.Dimensions[YGDimension.Width].unit != YGUnit.Auto)
            {
                node.Style.Dimensions[YGDimension.Width] = new YGValue(0, YGUnit.Auto);
                node.MarkDirtyAndPropagate();
            }
        }

        public static YGValue YGNodeStyleGetWidth(YGNode node)
        {
            var value = node.Style.Dimensions[YGDimension.Width];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Undefined)
            {
                node.Style.Dimensions[YGDimension.Width] = value = new YGValue(float.NaN, value.unit);
            }

            return value;
        }


        //YG_NODE_STYLE_PROPERTY_UNIT_AUTO_IMPL(YGValue,Height,height,dimensions[ YGDimension.Height]);
        public static void YGNodeStyleSetHeight(YGNode node, YGValue height)
        {
            var value = YGValue.Sanitized(height.value, YGUnit.Point);
            if (!FloatEqual(node.Style.Dimensions.Height.value, value.value) && value.unit != YGUnit.Undefined ||
                node.Style.Dimensions.Height.unit != value.unit)
            {
                node.Style.Dimensions.Height = value;
                node.MarkDirtyAndPropagate();
            }
        }

        public static void YGNodeStyleSetHeightPercent(YGNode node, YGValue height)
        {
            if (!FloatEqual(node.Style.Dimensions.Height.value, YGFloatSanitize(height.value)) ||
                node.Style.Dimensions.Height.unit != YGUnit.Percent)
            {
                node.Style.Dimensions.Height = height.IsNaN()
                    ? new YGValue(0,            YGUnit.Auto)
                    : new YGValue(height.value, YGUnit.Percent);
                node.MarkDirtyAndPropagate();
            }
        }

        public static void YGNodeStyleSetHeightAuto(YGNode node)
        {
            if (node.Style.Dimensions.Height.unit != YGUnit.Auto)
            {
                node.Style.Dimensions.Height = new YGValue(0, YGUnit.Auto);
                node.MarkDirtyAndPropagate();
            }
        }

        public static YGValue YGNodeStyleGetHeight(YGNode node)
        {
            var value = node.Style.Dimensions.Height;
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Undefined)
                node.Style.Dimensions.Height = value = new YGValue(float.NaN, value.unit);
            return value;
        }


        internal static void YGNodeStyleSetDimensions(
            YGNode      node,
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
                node.MarkDirtyAndPropagate();
            }
        }

        public static void YGNodeStyleSetMinWidth(YGNode node, float minWidth)
        {
            YGNodeStyleSetDimensions(node, node.Style.MinDimensions, YGDimension.Width, minWidth, YGUnit.Point);
        }

        public static void YGNodeStyleSetMinWidthPercent(YGNode node, float minWidth)
        {
            YGNodeStyleSetDimensions(node, node.Style.MinDimensions, YGDimension.Width, minWidth, YGUnit.Percent);
        }

        public static void YGNodeStyleSetMinHeight(YGNode node, float minHeight)
        {
            YGNodeStyleSetDimensions(node, node.Style.MinDimensions, YGDimension.Height, minHeight, YGUnit.Point);
        }

        public static void YGNodeStyleSetMinHeightPercent(YGNode node, float minHeight)
        {
            YGNodeStyleSetDimensions(node, node.Style.MinDimensions, YGDimension.Height, minHeight, YGUnit.Percent);
        }

        public static void YGNodeStyleSetMaxWidth(YGNode node, float maxWidth)
        {
            YGNodeStyleSetDimensions(node, node.Style.MaxDimensions, YGDimension.Width, maxWidth, YGUnit.Point);
        }

        public static void YGNodeStyleSetMaxWidthPercent(YGNode node, float maxWidth)
        {
            YGNodeStyleSetDimensions(node, node.Style.MaxDimensions, YGDimension.Width, maxWidth, YGUnit.Percent);
        }

        public static void YGNodeStyleSetMaxHeight(YGNode node, float maxHeight)
        {
            YGNodeStyleSetDimensions(node, node.Style.MaxDimensions, YGDimension.Height, maxHeight, YGUnit.Point);
        }

        public static void YGNodeStyleSetMaxHeightPercent(YGNode node, float maxHeight)
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
        public static float YGNodeLayoutGetMargin(YGNode node, YGEdge edge)
        {
            YGAssertWithNode(
                node,
                edge <= YGEdge.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case YGEdge.Left when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.Margin.End;
            case YGEdge.Left:
                return node.Layout.Margin.Start;
            case YGEdge.Right when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.Margin.Start;
            case YGEdge.Right:
                return node.Layout.Margin.End;
            }

            return node.Layout.Margin[edge];
        }


        // YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(float, Border,  border);
        public static float YGNodeLayoutGetBorder(YGNode node, YGEdge edge)
        {
            YGAssertWithNode(
                node,
                edge <= YGEdge.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case YGEdge.Left when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.Border.End;
            case YGEdge.Left:
                return node.Layout.Border.Start;
            case YGEdge.Right when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.Border.Start;
            case YGEdge.Right:
                return node.Layout.Border.End;
            }

            return node.Layout.Border[edge];
        }

        // YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(float, Padding, padding);
        public static float YGNodeLayoutGetPadding(YGNode node, YGEdge edge)
        {
            YGAssertWithNode(
                node,
                edge <= YGEdge.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case YGEdge.Left when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.Padding.End;
            case YGEdge.Left:
                return node.Layout.Padding.Start;
            case YGEdge.Right when node.Layout.Direction == YGDirection.RTL:
                return node.Layout.Padding.Start;
            case YGEdge.Right:
                return node.Layout.Padding.End;
            }

            return node.Layout.Padding[edge];
        }

        public static int gCurrentGenerationCount = 0;

        public static YGEdge[] leading  = {YGEdge.Top, YGEdge.Bottom, YGEdge.Left, YGEdge.Right};
        public static YGEdge[] trailing = {YGEdge.Bottom, YGEdge.Top, YGEdge.Right, YGEdge.Left};

        internal static YGEdge[]      pos = {YGEdge.Top, YGEdge.Bottom, YGEdge.Left, YGEdge.Right};
        internal static YGDimension[] dim = {YGDimension.Height, YGDimension.Height, YGDimension.Width, YGDimension.Width};

        internal static float YGNodePaddingAndBorderForAxis(
            YGNode          node,
            YGFlexDirection axis,
            float           widthSize)
        {
            return YGUnwrapFloatOptional(
                node.getLeadingPaddingAndBorder(axis, widthSize) +
                node.getTrailingPaddingAndBorder(axis, widthSize));
        }

        internal static YGAlign YGNodeAlignItem(
            YGNode node,
            YGNode child)
        {
            var align = child.Style.AlignSelf == YGAlign.Auto
                ? node.Style.AlignItems
                : child.Style.AlignSelf;
            if (align == YGAlign.Baseline &&
                YGFlexDirectionIsColumn(node.Style.FlexDirection))
                return YGAlign.FlexStart;

            return align;
        }

        internal static float YGBaseline(YGNode node)
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

            YGNode baselineChild = null;
            foreach (var child in node.Children)
            {
                if (child.getLineIndex() > 0) break;

                if (child.Style.PositionType == YGPositionType.Absolute) continue;

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

        internal static bool YGIsBaselineLayout(YGNode node)
        {
            if (YGFlexDirectionIsColumn(node.Style.FlexDirection))
                return false;

            if (node.Style.AlignItems == YGAlign.Baseline)
                return true;

            foreach (var child in node.Children)
            {
                if (child.Style.PositionType == YGPositionType.Relative &&
                    child.Style.AlignSelf    == YGAlign.Baseline)
                    return true;
            }

            return false;
        }

        // inline
        internal static float YGNodeDimWithMargin(
            YGNode          node,
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
            YGNode          node,
            YGFlexDirection axis,
            float           ownerSize)
        {
            var isUndefined = node.getResolvedDimension(dim[(int) axis]).IsNaN();
            return !(
                node.getResolvedDimension(dim[(int) axis]).unit == YGUnit.Auto      ||
                node.getResolvedDimension(dim[(int) axis]).unit == YGUnit.Undefined ||
                node.getResolvedDimension(dim[(int) axis]).unit == YGUnit.Point &&
                !isUndefined                                                    &&
                node.getResolvedDimension(dim[(int) axis]).value < 0.0f ||
                node.getResolvedDimension(dim[(int) axis]).unit == YGUnit.Percent &&
                !isUndefined                                                      &&
                (node.getResolvedDimension(dim[(int) axis]).value < 0.0f || ownerSize.IsNaN()));
        }

        // inline
        internal static bool YGNodeIsLayoutDimDefined(
            YGNode          node,
            YGFlexDirection axis)
        {
            var value = node.Layout.GetMeasuredDimension(dim[(int) axis]);
            return value.HasValue() && value >= 0.0f;
        }

        internal static float? YGNodeBoundAxisWithinMinAndMax(
            YGNode          node,
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
            YGNode          node,
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
            YGNode          node,
            YGNode          child,
            YGFlexDirection axis)
        {
            var size = child.Layout.GetMeasuredDimension(dim[(int) axis]);
            child.Layout.Position[trailing[(int) axis]] =
                node.Layout.GetMeasuredDimension(dim[(int) axis]) - size -
                child.Layout.Position[pos[(int) axis]];
        }

        internal static void YGConstrainMaxSizeForMode(
            YGNode            node,
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
            YGNode        node,
            YGNode        child,
            float         width,
            YGMeasureMode widthMode,
            float         height,
            float         ownerWidth,
            float         ownerHeight,
            YGMeasureMode heightMode,
            YGDirection   direction,
            YGConfig      config)
        {
            var mainAxis          = YGResolveFlexDirection(node.Style.FlexDirection, direction);
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
                if (child.Layout.ComputedFlexBasis.IsNaN() ||
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
                childWidth             = float.NaN;
                childHeight            = float.NaN;
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
                if (!isMainAxisRow && node.Style.Overflow == YGOverflow.Scroll ||
                    node.Style.Overflow != YGOverflow.Scroll)
                    if (childWidth.IsNaN() && width.HasValue())
                    {
                        childWidth            = width;
                        childWidthMeasureMode = YGMeasureMode.AtMost;
                    }

                if (isMainAxisRow && node.Style.Overflow == YGOverflow.Scroll ||
                    node.Style.Overflow != YGOverflow.Scroll)
                    if (childHeight.IsNaN() && height.HasValue())
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
                        childHeight            = (childWidth - marginRow) / child.Style.AspectRatio.Value;
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
            YGNode        node,
            YGNode        child,
            float         width,
            YGMeasureMode widthMode,
            float         height,
            YGDirection   direction,
            YGConfig      config)
        {
            var mainAxis      = YGResolveFlexDirection(node.Style.FlexDirection, direction);
            var crossAxis     = YGFlexDirectionCross(mainAxis, direction);
            var isMainAxisRow = YGFlexDirectionIsRow(mainAxis);

            var childWidth             = float.NaN;
            var childHeight            = float.NaN;
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
            if (childWidth.IsNaN() ^ childHeight.IsNaN())
                if (child.Style.AspectRatio.HasValue)
                {
                    if (childWidth.IsNaN())
                        childWidth = marginRow + (childHeight - marginColumn) * child.Style.AspectRatio.Value;
                    else if (childHeight.IsNaN())
                        childHeight = marginColumn + (childWidth - marginRow) / child.Style.AspectRatio.Value;
                }

            // If we're still missing one or the other dimension, measure the content.
            if (childWidth.IsNaN() || childHeight.IsNaN())
            {
                childWidthMeasureMode  = childWidth.IsNaN() ? YGMeasureMode.Undefined : YGMeasureMode.Exactly;
                childHeightMeasureMode = childHeight.IsNaN() ? YGMeasureMode.Undefined : YGMeasureMode.Exactly;

                // If the size of the owner is defined then try to constrain the absolute
                // child to that size as well. This allows text within the absolute child to
                // wrap to the size of its owner. This is the same behavior as many browsers
                // implement.
                if (!isMainAxisRow                       && childWidth.IsNaN() &&
                    widthMode != YGMeasureMode.Undefined && width.HasValue()   &&
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
                node.Style.JustifyContent == YGJustify.Center)
                child.Layout.Position[leading[(int) mainAxis]] =
                    (node.Layout.GetMeasuredDimension(dim[(int) mainAxis]) -
                        child.Layout.GetMeasuredDimension(dim[(int) mainAxis])) / 2.0f;
            else if (
                !child.isLeadingPositionDefined(mainAxis) &&
                node.Style.JustifyContent == YGJustify.FlexEnd)
                child.Layout.Position[leading[(int) mainAxis]] =
                    node.Layout.GetMeasuredDimension(dim[(int) mainAxis]) -
                    child.Layout.GetMeasuredDimension(dim[(int) mainAxis]);

            if (child.isTrailingPosDefined(crossAxis) &&
                !child.isLeadingPositionDefined(crossAxis))
                child.Layout.Position[leading[(int) crossAxis]] =
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
                child.Layout.Position[leading[(int) crossAxis]] =
                    (node.Layout.GetMeasuredDimension(dim[(int) crossAxis]) -
                        child.Layout.GetMeasuredDimension(dim[(int) crossAxis])) /
                    2.0f;

            else if (
                !child.isLeadingPositionDefined(crossAxis) &&
                (YGNodeAlignItem(node, child) == YGAlign.FlexEnd) ^
                (node.Style.FlexWrap          == YGWrap.WrapReverse))
                child.Layout.Position[leading[(int) crossAxis]] =
                    node.Layout.GetMeasuredDimension(dim[(int) crossAxis]) -
                    child.Layout.GetMeasuredDimension(dim[(int) crossAxis]);
        }

        internal static void YGNodeWithMeasureFuncSetMeasuredDimensions(
            YGNode        node,
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
            var innerWidth = availableWidth.IsNaN()
                ? availableWidth
                : YGFloatMax(0, availableWidth - marginAxisRow - paddingAndBorderAxisRow);
            var innerHeight = availableHeight.IsNaN()
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
            YGNode        node,
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
            YGNode        node,
            float         availableWidth,
            float         availableHeight,
            YGMeasureMode widthMeasureMode,
            YGMeasureMode heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight)
        {
            if (availableWidth.HasValue()                &&
                widthMeasureMode == YGMeasureMode.AtMost && availableWidth <= 0.0f ||
                availableHeight.HasValue()                &&
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
                        availableWidth.IsNaN() ||
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
                        availableHeight.IsNaN() ||
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

        private static void YGZeroOutLayoutRecursively(YGNode node)
        {
            node.Layout = new YGLayout();
            node.setHasNewLayout(true);

            foreach (var child in node.Children)
                YGZeroOutLayoutRecursively(child);
        }

        internal static float YGNodeCalculateAvailableInnerDim(
            YGNode          node,
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
                var minInnerDim = minDimensionOptional.IsNaN()
                    ? 0.0f
                    : minDimensionOptional.Value - paddingAndBorder;

                var maxDimensionOptional =
                    YGResolveValue(node.Style.MaxDimensions[dimension], ownerDim);

                var maxInnerDim = maxDimensionOptional.IsNaN()
                    ? float.MaxValue
                    : maxDimensionOptional.Value - paddingAndBorder;
                availableInnerDim =
                    YGFloatMax(YGFloatMin(availableInnerDim, maxInnerDim), minInnerDim);
            }

            return availableInnerDim;
        }

        private static void YGNodeComputeFlexBasisForChildren(
            YGNode          node,
            float           availableInnerWidth,
            float           availableInnerHeight,
            YGMeasureMode   widthMeasureMode,
            YGMeasureMode   heightMeasureMode,
            YGDirection     direction,
            YGFlexDirection mainAxis,
            YGConfig        config,
            bool            performLayout,
            float           totalOuterFlexBasis)
        {
            YGNode singleFlexChild = null;
            var    children        = node.Children;
            var measureModeMainDim =
                YGFlexDirectionIsRow(mainAxis) ? widthMeasureMode : heightMeasureMode;
            // If there is only one child with flexGrow + flexShrink it means we can set
            // the computedFlexBasis to 0 instead of measuring and shrinking / flexing the
            // child to exactly match the remaining space
            if (measureModeMainDim == YGMeasureMode.Exactly)
            {
                foreach (var child in children)
                {
                    if (child.isNodeFlexible())
                    {
                        if (singleFlexChild != null                     ||
                            FloatEqual(child.resolveFlexGrow(),   0.0f) ||
                            FloatEqual(child.resolveFlexShrink(), 0.0f))
                        {
                            // There is already a flexible child, or this flexible child doesn't
                            // have flexGrow and flexShrink, abort
                            singleFlexChild = null;
                            break;
                        }
                        singleFlexChild = child;
                    }
                }
            }

            foreach (var child in children)
            {
                child.resolveDimension();
                if (child.Style.Display == YGDisplay.None)
                {
                    YGZeroOutLayoutRecursively(child);
                    child.setHasNewLayout(true);
                    child.IsDirty = false;
                    continue;
                }

                if (performLayout)
                {
                    // Set the initial position (relative to the owner).
                    var childDirection = child.ResolveDirection(direction);
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

                if (child.Style.PositionType == YGPositionType.Absolute) continue;

                if (child == singleFlexChild)
                {
                    child.Layout.ComputedFlexBasisGeneration = gCurrentGenerationCount;
                    child.Layout.ComputedFlexBasis           = 0f;
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
        internal static CollectFlexItemsRowValues YGCalculateCollectFlexItemsRowValues(
            YGNode      node,
            YGDirection ownerDirection,
            float       mainAxisownerSize,
            float       availableInnerWidth,
            float       availableInnerMainDim,
            int         startOfLineIndex,
            int         lineCount)
        {
            var flexAlgoRowMeasurement = new CollectFlexItemsRowValues
            {
                RelativeChildren = new List<YGNode>(node.Children.Count)
            };

            float sizeConsumedOnCurrentLineIncludingMinConstraint = 0;
            var   mainAxis                                        = YGResolveFlexDirection(node.Style.FlexDirection, node.ResolveDirection(ownerDirection));
            var   isNodeFlexWrap                                  = node.Style.FlexWrap != YGWrap.NoWrap;

            // Add items to the current line until it's full or we run out of items.
            var endOfLineIndex = startOfLineIndex;
            for (; endOfLineIndex < node.Children.Count; endOfLineIndex++)
            {
                var child = node.Children[endOfLineIndex];
                if (child.Style.Display      == YGDisplay.None ||
                    child.Style.PositionType == YGPositionType.Absolute)
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
                    isNodeFlexWrap        && flexAlgoRowMeasurement.ItemsOnLine > 0)
                    break;

                sizeConsumedOnCurrentLineIncludingMinConstraint +=
                    flexBasisWithMinAndMaxConstraints + childMarginMainAxis;
                flexAlgoRowMeasurement.SizeConsumedOnCurrentLine +=
                    flexBasisWithMinAndMaxConstraints + childMarginMainAxis;
                flexAlgoRowMeasurement.ItemsOnLine++;

                if (child.isNodeFlexible())
                {
                    flexAlgoRowMeasurement.TotalFlexGrowFactors += child.resolveFlexGrow();

                    // Unlike the grow factor, the shrink factor is scaled relative to the
                    // child dimension.
                    flexAlgoRowMeasurement.TotalFlexShrinkScaledFactors +=
                        -child.resolveFlexShrink() *
                        YGUnwrapFloatOptional(child.Layout.ComputedFlexBasis);
                }

                flexAlgoRowMeasurement.RelativeChildren.Add(child);
            }

            // The total flex factor needs to be floored to 1.
            if (flexAlgoRowMeasurement.TotalFlexGrowFactors > 0 &&
                flexAlgoRowMeasurement.TotalFlexGrowFactors < 1)
                flexAlgoRowMeasurement.TotalFlexGrowFactors = 1;

            // The total flex shrink factor needs to be floored to 1.
            if (flexAlgoRowMeasurement.TotalFlexShrinkScaledFactors > 0 &&
                flexAlgoRowMeasurement.TotalFlexShrinkScaledFactors < 1)
                flexAlgoRowMeasurement.TotalFlexShrinkScaledFactors = 1;

            flexAlgoRowMeasurement.EndOfLineIndex = endOfLineIndex;
            return flexAlgoRowMeasurement;
        }

        // It distributes the free space to the flexible items and ensures that the size
        // of the flex items abide the min and max constraints. At the end of this
        // function the child nodes would have proper size. Prior using this function
        // please ensure that YGDistributeFreeSpaceFirstPass is called.
        internal static float YGDistributeFreeSpaceSecondPass(
            CollectFlexItemsRowValues collectedFlexItemsValues,
            YGNode                    node,
            YGFlexDirection           mainAxis,
            YGFlexDirection           crossAxis,
            float                     mainAxisownerSize,
            float                     availableInnerMainDim,
            float                     availableInnerCrossDim,
            float                     availableInnerWidth,
            float                     availableInnerHeight,
            bool                      flexBasisOverflows,
            YGMeasureMode             measureModeCrossDim,
            bool                      performLayout,
            YGConfig                  config)
        {
            float childFlexBasis         = 0;
            float flexShrinkScaledFactor = 0;
            float flexGrowFactor         = 0;
            float deltaFreeSpace         = 0;
            var   isMainAxisRow          = YGFlexDirectionIsRow(mainAxis);
            var   isNodeFlexWrap         = node.Style.FlexWrap != YGWrap.NoWrap;

            foreach (var currentRelativeChild in collectedFlexItemsValues.RelativeChildren)
            {
                childFlexBasis = YGUnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        currentRelativeChild,
                        mainAxis,
                        YGUnwrapFloatOptional(
                            currentRelativeChild.Layout.ComputedFlexBasis),
                        mainAxisownerSize));
                var updatedMainSize = childFlexBasis;

                if (collectedFlexItemsValues.RemainingFreeSpace.HasValue() &&
                    collectedFlexItemsValues.RemainingFreeSpace < 0)
                {
                    flexShrinkScaledFactor =
                        -currentRelativeChild.resolveFlexShrink() * childFlexBasis;
                    // Is this child able to shrink?
                    if (flexShrinkScaledFactor != 0f)
                    {
                        float childSize;

                        if (collectedFlexItemsValues.TotalFlexShrinkScaledFactors.HasValue() &&
                            collectedFlexItemsValues.TotalFlexShrinkScaledFactors == 0f)
                            childSize = childFlexBasis + flexShrinkScaledFactor;
                        else
                            childSize = childFlexBasis +
                                collectedFlexItemsValues.RemainingFreeSpace /
                                collectedFlexItemsValues.TotalFlexShrinkScaledFactors *
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
                    collectedFlexItemsValues.RemainingFreeSpace.HasValue() &&
                    collectedFlexItemsValues.RemainingFreeSpace > 0)
                {
                    flexGrowFactor = currentRelativeChild.resolveFlexGrow();

                    // Is this child able to grow?
                    if (flexGrowFactor.HasValue() && flexGrowFactor != 0)
                        updatedMainSize = YGNodeBoundAxis(
                            currentRelativeChild,
                            mainAxis,
                            childFlexBasis +
                            collectedFlexItemsValues.RemainingFreeSpace /
                            collectedFlexItemsValues.TotalFlexGrowFactors *
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
                    availableInnerCrossDim.HasValue()                                                 &&
                    !YGNodeIsStyleDimDefined(currentRelativeChild, crossAxis, availableInnerCrossDim) &&
                    measureModeCrossDim == YGMeasureMode.Exactly                                      &&
                    !(isNodeFlexWrap && flexBasisOverflows)                                           &&
                    YGNodeAlignItem(node, currentRelativeChild) == YGAlign.Stretch                    &&
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
                    childCrossMeasureMode = childCrossSize.IsNaN()
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
                        childCrossSize.IsNaN() || isLoosePercentageMeasurement
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
            CollectFlexItemsRowValues collectedFlexItemsValues,
            YGFlexDirection           mainAxis,
            float                     mainAxisownerSize,
            float                     availableInnerMainDim,
            float                     availableInnerWidth)
        {
            float flexShrinkScaledFactor = 0;
            float flexGrowFactor         = 0;
            float baseMainSize           = 0;
            float boundMainSize          = 0;
            float deltaFreeSpace         = 0;

            foreach (var currentRelativeChild in collectedFlexItemsValues.RelativeChildren)
            {
                var childFlexBasis = YGUnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        currentRelativeChild,
                        mainAxis,
                        YGUnwrapFloatOptional(
                            currentRelativeChild.Layout.ComputedFlexBasis),
                        mainAxisownerSize));

                if (collectedFlexItemsValues.RemainingFreeSpace < 0)
                {
                    flexShrinkScaledFactor =
                        -currentRelativeChild.resolveFlexShrink() * childFlexBasis;

                    // Is this child able to shrink?
                    if (flexShrinkScaledFactor.HasValue() && flexShrinkScaledFactor != 0)
                    {
                        baseMainSize = childFlexBasis +
                            collectedFlexItemsValues.RemainingFreeSpace /
                            collectedFlexItemsValues.TotalFlexShrinkScaledFactors *
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
                            collectedFlexItemsValues.TotalFlexShrinkScaledFactors -=
                                flexShrinkScaledFactor;
                        }
                    }
                }
                else if (
                    collectedFlexItemsValues.RemainingFreeSpace.HasValue() &&
                    collectedFlexItemsValues.RemainingFreeSpace > 0)
                {
                    flexGrowFactor = currentRelativeChild.resolveFlexGrow();

                    // Is this child able to grow?
                    if (flexGrowFactor.HasValue() && flexGrowFactor != 0)
                    {
                        baseMainSize = childFlexBasis +
                            collectedFlexItemsValues.RemainingFreeSpace /
                            collectedFlexItemsValues.TotalFlexGrowFactors * flexGrowFactor;
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
                            collectedFlexItemsValues.TotalFlexGrowFactors -= flexGrowFactor;
                        }
                    }
                }
            }

            collectedFlexItemsValues.RemainingFreeSpace -= deltaFreeSpace;
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
            YGNode                    node,
            CollectFlexItemsRowValues collectedFlexItemsValues,
            YGFlexDirection           mainAxis,
            YGFlexDirection           crossAxis,
            float                     mainAxisownerSize,
            float                     availableInnerMainDim,
            float                     availableInnerCrossDim,
            float                     availableInnerWidth,
            float                     availableInnerHeight,
            bool                      flexBasisOverflows,
            YGMeasureMode             measureModeCrossDim,
            bool                      performLayout,
            YGConfig                  config)
        {
            var originalFreeSpace = collectedFlexItemsValues.RemainingFreeSpace;
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

            collectedFlexItemsValues.RemainingFreeSpace =
                originalFreeSpace - distributedFreeSpace;
        }

        internal static void YGJustifyMainAxis(
            YGNode                    node,
            CollectFlexItemsRowValues collectedFlexItemsValues,
            int                       startOfLineIndex,
            YGFlexDirection           mainAxis,
            YGFlexDirection           crossAxis,
            YGMeasureMode             measureModeMainDim,
            YGMeasureMode             measureModeCrossDim,
            float                     mainAxisownerSize,
            float                     ownerWidth,
            float                     availableInnerMainDim,
            float                     availableInnerCrossDim,
            float                     availableInnerWidth,
            bool                      performLayout)
        {
            var style                        = node.Style;
            var leadingPaddingAndBorderMain  = YGUnwrapFloatOptional(node.getLeadingPaddingAndBorder(mainAxis, ownerWidth));
            var trailingPaddingAndBorderMain = YGUnwrapFloatOptional(node.getTrailingPaddingAndBorder(mainAxis, ownerWidth));
            // If we are using "at most" rules in the main axis, make sure that
            // remainingFreeSpace is 0 when min main dimension is not given
            if (measureModeMainDim                          == YGMeasureMode.AtMost &&
                collectedFlexItemsValues.RemainingFreeSpace > 0)
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
                        YGResolveValue(
                            style.MinDimensions[dim[(int) mainAxis]],
                            mainAxisownerSize)) - leadingPaddingAndBorderMain - trailingPaddingAndBorderMain;
                    var occupiedSpaceByChildNodes = availableInnerMainDim - collectedFlexItemsValues.RemainingFreeSpace;
                    collectedFlexItemsValues.RemainingFreeSpace =
                        YGFloatMax(0, minAvailableMainDim - occupiedSpaceByChildNodes);
                }
                else
                {
                    collectedFlexItemsValues.RemainingFreeSpace = 0;
                }
            }

            var numberOfAutoMarginsOnCurrentLine = 0;
            for (var i = startOfLineIndex;
                i < collectedFlexItemsValues.EndOfLineIndex;
                i++)
            {
                var child = node.Children[i];
                if (child.Style.PositionType == YGPositionType.Relative)
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
            var   justifyContent = node.Style.JustifyContent;

            if (numberOfAutoMarginsOnCurrentLine == 0)
                switch (justifyContent)
                {
                case YGJustify.Center:
                    leadingMainDim = collectedFlexItemsValues.RemainingFreeSpace / 2;
                    break;
                case YGJustify.FlexEnd:
                    leadingMainDim = collectedFlexItemsValues.RemainingFreeSpace;
                    break;
                case YGJustify.SpaceBetween:
                    if (collectedFlexItemsValues.ItemsOnLine > 1)
                        betweenMainDim =
                            YGFloatMax(collectedFlexItemsValues.RemainingFreeSpace, 0) /
                            (collectedFlexItemsValues.ItemsOnLine - 1);
                    else
                        betweenMainDim = 0;

                    break;
                case YGJustify.SpaceEvenly:
                    // Space is distributed evenly across all elements
                    betweenMainDim = collectedFlexItemsValues.RemainingFreeSpace /
                        (collectedFlexItemsValues.ItemsOnLine + 1);
                    leadingMainDim = betweenMainDim;
                    break;
                case YGJustify.SpaceAround:
                    // Space on the edges is half of the space between elements
                    betweenMainDim = collectedFlexItemsValues.RemainingFreeSpace /
                        collectedFlexItemsValues.ItemsOnLine;
                    leadingMainDim = betweenMainDim / 2;
                    break;
                case YGJustify.FlexStart:
                    break;
                }

            collectedFlexItemsValues.MainDim =
                leadingPaddingAndBorderMain + leadingMainDim;
            collectedFlexItemsValues.CrossDim = 0;

            float maxAscentForCurrentLine  = 0;
            float maxDescentForCurrentLine = 0;
            var   isNodeBaselineLayout     = YGIsBaselineLayout(node);
            for (var i = startOfLineIndex;
                i < collectedFlexItemsValues.EndOfLineIndex;
                i++)
            {
                var child       = node.Children[i];
                var childStyle  = child.Style;
                var childLayout = child.Layout;
                if (childStyle.Display == YGDisplay.None) continue;

                if (childStyle.PositionType == YGPositionType.Absolute &&
                    child.isLeadingPositionDefined(mainAxis))
                {
                    if (performLayout)
                        child.Layout.Position[pos[(int) mainAxis]] =
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
                    if (childStyle.PositionType == YGPositionType.Relative)
                    {
                        if (child.marginLeadingValue(mainAxis).unit == YGUnit.Auto)
                            collectedFlexItemsValues.MainDim +=
                                collectedFlexItemsValues.RemainingFreeSpace /
                                numberOfAutoMarginsOnCurrentLine;

                        if (performLayout)
                            child.Layout.Position[pos[(int) mainAxis]] =
                                childLayout.Position[pos[(int) mainAxis]] +
                                collectedFlexItemsValues.MainDim;

                        if (child.marginTrailingValue(mainAxis).unit == YGUnit.Auto)
                            collectedFlexItemsValues.MainDim +=
                                collectedFlexItemsValues.RemainingFreeSpace /
                                numberOfAutoMarginsOnCurrentLine;

                        var canSkipFlex =
                            !performLayout && measureModeCrossDim == YGMeasureMode.Exactly;
                        if (canSkipFlex)
                        {
                            // If we skipped the flex step, then we can't rely on the
                            // measuredDims because
                            // they weren't computed. This means we can't call
                            // YGNodeDimWithMargin.
                            collectedFlexItemsValues.MainDim += betweenMainDim +
                                YGUnwrapFloatOptional(
                                    child.getMarginForAxis(
                                        mainAxis,
                                        availableInnerWidth)) +
                                YGUnwrapFloatOptional(childLayout.ComputedFlexBasis);
                            collectedFlexItemsValues.CrossDim = availableInnerCrossDim;
                        }
                        else
                        {
                            // The main dimension is the sum of all the elements dimension plus
                            // the spacing.
                            collectedFlexItemsValues.MainDim += betweenMainDim +
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
                                collectedFlexItemsValues.CrossDim = YGFloatMax(
                                    collectedFlexItemsValues.CrossDim,
                                    YGNodeDimWithMargin(child, crossAxis, availableInnerWidth));
                            }
                        }
                    }
                    else if (performLayout)
                    {
                        child.Layout.Position[pos[(int) mainAxis]] =
                            childLayout.Position[pos[(int) mainAxis]] +
                            node.getLeadingBorder(mainAxis)           + leadingMainDim;
                    }
                }
            }

            collectedFlexItemsValues.MainDim += trailingPaddingAndBorderMain;

            if (isNodeBaselineLayout)
                collectedFlexItemsValues.CrossDim =
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
        //      or float.NaN if the size is not available; interpretation depends on
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
            YGNode        node,
            float         availableWidth,
            float         availableHeight,
            YGDirection   ownerDirection,
            YGMeasureMode widthMeasureMode,
            YGMeasureMode heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight,
            bool          performLayout,
            YGConfig      config)
        {
            YGAssertWithNode(
                node,
                availableWidth.IsNaN()
                    ? widthMeasureMode == YGMeasureMode.Undefined
                    : true,
                "availableWidth is indefinite so widthMeasureMode must be YGMeasureMode.Undefined");
            YGAssertWithNode(
                node,
                availableHeight.IsNaN()
                    ? heightMeasureMode == YGMeasureMode.Undefined
                    : true,
                "availableHeight is indefinite so heightMeasureMode must be YGMeasureMode.Undefined");

            // Set the resolved resolution in the node's layout.
            var direction = node.ResolveDirection(ownerDirection);
            node.Layout.Direction = direction;

            var flexRowDirection    = YGResolveFlexDirection(YGFlexDirection.Row,    direction);
            var flexColumnDirection = YGResolveFlexDirection(YGFlexDirection.Column, direction);

            node.Layout.Margin.Start  = YGUnwrapFloatOptional(node.getLeadingMargin(flexRowDirection, ownerWidth));
            node.Layout.Margin.End    = YGUnwrapFloatOptional(node.getTrailingMargin(flexRowDirection, ownerWidth));
            node.Layout.Margin.Top    = YGUnwrapFloatOptional(node.getLeadingMargin(flexColumnDirection, ownerWidth));
            node.Layout.Margin.Bottom = YGUnwrapFloatOptional(node.getTrailingMargin(flexColumnDirection, ownerWidth));

            node.Layout.Border.Start  = node.getLeadingBorder(flexRowDirection);
            node.Layout.Border.End    = node.getTrailingBorder(flexRowDirection);
            node.Layout.Border.Top    = node.getLeadingBorder(flexColumnDirection);
            node.Layout.Border.Bottom = node.getTrailingBorder(flexColumnDirection);

            node.Layout.Padding.Start  = YGUnwrapFloatOptional(node.getLeadingPadding(flexRowDirection, ownerWidth));
            node.Layout.Padding.End    = YGUnwrapFloatOptional(node.getTrailingPadding(flexRowDirection, ownerWidth));
            node.Layout.Padding.Top    = YGUnwrapFloatOptional(node.getLeadingPadding(flexColumnDirection, ownerWidth));
            node.Layout.Padding.Bottom = YGUnwrapFloatOptional(node.getTrailingPadding(flexColumnDirection, ownerWidth));

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

            var childCount = node.Children.Count;
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

            // Reset layout flags, as they could have changed.
            node.Layout.HadOverflow = false;

            // STEP 1: CALCULATE VALUES FOR REMAINDER OF ALGORITHM
            var mainAxis       = YGResolveFlexDirection(node.Style.FlexDirection, direction);
            var crossAxis      = YGFlexDirectionCross(mainAxis, direction);
            var isMainAxisRow  = YGFlexDirectionIsRow(mainAxis);
            var isNodeFlexWrap = node.Style.FlexWrap != YGWrap.NoWrap;

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
            float                     maxLineMainDim = 0;
            CollectFlexItemsRowValues collectedFlexItemsValues;
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
                endOfLineIndex = collectedFlexItemsValues.EndOfLineIndex;

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
                        collectedFlexItemsValues.SizeConsumedOnCurrentLine <
                        minInnerMainDim)
                    {
                        availableInnerMainDim = minInnerMainDim;
                    }
                    else if (
                        maxInnerMainDim.HasValue() &&
                        collectedFlexItemsValues.SizeConsumedOnCurrentLine >
                        maxInnerMainDim)
                    {
                        availableInnerMainDim = maxInnerMainDim;
                    }
                    else
                    {
                        if ((collectedFlexItemsValues.TotalFlexGrowFactors.IsNaN() &&
                            collectedFlexItemsValues.TotalFlexGrowFactors == 0 || node.resolveFlexGrow().IsNaN() &&
                            node.resolveFlexGrow() == 0f))
                            availableInnerMainDim = collectedFlexItemsValues.SizeConsumedOnCurrentLine;

                        sizeBasedOnContent = true;
                    }
                }

                if (!sizeBasedOnContent && availableInnerMainDim.HasValue())
                    collectedFlexItemsValues.RemainingFreeSpace = availableInnerMainDim -
                        collectedFlexItemsValues.SizeConsumedOnCurrentLine;
                else if (collectedFlexItemsValues.SizeConsumedOnCurrentLine < 0)
                    collectedFlexItemsValues.RemainingFreeSpace =
                        -collectedFlexItemsValues.SizeConsumedOnCurrentLine;

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

                node.Layout.HadOverflow = node.Layout.HadOverflow | (collectedFlexItemsValues.RemainingFreeSpace < 0);

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
                            collectedFlexItemsValues.CrossDim + paddingAndBorderAxisCross,
                            crossAxisownerSize,
                            ownerWidth) -
                        paddingAndBorderAxisCross;

                // If there's no flex wrap, the cross dimension is defined by the container.
                if (!isNodeFlexWrap && measureModeCrossDim == YGMeasureMode.Exactly) collectedFlexItemsValues.CrossDim = availableInnerCrossDim;

                // Clamp to the min/max size specified on the container.
                collectedFlexItemsValues.CrossDim =
                    YGNodeBoundAxis(
                        node,
                        crossAxis,
                        collectedFlexItemsValues.CrossDim + paddingAndBorderAxisCross,
                        crossAxisownerSize,
                        ownerWidth) -
                    paddingAndBorderAxisCross;

                // STEP 7: CROSS-AXIS ALIGNMENT
                // We can skip child alignment if we're just measuring the container.
                if (performLayout)
                    for (var i = startOfLineIndex; i < endOfLineIndex; i++)
                    {
                        var child = node.Children[i];
                        if (child.Style.Display == YGDisplay.None) continue;

                        if (child.Style.PositionType == YGPositionType.Absolute)
                        {
                            // If the child is absolutely positioned and has a
                            // top/left/bottom/right set, override
                            // all the previously computed positions to set it correctly.
                            var isChildLeadingPosDefined =
                                child.isLeadingPositionDefined(crossAxis);
                            if (isChildLeadingPosDefined)
                                child.Layout.Position[pos[(int) crossAxis]] =
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
                                child.Layout.Position[pos[(int) crossAxis]].IsNaN())
                                child.Layout.Position[pos[(int) crossAxis]] =
                                    node.getLeadingBorder(crossAxis) + YGUnwrapFloatOptional(child.getLeadingMargin(crossAxis, availableInnerWidth));
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
                                                child.getMarginForAxis(crossAxis, availableInnerWidth)) +
                                            (isMainAxisRow
                                                ? childMainSize / child.Style.AspectRatio.Value
                                                : childMainSize * child.Style.AspectRatio.Value)
                                            : collectedFlexItemsValues.CrossDim;

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
                                        childWidth.IsNaN() ? YGMeasureMode.Undefined : YGMeasureMode.Exactly;
                                    var childHeightMeasureMode =
                                        childHeight.IsNaN() ? YGMeasureMode.Undefined : YGMeasureMode.Exactly;

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
                            child.Layout.Position[pos[(int) crossAxis]] =
                                child.Layout.Position[pos[(int) crossAxis]] + totalLineCrossDim +
                                leadingCrossDim;
                        }
                    }

                totalLineCrossDim += collectedFlexItemsValues.CrossDim;
                maxLineMainDim =
                    YGFloatMax(maxLineMainDim, collectedFlexItemsValues.MainDim);
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
                    switch (node.Style.AlignContent)
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
                        var child = node.Children[ii];
                        if (child.Style.Display == YGDisplay.None) continue;

                        if (child.Style.PositionType == YGPositionType.Relative)
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
                            var child = node.Children[ii];
                            if (child.Style.Display == YGDisplay.None) continue;

                            if (child.Style.PositionType == YGPositionType.Relative)
                                switch (YGNodeAlignItem(node, child))
                                {
                                case YGAlign.FlexStart:
                                {
                                    child.Layout.Position[pos[(int) crossAxis]] =
                                        currentLead +
                                        YGUnwrapFloatOptional(child.getLeadingMargin(crossAxis, availableInnerWidth));
                                    break;
                                }
                                case YGAlign.FlexEnd:
                                {
                                    child.Layout.Position[pos[(int) crossAxis]] =
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

                                    child.Layout.Position[pos[(int) crossAxis]] =
                                        currentLead + (lineHeight - childHeight) / 2;
                                    break;
                                }
                                case YGAlign.Stretch:
                                {
                                    child.Layout.Position[pos[(int) crossAxis]] =
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

                                        if (!(FloatEqual(
                                                childWidth,
                                                child.Layout.MeasuredWidth) &&
                                            FloatEqual(
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
                node.Style.Overflow != YGOverflow.Scroll &&
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
                node.Style.Overflow == YGOverflow.Scroll)
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
                node.Style.Overflow != YGOverflow.Scroll &&
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
                node.Style.Overflow == YGOverflow.Scroll)
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
            if (performLayout && node.Style.FlexWrap == YGWrap.WrapReverse)
                for (var i = 0; i < childCount; i++)
                {
                    var child = node.Children[i];
                    if (child.Style.PositionType == YGPositionType.Relative)
                        child.Layout.Position[pos[(int) crossAxis]] =
                            node.Layout.GetMeasuredDimension(dim[(int) crossAxis]) -
                            child.Layout.Position[pos[(int) crossAxis]]            -
                            child.Layout.GetMeasuredDimension(dim[(int) crossAxis]);
                }

            if (performLayout)
            {
                // STEP 10: SIZING AND POSITIONING ABSOLUTE CHILDREN
                foreach (var child in node.Children)
                {
                    if (child.Style.PositionType != YGPositionType.Absolute) continue;

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
                        var child = node.Children[i];
                        if (child.Style.Display == YGDisplay.None) continue;

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
                FloatEqual(size, lastComputedSize);
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
                (size >= lastComputedSize || FloatEqual(size, lastComputedSize));
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
                size.HasValue()                         && lastComputedSize.HasValue() &&
                lastSize > size                         &&
                (lastComputedSize <= size || FloatEqual(size, lastComputedSize));
        }

        public static float YGRoundValueToPixelGrid(
            float value,
            float pointScaleFactor,
            bool  forceCeil,
            bool  forceFloor)
        {
            var scaledValue = value       * pointScaleFactor;
            var fractial    = scaledValue % 1.0f;
            if (FloatEqual(fractial, 0))
                scaledValue = scaledValue - fractial;
            else if (FloatEqual(fractial, 1.0f))
                scaledValue = scaledValue - fractial + 1.0f;
            else if (forceCeil)
                scaledValue = scaledValue - fractial + 1.0f;
            else if (forceFloor)
                scaledValue = scaledValue - fractial;
            else
                scaledValue = scaledValue - fractial +
                    (fractial.HasValue() &&
                        (fractial > 0.5f || FloatEqual(fractial, 0.5f))
                            ? 1.0f
                            : 0.0f);

            return scaledValue.IsNaN() || pointScaleFactor.IsNaN()
                ? float.NaN
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
            YGConfig      config)
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
                FloatEqual(effectiveLastWidth, effectiveWidth);
            var hasSameHeightSpec = lastHeightMode == heightMode &&
                FloatEqual(effectiveLastHeight, effectiveHeight);

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
            YGNode        node,
            float         availableWidth,
            float         availableHeight,
            YGDirection   ownerDirection,
            YGMeasureMode widthMeasureMode,
            YGMeasureMode heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight,
            bool          performLayout,
            string        reason,
            YGConfig      config)
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
                    layout.CachedLayout.WidthMeasureMode,
                    layout.CachedLayout.AvailableWidth,
                    layout.CachedLayout.HeightMeasureMode,
                    layout.CachedLayout.AvailableHeight,
                    layout.CachedLayout.ComputedWidth,
                    layout.CachedLayout.ComputedHeight,
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
                            layout.CachedMeasurements[i].WidthMeasureMode,
                            layout.CachedMeasurements[i].AvailableWidth,
                            layout.CachedMeasurements[i].HeightMeasureMode,
                            layout.CachedMeasurements[i].AvailableHeight,
                            layout.CachedMeasurements[i].ComputedWidth,
                            layout.CachedMeasurements[i].ComputedHeight,
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
                if (FloatEqual(layout.CachedLayout.AvailableWidth,  availableWidth)  &&
                    FloatEqual(layout.CachedLayout.AvailableHeight, availableHeight) &&
                    layout.CachedLayout.WidthMeasureMode  == widthMeasureMode        &&
                    layout.CachedLayout.HeightMeasureMode == heightMeasureMode)
                    cachedResults = layout.CachedLayout;
            }
            else
            {
                for (var i = 0; i < layout.NextCachedMeasurementsIndex; i++)
                    if (FloatEqual(
                            layout.CachedMeasurements[i].AvailableWidth,
                            availableWidth) &&
                        FloatEqual(
                            layout.CachedMeasurements[i].AvailableHeight,
                            availableHeight)                                              &&
                        layout.CachedMeasurements[i].WidthMeasureMode == widthMeasureMode &&
                        layout.CachedMeasurements[i].HeightMeasureMode ==
                        heightMeasureMode)
                    {
                        cachedResults = layout.CachedMeasurements[i];
                        break;
                    }
            }

            if (!needToVisitNode && cachedResults != null)
            {
                layout.SetMeasuredDimension(YGDimension.Width,  cachedResults.ComputedWidth);
                layout.SetMeasuredDimension(YGDimension.Height, cachedResults.ComputedHeight);

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

                    newCacheEntry.AvailableWidth    = availableWidth;
                    newCacheEntry.AvailableHeight   = availableHeight;
                    newCacheEntry.WidthMeasureMode  = widthMeasureMode;
                    newCacheEntry.HeightMeasureMode = heightMeasureMode;
                    newCacheEntry.ComputedWidth     = layout.MeasuredWidth;
                    newCacheEntry.ComputedHeight    = layout.MeasuredHeight;
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
            YGConfig config,
            float    pixelsInPoint)
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
            YGNode node,
            float  pointScaleFactor,
            float  absoluteLeft,
            float  absoluteTop)
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

            node.Layout.Position.Left = YGRoundValueToPixelGrid(nodeLeft, pointScaleFactor, false, textRounding);
            node.Layout.Position.Top  = YGRoundValueToPixelGrid(nodeTop,  pointScaleFactor, false, textRounding);

            // We multiply dimension by scale factor and if the result is close to the
            // whole number, we don't have any fraction To verify if the result is close
            // to whole number we want to check both floor and ceil numbers
            var hasFractionalWidth =
                !FloatEqual(nodeWidth * pointScaleFactor % 1.0f, 0) &&
                !FloatEqual(nodeWidth * pointScaleFactor % 1.0f, 1.0f);
            var hasFractionalHeight =
                !FloatEqual(nodeHeight * pointScaleFactor % 1.0f, 0f) &&
                !FloatEqual(nodeHeight * pointScaleFactor % 1.0f, 1f);

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

            foreach (var child in node.Children)
            {
                YGRoundToPixelGrid(
                    child,
                    pointScaleFactor,
                    absoluteNodeLeft,
                    absoluteNodeTop);
            }
        }

        public static void YGNodeCalculateLayout(
            YGNode      node,
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
            var width            = float.NaN;
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
                ownerWidth).IsNaN())
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
                widthMeasureMode = width.IsNaN()
                    ? YGMeasureMode.Undefined
                    : YGMeasureMode.Exactly;
            }

            var height            = float.NaN;
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
                ownerHeight).IsNaN())
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
                heightMeasureMode = height.IsNaN()
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
                    node.Print(YGPrintOptions.All);
            }
        }

        internal static void YGVLog(
            YGConfig   config,
            YGNode     node,
            YGLogLevel level,
            string     format, params object[] args)
        {
            var logConfig = config ?? YGConfig.DefaultConfig;
            logConfig.Logger(logConfig, node, level, format, args);

            if (level == YGLogLevel.Fatal) throw new SystemException();
        }

        public static void YGLogWithConfig(YGConfig config, YGLogLevel level, string message)
        {
            YGVLog(config, null, level, message);
        }

        public static void YGLog(YGNode node, YGLogLevel level, string message)
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
            YGNode node,
            bool   condition,
            string message)
        {
            if (!condition) YGLog(node, YGLogLevel.Fatal, $"{message}\n");
        }

        public static void YGAssertWithConfig(
            YGConfig config,
            bool     condition,
            string   message)
        {
            if (!condition)
                YGLogWithConfig(config, YGLogLevel.Fatal, "{message}\n");
        }

        internal static void YGTraverseChildrenPreOrder(
            IEnumerable<YGNode>   children,
            Action<YGNode> f)
        {
            foreach (var node in children)
            {
                f(node);
                YGTraverseChildrenPreOrder(node.Children, f);
            }
        }

        public static void YGTraversePreOrder(
            YGNode         node,
            Action<YGNode> f)
        {
            if (node == null)
                return;

            f(node);
            YGTraverseChildrenPreOrder(node.Children, f);
        }
    }
}
