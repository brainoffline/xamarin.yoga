using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;
    using static YGGlobal;

    /*
    WIN_EXPORT float YGRoundValueToPixelGrid(
        const float value,
        const float pointScaleFactor,
        const bool forceCeil,
        const bool forceFloor);
    */

    public static partial class YGGlobal
    {
        public static bool isUndefined(float value)
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

        //extern const std::array<YGEdge,  4> trailing;
        //extern const std::array<YGEdge,  4> leading;
        //extern       bool               YGValueEqual(const YGValue a,  const YGValue b);

        public static bool YGValueArrayEqual(
            in YGValue[] val1, 
            in YGValue[] val2)
        {
            if (val1 == null || val2 == null || val1.Length != val2.Length)
                return false;

            var areEqual = true;
            for (var i = 0; i < val1.Length && areEqual; ++i)
                areEqual = YGValueEqual(val1[i], val2[i]);

            return areEqual;
        }

        // This value was chosen based on empiracle data. Even the most complicated
        // layouts should not require more than 16 entries to fit within the cache.
        public const int YG_MAX_CACHED_RESULT_COUNT = 16;

        public const float kDefaultFlexGrow      = 0.0f;
        public const float kDefaultFlexShrink    = 0.0f;
        public const float kWebDefaultFlexShrink = 1.0f;
    }

    public class YGCachedMeasurement
    {
        public float         availableWidth;
        public float         availableHeight;
        public YGMeasureMode widthMeasureMode;
        public YGMeasureMode heightMeasureMode;

        public float computedWidth;
        public float computedHeight;

        public YGCachedMeasurement()
        {
            availableWidth = 0;
            availableHeight = 0;
            widthMeasureMode = YGMeasureMode.Undefined;
            heightMeasureMode = YGMeasureMode.Undefined;
            computedWidth = -1;
            computedHeight = -1;
        }

        public static bool operator ==(YGCachedMeasurement op, YGCachedMeasurement measurement)
        {
            var isEqual = op.widthMeasureMode == measurement.widthMeasureMode &&
                op.heightMeasureMode           == measurement.heightMeasureMode;

            if (!isUndefined(op.availableWidth) ||
                !isUndefined(measurement.availableWidth))
                isEqual = isEqual && YGFloatsEqual(op.availableWidth, measurement.availableWidth);

            if (!isUndefined(op.availableHeight) ||
                !isUndefined(measurement.availableHeight))
                isEqual = isEqual && YGFloatsEqual(op.availableHeight, measurement.availableHeight);

            if (!isUndefined(op.computedWidth) ||
                !isUndefined(measurement.computedWidth))
                isEqual = isEqual && YGFloatsEqual(op.computedWidth, measurement.computedWidth);

            if (!isUndefined(op.computedHeight) ||
                !isUndefined(measurement.computedHeight))
                isEqual = isEqual && YGFloatsEqual(op.computedHeight, measurement.computedHeight);

            return isEqual;
        }

        public static bool operator !=(YGCachedMeasurement op, YGCachedMeasurement measurement)
        {
            return !(op == measurement);
        }
    }

}
