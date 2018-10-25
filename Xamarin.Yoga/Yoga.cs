using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming
#pragma warning disable 414
#pragma warning disable 169

namespace Xamarin.Yoga
{
    public static partial class YGGlobal
    {
        public static int gCurrentGenerationCount;

        internal static int  gDepth;
        internal static bool gPrintChanges = false;
        internal static bool gPrintSkips   = false;

        internal static string spacer = "                                                            ";

        public static void YGAssert(bool condition, string message)
        {
            if (!condition) YGLog(null, LogLevel.Fatal, $"{message}\n");
        }

        public static void YGAssertWithConfig(
            YogaConfig config,
            bool       condition,
            string     message)
        {
            if (!condition)
                YGLogWithConfig(config, LogLevel.Fatal, "{message}\n");
        }

        public static void YGAssertWithNode(
            YGNode node,
            bool   condition,
            string message)
        {
            if (!condition) YGLog(node, LogLevel.Fatal, $"{message}\n");
        }

        public static void YGConfigSetPointScaleFactor(
            YogaConfig config,
            float      pixelsInPoint)
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

            gDepth++;

            var needToVisitNode =
                node.IsDirty && layout.GenerationCount != gCurrentGenerationCount ||
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
                var marginAxisRow = UnwrapFloatOptional(
                    node.GetMarginForAxis(FlexDirectionType.Row, ownerWidth));
                var marginAxisColumn = UnwrapFloatOptional(
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

                if (gPrintChanges && gPrintSkips)
                {
                    YGLog(
                        node,
                        LogLevel.Verbose,
                        $"{YGSpacer(gDepth)}{gDepth}.[[skipped] ");

                    node.PrintFunc?.Invoke(node);

                    YGLog(
                        node,
                        LogLevel.Verbose,
                        $"wm: {YGMeasureModeName(widthMeasureMode, performLayout)}, hm: {YGMeasureModeName(heightMeasureMode, performLayout)}, aw: {availableWidth} ah: {availableHeight} => d: ({cachedResults.ComputedWidth}, {cachedResults.ComputedHeight}) {reason}\n");
                }
            }
            else
            {
                if (gPrintChanges)
                {
                    YGLog(
                        node,
                        LogLevel.Verbose,
                        $"{YGSpacer(gDepth)}{gDepth}.{(needToVisitNode ? " * " : "")}");

                    node.PrintFunc?.Invoke(node);

                    YGLog(
                        node,
                        LogLevel.Verbose,
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
                        LogLevel.Verbose,
                        $"{YGSpacer(gDepth)}{gDepth}.]{(needToVisitNode ? "*" : "")}");

                    node.PrintFunc?.Invoke(node);

                    YGLog(
                        node,
                        LogLevel.Verbose,
                        $"wm: {YGMeasureModeName(widthMeasureMode, performLayout)}, hm: {YGMeasureModeName(heightMeasureMode, performLayout)}, d: ({layout.MeasuredWidth}, {layout.MeasuredHeight}) {reason}\n");
                }

                layout.LastOwnerDirection = ownerDirection;

                if (cachedResults == null)
                {
                    if (layout.CachedMeasurementFull)
                    {
                        if (gPrintChanges)
                            YGLog(node, LogLevel.Verbose, "Out of cache entries!\n");

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

            gDepth--;
            layout.GenerationCount = gCurrentGenerationCount;
            return needToVisitNode || cachedResults == null;
        }

        public static void YGLog(YGNode node, LogLevel level, string message)
        {
            YGVLog(
                node == null ? null : node.Config,
                node,
                level,
                message);
        }

        public static void YGLogWithConfig(YogaConfig config, LogLevel level, string message)
        {
            YGVLog(config, null, level, message);
        }

        public static void YGNodeCalculateLayout(
            YGNode        node,
            float         ownerWidth,
            float         ownerHeight,
            DirectionType ownerDirection)
        {
            // Increment the generation count. This will force the recursive routine to
            // visit
            // all dirty nodes at least once. Subsequent visits will be skipped if the
            // input
            // parameters don't change.
            gCurrentGenerationCount++;
            node.ResolveDimension();
            var width            = float.NaN;
            var widthMeasureMode = MeasureMode.Undefined;
            if (YGNodeIsStyleDimDefined(node, FlexDirectionType.Row, ownerWidth))
            {
                width = UnwrapFloatOptional(
                    ResolveValue(
                        node.ResolvedDimension[FlexDirectionType.Row.ToDimension()],
                        ownerWidth) +
                    node.GetMarginForAxis(FlexDirectionType.Row, ownerWidth));
                widthMeasureMode = MeasureMode.Exactly;
            }
            else if (!ResolveValue(node.Style.MaxWidth, ownerWidth).IsNaN())
            {
                width            = UnwrapFloatOptional(ResolveValue(node.Style.MaxWidth, ownerWidth));
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
            if (YGNodeIsStyleDimDefined(node, FlexDirectionType.Column, ownerHeight))
            {
                height = UnwrapFloatOptional(
                    ResolveValue(
                        node.ResolvedDimension[FlexDirectionType.Column.ToDimension()],
                        ownerHeight) +
                    node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth));
                heightMeasureMode = MeasureMode.Exactly;
            }
            else if (!ResolveValue(node.Style.MaxHeight, ownerHeight).IsNaN())
            {
                height            = UnwrapFloatOptional(ResolveValue(node.Style.MaxHeight, ownerHeight));
                heightMeasureMode = MeasureMode.AtMost;
            }
            else
            {
                height = ownerHeight;
                heightMeasureMode = height.IsNaN()
                    ? MeasureMode.Undefined
                    : MeasureMode.Exactly;
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
                node.SetPosition(
                    node.Layout.Direction,
                    ownerWidth,
                    ownerHeight,
                    ownerWidth);
                YGRoundToPixelGrid(node, node.Config.pointScaleFactor, 0.0f, 0.0f);

                if (node.Config.printTree)
                    node.Print(PrintOptionType.All);
            }
        }

        public static bool YGNodeCanUseCachedMeasurement(
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

        // YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(float, Border,  border);
        public static float YGNodeLayoutGetBorder(YGNode node, EdgeType edge)
        {
            YGAssertWithNode(
                node,
                edge <= EdgeType.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case EdgeType.Left when node.Layout.Direction == DirectionType.RTL:
                return node.Layout.Border.End;
            case EdgeType.Left:
                return node.Layout.Border.Start;
            case EdgeType.Right when node.Layout.Direction == DirectionType.RTL:
                return node.Layout.Border.Start;
            case EdgeType.Right:
                return node.Layout.Border.End;
            }

            return node.Layout.Border[edge];
        }

        // YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(float, Padding, padding);
        public static float YGNodeLayoutGetPadding(YGNode node, EdgeType edge)
        {
            YGAssertWithNode(
                node,
                edge <= EdgeType.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case EdgeType.Left when node.Layout.Direction == DirectionType.RTL:
                return node.Layout.Padding.End;
            case EdgeType.Left:
                return node.Layout.Padding.Start;
            case EdgeType.Right when node.Layout.Direction == DirectionType.RTL:
                return node.Layout.Padding.Start;
            case EdgeType.Right:
                return node.Layout.Padding.End;
            }

            return node.Layout.Padding[edge];
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

        public static void YGTraversePreOrder(
            YGNode         node,
            Action<YGNode> f)
        {
            if (node == null)
                return;

            f(node);
            YGTraverseChildrenPreOrder(node.Children, f);
        }

        internal static float YGBaseline(YGNode node)
        {
            if (node.BaselineFunc != null)
            {
                var baseline = node.BaselineFunc(
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
                if (child.LineIndex > 0) break;

                if (child.Style.PositionType == PositionType.Absolute) continue;

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

        // This function assumes that all the children of node have their
        // computedFlexBasis properly computed(To do this use
        // YGNodeComputeFlexBasisForChildren function).
        // This function calculates YGCollectFlexItemsRowMeasurement
        internal static CollectFlexItemsRowValues YGCalculateCollectFlexItemsRowValues(
            YGNode        node,
            DirectionType ownerDirection,
            float         mainAxisownerSize,
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
            var   mainAxis                                        = ResolveFlexDirection(node.Style.FlexDirection, node.ResolveDirection(ownerDirection));
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
                var childMarginMainAxis = UnwrapFloatOptional(child.GetMarginForAxis(mainAxis, availableInnerWidth));
                var flexBasisWithMinAndMaxConstraints = UnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        child,
                        mainAxis,
                        UnwrapFloatOptional(child.Layout.ComputedFlexBasis),
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

                if (child.IsNodeFlexible())
                {
                    flexAlgoRowMeasurement.TotalFlexGrowFactors += child.ResolveFlexGrow();

                    // Unlike the grow factor, the shrink factor is scaled relative to the
                    // child dimension.
                    flexAlgoRowMeasurement.TotalFlexShrinkScaledFactors +=
                        -child.ResolveFlexShrink() *
                        UnwrapFloatOptional(child.Layout.ComputedFlexBasis);
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

        internal static void YGConstrainMaxSizeForMode(
            YGNode            node,
            FlexDirectionType axis,
            float             ownerAxisSize,
            float             ownerWidth,
            ref MeasureMode   mode,
            ref float         size)
        {
            var maxSize =
                ResolveValue(node.Style.MaxDimension(axis.ToDimension()), ownerAxisSize) +
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

        // It distributes the free space to the flexible items.For those flexible items
        // whose min and max constraints are triggered, those flex item's clamped size
        // is removed from the remaingfreespace.
        internal static void YGDistributeFreeSpaceFirstPass(
            CollectFlexItemsRowValues collectedFlexItemsValues,
            FlexDirectionType         mainAxis,
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
                var childFlexBasis = UnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        currentRelativeChild,
                        mainAxis,
                        UnwrapFloatOptional(
                            currentRelativeChild.Layout.ComputedFlexBasis),
                        mainAxisownerSize));

                if (collectedFlexItemsValues.RemainingFreeSpace < 0)
                {
                    flexShrinkScaledFactor =
                        -currentRelativeChild.ResolveFlexShrink() * childFlexBasis;

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
                    flexGrowFactor = currentRelativeChild.ResolveFlexGrow();

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

        // It distributes the free space to the flexible items and ensures that the size
        // of the flex items abide the min and max constraints. At the end of this
        // function the child nodes would have proper size. Prior using this function
        // please ensure that YGDistributeFreeSpaceFirstPass is called.
        internal static float YGDistributeFreeSpaceSecondPass(
            CollectFlexItemsRowValues collectedFlexItemsValues,
            YGNode                    node,
            FlexDirectionType         mainAxis,
            FlexDirectionType         crossAxis,
            float                     mainAxisownerSize,
            float                     availableInnerMainDim,
            float                     availableInnerCrossDim,
            float                     availableInnerWidth,
            float                     availableInnerHeight,
            bool                      flexBasisOverflows,
            MeasureMode               measureModeCrossDim,
            bool                      performLayout,
            YogaConfig                config)
        {
            float childFlexBasis         = 0;
            float flexShrinkScaledFactor = 0;
            float flexGrowFactor         = 0;
            float deltaFreeSpace         = 0;
            var   isMainAxisRow          = mainAxis.IsRow();
            var   isNodeFlexWrap         = node.Style.FlexWrap != WrapType.NoWrap;

            foreach (var currentRelativeChild in collectedFlexItemsValues.RelativeChildren)
            {
                childFlexBasis = UnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        currentRelativeChild,
                        mainAxis,
                        UnwrapFloatOptional(
                            currentRelativeChild.Layout.ComputedFlexBasis),
                        mainAxisownerSize));
                var updatedMainSize = childFlexBasis;

                if (collectedFlexItemsValues.RemainingFreeSpace.HasValue() &&
                    collectedFlexItemsValues.RemainingFreeSpace < 0)
                {
                    flexShrinkScaledFactor =
                        -currentRelativeChild.ResolveFlexShrink() * childFlexBasis;
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
                    flexGrowFactor = currentRelativeChild.ResolveFlexGrow();

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

                var marginMain  = UnwrapFloatOptional(currentRelativeChild.GetMarginForAxis(mainAxis,  availableInnerWidth));
                var marginCross = UnwrapFloatOptional(currentRelativeChild.GetMarginForAxis(crossAxis, availableInnerWidth));

                float       childCrossSize;
                var         childMainSize = updatedMainSize + marginMain;
                MeasureMode childCrossMeasureMode;
                var         childMainMeasureMode = MeasureMode.Exactly;

                if (currentRelativeChild.Style.AspectRatio.HasValue)
                {
                    childCrossSize = isMainAxisRow
                        ? (childMainSize - marginMain) / currentRelativeChild.Style.AspectRatio.Value
                        : (childMainSize - marginMain) * currentRelativeChild.Style.AspectRatio.Value;
                    childCrossMeasureMode = MeasureMode.Exactly;

                    childCrossSize += marginCross;
                }
                else if (
                    availableInnerCrossDim.HasValue()                                                 &&
                    !YGNodeIsStyleDimDefined(currentRelativeChild, crossAxis, availableInnerCrossDim) &&
                    measureModeCrossDim == MeasureMode.Exactly                                        &&
                    !(isNodeFlexWrap && flexBasisOverflows)                                           &&
                    YGNodeAlignItem(node, currentRelativeChild) == YGAlign.Stretch                    &&
                    currentRelativeChild.MarginLeadingValue(crossAxis).Unit !=
                    ValueUnit.Auto &&
                    currentRelativeChild.MarginTrailingValue(crossAxis).Unit !=
                    ValueUnit.Auto)
                {
                    childCrossSize        = availableInnerCrossDim;
                    childCrossMeasureMode = MeasureMode.Exactly;
                }
                else if (!YGNodeIsStyleDimDefined(
                    currentRelativeChild,
                    crossAxis,
                    availableInnerCrossDim))
                {
                    childCrossSize = availableInnerCrossDim;
                    childCrossMeasureMode = childCrossSize.IsNaN()
                        ? MeasureMode.Undefined
                        : MeasureMode.AtMost;
                }
                else
                {
                    childCrossSize =
                        UnwrapFloatOptional(
                            ResolveValue(
                                currentRelativeChild.ResolvedDimension[crossAxis.ToDimension()],
                                availableInnerCrossDim)) +
                        marginCross;
                    var isLoosePercentageMeasurement =
                        currentRelativeChild.ResolvedDimension[crossAxis.ToDimension()].Unit ==
                        ValueUnit.Percent &&
                        measureModeCrossDim != MeasureMode.Exactly;
                    childCrossMeasureMode =
                        childCrossSize.IsNaN() || isLoosePercentageMeasurement
                            ? MeasureMode.Undefined
                            : MeasureMode.Exactly;
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
                    currentRelativeChild.MarginLeadingValue(crossAxis).Unit !=
                    ValueUnit.Auto &&
                    currentRelativeChild.MarginTrailingValue(crossAxis).Unit != ValueUnit.Auto;

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

        internal static bool YGIsBaselineLayout(YGNode node)
        {
            if (node.Style.FlexDirection.IsColumn())
                return false;

            if (node.Style.AlignItems == YGAlign.Baseline)
                return true;

            foreach (var child in node.Children)
                if (child.Style.PositionType == PositionType.Relative &&
                    child.Style.AlignSelf    == YGAlign.Baseline)
                    return true;

            return false;
        }

        internal static void YGJustifyMainAxis(
            YGNode                    node,
            CollectFlexItemsRowValues collectedFlexItemsValues,
            int                       startOfLineIndex,
            FlexDirectionType         mainAxis,
            FlexDirectionType         crossAxis,
            MeasureMode               measureModeMainDim,
            MeasureMode               measureModeCrossDim,
            float                     mainAxisownerSize,
            float                     ownerWidth,
            float                     availableInnerMainDim,
            float                     availableInnerCrossDim,
            float                     availableInnerWidth,
            bool                      performLayout)
        {
            var style                        = node.Style;
            var leadingPaddingAndBorderMain  = UnwrapFloatOptional(node.GetLeadingPaddingAndBorder(mainAxis, ownerWidth));
            var trailingPaddingAndBorderMain = UnwrapFloatOptional(node.GetTrailingPaddingAndBorder(mainAxis, ownerWidth));
            // If we are using "at most" rules in the main axis, make sure that
            // remainingFreeSpace is 0 when min main dimension is not given
            if (measureModeMainDim                          == MeasureMode.AtMost &&
                collectedFlexItemsValues.RemainingFreeSpace > 0)
            {
                if (style.MinDimension(mainAxis.ToDimension()).Unit != ValueUnit.Undefined &&
                    ResolveValue(style.MinDimension(mainAxis.ToDimension()), mainAxisownerSize).HasValue)
                {
                    // This condition makes sure that if the size of main dimension(after
                    // considering child nodes main dim, leading and trailing padding etc)
                    // falls below min dimension, then the remainingFreeSpace is reassigned
                    // considering the min dimension

                    // `minAvailableMainDim` denotes minimum available space in which child
                    // can be laid out, it will exclude space consumed by padding and border.
                    var minAvailableMainDim = UnwrapFloatOptional(
                        ResolveValue(
                            style.MinDimension(mainAxis.ToDimension()),
                            mainAxisownerSize)) - leadingPaddingAndBorderMain - trailingPaddingAndBorderMain;
                    var occupiedSpaceByChildNodes = availableInnerMainDim - collectedFlexItemsValues.RemainingFreeSpace;
                    collectedFlexItemsValues.RemainingFreeSpace =
                        FloatMax(0, minAvailableMainDim - occupiedSpaceByChildNodes);
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
                if (child.Style.PositionType == PositionType.Relative)
                {
                    if (child.MarginLeadingValue(mainAxis).Unit == ValueUnit.Auto) numberOfAutoMarginsOnCurrentLine++;

                    if (child.MarginTrailingValue(mainAxis).Unit == ValueUnit.Auto) numberOfAutoMarginsOnCurrentLine++;
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
                case JustifyType.Center:
                    leadingMainDim = collectedFlexItemsValues.RemainingFreeSpace / 2;
                    break;
                case JustifyType.FlexEnd:
                    leadingMainDim = collectedFlexItemsValues.RemainingFreeSpace;
                    break;
                case JustifyType.SpaceBetween:
                    if (collectedFlexItemsValues.ItemsOnLine > 1)
                        betweenMainDim =
                            FloatMax(collectedFlexItemsValues.RemainingFreeSpace, 0) /
                            (collectedFlexItemsValues.ItemsOnLine - 1);
                    else
                        betweenMainDim = 0;

                    break;
                case JustifyType.SpaceEvenly:
                    // Space is distributed evenly across all elements
                    betweenMainDim = collectedFlexItemsValues.RemainingFreeSpace /
                        (collectedFlexItemsValues.ItemsOnLine + 1);
                    leadingMainDim = betweenMainDim;
                    break;
                case JustifyType.SpaceAround:
                    // Space on the edges is half of the space between elements
                    betweenMainDim = collectedFlexItemsValues.RemainingFreeSpace /
                        collectedFlexItemsValues.ItemsOnLine;
                    leadingMainDim = betweenMainDim / 2;
                    break;
                case JustifyType.FlexStart:
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
                if (childStyle.Display == DisplayType.None) continue;

                if (childStyle.PositionType == PositionType.Absolute &&
                    child.IsLeadingPositionDefined(mainAxis))
                {
                    if (performLayout)
                        child.Layout.Position[mainAxis.ToPositionEdge()] =
                            UnwrapFloatOptional(
                                child.GetLeadingPosition(mainAxis, availableInnerMainDim)) +
                            node.GetLeadingBorder(mainAxis)                                +
                            UnwrapFloatOptional(
                                child.GetLeadingMargin(mainAxis, availableInnerWidth));
                }
                else
                {
                    // Now that we placed the element, we need to update the variables.
                    // We need to do that only for relative elements. Absolute elements
                    // do not take part in that phase.
                    if (childStyle.PositionType == PositionType.Relative)
                    {
                        if (child.MarginLeadingValue(mainAxis).Unit == ValueUnit.Auto)
                            collectedFlexItemsValues.MainDim +=
                                collectedFlexItemsValues.RemainingFreeSpace /
                                numberOfAutoMarginsOnCurrentLine;

                        if (performLayout)
                            child.Layout.Position[mainAxis.ToPositionEdge()] =
                                childLayout.Position[mainAxis.ToPositionEdge()] +
                                collectedFlexItemsValues.MainDim;

                        if (child.MarginTrailingValue(mainAxis).Unit == ValueUnit.Auto)
                            collectedFlexItemsValues.MainDim +=
                                collectedFlexItemsValues.RemainingFreeSpace /
                                numberOfAutoMarginsOnCurrentLine;

                        var canSkipFlex =
                            !performLayout && measureModeCrossDim == MeasureMode.Exactly;
                        if (canSkipFlex)
                        {
                            // If we skipped the flex step, then we can't rely on the
                            // measuredDims because
                            // they weren't computed. This means we can't call
                            // YGNodeDimWithMargin.
                            collectedFlexItemsValues.MainDim += betweenMainDim +
                                UnwrapFloatOptional(
                                    child.GetMarginForAxis(
                                        mainAxis,
                                        availableInnerWidth)) +
                                UnwrapFloatOptional(childLayout.ComputedFlexBasis);
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
                                    UnwrapFloatOptional(
                                        child.GetLeadingMargin(
                                            FlexDirectionType.Column,
                                            availableInnerWidth));
                                var descent =
                                    child.Layout.MeasuredHeight +
                                    UnwrapFloatOptional(
                                        child.GetMarginForAxis(
                                            FlexDirectionType.Column,
                                            availableInnerWidth)) -
                                    ascent;

                                maxAscentForCurrentLine =
                                    FloatMax(maxAscentForCurrentLine, ascent);
                                maxDescentForCurrentLine =
                                    FloatMax(maxDescentForCurrentLine, descent);
                            }
                            else
                            {
                                // The cross dimension is the max of the elements dimension since
                                // there can only be one element in that cross dimension in the case
                                // when the items are not baseline aligned
                                collectedFlexItemsValues.CrossDim = FloatMax(
                                    collectedFlexItemsValues.CrossDim,
                                    YGNodeDimWithMargin(child, crossAxis, availableInnerWidth));
                            }
                        }
                    }
                    else if (performLayout)
                    {
                        child.Layout.Position[mainAxis.ToPositionEdge()] =
                            childLayout.Position[mainAxis.ToPositionEdge()] +
                            node.GetLeadingBorder(mainAxis)                 + leadingMainDim;
                    }
                }
            }

            collectedFlexItemsValues.MainDim += trailingPaddingAndBorderMain;

            if (isNodeBaselineLayout)
                collectedFlexItemsValues.CrossDim =
                    maxAscentForCurrentLine + maxDescentForCurrentLine;
        }

        internal static string YGMeasureModeName(MeasureMode mode, bool performLayout)
        {
            string[] kMeasureModeNames = {"UNDEFINED", "EXACTLY", "AT_MOST"};
            string[] kLayoutModeNames  = {"LAY_UNDEFINED", "LAY_EXACTLY", "LAY_AT_MOST"};

            if (mode >= MeasureMode.AtMost) return "";

            return performLayout ? kLayoutModeNames[(int) mode] : kMeasureModeNames[(int) mode];
        }

        // inline
        internal static bool YGMeasureModeNewMeasureSizeIsStricterAndStillValid(
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
        internal static bool YGMeasureModeOldSizeIsUnspecifiedAndStillFits(
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
        internal static bool YGMeasureModeSizeIsExactAndMatchesOldMeasuredSize(
            MeasureMode sizeMode,
            float       size,
            float       lastComputedSize)
        {
            return sizeMode == MeasureMode.Exactly &&
                FloatEqual(size, lastComputedSize);
        }

        internal static void YGNodeAbsoluteLayoutChild(
            YGNode        node,
            YGNode        child,
            float         width,
            MeasureMode   widthMode,
            float         height,
            DirectionType direction,
            YogaConfig    config)
        {
            var mainAxis      = ResolveFlexDirection(node.Style.FlexDirection, direction);
            var crossAxis     = FlexDirectionCross(mainAxis, direction);
            var isMainAxisRow = mainAxis.IsRow();

            var childWidth             = float.NaN;
            var childHeight            = float.NaN;
            var childWidthMeasureMode  = MeasureMode.Undefined;
            var childHeightMeasureMode = MeasureMode.Undefined;

            var marginRow =
                UnwrapFloatOptional(child.GetMarginForAxis(FlexDirectionType.Row, width));
            var marginColumn = UnwrapFloatOptional(
                child.GetMarginForAxis(FlexDirectionType.Column, width));

            if (YGNodeIsStyleDimDefined(child, FlexDirectionType.Row, width))
            {
                childWidth = UnwrapFloatOptional(
                    ResolveValue(child.ResolvedDimension.Width, width)) + marginRow;
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
                        UnwrapFloatOptional(
                            child.GetLeadingPosition(FlexDirectionType.Row, width) +
                            child.GetTrailingPosition(FlexDirectionType.Row, width));
                    childWidth =
                        YGNodeBoundAxis(child, FlexDirectionType.Row, childWidth, width, width);
                }
            }

            if (YGNodeIsStyleDimDefined(child, FlexDirectionType.Column, height))
            {
                childHeight = UnwrapFloatOptional(
                    ResolveValue(child.ResolvedDimension.Height, height)) + marginColumn;
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
                        UnwrapFloatOptional(
                            child.GetLeadingPosition(FlexDirectionType.Column, height) +
                            child.GetTrailingPosition(FlexDirectionType.Column, height));
                    childHeight = YGNodeBoundAxis(
                        child,
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
                    UnwrapFloatOptional(
                        child.GetMarginForAxis(FlexDirectionType.Row, width));
                childHeight = child.Layout.MeasuredHeight +
                    UnwrapFloatOptional(
                        child.GetMarginForAxis(FlexDirectionType.Column, width));
            }

            YGLayoutNodeInternal(
                child,
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
                    node.Layout.GetMeasuredDimension(mainAxis.ToDimension())      -
                    child.Layout.GetMeasuredDimension(mainAxis.ToDimension())     -
                    node.GetTrailingBorder(mainAxis)                              -
                    UnwrapFloatOptional(child.GetTrailingMargin(mainAxis, width)) -
                    UnwrapFloatOptional(
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
                    node.Layout.GetMeasuredDimension(crossAxis.ToDimension())      -
                    child.Layout.GetMeasuredDimension(crossAxis.ToDimension())     -
                    node.GetTrailingBorder(crossAxis)                              -
                    UnwrapFloatOptional(child.GetTrailingMargin(crossAxis, width)) -
                    UnwrapFloatOptional(
                        child.GetTrailingPosition(
                            crossAxis,
                            isMainAxisRow ? height : width));
            else if (
                !child.IsLeadingPositionDefined(crossAxis) &&
                YGNodeAlignItem(node, child) == YGAlign.Center)
                child.Layout.Position[crossAxis.ToLeadingEdge()] =
                    (node.Layout.GetMeasuredDimension(crossAxis.ToDimension()) -
                        child.Layout.GetMeasuredDimension(crossAxis.ToDimension())) /
                    2.0f;

            else if (
                !child.IsLeadingPositionDefined(crossAxis) &&
                (YGNodeAlignItem(node, child) == YGAlign.FlexEnd) ^
                (node.Style.FlexWrap          == WrapType.WrapReverse))
                child.Layout.Position[crossAxis.ToLeadingEdge()] =
                    node.Layout.GetMeasuredDimension(crossAxis.ToDimension()) -
                    child.Layout.GetMeasuredDimension(crossAxis.ToDimension());
        }

        internal static YGAlign YGNodeAlignItem(
            YGNode node,
            YGNode child)
        {
            var align = child.Style.AlignSelf == YGAlign.Auto
                ? node.Style.AlignItems
                : child.Style.AlignSelf;
            if (align == YGAlign.Baseline &&
                node.Style.FlexDirection.IsColumn())
                return YGAlign.FlexStart;

            return align;
        }

        // Like YGNodeBoundAxisWithinMinAndMax but also ensures that the value doesn't
        // go below the padding and border amount.
        // inline
        internal static float YGNodeBoundAxis(
            YGNode            node,
            FlexDirectionType axis,
            float             value,
            float             axisSize,
            float             widthSize)
        {
            return FloatMax(
                UnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(node, axis, value, axisSize)),
                YGNodePaddingAndBorderForAxis(node, axis, widthSize));
        }

        internal static float? YGNodeBoundAxisWithinMinAndMax(
            YGNode            node,
            FlexDirectionType axis,
            float             value,
            float             axisSize)
        {
            float? min = null;
            float? max = null;

            if (axis.IsColumn())
            {
                min = ResolveValue(node.Style.MinHeight, axisSize);
                max = ResolveValue(node.Style.MaxHeight, axisSize);
            }
            else if (axis.IsRow())
            {
                min = ResolveValue(node.Style.MinWidth, axisSize);
                max = ResolveValue(node.Style.MaxWidth, axisSize);
            }

            if (max.HasValue && max >= 0 && value > max)
                return max;

            if (min.HasValue && min >= 0 && value < min)
                return min;

            return value;
        }

        internal static float YGNodeCalculateAvailableInnerDim(
            YGNode            node,
            FlexDirectionType axis,
            float             availableDim,
            float             ownerDim)
        {
            var direction = axis.IsRow() ? FlexDirectionType.Row : FlexDirectionType.Column;
            var dimension = axis.IsRow() ? DimensionType.Width : DimensionType.Height;

            var margin           = UnwrapFloatOptional(node.GetMarginForAxis(direction, ownerDim));
            var paddingAndBorder = YGNodePaddingAndBorderForAxis(node, direction, ownerDim);

            var availableInnerDim = availableDim - margin - paddingAndBorder;
            // Max dimension overrides predefined dimension value; Min dimension in turn
            // overrides both of the above
            if (availableInnerDim.HasValue())
            {
                // We want to make sure our available height does not violate min and max
                // constraints
                var minDimensionOptional =
                    ResolveValue(node.Style.MinDimension(dimension), ownerDim);
                var minInnerDim = minDimensionOptional.IsNaN()
                    ? 0.0f
                    : minDimensionOptional.Value - paddingAndBorder;

                var maxDimensionOptional =
                    ResolveValue(node.Style.MaxDimension(dimension), ownerDim);

                var maxInnerDim = maxDimensionOptional.IsNaN()
                    ? float.MaxValue
                    : maxDimensionOptional.Value - paddingAndBorder;
                availableInnerDim =
                    FloatMax(FloatMin(availableInnerDim, maxInnerDim), minInnerDim);
            }

            return availableInnerDim;
        }

        internal static void YGNodeComputeFlexBasisForChild(
            YGNode        node,
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
            var mainAxis          = ResolveFlexDirection(node.Style.FlexDirection, direction);
            var isMainAxisRow     = mainAxis.IsRow();
            var mainAxisSize      = isMainAxisRow ? width : height;
            var mainAxisownerSize = isMainAxisRow ? ownerWidth : ownerHeight;

            float       childWidth;
            float       childHeight;
            MeasureMode childWidthMeasureMode;
            MeasureMode childHeightMeasureMode;

            var resolvedFlexBasis = ResolveValue(child.ResolveFlexBasisPtr(), mainAxisownerSize);
            var isRowStyleDimDefined =
                YGNodeIsStyleDimDefined(child, FlexDirectionType.Row, ownerWidth);
            var isColumnStyleDimDefined =
                YGNodeIsStyleDimDefined(child, FlexDirectionType.Column, ownerHeight);

            if (resolvedFlexBasis.HasValue && mainAxisSize.HasValue())
            {
                if (child.Layout.ComputedFlexBasis.IsNaN() ||
                    child.Config.ExperimentalFeatures.HasFlag(ExperimentalFeatures.WebFlexBasis) &&
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
                    YGNodePaddingAndBorderForAxis(child, FlexDirectionType.Row, ownerWidth));

                child.Layout.ComputedFlexBasis =
                    FloatOptionalMax(
                        ResolveValue(child.ResolvedDimension.Width, ownerWidth),
                        paddingAndBorder);
            }
            else if (!isMainAxisRow && isColumnStyleDimDefined)
            {
                // The height is definite, so use that as the flex basis.
                var paddingAndBorder = new float?(
                    YGNodePaddingAndBorderForAxis(
                        child,
                        FlexDirectionType.Column,
                        ownerWidth));
                child.Layout.ComputedFlexBasis =
                    FloatOptionalMax(
                        ResolveValue(child.ResolvedDimension.Height, ownerHeight),
                        paddingAndBorder);
            }
            else
            {
                // Compute the flex basis and hypothetical main size (i.e. the clamped
                // flex basis).
                childWidth             = float.NaN;
                childHeight            = float.NaN;
                childWidthMeasureMode  = MeasureMode.Undefined;
                childHeightMeasureMode = MeasureMode.Undefined;

                var marginRow = UnwrapFloatOptional(
                    child.GetMarginForAxis(FlexDirectionType.Row, ownerWidth));
                var marginColumn = UnwrapFloatOptional(
                    child.GetMarginForAxis(FlexDirectionType.Column, ownerWidth));

                if (isRowStyleDimDefined)
                {
                    childWidth =
                        UnwrapFloatOptional(
                            ResolveValue(
                                child.ResolvedDimension.Width,
                                ownerWidth)) +
                        marginRow;
                    childWidthMeasureMode = MeasureMode.Exactly;
                }

                if (isColumnStyleDimDefined)
                {
                    childHeight =
                        UnwrapFloatOptional(
                            ResolveValue(child.ResolvedDimension.Height, ownerHeight)) + marginColumn;
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
                var childWidthStretch =
                    YGNodeAlignItem(node, child) == YGAlign.Stretch &&
                    childWidthMeasureMode        != MeasureMode.Exactly;
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
                var childHeightStretch =
                    YGNodeAlignItem(node, child) == YGAlign.Stretch &&
                    childHeightMeasureMode       != MeasureMode.Exactly;
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

                YGConstrainMaxSizeForMode(
                    child,
                    FlexDirectionType.Row,
                    ownerWidth,
                    ownerWidth,
                    ref childWidthMeasureMode,
                    ref childWidth);
                YGConstrainMaxSizeForMode(
                    child,
                    FlexDirectionType.Column,
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
                    FloatMax(
                        child.Layout.GetMeasuredDimension(mainAxis.ToDimension()),
                        YGNodePaddingAndBorderForAxis(child, mainAxis, ownerWidth));
            }

            child.Layout.ComputedFlexBasisGeneration = gCurrentGenerationCount;
        }

        // inline
        internal static float YGNodeDimWithMargin(
            YGNode            node,
            FlexDirectionType axis,
            float             widthSize)
        {
            return node.Layout.GetMeasuredDimension(axis.ToDimension()) +
                UnwrapFloatOptional(
                    node.GetLeadingMargin(axis, widthSize) +
                    node.GetTrailingMargin(axis, widthSize));
        }

        // For nodes with no children, use the available values if they were provided,
        // or the minimum size as indicated by the padding and border sizes.
        internal static void YGNodeEmptyContainerSetMeasuredDimensions(
            YGNode      node,
            float       availableWidth,
            float       availableHeight,
            MeasureMode widthMeasureMode,
            MeasureMode heightMeasureMode,
            float       ownerWidth,
            float       ownerHeight)
        {
            var paddingAndBorderAxisRow    = YGNodePaddingAndBorderForAxis(node, FlexDirectionType.Row,    ownerWidth);
            var paddingAndBorderAxisColumn = YGNodePaddingAndBorderForAxis(node, FlexDirectionType.Column, ownerWidth);
            var marginAxisRow              = UnwrapFloatOptional(node.GetMarginForAxis(FlexDirectionType.Row,    ownerWidth));
            var marginAxisColumn           = UnwrapFloatOptional(node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth));

            node.Layout.SetMeasuredDimension(
                DimensionType.Width,
                YGNodeBoundAxis(
                    node,
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
                YGNodeBoundAxis(
                    node,
                    FlexDirectionType.Column,
                    heightMeasureMode == MeasureMode.Undefined ||
                    heightMeasureMode == MeasureMode.AtMost
                        ? paddingAndBorderAxisColumn
                        : availableHeight - marginAxisColumn,
                    ownerHeight,
                    ownerWidth)
            );
        }

        internal static bool YGNodeFixedSizeSetMeasuredDimensions(
            YGNode      node,
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
                var marginAxisColumn = UnwrapFloatOptional(
                    node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth));
                var marginAxisRow = UnwrapFloatOptional(
                    node.GetMarginForAxis(FlexDirectionType.Row, ownerWidth));

                node.Layout.SetMeasuredDimension(
                    DimensionType.Width,
                    YGNodeBoundAxis(
                        node,
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
                    YGNodeBoundAxis(
                        node,
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

        // inline
        internal static bool YGNodeIsLayoutDimDefined(
            YGNode            node,
            FlexDirectionType axis)
        {
            var value = node.Layout.GetMeasuredDimension(axis.ToDimension());
            return value.HasValue() && value >= 0.0f;
        }

        // inline
        internal static bool YGNodeIsStyleDimDefined(
            YGNode            node,
            FlexDirectionType axis,
            float             ownerSize)
        {
            var isUndefined = node.ResolvedDimension[axis.ToDimension()].IsNaN();
            return !(
                node.ResolvedDimension[axis.ToDimension()].Unit == ValueUnit.Auto      ||
                node.ResolvedDimension[axis.ToDimension()].Unit == ValueUnit.Undefined ||
                node.ResolvedDimension[axis.ToDimension()].Unit == ValueUnit.Point &&
                !isUndefined                                                       &&
                node.ResolvedDimension[axis.ToDimension()].Value < 0.0f ||
                node.ResolvedDimension[axis.ToDimension()].Unit == ValueUnit.Percent &&
                !isUndefined                                                         &&
                (node.ResolvedDimension[axis.ToDimension()].Value < 0.0f || ownerSize.IsNaN()));
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
            DirectionType ownerDirection,
            MeasureMode   widthMeasureMode,
            MeasureMode   heightMeasureMode,
            float         ownerWidth,
            float         ownerHeight,
            bool          performLayout,
            YogaConfig    config)
        {
            YGAssertWithNode(
                node,
                availableWidth.IsNaN()
                    ? widthMeasureMode == MeasureMode.Undefined
                    : true,
                "availableWidth is indefinite so widthMeasureMode must be YGMeasureMode.Undefined");
            YGAssertWithNode(
                node,
                availableHeight.IsNaN()
                    ? heightMeasureMode == MeasureMode.Undefined
                    : true,
                "availableHeight is indefinite so heightMeasureMode must be YGMeasureMode.Undefined");

            // Set the resolved resolution in the node's layout.
            var direction = node.ResolveDirection(ownerDirection);
            node.Layout.Direction = direction;

            var flexRowDirection    = ResolveFlexDirection(FlexDirectionType.Row,    direction);
            var flexColumnDirection = ResolveFlexDirection(FlexDirectionType.Column, direction);

            node.Layout.Margin.Start  = UnwrapFloatOptional(node.GetLeadingMargin(flexRowDirection, ownerWidth));
            node.Layout.Margin.End    = UnwrapFloatOptional(node.GetTrailingMargin(flexRowDirection, ownerWidth));
            node.Layout.Margin.Top    = UnwrapFloatOptional(node.GetLeadingMargin(flexColumnDirection, ownerWidth));
            node.Layout.Margin.Bottom = UnwrapFloatOptional(node.GetTrailingMargin(flexColumnDirection, ownerWidth));

            node.Layout.Border.Start  = node.GetLeadingBorder(flexRowDirection);
            node.Layout.Border.End    = node.GetTrailingBorder(flexRowDirection);
            node.Layout.Border.Top    = node.GetLeadingBorder(flexColumnDirection);
            node.Layout.Border.Bottom = node.GetTrailingBorder(flexColumnDirection);

            node.Layout.Padding.Start  = UnwrapFloatOptional(node.GetLeadingPadding(flexRowDirection, ownerWidth));
            node.Layout.Padding.End    = UnwrapFloatOptional(node.GetTrailingPadding(flexRowDirection, ownerWidth));
            node.Layout.Padding.Top    = UnwrapFloatOptional(node.GetLeadingPadding(flexColumnDirection, ownerWidth));
            node.Layout.Padding.Bottom = UnwrapFloatOptional(node.GetTrailingPadding(flexColumnDirection, ownerWidth));

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
            var mainAxis       = ResolveFlexDirection(node.Style.FlexDirection, direction);
            var crossAxis      = FlexDirectionCross(mainAxis, direction);
            var isMainAxisRow  = mainAxis.IsRow();
            var isNodeFlexWrap = node.Style.FlexWrap != WrapType.NoWrap;

            var mainAxisownerSize  = isMainAxisRow ? ownerWidth : ownerHeight;
            var crossAxisownerSize = isMainAxisRow ? ownerHeight : ownerWidth;

            var leadingPaddingAndBorderCross = UnwrapFloatOptional(node.GetLeadingPaddingAndBorder(crossAxis, ownerWidth));
            var paddingAndBorderAxisMain     = YGNodePaddingAndBorderForAxis(node, mainAxis,  ownerWidth);
            var paddingAndBorderAxisCross    = YGNodePaddingAndBorderForAxis(node, crossAxis, ownerWidth);

            var measureModeMainDim  = isMainAxisRow ? widthMeasureMode : heightMeasureMode;
            var measureModeCrossDim = isMainAxisRow ? heightMeasureMode : widthMeasureMode;

            var paddingAndBorderAxisRow    = isMainAxisRow ? paddingAndBorderAxisMain : paddingAndBorderAxisCross;
            var paddingAndBorderAxisColumn = isMainAxisRow ? paddingAndBorderAxisCross : paddingAndBorderAxisMain;

            var marginAxisRow    = UnwrapFloatOptional(node.GetMarginForAxis(FlexDirectionType.Row,    ownerWidth));
            var marginAxisColumn = UnwrapFloatOptional(node.GetMarginForAxis(FlexDirectionType.Column, ownerWidth));

            var minInnerWidth = UnwrapFloatOptional(
                ResolveValue(node.Style.MinWidth, ownerWidth)) - paddingAndBorderAxisRow;
            var maxInnerWidth =
                UnwrapFloatOptional(
                    ResolveValue(node.Style.MaxWidth, ownerWidth)) - paddingAndBorderAxisRow;
            var minInnerHeight =
                UnwrapFloatOptional(
                    ResolveValue(node.Style.MinHeight, ownerHeight)) - paddingAndBorderAxisColumn;
            var maxInnerHeight =
                UnwrapFloatOptional(
                    ResolveValue(node.Style.MaxHeight, ownerHeight)) - paddingAndBorderAxisColumn;

            var minInnerMainDim = isMainAxisRow ? minInnerWidth : minInnerHeight;
            var maxInnerMainDim = isMainAxisRow ? maxInnerWidth : maxInnerHeight;

            // STEP 2: DETERMINE AVAILABLE SIZE IN MAIN AND CROSS DIRECTIONS

            var availableInnerWidth = YGNodeCalculateAvailableInnerDim(
                node,
                FlexDirectionType.Row,
                availableWidth,
                ownerWidth);
            var availableInnerHeight = YGNodeCalculateAvailableInnerDim(
                node,
                FlexDirectionType.Column,
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
                if (measureModeCrossDim == MeasureMode.Undefined ||
                    measureModeCrossDim == MeasureMode.AtMost)
                    containerCrossAxis =
                        YGNodeBoundAxis(
                            node,
                            crossAxis,
                            collectedFlexItemsValues.CrossDim + paddingAndBorderAxisCross,
                            crossAxisownerSize,
                            ownerWidth) -
                        paddingAndBorderAxisCross;

                // If there's no flex wrap, the cross dimension is defined by the container.
                if (!isNodeFlexWrap && measureModeCrossDim == MeasureMode.Exactly) collectedFlexItemsValues.CrossDim = availableInnerCrossDim;

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
                                    UnwrapFloatOptional(
                                        child.GetLeadingPosition(
                                            crossAxis,
                                            availableInnerCrossDim)) +
                                    node.GetLeadingBorder(crossAxis) +
                                    UnwrapFloatOptional(
                                        child.GetLeadingMargin(
                                            crossAxis,
                                            availableInnerWidth));

                            // If leading position is not defined or calculations result in Nan,
                            // default to border + margin
                            if (!isChildLeadingPosDefined ||
                                child.Layout.Position[crossAxis.ToPositionEdge()].IsNaN())
                                child.Layout.Position[crossAxis.ToPositionEdge()] =
                                    node.GetLeadingBorder(crossAxis) + UnwrapFloatOptional(child.GetLeadingMargin(crossAxis, availableInnerWidth));
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
                                child.MarginLeadingValue(crossAxis).Unit  != ValueUnit.Auto  &&
                                child.MarginTrailingValue(crossAxis).Unit != ValueUnit.Auto)
                            {
                                // If the child defines a definite size for its cross axis, there's
                                // no need to stretch.
                                if (!YGNodeIsStyleDimDefined(
                                    child,
                                    crossAxis,
                                    availableInnerCrossDim))
                                {
                                    var childMainSize = child.Layout.GetMeasuredDimension(mainAxis.ToDimension());
                                    var childCrossSize =
                                        child.Style.AspectRatio.HasValue
                                            ? UnwrapFloatOptional(
                                                child.GetMarginForAxis(crossAxis, availableInnerWidth)) +
                                            (isMainAxisRow
                                                ? childMainSize / child.Style.AspectRatio.Value
                                                : childMainSize * child.Style.AspectRatio.Value)
                                            : collectedFlexItemsValues.CrossDim;

                                    childMainSize += UnwrapFloatOptional(
                                        child.GetMarginForAxis(mainAxis, availableInnerWidth));

                                    var childMainMeasureMode  = MeasureMode.Exactly;
                                    var childCrossMeasureMode = MeasureMode.Exactly;
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
                                        childWidth.IsNaN() ? MeasureMode.Undefined : MeasureMode.Exactly;
                                    var childHeightMeasureMode =
                                        childHeight.IsNaN() ? MeasureMode.Undefined : MeasureMode.Exactly;

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
                maxLineMainDim =
                    FloatMax(maxLineMainDim, collectedFlexItemsValues.MainDim);
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
                        if (child.Style.Display == DisplayType.None) continue;

                        if (child.Style.PositionType == PositionType.Relative)
                        {
                            if (child.LineIndex != i) break;

                            if (YGNodeIsLayoutDimDefined(child, crossAxis))
                                lineHeight = FloatMax(
                                    lineHeight,
                                    child.Layout.GetMeasuredDimension(crossAxis.ToDimension()) +
                                    UnwrapFloatOptional(
                                        child.GetMarginForAxis(
                                            crossAxis,
                                            availableInnerWidth)));

                            if (YGNodeAlignItem(node, child) == YGAlign.Baseline)
                            {
                                var ascent = YGBaseline(child) +
                                    UnwrapFloatOptional(
                                        child.GetLeadingMargin(
                                            FlexDirectionType.Column,
                                            availableInnerWidth));
                                var descent =
                                    child.Layout.MeasuredHeight +
                                    UnwrapFloatOptional(
                                        child.GetMarginForAxis(
                                            FlexDirectionType.Column,
                                            availableInnerWidth)) -
                                    ascent;
                                maxAscentForCurrentLine =
                                    FloatMax(maxAscentForCurrentLine, ascent);
                                maxDescentForCurrentLine =
                                    FloatMax(maxDescentForCurrentLine, descent);
                                lineHeight = FloatMax(
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
                                switch (YGNodeAlignItem(node, child))
                                {
                                case YGAlign.FlexStart:
                                {
                                    child.Layout.Position[crossAxis.ToPositionEdge()] =
                                        currentLead +
                                        UnwrapFloatOptional(child.GetLeadingMargin(crossAxis, availableInnerWidth));
                                    break;
                                }
                                case YGAlign.FlexEnd:
                                {
                                    child.Layout.Position[crossAxis.ToPositionEdge()] =
                                        currentLead + lineHeight -
                                        UnwrapFloatOptional(
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
                                        UnwrapFloatOptional(child.GetLeadingMargin(crossAxis, availableInnerWidth));

                                    // Remeasure child with the line height as it as been only
                                    // measured with the owners height yet.
                                    if (!YGNodeIsStyleDimDefined(
                                        child,
                                        crossAxis,
                                        availableInnerCrossDim))
                                    {
                                        var childWidth = isMainAxisRow
                                            ? child.Layout.MeasuredWidth +
                                            UnwrapFloatOptional(
                                                child.GetMarginForAxis(
                                                    mainAxis,
                                                    availableInnerWidth))
                                            : lineHeight;

                                        var childHeight = !isMainAxisRow
                                            ? child.Layout.MeasuredHeight +
                                            UnwrapFloatOptional(
                                                child.GetMarginForAxis(
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
                                                MeasureMode.Exactly,
                                                MeasureMode.Exactly,
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
                                    child.Layout.Position[EdgeType.Top] =
                                        currentLead + maxAscentForCurrentLine - YGBaseline(child) +
                                        UnwrapFloatOptional(
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
                YGNodeBoundAxis(
                    node,
                    FlexDirectionType.Row,
                    availableWidth - marginAxisRow,
                    ownerWidth,
                    ownerWidth)
            );

            node.Layout.SetMeasuredDimension(
                DimensionType.Height,
                YGNodeBoundAxis(
                    node,
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
                    YGNodeBoundAxis(
                        node,
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
                    FloatMax(
                        FloatMin(
                            availableInnerMainDim + paddingAndBorderAxisMain,
                            UnwrapFloatOptional(
                                YGNodeBoundAxisWithinMinAndMax(
                                    node,
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
                    YGNodeBoundAxis(
                        node,
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
                    FloatMax(
                        FloatMin(
                            availableInnerCrossDim + paddingAndBorderAxisCross,
                            UnwrapFloatOptional(
                                YGNodeBoundAxisWithinMinAndMax(
                                    node,
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

                        if (needsMainTrailingPos) YGNodeSetChildTrailingPosition(node, child, mainAxis);

                        if (needsCrossTrailingPos) YGNodeSetChildTrailingPosition(node, child, crossAxis);
                    }
            }
        }


        internal static float YGNodePaddingAndBorderForAxis(
            YGNode            node,
            FlexDirectionType axis,
            float             widthSize)
        {
            return UnwrapFloatOptional(
                node.GetLeadingPaddingAndBorder(axis, widthSize) +
                node.GetTrailingPaddingAndBorder(axis, widthSize));
        }

        internal static void YGNodeSetChildTrailingPosition(
            YGNode            node,
            YGNode            child,
            FlexDirectionType axis)
        {
            var size = child.Layout.GetMeasuredDimension(axis.ToDimension());
            child.Layout.Position[axis.ToTrailingEdge()] =
                node.Layout.GetMeasuredDimension(axis.ToDimension()) - size -
                child.Layout.Position[axis.ToPositionEdge()];
        }

        internal static void YGNodeWithMeasureFuncSetMeasuredDimensions(
            YGNode      node,
            float       availableWidth,
            float       availableHeight,
            MeasureMode widthMeasureMode,
            MeasureMode heightMeasureMode,
            float       ownerWidth,
            float       ownerHeight)
        {
            YGAssertWithNode(
                node,
                node.MeasureFunc != null,
                "Expected node to have custom measure function");

            var paddingAndBorderAxisRow    = YGNodePaddingAndBorderForAxis(node, FlexDirectionType.Row,    availableWidth);
            var paddingAndBorderAxisColumn = YGNodePaddingAndBorderForAxis(node, FlexDirectionType.Column, availableWidth);
            var marginAxisRow              = UnwrapFloatOptional(node.GetMarginForAxis(FlexDirectionType.Row,    availableWidth));
            var marginAxisColumn           = UnwrapFloatOptional(node.GetMarginForAxis(FlexDirectionType.Column, availableWidth));

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
                node.Layout.SetMeasuredDimension(
                    DimensionType.Width,
                    YGNodeBoundAxis(
                        node,
                        FlexDirectionType.Row,
                        availableWidth - marginAxisRow,
                        ownerWidth,
                        ownerWidth)
                );
                node.Layout.SetMeasuredDimension(
                    DimensionType.Height,
                    YGNodeBoundAxis(
                        node,
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
                    YGNodeBoundAxis(
                        node,
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
                    YGNodeBoundAxis(
                        node,
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
            var textRounding = node.NodeType == NodeType.Text;

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
                DimensionType.Width,
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
                DimensionType.Height,
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
                YGRoundToPixelGrid(
                    child,
                    pointScaleFactor,
                    absoluteNodeLeft,
                    absoluteNodeTop);
        }

        internal static string YGSpacer(int level)
        {
            var spacerLen = spacer.Length;
            if (level > spacerLen)
                return spacer;
            return spacer.Substring(spacerLen - level);
        }

        internal static void YGTraverseChildrenPreOrder(
            IEnumerable<YGNode> children,
            Action<YGNode>      f)
        {
            foreach (var node in children)
            {
                f(node);
                YGTraverseChildrenPreOrder(node.Children, f);
            }
        }

        internal static void YGVLog(
            YogaConfig config,
            YGNode     node,
            LogLevel   level,
            string     format, params object[] args)
        {
            var logConfig = config ?? YogaConfig.DefaultConfig;
            logConfig.Logger(logConfig, node, level, format, args);

            if (level == LogLevel.Fatal) throw new SystemException();
        }

        private static void YGNodeComputeFlexBasisForChildren(
            YGNode            node,
            float             availableInnerWidth,
            float             availableInnerHeight,
            MeasureMode       widthMeasureMode,
            MeasureMode       heightMeasureMode,
            DirectionType     direction,
            FlexDirectionType mainAxis,
            YogaConfig        config,
            bool              performLayout,
            float             totalOuterFlexBasis)
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
                        if (singleFlexChild != null                     ||
                            FloatEqual(child.ResolveFlexGrow(),   0.0f) ||
                            FloatEqual(child.ResolveFlexShrink(), 0.0f))
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
                    YGZeroOutLayoutRecursively(child);
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

                totalOuterFlexBasis += UnwrapFloatOptional(
                    child.Layout.ComputedFlexBasis +
                    child.GetMarginForAxis(mainAxis, availableInnerWidth));
            }
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
            FlexDirectionType         mainAxis,
            FlexDirectionType         crossAxis,
            float                     mainAxisownerSize,
            float                     availableInnerMainDim,
            float                     availableInnerCrossDim,
            float                     availableInnerWidth,
            float                     availableInnerHeight,
            bool                      flexBasisOverflows,
            MeasureMode               measureModeCrossDim,
            bool                      performLayout,
            YogaConfig                config)
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

        private static void YGZeroOutLayoutRecursively(YGNode node)
        {
            node.Layout = new NodeLayout
            {
                MeasuredWidth  = 0,
                MeasuredHeight = 0,
                Width          = 0,
                Height         = 0
            };
            node.HasNewLayout = true;

            foreach (var child in node.Children)
                YGZeroOutLayoutRecursively(child);
        }
    }
}
