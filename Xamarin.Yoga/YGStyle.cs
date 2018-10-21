using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    using static YGGlobal;
    using static YGConst;

    public class YGStyle
    {
        internal float? _flex;
        internal float? _flexGrow;
        internal float? _flexShrink;
        private YGAlign _alignContent = YGAlign.FlexStart;

        private YGNode _owner;

        internal YGNode Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                Margin.Owner = value;
                Position.Owner = value;
                Padding.Owner = value;
                Border.Owner = value;
            }
        }

        private Edges _margin = new Edges();

        public Edges Margin
        {
            get => _margin;
            set
            {
                if (_margin != value)
                {
                    _margin = value;
                    _margin.Owner = Owner;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        private Edges _position = new Edges();

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

        private Edges _padding = new Edges();

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

        private Edges _border = new Edges();

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


        public YGAlign AlignContent
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

        private YGAlign _alignItems = YGAlign.Stretch;

        public YGAlign AlignItems
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

        private YGAlign _alignSelf = YGAlign.Auto;

        public YGAlign AlignSelf
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

        private YGDirection _direction = YGDirection.Inherit;

        public YGDirection Direction
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

        private YGDisplay _display = YGDisplay.Flex;

        public YGDisplay Display
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

        private YGValue _flexBasis = kYGValueAuto;

        public YGValue FlexBasis
        {
            get => _flexBasis;
            set
            {
                if ((value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) && !value.value.IsNaN())
                    value = new YGValue(float.NaN, value.unit);
                if (value.unit == YGUnit.Percent && value.value.IsNaN())
                    value = YGValue.Auto;

                if (_flexBasis != value)
                {
                    _flexBasis = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        private YGFlexDirection _flexDirection = YGFlexDirection.Column;

        public YGFlexDirection FlexDirection
        {
            get => _flexDirection;
            set {
                if (_flexDirection != value)
                {
                    _flexDirection = value;
                    Owner?.MarkDirtyAndPropagate();
                }

            }
        }

        public float? Flex
        {
            get => _flex;
            set
            {
                if (!FloatOptionalEqual(_flex, value))
                {
                    _flex = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public float? FlexGrow
        {
            get => _flexGrow;
            set
            {
                if (!FloatOptionalEqual(_flexGrow, value))
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
                        return kWebDefaultFlexShrink;
                    return kDefaultFlexShrink;
                }

                return _flexShrink;
            }
            set
            {
                if (!FloatOptionalEqual(_flexShrink, value))
                {
                    _flexShrink = value.IsNaN() ? null : value;
                    Owner?.MarkDirtyAndPropagate();
                }

            }
        }

        private YGWrap _flexWrap = YGWrap.NoWrap;

        public YGWrap FlexWrap
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

        private YGJustify _justifyContent = YGJustify.FlexStart;

        public YGJustify JustifyContent
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

        private YGOverflow _overflow = YGOverflow.Visible;

        public YGOverflow Overflow
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

        private YGPositionType _positionType = YGPositionType.Relative;

        public YGPositionType PositionType
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

        private Dimensions Dimensions    { get; } = new Dimensions(kYGValueAuto,      kYGValueAuto);
        private Dimensions MinDimensions { get; } = new Dimensions(kYGValueUndefined, kYGValueUndefined);
        private Dimensions MaxDimensions { get; } = new Dimensions(kYGValueUndefined, kYGValueUndefined);

        public YGValue Width
        {
            get => Dimensions.Width;
            set
            {
                if ((value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) && !value.value.IsNaN())
                    value = new YGValue(float.NaN, value.unit);
                if (value.unit == YGUnit.Percent && value.value.IsNaN())
                    value = YGValue.Auto;

                if (Dimensions.Width != value)
                {
                    Dimensions.Width = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public YGValue Height
        {
            get => Dimensions.Height;
            set
            {
                if ((value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) && !value.value.IsNaN())
                    value = new YGValue(float.NaN, value.unit);
                if (value.unit == YGUnit.Percent && value.value.IsNaN())
                    value = YGValue.Auto;
               
                if (Dimensions.Height != value)
                {
                    Dimensions.Height = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public YGValue MinWidth
        {
            get => MinDimensions.Width;
            set
            {
                if ((value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) && !value.value.IsNaN())
                    value = new YGValue(float.NaN, value.unit);
                if (MinDimensions.Width != value)
                {
                    MinDimensions.Width = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public YGValue MinHeight
        {
            get => MinDimensions.Height;
            set
            {
                if ((value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) && !value.value.IsNaN())
                    value = new YGValue(float.NaN, value.unit);
                if (MinDimensions.Height != value)
                {
                    MinDimensions.Height = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public YGValue MaxWidth
        {
            get => MaxDimensions.Width;
            set
            {
                if ((value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) && !value.value.IsNaN())
                    value = new YGValue(float.NaN, value.unit);
                if (MaxDimensions.Width != value)
                {
                    MaxDimensions.Width = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        public YGValue MaxHeight
        {
            get => MaxDimensions.Height;
            set
            {
                if ((value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) && !value.value.IsNaN())
                    value = new YGValue(float.NaN, value.unit);
                if (MaxDimensions.Height != value)
                {
                    MaxDimensions.Height = value;
                    Owner?.MarkDirtyAndPropagate();
                }

            }
        }

        public YGValue Dimension(YGDimension dim) => Dimensions[dim];
        public YGValue MinDimension(YGDimension dim) => MinDimensions[dim];
        public YGValue MaxDimension(YGDimension dim) => MaxDimensions[dim];


        private float? _aspectRatio;

        // Yoga specific properties, not compatible with flexbox specification
        public float? AspectRatio
        {
            get => _aspectRatio;
            set
            {
                if (!FloatOptionalEqual(_aspectRatio, value))
                {
                    _aspectRatio = value;
                    Owner?.MarkDirtyAndPropagate();
                }
            }
        }

        private static readonly YGValue kYGValueUndefined = new YGValue(0, YGUnit.Undefined);
        private static readonly YGValue kYGValueAuto      = new YGValue(0, YGUnit.Auto);

        public YGStyle() { }

        public YGStyle(YGStyle style)
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
            Flex           = style.Flex;
            FlexGrow       = style.FlexGrow;
            FlexShrink     = style.FlexShrink;
            FlexBasis      = style.FlexBasis;
            _margin         = Margin.Clone();
            Position       = style.Position.Clone();
            Padding        = style.Padding.Clone();
            Border         = style.Border.Clone();
            Dimensions     = style.Dimensions.Clone();
            MinDimensions  = style.MinDimensions.Clone();
            MaxDimensions  = style.MaxDimensions.Clone();
            AspectRatio    = style.AspectRatio;
        }

        public static bool operator ==(YGStyle style1, YGStyle style2)
        {
            if (ReferenceEquals(style1, style2))
                return true;
            if (ReferenceEquals(style1, null) || ReferenceEquals(style2, null))
                return false;

            return EqualityComparer<YGStyle>.Default.Equals(style1, style2);
        }

        public static bool operator !=(YGStyle style1, YGStyle style2)
        {
            return !(style1 == style2);
        }

        public override bool Equals(object obj)
        {
            var style = obj as YGStyle;
            if (ReferenceEquals(style, null))
                return false;

            return
                Direction      == style.Direction                                     &&
                FlexDirection  == style.FlexDirection                                 &&
                JustifyContent == style.JustifyContent                                &&
                AlignContent   == style.AlignContent                                  &&
                AlignItems     == style.AlignItems                                    &&
                AlignSelf      == style.AlignSelf                                     &&
                PositionType   == style.PositionType                                  &&
                FlexWrap       == style.FlexWrap                                      &&
                Overflow       == style.Overflow                                      &&
                Display        == style.Display                                       &&
                EqualityComparer<float?>.Default.Equals(Flex,       style.Flex)       &&
                EqualityComparer<float?>.Default.Equals(FlexGrow,   style.FlexGrow)   &&
                EqualityComparer<float?>.Default.Equals(FlexShrink, style.FlexShrink) &&
                EqualityComparer<YGValue>.Default.Equals(FlexBasis, style.FlexBasis)  &&
                Margin   == style.Margin                                              &&
                Position == style.Position                                            &&
                Padding  == style.Padding                                             &&
                Border   == style.Border                                              &&
                Dimensions.Equals(style.Dimensions)                                   &&
                MinDimensions.Equals(style.MinDimensions)                             &&
                MaxDimensions.Equals(style.MaxDimensions)                             &&
                AspectRatio.Equals(style.AspectRatio);
        }

        public override int GetHashCode()
        {
            var hashCode = 1546191664;
            hashCode = hashCode * -1521134295 + Direction.GetHashCode();
            hashCode = hashCode * -1521134295 + FlexDirection.GetHashCode();
            hashCode = hashCode * -1521134295 + JustifyContent.GetHashCode();
            hashCode = hashCode * -1521134295 + AlignContent.GetHashCode();
            hashCode = hashCode * -1521134295 + AlignItems.GetHashCode();
            hashCode = hashCode * -1521134295 + AlignSelf.GetHashCode();
            hashCode = hashCode * -1521134295 + PositionType.GetHashCode();
            hashCode = hashCode * -1521134295 + FlexWrap.GetHashCode();
            hashCode = hashCode * -1521134295 + Overflow.GetHashCode();
            hashCode = hashCode * -1521134295 + Display.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(Flex);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(FlexGrow);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(FlexShrink);
            hashCode = hashCode * -1521134295 + EqualityComparer<YGValue>.Default.GetHashCode(FlexBasis);
            hashCode = hashCode * -1521134295 + EqualityComparer<Edges>.Default.GetHashCode(Margin);
            hashCode = hashCode * -1521134295 + EqualityComparer<Edges>.Default.GetHashCode(Position);
            hashCode = hashCode * -1521134295 + EqualityComparer<Edges>.Default.GetHashCode(Padding);
            hashCode = hashCode * -1521134295 + EqualityComparer<Edges>.Default.GetHashCode(Border);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dimensions>.Default.GetHashCode(Dimensions);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dimensions>.Default.GetHashCode(MinDimensions);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dimensions>.Default.GetHashCode(MaxDimensions);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(AspectRatio);
            return hashCode;
        }
    }
}
