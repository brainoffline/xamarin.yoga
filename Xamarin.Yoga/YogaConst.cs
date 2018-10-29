namespace Xamarin.Yoga
{
    public static class YogaConst
    {
        public const           float   DefaultFlexGrow      = 0.0f;
        public const           float   DefaultFlexShrink    = 0.0f;
        public const           float   WebDefaultFlexShrink = 1.0f;
        public static readonly Value ValueAuto          = new Value(float.NaN, ValueUnit.Auto);
        public static readonly Value ValueUndefined     = new Value(float.NaN, ValueUnit.Undefined);
        public static readonly Value ValueZero          = new Value(0,         ValueUnit.Point);
    }
}
