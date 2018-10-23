using System;
using System.Collections.Generic;

namespace Xamarin.Yoga
{
    public class Dimensions : IEquatable<Dimensions>
    {
        public Dimensions(YGValue width, YGValue height)
        {
            Width  = width;
            Height = height;
        }

        public YGValue Height { get; internal set; }

        public YGValue this[DimensionType key]
        {
            get => key == DimensionType.Width ? Width : Height;
            set
            {
                if (key == DimensionType.Width)
                    Width = value;
                else
                    Height = value;
            }
        }

        public YGValue Width { get; internal set; }

        /// <inheritdoc />
        public bool Equals(Dimensions other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Width, other.Width) && Equals(Height, other.Height);
        }

        public Dimensions Clone()
        {
            return new Dimensions(Width, Height);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Dimensions) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return 
                    ((Width != null ? Width.GetHashCode() : 0) * 397) 
                    ^ (Height != null ? Height.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Dimensions dimensions1, Dimensions dimensions2)
        {
            return EqualityComparer<Dimensions>.Default.Equals(dimensions1, dimensions2);
        }

        public static bool operator !=(Dimensions dimensions1, Dimensions dimensions2)
        {
            return !(dimensions1 == dimensions2);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[width:{Width} height:{Height}]";
        }
    }
}
