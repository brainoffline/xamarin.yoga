using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga
{
    public class NodeCalculator
    {
        private readonly YGNode node;

        public NodeCalculator(YGNode node)
        {
            this.node = node;
        }

        //
        // This is a wrapper around the YGNodelayoutImpl function. It determines
        // whether the layout request is redundant and can be skipped.
        //
        // Parameters:
        //  Input parameters are the same as YGNodelayoutImpl (see above)
        //  Return parameter is true if layout was performed, false if skipped
        //

        internal bool LayoutInternal(
            float         availableWidth,
            float         availableHeight,
            DirectionType ownerDirection,
            MeasureMode   widthMeasureMode,
            MeasureMode   heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight,
            bool          performLayout,
            string        reason,
            YogaConfig    config)
        {
            var layout = node.Layout;

            YGGlobal.gDepth++;

            var needToVisitNode =
                node.IsDirty && layout.GenerationCount != YGGlobal.gCurrentGenerationCount ||
                layout.LastOwnerDirection != ownerDirection;

            if (needToVisitNode)
                layout.InvalidateCache();

            CachedMeasurement cachedResults = null;

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
                var marginAxisRow = YGGlobal.UnwrapFloatOptional(
                    node.GetMarginForAxis(FlexDirectionType.Row, ownerWidth));
                var marginAxisColumn = YGGlobal.UnwrapFloatOptional(
                    node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth));

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
                if (NumberExtensions.FloatEqual(layout.CachedLayout.AvailableWidth,  availableWidth)  &&
                    NumberExtensions.FloatEqual(layout.CachedLayout.AvailableHeight, availableHeight) &&
                    layout.CachedLayout.WidthMeasureMode  == widthMeasureMode                 &&
                    layout.CachedLayout.HeightMeasureMode == heightMeasureMode)
                    cachedResults = layout.CachedLayout;
            }
            else
            {
                for (var i = 0; i < layout.NextCachedMeasurementsIndex; i++)
                    if (NumberExtensions.FloatEqual(
                            layout.CachedMeasurements[i].AvailableWidth,
                            availableWidth) &&
                        NumberExtensions.FloatEqual(
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
                layout.SetMeasuredDimension(DimensionType.Width,  cachedResults.ComputedWidth);
                layout.SetMeasuredDimension(DimensionType.Height, cachedResults.ComputedHeight);

                if (YGGlobal.gPrintChanges && YGGlobal.gPrintSkips)
                {
                    YogaGlobal.Log(
                        node,
                        LogLevel.Verbose,
                        $"{YGGlobal.YGSpacer(YGGlobal.gDepth)}{YGGlobal.gDepth}.[[skipped] ");

                    node.PrintFunc?.Invoke(node);

                    YogaGlobal.Log(
                        node,
                        LogLevel.Verbose,
                        $"{(performLayout ? "LAY " : "")}wm: {widthMeasureMode.ToDescription()}, hm: {heightMeasureMode.ToDescription()}, aw: {availableWidth} ah: {availableHeight} => d: ({cachedResults.ComputedWidth}, {cachedResults.ComputedHeight}) {reason}\n");
                }
            }
            else
            {
                if (YGGlobal.gPrintChanges)
                {
                    YogaGlobal.Log(
                        node,
                        LogLevel.Verbose,
                        $"{YGGlobal.YGSpacer(YGGlobal.gDepth)}{YGGlobal.gDepth}.{(needToVisitNode ? " * " : "")}");

                    node.PrintFunc?.Invoke(node);

                    YogaGlobal.Log(
                        node,
                        LogLevel.Verbose,
                        $"{(performLayout ? "LAY " : "")}wm: {widthMeasureMode.ToDescription()}, hm: {heightMeasureMode.ToDescription()}, aw: {availableWidth} ah: {availableHeight} {reason}\n");
                }

                YGGlobal.YGNodelayoutImpl(
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

                if (YGGlobal.gPrintChanges)
                {
                    YogaGlobal.Log(
                        node,
                        LogLevel.Verbose,
                        $"{YGGlobal.YGSpacer(YGGlobal.gDepth)}{YGGlobal.gDepth}.]{(needToVisitNode ? "*" : "")}");

                    node.PrintFunc?.Invoke(node);

                    YogaGlobal.Log(
                        node,
                        LogLevel.Verbose,
                        $"{(performLayout ? "LAY " : "")}wm: {widthMeasureMode}, hm: {heightMeasureMode.ToDescription()}, d: ({layout.MeasuredWidth}, {layout.MeasuredHeight}) {reason}\n");
                }

                layout.LastOwnerDirection = ownerDirection;

                if (cachedResults == null)
                {
                    if (layout.CachedMeasurementFull)
                    {
                        if (YGGlobal.gPrintChanges) YogaGlobal.Log(node, LogLevel.Verbose, "Out of cache entries!\n");

                        layout.ResetNextCachedMeasurement();
                    }

                    CachedMeasurement newCacheEntry;
                    if (performLayout)
                        newCacheEntry = layout.CachedLayout;
                    else
                        newCacheEntry = layout.GetNextCachedMeasurement();

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
                node.Layout.Width  = node.Layout.MeasuredWidth;
                node.Layout.Height = node.Layout.MeasuredHeight;

                node.HasNewLayout = true;
                node.IsDirty      = false;
            }

            YGGlobal.gDepth--;
            layout.GenerationCount = YGGlobal.gCurrentGenerationCount;
            return needToVisitNode || cachedResults == null;
        }

        public void CalculateLayout(
            float         ownerWidth,
            float         ownerHeight,
            DirectionType ownerDirection)
        {
            // Increment the generation count. This will force the recursive routine to
            // visit
            // all dirty nodes at least once. Subsequent visits will be skipped if the
            // input
            // parameters don't change.
            YGGlobal.gCurrentGenerationCount++;
            node.ResolveDimension();
            var width            = Single.NaN;
            var widthMeasureMode = MeasureMode.Undefined;
            if (YGGlobal.YGNodeIsStyleDimDefined(node, FlexDirectionType.Row, ownerWidth))
            {
                width = YGGlobal.UnwrapFloatOptional(
                    node.ResolvedDimension[FlexDirectionType.Row.ToDimension()].ResolveValue(ownerWidth) +
                    node.GetMarginForAxis(FlexDirectionType.Row, ownerWidth));
                widthMeasureMode = MeasureMode.Exactly;
            }
            else if (!node.Style.MaxWidth.ResolveValue(ownerWidth).IsNaN())
            {
                width            = YGGlobal.UnwrapFloatOptional(node.Style.MaxWidth.ResolveValue(ownerWidth));
                widthMeasureMode = MeasureMode.AtMost;
            }
            else
            {
                width = ownerWidth;
                widthMeasureMode = width.IsNaN()
                    ? MeasureMode.Undefined
                    : MeasureMode.Exactly;
            }

            var height            = Single.NaN;
            var heightMeasureMode = MeasureMode.Undefined;
            if (YGGlobal.YGNodeIsStyleDimDefined(node, FlexDirectionType.Column, ownerHeight))
            {
                height = YGGlobal.UnwrapFloatOptional(
                    node.ResolvedDimension[FlexDirectionType.Column.ToDimension()].ResolveValue(ownerHeight) +
                    node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth));
                heightMeasureMode = MeasureMode.Exactly;
            }
            else if (!node.Style.MaxHeight.ResolveValue(ownerHeight).IsNaN())
            {
                height            = YGGlobal.UnwrapFloatOptional(node.Style.MaxHeight.ResolveValue(ownerHeight));
                heightMeasureMode = MeasureMode.AtMost;
            }
            else
            {
                height = ownerHeight;
                heightMeasureMode = height.IsNaN()
                    ? MeasureMode.Undefined
                    : MeasureMode.Exactly;
            }

            if (LayoutInternal(
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
                node.SetPosition(
                    node.Layout.Direction,
                    ownerWidth,
                    ownerHeight,
                    ownerWidth);
                YGGlobal.YGRoundToPixelGrid(node, node.Config.PointScaleFactor, 0.0f, 0.0f);

                if (node.Config.printTree)
                    node.Print(PrintOptionType.All);
            }
        }

        public bool YGNodeCanUseCachedMeasurement(
            MeasureMode widthMode,
            float       width,
            MeasureMode heightMode,
            float       height,
            MeasureMode lastWidthMode,
            float       lastWidth,
            MeasureMode lastHeightMode,
            float       lastHeight,
            float       lastComputedWidth,
            float       lastComputedHeight,
            float       marginRow,
            float       marginColumn,
            YogaConfig  config)
        {
            if (lastComputedHeight.HasValue() && lastComputedHeight < 0 ||
                lastComputedWidth.HasValue()  && lastComputedWidth  < 0)
                return false;

            var useRoundedComparison =
                config != null && config.PointScaleFactor != 0;
            var effectiveWidth = useRoundedComparison
                ? NumberExtensions.RoundValueToPixelGrid(width, config.PointScaleFactor, false, false)
                : width;
            var effectiveHeight = useRoundedComparison
                ? NumberExtensions.RoundValueToPixelGrid(height, config.PointScaleFactor, false, false)
                : height;
            var effectiveLastWidth = useRoundedComparison
                ? NumberExtensions.RoundValueToPixelGrid(
                    lastWidth,
                    config.PointScaleFactor,
                    false,
                    false)
                : lastWidth;
            var effectiveLastHeight = useRoundedComparison
                ? NumberExtensions.RoundValueToPixelGrid(
                    lastHeight,
                    config.PointScaleFactor,
                    false,
                    false)
                : lastHeight;

            var hasSameWidthSpec = lastWidthMode == widthMode &&
                NumberExtensions.FloatEqual(effectiveLastWidth, effectiveWidth);
            var hasSameHeightSpec = lastHeightMode == heightMode &&
                NumberExtensions.FloatEqual(effectiveLastHeight, effectiveHeight);

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

        // This function assumes that all the children of node have their
        // computedFlexBasis properly computed(To do this use
        // YGNodeComputeFlexBasisForChildren function).
        // This function calculates YGCollectFlexItemsRowMeasurement

        internal CollectFlexItemsRowValues CalculateCollectFlexItemsRowValues(
            DirectionType ownerDirection,
            float         mainAxisOwnerSize,
            float         availableInnerWidth,
            float         availableInnerMainDim,
            int           startOfLineIndex,
            int           lineCount)
        {
            var flexAlgoRowMeasurement = new CollectFlexItemsRowValues
            {
                RelativeChildren = new List<YGNode>(node.Children.Count)
            };

            float sizeConsumedOnCurrentLineIncludingMinConstraint = 0;
            var   mainAxis                                        = YGGlobal.ResolveFlexDirection(node.Style.FlexDirection, node.ResolveDirection(ownerDirection));
            var   isNodeFlexWrap                                  = node.Style.FlexWrap != WrapType.NoWrap;

            // Add items to the current line until it's full or we run out of items.
            var endOfLineIndex = startOfLineIndex;
            for (; endOfLineIndex < node.Children.Count; endOfLineIndex++)
            {
                var child = node.Children[endOfLineIndex];
                if (child.Style.Display      == DisplayType.None ||
                    child.Style.PositionType == PositionType.Absolute)
                    continue;

                child.LineIndex = lineCount;
                var childMarginMainAxis = YGGlobal.UnwrapFloatOptional(child.GetMarginForAxis(mainAxis, availableInnerWidth));
                var flexBasisWithMinAndMaxConstraints = YGGlobal.UnwrapFloatOptional(
                    child.YGNodeBoundAxisWithinMinAndMax(
                        mainAxis,
                        YGGlobal.UnwrapFloatOptional(child.Layout.ComputedFlexBasis),
                        mainAxisOwnerSize));

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

                if (child.IsNodeFlexible())
                {
                    flexAlgoRowMeasurement.TotalFlexGrowFactors += child.ResolveFlexGrow();

                    // Unlike the grow factor, the shrink factor is scaled relative to the
                    // child dimension.
                    flexAlgoRowMeasurement.TotalFlexShrinkScaledFactors +=
                        -child.ResolveFlexShrink() *
                        YGGlobal.UnwrapFloatOptional(child.Layout.ComputedFlexBasis);
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

        internal void YGConstrainMaxSizeForMode(
            FlexDirectionType axis,
            float             ownerAxisSize,
            float             ownerWidth,
            ref MeasureMode   mode,
            ref float         size)
        {
            var maxSize =
                node.Style.MaxDimension(axis.ToDimension()).ResolveValue(ownerAxisSize) +
                node.GetMarginForAxis(axis, ownerWidth);
            switch (mode)
            {
            case MeasureMode.Exactly:
            case MeasureMode.AtMost:
                if (maxSize.HasValue && size > maxSize)
                    size = maxSize.Value;
                break;
            case MeasureMode.Undefined:
                if (maxSize.HasValue)
                {
                    mode = MeasureMode.AtMost;
                    size = maxSize.Value;
                }

                break;
            }
        }

        // inline

        private bool YGMeasureModeNewMeasureSizeIsStricterAndStillValid(
            MeasureMode sizeMode,
            float       size,
            MeasureMode lastSizeMode,
            float       lastSize,
            float       lastComputedSize)
        {
            return lastSizeMode == MeasureMode.AtMost &&
                sizeMode        == MeasureMode.AtMost && lastSize.HasValue()         &&
                size.HasValue()                       && lastComputedSize.HasValue() &&
                lastSize > size                       &&
                (lastComputedSize <= size || NumberExtensions.FloatEqual(size, lastComputedSize));
        }

        // inline

        private bool YGMeasureModeOldSizeIsUnspecifiedAndStillFits(
            MeasureMode sizeMode,
            float       size,
            MeasureMode lastSizeMode,
            float       lastComputedSize)
        {
            return sizeMode  == MeasureMode.AtMost    &&
                lastSizeMode == MeasureMode.Undefined &&
                (size >= lastComputedSize || NumberExtensions.FloatEqual(size, lastComputedSize));
        }

        // inline

        private bool YGMeasureModeSizeIsExactAndMatchesOldMeasuredSize(
            MeasureMode sizeMode,
            float       size,
            float       lastComputedSize)
        {
            return sizeMode == MeasureMode.Exactly && NumberExtensions.FloatEqual(size, lastComputedSize);
        }

        internal void YGNodeAbsoluteLayoutChild(
            YGNode        child,
            float         width,
            MeasureMode   widthMode,
            float         height,
            DirectionType direction,
            YogaConfig    config)
        {
            var mainAxis      = YGGlobal.ResolveFlexDirection(node.Style.FlexDirection, direction);
            var crossAxis     = YGGlobal.FlexDirectionCross(mainAxis, direction);
            var isMainAxisRow = mainAxis.IsRow();

            var childWidth             = Single.NaN;
            var childHeight            = Single.NaN;
            var childWidthMeasureMode  = MeasureMode.Undefined;
            var childHeightMeasureMode = MeasureMode.Undefined;

            var marginRow =
                YGGlobal.UnwrapFloatOptional(child.GetMarginForAxis(FlexDirectionType.Row, width));
            var marginColumn = YGGlobal.UnwrapFloatOptional(
                child.GetMarginForAxis(FlexDirectionType.Column, width));

            if (YGGlobal.YGNodeIsStyleDimDefined(child, FlexDirectionType.Row, width))
            {
                childWidth = YGGlobal.UnwrapFloatOptional(
                    child.ResolvedDimension.Width.ResolveValue(width)) + marginRow;
            }
            else
            {
                // If the child doesn't have a specified width, compute the width based
                // on the left/right
                // offsets if they're defined.
                if (child.IsLeadingPositionDefined(FlexDirectionType.Row) &&
                    child.IsTrailingPosDefined(FlexDirectionType.Row))
                {
                    childWidth = node.Layout.MeasuredWidth -
                        (node.GetLeadingBorder(FlexDirectionType.Row) +
                            node.GetTrailingBorder(FlexDirectionType.Row)) -
                        YGGlobal.UnwrapFloatOptional(
                            child.GetLeadingPosition(FlexDirectionType.Row, width) +
                            child.GetTrailingPosition(FlexDirectionType.Row, width));
                    childWidth =
                        child.YGNodeBoundAxis(FlexDirectionType.Row, childWidth, width, width);
                }
            }

            if (YGGlobal.YGNodeIsStyleDimDefined(child, FlexDirectionType.Column, height))
            {
                childHeight = YGGlobal.UnwrapFloatOptional(
                    child.ResolvedDimension.Height.ResolveValue(height)) + marginColumn;
            }
            else
            {
                // If the child doesn't have a specified height, compute the height
                // based on the top/bottom
                // offsets if they're defined.
                if (child.IsLeadingPositionDefined(FlexDirectionType.Column) &&
                    child.IsTrailingPosDefined(FlexDirectionType.Column))
                {
                    childHeight =
                        node.Layout.MeasuredHeight -
                        (node.GetLeadingBorder(FlexDirectionType.Column) +
                            node.GetTrailingBorder(FlexDirectionType.Column)) -
                        YGGlobal.UnwrapFloatOptional(
                            child.GetLeadingPosition(FlexDirectionType.Column, height) +
                            child.GetTrailingPosition(FlexDirectionType.Column, height));
                    childHeight = child.YGNodeBoundAxis(
                        FlexDirectionType.Column,
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
                childWidthMeasureMode  = childWidth.IsNaN() ? MeasureMode.Undefined : MeasureMode.Exactly;
                childHeightMeasureMode = childHeight.IsNaN() ? MeasureMode.Undefined : MeasureMode.Exactly;

                // If the size of the owner is defined then try to constrain the absolute
                // child to that size as well. This allows text within the absolute child to
                // wrap to the size of its owner. This is the same behavior as many browsers
                // implement.
                if (!isMainAxisRow                     && childWidth.IsNaN() &&
                    widthMode != MeasureMode.Undefined && width.HasValue()   &&
                    width     > 0)
                {
                    childWidth            = width;
                    childWidthMeasureMode = MeasureMode.AtMost;
                }

                child.Calc.LayoutInternal(
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
                    YGGlobal.UnwrapFloatOptional(
                        child.GetMarginForAxis(FlexDirectionType.Row, width));
                childHeight = child.Layout.MeasuredHeight +
                    YGGlobal.UnwrapFloatOptional(
                        child.GetMarginForAxis(FlexDirectionType.Column, width));
            }

            child.Calc.LayoutInternal(
                childWidth,
                childHeight,
                direction,
                MeasureMode.Exactly,
                MeasureMode.Exactly,
                childWidth,
                childHeight,
                true,
                "abs-layout",
                config);

            if (child.IsTrailingPosDefined(mainAxis) &&
                !child.IsLeadingPositionDefined(mainAxis))
                child.Layout.Position[mainAxis.ToLeadingEdge()] =
                    node.Layout.GetMeasuredDimension(mainAxis.ToDimension())               -
                    child.Layout.GetMeasuredDimension(mainAxis.ToDimension())              -
                    node.GetTrailingBorder(mainAxis)                                       -
                    YGGlobal.UnwrapFloatOptional(child.GetTrailingMargin(mainAxis, width)) -
                    YGGlobal.UnwrapFloatOptional(
                        child.GetTrailingPosition(
                            mainAxis,
                            isMainAxisRow ? width : height));
            else if (
                !child.IsLeadingPositionDefined(mainAxis) &&
                node.Style.JustifyContent == JustifyType.Center)
                child.Layout.Position[mainAxis.ToLeadingEdge()] =
                    (node.Layout.GetMeasuredDimension(mainAxis.ToDimension()) -
                        child.Layout.GetMeasuredDimension(mainAxis.ToDimension())) / 2.0f;
            else if (
                !child.IsLeadingPositionDefined(mainAxis) &&
                node.Style.JustifyContent == JustifyType.FlexEnd)
                child.Layout.Position[mainAxis.ToLeadingEdge()] =
                    node.Layout.GetMeasuredDimension(mainAxis.ToDimension()) -
                    child.Layout.GetMeasuredDimension(mainAxis.ToDimension());

            if (child.IsTrailingPosDefined(crossAxis) &&
                !child.IsLeadingPositionDefined(crossAxis))
                child.Layout.Position[crossAxis.ToLeadingEdge()] =
                    node.Layout.GetMeasuredDimension(crossAxis.ToDimension())               -
                    child.Layout.GetMeasuredDimension(crossAxis.ToDimension())              -
                    node.GetTrailingBorder(crossAxis)                                       -
                    YGGlobal.UnwrapFloatOptional(child.GetTrailingMargin(crossAxis, width)) -
                    YGGlobal.UnwrapFloatOptional(
                        child.GetTrailingPosition(
                            crossAxis,
                            isMainAxisRow ? height : width));
            else if (
                !child.IsLeadingPositionDefined(crossAxis) &&
                node.YGNodeAlignItem(child) == YGAlign.Center)
                child.Layout.Position[crossAxis.ToLeadingEdge()] =
                    (node.Layout.GetMeasuredDimension(crossAxis.ToDimension()) -
                        child.Layout.GetMeasuredDimension(crossAxis.ToDimension())) /
                    2.0f;

            else if (
                !child.IsLeadingPositionDefined(crossAxis) &&
                (node.YGNodeAlignItem(child) == YGAlign.FlexEnd) ^
                (node.Style.FlexWrap                   == WrapType.WrapReverse))
                child.Layout.Position[crossAxis.ToLeadingEdge()] =
                    node.Layout.GetMeasuredDimension(crossAxis.ToDimension()) -
                    child.Layout.GetMeasuredDimension(crossAxis.ToDimension());
        }
    }
}
