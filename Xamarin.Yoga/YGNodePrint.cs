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

        internal static bool areFourValuesEqual(in YGValue[] four)
        {
            return
                YGValueEqual(four[0], four[1]) &&
                YGValueEqual(four[0], four[2]) &&
                YGValueEqual(four[0], four[3]);
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
            in string     key,
            in YGValue[]  edges)
        {
            if (areFourValuesEqual(edges))
                appendNumberIfNotZero(sb, key, edges[(int) YGEdge.Left]);
            else
                for (var edge = (int) YGEdge.Left; edge != (int) YGEdge.All; ++edge)
                {
                    var str = $"{key}-{((YGEdge)edge).ToDescription()}";
                    appendNumberIfNotZero(sb, str, edges[edge]);
                }
        }

        internal static void appendEdgeIfNotUndefined(
            StringBuilder sb,
            in string     str,
            in YGValue[]  edges,
            in YGEdge     edge)
        {
            appendNumberIfNotUndefined(
                sb,
                str,
                YGComputedEdgeValue(edges, edge, YGConst.YGValueUndefined));
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
                appendFormatedString(sb, $"width: {node.getLayout().dimensions[(int) YGDimension.Width]}; ");
                appendFormatedString(sb, $"height: {node.getLayout().dimensions[(int) YGDimension.Height]}; ");
                appendFormatedString(sb, $"top: {node.getLayout().position[(int) YGEdge.Top]}; ");
                appendFormatedString(sb, $"left: {node.getLayout().position[(int) YGEdge.Left]};");
                appendFormatedString(sb, "\" ");
            }

            if (options.HasFlag(YGPrintOptions.Style))
            {
                var defaultStyle = new YGNode().getStyle();

                appendFormatedString(sb, "style=\"");
                if (node.getStyle().flexDirection != defaultStyle.flexDirection) appendFormatedString(sb, $"flex-direction: {node.getStyle().flexDirection.ToDescription()}; ");

                if (node.getStyle().justifyContent != defaultStyle.justifyContent) appendFormatedString(sb, $"justify-content: {node.getStyle().justifyContent.ToDescription()}; ");

                if (node.getStyle().alignItems != defaultStyle.alignItems) appendFormatedString(sb, $"align-items: {node.getStyle().alignItems.ToDescription()}; ");

                if (node.getStyle().alignContent != defaultStyle.alignContent) appendFormatedString(sb, $"align-content: {node.getStyle().alignContent.ToDescription()}; ");

                if (node.getStyle().alignSelf != defaultStyle.alignSelf) appendFormatedString(sb, $"align-self: {node.getStyle().alignSelf.ToDescription()}; ");

                appendFloatOptionalIfDefined(sb, "flex-grow",   node.getStyle().flexGrow);
                appendFloatOptionalIfDefined(sb, "flex-shrink", node.getStyle().flexShrink);
                appendNumberIfNotAuto(sb, "flex-basis", node.getStyle().flexBasis);
                appendFloatOptionalIfDefined(sb, "flex", node.getStyle().flex);

                if (node.getStyle().flexWrap != defaultStyle.flexWrap) appendFormatedString(sb, $"flexWrap: {node.getStyle().flexWrap.ToDescription()}; ");

                if (node.getStyle().overflow != defaultStyle.overflow) appendFormatedString(sb, $"overflow: {node.getStyle().overflow.ToDescription()}; ");

                if (node.getStyle().display != defaultStyle.display) appendFormatedString(sb, $"display: {node.getStyle().display.ToDescription()}; ");

                appendEdges(sb, "margin",  node.getStyle().margin);
                appendEdges(sb, "padding", node.getStyle().padding);
                appendEdges(sb, "border",  node.getStyle().border);

                appendNumberIfNotAuto(sb, "width",      node.getStyle().dimensions[YGDimension.Width]);
                appendNumberIfNotAuto(sb, "height",     node.getStyle().dimensions[YGDimension.Height]);
                appendNumberIfNotAuto(sb, "max-width",  node.getStyle().maxDimensions[YGDimension.Width]);
                appendNumberIfNotAuto(sb, "max-height", node.getStyle().maxDimensions[YGDimension.Height]);
                appendNumberIfNotAuto(sb, "min-width",  node.getStyle().minDimensions[YGDimension.Width]);
                appendNumberIfNotAuto(sb, "min-height", node.getStyle().minDimensions[YGDimension.Height]);

                if (node.getStyle().positionType != defaultStyle.positionType) appendFormatedString(sb, $"position: {node.getStyle().positionType.ToDescription()}; ");

                appendEdgeIfNotUndefined(sb, "left",   node.getStyle().position, YGEdge.Left);
                appendEdgeIfNotUndefined(sb, "right",  node.getStyle().position, YGEdge.Right);
                appendEdgeIfNotUndefined(sb, "top",    node.getStyle().position, YGEdge.Top);
                appendEdgeIfNotUndefined(sb, "bottom", node.getStyle().position, YGEdge.Bottom);
                appendFormatedString(sb, "\" ");

                if (node.getMeasure() != null) appendFormatedString(sb, "has-custom-measure=\"true\"");
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
