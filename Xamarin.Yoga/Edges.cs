using System;

// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    public class Edges : IEquatable<Edges>
    {
        private static readonly YGValue YGValueUndefined = new YGValue(0, YGUnit.Undefined);

        public YGValue Left       { get; set; } = YGValueUndefined;
        public YGValue Top        { get; set; } = YGValueUndefined;
        public YGValue Right      { get; set; } = YGValueUndefined;
        public YGValue Bottom     { get; set; } = YGValueUndefined;
        public YGValue Start      { get; set; } = YGValueUndefined;
        public YGValue End        { get; set; } = YGValueUndefined;
        public YGValue Horizontal { get; set; } = YGValueUndefined;
        public YGValue Vertical   { get; set; } = YGValueUndefined;
        public YGValue All        { get; set; } = YGValueUndefined;

        public YGValue this[YGEdge key]
        {
            get
            {
                switch (key)
                {
                case YGEdge.Left:       return Left;
                case YGEdge.Top:        return Top;
                case YGEdge.Right:      return Right;
                case YGEdge.Bottom:     return Bottom;
                case YGEdge.Start:      return Start;
                case YGEdge.End:        return End;
                case YGEdge.Horizontal: return Horizontal;
                case YGEdge.Vertical:   return Vertical;
                case YGEdge.All:        return All;
                default:
                    return YGValueUndefined;
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
                case YGEdge.Horizontal:
                    Horizontal = value;
                    break;
                case YGEdge.Vertical:
                    Vertical = value;
                    break;
                case YGEdge.All:
                    All = value;
                    break;
                }
            }
        }


        public Edges Clone()
        {
            return new Edges
            {
                Left       = Left,
                Top        = Top,
                Right      = Right,
                Bottom     = Bottom,
                Start      = Start,
                End        = End,
                Horizontal = Horizontal,
                Vertical   = Vertical,
                All        = All
            };
        }

        public YGValue ComputedEdgeValue(
            YGEdge  edge,
            YGValue defaultValue)
        {
            if (this[edge].unit != YGUnit.Undefined)
                return this[edge];

            if ((edge == YGEdge.Top || edge == YGEdge.Bottom) && Vertical.unit != YGUnit.Undefined)
                return Vertical;

            if ((edge == YGEdge.Left || edge == YGEdge.Right || edge == YGEdge.Start || edge == YGEdge.End) &&
                Horizontal.unit != YGUnit.Undefined)
                return Horizontal;

            if (All.unit != YGUnit.Undefined)
                return All;

            if (edge == YGEdge.Start || edge == YGEdge.End)
                return YGConst.YGValueUndefined;

            return defaultValue;
        }


        /// <inheritdoc />
        public override string ToString()
        {
            return $"<{Left}, ^{Top}, >{Right}, v{Bottom}  <{Start}={End}>  _{Horizontal}|{Vertical}  ({All})";
        }

        /// <inheritdoc />
        public bool Equals(Edges other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Left,       other.Left)
                && Equals(Top,        other.Top)
                && Equals(Right,      other.Right)
                && Equals(Bottom,     other.Bottom)
                && Equals(Start,      other.Start)
                && Equals(End,        other.End)
                && Equals(Horizontal, other.Horizontal)
                && Equals(Vertical,   other.Vertical)
                && Equals(All,        other.All);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Edges) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Left != null ? Left.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Top        != null ? Top.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Right      != null ? Right.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Bottom     != null ? Bottom.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Start      != null ? Start.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (End        != null ? End.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Horizontal != null ? Horizontal.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Vertical   != null ? Vertical.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (All        != null ? All.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Edges left, Edges right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Edges left, Edges right)
        {
            return !Equals(left, right);
        }
    }
}
