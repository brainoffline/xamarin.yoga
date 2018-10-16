namespace Xamarin.Yoga
{
    public static class YGConst
    {
        public static readonly YGValue YGValueUndefined = new YGValue(float.NaN, YGUnit.Undefined);
        public static readonly YGValue YGValueAuto      = new YGValue(float.NaN, YGUnit.Auto);
        public static readonly YGValue YGValueZero      = new YGValue(0,         YGUnit.Point);

        public const float kDefaultFlexGrow      = 0.0f;
        public const float kDefaultFlexShrink    = 0.0f;
        public const float kWebDefaultFlexShrink = 1.0f;
    }
}
