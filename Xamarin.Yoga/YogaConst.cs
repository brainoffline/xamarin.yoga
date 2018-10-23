using System.Drawing;

namespace Xamarin.Yoga
{
    public delegate SizeF MeasureFunc(
        YGNode      node,
        float       width,
        MeasureMode widthMode,
        float       height,
        MeasureMode heightMode);

    public delegate float BaselineFunc(YGNode node, float width, float height);

    public delegate void DirtiedFunc(YGNode node);

    public delegate void PrintFunc(YGNode node);

    public delegate void LoggerFunc(
        YogaConfig      config,
        YGNode          node,
        LogLevel        level,
        string          format,
        params object[] args);

    public static class YogaConst
    {
        public const           float   DefaultFlexGrow      = 0.0f;
        public const           float   DefaultFlexShrink    = 0.0f;
        public const           float   WebDefaultFlexShrink = 1.0f;
        public static readonly YGValue YGValueAuto          = new YGValue(float.NaN, ValueUnit.Auto);
        public static readonly YGValue YGValueUndefined     = new YGValue(float.NaN, ValueUnit.Undefined);
        public static readonly YGValue YGValueZero          = new YGValue(0,         ValueUnit.Point);
    }
}
