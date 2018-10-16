using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Yoga
{
    public class Dimensions : IEquatable<Dimensions>
    {
        public YGValue Width  { get; set; }
        public YGValue Height { get; set; }

        public Dimensions(YGValue width, YGValue height)
        {
            Width  = width;
            Height = height;
        }

        public Dimensions Clone()
        {
            return new Dimensions(Width, Height);
        }

        /*
        public static implicit operator Dimensions(YGValue[] values)
        {
            return new Dimensions(values);
        }
        */

        public static bool operator ==(Dimensions dimensions1, Dimensions dimensions2)
        {
            return EqualityComparer<Dimensions>.Default.Equals(dimensions1, dimensions2);
        }

        public static bool operator !=(Dimensions dimensions1, Dimensions dimensions2)
        {
            return !(dimensions1 == dimensions2);
        }

        public YGValue this[YGDimension key]
        {
            get => key == YGDimension.Width ? Width : Height;
            set
            {
                if (key == YGDimension.Width)
                    Width = value;
                else
                    Height = value;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[width:{Width} height:{Height}]";
        }

        /// <inheritdoc />
        public bool Equals(Dimensions other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Width, other.Width) && Equals(Height, other.Height);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Dimensions) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Width != null ? Width.GetHashCode() : 0) * 397) ^ (Height != null ? Height.GetHashCode() : 0);
            }
        }
    }
}
