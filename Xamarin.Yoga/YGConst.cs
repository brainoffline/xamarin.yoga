using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga
{
    public static class YGConst
    {
        /** Large positive number signifies that the property(float) is undefined.
         *Earlier we used to have YGundefined as NAN, but the downside of this is that
         *we can't use -ffast-math compiler flag as it assumes all floating-point
         *calculation involve and result into finite numbers. For more information
         *regarding -ffast-math compiler flag in clang, have a look at
         *https://clang.llvm.org/docs/UsersManual.html#cmdoption-ffast-math
         **/
        public const float YGUndefined = 10E20F;

        public static readonly YGValue YGValueUndefined = new YGValue (YGUndefined, YGUnit.Undefined );
        public static readonly YGValue YGValueAuto = new YGValue(YGUndefined, YGUnit.Auto);
        public static readonly YGValue YGValueZero = new YGValue(0, YGUnit.Point);

        public const float kDefaultFlexGrow      = 0.0f;
        public const float kDefaultFlexShrink    = 0.0f;
        public const float kWebDefaultFlexShrink = 1.0f;
    }
}
