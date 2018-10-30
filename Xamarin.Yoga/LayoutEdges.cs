using System;

namespace Xamarin.Yoga
{
    public class LayoutEdges : IEquatable<LayoutEdges>
    {
        public float Bottom { get; set; }
        public float End    { get; set; }

        public float this[EdgeType key]
        {
            get
            {
                switch (key)
                {
                case EdgeType.Left:   return Left;
                case EdgeType.Top:    return Top;
                case EdgeType.Right:  return Right;
                case EdgeType.Bottom: return Bottom;
                case EdgeType.Start:  return Start;
                case EdgeType.End:    return End;
                default:
                    return float.NaN;
                }
            }
            set
            {
                switch (key)
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
                case EdgeType.Start:
                    Start = value;
                    break;
                case EdgeType.End:
                    End = value;
                    break;
                }
            }
        }

        public float Left  { get; set; }
        public float Right { get; set; }
        public float Start { get; set; }
        public float Top   { get; set; }

        /// <inheritdoc />
        public bool Equals(LayoutEdges other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return NumberExtensions.FloatEqual(Left, other.Left)   &&
                NumberExtensions.FloatEqual(Top,     other.Top)    &&
                NumberExtensions.FloatEqual(Right,   other.Right)  &&
                NumberExtensions.FloatEqual(Bottom,  other.Bottom) &&
                NumberExtensions.FloatEqual(Start,   other.Start)  &&
                NumberExtensions.FloatEqual(End,     other.End);
        }

        public LayoutEdges Clone()
        {
            return new LayoutEdges
            {
                Left   = Left,
                Top    = Top,
                Right  = Right,
                Bottom = Bottom,
                Start  = Start,
                End    = End
            };
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LayoutEdges) obj);
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
                hashCode = (hashCode * 397) ^ Start.GetHashCode();
                hashCode = (hashCode * 397) ^ End.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(LayoutEdges left, LayoutEdges right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LayoutEdges left, LayoutEdges right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"<{Left}, ^{Top}, >{Right}, v{Bottom}  <{Start}={End}>";
        }
    }
}
