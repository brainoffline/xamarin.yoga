using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    using static YogaConst;

    public class NodeStyle : IEquatable<NodeStyle>
    {
        private static readonly Value ValueAuto      = new Value(0, ValueUnit.Auto);
        private static readonly Value ValueUndefined = new Value(0, ValueUnit.Undefined);

        private          AlignType       _alignContent = AlignType.FlexStart;
        private          AlignType       _alignItems   = AlignType.Stretch;
        private          AlignType       _alignSelf    = AlignType.Auto;
        private          float?        _aspectRatio;
        private          Edges         _border     = new Edges();
        private readonly Dimensions    _dimensions = new Dimensions(ValueAuto, ValueAuto);
        private          DirectionType _direction  = DirectionType.Inherit;
        private          DisplayType   _display    = DisplayType.Flex;

        internal float?            _flex;
        private  Value           _flexBasis     = ValueAuto;
        private  FlexDirectionType _flexDirection = FlexDirectionType.Column;
        internal float?            _flexGrow;
        internal float?            _flexShrink;
        private  WrapType          _flexWrap       = WrapType.NoWrap;
        private  JustifyType       _justifyContent = JustifyType.FlexStart;
        private  Edges             _margin         = new Edges();
        private  OverflowType      _overflow       = OverflowType.Visible;
        private  YogaNode            _owner;
        private  Edges             _padding      = new Edges();
        private  Edges             _position     = new Edges();
        private  PositionType      _positionType = PositionType.Relative;


        public NodeStyle() { }

        public NodeStyle(NodeStyle style)
        {
            Direction      = style.Direction;
            FlexDirection  = style.FlexDirection;
            JustifyContent = style.JustifyContent;
            AlignContent   = style.AlignContent;
            AlignItems     = style.AlignItems;
            AlignSelf      = style.AlignSelf;
            PositionType   = style.PositionType;
            FlexWrap       = style.FlexWrap;
            Overflow       = style.Overflow;
            Display        = style.Display;
            _flex          = style._flex;
            _flexGrow      = style._flexGrow;
            _flexShrink    = style._flexShrink;
            _flexBasis     = style._flexBasis;
            _margin        = Margin.Clone();
            Position       = style.Position.Clone();
            Padding        = style.Padding.Clone();
            Border         = style.Border.Clone();
            _dimensions    = style._dimensions.Clone();
            MinDimensions  = style.MinDimensions.Clone();
            MaxDimensions  = style.MaxDimensions.Clone();
            AspectRatio    = style.AspectRatio;
        }

        public AlignType AlignContent
        {
            get => _alignContent;
            set
            {
                if (_alignContent != value)
                {
                    _alignContent = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public AlignType AlignItems
        {
            get => _alignItems;
            set
            {
                if (_alignItems != value)
                {
                    _alignItems = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public AlignType AlignSelf
        {
            get => _alignSelf;
            set
            {
                if (_alignSelf != value)
                {
                    _alignSelf = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        // Yoga specific properties, not compatible with flexbox specification
        public float? AspectRatio
        {
            get => _aspectRatio;
            set
            {
                if (!NumberExtensions.FloatOptionalEqual(_aspectRatio, value))
                {
                    _aspectRatio = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public Edges Border
        {
            get => _border;
            set
            {
                if (_border != value)
                {
                    _border       = value;
                    _border.Owner = Owner;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }


        public DirectionType Direction
        {
            get => _direction;
            set
            {
                if (_direction != value)
                {
                    _direction = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public DisplayType Display
        {
            get => _display;
            set
            {
                if (_display != value)
                {
                    _display = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public float? Flex
        {
            get => _flex;
            set
            {
                if (!NumberExtensions.FloatOptionalEqual(_flex, value))
                {
                    _flex = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public Value FlexBasis
        {
            get => _flexBasis;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (value.Unit == ValueUnit.Percent && value.Number.IsNaN())
                    value = Value.Auto;

                if (_flexBasis != value)
                {
                    _flexBasis = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public FlexDirectionType FlexDirection
        {
            get => _flexDirection;
            set
            {
                if (_flexDirection != value)
                {
                    _flexDirection = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public float? FlexGrow
        {
            get => _flexGrow;
            set
            {
                if (!NumberExtensions.FloatOptionalEqual(_flexGrow, value))
                {
                    _flexGrow = value.IsNaN() ? null : value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public float? FlexShrink
        {
            get
            {
                if (!_flexShrink.HasValue)
                {
                    if (Owner?.Config?.UseWebDefaults ?? false)
                        return WebDefaultFlexShrink;
                    return DefaultFlexShrink;
                }

                return _flexShrink;
            }
            set
            {
                if (!NumberExtensions.FloatOptionalEqual(_flexShrink, value))
                {
                    _flexShrink = value.IsNaN() ? null : value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public WrapType FlexWrap
        {
            get => _flexWrap;
            set
            {
                if (_flexWrap != value)
                {
                    _flexWrap = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public Value Height
        {
            get => _dimensions.Height;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (value.Unit == ValueUnit.Percent && value.Number.IsNaN())
                    value = Value.Auto;

                if (_dimensions.Height != value)
                {
                    _dimensions.Height = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public JustifyType JustifyContent
        {
            get => _justifyContent;
            set
            {
                if (_justifyContent != value)
                {
                    _justifyContent = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public Edges Margin
        {
            get => _margin;
            set
            {
                if (_margin != value)
                {
                    _margin       = value;
                    _margin.Owner = Owner;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        private Dimensions MaxDimensions { get; } = new Dimensions(ValueUndefined, ValueUndefined);

        public Value MaxHeight
        {
            get => MaxDimensions.Height;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (MaxDimensions.Height != value)
                {
                    MaxDimensions.Height = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public Value MaxWidth
        {
            get => MaxDimensions.Width;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (MaxDimensions.Width != value)
                {
                    MaxDimensions.Width = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        private Dimensions MinDimensions { get; } = new Dimensions(ValueUndefined, ValueUndefined);

        public Value MinHeight
        {
            get => MinDimensions.Height;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (MinDimensions.Height != value)
                {
                    MinDimensions.Height = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public Value MinWidth
        {
            get => MinDimensions.Width;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (MinDimensions.Width != value)
                {
                    MinDimensions.Width = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public OverflowType Overflow
        {
            get => _overflow;
            set
            {
                if (_overflow != value)
                {
                    _overflow = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        internal YogaNode Owner
        {
            get => _owner;
            set
            {
                _owner         = value;
                Margin.Owner   = value;
                Position.Owner = value;
                Padding.Owner  = value;
                Border.Owner   = value;
            }
        }

        public Edges Padding
        {
            get => _padding;
            set
            {
                if (_padding != value)
                {
                    _padding       = value;
                    _padding.Owner = Owner;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public Edges Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position       = value;
                    _position.Owner = Owner;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public PositionType PositionType
        {
            get => _positionType;
            set
            {
                if (_positionType != value)
                {
                    _positionType = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public Value Width
        {
            get => _dimensions.Width;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (value.Unit == ValueUnit.Percent && value.Number.IsNaN())
                    value = Value.Auto;

                if (_dimensions.Width != value)
                {
                    _dimensions.Width = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public bool Equals(NodeStyle other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var result = _flex.Equals(other._flex)    &&
                _flexGrow.Equals(other._flexGrow)     &&
                _flexShrink.Equals(other._flexShrink) &&
                _alignContent == other._alignContent;
            result = result                        &&
                Equals(_margin,   other._margin)   &&
                Equals(_position, other._position) &&
                Equals(_padding,  other._padding)  &&
                Equals(_border,   other._border);
            result = result                      &&
                _alignItems == other._alignItems &&
                _alignSelf  == other._alignSelf  &&
                _direction  == other._direction  &&
                _display    == other._display;
            result = result                              &&
                Equals(_flexBasis, other._flexBasis)     &&
                _flexDirection  == other._flexDirection  &&
                _flexWrap       == other._flexWrap       &&
                _justifyContent == other._justifyContent &&
                _overflow       == other._overflow       &&
                _positionType   == other._positionType   &&
                _aspectRatio.Equals(other._aspectRatio);
            result = result                                &&
                Equals(_dimensions,   other._dimensions)   &&
                Equals(MinDimensions, other.MinDimensions) &&
                Equals(MaxDimensions, other.MaxDimensions);
            return result;
        }

        public Value Dimension(DimensionType dim)
        {
            return _dimensions[dim];
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NodeStyle);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _flex.GetHashCode();
                hashCode = (hashCode * 397) ^ _flexGrow.GetHashCode();
                hashCode = (hashCode * 397) ^ _flexShrink.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) _alignContent;
                hashCode = (hashCode * 397) ^ (_owner    != null ? _owner.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_margin   != null ? _margin.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_position != null ? _position.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_padding  != null ? _padding.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_border   != null ? _border.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) _alignItems;
                hashCode = (hashCode * 397) ^ (int) _alignSelf;
                hashCode = (hashCode * 397) ^ (int) _direction;
                hashCode = (hashCode * 397) ^ (int) _display;
                hashCode = (hashCode * 397) ^ (_flexBasis != null ? _flexBasis.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) _flexDirection;
                hashCode = (hashCode * 397) ^ (int) _flexWrap;
                hashCode = (hashCode * 397) ^ (int) _justifyContent;
                hashCode = (hashCode * 397) ^ (int) _overflow;
                hashCode = (hashCode * 397) ^ (int) _positionType;
                hashCode = (hashCode * 397) ^ _aspectRatio.GetHashCode();
                hashCode = (hashCode * 397) ^ (_dimensions   != null ? _dimensions.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MinDimensions != null ? MinDimensions.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MaxDimensions != null ? MaxDimensions.GetHashCode() : 0);
                return hashCode;
            }
        }

        public Value MaxDimension(DimensionType dim)
        {
            return MaxDimensions[dim];
        }

        public Value MinDimension(DimensionType dim)
        {
            return MinDimensions[dim];
        }

        public static bool operator ==(NodeStyle style1, NodeStyle style2)
        {
            if (ReferenceEquals(style1, style2))
                return true;
            if (ReferenceEquals(style1, null) || ReferenceEquals(style2, null))
                return false;

            return EqualityComparer<NodeStyle>.Default.Equals(style1, style2);
        }

        public static bool operator !=(NodeStyle style1, NodeStyle style2)
        {
            return !(style1 == style2);
        }
    }
}
