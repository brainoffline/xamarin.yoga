using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Xamarin.Yoga
{
    using static YGGlobal;

    public class NodePrint
    {
        private readonly YGNode          _node;
        private readonly PrintOptionType _options;

        public NodePrint(YGNode node, PrintOptionType options)
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

        private void AppendEdgeIfNotUndefined(
            StringBuilder sb,
            string        str,
            Edges         edges,
            EdgeType      edge)
        {
            AppendNumberIfNotUndefined(
                sb,
                str,
                edges.ComputedEdgeValue(edge, YogaConst.YGValueUndefined));
        }

        private void AppendEdges(
            StringBuilder sb,
            string        key,
            Edges         edges)
        {
            if (AreFourValuesEqual(edges))
                AppendNumberIfNotZero(sb, key, edges[EdgeType.Left]);
            else
                for (var edge = EdgeType.Left; edge != EdgeType.All; ++edge)
                {
                    var str = $"{key}-{edge.ToDescription()}";
                    AppendNumberIfNotZero(sb, str, edges[edge]);
                }
        }

        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        private void AppendFloatOptionalIfDefined(
            StringBuilder sb,
            string        key,
            float?        num)
        {
            if (num.HasValue && !num.Value.IsNaN())
                sb.Append($"{key}: {num.Value}; ");
        }

        private void AppendNumberIfNotAuto(StringBuilder sb, in string key, in YGValue number)
        {
            if (number.Unit != ValueUnit.Auto)
                AppendNumberIfNotUndefined(sb, key, number);
        }

        private void AppendNumberIfNotUndefined(
            StringBuilder sb,
            string        key,
            YGValue       number)
        {
            if (number.Unit != ValueUnit.Undefined)
            {
                if (number.Unit == ValueUnit.Auto)
                {
                    sb.Append($"{key}: auto; ");
                }
                else
                {
                    var unit = number.Unit == ValueUnit.Point ? "px" : "%";
                    sb.Append($"{key}: {number.Value}{unit}; ");
                }
            }
        }

        private void AppendNumberIfNotZero(StringBuilder sb, in string str, in YGValue number)
        {
            if (number.Unit == ValueUnit.Auto)
                sb.Append($"{str}: auto; ");
            else if (!NumberExtensions.FloatEqual(number.Value, 0))
                AppendNumberIfNotUndefined(sb, str, number);
        }

        private bool AreFourValuesEqual(Edges edges)
        {
            return
                YGValueEqual(edges.Left, edges.Top)   &&
                YGValueEqual(edges.Left, edges.Right) &&
                YGValueEqual(edges.Left, edges.Bottom);
        }


        private void Indent(StringBuilder sb, int level)
        {
            for (var i = 0; i < level; ++i)
                sb.Append("  ");
        }

        private StringBuilder NodeToString(
            StringBuilder   sb,
            YGNode          node,
            PrintOptionType options,
            int             level = 0)
        {
            if (sb == null)
                sb = new StringBuilder();

            Indent(sb, level);
            sb.Append("<div ");
            node.PrintFunc?.Invoke(node);

            if (!string.IsNullOrWhiteSpace(node.Name))
                sb.Append($"name=\"{node.Name}\" ");

            if (options.HasFlag(PrintOptionType.Layout))
            {
                sb.Append("layout=\"");
                sb.Append($"width: {node.Layout.Width}; ");
                sb.Append($"height: {node.Layout.Height}; ");
                sb.Append($"top: {node.Layout.Position.Top}; ");
                sb.Append($"left: {node.Layout.Position.Left};");
                sb.Append("\" ");
            }

            if (options.HasFlag(PrintOptionType.Style))
            {
                var defaultStyle = new YGNode().Style;

                sb.Append("style=\"");
                if (node.Style.FlexDirection != defaultStyle.FlexDirection)
                    sb.Append($"flex-direction: {node.Style.FlexDirection.ToDescription()}; ");

                if (node.Style.JustifyContent != defaultStyle.JustifyContent)
                    sb.Append($"justify-content: {node.Style.JustifyContent.ToDescription()}; ");

                if (node.Style.AlignItems != defaultStyle.AlignItems)
                    sb.Append($"align-items: {node.Style.AlignItems.ToDescription()}; ");

                if (node.Style.AlignContent != defaultStyle.AlignContent)
                    sb.Append($"align-content: {node.Style.AlignContent.ToDescription()}; ");

                if (node.Style.AlignSelf != defaultStyle.AlignSelf)
                    sb.Append($"align-self: {node.Style.AlignSelf.ToDescription()}; ");

                AppendFloatOptionalIfDefined(sb, "flex-grow",   node.Style._flexGrow);
                AppendFloatOptionalIfDefined(sb, "flex-shrink", node.Style._flexShrink);
                AppendNumberIfNotAuto(sb, "flex-basis", node.Style.FlexBasis);
                AppendFloatOptionalIfDefined(sb, "flex", node.Style._flex);

                if (node.Style.FlexWrap != defaultStyle.FlexWrap)
                    sb.Append($"flexWrap: {node.Style.FlexWrap.ToDescription()}; ");

                if (node.Style.Overflow != defaultStyle.Overflow)
                    sb.Append($"overflow: {node.Style.Overflow.ToDescription()}; ");

                if (node.Style.Display != defaultStyle.Display)
                    sb.Append($"display: {node.Style.Display.ToDescription()}; ");

                AppendEdges(sb, "margin",  node.Style.Margin);
                AppendEdges(sb, "padding", node.Style.Padding);
                AppendEdges(sb, "border",  node.Style.Border);

                AppendNumberIfNotAuto(sb, "width",      node.Style.Width);
                AppendNumberIfNotAuto(sb, "height",     node.Style.Height);
                AppendNumberIfNotAuto(sb, "max-width",  node.Style.MaxWidth);
                AppendNumberIfNotAuto(sb, "max-height", node.Style.MaxHeight);
                AppendNumberIfNotAuto(sb, "min-width",  node.Style.MinWidth);
                AppendNumberIfNotAuto(sb, "min-height", node.Style.MinHeight);

                if (node.Style.PositionType != defaultStyle.PositionType)
                    sb.Append($"position: {node.Style.PositionType.ToDescription()}; ");

                AppendEdgeIfNotUndefined(sb, "left",   node.Style.Position, EdgeType.Left);
                AppendEdgeIfNotUndefined(sb, "right",  node.Style.Position, EdgeType.Right);
                AppendEdgeIfNotUndefined(sb, "top",    node.Style.Position, EdgeType.Top);
                AppendEdgeIfNotUndefined(sb, "bottom", node.Style.Position, EdgeType.Bottom);
                sb.Append("\" ");

                if (node.MeasureFunc != null)
                    sb.Append("has-custom-measure=\"true\"");
            }

            sb.Append(">");

            var childCount = node.Children.Count;
            if (options.HasFlag(PrintOptionType.Children) && childCount > 0)
            {
                foreach (var child in node.Children)
                {
                    sb.Append("\n");
                    NodeToString(sb, child, options, level + 1);
                }

                sb.Append("\n");
                Indent(sb, level);
            }

            sb.Append("</div>");

            return sb;
        }
    }
}
