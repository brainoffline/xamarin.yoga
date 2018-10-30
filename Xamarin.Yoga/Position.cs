using System;

namespace Xamarin.Yoga
{
    public class Position : IEquatable<Position>
    {
        public float Bottom { get; set; }

        public float this[EdgeType edge]
        {
            get
            {
                switch (edge)
                {
                case EdgeType.Left:   return Left;
                case EdgeType.Top:    return Top;
                case EdgeType.Right:  return Right;
                case EdgeType.Bottom: return Bottom;
                default:              return float.NaN;
                }
            }
            set
            {
                switch (edge)
                {
                case EdgeType.Left:
                    Left = value;
                    break;
                case EdgeType.Top:
                    Top = value;
                    break;
                case EdgeType.Right:
                    Right = value;
                    break;
                case EdgeType.Bottom:
                    Bottom = value;
                    break;
                }
            }
        }

        public float Left  { get; set; }
        public float Right { get; set; }
        public float Top   { get; set; }

        /// <inheritdoc />
        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                Left.Equals(other.Left)   &&
                Top.Equals(other.Top)     &&
                Right.Equals(other.Right) &&
                Bottom.Equals(other.Bottom);
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
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
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
