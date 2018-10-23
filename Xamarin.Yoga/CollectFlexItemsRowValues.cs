﻿using System.Collections.Generic;
using System.Diagnostics;

namespace Xamarin.Yoga
{
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
        public List<YGNode> RelativeChildren;
        public float        RemainingFreeSpace;
        public float        SizeConsumedOnCurrentLine;
        public float        TotalFlexGrowFactors;
        public float        TotalFlexShrinkScaledFactors;
    }
}
