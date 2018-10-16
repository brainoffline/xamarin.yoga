using System.Collections.Generic;

namespace Xamarin.Yoga
{
    public class YGCachedMeasurement
    {
        public float         AvailableWidth    { get; set; }
        public float         AvailableHeight   { get; set; }
        public YGMeasureMode WidthMeasureMode  { get; set; } = YGMeasureMode.NotSet;
        public YGMeasureMode HeightMeasureMode { get; set; } = YGMeasureMode.NotSet;
        public float         ComputedWidth     { get; set; } = -1;
        public float         ComputedHeight    { get; set; } = -1;

        public YGCachedMeasurement Clone()
        {
            return (YGCachedMeasurement) MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (obj is YGCachedMeasurement other)
                return Equals(other);
            return false;
        }

        public bool Equals(YGCachedMeasurement other)
        {
            return other          != null                                   &&
                WidthMeasureMode  == other.WidthMeasureMode                 &&
                HeightMeasureMode == other.HeightMeasureMode                &&
                YGGlobal.FloatEqual(AvailableWidth,  other.AvailableWidth)  &&
                YGGlobal.FloatEqual(AvailableHeight, other.AvailableHeight) &&
                YGGlobal.FloatEqual(ComputedWidth,   other.ComputedWidth)   &&
                YGGlobal.FloatEqual(ComputedHeight,  other.ComputedHeight);
        }

        public override int GetHashCode()
        {
            var hashCode = 838407653;
            hashCode = hashCode * -1521134295 + AvailableWidth.GetHashCode();
            hashCode = hashCode * -1521134295 + AvailableHeight.GetHashCode();
            hashCode = hashCode * -1521134295 + WidthMeasureMode.GetHashCode();
            hashCode = hashCode * -1521134295 + HeightMeasureMode.GetHashCode();
            hashCode = hashCode * -1521134295 + ComputedWidth.GetHashCode();
            hashCode = hashCode * -1521134295 + ComputedHeight.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(YGCachedMeasurement measurement1, YGCachedMeasurement measurement2)
        {
            return EqualityComparer<YGCachedMeasurement>.Default.Equals(measurement1, measurement2);
        }

        public static bool operator !=(YGCachedMeasurement measurement1, YGCachedMeasurement measurement2)
        {
            return !(measurement1 == measurement2);
        }
    }
}
