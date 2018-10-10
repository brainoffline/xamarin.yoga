using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;
    using static YGGlobal;
    using static YGConst;

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

    public struct YGCollectFlexItemsRowValues
    {
        public uint itemsOnLine;
        public float sizeConsumedOnCurrentLine;
        public float totalFlexGrowFactors;
        public float totalFlexShrinkScaledFactors;
        public int endOfLineIndex;
        public List<YGNodeRef> relativeChildren;
        public float remainingFreeSpace;

        // The size of the mainDim for the row after considering size, padding, margin
        // and border of flex items. This is used to calculate maxLineDim after going
        // through all the rows to decide on the main axis size of owner.
        public float mainDim;

        // The size of the crossDim for the row after considering size, padding,
        // margin and border of flex items. Used for calculating containers crossSize.
        public float crossDim;
    };

    public static partial class YGGlobal
    {
        // This custom float comparision function compares the array of float with
        // YGFloatsEqual, as the default float comparision operator will not work(Look
        // at the comments of YGFloatsEqual function).
        public static bool YGFloatArrayEqual(
            in float[] val1,
            in float[] val2)
        {
            if (val1 == null ||
                val2 == null ||
                val1.Length != val2.Length)
                return false;

            var areEqual = true;
            for (var i = 0; i < val1.Length && areEqual; ++i) areEqual = YGFloatsEqual(val1[i], val2[i]);

            return areEqual;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool YGFlexDirectionIsRow(in YGFlexDirection flexDirection)
        {
            return flexDirection == YGFlexDirection.Row ||
                flexDirection == YGFlexDirection.RowReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static YGFloatOptional YGResolveValue(in YGValue value, in float ownerSize)
        {
            switch (value.unit)
            {
                case YGUnit.Undefined:
                case YGUnit.Auto:
                    return new YGFloatOptional();
                case YGUnit.Point:
                    return new YGFloatOptional(value.value);
                case YGUnit.Percent:
                    return new YGFloatOptional(value.value * ownerSize * 0.01f);
            }

            return new YGFloatOptional();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool YGFlexDirectionIsColumn(in YGFlexDirection flexDirection)
        {
            return flexDirection == YGFlexDirection.Column ||
                flexDirection == YGFlexDirection.ColumnReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static YGFlexDirection YGResolveFlexDirection(
            in YGFlexDirection flexDirection,
            in YGDirection direction)
        {
            if (direction == YGDirection.RTL)
            {
                if (flexDirection == YGFlexDirection.Row) return YGFlexDirection.RowReverse;
                if (flexDirection == YGFlexDirection.RowReverse) return YGFlexDirection.Row;
            }
            return flexDirection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static YGFloatOptional YGResolveValueMargin(
            in YGValue value,
            in float ownerSize)
        {
            return value.unit == YGUnit.Auto
                ? new YGFloatOptional(0)
                : YGResolveValue(value, ownerSize);
        }

        public static YGFlexDirection YGFlexDirectionCross(
            in YGFlexDirection flexDirection,
            in YGDirection direction)
        {
            return YGFlexDirectionIsColumn(flexDirection)
                ? YGResolveFlexDirection(YGFlexDirection.Row, direction)
                : YGFlexDirection.Column;
        }

        // We need custom max function, since we want that, if one argument is
        // YGUndefined then the max funtion should return the other argument as the max
        // value. We wouldn't have needed a custom max function if YGUndefined was NAN
        // as fmax has the same behaviour, but with NAN we cannot use `-ffast-math`
        // compiler flag.
        public static float YGFloatMax(in float a, in float b)
        {
            if (!isUndefined(a) && !isUndefined(b)) return Math.Max(a, b);

            return isUndefined(a) ? b : a;
        }

        // We need custom min function, since we want that, if one argument is
        // YGUndefined then the min funtion should return the other argument as the min
        // value. We wouldn't have needed a custom min function if YGUndefined was NAN
        // as fmin has the same behaviour, but with NAN we cannot use `-ffast-math`
        // compiler flag.
        public static float YGFloatMin(in float a, in float b)
        {
            if (!isUndefined(a) && !isUndefined(b)) return Math.Min(a, b);

            return isUndefined(a) ? b : a;
        }

        public static bool YGValueEqual(in YGValue a, in YGValue b)
        {
            if (a.unit != b.unit) return false;

            if (a.unit == YGUnit.Undefined ||
                isUndefined(a.value) && isUndefined(b.value))
                return true;

            return Math.Abs(a.value - b.value) < 0.0001f;
        }

        // This custom float equality function returns true if either absolute
        // difference between two floats is less than 0.0001f or both are undefined.
        public static bool YGFloatsEqual(in float a, in float b)
        {
            if (!isUndefined(a) && !isUndefined(b)) return Math.Abs(a - b) < 0.0001f;

            return isUndefined(a) && isUndefined(b);
        }

        // This function returns 0 if YGFloatIsUndefined(val) is true and val otherwise
        public static float YGFloatSanitize(in float val)
        {
            return isUndefined(val) ? 0 : val;
        }

        // This function unwraps optional and returns YGUndefined if not defined or
        // op.value otherwise
        // TODO: Get rid off this function
        public static float YGUnwrapFloatOptional(in YGFloatOptional op)
        {
            return op.isUndefined() ? YGUndefined : op.getValue();
        }

        public static YGFloatOptional YGFloatOptionalMax(
            in YGFloatOptional op1,
            in YGFloatOptional op2)
        {
            if (!op1.isUndefined() && !op2.isUndefined()) return op1.getValue() > op2.getValue() ? op1 : op2;

            return op1.isUndefined() ? op2 : op1;
        }
    }
}

