﻿namespace Xamarin.Yoga
{
    public static class NumberExtensions
    {
        public static bool HasValue(this float value) => !float.IsNaN(value);
        public static bool IsNaN(this    float value) => float.IsNaN(value);

        public static bool HasValue(this float? value) => value.HasValue && float.IsNaN(value.Value);
        public static bool IsNaN(this    float? value) => !value.HasValue || float.IsNaN(value.Value);

        public static YGValue Percent(this int value) => new YGValue(value, YGUnit.Percent);
        public static YGValue Percent(this float value) => new YGValue(value, YGUnit.Percent);
    }
}
