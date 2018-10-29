using System.Collections.Generic;
using System.Diagnostics;

namespace Xamarin.Yoga
{
    using static NumberExtensions;

    // This struct is an helper model to hold the data for step 4 of flexbox
    // algo, which is collecting the flex items in a line.
    //
    // - itemsOnLine: Number of items which can fit in a line considering the
    // available Inner dimension, the flex items computed flexbasis and their
    // margin. It may be different than the difference between start and end
    // indicates because we skip over absolute-positioned items.
    //
    // - sizeConsumedOnCurrentLine: It is accumulation of the dimensions and margin
    // of all the children on the current line. This will be used in order to either
    // set the dimensions of the node if none already exist or to compute the
    // remaining space left for the flexible children.
    //
    // - totalFlexGrowFactors: total flex grow factors of flex items which are to be
    // layed in the current line
    //
    // - totalFlexShrinkFactors: total flex shrink factors of flex items which are
    // to be layed in the current line
    //
    // - endOfLineIndex: Its the end index of the last flex item which was examined
    // and it may or may not be part of the current line(as it may be absolutely
    // positioned or inculding it may have caused to overshoot availableInnerDim)
    //
    // - relativeChildren: Maintain a vector of the child nodes that can shrink
    // and/or grow.

    [DebuggerDisplay(
        "items:{ItemsOnLine} sizeConsumed:{SizeConsumedOnCurrentLine} TFG:{TotalFlexGrowFactors} TFS:{TotalFlexShrinkScaledFactors} EOL:{EndOfLineIndex} RFS:{RemainingFreeSpace} Main:{MainDim} Cross:{CrossDim}")]
    internal class CollectFlexItemsRowValues
    {
        // The size of the crossDim for the row after considering size, padding,
        // margin and border of flex items. Used for calculating containers crossSize.
        public float CrossDim;
        public int   EndOfLineIndex;
        public uint  ItemsOnLine;

        // The size of the mainDim for the row after considering size, padding, margin
        // and border of flex items. This is used to calculate maxLineDim after going
        // through all the rows to decide on the main axis size of owner.
        public float        MainDim;
        public List<YogaNode> RelativeChildren;
        public float        RemainingFreeSpace;
        public float        SizeConsumedOnCurrentLine;
        public float        TotalFlexGrowFactors;
        public float        TotalFlexShrinkScaledFactors;

        // It distributes the free space to the flexible items.For those flexible items
        // whose min and max constraints are triggered, those flex item's clamped size
        // is removed from the remaingfreespace.

        internal void YGDistributeFreeSpaceFirstPass(
            FlexDirectionType mainAxis,
            float             mainAxisownerSize,
            float             availableInnerMainDim,
            float             availableInnerWidth)
        {
            float flexShrinkScaledFactor = 0;
            float flexGrowFactor         = 0;
            float baseMainSize           = 0;
            float boundMainSize          = 0;
            float deltaFreeSpace         = 0;

            foreach (var currentRelativeChild in RelativeChildren)
            {
                var childFlexBasis = 
                    currentRelativeChild.BoundAxisWithinMinAndMax(
                        mainAxis,
                        currentRelativeChild.Layout.ComputedFlexBasis.Unwrap(),
                        mainAxisownerSize);

                if (RemainingFreeSpace < 0)
                {
                    flexShrinkScaledFactor =
                        -currentRelativeChild.ResolveFlexShrink() * childFlexBasis;

                    // Is this child able to shrink?
                    if (flexShrinkScaledFactor.HasValue() && flexShrinkScaledFactor != 0)
                    {
                        baseMainSize = childFlexBasis + RemainingFreeSpace / TotalFlexShrinkScaledFactors * flexShrinkScaledFactor;
                        boundMainSize = currentRelativeChild.BoundAxis(
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
                            deltaFreeSpace               += boundMainSize - childFlexBasis;
                            TotalFlexShrinkScaledFactors -= flexShrinkScaledFactor;
                        }
                    }
                }
                else if (RemainingFreeSpace.HasValue() && RemainingFreeSpace > 0)
                {
                    flexGrowFactor = currentRelativeChild.ResolveFlexGrow();

                    // Is this child able to grow?
                    if (flexGrowFactor.HasValue() && flexGrowFactor != 0)
                    {
                        baseMainSize = childFlexBasis + RemainingFreeSpace / TotalFlexGrowFactors * flexGrowFactor;
                        boundMainSize = currentRelativeChild.BoundAxis(
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
                            deltaFreeSpace       += boundMainSize - childFlexBasis;
                            TotalFlexGrowFactors -= flexGrowFactor;
                        }
                    }
                }
            }

            RemainingFreeSpace -= deltaFreeSpace;
        }

