using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga
{
    public static class NumberExtensions
    {
        public static bool IsUndefined(this float value)
        {
            // Value of a float in the case of it being not defined is 10.1E20. Earlier
            // it used to be NAN, the benefit of which was that if NAN is involved in any
            // mathematical expression the result was NAN. But since we want to have
            // `-ffast-math` flag being used by compiler which assumes that the floating
            // point values are not NAN and Inf, we represent YGUndefined as 10.1E20. But
            // now if YGUndefined is involved in any mathematical operations this
            // value(10.1E20) would change. So the following check makes sure that if the
            // value is outside a range (-10E8, 10E8) then it is undefined.
            return value >= 10E8F || value <= -10E8F;
        }

        public static bool HasValue(this float value)
        {
            return !value.IsUndefined();
        }

        public static bool IsUndefined(this float? value)
        {
            if (!value.HasValue)
                return true;
            return value >= 10E8F || value <= -10E8F;
        }

        public static bool HasValue(this float? value)
        {
            return !value.IsUndefined();
        }

    }
}
