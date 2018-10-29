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

            return NumberExtensions.FloatEqual(a.Value, b.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float ResolveValueMargin(YGValue value, float ownerSize)
        {
            return value.Unit == ValueUnit.Auto
                ? 0
                : value.ResolveValue(ownerSize);
        }
    }
}
