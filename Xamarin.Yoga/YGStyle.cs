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
        public YGFloatOptional flex;
        public YGFloatOptional flexGrow;
        public YGFloatOptional flexShrink;
        public YGValue         flexBasis;

        public readonly Edges Margin   = new Edges();
        public readonly Edges Position = new Edges();
        public readonly Edges Padding  = new Edges();
        public readonly Edges Border   = new Edges();

        public readonly Dimensions Dimensions    = new Dimensions(kYGValueAuto,      kYGValueAuto);
        public readonly Dimensions MinDimensions = new Dimensions(kYGValueUndefined, kYGValueUndefined);
        public readonly Dimensions MaxDimensions = new Dimensions(kYGValueUndefined, kYGValueUndefined);

        // Yoga specific properties, not compatible with flexbox specification
        public YGFloatOptional AspectRatio { get; set; }

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
            flex           = new YGFloatOptional();
            flexGrow       = new YGFloatOptional();
            flexShrink     = new YGFloatOptional();
            flexBasis      = kYGValueAuto;
            AspectRatio    = new YGFloatOptional();
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
            flex           = style.flex.Clone();
            flexGrow       = style.flexGrow.Clone();
            flexShrink     = style.flexShrink.Clone();
            flexBasis      = style.flexBasis;
            Margin         = Margin.Clone();
            Position       = style.Position.Clone();
            Padding        = style.Padding.Clone();
            Border         = style.Border.Clone();
            Dimensions     = style.Dimensions.Clone();
            MinDimensions  = style.MinDimensions.Clone();
            MaxDimensions  = style.MaxDimensions.Clone();
            AspectRatio    = style.AspectRatio.Clone();
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
                direction      == style.direction                                              &&
                flexDirection  == style.flexDirection                                          &&
                justifyContent == style.justifyContent                                         &&
                alignContent   == style.alignContent                                           &&
                alignItems     == style.alignItems                                             &&
                alignSelf      == style.alignSelf                                              &&
                positionType   == style.positionType                                           &&
                flexWrap       == style.flexWrap                                               &&
                overflow       == style.overflow                                               &&
                display        == style.display                                                &&
                EqualityComparer<YGFloatOptional>.Default.Equals(flex,       style.flex)       &&
                EqualityComparer<YGFloatOptional>.Default.Equals(flexGrow,   style.flexGrow)   &&
                EqualityComparer<YGFloatOptional>.Default.Equals(flexShrink, style.flexShrink) &&
                EqualityComparer<YGValue>.Default.Equals(flexBasis, style.flexBasis)           &&
                Margin   == style.Margin                                                       &&
                Position == style.Position                                                     &&
                Padding  == style.Padding                                                      &&
                Border   == style.Border                                                       &&
                Dimensions.Equals(style.Dimensions)                                            &&
                MinDimensions.Equals(style.MinDimensions)                                      &&
                MaxDimensions.Equals(style.MaxDimensions)                                      &&
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
            hashCode = hashCode * -1521134295 + EqualityComparer<YGFloatOptional>.Default.GetHashCode(flex);
            hashCode = hashCode * -1521134295 + EqualityComparer<YGFloatOptional>.Default.GetHashCode(flexGrow);
            hashCode = hashCode * -1521134295 + EqualityComparer<YGFloatOptional>.Default.GetHashCode(flexShrink);
            hashCode = hashCode * -1521134295 + EqualityComparer<YGValue>.Default.GetHashCode(flexBasis);
            hashCode = hashCode * -1521134295 + EqualityComparer<Edges>.Default.GetHashCode(Margin);
            hashCode = hashCode * -1521134295 + EqualityComparer<Edges>.Default.GetHashCode(Position);
            hashCode = hashCode * -1521134295 + EqualityComparer<Edges>.Default.GetHashCode(Padding);
            hashCode = hashCode * -1521134295 + EqualityComparer<Edges>.Default.GetHashCode(Border);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dimensions>.Default.GetHashCode(Dimensions);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dimensions>.Default.GetHashCode(MinDimensions);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dimensions>.Default.GetHashCode(MaxDimensions);
            hashCode = hashCode * -1521134295 + EqualityComparer<YGFloatOptional>.Default.GetHashCode(AspectRatio);
            return hashCode;
        }
    }
}
