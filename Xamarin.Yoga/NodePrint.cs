using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Xamarin.Yoga
{
    using static NumberExtensions;

    internal class NodePrint
    {
        private readonly YogaNode        _node;
        private readonly PrintOptionType _options;

        public NodePrint(YogaNode node, PrintOptionType options)
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
                edges.ComputedEdgeValue(edge, YogaConst.ValueUndefined));
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

        private void AppendNumberIfNotAuto(StringBuilder sb, in string key, in Value number)
        {
            if (number.Unit != ValueUnit.Auto)
                AppendNumberIfNotUndefined(sb, key, number);
        }

        private void AppendNumberIfNotUndefined(
            StringBuilder sb,
            string        key,
            Value         number)
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
                    sb.Append($"{key}: {number.Number}{unit}; ");
                }
            }
        }

        private void AppendNumberIfNotZero(StringBuilder sb, in string str, in Value number)
        {
            if (number.Unit == ValueUnit.Auto)
                sb.Append($"{str}: auto; ");
            else if (!FloatEqual(number.Number, 0))
                AppendNumberIfNotUndefined(sb, str, number);
        }

        private bool AreFourValuesEqual(Edges edges)
        {
            return
                ValueEqual(edges.Left, edges.Top)   &&
                ValueEqual(edges.Left, edges.Right) &&
                ValueEqual(edges.Left, edges.Bottom);
        }


        private void Indent(StringBuilder sb, int level)
        {
            for (var i = 0; i < level; ++i)
                sb.Append("  ");
        }

        private StringBuilder NodeToString(
            StringBuilder   sb,
            YogaNode        node,
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
                var defaultStyle = (INodeStyle)(new YogaNode());

                sb.Append("style=\"");
                if (node.FlexDirection != defaultStyle.FlexDirection)
                    sb.Append($"flex-direction: {node.FlexDirection.ToDescription()}; ");

                if (node.JustifyContent != defaultStyle.JustifyContent)
                    sb.Append($"justify-content: {node.JustifyContent.ToDescription()}; ");

                if (node.AlignItems != defaultStyle.AlignItems)
                    sb.Append($"align-items: {node.AlignItems.ToDescription()}; ");

                if (node.AlignContent != defaultStyle.AlignContent)
                    sb.Append($"align-content: {node.AlignContent.ToDescription()}; ");

                if (node.AlignSelf != defaultStyle.AlignSelf)
                    sb.Append($"align-self: {node.AlignSelf.ToDescription()}; ");

                AppendFloatOptionalIfDefined(sb, "flex-grow",   node.FlexGrow);
                AppendFloatOptionalIfDefined(sb, "flex-shrink", node.GetFlexShrink());
                AppendNumberIfNotAuto(sb, "flex-basis", node.FlexBasis);
                AppendFloatOptionalIfDefined(sb, "flex", node.Flex);

                if (node.FlexWrap != defaultStyle.FlexWrap)
                    sb.Append($"flexWrap: {node.FlexWrap.ToDescription()}; ");

                if (node.Overflow != defaultStyle.Overflow)
                    sb.Append($"overflow: {node.Overflow.ToDescription()}; ");

                if (node.Display != defaultStyle.Display)
                    sb.Append($"display: {node.Display.ToDescription()}; ");

                AppendEdges(sb, "margin",  node.Margin);
                AppendEdges(sb, "padding", node.Padding);
                AppendEdges(sb, "border",  node.Border);

                AppendNumberIfNotAuto(sb, "width",      node.Width);
                AppendNumberIfNotAuto(sb, "height",     node.Height);
                AppendNumberIfNotAuto(sb, "max-width",  node.MaxWidth);
                AppendNumberIfNotAuto(sb, "max-height", node.MaxHeight);
                AppendNumberIfNotAuto(sb, "min-width",  node.MinWidth);
                AppendNumberIfNotAuto(sb, "min-height", node.MinHeight);

                if (node.PositionType != defaultStyle.PositionType)
                    sb.Append($"position: {node.PositionType.ToDescription()}; ");

                AppendEdgeIfNotUndefined(sb, "left",   node.Position, EdgeType.Left);
                AppendEdgeIfNotUndefined(sb, "right",  node.Position, EdgeType.Right);
                AppendEdgeIfNotUndefined(sb, "top",    node.Position, EdgeType.Top);
                AppendEdgeIfNotUndefined(sb, "bottom", node.Position, EdgeType.Bottom);
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
