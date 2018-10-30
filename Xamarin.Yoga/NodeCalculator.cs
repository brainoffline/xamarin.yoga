#if DEBUG
//#define PRINT_CHANGES
//#define PRINT_SKIPS
#endif

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Xamarin.Yoga
{
    using static NumberExtensions;
    using static EnumExtensions;

    public class NodeCalculator
    {
        [ThreadStatic] private static int      _depth;
        private static                int      _currentGenerationCount;
        private readonly              YogaNode _node;

        public NodeCalculator(YogaNode node)
        {
            _node = node;
        }

        public void CalculateLayout(
            float         ownerWidth,
            float         ownerHeight,
            DirectionType ownerDirection)
        {
            // Increment the generation count. This will force the recursive routine to visit
            // all dirty nodes at least once. Subsequent visits will be skipped if the input
            // parameters don't change.
            Interlocked.Increment(ref _currentGenerationCount);

            _node.ResolveDimension();
            var width            = float.NaN;
            var widthMeasureMode = MeasureMode.Undefined;
            if (_node.IsStyleDimensionDefined(FlexDirectionType.Row, ownerWidth))
            {
                width =
                    _node.ResolvedDimension[FlexDirectionType.Row.ToDimension()].ResolveValue(ownerWidth) +
                    _node.GetMarginForAxis(FlexDirectionType.Row, ownerWidth);
                widthMeasureMode = MeasureMode.Exactly;
            }
            else if (!_node.Style.MaxWidth.ResolveValue(ownerWidth).IsNaN())
            {
                width            = _node.Style.MaxWidth.ResolveValue(ownerWidth);
                widthMeasureMode = MeasureMode.AtMost;
            }
            else
            {
                width = ownerWidth;
                widthMeasureMode = width.IsNaN()
                    ? MeasureMode.Undefined
                    : MeasureMode.Exactly;
            }

            var height            = float.NaN;
            var heightMeasureMode = MeasureMode.Undefined;
            if (_node.IsStyleDimensionDefined(FlexDirectionType.Column, ownerHeight))
            {
                height =
                    _node.ResolvedDimension[FlexDirectionType.Column.ToDimension()].ResolveValue(ownerHeight) +
                    _node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth);
                heightMeasureMode = MeasureMode.Exactly;
            }
            else if (!_node.Style.MaxHeight.ResolveValue(ownerHeight).IsNaN())
            {
                height            = _node.Style.MaxHeight.ResolveValue(ownerHeight);
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
                _node.Config))
            {
                _node.SetPosition(
                    _node.Layout.Direction,
                    ownerWidth,
                    ownerHeight,
                    ownerWidth);
                RoundToPixelGrid(_node.Config.PointScaleFactor, 0.0f, 0.0f);

                if (_node.Config.PrintTree)
                    _node.Print(PrintOptionType.All);
            }
        }

        private bool CanUseCachedMeasurement(
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
                ? RoundValueToPixelGrid(width, config.PointScaleFactor, false, false)
                : width;
            var effectiveHeight = useRoundedComparison
                ? RoundValueToPixelGrid(height, config.PointScaleFactor, false, false)
                : height;
            var effectiveLastWidth = useRoundedComparison
                ? RoundValueToPixelGrid(
                    lastWidth,
                    config.PointScaleFactor,
                    false,
                    false)
                : lastWidth;
            var effectiveLastHeight = useRoundedComparison
                ? RoundValueToPixelGrid(
                    lastHeight,
                    config.PointScaleFactor,
                    false,
                    false)
                : lastHeight;

            var hasSameWidthSpec = lastWidthMode == widthMode &&
                FloatEqual(effectiveLastWidth, effectiveWidth);
            var hasSameHeightSpec = lastHeightMode == heightMode &&
                FloatEqual(effectiveLastHeight, effectiveHeight);

            var widthIsCompatible =
                hasSameWidthSpec ||
                MeasureModeSizeIsExactAndMatchesOldMeasuredSize(
                    widthMode,
                    width - marginRow,
                    lastComputedWidth) ||
                MeasureModeOldSizeIsUnspecifiedAndStillFits(
                    widthMode,
                    width - marginRow,
                    lastWidthMode,
                    lastComputedWidth) ||
                MeasureModeNewMeasureSizeIsStricterAndStillValid(
                    widthMode,
                    width - marginRow,
                    lastWidthMode,
                    lastWidth,
                    lastComputedWidth);

            var heightIsCompatible =
                hasSameHeightSpec ||
                MeasureModeSizeIsExactAndMatchesOldMeasuredSize(
                    heightMode,
                    height - marginColumn,
                    lastComputedHeight) ||
                MeasureModeOldSizeIsUnspecifiedAndStillFits(
                    heightMode,
                    height - marginColumn,
                    lastHeightMode,
                    lastComputedHeight) ||
                MeasureModeNewMeasureSizeIsStricterAndStillValid(
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

        private CollectFlexItemsRowValues CalculateCollectFlexItemsRowValues(
            DirectionType ownerDirection,
            float         mainAxisOwnerSize,
            float         availableInnerWidth,
            float         availableInnerMainDim,
            int           startOfLineIndex,
            int           lineCount)
        {
            var flexAlgoRowMeasurement = new CollectFlexItemsRowValues
            {
                RelativeChildren = new List<YogaNode>(_node.Children.Count)
            };

            float sizeConsumedOnCurrentLineIncludingMinConstraint = 0;
            var   mainAxis                                        = ResolveFlexDirection(_node.Style.FlexDirection, _node.ResolveDirection(ownerDirection));
            var   isNodeFlexWrap                                  = _node.Style.FlexWrap != WrapType.NoWrap;

            // Add items to the current line until it's full or we run out of items.
            var endOfLineIndex = startOfLineIndex;
            for (; endOfLineIndex < _node.Children.Count; endOfLineIndex++)
            {
                var child = _node.Children[endOfLineIndex];
                if (child.Style.Display      == DisplayType.None ||
                    child.Style.PositionType == PositionType.Absolute)
                    continue;

                child.LineIndex = lineCount;
                var childMarginMainAxis = child.GetMarginForAxis(mainAxis, availableInnerWidth);
                var flexBasisWithMinAndMaxConstraints =
                    child.BoundAxisWithinMinAndMax(
                        mainAxis,
                        child.Layout.ComputedFlexBasis.Unwrap(),
                        mainAxisOwnerSize);

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
                        -child.ResolveFlexShrink() * child.Layout.ComputedFlexBasis.Unwrap();
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
            var layout = _node.Layout;

            Interlocked.Increment(ref _depth);

            var needToVisitNode =
                _node.IsDirty && layout.GenerationCount != _currentGenerationCount ||
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
            if (_node.MeasureFunc != null)
            {
                var marginAxisRow    = _node.GetMarginForAxis(FlexDirectionType.Row,    ownerWidth);
                var marginAxisColumn = _node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth);

                // First, try to use the layout cache.
                if (CanUseCachedMeasurement(
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
                        if (CanUseCachedMeasurement(
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

                LayoutImplementation(
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
                _node.Layout.Width  = _node.Layout.MeasuredWidth;
                _node.Layout.Height = _node.Layout.MeasuredHeight;

                _node.HasNewLayout = true;
                _node.IsDirty      = false;
            }

            Interlocked.Decrement(ref _depth);
            layout.GenerationCount = _currentGenerationCount;

            return needToVisitNode || cachedResults == null;
        }

        internal void ConstrainMaxSizeForMode(
            FlexDirectionType axis,
            float             ownerAxisSize,
            float             ownerWidth,
            ref MeasureMode   mode,
            ref float         size)
        {
            var maxSize =
                _node.Style.MaxDimension(axis.ToDimension()).ResolveValue(ownerAxisSize) +
                _node.GetMarginForAxis(axis, ownerWidth);
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

        internal void AbsoluteLayoutChild(
            YogaNode      child,
            float         width,
            MeasureMode   widthMode,
            float         height,
            DirectionType direction,
            YogaConfig    config)
        {
            var mainAxis      = ResolveFlexDirection(_node.Style.FlexDirection, direction);
            var crossAxis     = FlexDirectionCross(mainAxis, direction);
            var isMainAxisRow = mainAxis.IsRow();

            var childWidth             = float.NaN;
            var childHeight            = float.NaN;

            var marginRow    = child.GetMarginForAxis(FlexDirectionType.Row,    width);
            var marginColumn = child.GetMarginForAxis(FlexDirectionType.Column, width);

            if (child.IsStyleDimensionDefined(FlexDirectionType.Row, width))
            {
                childWidth = child.ResolvedDimension.Width.ResolveValue(width) + marginRow;
            }
            else
            {
                // If the child doesn't have a specified width, compute the width based
                // on the left/right
                // offsets if they're defined.
                if (child.IsLeadingPositionDefined(FlexDirectionType.Row) &&
                    child.IsTrailingPosDefined(FlexDirectionType.Row))
                {
                    childWidth = _node.Layout.MeasuredWidth
                        - (_node.GetLeadingBorder(FlexDirectionType.Row)
                            + _node.GetTrailingBorder(FlexDirectionType.Row))
                        - (child.GetLeadingPosition(FlexDirectionType.Row, width)
                            + child.GetTrailingPosition(FlexDirectionType.Row, width));
                    childWidth = child.BoundAxis(FlexDirectionType.Row, childWidth, width, width);
                }
            }

            if (child.IsStyleDimensionDefined(FlexDirectionType.Column, height))
            {
                childHeight = child.ResolvedDimension.Height.ResolveValue(height) + marginColumn;
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
                        _node.Layout.MeasuredHeight
                        - (_node.GetLeadingBorder(FlexDirectionType.Column)
                            + _node.GetTrailingBorder(FlexDirectionType.Column))
                        - (child.GetLeadingPosition(FlexDirectionType.Column, height)
                            + child.GetTrailingPosition(FlexDirectionType.Column, height));
                    childHeight = child.BoundAxis(
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
                        childWidth = marginRow + ((childHeight - marginColumn) * child.Style.AspectRatio.Value);
                    else if (childHeight.IsNaN())
                        childHeight = marginColumn + ((childWidth - marginRow) / child.Style.AspectRatio.Value);
                }

            // If we're still missing one or the other dimension, measure the content.
            if (childWidth.IsNaN() || childHeight.IsNaN())
            {
                var childWidthMeasureMode = childWidth.IsNaN() ? MeasureMode.Undefined : MeasureMode.Exactly;
                var childHeightMeasureMode = childHeight.IsNaN() ? MeasureMode.Undefined : MeasureMode.Exactly;

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
                childWidth  = child.Layout.MeasuredWidth  + child.GetMarginForAxis(FlexDirectionType.Row,    width);
                childHeight = child.Layout.MeasuredHeight + child.GetMarginForAxis(FlexDirectionType.Column, width);
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
                    _node.Layout.GetMeasuredDimension(mainAxis.ToDimension())  -
                    child.Layout.GetMeasuredDimension(mainAxis.ToDimension()) -
                    _node.GetTrailingBorder(mainAxis)                          -
                    child.GetTrailingMargin(mainAxis, width)                  -
                    child.GetTrailingPosition(mainAxis, isMainAxisRow ? width : height);
            else if (
                !child.IsLeadingPositionDefined(mainAxis) &&
                _node.Style.JustifyContent == JustifyType.Center)
                child.Layout.Position[mainAxis.ToLeadingEdge()] =
                    (_node.Layout.GetMeasuredDimension(mainAxis.ToDimension()) -
                        child.Layout.GetMeasuredDimension(mainAxis.ToDimension())) / 2.0f;
            else if (
                !child.IsLeadingPositionDefined(mainAxis) &&
                _node.Style.JustifyContent == JustifyType.FlexEnd)
                child.Layout.Position[mainAxis.ToLeadingEdge()] =
                    _node.Layout.GetMeasuredDimension(mainAxis.ToDimension()) -
                    child.Layout.GetMeasuredDimension(mainAxis.ToDimension());

            if (child.IsTrailingPosDefined(crossAxis) &&
                !child.IsLeadingPositionDefined(crossAxis))
                child.Layout.Position[crossAxis.ToLeadingEdge()] =
                    _node.Layout.GetMeasuredDimension(crossAxis.ToDimension())  -
                    child.Layout.GetMeasuredDimension(crossAxis.ToDimension()) -
                    _node.GetTrailingBorder(crossAxis)                          -
                    child.GetTrailingMargin(crossAxis, width)                  -
                    child.GetTrailingPosition(crossAxis, isMainAxisRow ? height : width);
            else if (
                !child.IsLeadingPositionDefined(crossAxis) &&
                _node.AlignChild(child) == AlignType.Center)
                child.Layout.Position[crossAxis.ToLeadingEdge()] =
                    (_node.Layout.GetMeasuredDimension(crossAxis.ToDimension()) -
                        child.Layout.GetMeasuredDimension(crossAxis.ToDimension())) /
                    2.0f;

            else if (
                !child.IsLeadingPositionDefined(crossAxis) &&
                (_node.AlignChild(child) == AlignType.FlexEnd) ^
                (_node.Style.FlexWrap    == WrapType.WrapReverse))
                child.Layout.Position[crossAxis.ToLeadingEdge()] =
                    _node.Layout.GetMeasuredDimension(crossAxis.ToDimension()) -
                    child.Layout.GetMeasuredDimension(crossAxis.ToDimension());
        }

        internal float CalculateAvailableInnerDim(
            FlexDirectionType axis,
            float             availableDim,
            float             ownerDim)
        {
            var direction = axis.IsRow() ? FlexDirectionType.Row : FlexDirectionType.Column;
            var dimension = axis.IsRow() ? DimensionType.Width : DimensionType.Height;

            var margin           = _node.GetMarginForAxis(direction, ownerDim);
            var paddingAndBorder = _node.PaddingAndBorderForAxis(direction, ownerDim);

            var availableInnerDim = availableDim - margin - paddingAndBorder;
            // Max dimension overrides predefined dimension value; Min dimension in turn
            // overrides both of the above
            if (availableInnerDim.HasValue())
            {
                // We want to make sure our available height does not violate min and max
                // constraints
                var minDimensionOptional = _node.Style.MinDimension(dimension).ResolveValue(ownerDim);
                var minInnerDim = minDimensionOptional.IsNaN()
                    ? 0.0f
                    : minDimensionOptional - paddingAndBorder;

                var maxDimensionOptional = _node.Style.MaxDimension(dimension).ResolveValue(ownerDim);

                var maxInnerDim = maxDimensionOptional.IsNaN()
                    ? float.MaxValue
                    : maxDimensionOptional - paddingAndBorder;
                availableInnerDim = FloatMax(FloatMin(availableInnerDim, maxInnerDim), minInnerDim);
            }

            return availableInnerDim;
        }

        private void ComputeFlexBasisForChild(
            YogaNode      child,
            float         width,
            MeasureMode   widthMode,
            float         height,
            float         ownerWidth,
            float         ownerHeight,
            MeasureMode   heightMode,
            DirectionType direction,
            YogaConfig    config)
        {
            var mainAxis          = ResolveFlexDirection(_node.Style.FlexDirection, direction);
            var isMainAxisRow     = mainAxis.IsRow();
            var mainAxisSize      = isMainAxisRow ? width : height;
            var mainAxisOwnerSize = isMainAxisRow ? ownerWidth : ownerHeight;

            var resolvedFlexBasis       = child.ResolveFlexBasisPtr().ResolveValue(mainAxisOwnerSize);
            var isRowStyleDimDefined    = child.IsStyleDimensionDefined(FlexDirectionType.Row,    ownerWidth);
            var isColumnStyleDimDefined = child.IsStyleDimensionDefined(FlexDirectionType.Column, ownerHeight);

            if (resolvedFlexBasis.HasValue() && mainAxisSize.HasValue())
            {
                if (child.Layout.ComputedFlexBasis.IsNaN() ||
                    child.Config.ExperimentalFeatures.HasFlag(ExperimentalFeatures.WebFlexBasis) &&
                    child.Layout.ComputedFlexBasisGeneration != _currentGenerationCount)
                {
                    var paddingAndBorder = new float?(child.PaddingAndBorderForAxis(mainAxis, ownerWidth));
                    child.Layout.ComputedFlexBasis = FloatOptionalMax(resolvedFlexBasis, paddingAndBorder);
                }
            }
            else if (isMainAxisRow && isRowStyleDimDefined)
            {
                // The width is definite, so use that as the flex basis.
                var paddingAndBorder = new float?(
                    child.PaddingAndBorderForAxis(FlexDirectionType.Row, ownerWidth));

                child.Layout.ComputedFlexBasis = FloatOptionalMax(
                    child.ResolvedDimension.Width.ResolveValue(ownerWidth),
                    paddingAndBorder);
            }
            else if (!isMainAxisRow && isColumnStyleDimDefined)
            {
                // The height is definite, so use that as the flex basis.
                var paddingAndBorder = child.PaddingAndBorderForAxis(FlexDirectionType.Column, ownerWidth);
                child.Layout.ComputedFlexBasis = FloatOptionalMax(
                    child.ResolvedDimension.Height.ResolveValue(ownerHeight),
                    paddingAndBorder);
            }
            else
            {
                // Compute the flex basis and hypothetical main size (i.e. the clamped
                // flex basis).
                var childWidth = float.NaN;
                var childHeight = float.NaN;
                var childWidthMeasureMode = MeasureMode.Undefined;
                var childHeightMeasureMode = MeasureMode.Undefined;

                var marginRow    = child.GetMarginForAxis(FlexDirectionType.Row,    ownerWidth);
                var marginColumn = child.GetMarginForAxis(FlexDirectionType.Column, ownerWidth);

                if (isRowStyleDimDefined)
                {
                    childWidth            = child.ResolvedDimension.Width.ResolveValue(ownerWidth) + marginRow;
                    childWidthMeasureMode = MeasureMode.Exactly;
                }

                if (isColumnStyleDimDefined)
                {
                    childHeight            = child.ResolvedDimension.Height.ResolveValue(ownerHeight) + marginColumn;
                    childHeightMeasureMode = MeasureMode.Exactly;
                }

                // The W3C spec doesn't say anything about the 'overflow' property,
                // but all major browsers appear to implement the following logic.
                if (!isMainAxisRow && _node.Style.Overflow == OverflowType.Scroll ||
                    _node.Style.Overflow != OverflowType.Scroll)
                    if (childWidth.IsNaN() && width.HasValue())
                    {
                        childWidth            = width;
                        childWidthMeasureMode = MeasureMode.AtMost;
                    }

                if (isMainAxisRow && _node.Style.Overflow == OverflowType.Scroll ||
                    _node.Style.Overflow != OverflowType.Scroll)
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
                            ((childWidth - marginRow) / child.Style.AspectRatio.Value);
                        childHeightMeasureMode = MeasureMode.Exactly;
                    }
                    else if (
                        isMainAxisRow && childHeightMeasureMode == MeasureMode.Exactly)
                    {
                        childWidth = marginRow +
                            ((childHeight - marginColumn) *
                                child.Style.AspectRatio.Value);
                        childWidthMeasureMode = MeasureMode.Exactly;
                    }
                }

                // If child has no defined size in the cross axis and is set to stretch,
                // set the cross
                // axis to be measured exactly with the available inner width

                var hasExactWidth = width.HasValue() && widthMode == MeasureMode.Exactly;
                var childWidthStretch = _node.AlignChild(child) == AlignType.Stretch &&
                    childWidthMeasureMode                      != MeasureMode.Exactly;
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
                var childHeightStretch = _node.AlignChild(child) == AlignType.Stretch &&
                    childHeightMeasureMode                      != MeasureMode.Exactly;
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

                child.Calc.ConstrainMaxSizeForMode(
                    FlexDirectionType.Row,
                    ownerWidth,
                    ownerWidth,
                    ref childWidthMeasureMode,
                    ref childWidth);
                child.Calc.ConstrainMaxSizeForMode(
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

                child.Layout.ComputedFlexBasis = FloatMax(
                    child.Layout.GetMeasuredDimension(mainAxis.ToDimension()),
                    child.PaddingAndBorderForAxis(mainAxis, ownerWidth));
            }

            child.Layout.ComputedFlexBasisGeneration = _currentGenerationCount;
        }

        // For nodes with no children, use the available values if they were provided,
        // or the minimum size as indicated by the padding and border sizes.

        private void EmptyContainerSetMeasuredDimensions(
            float       availableWidth,
            float       availableHeight,
            MeasureMode widthMeasureMode,
            MeasureMode heightMeasureMode,
            float       ownerWidth,
            float       ownerHeight)
        {
            var paddingAndBorderAxisRow    = _node.PaddingAndBorderForAxis(FlexDirectionType.Row,    ownerWidth);
            var paddingAndBorderAxisColumn = _node.PaddingAndBorderForAxis(FlexDirectionType.Column, ownerWidth);
            var marginAxisRow              = _node.GetMarginForAxis(FlexDirectionType.Row,    ownerWidth);
            var marginAxisColumn           = _node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth);

            _node.Layout.SetMeasuredDimension(
                DimensionType.Width,
                _node.BoundAxis(
                    FlexDirectionType.Row,
                    widthMeasureMode == MeasureMode.Undefined ||
                    widthMeasureMode == MeasureMode.AtMost
                        ? paddingAndBorderAxisRow
                        : availableWidth - marginAxisRow,
                    ownerWidth,
                    ownerWidth)
            );

            _node.Layout.SetMeasuredDimension(
                DimensionType.Height,
                _node.BoundAxis(
                    FlexDirectionType.Column,
                    heightMeasureMode == MeasureMode.Undefined ||
                    heightMeasureMode == MeasureMode.AtMost
                        ? paddingAndBorderAxisColumn
                        : availableHeight - marginAxisColumn,
                    ownerHeight,
                    ownerWidth)
            );
        }

        private bool FixedSizeSetMeasuredDimensions(
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
                var marginAxisColumn = _node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth);
                var marginAxisRow    = _node.GetMarginForAxis(FlexDirectionType.Row,    ownerWidth);

                _node.Layout.SetMeasuredDimension(
                    DimensionType.Width,
                    _node.BoundAxis(
                        FlexDirectionType.Row,
                        availableWidth.IsNaN() ||
                        widthMeasureMode == MeasureMode.AtMost &&
                        availableWidth   < 0.0f
                            ? 0.0f
                            : availableWidth - marginAxisRow,
                        ownerWidth,
                        ownerWidth)
                );

                _node.Layout.SetMeasuredDimension(
                    DimensionType.Height,
                    _node.BoundAxis(
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
        //  * Display property is always assumed to be 'flex' except for Text nodes, which
        //    are assumed to be 'inline-flex'.
        //  * The 'zIndex' property (or any form of z ordering) is not supported. Nodes are
        //    stacked in document order.
        //  * The 'order' property is not supported. The order of flex items is always defined
        //    by document order.
        //  * The 'visibility' property is always assumed to be 'visible'. Values of 'collapse'
        //    and 'hidden' are not supported.
        //  * There is no support for forced breaks.
        //  * It does not support vertical inline directions (top-to-bottom or bottom-to-top text).
        //
        // Deviations from standard:
        //  * Section 4.5 of the spec indicates that all flex items have a default minimum
        //    main size. For text blocks, for example, this is the width of the widest word.
        //    Calculating the minimum width is expensive, so we forgo it and assume a default
        //    minimum main size of 0.
        //  * Min/Max sizes in the main axis are not honored when resolving flexible lengths.
        //  * The spec indicates that the default value for 'flexDirection' is 'row', but
        //    the algorithm below assumes a default of 'column'.
        //
        // Input parameters:
        //    - node: current node to be sized and laid out
        //    - availableWidth & availableHeight: available size to be used for sizing the node
        //      or float.NaN if the size is not available; interpretation depends on layout flags
        //    - ownerDirection: the inline (text) direction within the owner (left-to-right or right-to-left)
        //    - widthMeasureMode: indicates the sizing rules for the width (see below for explanation)
        //    - heightMeasureMode: indicates the sizing rules for the height (see below for explanation)
        //    - performLayout: specifies whether the caller is interested in just the dimensions
        //      of the node or it requires the entire node and its subtree to be laid out
        //      (with final positions)
        //
        // Details:
        //    This routine is called recursively to lay out subtrees of flexbox elements. It uses the
        //    information in node.style, which is treated as a read-only input. It is responsible for
        //    setting the layout.direction and layout.measuredDimensions fields for the input node as well
        //    as the layout.position and layout.lineIndex fields for its child nodes.
        //    The layout.measuredDimensions field includes any border or padding for the node but does
        //    not include margins.
        //
        //    The spec describes four different layout modes: "fill available", "max content", "min content",
        //    and "fit content". Of these, we don't use "min content" because we don't support default
        //    minimum main sizes (see above for details). Each of our measure modes maps
        //    to a layout mode from the spec (https://www.w3.org/TR/YG3-sizing/#terms):
        //      - MeasureMode.Undefined: max content
        //      - MeasureMode.Exactly: fill available
        //      - MeasureMode.AtMost: fit content
        //
        //    When calling LayoutImplementation and LayoutNodeInternal, if the caller
        //    passes an available size of undefined then it must also pass a measure
        //    mode of YGMeasureMode.Undefined in that dimension.
        //

        internal void LayoutImplementation(
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
            YogaGlobal.YogaAssert(
                _node,
                availableWidth.HasValue() || widthMeasureMode == MeasureMode.Undefined,
                "availableWidth is indefinite so widthMeasureMode must be YGMeasureMode.Undefined");
            YogaGlobal.YogaAssert(
                _node,
                availableHeight.HasValue() || heightMeasureMode == MeasureMode.Undefined,
                "availableHeight is indefinite so heightMeasureMode must be YGMeasureMode.Undefined");

            // Set the resolved resolution in the node's layout.
            var direction = _node.ResolveDirection(ownerDirection);
            _node.Layout.Direction = direction;

            var flexRowDirection    = ResolveFlexDirection(FlexDirectionType.Row,    direction);
            var flexColumnDirection = ResolveFlexDirection(FlexDirectionType.Column, direction);

            _node.Layout.Margin.Start  = _node.GetLeadingMargin(flexRowDirection, ownerWidth);
            _node.Layout.Margin.End    = _node.GetTrailingMargin(flexRowDirection, ownerWidth);
            _node.Layout.Margin.Top    = _node.GetLeadingMargin(flexColumnDirection, ownerWidth);
            _node.Layout.Margin.Bottom = _node.GetTrailingMargin(flexColumnDirection, ownerWidth);

            _node.Layout.Border.Start  = _node.GetLeadingBorder(flexRowDirection);
            _node.Layout.Border.End    = _node.GetTrailingBorder(flexRowDirection);
            _node.Layout.Border.Top    = _node.GetLeadingBorder(flexColumnDirection);
            _node.Layout.Border.Bottom = _node.GetTrailingBorder(flexColumnDirection);

            _node.Layout.Padding.Start  = _node.GetLeadingPadding(flexRowDirection, ownerWidth);
            _node.Layout.Padding.End    = _node.GetTrailingPadding(flexRowDirection, ownerWidth);
            _node.Layout.Padding.Top    = _node.GetLeadingPadding(flexColumnDirection, ownerWidth);
            _node.Layout.Padding.Bottom = _node.GetTrailingPadding(flexColumnDirection, ownerWidth);

            if (_node.MeasureFunc != null)
            {
                WithMeasureFuncSetMeasuredDimensions(
                    availableWidth,
                    availableHeight,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight);
                return;
            }

            var childCount = _node.Children.Count;
            if (childCount == 0)
            {
                _node.Calc.EmptyContainerSetMeasuredDimensions(
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
            if (!performLayout && _node.Calc.FixedSizeSetMeasuredDimensions(
                availableWidth,
                availableHeight,
                widthMeasureMode,
                heightMeasureMode,
                ownerWidth,
                ownerHeight))
                return;

            // Reset layout flags, as they could have changed.
            _node.Layout.HadOverflow = false;

            // STEP 1: CALCULATE VALUES FOR REMAINDER OF ALGORITHM
            var mainAxis       = ResolveFlexDirection(_node.Style.FlexDirection, direction);
            var crossAxis      = FlexDirectionCross(mainAxis, direction);
            var isMainAxisRow  = mainAxis.IsRow();
            var isNodeFlexWrap = _node.Style.FlexWrap != WrapType.NoWrap;

            var mainAxisOwnerSize  = isMainAxisRow ? ownerWidth : ownerHeight;
            var crossAxisOwnerSize = isMainAxisRow ? ownerHeight : ownerWidth;

            var leadingPaddingAndBorderCross = _node.GetLeadingPaddingAndBorder(crossAxis, ownerWidth);
            var paddingAndBorderAxisMain     = _node.PaddingAndBorderForAxis(mainAxis,  ownerWidth);
            var paddingAndBorderAxisCross    = _node.PaddingAndBorderForAxis(crossAxis, ownerWidth);

            var measureModeMainDim  = isMainAxisRow ? widthMeasureMode : heightMeasureMode;
            var measureModeCrossDim = isMainAxisRow ? heightMeasureMode : widthMeasureMode;

            var paddingAndBorderAxisRow    = isMainAxisRow ? paddingAndBorderAxisMain : paddingAndBorderAxisCross;
            var paddingAndBorderAxisColumn = isMainAxisRow ? paddingAndBorderAxisCross : paddingAndBorderAxisMain;

            var marginAxisRow    = _node.GetMarginForAxis(FlexDirectionType.Row,    ownerWidth);
            var marginAxisColumn = _node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth);

            var minInnerWidth  = _node.Style.MinWidth.ResolveValue(ownerWidth)   - paddingAndBorderAxisRow;
            var maxInnerWidth  = _node.Style.MaxWidth.ResolveValue(ownerWidth)   - paddingAndBorderAxisRow;
            var minInnerHeight = _node.Style.MinHeight.ResolveValue(ownerHeight) - paddingAndBorderAxisColumn;
            var maxInnerHeight = _node.Style.MaxHeight.ResolveValue(ownerHeight) - paddingAndBorderAxisColumn;

            var minInnerMainDim = isMainAxisRow ? minInnerWidth : minInnerHeight;
            var maxInnerMainDim = isMainAxisRow ? maxInnerWidth : maxInnerHeight;

            // STEP 2: DETERMINE AVAILABLE SIZE IN MAIN AND CROSS DIRECTIONS

            var availableInnerWidth = CalculateAvailableInnerDim(
                FlexDirectionType.Row,
                availableWidth,
                ownerWidth);
            var availableInnerHeight = CalculateAvailableInnerDim(
                FlexDirectionType.Column,
                availableHeight,
                ownerHeight);

            var availableInnerMainDim  = isMainAxisRow ? availableInnerWidth : availableInnerHeight;
            var availableInnerCrossDim = isMainAxisRow ? availableInnerHeight : availableInnerWidth;

            float totalOuterFlexBasis = 0;

            // STEP 3: DETERMINE FLEX BASIS FOR EACH ITEM

            _node.Calc.ComputeFlexBasisForChildren(
                availableInnerWidth,
                availableInnerHeight,
                widthMeasureMode,
                heightMeasureMode,
                direction,
                mainAxis,
                config,
                performLayout,
                ref totalOuterFlexBasis);

            var flexBasisOverflows = measureModeMainDim != MeasureMode.Undefined && totalOuterFlexBasis > availableInnerMainDim;
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
            float maxLineMainDim = 0;
            for (;
                endOfLineIndex < childCount;
                lineCount++, startOfLineIndex = endOfLineIndex)
            {
                var collectedFlexItemsValues = _node.Calc.CalculateCollectFlexItemsRowValues(
                    ownerDirection,
                    mainAxisOwnerSize,
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
                            collectedFlexItemsValues.TotalFlexGrowFactors == 0 || _node.ResolveFlexGrow().IsNaN() &&
                            _node.ResolveFlexGrow() == 0f)
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
                    collectedFlexItemsValues.ResolveFlexibleLength(
                        _node,
                        mainAxis,
                        crossAxis,
                        mainAxisOwnerSize,
                        availableInnerMainDim,
                        availableInnerCrossDim,
                        availableInnerWidth,
                        availableInnerHeight,
                        flexBasisOverflows,
                        measureModeCrossDim,
                        performLayout,
                        config);
                }

                _node.Layout.HadOverflow = _node.Layout.HadOverflow | (collectedFlexItemsValues.RemainingFreeSpace < 0);

                // STEP 6: MAIN-AXIS JUSTIFICATION & CROSS-AXIS SIZE DETERMINATION

                // At this point, all the children have their dimensions set in the main
                // axis.
                // Their dimensions are also set in the cross axis with the exception of
                // items
                // that are aligned "stretch". We need to compute these stretch values and
                // set the final positions.

                collectedFlexItemsValues.JustifyMainAxis(
                    _node,
                    startOfLineIndex,
                    mainAxis,
                    crossAxis,
                    measureModeMainDim,
                    measureModeCrossDim,
                    mainAxisOwnerSize,
                    ownerWidth,
                    availableInnerMainDim,
                    availableInnerCrossDim,
                    availableInnerWidth,
                    performLayout);

                var containerCrossAxis = availableInnerCrossDim;
                if (measureModeCrossDim == MeasureMode.Undefined ||
                    measureModeCrossDim == MeasureMode.AtMost)
                    containerCrossAxis = _node.BoundAxis(
                            crossAxis,
                            collectedFlexItemsValues.CrossDim + paddingAndBorderAxisCross,
                            crossAxisOwnerSize,
                            ownerWidth) -
                        paddingAndBorderAxisCross;

                // If there's no flex wrap, the cross dimension is defined by the container.
                if (!isNodeFlexWrap && measureModeCrossDim == MeasureMode.Exactly) collectedFlexItemsValues.CrossDim = availableInnerCrossDim;

                // Clamp to the min/max size specified on the container.
                collectedFlexItemsValues.CrossDim = _node.BoundAxis(
                        crossAxis,
                        collectedFlexItemsValues.CrossDim + paddingAndBorderAxisCross,
                        crossAxisOwnerSize,
                        ownerWidth) -
                    paddingAndBorderAxisCross;

                // STEP 7: CROSS-AXIS ALIGNMENT
                // We can skip child alignment if we're just measuring the container.
                if (performLayout)
                    for (var i = startOfLineIndex; i < endOfLineIndex; i++)
                    {
                        var child = _node.Children[i];
                        if (child.Style.Display == DisplayType.None) continue;

                        if (child.Style.PositionType == PositionType.Absolute)
                        {
                            // If the child is absolutely positioned and has a
                            // top/left/bottom/right set, override
                            // all the previously computed positions to set it correctly.
                            var isChildLeadingPosDefined = child.IsLeadingPositionDefined(crossAxis);
                            if (isChildLeadingPosDefined)
                                child.Layout.Position[crossAxis.ToPositionEdge()] =
                                    child.GetLeadingPosition(crossAxis, availableInnerCrossDim)
                                    + _node.GetLeadingBorder(crossAxis)
                                    + child.GetLeadingMargin(crossAxis, availableInnerWidth);

                            // If leading position is not defined or calculations result in Nan,
                            // default to border + margin
                            if (!isChildLeadingPosDefined ||
                                child.Layout.Position[crossAxis.ToPositionEdge()].IsNaN())
                                child.Layout.Position[crossAxis.ToPositionEdge()] =
                                    _node.GetLeadingBorder(crossAxis)
                                    + child.GetLeadingMargin(crossAxis, availableInnerWidth);
                        }
                        else
                        {
                            var leadingCrossDim = leadingPaddingAndBorderCross;

                            // For a relative children, we're either using alignItems (owner) or
                            // alignSelf (child) in order to determine the position in the cross
                            // axis
                            var alignItem = _node.AlignChild(child);

                            // If the child uses align stretch, we need to lay it out one more
                            // time, this time
                            // forcing the cross-axis size to be the computed cross size for the
                            // current line.
                            if (alignItem                                 == AlignType.Stretch &&
                                child.MarginLeadingValue(crossAxis).Unit  != ValueUnit.Auto    &&
                                child.MarginTrailingValue(crossAxis).Unit != ValueUnit.Auto)
                            {
                                // If the child defines a definite size for its cross axis, there's
                                // no need to stretch.
                                if (!child.IsStyleDimensionDefined(crossAxis, availableInnerCrossDim))
                                {
                                    var childMainSize = child.Layout.GetMeasuredDimension(mainAxis.ToDimension());
                                    var childCrossSize =
                                        child.Style.AspectRatio.HasValue
                                            ? child.GetMarginForAxis(crossAxis, availableInnerWidth) +
                                            (isMainAxisRow
                                                ? childMainSize / child.Style.AspectRatio.Value
                                                : childMainSize * child.Style.AspectRatio.Value)
                                            : collectedFlexItemsValues.CrossDim;

                                    childMainSize += child.GetMarginForAxis(mainAxis, availableInnerWidth);

                                    var childMainMeasureMode  = MeasureMode.Exactly;
                                    var childCrossMeasureMode = MeasureMode.Exactly;
                                    child.Calc.ConstrainMaxSizeForMode(
                                        mainAxis,
                                        availableInnerMainDim,
                                        availableInnerWidth,
                                        ref childMainMeasureMode,
                                        ref childMainSize);
                                    child.Calc.ConstrainMaxSizeForMode(
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
                                var remainingCrossDim = containerCrossAxis - child.DimensionWithMargin(crossAxis, availableInnerWidth);

                                if (child.MarginLeadingValue(crossAxis).Unit  == ValueUnit.Auto &&
                                    child.MarginTrailingValue(crossAxis).Unit == ValueUnit.Auto)
                                {
                                    leadingCrossDim += FloatMax(0.0f, remainingCrossDim / 2);
                                }
                                else if (
                                    child.MarginTrailingValue(crossAxis).Unit == ValueUnit.Auto)
                                {
                                    // No-Op
                                }
                                else if (
                                    child.MarginLeadingValue(crossAxis).Unit == ValueUnit.Auto)
                                {
                                    leadingCrossDim += FloatMax(0.0f, remainingCrossDim);
                                }
                                else if (alignItem == AlignType.FlexStart)
                                {
                                    // No-Op
                                }
                                else if (alignItem == AlignType.Center)
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
                maxLineMainDim    =  FloatMax(maxLineMainDim, collectedFlexItemsValues.MainDim);
            }

            // STEP 8: MULTI-LINE CONTENT ALIGNMENT
            // currentLead stores the size of the cross dim
            if (performLayout && (lineCount > 1 || _node.IsBaselineLayout()))
            {
                float crossDimLead = 0;
                var   currentLead  = leadingPaddingAndBorderCross;
                if (availableInnerCrossDim.HasValue())
                {
                    var remainingAlignContentDim = availableInnerCrossDim - totalLineCrossDim;
                    switch (_node.Style.AlignContent)
                    {
                    case AlignType.FlexEnd:
                        currentLead += remainingAlignContentDim;
                        break;
                    case AlignType.Center:
                        currentLead += remainingAlignContentDim / 2;
                        break;
                    case AlignType.Stretch:
                        if (availableInnerCrossDim > totalLineCrossDim) crossDimLead = remainingAlignContentDim / lineCount;

                        break;
                    case AlignType.SpaceAround:
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
                    case AlignType.SpaceBetween:
                        if (availableInnerCrossDim > totalLineCrossDim && lineCount > 1) crossDimLead = remainingAlignContentDim / (lineCount - 1);

                        break;
                    case AlignType.Auto:
                    case AlignType.FlexStart:
                    case AlignType.Baseline:
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
                        var child = _node.Children[ii];
                        if (child.Style.Display == DisplayType.None)
                            continue;

                        if (child.Style.PositionType == PositionType.Relative)
                        {
                            if (child.LineIndex != i)
                                break;

                            if (child.IsLayoutDimensionDefined(crossAxis))
                                lineHeight = FloatMax(
                                    lineHeight,
                                    child.Layout.GetMeasuredDimension(crossAxis.ToDimension())
                                    + child.GetMarginForAxis(crossAxis, availableInnerWidth));

                            if (_node.AlignChild(child) == AlignType.Baseline)
                            {
                                var ascent = child.Baseline()
                                    + child.GetLeadingMargin(FlexDirectionType.Column, availableInnerWidth);
                                var descent = (child.Layout.MeasuredHeight
                                        + child.GetMarginForAxis(FlexDirectionType.Column, availableInnerWidth))
                                    - ascent;
                                maxAscentForCurrentLine  = FloatMax(maxAscentForCurrentLine,  ascent);
                                maxDescentForCurrentLine = FloatMax(maxDescentForCurrentLine, descent);
                                lineHeight               = FloatMax(lineHeight,               maxAscentForCurrentLine + maxDescentForCurrentLine);
                            }
                        }
                    }

                    endIndex   =  ii;
                    lineHeight += crossDimLead;

                    if (performLayout)
                        for (ii = startIndex; ii < endIndex; ii++)
                        {
                            var child = _node.Children[ii];
                            if (child.Style.Display == DisplayType.None) continue;

                            if (child.Style.PositionType == PositionType.Relative)
                                switch (_node.AlignChild(child))
                                {
                                case AlignType.FlexStart:
                                {
                                    child.Layout.Position[crossAxis.ToPositionEdge()] =
                                        currentLead + child.GetLeadingMargin(crossAxis, availableInnerWidth);
                                    break;
                                }
                                case AlignType.FlexEnd:
                                {
                                    child.Layout.Position[crossAxis.ToPositionEdge()] =
                                        (currentLead
                                            + lineHeight)
                                        - child.GetTrailingMargin(crossAxis, availableInnerWidth)
                                        - child.Layout.GetMeasuredDimension(crossAxis.ToDimension());
                                    break;
                                }
                                case AlignType.Center:
                                {
                                    var childHeight =
                                        child.Layout.GetMeasuredDimension(crossAxis.ToDimension());

                                    child.Layout.Position[crossAxis.ToPositionEdge()] =
                                        currentLead
                                        + ((lineHeight - childHeight) / 2);
                                    break;
                                }
                                case AlignType.Stretch:
                                {
                                    child.Layout.Position[crossAxis.ToPositionEdge()] =
                                        currentLead + child.GetLeadingMargin(crossAxis, availableInnerWidth);

                                    // Remeasure child with the line height as it as been only
                                    // measured with the owners height yet.
                                    if (!child.IsStyleDimensionDefined(crossAxis, availableInnerCrossDim))
                                    {
                                        var childWidth = isMainAxisRow
                                            ? child.Layout.MeasuredWidth + child.GetMarginForAxis(mainAxis, availableInnerWidth)
                                            : lineHeight;

                                        var childHeight = !isMainAxisRow
                                            ? child.Layout.MeasuredHeight + child.GetMarginForAxis(crossAxis, availableInnerWidth)
                                            : lineHeight;

                                        if (!(FloatEqual(childWidth, child.Layout.MeasuredWidth) &&
                                            FloatEqual(childHeight,  child.Layout.MeasuredHeight)))
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
                                case AlignType.Baseline:
                                {
                                    child.Layout.Position[EdgeType.Top] =
                                        ((currentLead
                                                + maxAscentForCurrentLine)
                                            - child.Baseline())
                                        + child.GetLeadingPosition(FlexDirectionType.Column, availableInnerCrossDim);

                                    break;
                                }
                                case AlignType.Auto:
                                case AlignType.SpaceBetween:
                                case AlignType.SpaceAround:
                                    break;
                                }
                        }

                    currentLead += lineHeight;
                }
            }

            // STEP 9: COMPUTING FINAL DIMENSIONS

            _node.Layout.SetMeasuredDimension(
                DimensionType.Width,
                _node.BoundAxis(
                    FlexDirectionType.Row,
                    availableWidth - marginAxisRow,
                    ownerWidth,
                    ownerWidth)
            );

            _node.Layout.SetMeasuredDimension(
                DimensionType.Height,
                _node.BoundAxis(
                    FlexDirectionType.Column,
                    availableHeight - marginAxisColumn,
                    ownerHeight,
                    ownerWidth)
            );

            // If the user didn't specify a width or height for the node, set the
            // dimensions based on the children.
            if (measureModeMainDim == MeasureMode.Undefined ||
                _node.Style.Overflow != OverflowType.Scroll &&
                measureModeMainDim  == MeasureMode.AtMost)
                _node.Layout.SetMeasuredDimension(
                    mainAxis.ToDimension(),
                    _node.BoundAxis(
                        mainAxis,
                        maxLineMainDim,
                        mainAxisOwnerSize,
                        ownerWidth)
                );
            else if (
                measureModeMainDim  == MeasureMode.AtMost &&
                _node.Style.Overflow == OverflowType.Scroll)
                _node.Layout.SetMeasuredDimension(
                    mainAxis.ToDimension(),
                    FloatMax(
                        FloatMin(
                            availableInnerMainDim + paddingAndBorderAxisMain,
                            _node.BoundAxisWithinMinAndMax(mainAxis, maxLineMainDim, mainAxisOwnerSize)),
                        paddingAndBorderAxisMain)
                );

            if (measureModeCrossDim == MeasureMode.Undefined ||
                _node.Style.Overflow != OverflowType.Scroll &&
                measureModeCrossDim == MeasureMode.AtMost)
            {
                _node.Layout.SetMeasuredDimension(
                    crossAxis.ToDimension(),
                    _node.BoundAxis(
                        crossAxis,
                        totalLineCrossDim + paddingAndBorderAxisCross,
                        crossAxisOwnerSize,
                        ownerWidth)
                );
            }
            else if (
                measureModeCrossDim == MeasureMode.AtMost &&
                _node.Style.Overflow == OverflowType.Scroll)
            {
                _node.Layout.SetMeasuredDimension(
                    crossAxis.ToDimension(),
                    FloatMax(
                        FloatMin(
                            availableInnerCrossDim + paddingAndBorderAxisCross,
                            _node.BoundAxisWithinMinAndMax(
                                crossAxis,
                                totalLineCrossDim + paddingAndBorderAxisCross,
                                crossAxisOwnerSize)),
                        paddingAndBorderAxisCross)
                );
            }

            // As we only wrapped in normal direction yet, we need to reverse the
            // positions on wrap-reverse.
            if (performLayout && _node.Style.FlexWrap == WrapType.WrapReverse)
                for (var i = 0; i < childCount; i++)
                {
                    var child = _node.Children[i];
                    if (child.Style.PositionType == PositionType.Relative)
                        child.Layout.Position[crossAxis.ToPositionEdge()] =
                            _node.Layout.GetMeasuredDimension(crossAxis.ToDimension()) -
                            child.Layout.Position[crossAxis.ToPositionEdge()]         -
                            child.Layout.GetMeasuredDimension(crossAxis.ToDimension());
                }

            if (performLayout)
            {
                // STEP 10: SIZING AND POSITIONING ABSOLUTE CHILDREN
                foreach (var child in _node.Children)
                {
                    if (child.Style.PositionType != PositionType.Absolute) continue;

                    _node.Calc.AbsoluteLayoutChild(
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
                        var child = _node.Children[i];
                        if (child.Style.Display == DisplayType.None) continue;

                        if (needsMainTrailingPos)
                            SetChildTrailingPosition(child, mainAxis);

                        if (needsCrossTrailingPos)
                            SetChildTrailingPosition(child, crossAxis);
                    }
            }
        }

        private void SetChildTrailingPosition(YogaNode child, FlexDirectionType axis)
        {
            var size = child.Layout.GetMeasuredDimension(axis.ToDimension());

            child.Layout.Position[axis.ToTrailingEdge()] =
                _node.Layout.GetMeasuredDimension(axis.ToDimension())
                - size
                - child.Layout.Position[axis.ToPositionEdge()];
        }

        private void WithMeasureFuncSetMeasuredDimensions(
            float       availableWidth,
            float       availableHeight,
            MeasureMode widthMeasureMode,
            MeasureMode heightMeasureMode,
            float       ownerWidth,
            float       ownerHeight)
        {
            YogaGlobal.YogaAssert(
                _node,
                _node.MeasureFunc != null,
                "Expected node to have custom measure function");

            var paddingAndBorderAxisRow    = _node.PaddingAndBorderForAxis(FlexDirectionType.Row,    availableWidth);
            var paddingAndBorderAxisColumn = _node.PaddingAndBorderForAxis(FlexDirectionType.Column, availableWidth);
            var marginAxisRow              = _node.GetMarginForAxis(FlexDirectionType.Row,    availableWidth);
            var marginAxisColumn           = _node.GetMarginForAxis(FlexDirectionType.Column, availableWidth);

            // We want to make sure we don't call measure with negative size
            var innerWidth = availableWidth.IsNaN()
                ? availableWidth
                : FloatMax(0, availableWidth - marginAxisRow - paddingAndBorderAxisRow);
            var innerHeight = availableHeight.IsNaN()
                ? availableHeight
                : FloatMax(0, availableHeight - marginAxisColumn - paddingAndBorderAxisColumn);

            if (widthMeasureMode  == MeasureMode.Exactly &&
                heightMeasureMode == MeasureMode.Exactly)
            {
                // Don't bother sizing the text if both dimensions are already defined.
                _node.Layout.SetMeasuredDimension(
                    DimensionType.Width,
                    _node.BoundAxis(
                        FlexDirectionType.Row,
                        availableWidth - marginAxisRow,
                        ownerWidth,
                        ownerWidth)
                );
                _node.Layout.SetMeasuredDimension(
                    DimensionType.Height,
                    _node.BoundAxis(
                        FlexDirectionType.Column,
                        availableHeight - marginAxisColumn,
                        ownerHeight,
                        ownerWidth)
                );
            }
            else
            {
                // Measure the text under the current constraints.
                var measuredSize = _node.MeasureFunc(_node, innerWidth, widthMeasureMode, innerHeight, heightMeasureMode);

                _node.Layout.SetMeasuredDimension(
                    DimensionType.Width,
                    _node.BoundAxis(
                        FlexDirectionType.Row,
                        widthMeasureMode == MeasureMode.Undefined ||
                        widthMeasureMode == MeasureMode.AtMost
                            ? measuredSize.Width + paddingAndBorderAxisRow
                            : availableWidth     - marginAxisRow,
                        ownerWidth,
                        ownerWidth)
                );

                _node.Layout.SetMeasuredDimension(
                    DimensionType.Height,
                    _node.BoundAxis(
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

        private void RoundToPixelGrid(
            float pointScaleFactor,
            float absoluteLeft,
            float absoluteTop)
        {
            if (FloatEqual(pointScaleFactor,0.0f))
                return;

            var nodeLeft = _node.Layout.Position.Left;
            var nodeTop  = _node.Layout.Position.Top;

            var nodeWidth  = _node.Layout.Width;
            var nodeHeight = _node.Layout.Height;

            var absoluteNodeLeft = absoluteLeft + nodeLeft;
            var absoluteNodeTop  = absoluteTop  + nodeTop;

            var absoluteNodeRight  = absoluteNodeLeft + nodeWidth;
            var absoluteNodeBottom = absoluteNodeTop  + nodeHeight;

            // If a node has a custom measure function we never want to round down its
            // size as this could lead to unwanted text truncation.
            var textRounding = _node.NodeType == NodeType.Text;

            _node.Layout.Position.Left = RoundValueToPixelGrid(nodeLeft, pointScaleFactor, false, textRounding);
            _node.Layout.Position.Top  = RoundValueToPixelGrid(nodeTop,  pointScaleFactor, false, textRounding);

            // We multiply dimension by scale factor and if the result is close to the
            // whole number, we don't have any fraction To verify if the result is close
            // to whole number we want to check both floor and ceil numbers
            var hasFractionalWidth =
                !FloatEqual((nodeWidth * pointScaleFactor) % 1.0f, 0) &&
                !FloatEqual((nodeWidth * pointScaleFactor) % 1.0f, 1.0f);
            var hasFractionalHeight =
                !FloatEqual((nodeHeight * pointScaleFactor) % 1.0f, 0f) &&
                !FloatEqual((nodeHeight * pointScaleFactor) % 1.0f, 1f);

            _node.Layout.SetDimension(
                DimensionType.Width,
                RoundValueToPixelGrid(
                    absoluteNodeRight,
                    pointScaleFactor,
                    textRounding && hasFractionalWidth,
                    textRounding && !hasFractionalWidth) - RoundValueToPixelGrid(
                    absoluteNodeLeft,
                    pointScaleFactor,
                    false,
                    textRounding)
            );

            _node.Layout.SetDimension(
                DimensionType.Height,
                RoundValueToPixelGrid(
                    absoluteNodeBottom,
                    pointScaleFactor,
                    textRounding && hasFractionalHeight,
                    textRounding && !hasFractionalHeight) - RoundValueToPixelGrid(
                    absoluteNodeTop,
                    pointScaleFactor,
                    false,
                    textRounding)
            );

            foreach (var child in _node.Children)
                child.Calc.RoundToPixelGrid(
                    pointScaleFactor,
                    absoluteNodeLeft,
                    absoluteNodeTop);
        }

        // inline
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool MeasureModeNewMeasureSizeIsStricterAndStillValid(
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
                (lastComputedSize <= size || FloatEqual(size, lastComputedSize));
        }

        // inline
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool MeasureModeOldSizeIsUnspecifiedAndStillFits(
            MeasureMode sizeMode,
            float       size,
            MeasureMode lastSizeMode,
            float       lastComputedSize)
        {
            return sizeMode  == MeasureMode.AtMost    &&
                lastSizeMode == MeasureMode.Undefined &&
                (size >= lastComputedSize || FloatEqual(size, lastComputedSize));
        }

        // inline
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool MeasureModeSizeIsExactAndMatchesOldMeasuredSize(
            MeasureMode sizeMode,
            float       size,
            float       lastComputedSize)
        {
            return sizeMode == MeasureMode.Exactly && FloatEqual(size, lastComputedSize);
        }

        private void ComputeFlexBasisForChildren(
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
            YogaNode singleFlexChild    = null;
            var      children           = _node.Children;
            var      measureModeMainDim = mainAxis.IsRow() ? widthMeasureMode : heightMeasureMode;
            // If there is only one child with flexGrow + flexShrink it means we can set
            // the computedFlexBasis to 0 instead of measuring and shrinking / flexing the
            // child to exactly match the remaining space
            if (measureModeMainDim == MeasureMode.Exactly)
                foreach (var child in children)
                    if (child.IsNodeFlexible())
                    {
                        if (singleFlexChild != null || FloatEqual(child.ResolveFlexGrow(), 0.0f) || FloatEqual(child.ResolveFlexShrink(), 0.0f))
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
                    child.ZeroOutLayoutRecursively();
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
                    child.Layout.ComputedFlexBasisGeneration = _currentGenerationCount;
                    child.Layout.ComputedFlexBasis           = 0f;
                }
                else
                {
                    _node.Calc.ComputeFlexBasisForChild(
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

                totalOuterFlexBasis +=
                    child.Layout.ComputedFlexBasis.Unwrap() +
                    child.GetMarginForAxis(mainAxis, availableInnerWidth);
            }
        }
    }
}
