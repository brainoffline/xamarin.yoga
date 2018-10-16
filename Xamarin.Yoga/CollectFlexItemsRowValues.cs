using System.Collections.Generic;
using System.Diagnostics;

namespace Xamarin.Yoga
{
    [DebuggerDisplay(
        "items:{ItemsOnLine} sizeConsumed:{SizeConsumedOnCurrentLine} TFG:{TotalFlexGrowFactors} TFS:{TotalFlexShrinkScaledFactors} EOL:{EndOfLineIndex} RFS:{RemainingFreeSpace} Main:{MainDim} Cross:{CrossDim}")]
    internal class CollectFlexItemsRowValues
    {
        public uint         ItemsOnLine;
        public float        SizeConsumedOnCurrentLine;
        public float        TotalFlexGrowFactors;
        public float        TotalFlexShrinkScaledFactors;
        public int          EndOfLineIndex;
        public List<YGNode> RelativeChildren;
        public float        RemainingFreeSpace;

        // The size of the mainDim for the row after considering size, padding, margin
        // and border of flex items. This is used to calculate maxLineDim after going
        // through all the rows to decide on the main axis size of owner.
        public float MainDim;

        // The size of the crossDim for the row after considering size, padding,
        // margin and border of flex items. Used for calculating containers crossSize.
        public float CrossDim;
    };
}
