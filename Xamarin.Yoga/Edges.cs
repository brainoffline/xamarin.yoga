using System;

// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    public class Edges : IEquatable<Edges>
    {
        private static readonly YGValue YGValueUndefined = new YGValue(float.NaN, ValueUnit.Undefined);
        private YGValue _all = YGValueUndefined;
        private YGValue _bottom = YGValueUndefined;
        private YGValue _end = YGValueUndefined;
        private YGValue _horizontal = YGValueUndefined;
        private YGValue _left = YGValueUndefined;
        private YGValue _right = YGValueUndefined;
        private YGValue _start = YGValueUndefined;
        private YGValue _top = YGValueUndefined;
        private YGValue _vertical = YGValueUndefined;

        public Edges() { }

        public Edges(float left, float top, float right, float bottom)
        {
            Left   = left;
            Top    = top;
            Right  = right;
            Bottom = bottom;
        }

        public YGValue All
        {
            get => _all;
            set => Change(ref _all, value);
        }

        public YGValue Bottom
        {
            get => _bottom;
            set => Change(ref _bottom, value);
        }

        public YGValue End
        {
            get => _end;
            set => Change(ref _end, value);
        }

        public YGValue Horizontal
        {
            get => _horizontal;
            set => Change(ref _horizontal, value);
        }

        public YGValue this[EdgeType key]
        {
            get
            {
                switch (key)
                {
                case EdgeType.Left:       return Left;
                case EdgeType.Top:        return Top;
                case EdgeType.Right:      return Right;
                case EdgeType.Bottom:     return Bottom;
                case EdgeType.Start:      return Start;
                case EdgeType.End:        return End;
                case EdgeType.Horizontal: return Horizontal;
                case EdgeType.Vertical:   return Vertical;
                case EdgeType.All:        return All;
                default:
                    return YGValueUndefined;
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
                case EdgeType.Horizontal:
                    Horizontal = value;
                    break;
                case EdgeType.Vertical:
                    Vertical = value;
                    break;
                case EdgeType.All:
                    All = value;
                    break;
                }
            }
        }

        public YGValue Left
        {
            get => _left;
            set => Change(ref _left, value);
        }

        internal YGNode Owner { get; set; }

        public YGValue Right
        {
            get => _right;
            set => Change(ref _right, value);
        }

        public YGValue Start
        {
            get => _start;
            set => Change(ref _start, value);
        }

        public YGValue Top
        {
            get => _top;
            set => Change(ref _top, value);
        }

        public YGValue Vertical
        {
            get => _vertical;
            set => Change(ref _vertical, value);
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
            EdgeType edge,
            YGValue  defaultValue)
        {
            if (this[edge].Unit != ValueUnit.Undefined)
                return this[edge];

            if ((edge == EdgeType.Top || edge == EdgeType.Bottom) && Vertical.Unit != ValueUnit.Undefined)
                return Vertical;

            if ((edge == EdgeType.Left || edge == EdgeType.Right || edge == EdgeType.Start || edge == EdgeType.End) &&
                Horizontal.Unit != ValueUnit.Undefined)
                return Horizontal;

            if (All.Unit != ValueUnit.Undefined)
                return All;

            if (edge == EdgeType.Start || edge == EdgeType.End)
                return YogaConst.YGValueUndefined;

            return defaultValue;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Edges) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Left                       != null ? Left.GetHashCode() : 0;
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


        /// <inheritdoc />
        public override string ToString()
        {
            return $"<{Left}, ^{Top}, >{Right}, v{Bottom}  <{Start}={End}>  _{Horizontal}|{Vertical}  ({All})";
        }

        private void Change(ref YGValue val, YGValue value)
        {
            if (val != value)
            {
                val = value;
                Owner?.MarkDirtyAndPropagate();
            }
        }
    }
}