        // It distributes the free space to the flexible items and ensures that the size
        // of the flex items abide the min and max constraints. At the end of this
        // function the child nodes would have proper size. Prior using this function
        // please ensure that YGDistributeFreeSpaceFirstPass is called.

        private float YGDistributeFreeSpaceSecondPass(
            YogaNode            node,
            FlexDirectionType mainAxis,
            FlexDirectionType crossAxis,
            float             mainAxisOwnerSize,
            float             availableInnerMainDim,
            float             availableInnerCrossDim,
            float             availableInnerWidth,
            float             availableInnerHeight,
            bool              flexBasisOverflows,
            MeasureMode       measureModeCrossDim,
            bool              performLayout,
            YogaConfig        config)
        {
            float childFlexBasis         = 0;
            float flexShrinkScaledFactor = 0;
            float flexGrowFactor         = 0;
            float deltaFreeSpace         = 0;
            var   isMainAxisRow          = mainAxis.IsRow();
            var   isNodeFlexWrap         = node.Style.FlexWrap != WrapType.NoWrap;

            foreach (var currentRelativeChild in RelativeChildren)
            {
                childFlexBasis = 
                    currentRelativeChild.BoundAxisWithinMinAndMax(
                        mainAxis,
                        currentRelativeChild.Layout.ComputedFlexBasis.Unwrap(),
                        mainAxisOwnerSize);
                var updatedMainSize = childFlexBasis;

                if (RemainingFreeSpace.HasValue() && RemainingFreeSpace < 0)
                {
                    flexShrinkScaledFactor = -currentRelativeChild.ResolveFlexShrink() * childFlexBasis;
                    // Is this child able to shrink?
                    if (flexShrinkScaledFactor != 0f)
                    {
                        float childSize;

                        if (TotalFlexShrinkScaledFactors.HasValue() && TotalFlexShrinkScaledFactors == 0f)
                            childSize = childFlexBasis + flexShrinkScaledFactor;
                        else
                            childSize = childFlexBasis + RemainingFreeSpace / TotalFlexShrinkScaledFactors * flexShrinkScaledFactor;

                        updatedMainSize = currentRelativeChild.BoundAxis(
                            mainAxis,
                            childSize,
                            availableInnerMainDim,
                            availableInnerWidth);
                    }
                }
                else if (RemainingFreeSpace.HasValue() && RemainingFreeSpace > 0)
                {
                    flexGrowFactor = currentRelativeChild.ResolveFlexGrow();

                    // Is this child able to grow?
                    if (flexGrowFactor.HasValue() && flexGrowFactor != 0)
                        updatedMainSize = currentRelativeChild.BoundAxis(
                            mainAxis,
                            childFlexBasis + RemainingFreeSpace / TotalFlexGrowFactors * flexGrowFactor,
                            availableInnerMainDim,
                            availableInnerWidth);
                }

                deltaFreeSpace += updatedMainSize - childFlexBasis;

                var marginMain  = currentRelativeChild.GetMarginForAxis(mainAxis,  availableInnerWidth);
                var marginCross = currentRelativeChild.GetMarginForAxis(crossAxis, availableInnerWidth);

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
                    availableInnerCrossDim.HasValue()                                                &&
                    !currentRelativeChild.IsStyleDimensionDefined(crossAxis, availableInnerCrossDim) &&
                    measureModeCrossDim == MeasureMode.Exactly                                       &&
                    !(isNodeFlexWrap && flexBasisOverflows)                                          &&
                    node.AlignChild(currentRelativeChild) == AlignType.Stretch                    &&
                    currentRelativeChild.MarginLeadingValue(crossAxis).Unit !=
                    ValueUnit.Auto &&
                    currentRelativeChild.MarginTrailingValue(crossAxis).Unit !=
                    ValueUnit.Auto)
                {
                    childCrossSize        = availableInnerCrossDim;
                    childCrossMeasureMode = MeasureMode.Exactly;
                }
                else if (!currentRelativeChild.IsStyleDimensionDefined(crossAxis, availableInnerCrossDim))
                {
                    childCrossSize = availableInnerCrossDim;
                    childCrossMeasureMode = childCrossSize.IsNaN()
                        ? MeasureMode.Undefined
                        : MeasureMode.AtMost;
                }
                else
                {
                    childCrossSize =
                        currentRelativeChild.ResolvedDimension[crossAxis.ToDimension()].ResolveValue(availableInnerCrossDim) + marginCross;
                    var isLoosePercentageMeasurement =
                        currentRelativeChild.ResolvedDimension[crossAxis.ToDimension()].Unit == ValueUnit.Percent &&
                        measureModeCrossDim                                                  != MeasureMode.Exactly;
                    childCrossMeasureMode =
                        childCrossSize.IsNaN() || isLoosePercentageMeasurement
                            ? MeasureMode.Undefined
                            : MeasureMode.Exactly;
                }

                currentRelativeChild.Calc.ConstrainMaxSizeForMode(
                    mainAxis,
                    availableInnerMainDim,
                    availableInnerWidth,
                    ref childMainMeasureMode,
                    ref childMainSize);
                currentRelativeChild.Calc.ConstrainMaxSizeForMode(
                    crossAxis,
                    availableInnerCrossDim,
                    availableInnerWidth,
                    ref childCrossMeasureMode,
                    ref childCrossSize);

                var requiresStretchLayout =
                    !currentRelativeChild.IsStyleDimensionDefined(crossAxis, availableInnerCrossDim) &&
                    node.AlignChild(currentRelativeChild) == AlignType.Stretch                    &&
                    currentRelativeChild.MarginLeadingValue(crossAxis).Unit !=
                    ValueUnit.Auto &&
                    currentRelativeChild.MarginTrailingValue(crossAxis).Unit != ValueUnit.Auto;

                var childWidth  = isMainAxisRow ? childMainSize : childCrossSize;
                var childHeight = !isMainAxisRow ? childMainSize : childCrossSize;

                var childWidthMeasureMode  = isMainAxisRow ? childMainMeasureMode : childCrossMeasureMode;
                var childHeightMeasureMode = !isMainAxisRow ? childMainMeasureMode : childCrossMeasureMode;

                // Recursively call the layout algorithm for this child with the updated main size.
                currentRelativeChild.Calc.LayoutInternal(
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
        internal void YGResolveFlexibleLength(
            YogaNode            node,
            FlexDirectionType mainAxis,
            FlexDirectionType crossAxis,
            float             mainAxisownerSize,
            float             availableInnerMainDim,
            float             availableInnerCrossDim,
            float             availableInnerWidth,
            float             availableInnerHeight,
            bool              flexBasisOverflows,
            MeasureMode       measureModeCrossDim,
            bool              performLayout,
            YogaConfig        config)
        {
            var originalFreeSpace = RemainingFreeSpace;
            // First pass: detect the flex items whose min/max constraints trigger
            YGDistributeFreeSpaceFirstPass(
                mainAxis,
                mainAxisownerSize,
                availableInnerMainDim,
                availableInnerWidth);

            // Second pass: resolve the sizes of the flexible items
            var distributedFreeSpace = YGDistributeFreeSpaceSecondPass(
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

            RemainingFreeSpace = originalFreeSpace - distributedFreeSpace;
        }

        internal void YGJustifyMainAxis(
            YogaNode            node,
            int               startOfLineIndex,
            FlexDirectionType mainAxis,
            FlexDirectionType crossAxis,
            MeasureMode       measureModeMainDim,
            MeasureMode       measureModeCrossDim,
            float             mainAxisOwnerSize,
            float             ownerWidth,
            float             availableInnerMainDim,
            float             availableInnerCrossDim,
            float             availableInnerWidth,
            bool              performLayout)
        {
            var style                        = node.Style;
            var leadingPaddingAndBorderMain  = node.GetLeadingPaddingAndBorder(mainAxis, ownerWidth);
            var trailingPaddingAndBorderMain = node.GetTrailingPaddingAndBorder(mainAxis, ownerWidth);
            // If we are using "at most" rules in the main axis, make sure that
            // remainingFreeSpace is 0 when min main dimension is not given
            if (measureModeMainDim == MeasureMode.AtMost && RemainingFreeSpace > 0)
            {
                if (style.MinDimension(mainAxis.ToDimension()).Unit != ValueUnit.Undefined &&
                    style.MinDimension(mainAxis.ToDimension()).ResolveValue(mainAxisOwnerSize).HasValue())
                {
                    // This condition makes sure that if the size of main dimension(after
                    // considering child nodes main dim, leading and trailing padding etc)
                    // falls below min dimension, then the remainingFreeSpace is reassigned
                    // considering the min dimension

                    // `minAvailableMainDim` denotes minimum available space in which child
                    // can be laid out, it will exclude space consumed by padding and border.
                    var minAvailableMainDim = 
                        style.MinDimension(mainAxis.ToDimension()).ResolveValue(mainAxisOwnerSize) 
                        - leadingPaddingAndBorderMain 
                        - trailingPaddingAndBorderMain;
                    var occupiedSpaceByChildNodes = availableInnerMainDim - RemainingFreeSpace;
                    RemainingFreeSpace = FloatMax(0, minAvailableMainDim - occupiedSpaceByChildNodes);
                }
                else
                {
                    RemainingFreeSpace = 0;
                }
            }

            var numberOfAutoMarginsOnCurrentLine = 0;
            for (var i = startOfLineIndex; i < EndOfLineIndex; i++)
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
                    leadingMainDim = RemainingFreeSpace / 2;
                    break;
                case JustifyType.FlexEnd:
                    leadingMainDim = RemainingFreeSpace;
                    break;
                case JustifyType.SpaceBetween:
                    if (ItemsOnLine > 1)
                        betweenMainDim = FloatMax(RemainingFreeSpace, 0) /
                            (ItemsOnLine - 1);
                    else
                        betweenMainDim = 0;

                    break;
                case JustifyType.SpaceEvenly:
                    // Space is distributed evenly across all elements
                    betweenMainDim = RemainingFreeSpace / (ItemsOnLine + 1);
                    leadingMainDim = betweenMainDim;
                    break;
                case JustifyType.SpaceAround:
                    // Space on the edges is half of the space between elements
                    betweenMainDim = RemainingFreeSpace / ItemsOnLine;
                    leadingMainDim = betweenMainDim     / 2;
                    break;
                case JustifyType.FlexStart:
                    break;
                }

            MainDim  = leadingPaddingAndBorderMain + leadingMainDim;
            CrossDim = 0;

            float maxAscentForCurrentLine  = 0;
            float maxDescentForCurrentLine = 0;
            var   isNodeBaselineLayout     = node.IsBaselineLayout();
            for (var i = startOfLineIndex; i < EndOfLineIndex; i++)
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
                            child.GetLeadingPosition(mainAxis, availableInnerMainDim) 
                            + node.GetLeadingBorder(mainAxis)                                
                            + child.GetLeadingMargin(mainAxis, availableInnerWidth);
                }
                else
                {
                    // Now that we placed the element, we need to update the variables.
                    // We need to do that only for relative elements. Absolute elements
                    // do not take part in that phase.
                    if (childStyle.PositionType == PositionType.Relative)
                    {
                        if (child.MarginLeadingValue(mainAxis).Unit == ValueUnit.Auto)
                            MainDim += RemainingFreeSpace / numberOfAutoMarginsOnCurrentLine;

                        if (performLayout)
                            child.Layout.Position[mainAxis.ToPositionEdge()] =
                                childLayout.Position[mainAxis.ToPositionEdge()] + MainDim;

                        if (child.MarginTrailingValue(mainAxis).Unit == ValueUnit.Auto)
                            MainDim += RemainingFreeSpace / numberOfAutoMarginsOnCurrentLine;

                        var canSkipFlex =
                            !performLayout && measureModeCrossDim == MeasureMode.Exactly;
                        if (canSkipFlex)
                        {
                            // If we skipped the flex step, then we can't rely on the
                            // measuredDims because
                            // they weren't computed. This means we can't call
                            // YGNodeDimWithMargin.
                            MainDim += betweenMainDim 
                                + child.GetMarginForAxis(mainAxis, availableInnerWidth) 
                                + childLayout.ComputedFlexBasis.Unwrap();
                            CrossDim = availableInnerCrossDim;
                        }
                        else
                        {
                            // The main dimension is the sum of all the elements dimension plus
                            // the spacing.
                            MainDim += betweenMainDim + child.DimensionWithMargin(mainAxis, availableInnerWidth);

                            if (isNodeBaselineLayout)
                            {
                                // If the child is baseline aligned then the cross dimension is
                                // calculated by adding maxAscent and maxDescent from the baseline.
                                var ascent = child.Baseline() +
                                    child.GetLeadingMargin(FlexDirectionType.Column, availableInnerWidth);
                                var descent =
                                    child.Layout.MeasuredHeight 
                                    + child.GetMarginForAxis(FlexDirectionType.Column, availableInnerWidth) 
                                    - ascent;

                                maxAscentForCurrentLine  = FloatMax(maxAscentForCurrentLine,  ascent);
                                maxDescentForCurrentLine = FloatMax(maxDescentForCurrentLine, descent);
                            }
                            else
                            {
                                // The cross dimension is the max of the elements dimension since
                                // there can only be one element in that cross dimension in the case
                                // when the items are not baseline aligned
                                CrossDim = FloatMax(CrossDim, child.DimensionWithMargin(crossAxis, availableInnerWidth));
                            }
                        }
                    }
                    else if (performLayout)
                    {
                        child.Layout.Position[mainAxis.ToPositionEdge()] =
                            childLayout.Position[mainAxis.ToPositionEdge()] 
                            + node.GetLeadingBorder(mainAxis)                 
                            + leadingMainDim;
                    }
                }
            }

            MainDim += trailingPaddingAndBorderMain;

            if (isNodeBaselineLayout)
                CrossDim = maxAscentForCurrentLine + maxDescentForCurrentLine;
        }
    }
}
