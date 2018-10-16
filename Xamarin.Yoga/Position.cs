using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga
{
    using static YGGlobal;
    using static YGConst;

    public class Position : IEquatable<Position>
    {
        public float Left   { get; set; }
        public float Top    { get; set; }
        public float Right  { get; set; }
        public float Bottom { get; set; }

        public float this[YGEdge edge]
        {
            get
            {
                switch (edge)
                {
                case YGEdge.Left:   return Left;
                case YGEdge.Top:    return Top;
                case YGEdge.Right:  return Right;
                case YGEdge.Bottom: return Bottom;
                default:            return YGUndefined;
                }
            }
            set
            {
                switch (edge)
                {
                case YGEdge.Left:
                    Left = value;
                    break;
                case YGEdge.Top:
                    Top = value;
                    break;
                case YGEdge.Right:
                    Right = value;
                    break;
                case YGEdge.Bottom:
                    Bottom = value;
                    break;
                }
            }
        }

        public Position Clone()
        {
            return new Position
            {
                Left   = Left,
                Top    = Top,
                Right  = Right,
                Bottom = Bottom
            };
        }

        /// <inheritdoc />
        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                Left.Equals(other.Left) && 
                Top.Equals(other.Top) && 
                Right.Equals(other.Right) && 
                Bottom.Equals(other.Bottom);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Left.GetHashCode();
                hashCode = (hashCode * 397) ^ Top.GetHashCode();
                hashCode = (hashCode * 397) ^ Right.GetHashCode();
                hashCode = (hashCode * 397) ^ Bottom.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Position left, Position right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !Equals(left, right);
        }
    }
}
