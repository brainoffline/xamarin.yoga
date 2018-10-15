using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Yoga.Extensions;

namespace Xamarin.Yoga
{
    using static YGGlobal;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    public static partial class YGGlobal
    {
        internal static void indent(StringBuilder sb, int level)
        {
            for (var i = 0; i < level; ++i)
                sb.Append("  ");
        }

        internal static bool areFourValuesEqual(Edges edges)
        {
            return
                YGValueEqual(edges.Left, edges.Top)   &&
                YGValueEqual(edges.Left, edges.Right) &&
                YGValueEqual(edges.Left, edges.Bottom);
        }

        internal static void appendFormatedString(StringBuilder sb, in string message)
        {
            sb.Append(message);
        }

        internal static void appendFloatOptionalIfDefined(
            StringBuilder      sb,
            in string          key,
            in YGFloatOptional num)
        {
            if (!num.isUndefined())
                appendFormatedString(sb, $"{key}: {num.getValue()}; ");
        }

        internal static void appendNumberIfNotUndefined(
            StringBuilder sb,
            in string     key,
            in YGValue    number)
        {
            if (number.unit != YGUnit.Undefined)
            {
                if (number.unit == YGUnit.Auto)
                {
                    sb.Append($"{key}: auto; ");
                }
                else
                {
                    var unit = number.unit == YGUnit.Point ? "px" : "%";
                    appendFormatedString(sb, $"{key}: {number.value}{unit}; ");
                }
            }
        }

        internal static void appendNumberIfNotAuto(StringBuilder sb, in string key, in YGValue number)
        {
            if (number.unit != YGUnit.Auto) appendNumberIfNotUndefined(sb, key, number);
        }

        internal static void appendNumberIfNotZero(StringBuilder sb, in string str, in YGValue number)
        {
            if (number.unit == YGUnit.Auto)
                sb.Append($"{str}: auto; ");
            else if (!YGFloatsEqual(number.value, 0)) appendNumberIfNotUndefined(sb, str, number);
        }

        internal static void appendEdges(
            StringBuilder sb,
            string        key,
            Edges         edges)
        {
            if (areFourValuesEqual(edges))
                appendNumberIfNotZero(sb, key, edges[(int) YGEdge.Left]);
            else
                for (var edge = YGEdge.Left; edge != YGEdge.All; ++edge)
                {
                    var str = $"{key}-{edge.ToDescription()}";
                    appendNumberIfNotZero(sb, str, edges[edge]);
                }
        }

        internal static void appendEdgeIfNotUndefined(
            StringBuilder sb,
            string        str,
            Edges         edges,
            YGEdge        edge)
        {
            appendNumberIfNotUndefined(
                sb,
                str,
                edges.ComputedEdgeValue(edge, YGConst.YGValueUndefined));
        }

        public static StringBuilder YGNodeToString(
            StringBuilder  sb,
            YGNodeRef      node,
            YGPrintOptions options,
            int            level = 0)
        {
            if (sb == null)
                sb = new StringBuilder();

            indent(sb, level);
            appendFormatedString(sb, "<div ");
            node.getPrintFunc()?.Invoke(node);

            if (options.HasFlag(YGPrintOptions.Layout))
            {
                appendFormatedString(sb, "layout=\"");
                appendFormatedString(sb, $"width: {node.Layout.Width}; ");
                appendFormatedString(sb, $"height: {node.Layout.Height}; ");
                appendFormatedString(sb, $"top: {node.Layout.Position.Top}; ");
                appendFormatedString(sb, $"left: {node.Layout.Position.Left};");
                appendFormatedString(sb, "\" ");
            }

            if (options.HasFlag(YGPrintOptions.Style))
            {
                var defaultStyle = new YGNode().Style;

                appendFormatedString(sb, "style=\"");
                if (node.Style.flexDirection != defaultStyle.flexDirection) appendFormatedString(sb, $"flex-direction: {node.Style.flexDirection.ToDescription()}; ");

                if (node.Style.justifyContent != defaultStyle.justifyContent) appendFormatedString(sb, $"justify-content: {node.Style.justifyContent.ToDescription()}; ");

                if (node.Style.alignItems != defaultStyle.alignItems) appendFormatedString(sb, $"align-items: {node.Style.alignItems.ToDescription()}; ");

                if (node.Style.alignContent != defaultStyle.alignContent) appendFormatedString(sb, $"align-content: {node.Style.alignContent.ToDescription()}; ");

                if (node.Style.alignSelf != defaultStyle.alignSelf) appendFormatedString(sb, $"align-self: {node.Style.alignSelf.ToDescription()}; ");

                appendFloatOptionalIfDefined(sb, "flex-grow",   node.Style.flexGrow);
                appendFloatOptionalIfDefined(sb, "flex-shrink", node.Style.flexShrink);
                appendNumberIfNotAuto(sb, "flex-basis", node.Style.flexBasis);
                appendFloatOptionalIfDefined(sb, "flex", node.Style.flex);

                if (node.Style.flexWrap != defaultStyle.flexWrap) appendFormatedString(sb, $"flexWrap: {node.Style.flexWrap.ToDescription()}; ");

                if (node.Style.overflow != defaultStyle.overflow) appendFormatedString(sb, $"overflow: {node.Style.overflow.ToDescription()}; ");

                if (node.Style.display != defaultStyle.display) appendFormatedString(sb, $"display: {node.Style.display.ToDescription()}; ");

                appendEdges(sb, "margin",  node.Style.Margin);
                appendEdges(sb, "padding", node.Style.Padding);
                appendEdges(sb, "border",  node.Style.Border);

                appendNumberIfNotAuto(sb, "width",      node.Style.Dimensions[YGDimension.Width]);
                appendNumberIfNotAuto(sb, "height",     node.Style.Dimensions[YGDimension.Height]);
                appendNumberIfNotAuto(sb, "max-width",  node.Style.MaxDimensions[YGDimension.Width]);
                appendNumberIfNotAuto(sb, "max-height", node.Style.MaxDimensions[YGDimension.Height]);
                appendNumberIfNotAuto(sb, "min-width",  node.Style.MinDimensions[YGDimension.Width]);
                appendNumberIfNotAuto(sb, "min-height", node.Style.MinDimensions[YGDimension.Height]);

                if (node.Style.positionType != defaultStyle.positionType) appendFormatedString(sb, $"position: {node.Style.positionType.ToDescription()}; ");

                appendEdgeIfNotUndefined(sb, "left",   node.Style.Position, YGEdge.Left);
                appendEdgeIfNotUndefined(sb, "right",  node.Style.Position, YGEdge.Right);
                appendEdgeIfNotUndefined(sb, "top",    node.Style.Position, YGEdge.Top);
                appendEdgeIfNotUndefined(sb, "bottom", node.Style.Position, YGEdge.Bottom);
                appendFormatedString(sb, "\" ");

                if (node.MeasureFunc != null)
                    appendFormatedString(sb, "has-custom-measure=\"true\"");
            }

            appendFormatedString(sb, ">");

            var childCount = node.getChildren().Count;
            if (options.HasFlag(YGPrintOptions.Children) && childCount > 0)
            {
                for (var i = 0; i < childCount; i++)
                {
                    appendFormatedString(sb, "\n");
                    YGNodeToString(sb, YGNodeGetChild(node, i), options, level + 1);
                }

                appendFormatedString(sb, "\n");
                indent(sb, level);
            }

            appendFormatedString(sb, "</div>");

            return sb;
        }
    }
}
