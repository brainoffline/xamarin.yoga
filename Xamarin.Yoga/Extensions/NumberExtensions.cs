using System;

namespace Xamarin.Yoga
{
    public static class NumberExtensions
    {
        public static bool HasValue(this float value)
        {
            return !Single.IsNaN(value);
        }

        public static bool HasValue(this float? value)
        {
            return value.HasValue && Single.IsNaN(value.Value);
        }

        public static bool IsNaN(this float value)
        {
            return Single.IsNaN(value);
        }

        public static bool IsNaN(this float? value)
        {
            return !value.HasValue || Single.IsNaN(value.Value);
        }

        public static YGValue Percent(this int value)
        {
            return new YGValue(value, ValueUnit.Percent);
        }

        public static YGValue Percent(this float value)
        {
            return new YGValue(value, ValueUnit.Percent);
        }

        public static float RoundValueToPixelGrid(
            float value,
            float pointScaleFactor,
            bool  forceCeil,
            bool  forceFloor)
        {
            var scaledValue = value       * pointScaleFactor;
            var fraction    = scaledValue % 1.0f;
            if (FloatEqual(fraction, 0))
                scaledValue = scaledValue - fraction;
            else if (FloatEqual(fraction, 1.0f))
                scaledValue = scaledValue - fraction + 1.0f;
            else if (forceCeil)
                scaledValue = scaledValue - fraction + 1.0f;
            else if (forceFloor)
                scaledValue = scaledValue - fraction;
            else
                scaledValue = scaledValue - fraction +
                    (fraction.HasValue() &&
                        (fraction > 0.5f || FloatEqual(fraction, 0.5f))
                            ? 1.0f
                            : 0.0f);

            return scaledValue.IsNaN() || pointScaleFactor.IsNaN()
                ? Single.NaN
                : scaledValue / pointScaleFactor;
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
        // float.NaN then the max function should return the other argument as the max value. 

        public static float FloatMax(float a, float b)
        {
            if (a.HasValue() && b.HasValue())
                return Math.Max(a, b);

            return a.HasValue() ? a : b;
        }

        // We need custom min function, since we want that, if one argument is
        // float.NaN then the min function should return the other argument as the min value. 

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
    }
}
