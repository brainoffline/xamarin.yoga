#if DEBUG
//#define PRINT_CHANGES
//#define PRINT_SKIPS
#endif

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Xamarin.Yoga
{
    public class NodeCalculator
    {
        private static int CurrentGenerationCount = 0;

        [ThreadStatic]
        private static int Depth;


        private readonly YGNode node;

        public NodeCalculator(YGNode node)
        {
            this.node = node;
        }

        //
        // This is a wrapper around the YGNodeLayoutImpl function. It determines
        // whether the layout request is redundant and can be skipped.
        //
        // Parameters:
        //  Input parameters are the same as YGNodeLayoutImpl (see above)
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

            Interlocked.Increment(ref Depth);

            var needToVisitNode =
                node.IsDirty && layout.GenerationCount != CurrentGenerationCount ||
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

#if PRINT_CHANGES
                YogaGlobal.Log(
                    node,
                    LogLevel.Verbose,
                    $"{Spacer(YGGlobal.gDepth)}{YGGlobal.gDepth}.[[skipped] ");

                node.PrintFunc?.Invoke(node);

                YogaGlobal.Log(
                    node,
                    LogLevel.Verbose,
                    $"{(performLayout ? "LAY " : "")}wm: {widthMeasureMode.ToDescription()}, hm: {heightMeasureMode.ToDescription()}, aw: {availableWidth} ah: {availableHeight} => d: ({cachedResults.ComputedWidth}, {cachedResults.ComputedHeight}) {reason}\n");
#endif
            }
            else
            {
#if PRINT_CHANGES
                YogaGlobal.Log(
                    node,
                    LogLevel.Verbose,
                    $"{Spacer(YGGlobal.gDepth)}{YGGlobal.gDepth}.{(needToVisitNode ? " * " : "")}");

                node.PrintFunc?.Invoke(node);

                YogaGlobal.Log(
                    node,
                    LogLevel.Verbose,
                    $"{(performLayout ? "LAY " : "")}wm: {widthMeasureMode.ToDescription()}, hm: {heightMeasureMode.ToDescription()}, aw: {availableWidth} ah: {availableHeight} {reason}\n");
#endif

                YGNodelayoutImpl(
                    availableWidth,
                    availableHeight,
                    ownerDirection,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight,
                    performLayout,
                    config);

#if PRINT_CHANGES
                YogaGlobal.Log(
                    node,
                    LogLevel.Verbose,
                    $"{Spacer(YGGlobal.gDepth)}{YGGlobal.gDepth}.]{(needToVisitNode ? "*" : "")}");

                node.PrintFunc?.Invoke(node);

                YogaGlobal.Log(
                    node,
                    LogLevel.Verbose,
                    $"{(performLayout ? "LAY " : "")}wm: {widthMeasureMode}, hm: {heightMeasureMode.ToDescription()}, d: ({layout.MeasuredWidth}, {layout.MeasuredHeight}) {reason}\n");
#endif

                layout.LastOwnerDirection = ownerDirection;

                if (cachedResults == null)
                {
                    if (layout.CachedMeasurementFull)
                    {
#if PRINT_CHANGES
                        YogaGlobal.Log(node, LogLevel.Verbose, "Out of cache entries!\n");
#endif

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

            Interlocked.Decrement(ref Depth);
            layout.GenerationCount = CurrentGenerationCount;

            return needToVisitNode || cachedResults == null;
        }

        public void CalculateLayout(
            float         ownerWidth,
            float         ownerHeight,
            DirectionType ownerDirection)
        {
            // Increment the generation count. This will force the recursive routine to visit
            // all dirty nodes at least once. Subsequent visits will be skipped if the input
            // parameters don't change.
            Interlocked.Increment(ref CurrentGenerationCount);

            node.ResolveDimension();
            var width            = Single.NaN;
            var widthMeasureMode = MeasureMode.Undefined;
            if (node.YGNodeIsStyleDimDefined(FlexDirectionType.Row, ownerWidth))
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
            if (node.YGNodeIsStyleDimDefined(FlexDirectionType.Column, ownerHeight))
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
                YGRoundToPixelGrid(node.Config.PointScaleFactor, 0.0f, 0.0f);

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
                if (maxSize.HasValue() && size > maxSize)
                    size = maxSize;
                break;
            case MeasureMode.Undefined:
                if (maxSize.HasValue())
                {
                    mode = MeasureMode.AtMost;
                    size = maxSize;
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

            if (child.YGNodeIsStyleDimDefined(FlexDirectionType.Row, width))
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

            if (child.YGNodeIsStyleDimDefined(FlexDirectionType.Column, height))
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

        internal void YGNodeComputeFlexBasisForChild(
            YGNode        child,
            float         width,
            MeasureMode   widthMode,
            float         height,
            float         ownerWidth,
            float         ownerHeight,
            MeasureMode   heightMode,
            DirectionType direction,
            YogaConfig    config)
        {
            var mainAxis          = YGGlobal.ResolveFlexDirection(node.Style.FlexDirection, direction);
            var isMainAxisRow     = mainAxis.IsRow();
            var mainAxisSize      = isMainAxisRow ? width : height;
            var mainAxisownerSize = isMainAxisRow ? ownerWidth : ownerHeight;

            float       childWidth;
            float       childHeight;
            MeasureMode childWidthMeasureMode;
            MeasureMode childHeightMeasureMode;

            var resolvedFlexBasis = child.ResolveFlexBasisPtr().ResolveValue(mainAxisownerSize);
            var isRowStyleDimDefined = child.YGNodeIsStyleDimDefined(FlexDirectionType.Row, ownerWidth);
            var isColumnStyleDimDefined = child.YGNodeIsStyleDimDefined(FlexDirectionType.Column, ownerHeight);

            if (resolvedFlexBasis.HasValue() && mainAxisSize.HasValue())
            {
                if (child.Layout.ComputedFlexBasis.IsNaN() ||
                    child.Config.ExperimentalFeatures.HasFlag(ExperimentalFeatures.WebFlexBasis) &&
                    child.Layout.ComputedFlexBasisGeneration != CurrentGenerationCount)
                {
                    var paddingAndBorder = new float?(child.YGNodePaddingAndBorderForAxis(mainAxis, ownerWidth));
                    child.Layout.ComputedFlexBasis = NumberExtensions.FloatOptionalMax(resolvedFlexBasis, paddingAndBorder);
                }
            }
            else if (isMainAxisRow && isRowStyleDimDefined)
            {
                // The width is definite, so use that as the flex basis.
                var paddingAndBorder = new float?(
                    child.YGNodePaddingAndBorderForAxis(FlexDirectionType.Row, ownerWidth));

                child.Layout.ComputedFlexBasis = NumberExtensions.FloatOptionalMax(
                    child.ResolvedDimension.Width.ResolveValue(ownerWidth),
                    paddingAndBorder);
            }
            else if (!isMainAxisRow && isColumnStyleDimDefined)
            {
                // The height is definite, so use that as the flex basis.
                var paddingAndBorder = child.YGNodePaddingAndBorderForAxis(FlexDirectionType.Column, ownerWidth);
                child.Layout.ComputedFlexBasis = NumberExtensions.FloatOptionalMax(
                    child.ResolvedDimension.Height.ResolveValue(ownerHeight), paddingAndBorder);
            }
            else
            {
                // Compute the flex basis and hypothetical main size (i.e. the clamped
                // flex basis).
                childWidth             = Single.NaN;
                childHeight            = Single.NaN;
                childWidthMeasureMode  = MeasureMode.Undefined;
                childHeightMeasureMode = MeasureMode.Undefined;

                var marginRow = YGGlobal.UnwrapFloatOptional(
                    child.GetMarginForAxis(FlexDirectionType.Row, ownerWidth));
                var marginColumn = YGGlobal.UnwrapFloatOptional(
                    child.GetMarginForAxis(FlexDirectionType.Column, ownerWidth));

                if (isRowStyleDimDefined)
                {
                    childWidth            = YGGlobal.UnwrapFloatOptional(child.ResolvedDimension.Width.ResolveValue(ownerWidth)) + marginRow;
                    childWidthMeasureMode = MeasureMode.Exactly;
                }

                if (isColumnStyleDimDefined)
                {
                    childHeight            = YGGlobal.UnwrapFloatOptional(child.ResolvedDimension.Height.ResolveValue(ownerHeight)) + marginColumn;
                    childHeightMeasureMode = MeasureMode.Exactly;
                }

                // The W3C spec doesn't say anything about the 'overflow' property,
                // but all major browsers appear to implement the following logic.
                if (!isMainAxisRow && node.Style.Overflow == OverflowType.Scroll ||
                    node.Style.Overflow != OverflowType.Scroll)
                    if (childWidth.IsNaN() && width.HasValue())
                    {
                        childWidth            = width;
                        childWidthMeasureMode = MeasureMode.AtMost;
                    }

                if (isMainAxisRow && node.Style.Overflow == OverflowType.Scroll ||
                    node.Style.Overflow != OverflowType.Scroll)
                    if (childHeight.IsNaN() && height.HasValue())
                    {
                        childHeight            = height;
                        childHeightMeasureMode = MeasureMode.AtMost;
                    }

                if (child.Style.AspectRatio.HasValue)
                {
                    if (!isMainAxisRow && childWidthMeasureMode == MeasureMode.Exactly)
                    {
                        childHeight = marginColumn +
                            (childWidth - marginRow) / child.Style.AspectRatio.Value;
                        childHeightMeasureMode = MeasureMode.Exactly;
                    }
                    else if (
                        isMainAxisRow && childHeightMeasureMode == MeasureMode.Exactly)
                    {
                        childWidth = marginRow +
                            (childHeight - marginColumn) *
                            child.Style.AspectRatio.Value;
                        childWidthMeasureMode = MeasureMode.Exactly;
                    }
                }

                // If child has no defined size in the cross axis and is set to stretch,
                // set the cross
                // axis to be measured exactly with the available inner width

                var hasExactWidth = width.HasValue() && widthMode == MeasureMode.Exactly;
                var childWidthStretch = node.YGNodeAlignItem(child) == YGAlign.Stretch &&
                    childWidthMeasureMode                           != MeasureMode.Exactly;
                if (!isMainAxisRow && !isRowStyleDimDefined && hasExactWidth &&
                    childWidthStretch)
                {
                    childWidth            = width;
                    childWidthMeasureMode = MeasureMode.Exactly;
                    if (child.Style.AspectRatio.HasValue)
                    {
                        childHeight            = (childWidth - marginRow) / child.Style.AspectRatio.Value;
                        childHeightMeasureMode = MeasureMode.Exactly;
                    }
                }

                var hasExactHeight = height.HasValue() && heightMode == MeasureMode.Exactly;
                var childHeightStretch = node.YGNodeAlignItem(child) == YGAlign.Stretch &&
                    childHeightMeasureMode                           != MeasureMode.Exactly;
                if (isMainAxisRow && !isColumnStyleDimDefined && hasExactHeight &&
                    childHeightStretch)
                {
                    childHeight            = height;
                    childHeightMeasureMode = MeasureMode.Exactly;

                    if (child.Style.AspectRatio.HasValue)
                    {
                        childWidth = (childHeight - marginColumn) *
                            child.Style.AspectRatio.Value;
                        childWidthMeasureMode = MeasureMode.Exactly;
                    }
                }

                child.Calc.YGConstrainMaxSizeForMode(
                    FlexDirectionType.Row,
                    ownerWidth,
                    ownerWidth,
                    ref childWidthMeasureMode,
                    ref childWidth);
                child.Calc.YGConstrainMaxSizeForMode(
                    FlexDirectionType.Column,
                    ownerHeight,
                    ownerWidth,
                    ref childHeightMeasureMode,
                    ref childHeight);

                // Measure the child
                child.Calc.LayoutInternal(
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

                child.Layout.ComputedFlexBasis = NumberExtensions.FloatMax(
                    child.Layout.GetMeasuredDimension(mainAxis.ToDimension()),
                    child.YGNodePaddingAndBorderForAxis(mainAxis, ownerWidth));
            }

            child.Layout.ComputedFlexBasisGeneration = CurrentGenerationCount;
        }

        // For nodes with no children, use the available values if they were provided,
        // or the minimum size as indicated by the padding and border sizes.

        internal void YGNodeEmptyContainerSetMeasuredDimensions(
            float       availableWidth,
            float       availableHeight,
            MeasureMode widthMeasureMode,
            MeasureMode heightMeasureMode,
            float       ownerWidth,
            float       ownerHeight)
        {
            var paddingAndBorderAxisRow    = node.YGNodePaddingAndBorderForAxis(FlexDirectionType.Row,    ownerWidth);
            var paddingAndBorderAxisColumn = node.YGNodePaddingAndBorderForAxis(FlexDirectionType.Column, ownerWidth);
            var marginAxisRow              = node.GetMarginForAxis(FlexDirectionType.Row,    ownerWidth);
            var marginAxisColumn           = node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth);

            node.Layout.SetMeasuredDimension(
                DimensionType.Width,
                node.YGNodeBoundAxis(
                    FlexDirectionType.Row,
                    widthMeasureMode == MeasureMode.Undefined ||
                    widthMeasureMode == MeasureMode.AtMost
                        ? paddingAndBorderAxisRow
                        : availableWidth - marginAxisRow,
                    ownerWidth,
                    ownerWidth)
            );

            node.Layout.SetMeasuredDimension(
                DimensionType.Height,
                node.YGNodeBoundAxis(
                    FlexDirectionType.Column,
                    heightMeasureMode == MeasureMode.Undefined ||
                    heightMeasureMode == MeasureMode.AtMost
                        ? paddingAndBorderAxisColumn
                        : availableHeight - marginAxisColumn,
                    ownerHeight,
                    ownerWidth)
            );
        }

        internal bool YGNodeFixedSizeSetMeasuredDimensions(
            float       availableWidth,
            float       availableHeight,
            MeasureMode widthMeasureMode,
            MeasureMode heightMeasureMode,
            float       ownerWidth,
            float       ownerHeight)
        {
            if (availableWidth.HasValue()              &&
                widthMeasureMode == MeasureMode.AtMost && availableWidth <= 0.0f ||
                availableHeight.HasValue()              &&
                heightMeasureMode == MeasureMode.AtMost && availableHeight <= 0.0f ||
                widthMeasureMode  == MeasureMode.Exactly &&
                heightMeasureMode == MeasureMode.Exactly)
            {
                var marginAxisColumn = node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth);
                var marginAxisRow = node.GetMarginForAxis(FlexDirectionType.Row, ownerWidth);

                node.Layout.SetMeasuredDimension(
                    DimensionType.Width,
                    node.YGNodeBoundAxis(
                        FlexDirectionType.Row,
                        availableWidth.IsNaN() ||
                        widthMeasureMode == MeasureMode.AtMost &&
                        availableWidth   < 0.0f
                            ? 0.0f
                            : availableWidth - marginAxisRow,
                        ownerWidth,
                        ownerWidth)
                );

                node.Layout.SetMeasuredDimension(
                    DimensionType.Height,
                    node.YGNodeBoundAxis(
                        FlexDirectionType.Column,
                        availableHeight.IsNaN() ||
                        heightMeasureMode == MeasureMode.AtMost &&
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

        internal void YGNodelayoutImpl(
            float         availableWidth,
            float         availableHeight,
            DirectionType ownerDirection,
            MeasureMode   widthMeasureMode,
            MeasureMode   heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight,
            bool          performLayout,
            YogaConfig    config)
        {
            YogaGlobal.YGAssert(
                node,
                availableWidth.IsNaN()
                    ? widthMeasureMode == MeasureMode.Undefined
                    : true,
                "availableWidth is indefinite so widthMeasureMode must be YGMeasureMode.Undefined");
            YogaGlobal.YGAssert(
                node,
                availableHeight.IsNaN()
                    ? heightMeasureMode == MeasureMode.Undefined
                    : true,
                "availableHeight is indefinite so heightMeasureMode must be YGMeasureMode.Undefined");

            // Set the resolved resolution in the node's layout.
            var direction = node.ResolveDirection(ownerDirection);
            node.Layout.Direction = direction;

            var flexRowDirection    = YGGlobal.ResolveFlexDirection(FlexDirectionType.Row,    direction);
            var flexColumnDirection = YGGlobal.ResolveFlexDirection(FlexDirectionType.Column, direction);

            node.Layout.Margin.Start  = YGGlobal.UnwrapFloatOptional(node.GetLeadingMargin(flexRowDirection, ownerWidth));
            node.Layout.Margin.End    = YGGlobal.UnwrapFloatOptional(node.GetTrailingMargin(flexRowDirection, ownerWidth));
            node.Layout.Margin.Top    = YGGlobal.UnwrapFloatOptional(node.GetLeadingMargin(flexColumnDirection, ownerWidth));
            node.Layout.Margin.Bottom = YGGlobal.UnwrapFloatOptional(node.GetTrailingMargin(flexColumnDirection, ownerWidth));

            node.Layout.Border.Start  = node.GetLeadingBorder(flexRowDirection);
            node.Layout.Border.End    = node.GetTrailingBorder(flexRowDirection);
            node.Layout.Border.Top    = node.GetLeadingBorder(flexColumnDirection);
            node.Layout.Border.Bottom = node.GetTrailingBorder(flexColumnDirection);

            node.Layout.Padding.Start  = YGGlobal.UnwrapFloatOptional(node.GetLeadingPadding(flexRowDirection, ownerWidth));
            node.Layout.Padding.End    = YGGlobal.UnwrapFloatOptional(node.GetTrailingPadding(flexRowDirection, ownerWidth));
            node.Layout.Padding.Top    = YGGlobal.UnwrapFloatOptional(node.GetLeadingPadding(flexColumnDirection, ownerWidth));
            node.Layout.Padding.Bottom = YGGlobal.UnwrapFloatOptional(node.GetTrailingPadding(flexColumnDirection, ownerWidth));

            if (node.MeasureFunc != null)
            {
                YGNodeWithMeasureFuncSetMeasuredDimensions(
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
                node.Calc.YGNodeEmptyContainerSetMeasuredDimensions(
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
            if (!performLayout && node.Calc.YGNodeFixedSizeSetMeasuredDimensions(
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
            var mainAxis       = YGGlobal.ResolveFlexDirection(node.Style.FlexDirection, direction);
            var crossAxis      = YGGlobal.FlexDirectionCross(mainAxis, direction);
            var isMainAxisRow  = mainAxis.IsRow();
            var isNodeFlexWrap = node.Style.FlexWrap != WrapType.NoWrap;

            var mainAxisownerSize  = isMainAxisRow ? ownerWidth : ownerHeight;
            var crossAxisownerSize = isMainAxisRow ? ownerHeight : ownerWidth;

            var leadingPaddingAndBorderCross = YGGlobal.UnwrapFloatOptional(node.GetLeadingPaddingAndBorder(crossAxis, ownerWidth));
            var paddingAndBorderAxisMain     = node.YGNodePaddingAndBorderForAxis(mainAxis,  ownerWidth);
            var paddingAndBorderAxisCross    = node.YGNodePaddingAndBorderForAxis(crossAxis, ownerWidth);

            var measureModeMainDim  = isMainAxisRow ? widthMeasureMode : heightMeasureMode;
            var measureModeCrossDim = isMainAxisRow ? heightMeasureMode : widthMeasureMode;

            var paddingAndBorderAxisRow    = isMainAxisRow ? paddingAndBorderAxisMain : paddingAndBorderAxisCross;
            var paddingAndBorderAxisColumn = isMainAxisRow ? paddingAndBorderAxisCross : paddingAndBorderAxisMain;

            var marginAxisRow    = YGGlobal.UnwrapFloatOptional(node.GetMarginForAxis(FlexDirectionType.Row,    ownerWidth));
            var marginAxisColumn = YGGlobal.UnwrapFloatOptional(node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth));

            var minInnerWidth = YGGlobal.UnwrapFloatOptional(
                node.Style.MinWidth.ResolveValue(ownerWidth)) - paddingAndBorderAxisRow;
            var maxInnerWidth =
                YGGlobal.UnwrapFloatOptional(
                    node.Style.MaxWidth.ResolveValue(ownerWidth)) - paddingAndBorderAxisRow;
            var minInnerHeight =
                YGGlobal.UnwrapFloatOptional(
                    node.Style.MinHeight.ResolveValue(ownerHeight)) - paddingAndBorderAxisColumn;
            var maxInnerHeight =
                YGGlobal.UnwrapFloatOptional(
                    node.Style.MaxHeight.ResolveValue(ownerHeight)) - paddingAndBorderAxisColumn;

            var minInnerMainDim = isMainAxisRow ? minInnerWidth : minInnerHeight;
            var maxInnerMainDim = isMainAxisRow ? maxInnerWidth : maxInnerHeight;

            // STEP 2: DETERMINE AVAILABLE SIZE IN MAIN AND CROSS DIRECTIONS

            var availableInnerWidth = node.YGNodeCalculateAvailableInnerDim(
                FlexDirectionType.Row,
                availableWidth,
                ownerWidth);
            var availableInnerHeight = node.YGNodeCalculateAvailableInnerDim(
                FlexDirectionType.Column,
                availableHeight,
                ownerHeight);

            var availableInnerMainDim  = isMainAxisRow ? availableInnerWidth : availableInnerHeight;
            var availableInnerCrossDim = isMainAxisRow ? availableInnerHeight : availableInnerWidth;

            float totalOuterFlexBasis = 0;

            // STEP 3: DETERMINE FLEX BASIS FOR EACH ITEM

            node.Calc.YGNodeComputeFlexBasisForChildren(
                availableInnerWidth,
                availableInnerHeight,
                widthMeasureMode,
                heightMeasureMode,
                direction,
                mainAxis,
                config,
                performLayout,
                ref totalOuterFlexBasis);

            var flexBasisOverflows = measureModeMainDim == MeasureMode.Undefined
                ? false
                : totalOuterFlexBasis > availableInnerMainDim;
            if (isNodeFlexWrap && flexBasisOverflows &&
                measureModeMainDim == MeasureMode.AtMost)
                measureModeMainDim = MeasureMode.Exactly;
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
                collectedFlexItemsValues = node.Calc.CalculateCollectFlexItemsRowValues(
                    ownerDirection,
                    mainAxisownerSize,
                    availableInnerWidth,
                    availableInnerMainDim,
                    startOfLineIndex,
                    lineCount);
                endOfLineIndex = collectedFlexItemsValues.EndOfLineIndex;

                // If we don't need to measure the cross axis, we can skip the entire flex
                // step.
                var canSkipFlex = !performLayout && measureModeCrossDim == MeasureMode.Exactly;

                // STEP 5: RESOLVING FLEXIBLE LENGTHS ON MAIN AXIS
                // Calculate the remaining available space that needs to be allocated.
                // If the main dimension size isn't known, it is computed based on
                // the line length, so there's no more space left to distribute.

                var sizeBasedOnContent = false;
                // If we don't measure with exact main dimension we want to ensure we don't
                // violate min and max
                if (measureModeMainDim != MeasureMode.Exactly)
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
                        if (collectedFlexItemsValues.TotalFlexGrowFactors.IsNaN() &&
                            collectedFlexItemsValues.TotalFlexGrowFactors == 0 || node.ResolveFlexGrow().IsNaN() &&
                            node.ResolveFlexGrow() == 0f)
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
                {
                    collectedFlexItemsValues.YGResolveFlexibleLength(
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
                }

                node.Layout.HadOverflow = node.Layout.HadOverflow | (collectedFlexItemsValues.RemainingFreeSpace < 0);

                // STEP 6: MAIN-AXIS JUSTIFICATION & CROSS-AXIS SIZE DETERMINATION

                // At this point, all the children have their dimensions set in the main
                // axis.
                // Their dimensions are also set in the cross axis with the exception of
                // items
                // that are aligned "stretch". We need to compute these stretch values and
                // set the final positions.

                collectedFlexItemsValues.YGJustifyMainAxis(
                    node,
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
                if (measureModeCrossDim == MeasureMode.Undefined ||
                    measureModeCrossDim == MeasureMode.AtMost)
                    containerCrossAxis = node.YGNodeBoundAxis(
                            crossAxis,
                            collectedFlexItemsValues.CrossDim + paddingAndBorderAxisCross,
                            crossAxisownerSize,
                            ownerWidth) -
                        paddingAndBorderAxisCross;

                // If there's no flex wrap, the cross dimension is defined by the container.
                if (!isNodeFlexWrap && measureModeCrossDim == MeasureMode.Exactly) collectedFlexItemsValues.CrossDim = availableInnerCrossDim;

                // Clamp to the min/max size specified on the container.
                collectedFlexItemsValues.CrossDim = node.YGNodeBoundAxis(
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
                        if (child.Style.Display == DisplayType.None) continue;

                        if (child.Style.PositionType == PositionType.Absolute)
                        {
                            // If the child is absolutely positioned and has a
                            // top/left/bottom/right set, override
                            // all the previously computed positions to set it correctly.
                            var isChildLeadingPosDefined =
                                child.IsLeadingPositionDefined(crossAxis);
                            if (isChildLeadingPosDefined)
                                child.Layout.Position[crossAxis.ToPositionEdge()] =
                                    YGGlobal.UnwrapFloatOptional(
                                        child.GetLeadingPosition(
                                            crossAxis,
                                            availableInnerCrossDim)) +
                                    node.GetLeadingBorder(crossAxis) +
                                    YGGlobal.UnwrapFloatOptional(
                                        child.GetLeadingMargin(
                                            crossAxis,
                                            availableInnerWidth));

                            // If leading position is not defined or calculations result in Nan,
                            // default to border + margin
                            if (!isChildLeadingPosDefined ||
                                child.Layout.Position[crossAxis.ToPositionEdge()].IsNaN())
                                child.Layout.Position[crossAxis.ToPositionEdge()] =
                                    node.GetLeadingBorder(crossAxis) + YGGlobal.UnwrapFloatOptional(child.GetLeadingMargin(crossAxis, availableInnerWidth));
                        }
                        else
                        {
                            var leadingCrossDim = leadingPaddingAndBorderCross;

                            // For a relative children, we're either using alignItems (owner) or
                            // alignSelf (child) in order to determine the position in the cross
                            // axis
                            var alignItem = node.YGNodeAlignItem(child);

                            // If the child uses align stretch, we need to lay it out one more
                            // time, this time
                            // forcing the cross-axis size to be the computed cross size for the
                            // current line.
                            if (alignItem                                 == YGAlign.Stretch &&
                                child.MarginLeadingValue(crossAxis).Unit  != ValueUnit.Auto  &&
                                child.MarginTrailingValue(crossAxis).Unit != ValueUnit.Auto)
                            {
                                // If the child defines a definite size for its cross axis, there's
                                // no need to stretch.
                                if (!child.YGNodeIsStyleDimDefined(crossAxis, availableInnerCrossDim))
                                {
                                    var childMainSize = child.Layout.GetMeasuredDimension(mainAxis.ToDimension());
                                    var childCrossSize =
                                        child.Style.AspectRatio.HasValue
                                            ? YGGlobal.UnwrapFloatOptional(
                                                child.GetMarginForAxis(crossAxis, availableInnerWidth)) +
                                            (isMainAxisRow
                                                ? childMainSize / child.Style.AspectRatio.Value
                                                : childMainSize * child.Style.AspectRatio.Value)
                                            : collectedFlexItemsValues.CrossDim;

                                    childMainSize += YGGlobal.UnwrapFloatOptional(
                                        child.GetMarginForAxis(mainAxis, availableInnerWidth));

                                    var childMainMeasureMode  = MeasureMode.Exactly;
                                    var childCrossMeasureMode = MeasureMode.Exactly;
                                    child.Calc.YGConstrainMaxSizeForMode(
                                        mainAxis,
                                        availableInnerMainDim,
                                        availableInnerWidth,
                                        ref childMainMeasureMode,
                                        ref childMainSize);
                                    child.Calc.YGConstrainMaxSizeForMode(
                                        crossAxis,
                                        availableInnerCrossDim,
                                        availableInnerWidth,
                                        ref childCrossMeasureMode,
                                        ref childCrossSize);

                                    var childWidth  = isMainAxisRow ? childMainSize : childCrossSize;
                                    var childHeight = !isMainAxisRow ? childMainSize : childCrossSize;

                                    var childWidthMeasureMode =
                                        childWidth.IsNaN() ? MeasureMode.Undefined : MeasureMode.Exactly;
                                    var childHeightMeasureMode =
                                        childHeight.IsNaN() ? MeasureMode.Undefined : MeasureMode.Exactly;

                                    child.Calc.LayoutInternal(
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
                                var remainingCrossDim = containerCrossAxis - child.YGNodeDimWithMargin(crossAxis, availableInnerWidth);

                                if (child.MarginLeadingValue(crossAxis).Unit  == ValueUnit.Auto &&
                                    child.MarginTrailingValue(crossAxis).Unit == ValueUnit.Auto)
                                {
                                    leadingCrossDim += NumberExtensions.FloatMax(0.0f, remainingCrossDim / 2);
                                }
                                else if (
                                    child.MarginTrailingValue(crossAxis).Unit == ValueUnit.Auto)
                                {
                                    // No-Op
                                }
                                else if (
                                    child.MarginLeadingValue(crossAxis).Unit == ValueUnit.Auto)
                                {
                                    leadingCrossDim += NumberExtensions.FloatMax(0.0f, remainingCrossDim);
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
                            child.Layout.Position[crossAxis.ToPositionEdge()] =
                                child.Layout.Position[crossAxis.ToPositionEdge()] + totalLineCrossDim +
                                leadingCrossDim;
                        }
                    }

                totalLineCrossDim += collectedFlexItemsValues.CrossDim;
                maxLineMainDim    =  NumberExtensions.FloatMax(maxLineMainDim, collectedFlexItemsValues.MainDim);
            }

            // STEP 8: MULTI-LINE CONTENT ALIGNMENT
            // currentLead stores the size of the cross dim
            if (performLayout && (lineCount > 1 || node.YGIsBaselineLayout()))
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
                        if (child.Style.Display == DisplayType.None) continue;

                        if (child.Style.PositionType == PositionType.Relative)
                        {
                            if (child.LineIndex != i) break;

                            if (child.YGNodeIsLayoutDimDefined(crossAxis))
                                lineHeight = NumberExtensions.FloatMax(
                                    lineHeight,
                                    child.Layout.GetMeasuredDimension(crossAxis.ToDimension()) +
                                    YGGlobal.UnwrapFloatOptional(
                                        child.GetMarginForAxis(
                                            crossAxis,
                                            availableInnerWidth)));

                            if (node.YGNodeAlignItem(child) == YGAlign.Baseline)
                            {
                                var ascent = child.Baseline() +
                                    YGGlobal.UnwrapFloatOptional(
                                        child.GetLeadingMargin(
                                            FlexDirectionType.Column,
                                            availableInnerWidth));
                                var descent =
                                    child.Layout.MeasuredHeight +
                                    YGGlobal.UnwrapFloatOptional(
                                        child.GetMarginForAxis(
                                            FlexDirectionType.Column,
                                            availableInnerWidth)) -
                                    ascent;
                                maxAscentForCurrentLine  = NumberExtensions.FloatMax(maxAscentForCurrentLine,  ascent);
                                maxDescentForCurrentLine = NumberExtensions.FloatMax(maxDescentForCurrentLine, descent);
                                lineHeight = NumberExtensions.FloatMax(
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
                            if (child.Style.Display == DisplayType.None) continue;

                            if (child.Style.PositionType == PositionType.Relative)
                                switch (node.YGNodeAlignItem(child))
                                {
                                case YGAlign.FlexStart:
                                {
                                    child.Layout.Position[crossAxis.ToPositionEdge()] =
                                        currentLead +
                                        YGGlobal.UnwrapFloatOptional(child.GetLeadingMargin(crossAxis, availableInnerWidth));
                                    break;
                                }
                                case YGAlign.FlexEnd:
                                {
                                    child.Layout.Position[crossAxis.ToPositionEdge()] =
                                        currentLead + lineHeight -
                                        YGGlobal.UnwrapFloatOptional(
                                            child.GetTrailingMargin(
                                                crossAxis,
                                                availableInnerWidth)) -
                                        child.Layout.GetMeasuredDimension(crossAxis.ToDimension());
                                    break;
                                }
                                case YGAlign.Center:
                                {
                                    var childHeight =
                                        child.Layout.GetMeasuredDimension(crossAxis.ToDimension());

                                    child.Layout.Position[crossAxis.ToPositionEdge()] =
                                        currentLead + (lineHeight - childHeight) / 2;
                                    break;
                                }
                                case YGAlign.Stretch:
                                {
                                    child.Layout.Position[crossAxis.ToPositionEdge()] =
                                        currentLead +
                                        YGGlobal.UnwrapFloatOptional(child.GetLeadingMargin(crossAxis, availableInnerWidth));

                                    // Remeasure child with the line height as it as been only
                                    // measured with the owners height yet.
                                    if (!child.YGNodeIsStyleDimDefined(crossAxis, availableInnerCrossDim))
                                    {
                                        var childWidth = isMainAxisRow
                                            ? child.Layout.MeasuredWidth +

                                            child.GetMarginForAxis(
                                                mainAxis,
                                                availableInnerWidth)
                                            : lineHeight;

                                        var childHeight = !isMainAxisRow
                                            ? child.Layout.MeasuredHeight +

                                            child.GetMarginForAxis(
                                                crossAxis,
                                                availableInnerWidth)
                                            : lineHeight;

                                        if (!(NumberExtensions.FloatEqual(childWidth, child.Layout.MeasuredWidth) &&
                                            NumberExtensions.FloatEqual(childHeight,  child.Layout.MeasuredHeight)))
                                        {
                                            child.Calc.LayoutInternal(
                                                childWidth,
                                                childHeight,
                                                direction,
                                                MeasureMode.Exactly,
                                                MeasureMode.Exactly,
                                                availableInnerWidth,
                                                availableInnerHeight,
                                                true,
                                                "multiline-stretch",
                                                config);
                                        }
                                    }

                                    break;
                                }
                                case YGAlign.Baseline:
                                {
                                    child.Layout.Position[EdgeType.Top] =
                                        currentLead + maxAscentForCurrentLine - child.Baseline() +
                                        YGGlobal.UnwrapFloatOptional(
                                            child.GetLeadingPosition(
                                                FlexDirectionType.Column,
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
                DimensionType.Width,
                node.YGNodeBoundAxis(
                    FlexDirectionType.Row,
                    availableWidth - marginAxisRow,
                    ownerWidth,
                    ownerWidth)
            );

            node.Layout.SetMeasuredDimension(
                DimensionType.Height,
                node.YGNodeBoundAxis(
                    FlexDirectionType.Column,
                    availableHeight - marginAxisColumn,
                    ownerHeight,
                    ownerWidth)
            );

            // If the user didn't specify a width or height for the node, set the
            // dimensions based on the children.
            if (measureModeMainDim == MeasureMode.Undefined ||
                node.Style.Overflow != OverflowType.Scroll &&
                measureModeMainDim  == MeasureMode.AtMost)
                node.Layout.SetMeasuredDimension(
                    mainAxis.ToDimension(),
                    node.YGNodeBoundAxis(
                        mainAxis,
                        maxLineMainDim,
                        mainAxisownerSize,
                        ownerWidth)
                );
            else if (
                measureModeMainDim  == MeasureMode.AtMost &&
                node.Style.Overflow == OverflowType.Scroll)
                node.Layout.SetMeasuredDimension(
                    mainAxis.ToDimension(),
                    NumberExtensions.FloatMax(
                        NumberExtensions.FloatMin(
                            availableInnerMainDim + paddingAndBorderAxisMain,
                            YGGlobal.UnwrapFloatOptional(
                                node.YGNodeBoundAxisWithinMinAndMax(
                                    mainAxis,
                                    maxLineMainDim,
                                    mainAxisownerSize))),
                        paddingAndBorderAxisMain)
                );

            if (measureModeCrossDim == MeasureMode.Undefined ||
                node.Style.Overflow != OverflowType.Scroll &&
                measureModeCrossDim == MeasureMode.AtMost)
                node.Layout.SetMeasuredDimension(
                    crossAxis.ToDimension(),
                    node.YGNodeBoundAxis(
                        crossAxis,
                        totalLineCrossDim + paddingAndBorderAxisCross,
                        crossAxisownerSize,
                        ownerWidth)
                );
            else if (
                measureModeCrossDim == MeasureMode.AtMost &&
                node.Style.Overflow == OverflowType.Scroll)
                node.Layout.SetMeasuredDimension(
                    crossAxis.ToDimension(),
                    NumberExtensions.FloatMax(
                        NumberExtensions.FloatMin(
                            availableInnerCrossDim + paddingAndBorderAxisCross,
                            YGGlobal.UnwrapFloatOptional(
                                node.YGNodeBoundAxisWithinMinAndMax(
                                    crossAxis,
                                    totalLineCrossDim + paddingAndBorderAxisCross,
                                    crossAxisownerSize))),
                        paddingAndBorderAxisCross)
                );

            // As we only wrapped in normal direction yet, we need to reverse the
            // positions on wrap-reverse.
            if (performLayout && node.Style.FlexWrap == WrapType.WrapReverse)
                for (var i = 0; i < childCount; i++)
                {
                    var child = node.Children[i];
                    if (child.Style.PositionType == PositionType.Relative)
                        child.Layout.Position[crossAxis.ToPositionEdge()] =
                            node.Layout.GetMeasuredDimension(crossAxis.ToDimension()) -
                            child.Layout.Position[crossAxis.ToPositionEdge()]         -
                            child.Layout.GetMeasuredDimension(crossAxis.ToDimension());
                }

            if (performLayout)
            {
                // STEP 10: SIZING AND POSITIONING ABSOLUTE CHILDREN
                foreach (var child in node.Children)
                {
                    if (child.Style.PositionType != PositionType.Absolute) continue;

                    node.Calc.YGNodeAbsoluteLayoutChild(
                        child,
                        availableInnerWidth,
                        isMainAxisRow ? measureModeMainDim : measureModeCrossDim,
                        availableInnerHeight,
                        direction,
                        config);
                }

                // STEP 11: SETTING TRAILING POSITIONS FOR CHILDREN
                var needsMainTrailingPos = mainAxis == FlexDirectionType.RowReverse ||
                    mainAxis                        == FlexDirectionType.ColumnReverse;
                var needsCrossTrailingPos = crossAxis == FlexDirectionType.RowReverse ||
                    crossAxis                         == FlexDirectionType.ColumnReverse;

                // Set trailing position if necessary.
                if (needsMainTrailingPos || needsCrossTrailingPos)
                    for (var i = 0; i < childCount; i++)
                    {
                        var child = node.Children[i];
                        if (child.Style.Display == DisplayType.None) continue;

                        if (needsMainTrailingPos)
                            YGNodeSetChildTrailingPosition(child, mainAxis);

                        if (needsCrossTrailingPos)
                            YGNodeSetChildTrailingPosition(child, crossAxis);
                    }
            }
        }

        private void YGNodeComputeFlexBasisForChildren(
            float             availableInnerWidth,
            float             availableInnerHeight,
            MeasureMode       widthMeasureMode,
            MeasureMode       heightMeasureMode,
            DirectionType     direction,
            FlexDirectionType mainAxis,
            YogaConfig        config,
            bool              performLayout,
            ref float         totalOuterFlexBasis)
        {
            YGNode singleFlexChild    = null;
            var    children           = node.Children;
            var    measureModeMainDim = mainAxis.IsRow() ? widthMeasureMode : heightMeasureMode;
            // If there is only one child with flexGrow + flexShrink it means we can set
            // the computedFlexBasis to 0 instead of measuring and shrinking / flexing the
            // child to exactly match the remaining space
            if (measureModeMainDim == MeasureMode.Exactly)
                foreach (var child in children)
                    if (child.IsNodeFlexible())
                    {
                        if (singleFlexChild != null || NumberExtensions.FloatEqual(child.ResolveFlexGrow(), 0.0f) || NumberExtensions.FloatEqual(child.ResolveFlexShrink(), 0.0f))
                        {
                            // There is already a flexible child, or this flexible child doesn't
                            // have flexGrow and flexShrink, abort
                            singleFlexChild = null;
                            break;
                        }

                        singleFlexChild = child;
                    }

            foreach (var child in children)
            {
                child.ResolveDimension();
                if (child.Style.Display == DisplayType.None)
                {
                    child.YGZeroOutLayoutRecursively();
                    child.HasNewLayout = true;
                    child.IsDirty      = false;
                    continue;
                }

                if (performLayout)
                {
                    // Set the initial position (relative to the owner).
                    var childDirection = child.ResolveDirection(direction);
                    var mainDim = mainAxis.IsRow()
                        ? availableInnerWidth
                        : availableInnerHeight;
                    var crossDim = mainAxis.IsRow()
                        ? availableInnerHeight
                        : availableInnerWidth;
                    child.SetPosition(
                        childDirection,
                        mainDim,
                        crossDim,
                        availableInnerWidth);
                }

                if (child.Style.PositionType == PositionType.Absolute) continue;

                if (child == singleFlexChild)
                {
                    child.Layout.ComputedFlexBasisGeneration = CurrentGenerationCount;
                    child.Layout.ComputedFlexBasis           = 0f;
                }
                else
                {
                    node.Calc.YGNodeComputeFlexBasisForChild(
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

                totalOuterFlexBasis += YGGlobal.UnwrapFloatOptional(
                    child.Layout.ComputedFlexBasis +
                    child.GetMarginForAxis(mainAxis, availableInnerWidth));
            }
        }

        private static readonly string _spacer = "                                                            ";

        private static string Spacer(int level)
        {
            var spacerLen = _spacer.Length;
            if (level > spacerLen)
                return _spacer;
            return _spacer.Substring(spacerLen - level);
        }

        internal void YGNodeSetChildTrailingPosition(YGNode            child,FlexDirectionType axis)
        {
            var size = child.Layout.GetMeasuredDimension(axis.ToDimension());

            child.Layout.Position[axis.ToTrailingEdge()] =
                node.Layout.GetMeasuredDimension(axis.ToDimension()) 
                - size 
                - child.Layout.Position[axis.ToPositionEdge()];
        }

        internal void YGNodeWithMeasureFuncSetMeasuredDimensions(
            float       availableWidth,
            float       availableHeight,
            MeasureMode widthMeasureMode,
            MeasureMode heightMeasureMode,
            float       ownerWidth,
            float       ownerHeight)
        {
            YogaGlobal.YGAssert(
                node,
                node.MeasureFunc != null,
                "Expected node to have custom measure function");

            var paddingAndBorderAxisRow    = node.YGNodePaddingAndBorderForAxis(FlexDirectionType.Row,    availableWidth);
            var paddingAndBorderAxisColumn = node.YGNodePaddingAndBorderForAxis(FlexDirectionType.Column, availableWidth);
            var marginAxisRow              = YGGlobal.UnwrapFloatOptional(node.GetMarginForAxis(FlexDirectionType.Row,    availableWidth));
            var marginAxisColumn           = YGGlobal.UnwrapFloatOptional(node.GetMarginForAxis(FlexDirectionType.Column, availableWidth));

            // We want to make sure we don't call measure with negative size
            var innerWidth = availableWidth.IsNaN()
                ? availableWidth
                : NumberExtensions.FloatMax(0, availableWidth - marginAxisRow - paddingAndBorderAxisRow);
            var innerHeight = availableHeight.IsNaN()
                ? availableHeight
                : NumberExtensions.FloatMax(0, availableHeight - marginAxisColumn - paddingAndBorderAxisColumn);

            if (widthMeasureMode  == MeasureMode.Exactly &&
                heightMeasureMode == MeasureMode.Exactly)
            {
                // Don't bother sizing the text if both dimensions are already defined.
                node.Layout.SetMeasuredDimension(
                    DimensionType.Width,
                    node.YGNodeBoundAxis(
                        FlexDirectionType.Row,
                        availableWidth - marginAxisRow,
                        ownerWidth,
                        ownerWidth)
                );
                node.Layout.SetMeasuredDimension(
                    DimensionType.Height,
                    node.YGNodeBoundAxis(
                        FlexDirectionType.Column,
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
                    DimensionType.Width,
                    node.YGNodeBoundAxis(
                        FlexDirectionType.Row,
                        widthMeasureMode == MeasureMode.Undefined ||
                        widthMeasureMode == MeasureMode.AtMost
                            ? measuredSize.Width + paddingAndBorderAxisRow
                            : availableWidth     - marginAxisRow,
                        ownerWidth,
                        ownerWidth)
                );

                node.Layout.SetMeasuredDimension(
                    DimensionType.Height,
                    node.YGNodeBoundAxis(
                        FlexDirectionType.Column,
                        heightMeasureMode == MeasureMode.Undefined ||
                        heightMeasureMode == MeasureMode.AtMost
                            ? measuredSize.Height + paddingAndBorderAxisColumn
                            : availableHeight     - marginAxisColumn,
                        ownerHeight,
                        ownerWidth)
                );
            }
        }

        internal void YGRoundToPixelGrid(
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
            var textRounding = node.NodeType == NodeType.Text;

            node.Layout.Position.Left = NumberExtensions.RoundValueToPixelGrid(nodeLeft, pointScaleFactor, false, textRounding);
            node.Layout.Position.Top  = NumberExtensions.RoundValueToPixelGrid(nodeTop,  pointScaleFactor, false, textRounding);

            // We multiply dimension by scale factor and if the result is close to the
            // whole number, we don't have any fraction To verify if the result is close
            // to whole number we want to check both floor and ceil numbers
            var hasFractionalWidth =
                !NumberExtensions.FloatEqual(nodeWidth * pointScaleFactor % 1.0f, 0) &&
                !NumberExtensions.FloatEqual(nodeWidth * pointScaleFactor % 1.0f, 1.0f);
            var hasFractionalHeight =
                !NumberExtensions.FloatEqual(nodeHeight * pointScaleFactor % 1.0f, 0f) &&
                !NumberExtensions.FloatEqual(nodeHeight * pointScaleFactor % 1.0f, 1f);

            node.Layout.SetDimension(
                DimensionType.Width,
                NumberExtensions.RoundValueToPixelGrid(
                    absoluteNodeRight,
                    pointScaleFactor,
                    textRounding && hasFractionalWidth,
                    textRounding && !hasFractionalWidth) - NumberExtensions.RoundValueToPixelGrid(
                    absoluteNodeLeft,
                    pointScaleFactor,
                    false,
                    textRounding)
            );

            node.Layout.SetDimension(
                DimensionType.Height,
                NumberExtensions.RoundValueToPixelGrid(
                    absoluteNodeBottom,
                    pointScaleFactor,
                    textRounding && hasFractionalHeight,
                    textRounding && !hasFractionalHeight) - NumberExtensions.RoundValueToPixelGrid(
                    absoluteNodeTop,
                    pointScaleFactor,
                    false,
                    textRounding)
            );

            foreach (var child in node.Children)
                child.Calc.YGRoundToPixelGrid(
                    pointScaleFactor,
                    absoluteNodeLeft,
                    absoluteNodeTop);
        }
    }
}
