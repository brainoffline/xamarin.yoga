using System;
using System.Runtime.CompilerServices;

// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    public static partial class YGGlobal
    {
        public static FlexDirectionType FlexDirectionCross(FlexDirectionType flexDirection, DirectionType direction)
        {
            return flexDirection.IsColumn()
                ? ResolveFlexDirection(FlexDirectionType.Row, direction)
                : FlexDirectionType.Column;
        }

        // This custom float equality function returns true if either absolute
        // difference between two floats is less than 0.0001f or both are undefined.
        public static bool FloatEqual(float a, float b)
        {
            if (a.IsNaN() && b.IsNaN()) return true;
            if (a.IsNaN() || b.IsNaN()) return false;
            return Math.Abs(a - b) < 0.0001f;
        }

        // We need custom max function, since we want that, if one argument is
        // float.NaN then the max funtion should return the other argument as the max
        // value. We wouldn't have needed a custom max function if float.NaN was NAN
        // as fmax has the same behaviour, but with NAN we cannot use `-ffast-math`
        // compiler flag.
        public static float FloatMax(float a, float b)
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
        public static float FloatMin(float a, float b)
        {
            if (a.HasValue() && b.HasValue())
                return Math.Min(a, b);

            return a.HasValue() ? a : b;
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

        public static float? FloatOptionalMax(float? op1, float? op2)
        {
            if (op1.HasValue && op2.HasValue)
                return op1 > op2 ? op1 : op2;

            return op1 ?? op2;
        }


        // This function returns 0 if YGFloatIsUndefined(val) is true and val otherwise
        public static float FloatSanitize(float val)
        {
            return val.IsNaN() ? 0 : val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FlexDirectionType ResolveFlexDirection(FlexDirectionType flexDirection, DirectionType direction)
        {
            if (direction == DirectionType.RTL)
            {
                if (flexDirection == FlexDirectionType.Row) return FlexDirectionType.RowReverse;
                if (flexDirection == FlexDirectionType.RowReverse) return FlexDirectionType.Row;
            }

            return flexDirection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float? ResolveValue(YGValue value, float ownerSize)
        {
            switch (value.Unit)
            {
            case ValueUnit.Undefined:
            case ValueUnit.Auto:
                return null;
            case ValueUnit.Point:
                return value.Value;
            case ValueUnit.Percent:
                return value.Value * ownerSize * 0.01f;
            }

            return null;
        }

        // This function unwraps optional and returns float.NaN if not defined or
        // op.value otherwise
        // TODO: Get rid off this function
        public static float UnwrapFloatOptional(float? op)
        {
            return op ?? float.NaN;
        }

        public static bool YGValueEqual(YGValue a, YGValue b)
        {
            if (a.Unit != b.Unit)
                return false;

            if (a.Unit == ValueUnit.Undefined || a.Value.IsNaN() && b.Value.IsNaN())
                return true;

            return FloatEqual(a.Value, b.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float? ResolveValueMargin(YGValue value, float ownerSize)
        {
            return value.Unit == ValueUnit.Auto
                ? 0
                : ResolveValue(value, ownerSize);
        }
    }
}
