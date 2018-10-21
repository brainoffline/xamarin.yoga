using System;

// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    public class Edges : IEquatable<Edges>
    {
        private static readonly YGValue YGValueUndefined = new YGValue(float.NaN, YGUnit.Undefined);

        internal YGNode Owner { get; set; }

        private void Change(ref YGValue val, YGValue value)
        {
            if (val != value)
            {
                val = value;
                Owner?.MarkDirtyAndPropagate();
            }
        }

        private YGValue _left = YGValueUndefined;

        public YGValue Left
        {
            get => _left;
            set => Change(ref _left, value);
        }

        private YGValue _top = YGValueUndefined;

        public YGValue Top
        {
            get => _top;
            set => Change(ref _top, value);
        }

        private YGValue _right = YGValueUndefined;

        public YGValue Right
        {
            get => _right;
            set => Change(ref _right, value);
        }

        private YGValue _bottom = YGValueUndefined;

        public YGValue Bottom
        {
            get => _bottom;
            set => Change(ref _bottom, value);
        }

        private YGValue _start = YGValueUndefined;

        public YGValue Start
        {
            get => _start;
            set => Change(ref _start, value);
        }

        private YGValue _end = YGValueUndefined;

        public YGValue End
        {
            get => _end;
            set => Change(ref _end, value);
        }

        private YGValue _horizontal = YGValueUndefined;

        public YGValue Horizontal
        {
            get => _horizontal;
            set => Change(ref _horizontal, value);
        }

        private YGValue _vertical = YGValueUndefined;

        public YGValue Vertical
        {
            get => _vertical;
            set => Change(ref _vertical, value);
        }

        private YGValue _all = YGValueUndefined;

        public YGValue All
        {
            get => _all;
            set => Change(ref _all, value);
        }

        public Edges() { }

        public Edges(float left, float top, float right, float bottom)
        {
            Left   = left;
            Top    = top;
            Right  = right;
            Bottom = bottom;
        }

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
                _left       = _left,
                _top        = _top,
                _right      = _right,
                _bottom     = _bottom,
                _start      = _start,
                _end        = _end,
                _horizontal = _horizontal,
                _vertical   = _vertical,
                _all        = _all
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
