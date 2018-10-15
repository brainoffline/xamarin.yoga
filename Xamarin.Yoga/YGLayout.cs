using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xamarin.Yoga
{
    using static YGGlobal;
    using static YGConst;

    public class YGLayout : IEquatable<YGLayout>
    {
        // This value was chosen based on empiracle data. Even the most complicated
        // layouts should not require more than 16 entries to fit within the cache.
        private const int MaxCachedResultCount = 16;

        public float       Width              { get; private set; } = YGUndefined;
        public float       Height             { get; private set; } = YGUndefined;
        public float       MeasuredWidth      { get; private set; } = YGUndefined;
        public float       MeasuredHeight     { get; private set; } = YGUndefined;
        public YGDirection Direction          { get; set; }
        public YGDirection LastOwnerDirection { get; set; }
        public bool        HadOverflow        { get; set; }

        // Instead of recomputing the entire layout every single time, we
        // cache some information to break early when nothing changed
        public          int                   GenerationCount             { get; set; }
        public          int                   NextCachedMeasurementsIndex { get; private set; }
        public readonly YGCachedMeasurement[] CachedMeasurements = new YGCachedMeasurement[MaxCachedResultCount];
        public          YGCachedMeasurement   CachedLayout                       { get; } = new YGCachedMeasurement();
        public          int                   ComputedFlexBasisGeneration        { get; set; }
        public          float?       ComputedFlexBasis                  { get; set; }

        public YGPosition Position { get; } = new YGPosition();
        public float[] margin   = new float[6];
        public float[] border   = new float[6];
        public float[] padding  = new float[6];


        public void SetDimension(YGDimension dim, float value)
        {
            if (dim == YGDimension.Width)
                Width = value;
            else
                Height = value;
        }

        public float GetMeasuredDimension(YGDimension dim)
        {
            return dim == YGDimension.Width ? MeasuredWidth : MeasuredHeight;
        }

        public void SetMeasuredDimension(YGDimension dim, float value)
        {
            if (dim == YGDimension.Width)
                MeasuredWidth = value;
            else
                MeasuredHeight = value;
        }

        public void InvalidateCache()
        {
            NextCachedMeasurementsIndex    = 0;
            CachedLayout.widthMeasureMode  = YGMeasureMode.NotSet;
            CachedLayout.heightMeasureMode = YGMeasureMode.NotSet;
            CachedLayout.computedWidth     = -1;
            CachedLayout.computedHeight    = -1;
        }

        public YGCachedMeasurement GetNextCachedMeasurement()
        {
            var cache = CachedMeasurements[NextCachedMeasurementsIndex];
            if (cache == null)
                cache = CachedMeasurements[NextCachedMeasurementsIndex] = new YGCachedMeasurement();
            NextCachedMeasurementsIndex++;
            return cache;
        }

        public void ResetNextCachedMeasurement()
        {
            NextCachedMeasurementsIndex = 0;
        }

        public bool CachedMeasurementFull => NextCachedMeasurementsIndex >= MaxCachedResultCount;

        public YGLayout()
        {
            Direction                   = YGDirection.Inherit;
            ComputedFlexBasisGeneration = 0;
            ComputedFlexBasis           = null;
            HadOverflow                 = false;
            GenerationCount             = 0;

            LastOwnerDirection                 = YGDirection.NotSet;
            NextCachedMeasurementsIndex        = 0;

            for (var i = 0; i < MaxCachedResultCount; i++) CachedMeasurements[i] = new YGCachedMeasurement();
        }

        public YGLayout(YGLayout other)
        {
            Width                       = other.Width;
            Height                      = other.Height;
            MeasuredWidth               = other.MeasuredWidth;
            MeasuredHeight              = other.MeasuredHeight;
            Position                    = other.Position.Clone();
            margin                      = (float[]) other.margin.Clone();
            border                      = (float[]) other.border.Clone();
            padding                     = (float[]) other.padding.Clone();
            Direction                   = other.Direction;
            ComputedFlexBasisGeneration = other.ComputedFlexBasisGeneration;
            ComputedFlexBasis           = other.ComputedFlexBasis;
            HadOverflow                 = other.HadOverflow;

            LastOwnerDirection                 = other.LastOwnerDirection;
            NextCachedMeasurementsIndex        = other.NextCachedMeasurementsIndex;
            CachedLayout                       = other.CachedLayout.Clone();

            for (var i = 0; i < MaxCachedResultCount; i++)
                CachedMeasurements[i] = other.CachedMeasurements[i].Clone();
        }

        /// <inheritdoc />
        public bool Equals(YGLayout other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;


            var isEqual =
                Position == other.Position                      &&
                YGFloatArrayEqual(margin,   other.margin)                        &&
                YGFloatArrayEqual(border,   other.border)                        &&
                YGFloatArrayEqual(padding,  other.padding)                       &&
                YGFloatsEqual(Width,  other.Width)                               &&
                YGFloatsEqual(Height, other.Height)                              &&
                Direction                   == other.Direction                   &&
                HadOverflow                 == other.HadOverflow                 &&
                LastOwnerDirection          == other.LastOwnerDirection          &&
                NextCachedMeasurementsIndex == other.NextCachedMeasurementsIndex &&
                CachedLayout                == other.CachedLayout                &&
                ComputedFlexBasis           == other.ComputedFlexBasis;

            for (var i = 0; i < MaxCachedResultCount && isEqual; ++i)
                isEqual = isEqual && CachedMeasurements[i] == other.CachedMeasurements[i];

            if (MeasuredWidth.HasValue() || other.MeasuredWidth.HasValue())
                isEqual = isEqual && YGFloatsEqual(MeasuredWidth, other.MeasuredWidth);
            if (MeasuredHeight.HasValue() || other.MeasuredHeight.HasValue())
                isEqual = isEqual && YGFloatsEqual(MeasuredHeight, other.MeasuredHeight);
            return isEqual;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is YGLayout layout)
                return Equals(layout);
            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Position != null ? Position.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (int) Width;
                hashCode = (hashCode * 397) ^ (int) Height;
                hashCode = (hashCode * 397) ^ (int) MeasuredWidth;
                hashCode = (hashCode * 397) ^ (int) MeasuredHeight;
                hashCode = (hashCode * 397) ^ (margin  != null ? margin.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (border  != null ? border.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (padding != null ? padding.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Direction;
                hashCode = (hashCode * 397) ^ ComputedFlexBasisGeneration;
                hashCode = (hashCode * 397) ^ ComputedFlexBasis.GetHashCode();
                hashCode = (hashCode * 397) ^ HadOverflow.GetHashCode();
                hashCode = (hashCode * 397) ^ GenerationCount;
                hashCode = (hashCode * 397) ^ (int) LastOwnerDirection;
                hashCode = (hashCode * 397) ^ NextCachedMeasurementsIndex;
                hashCode = (hashCode * 397) ^ (CachedMeasurements != null ? CachedMeasurements.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CachedLayout       != null ? CachedLayout.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(YGLayout left, YGLayout right)
        {
            if ((object) left == null || (object) right == null)
                return ReferenceEquals(left, right);
            return Equals(left, right);
        }

        public static bool operator !=(YGLayout left, YGLayout right)
        {
            return !Equals(left, right);
        }
    }
}
