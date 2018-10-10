using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga
{
    public static class YGConst
    {
        public static readonly YGValue YGValueUndefined = new YGValue (0, YGUnit.Undefined );
        public static readonly YGValue YGValueAuto = new YGValue(0, YGUnit.Auto);
        public static readonly YGValue YGValueZero = new YGValue(0, YGUnit.Point);

        public static readonly YGValue[] kYGDefaultDimensionValuesAutoUnit = { YGValueAuto, YGValueAuto };
        public static readonly YGValue[] kYGDefaultDimensionValuesUnit     = { YGValueUndefined, YGValueUndefined };

        // This value was chosen based on empiracle data. Even the most complicated
        // layouts should not require more than 16 entries to fit within the cache.
        public const int YG_MAX_CACHED_RESULT_COUNT = 16;

        public const float kDefaultFlexGrow      = 0.0f;
        public const float kDefaultFlexShrink    = 0.0f;
        public const float kWebDefaultFlexShrink = 1.0f;

        /** Large positive number signifies that the property(float) is undefined.
         *Earlier we used to have YGundefined as NAN, but the downside of this is that
         *we can't use -ffast-math compiler flag as it assumes all floating-point
         *calculation involve and result into finite numbers. For more information
         *regarding -ffast-math compiler flag in clang, have a look at
         *https://clang.llvm.org/docs/UsersManual.html#cmdoption-ffast-math
         **/
        public const float YGUndefined = 10E20F;

    }
}
