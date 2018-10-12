using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga
{
    using static YGGlobal;
    using static YGConst;

    public class YGLayout : IEquatable<YGLayout>
    {
        public float[]     position   = new float[4];
        public float[]     dimensions = new float[2];
        public float[]     margin     = new float[6];
        public float[]     border     = new float[6];
        public float[]     padding    = new float[6];
        public YGDirection direction;

        public int             computedFlexBasisGeneration;
        public YGFloatOptional computedFlexBasis;
        public bool            hadOverflow;

        // Instead of recomputing the entire layout every single time, we
        // cache some information to break early when nothing changed
        public int         generationCount;
        public YGDirection lastOwnerDirection;

        public int nextCachedMeasurementsIndex;

        public YGCachedMeasurement[] cachedMeasurements = new YGCachedMeasurement[YG_MAX_CACHED_RESULT_COUNT];
        public float[]               measuredDimensions = new float[2];

        public YGCachedMeasurement cachedLayout;
        public bool                didUseLegacyFlag;
        public bool                doesLegacyStretchFlagAffectsLayout;

        public YGLayout()
        {
            dimensions                  = (float[]) kYGDefaultDimensionValues.Clone();
            direction                   = YGDirection.Inherit;
            computedFlexBasisGeneration = 0;
            computedFlexBasis           = new YGFloatOptional();
            hadOverflow                 = false;
            generationCount             = 0;

            lastOwnerDirection                 = YGDirection.Inherit;
            nextCachedMeasurementsIndex        = 0;
            measuredDimensions                 = (float[]) kYGDefaultDimensionValues.Clone();
            cachedLayout                       = new YGCachedMeasurement();
            didUseLegacyFlag                   = false;
            doesLegacyStretchFlagAffectsLayout = false;
        }

        /// <inheritdoc />
        public bool Equals(YGLayout other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                Equals(position,   other.position)                               &&
                Equals(dimensions, other.dimensions)                             &&
                Equals(margin,     other.margin)                                 &&
                Equals(border,     other.border)                                 &&
                Equals(padding,    other.padding)                                &&
                direction                   == other.direction                   &&
                computedFlexBasisGeneration == other.computedFlexBasisGeneration &&
                computedFlexBasis.Equals(other.computedFlexBasis)                &&
                hadOverflow                 == other.hadOverflow                 &&
                generationCount             == other.generationCount             &&
                lastOwnerDirection          == other.lastOwnerDirection          &&
                nextCachedMeasurementsIndex == other.nextCachedMeasurementsIndex &&
                Equals(cachedMeasurements, other.cachedMeasurements)             &&
                Equals(measuredDimensions, other.measuredDimensions)             &&
                Equals(cachedLayout,       other.cachedLayout)                   &&
                didUseLegacyFlag                   == other.didUseLegacyFlag     &&
                doesLegacyStretchFlagAffectsLayout == other.doesLegacyStretchFlagAffectsLayout;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((YGLayout) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (position != null ? position.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (dimensions != null ? dimensions.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (margin     != null ? margin.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (border     != null ? border.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (padding    != null ? padding.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) direction;
                hashCode = (hashCode * 397) ^ computedFlexBasisGeneration;
                hashCode = (hashCode * 397) ^ computedFlexBasis.GetHashCode();
                hashCode = (hashCode * 397) ^ hadOverflow.GetHashCode();
                hashCode = (hashCode * 397) ^ generationCount;
                hashCode = (hashCode * 397) ^ (int) lastOwnerDirection;
                hashCode = (hashCode * 397) ^ nextCachedMeasurementsIndex;
                hashCode = (hashCode * 397) ^ (cachedMeasurements != null ? cachedMeasurements.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (measuredDimensions != null ? measuredDimensions.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (cachedLayout       != null ? cachedLayout.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ didUseLegacyFlag.GetHashCode();
                hashCode = (hashCode * 397) ^ doesLegacyStretchFlagAffectsLayout.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(YGLayout left, YGLayout right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(YGLayout left, YGLayout right)
        {
            return !Equals(left, right);
        }

        /*
        public static bool operator==(YGLayout op, YGLayout layout)
        {
            var isEqual = 
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

            for (var i = 0; i < YG_MAX_CACHED_RESULT_COUNT && isEqual; ++i) isEqual = isEqual && op.cachedMeasurements[i] == layout.cachedMeasurements[i];

            if (!isUndefined(op.measuredDimensions[0]) ||
                !isUndefined(layout.measuredDimensions[0]))
                isEqual = isEqual && op.measuredDimensions[0] == layout.measuredDimensions[0];
            if (!isUndefined(op.measuredDimensions[1]) ||
                !isUndefined(layout.measuredDimensions[1]))
                isEqual = isEqual && op.measuredDimensions[1] == layout.measuredDimensions[1];
            return isEqual;
        }

        public static bool operator !=(YGLayout op, YGLayout layout)
        {
            return !(op == layout);
        }
        */
    }
}
