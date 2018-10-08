using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga
{
    public static class YGConst
    {
        public static readonly YGValue YGValueUndefined = new YGValue { unit = YGUnit.Undefined };
        public static readonly YGValue YGValueAuto = new YGValue { unit = YGUnit.Auto };
        public static readonly YGValue YGValueZero = new YGValue { unit = YGUnit.Point, value = 0 };

        public static readonly YGValue[] kYGDefaultDimensionValuesAutoUnit = { YGValueAuto, YGValueAuto };
        public static readonly YGValue[] kYGDefaultDimensionValuesUnit     = { YGValueUndefined, YGValueUndefined };

        // This value was chosen based on empiracle data. Even the most complicated
        // layouts should not require more than 16 entries to fit within the cache.
        public const int YG_MAX_CACHED_RESULT_COUNT = 16;

        public const float kDefaultFlexGrow      = 0.0f;
        public const float kDefaultFlexShrink    = 0.0f;
        public const float kWebDefaultFlexShrink = 1.0f;
    }
}
