using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga
{
    using static YGGlobal;

    public static partial class YGGlobal
    {
        public static float[] kYGDefaultDimensionValues = { YGUndefined, YGUndefined };
    }

    public class YGLayout
    {
        public float[] position = new float[4];
        public float[] dimensions = new float[2];
        public float[] margin = new float[6];
        public float[] border = new float[6];
        public float[] padding = new float[6];
        public YGDirection direction;

        public int        computedFlexBasisGeneration;
        public YGFloatOptional computedFlexBasis;
        public bool            hadOverflow;

        // Instead of recomputing the entire layout every single time, we
        // cache some information to break early when nothing changed
        public int    generationCount;
        public YGDirection lastOwnerDirection;

        public int nextCachedMeasurementsIndex;

        public YGCachedMeasurement[] cachedMeasurements = new YGCachedMeasurement[YG_MAX_CACHED_RESULT_COUNT];
        public float[] measuredDimensions = new float[2];

        public YGCachedMeasurement cachedLayout;
        public bool                didUseLegacyFlag;
        public bool                doesLegacyStretchFlagAffectsLayout;

        public YGLayout()
        {
            dimensions = kYGDefaultDimensionValues;
            direction = YGDirection.Inherit;
            computedFlexBasisGeneration = 0;
            computedFlexBasis = new YGFloatOptional();
            hadOverflow = false;
            generationCount = 0;

            lastOwnerDirection = YGDirection.Inherit;
            nextCachedMeasurementsIndex = 0;
            measuredDimensions = kYGDefaultDimensionValues;
            cachedLayout = new YGCachedMeasurement();
            didUseLegacyFlag = false;
            doesLegacyStretchFlagAffectsLayout = false;
        }

        public static bool operator==(YGLayout op, YGLayout layout)
        {
            bool isEqual = 
                YGFloatArrayEqual(op.position, layout.position) &&
                YGFloatArrayEqual(op.dimensions, layout.dimensions) &&
                YGFloatArrayEqual(op.margin, layout.margin) &&
                YGFloatArrayEqual(op.border, layout.border) &&
                YGFloatArrayEqual(op.padding, layout.padding) &&
                op.direction == layout.direction && 
                op.hadOverflow == layout.hadOverflow &&
                op.lastOwnerDirection == layout.lastOwnerDirection &&
                op.nextCachedMeasurementsIndex == layout.nextCachedMeasurementsIndex &&
                op.cachedLayout == layout.cachedLayout &&
                op.computedFlexBasis == layout.computedFlexBasis;

            for (int i = 0; i < YG_MAX_CACHED_RESULT_COUNT && isEqual; ++i)
            {
                isEqual = isEqual && op.cachedMeasurements[i] == layout.cachedMeasurements[i];
            }

            if (!isUndefined(op.measuredDimensions[0]) ||
                !isUndefined(layout.measuredDimensions[0]))
            {
                isEqual = isEqual && (op.measuredDimensions[0] == layout.measuredDimensions[0]);
            }
            if (!isUndefined(op.measuredDimensions[1]) ||
                !isUndefined(layout.measuredDimensions[1]))
            {
                isEqual = isEqual && (op.measuredDimensions[1] == layout.measuredDimensions[1]);
            }
            return isEqual;
        }

        public static bool operator !=(YGLayout op, YGLayout layout)
        {
            return !(op == layout);
        }
    }

}
