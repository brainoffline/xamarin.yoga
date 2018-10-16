using System;

namespace Xamarin.Yoga
{
    using static YGGlobal;

    public class LayoutEdges : IEquatable<LayoutEdges>
    {
        public float Left   { get; set; }
        public float Top    { get; set; }
        public float Right  { get; set; }
        public float Bottom { get; set; }
        public float Start  { get; set; }
        public float End    { get; set; }

        public float this[YGEdge key]
        {
            get
            {
                switch (key)
                {
                case YGEdge.Left:   return Left;
                case YGEdge.Top:    return Top;
                case YGEdge.Right:  return Right;
                case YGEdge.Bottom: return Bottom;
                case YGEdge.Start:  return Start;
                case YGEdge.End:    return End;
                default:
                    return YGConst.YGUndefined;
                }
            }
            set
            {
                switch (key)
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
                case YGEdge.Start:
                    Start = value;
                    break;
                case YGEdge.End:
                    End = value;
                    break;
                }
            }
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
                End    = End,
            };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"<{Left}, ^{Top}, >{Right}, v{Bottom}  <{Start}={End}>";
        }

        /// <inheritdoc />
        public bool Equals(LayoutEdges other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return YGFloatsEqual(Left, other.Left) && 
                YGFloatsEqual(Top, other.Top) && 
                YGFloatsEqual(Right, other.Right) && 
                YGFloatsEqual(Bottom, other.Bottom) && 
                YGFloatsEqual(Start, other.Start) &&
                YGFloatsEqual(End, other.End);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
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
    }
}