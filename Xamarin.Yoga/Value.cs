using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Xamarin.Yoga
{
    using static NumberExtensions;

    [DebuggerDisplay("{Number} {Unit}")]
    public class Value : IEquatable<Value>
    {
        public static readonly Value     Auto = new Value(float.NaN, ValueUnit.Auto);
        public readonly        float     Number;
        public readonly        ValueUnit Unit;

        public Value()
        {
            Number = float.NaN;
            Unit   = ValueUnit.Undefined;
        }

        public Value(Value value)
        {
            Number = value.Number;
            Unit   = value.Unit;
        }

        public Value(float value, ValueUnit unit)
        {
            Number = value;
            Unit   = unit;
        }

        /// <inheritdoc />
        public bool Equals(Value other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return Number.Equals(other.Number) && Unit == other.Unit;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (obj is float f)
                if (FloatEqual(Number, f) && Unit == ValueUnit.Point)
                    return true;

            if (obj is Value other)
                return Equals(other);

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Number.GetHashCode() * 397) ^ (int) Unit;
            }
        }

        public bool IsNaN()
        {
            return Number.IsNaN();
        }

        public static bool operator ==(Value value1, Value value2)
        {
            return EqualityComparer<Value>.Default.Equals(value1, value2);
        }

        public static implicit operator Value(float value)
        {
            return new Value(value, ValueUnit.Point);
        }

        public static bool operator !=(Value value1, Value value2)
        {
            return !(value1 == value2);
        }

        public static Value Percent(float value)
        {
            return new Value(value, ValueUnit.Percent);
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
                return Number;
            case ValueUnit.Percent:
                return Number * ownerSize * 0.01f;
            }

            return float.NaN;
        }

        public static Value Sanitized(float value, ValueUnit unit)
        {
            return new Value(
                value.IsNaN() ? 0 : value,
                value.IsNaN() ? ValueUnit.Undefined : unit);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            switch (Unit)
            {
            case ValueUnit.Auto:
                return $"{Number}: auto";
            case ValueUnit.Percent:
                return $"{Number}%";
            case ValueUnit.Point:
                return $"{Number}pt";
            case ValueUnit.Undefined:
            default:
                return string.Empty;
            }
        }
    }
}
