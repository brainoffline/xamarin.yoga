using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    using static YGGlobal;
    using static YGConst;
    using System;

    public class Dimensions : IEquatable<Dimensions>
    {
        private YGValue[] values;

        public Dimensions(YGValue[] values)
        {
            this.values = values;
        }

        public Dimensions Clone()
        {
            return new Dimensions((YGValue[])values.Clone());
        }

        public static implicit operator Dimensions(YGValue[] values)
        {
            return new Dimensions(values);
        }

        public static bool operator ==(Dimensions dimensions1, Dimensions dimensions2)
        {
            return EqualityComparer<Dimensions>.Default.Equals(dimensions1, dimensions2);
        }

        public static bool operator !=(Dimensions dimensions1, Dimensions dimensions2)
        {
            return !(dimensions1 == dimensions2);
        }

        public YGValue this[int key]
        {
            get => values[key];
            set => values[key] = value;
        }

        public YGValue this[YGDimension key]
        {
            get => values[(int) key];
            set => values[(int) key] = value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Dimensions);
        }

        public bool Equals(Dimensions other)
        {
            return !ReferenceEquals(other, null) &&
                values.SequenceEqual(other.values);
        }
    }

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

        public YGValue[]  margin   = new YGValue[YGEdgeCount];
        public YGValue[]  position = new YGValue[YGEdgeCount];
        public YGValue[]  padding  = new YGValue[YGEdgeCount];
        public YGValue[]  border   = new YGValue[YGEdgeCount];
        public Dimensions dimensions;
        public Dimensions minDimensions;

        public Dimensions maxDimensions;

        // Yoga specific properties, not compatible with flexbox specification
        public YGFloatOptional aspectRatio;


        private static readonly YGValue kYGValueUndefined = new YGValue(0, YGUnit.Undefined);
        private static readonly YGValue kYGValueAuto = new YGValue(0, YGUnit.Auto);

        public static readonly YGValue[] kYGDefaultDimensionValuesAutoUnit = { kYGValueAuto, kYGValueAuto };
        public static readonly YGValue[] kYGDefaultDimensionValuesUnit     = { kYGValueUndefined, kYGValueUndefined };

        private static readonly YGValue[] kYGDefaultEdgeValuesUnit =
        {
            kYGValueUndefined,
            kYGValueUndefined,
            kYGValueUndefined,
            kYGValueUndefined,
            kYGValueUndefined,
            kYGValueUndefined,
            kYGValueUndefined,
            kYGValueUndefined,
            kYGValueUndefined
        };

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
            margin         = (YGValue[]) kYGDefaultEdgeValuesUnit.Clone();
            position       = (YGValue[]) kYGDefaultEdgeValuesUnit.Clone();
            padding        = (YGValue[]) kYGDefaultEdgeValuesUnit.Clone();
            border         = (YGValue[]) kYGDefaultEdgeValuesUnit.Clone();
            dimensions     = (YGValue[]) kYGDefaultDimensionValuesAutoUnit.Clone();
            minDimensions  = (YGValue[]) kYGDefaultDimensionValuesUnit.Clone();
            maxDimensions  = (YGValue[]) kYGDefaultDimensionValuesUnit.Clone();
            aspectRatio    = new YGFloatOptional();
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
            flex = style.flex.Clone();
            flexGrow = style.flexGrow.Clone();
            flexShrink = style.flexShrink.Clone();
            flexBasis      = style.flexBasis;
            margin         = (YGValue[])style.margin.Clone();
            position       = (YGValue[])style.position.Clone();
            padding        = (YGValue[])style.padding.Clone();
            border         = (YGValue[])style.border.Clone();
            dimensions     = style.dimensions.Clone();
            minDimensions = style.minDimensions.Clone();
            maxDimensions = style.maxDimensions.Clone();
            aspectRatio    = style.aspectRatio.Clone();
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
                margin.SequenceEqual(style.margin)                                             &&
                position.SequenceEqual(style.position)                                         &&
                padding.SequenceEqual(style.padding)                                           &&
                border.SequenceEqual(style.border)                                             &&
                dimensions.Equals(style.dimensions)                                            &&
                minDimensions.Equals(style.minDimensions)                                      &&
                maxDimensions.Equals(style.maxDimensions)                                      &&
                aspectRatio.Equals(style.aspectRatio);
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
            hashCode = hashCode * -1521134295 + EqualityComparer<YGValue[]>.Default.GetHashCode(margin);
            hashCode = hashCode * -1521134295 + EqualityComparer<YGValue[]>.Default.GetHashCode(position);
            hashCode = hashCode * -1521134295 + EqualityComparer<YGValue[]>.Default.GetHashCode(padding);
            hashCode = hashCode * -1521134295 + EqualityComparer<YGValue[]>.Default.GetHashCode(border);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dimensions>.Default.GetHashCode(dimensions);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dimensions>.Default.GetHashCode(minDimensions);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dimensions>.Default.GetHashCode(maxDimensions);
            hashCode = hashCode * -1521134295 + EqualityComparer<YGFloatOptional>.Default.GetHashCode(aspectRatio);
            return hashCode;
        }
    }
}
