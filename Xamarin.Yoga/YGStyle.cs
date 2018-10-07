using System.Collections.Generic;

using static Xamarin.Yoga.YGGlobal;
// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga
{
    public static partial class YGGlobal
    {
        public static readonly YGValue[] kYGDefaultDimensionValuesAutoUnit = { YGValueAuto, YGValueAuto };
        public static readonly YGValue[] kYGDefaultDimensionValuesUnit = { YGValueUndefined, YGValueUndefined };
    }

    public class Dimensions
    {
        private YGValue[] values;

        public Dimensions(YGValue[] values) => this.values = values;
        public static implicit operator Dimensions(YGValue[] values) => new Dimensions(values);

        public YGValue this[int key]
        {
            get => values[key];
            set => values[key] = value;
        }

        public YGValue this[YGDimension key]
        {
            get => values[(int)key];
            set => values[(int)key] = value;
        }
    }

    public class YGStyle
    {
        public YGDirection direction;
        public YGFlexDirection flexDirection;
        public YGJustify justifyContent;
        public YGAlign alignContent;
        public YGAlign alignItems;
        public YGAlign alignSelf;
        public YGPositionType positionType;
        public YGWrap flexWrap;
        public YGOverflow overflow;
        public YGDisplay display;
        public YGFloatOptional flex;
        public YGFloatOptional flexGrow;
        public YGFloatOptional flexShrink;
        public YGValue flexBasis;

        public YGValue[] margin = new YGValue[YGEdgeCount];
        public YGValue[] position = new YGValue[YGEdgeCount];
        public YGValue[] padding = new YGValue[YGEdgeCount];
        public YGValue[] border = new YGValue[YGEdgeCount];
        public Dimensions dimensions;
        public Dimensions minDimensions;
        public Dimensions maxDimensions;
        // Yoga specific properties, not compatible with flexbox specification
        public YGFloatOptional aspectRatio;


        //private static readonly YGValue kYGValueUndefined = YGValue.YGValueUndefined;
        private static readonly YGValue kYGValueAuto = YGValueAuto;
        private static readonly YGValue[] kYGDefaultEdgeValuesUnit = {
            YGValueUndefined,
            YGValueUndefined,
            YGValueUndefined,
            YGValueUndefined,
            YGValueUndefined,
            YGValueUndefined,
            YGValueUndefined,
            YGValueUndefined,
            YGValueUndefined};

        public YGStyle()
        {
            direction = YGDirection.Inherit;
            flexDirection = YGFlexDirection.Column;
            justifyContent = YGJustify.FlexStart;
            alignContent = YGAlign.FlexStart;
            alignItems = YGAlign.Stretch;
            alignSelf = YGAlign.Auto;
            positionType = YGPositionType.Relative;
            flexWrap = YGWrap.NoWrap;
            overflow = YGOverflow.Visible;
            display = YGDisplay.Flex;
            flex = new YGFloatOptional();
            flexGrow = new YGFloatOptional();
            flexShrink = new YGFloatOptional();
            flexBasis = kYGValueAuto;
            margin = kYGDefaultEdgeValuesUnit;
            position = kYGDefaultEdgeValuesUnit;
            padding = kYGDefaultEdgeValuesUnit;
            border = kYGDefaultEdgeValuesUnit;
            dimensions = kYGDefaultDimensionValuesAutoUnit;
            minDimensions = kYGDefaultDimensionValuesUnit;
            maxDimensions = kYGDefaultDimensionValuesUnit;
            aspectRatio = new YGFloatOptional();
        }

        public static bool operator ==(YGStyle style1, YGStyle style2)
        {
            return EqualityComparer<YGStyle>.Default.Equals(style1, style2);
        }

        public static bool operator !=(YGStyle style1, YGStyle style2)
        {
            return !(style1 == style2);
        }

        public override bool Equals(object obj)
        {
            YGStyle style = obj as YGStyle;
            return style != null &&
                   direction == style.direction &&
                   flexDirection == style.flexDirection &&
                   justifyContent == style.justifyContent &&
                   alignContent == style.alignContent &&
                   alignItems == style.alignItems &&
                   alignSelf == style.alignSelf &&
                   positionType == style.positionType &&
                   flexWrap == style.flexWrap &&
                   overflow == style.overflow &&
                   display == style.display &&
                   EqualityComparer<YGFloatOptional>.Default.Equals(flex, style.flex) &&
                   EqualityComparer<YGFloatOptional>.Default.Equals(flexGrow, style.flexGrow) &&
                   EqualityComparer<YGFloatOptional>.Default.Equals(flexShrink, style.flexShrink) &&
                   EqualityComparer<YGValue>.Default.Equals(flexBasis, style.flexBasis) &&
                   EqualityComparer<YGValue[]>.Default.Equals(margin, style.margin) &&
                   EqualityComparer<YGValue[]>.Default.Equals(position, style.position) &&
                   EqualityComparer<YGValue[]>.Default.Equals(padding, style.padding) &&
                   EqualityComparer<YGValue[]>.Default.Equals(border, style.border) &&
                   EqualityComparer<Dimensions>.Default.Equals(dimensions, style.dimensions) &&
                   EqualityComparer<Dimensions>.Default.Equals(minDimensions, style.minDimensions) &&
                   EqualityComparer<Dimensions>.Default.Equals(maxDimensions, style.maxDimensions) &&
                   EqualityComparer<YGFloatOptional>.Default.Equals(aspectRatio, style.aspectRatio);
        }

        public override int GetHashCode()
        {
            int hashCode = 1546191664;
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
