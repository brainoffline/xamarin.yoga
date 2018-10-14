using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    using static YGGlobal;

    public class YGFloatOptional
    {
        public static readonly YGFloatOptional Empty = new YGFloatOptional();

        private float? value;

        public YGFloatOptional()
        {
            value = null;
        }

        public YGFloatOptional(float value)
        {
            this.value = value;
        }

        public float getValue()
        {
            return value ?? 0;
        }

        public void setValue(float val)
        {
            value = val;
        }

        public YGFloatOptional Clone()
        {
            return new YGFloatOptional { value = this.value };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (value.HasValue)
                return value.ToString();
            return string.Empty;
        }


        // Sets the value of float optional, and thus isUndefined is assigned false.
        //public void setValue(float val) => value = val;

        public bool isUndefined()
        {
            return !value.HasValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (obj is YGFloatOptional optional)
            {
                return EqualityComparer<float?>.Default.Equals(value, optional.value);
            }

            if (obj is float f)
            {
                if (!value.HasValue)
                    return false;
                return EqualityComparer<float?>.Default.Equals(value, f);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return -1584136870 + EqualityComparer<float?>.Default.GetHashCode(value);
        }

        public static YGFloatOptional operator +(in YGFloatOptional left, in YGFloatOptional other)
        {
            if (left.value.HasValue && other.value.HasValue)
                return new YGFloatOptional(left.value.Value + other.value.Value);
            return new YGFloatOptional();
        }

        public static bool operator >(in YGFloatOptional op, in YGFloatOptional other)
        {
            if (!op.value.HasValue || !other.value.HasValue)
                return false;
            return op.value.Value > other.value.Value;
        }

        public static bool operator <(YGFloatOptional op, YGFloatOptional other)
        {
            if (!op.value.HasValue || !other.value.HasValue)
                return false;
            return op.value.Value < other.value.Value;
        }

        public static bool operator >=(in YGFloatOptional op, in YGFloatOptional other)
        {
            if (!op.value.HasValue || !other.value.HasValue)
                return false;
            return op.value.Value >= other.value.Value;
        }

        public static bool operator <=(YGFloatOptional op, YGFloatOptional other)
        {
            if (!op.value.HasValue || !other.value.HasValue)
                return false;
            return op.value.Value <= other.value.Value;
        }

        public static bool operator ==(YGFloatOptional op, YGFloatOptional other)
        {
            return op.Equals(other);
        }

        public static bool operator !=(YGFloatOptional op, YGFloatOptional other)
        {
            return !(op == other);
        }

        public static bool operator ==(YGFloatOptional op, float other)
        {
            return op.Equals(other);
        }

        public static bool operator !=(YGFloatOptional op, float other)
        {
            return !(op == other);
        }

    }
}
