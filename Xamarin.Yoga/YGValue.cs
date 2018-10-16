using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Xamarin.Yoga
{
    [DebuggerDisplay("{value} {unit}")]
    public class YGValue : IEquatable<YGValue>
    {
        public readonly float  value;
        public readonly YGUnit unit;

        public YGValue()
        {
            value = float.NaN;
            unit  = YGUnit.Undefined;
        }

        public YGValue(YGValue value)
        {
            this.value = value.value;
            unit       = value.unit;
        }

        public YGValue(float value, YGUnit unit)
        {
            this.value = value;
            this.unit  = unit;
        }

        public bool IsNaN() => value.IsNaN();

        public static YGValue Sanitized(float value, YGUnit unit)
        {
            return new YGValue(
                value.IsNaN() ? 0 : value,
                value.IsNaN() ? YGUnit.Undefined : unit);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (obj is float f)
            {
                if (YGGlobal.FloatEqual(value, f) && unit == YGUnit.Point)
                    return true;
            }

            if (obj is YGValue other)
                return Equals(other);

            return false;
        }

        /// <inheritdoc />
        public bool Equals(YGValue other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return value.Equals(other.value) && unit == other.unit;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (value.GetHashCode() * 397) ^ (int) unit;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            switch (unit)
            {
            case YGUnit.Auto:
                return $"{value}: auto";
            case YGUnit.Percent:
                return $"{value}%";
            case YGUnit.Point:
                return $"{value}pt";
            case YGUnit.Undefined:
            default:
                return string.Empty;
            }
        }

        public static bool operator ==(YGValue value1, YGValue value2)
        {
            return EqualityComparer<YGValue>.Default.Equals(value1, value2);
        }

        public static bool operator !=(YGValue value1, YGValue value2)
        {
            return !(value1 == value2);
        }

        public static implicit operator YGValue(float value)
        {
            return new YGValue(value, YGUnit.Point);
        }
    }
}
