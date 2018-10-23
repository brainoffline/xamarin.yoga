namespace Xamarin.Yoga
{
    public static class NumberExtensions
    {
        public static bool HasValue(this float value)
        {
            return !float.IsNaN(value);
        }

        public static bool HasValue(this float? value)
        {
            return value.HasValue && float.IsNaN(value.Value);
        }

        public static bool IsNaN(this float value)
        {
            return float.IsNaN(value);
        }

        public static bool IsNaN(this float? value)
        {
            return !value.HasValue || float.IsNaN(value.Value);
        }

        public static YGValue Percent(this int value)
        {
            return new YGValue(value, ValueUnit.Percent);
        }

        public static YGValue Percent(this float value)
        {
            return new YGValue(value, ValueUnit.Percent);
        }
    }
}
