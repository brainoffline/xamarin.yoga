using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Xamarin.Yoga
{
    [DebuggerDisplay("{Value} {Unit}")]
    public class YGValue : IEquatable<YGValue>
    {
        public static readonly YGValue   Auto = new YGValue(float.NaN, ValueUnit.Auto);
        public readonly        ValueUnit Unit;
        public readonly        float     Value;

        public YGValue()
        {
            Value = float.NaN;
            Unit  = ValueUnit.Undefined;
        }

        public YGValue(YGValue value)
        {
            Value = value.Value;
            Unit  = value.Unit;
        }

        public YGValue(float value, ValueUnit unit)
        {
            Value = value;
            Unit  = unit;
        }

        /// <inheritdoc />
        public bool Equals(YGValue other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return Value.Equals(other.Value) && Unit == other.Unit;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (obj is float f)
                if (NumberExtensions.FloatEqual(Value, f) && Unit == ValueUnit.Point)
                    return true;

            if (obj is YGValue other)
                return Equals(other);

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ (int) Unit;
            }
        }

        public bool IsNaN()
        {
            return Value.IsNaN();
        }

        public static bool operator ==(YGValue value1, YGValue value2)
        {
            return EqualityComparer<YGValue>.Default.Equals(value1, value2);
        }

        public static implicit operator YGValue(float value)
        {
            return new YGValue(value, ValueUnit.Point);
        }

        public static bool operator !=(YGValue value1, YGValue value2)
        {
            return !(value1 == value2);
        }

        public static YGValue Percent(float value)
        {
            return new YGValue(value, ValueUnit.Percent);
        }

        public static YGValue Sanitized(float value, ValueUnit unit)
        {
            return new YGValue(
                value.IsNaN() ? 0 : value,
                value.IsNaN() ? ValueUnit.Undefined : unit);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            switch (Unit)
            {
            case ValueUnit.Auto:
                return $"{Value}: auto";
            case ValueUnit.Percent:
                return $"{Value}%";
            case ValueUnit.Point:
                return $"{Value}pt";
            case ValueUnit.Undefined:
            default:
                return string.Empty;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ResolveValue(float ownerSize)
        {
            switch (Unit)
            {
            case ValueUnit.Undefined:
            case ValueUnit.Auto:
                return float.NaN;
            case ValueUnit.Point:
                return Value;
            case ValueUnit.Percent:
                return Value * ownerSize * 0.01f;
            }

            return float.NaN;
        }

    }
}
