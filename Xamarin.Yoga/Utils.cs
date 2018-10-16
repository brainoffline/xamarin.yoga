using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable InconsistentNaming

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

    public static partial class YGGlobal
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool YGFlexDirectionIsRow(YGFlexDirection flexDirection)
        {
            return
                flexDirection == YGFlexDirection.Row ||
                flexDirection == YGFlexDirection.RowReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool YGFlexDirectionIsColumn(in YGFlexDirection flexDirection)
        {
            return
                flexDirection == YGFlexDirection.Column ||
                flexDirection == YGFlexDirection.ColumnReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float? YGResolveValue(in YGValue value, in float ownerSize)
        {
            switch (value.unit)
            {
            case YGUnit.Undefined:
            case YGUnit.Auto:
                return null;
            case YGUnit.Point:
                return value.value;
            case YGUnit.Percent:
                return value.value * ownerSize * 0.01f;
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static YGFlexDirection YGResolveFlexDirection(
            in YGFlexDirection flexDirection,
            in YGDirection     direction)
        {
            if (direction == YGDirection.RTL)
            {
                if (flexDirection == YGFlexDirection.Row) return YGFlexDirection.RowReverse;
                if (flexDirection == YGFlexDirection.RowReverse) return YGFlexDirection.Row;
            }

            return flexDirection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float? YGResolveValueMargin(YGValue value, float ownerSize)
        {
            return value.unit == YGUnit.Auto
                ? 0
                : YGResolveValue(value, ownerSize);
        }

        public static YGFlexDirection YGFlexDirectionCross(YGFlexDirection flexDirection, YGDirection direction)
        {
            return YGFlexDirectionIsColumn(flexDirection)
                ? YGResolveFlexDirection(YGFlexDirection.Row, direction)
                : YGFlexDirection.Column;
        }

        // We need custom max function, since we want that, if one argument is
        // float.NaN then the max funtion should return the other argument as the max
        // value. We wouldn't have needed a custom max function if float.NaN was NAN
        // as fmax has the same behaviour, but with NAN we cannot use `-ffast-math`
        // compiler flag.
        public static float YGFloatMax(float a, float b)
        {
            if (a.HasValue() && b.HasValue())
                return Math.Max(a, b);

            return a.HasValue() ? a : b;
        }

        // We need custom min function, since we want that, if one argument is
        // float.NaN then the min funtion should return the other argument as the min
        // value. We wouldn't have needed a custom min function if float.NaN was NAN
        // as fmin has the same behaviour, but with NAN we cannot use `-ffast-math`
        // compiler flag.
        public static float YGFloatMin(in float a, in float b)
        {
            if (a.HasValue() && b.HasValue())
                return Math.Min(a, b);

            return a.HasValue() ? a : b;
        }

        public static bool YGValueEqual(YGValue a, YGValue b)
        {
            if (a.unit != b.unit)
                return false;

            if (a.unit == YGUnit.Undefined || a.value.IsNaN() && b.value.IsNaN())
                return true;

            return FloatEqual(a.value, b.value);
        }

        // This custom float equality function returns true if either absolute
        // difference between two floats is less than 0.0001f or both are undefined.
        public static bool FloatEqual(float a, float b)
        {
            if (a.IsNaN() && b.IsNaN()) return true;
            if (a.IsNaN() || b.IsNaN()) return false;
            return Math.Abs(a - b) < 0.0001f;
        }

        // This custom float equality function returns true if either absolute
        // difference between two floats is less than 0.0001f or both are undefined.
        public static bool FloatOptionalEqual(float? a, float? b)
        {
            if (a.IsNaN() && b.IsNaN()) return true;
            if (a.IsNaN() || b.IsNaN()) return false;

            // ReSharper disable PossibleInvalidOperationException
            return Math.Abs(a.Value - b.Value) < 0.0001f;
            // ReSharper restore PossibleInvalidOperationException
        }


        // This function returns 0 if YGFloatIsUndefined(val) is true and val otherwise
        public static float YGFloatSanitize(float val)
        {
            return val.IsNaN() ? 0 : val;
        }

        // This function unwraps optional and returns float.NaN if not defined or
        // op.value otherwise
        // TODO: Get rid off this function
        public static float YGUnwrapFloatOptional(float? op)
        {
            return op ?? float.NaN;
        }

        public static float? FloatOptionalMax(
            in float? op1,
            in float? op2)
        {
            if (op1.HasValue && op2.HasValue)
                return op1 > op2 ? op1 : op2;

            return op1 ?? op2;
        }
    }
}
