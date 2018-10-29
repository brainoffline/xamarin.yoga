using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xamarin.Yoga
{
    public static class EnumExtensions
    {
        public static string ToDescription<T>(this T value) where T : struct
        {
            var type = value.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(value));

            var memberInfo = type.GetMember(value.ToString());
            if (memberInfo.Length <= 0)
                return value.ToString();

            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attrs.Length > 0
                ? ((DescriptionAttribute) attrs[0]).Description
                : value.ToString();
        }

        public static DimensionType ToDimension(this FlexDirectionType axis)
        {
            //{ YGDimension.Height, YGDimension.Height, YGDimension.Width, YGDimension.Width }
            switch (axis)
            {
            case FlexDirectionType.Column:
            case FlexDirectionType.ColumnReverse: return DimensionType.Height;
            case FlexDirectionType.Row:
            case FlexDirectionType.RowReverse: return DimensionType.Width;
            }

            throw new ArgumentException("Unknown Flex Direction", nameof(axis));
        }

        public static EdgeType ToLeadingEdge(this FlexDirectionType axis)
        {
            // { YGEdge.Top, YGEdge.Bottom, YGEdge.Left, YGEdge.Right }
            switch (axis)
            {
            case FlexDirectionType.Column:        return EdgeType.Top;
            case FlexDirectionType.ColumnReverse: return EdgeType.Bottom;
            case FlexDirectionType.Row:           return EdgeType.Left;
            case FlexDirectionType.RowReverse:    return EdgeType.Right;
            }

            throw new ArgumentException("Unknown Flex Direction", nameof(axis));
        }

        public static EdgeType ToPositionEdge(this FlexDirectionType axis)
        {
            // { YGEdge.Top, YGEdge.Bottom, YGEdge.Left, YGEdge.Right }
            switch (axis)
            {
            case FlexDirectionType.Column:        return EdgeType.Top;
            case FlexDirectionType.ColumnReverse: return EdgeType.Bottom;
            case FlexDirectionType.Row:           return EdgeType.Left;
            case FlexDirectionType.RowReverse:    return EdgeType.Right;
            }

            throw new ArgumentException("Unknown Flex Direction", nameof(axis));
        }

        public static EdgeType ToTrailingEdge(this FlexDirectionType axis)
        {
            // { YGEdge.Bottom, YGEdge.Top, YGEdge.Right, YGEdge.Left }
            switch (axis)
            {
            case FlexDirectionType.Column:        return EdgeType.Bottom;
            case FlexDirectionType.ColumnReverse: return EdgeType.Top;
            case FlexDirectionType.Row:           return EdgeType.Right;
            case FlexDirectionType.RowReverse:    return EdgeType.Left;
            }

            throw new ArgumentException("Unknown Flex Direction", nameof(axis));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsColumn(this FlexDirectionType flexDirection)
        {
            return flexDirection == FlexDirectionType.Column ||
                flexDirection    == FlexDirectionType.ColumnReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsRow(this FlexDirectionType flexDirection)
        {
            return flexDirection == FlexDirectionType.Row ||
                flexDirection    == FlexDirectionType.RowReverse;
        }

        public static FlexDirectionType FlexDirectionCross(FlexDirectionType flexDirection, DirectionType direction)
        {
            return flexDirection.IsColumn()
                ? ResolveFlexDirection(FlexDirectionType.Row, direction)
                : FlexDirectionType.Column;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FlexDirectionType ResolveFlexDirection(FlexDirectionType flexDirection, DirectionType direction)
        {
            if (direction == DirectionType.RTL)
            {
                if (flexDirection == FlexDirectionType.Row) return FlexDirectionType.RowReverse;
                if (flexDirection == FlexDirectionType.RowReverse) return FlexDirectionType.Row;
            }

            return flexDirection;
        }
    }
}
