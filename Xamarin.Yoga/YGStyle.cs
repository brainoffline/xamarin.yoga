using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    using static YGGlobal;
    using static YGConst;

    public class YGStyle
    {
        private float? _flexShrink;

        internal YGNode Owner { get; set; }

        public Edges Margin   { get; internal set; } = new Edges();
        public Edges Position { get; internal set; } = new Edges();
        public Edges Padding  { get; internal set; } = new Edges();
        public Edges Border   { get; internal set; } = new Edges();

        public YGAlign         AlignContent   { get; internal set; } = YGAlign.FlexStart;
        public YGAlign         AlignItems     { get; internal set; } = YGAlign.Stretch;
        public YGAlign         AlignSelf      { get; internal set; } = YGAlign.Auto;
        public YGDirection     Direction      { get; internal set; } = YGDirection.Inherit;
        public YGDisplay       Display        { get; internal set; } = YGDisplay.Flex;
        public float?          Flex           { get; internal set; }
        public YGValue         FlexBasis      { get; internal set; } = kYGValueAuto;
        public YGFlexDirection FlexDirection  { get; internal set; } = YGFlexDirection.Column;
        public float?          FlexGrow       { get; internal set; }

        public float? FlexShrink
        {
            get
            {
                if (_flexShrink.HasValue && !_flexShrink.IsNaN())
                    return _flexShrink;

                if (Owner?.Config?.UseWebDefaults ?? false)
                    return kWebDefaultFlexShrink;
                return kDefaultFlexShrink;
            }
            internal set => _flexShrink = value;
        }

        public YGWrap          FlexWrap       { get; internal set; } = YGWrap.NoWrap;
        public YGJustify       JustifyContent { get; internal set; } = YGJustify.FlexStart;
        public YGOverflow      Overflow       { get; internal set; } = YGOverflow.Visible;
        public YGPositionType  PositionType   { get; internal set; } = YGPositionType.Relative;

        public Dimensions Dimensions    { get; } = new Dimensions(kYGValueAuto,      kYGValueAuto);
        public Dimensions MinDimensions { get; } = new Dimensions(kYGValueUndefined, kYGValueUndefined);
        public Dimensions MaxDimensions { get; } = new Dimensions(kYGValueUndefined, kYGValueUndefined);

        // Yoga specific properties, not compatible with flexbox specification
        public float? AspectRatio { get; internal set; }

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
            Margin         = Margin.Clone();
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
