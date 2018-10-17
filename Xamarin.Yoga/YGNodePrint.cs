using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Xamarin.Yoga
{
    using static YGGlobal;

    public class NodePrint
    {
        private readonly YGNode         _node;
        private readonly YGPrintOptions _options;

        public NodePrint(YGNode node, YGPrintOptions options)
        {
            _node    = node ?? throw new ArgumentNullException(nameof(node));
            _options = options;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = NodeToString(null, _node, _options);
            return sb.ToString();
        }


        private void Indent(StringBuilder sb, int level)
        {
            for (var i = 0; i < level; ++i)
                sb.Append("  ");
        }

        private bool AreFourValuesEqual(Edges edges)
        {
            return
                YGValueEqual(edges.Left, edges.Top)   &&
                YGValueEqual(edges.Left, edges.Right) &&
                YGValueEqual(edges.Left, edges.Bottom);
        }

        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        private void AppendFloatOptionalIfDefined(
            StringBuilder sb,
            string        key,
            float?        num)
        {
            if (!num.IsNaN())
                sb.Append($"{key}: {num.Value}; ");
        }

        private void AppendNumberIfNotUndefined(
            StringBuilder sb,
            string        key,
            YGValue       number)
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
                    sb.Append($"{key}: {number.value}{unit}; ");
                }
            }
        }

        private void AppendNumberIfNotAuto(StringBuilder sb, in string key, in YGValue number)
        {
            if (number.unit != YGUnit.Auto)
                AppendNumberIfNotUndefined(sb, key, number);
        }

        private void AppendNumberIfNotZero(StringBuilder sb, in string str, in YGValue number)
        {
            if (number.unit == YGUnit.Auto)
                sb.Append($"{str}: auto; ");
            else if (!FloatEqual(number.value, 0))
                AppendNumberIfNotUndefined(sb, str, number);
        }

        private void AppendEdges(
            StringBuilder sb,
            string        key,
            Edges         edges)
        {
            if (AreFourValuesEqual(edges))
                AppendNumberIfNotZero(sb, key, edges[YGEdge.Left]);
            else
                for (var edge = YGEdge.Left; edge != YGEdge.All; ++edge)
                {
                    var str = $"{key}-{edge.ToDescription()}";
                    AppendNumberIfNotZero(sb, str, edges[edge]);
                }
        }

        private void AppendEdgeIfNotUndefined(
            StringBuilder sb,
            string        str,
            Edges         edges,
            YGEdge        edge)
        {
            AppendNumberIfNotUndefined(
                sb,
                str,
                edges.ComputedEdgeValue(edge, YGConst.YGValueUndefined));
        }

        private StringBuilder NodeToString(
            StringBuilder  sb,
            YGNode         node,
            YGPrintOptions options,
            int            level = 0)
        {
            if (sb == null)
                sb = new StringBuilder();

            Indent(sb, level);
            sb.Append("<div ");
            node.getPrintFunc()?.Invoke(node);

            if (options.HasFlag(YGPrintOptions.Layout))
            {
                sb.Append("layout=\"");
                sb.Append($"width: {node.Layout.Width}; ");
                sb.Append($"height: {node.Layout.Height}; ");
                sb.Append($"top: {node.Layout.Position.Top}; ");
                sb.Append($"left: {node.Layout.Position.Left};");
                sb.Append("\" ");
            }

            if (options.HasFlag(YGPrintOptions.Style))
            {
                var defaultStyle = new YGNode().Style;

                sb.Append("style=\"");
                if (node.Style.flexDirection != defaultStyle.flexDirection)
                    sb.Append($"flex-direction: {node.Style.flexDirection.ToDescription()}; ");

                if (node.Style.justifyContent != defaultStyle.justifyContent)
                    sb.Append($"justify-content: {node.Style.justifyContent.ToDescription()}; ");

                if (node.Style.alignItems != defaultStyle.alignItems)
                    sb.Append($"align-items: {node.Style.alignItems.ToDescription()}; ");

                if (node.Style.alignContent != defaultStyle.alignContent)
                    sb.Append($"align-content: {node.Style.alignContent.ToDescription()}; ");

                if (node.Style.alignSelf != defaultStyle.alignSelf)
                    sb.Append($"align-self: {node.Style.alignSelf.ToDescription()}; ");

                AppendFloatOptionalIfDefined(sb, "flex-grow",   node.Style.flexGrow);
                AppendFloatOptionalIfDefined(sb, "flex-shrink", node.Style.flexShrink);
                AppendNumberIfNotAuto(sb, "flex-basis", node.Style.flexBasis);
                AppendFloatOptionalIfDefined(sb, "flex", node.Style.flex);

                if (node.Style.flexWrap != defaultStyle.flexWrap)
                    sb.Append($"flexWrap: {node.Style.flexWrap.ToDescription()}; ");

                if (node.Style.overflow != defaultStyle.overflow)
                    sb.Append($"overflow: {node.Style.overflow.ToDescription()}; ");

                if (node.Style.display != defaultStyle.display)
                    sb.Append($"display: {node.Style.display.ToDescription()}; ");

                AppendEdges(sb, "margin",  node.Style.Margin);
                AppendEdges(sb, "padding", node.Style.Padding);
                AppendEdges(sb, "border",  node.Style.Border);

                AppendNumberIfNotAuto(sb, "width",      node.Style.Dimensions[YGDimension.Width]);
                AppendNumberIfNotAuto(sb, "height",     node.Style.Dimensions[YGDimension.Height]);
                AppendNumberIfNotAuto(sb, "max-width",  node.Style.MaxDimensions[YGDimension.Width]);
                AppendNumberIfNotAuto(sb, "max-height", node.Style.MaxDimensions[YGDimension.Height]);
                AppendNumberIfNotAuto(sb, "min-width",  node.Style.MinDimensions[YGDimension.Width]);
                AppendNumberIfNotAuto(sb, "min-height", node.Style.MinDimensions[YGDimension.Height]);

                if (node.Style.positionType != defaultStyle.positionType)
                    sb.Append($"position: {node.Style.positionType.ToDescription()}; ");

                AppendEdgeIfNotUndefined(sb, "left",   node.Style.Position, YGEdge.Left);
                AppendEdgeIfNotUndefined(sb, "right",  node.Style.Position, YGEdge.Right);
                AppendEdgeIfNotUndefined(sb, "top",    node.Style.Position, YGEdge.Top);
                AppendEdgeIfNotUndefined(sb, "bottom", node.Style.Position, YGEdge.Bottom);
                sb.Append("\" ");

                if (node.MeasureFunc != null)
                    sb.Append("has-custom-measure=\"true\"");
            }

            sb.Append(">");

            var childCount = node.Children.Count;
            if (options.HasFlag(YGPrintOptions.Children) && childCount > 0)
            {
                for (var i = 0; i < childCount; i++)
                {
                    sb.Append("\n");
                    NodeToString(sb, node.Children[i], options, level + 1);
                }

                sb.Append("\n");
                Indent(sb, level);
            }

            sb.Append("</div>");

            return sb;
        }
    }
}
