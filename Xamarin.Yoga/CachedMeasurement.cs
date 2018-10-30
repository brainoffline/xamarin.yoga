using System;
using System.Collections.Generic;

namespace Xamarin.Yoga
{
    public class CachedMeasurement : IEquatable<CachedMeasurement>
    {
        public float       AvailableHeight   { get; set; }
        public float       AvailableWidth    { get; set; }
        public float       ComputedHeight    { get; set; } = -1;
        public float       ComputedWidth     { get; set; } = -1;
        public MeasureMode HeightMeasureMode { get; set; } = MeasureMode.NotSet;
        public MeasureMode WidthMeasureMode  { get; set; } = MeasureMode.NotSet;

        /// <inheritdoc />
        public bool Equals(CachedMeasurement other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                AvailableHeight.Equals(other.AvailableHeight) &&
                AvailableWidth.Equals(other.AvailableWidth)   &&
                ComputedHeight.Equals(other.ComputedHeight)   &&
                ComputedWidth.Equals(other.ComputedWidth)     &&
                HeightMeasureMode == other.HeightMeasureMode  &&
                WidthMeasureMode  == other.WidthMeasureMode;
        }

        public CachedMeasurement Clone()
        {
            return (CachedMeasurement) MemberwiseClone();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CachedMeasurement) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AvailableHeight.GetHashCode();
                hashCode = (hashCode * 397) ^ AvailableWidth.GetHashCode();
                hashCode = (hashCode * 397) ^ ComputedHeight.GetHashCode();
                hashCode = (hashCode * 397) ^ ComputedWidth.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) HeightMeasureMode;
                hashCode = (hashCode * 397) ^ (int) WidthMeasureMode;
                return hashCode;
            }
        }

        public static bool operator ==(CachedMeasurement measurement1, CachedMeasurement measurement2)
        {
            return EqualityComparer<CachedMeasurement>.Default.Equals(measurement1, measurement2);
        }

        public static bool operator !=(CachedMeasurement measurement1, CachedMeasurement measurement2)
        {
            return !(measurement1 == measurement2);
        }
    }
}
