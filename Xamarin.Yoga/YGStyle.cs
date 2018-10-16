using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    using static YGGlobal;
    using static YGConst;

    public class YGStyle
    {
        public YGDirection     direction;
        public YGFlexDirection flexDirection;
        public YGJustify       justifyContent;
        public YGAlign         alignContent;
        public YGAlign         alignItems;
        public YGAlign         alignSelf;
        public YGPositionType  positionType;
        public YGWrap          flexWrap;
        public YGOverflow      overflow;
        public YGDisplay       display;
        public float?          flex;
        public float?          flexGrow;
        public float?          flexShrink;
        public YGValue         flexBasis;

        public Edges Margin   { get; set; } = new Edges();
        public Edges Position { get; set; } = new Edges();
        public Edges Padding  { get; set; } = new Edges();
        public Edges Border   { get; set; } = new Edges();

        public Dimensions Dimensions    { get; } = new Dimensions(kYGValueAuto,      kYGValueAuto);
        public Dimensions MinDimensions { get; } = new Dimensions(kYGValueUndefined, kYGValueUndefined);
        public Dimensions MaxDimensions { get; } = new Dimensions(kYGValueUndefined, kYGValueUndefined);

        // Yoga specific properties, not compatible with flexbox specification
        public float? AspectRatio { get; set; }

        private static readonly YGValue kYGValueUndefined = new YGValue(0, YGUnit.Undefined);
        private static readonly YGValue kYGValueAuto      = new YGValue(0, YGUnit.Auto);

        public YGStyle()
        {
            direction      = YGDirection.Inherit;
            flexDirection  = YGFlexDirection.Column;
            justifyContent = YGJustify.FlexStart;
            alignContent   = YGAlign.FlexStart;
            alignItems     = YGAlign.Stretch;
            alignSelf      = YGAlign.Auto;
            positionType   = YGPositionType.Relative;
            flexWrap       = YGWrap.NoWrap;
            overflow       = YGOverflow.Visible;
            display        = YGDisplay.Flex;
            flex           = null;
            flexGrow       = null;
            flexShrink     = null;
            flexBasis      = kYGValueAuto;
            AspectRatio    = null;
        }

        public YGStyle(YGStyle style)
        {
            direction      = style.direction;
            flexDirection  = style.flexDirection;
            justifyContent = style.justifyContent;
            alignContent   = style.alignContent;
            alignItems     = style.alignItems;
            alignSelf      = style.alignSelf;
            positionType   = style.positionType;
            flexWrap       = style.flexWrap;
            overflow       = style.overflow;
            display        = style.display;
            flex           = style.flex;
            flexGrow       = style.flexGrow;
            flexShrink     = style.flexShrink;
            flexBasis      = style.flexBasis;
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
                direction      == style.direction                                     &&
                flexDirection  == style.flexDirection                                 &&
                justifyContent == style.justifyContent                                &&
                alignContent   == style.alignContent                                  &&
                alignItems     == style.alignItems                                    &&
                alignSelf      == style.alignSelf                                     &&
                positionType   == style.positionType                                  &&
                flexWrap       == style.flexWrap                                      &&
                overflow       == style.overflow                                      &&
                display        == style.display                                       &&
                EqualityComparer<float?>.Default.Equals(flex,       style.flex)       &&
                EqualityComparer<float?>.Default.Equals(flexGrow,   style.flexGrow)   &&
                EqualityComparer<float?>.Default.Equals(flexShrink, style.flexShrink) &&
                EqualityComparer<YGValue>.Default.Equals(flexBasis, style.flexBasis)  &&
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
            hashCode = hashCode * -1521134295 + direction.GetHashCode();
            hashCode = hashCode * -1521134295 + flexDirection.GetHashCode();
            hashCode = hashCode * -1521134295 + justifyContent.GetHashCode();
            hashCode = hashCode * -1521134295 + alignContent.GetHashCode();
            hashCode = hashCode * -1521134295 + alignItems.GetHashCode();
            hashCode = hashCode * -1521134295 + alignSelf.GetHashCode();
            hashCode = hashCode * -1521134295 + positionType.GetHashCode();
            hashCode = hashCode * -1521134295 + flexWrap.GetHashCode();
            hashCode = hashCode * -1521134295 + overflow.GetHashCode();
            hashCode = hashCode * -1521134295 + display.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(flex);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(flexGrow);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(flexShrink);
            hashCode = hashCode * -1521134295 + EqualityComparer<YGValue>.Default.GetHashCode(flexBasis);
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
