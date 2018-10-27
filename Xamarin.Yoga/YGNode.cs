using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Xamarin.Yoga
{
    using static YogaConst;
    using static YGGlobal;


    public class YGNode : IEquatable<YGNode>
    {
        private readonly ObservableCollection<YGNode> _children = new ObservableCollection<YGNode>();
        private          bool                         _isDirty;
        private          NodeLayout                   _layout = new NodeLayout();

        private MeasureFunc _measureFunc;
        private YGNode      _owner;
        private NodeStyle   _style;
        private NodeCalculator _nodeCalculator;

        public YGNode() : this(YogaConfig.DefaultConfig) { }

        public YGNode(YogaConfig newConfig)
        {
            _style = new NodeStyle
            {
                Owner = this
            };

            Config = newConfig;

            if (Config.UseWebDefaults)
            {
                Style.FlexDirection = FlexDirectionType.Row;
                Style.AlignContent  = YGAlign.Stretch;
            }

            _children.CollectionChanged += ChildrenChanged;
        }

        public YGNode(YGNode node)
        {
            if (ReferenceEquals(node, this))
                return;

            _style = new NodeStyle(node.Style) {Owner = this};

            Context           = node.Context;
            PrintFunc         = node.PrintFunc;
            HasNewLayout      = node.HasNewLayout;
            NodeType          = node.NodeType;
            MeasureFunc       = node.MeasureFunc;
            BaselineFunc      = node.BaselineFunc;
            DirtiedFunc       = node.DirtiedFunc;
            _layout           = new NodeLayout(node._layout);
            LineIndex         = node.LineIndex;
            Owner             = null;
            Config            = new YogaConfig(node.Config);
            _isDirty          = node.IsDirty;
            ResolvedDimension = node.ResolvedDimension.Clone();

            foreach (var child in node.Children)
                _children.Add(new YGNode(child));

            _children.CollectionChanged += ChildrenChanged;
        }

        public BaselineFunc BaselineFunc { get; set; }

        public Collection<YGNode> Children => _children;

        public YogaConfig  Config       { get; set; }
        public object      Context      { get; set; }
        public DirtiedFunc DirtiedFunc  { get; set; }
        public bool        HasNewLayout { get; set; } = true;

        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                if (value == _isDirty)
                    return;

                _isDirty = value;
                if (value)
                    DirtiedFunc?.Invoke(this);
            }
        }

        public NodeLayout Layout
        {
            get => _layout = _layout ?? new NodeLayout();
            set => _layout = value;
        }

        public int LineIndex { get; set; }

        public MeasureFunc MeasureFunc
        {
            get => _measureFunc;
            set
            {
                if (value == null)
                {
                    NodeType = NodeType.Default;
                }
                else
                {
                    YogaGlobal.YGAssert(
                        this,
                        Children.Count == 0,
                        "Cannot set measure function: Nodes with measure functions cannot have children.");

                    NodeType = NodeType.Text;
                }

                _measureFunc = value;
            }
        }

        public string   Name     { get; set; }
        public NodeType NodeType { get; set; } = NodeType.Default;

        public YGNode Owner
        {
            get => _owner;
            set
            {
                if (_owner != value)
                {
                    Layout = null;
                    _owner = value;
                }
            }
        }

        public PrintFunc  PrintFunc         { get; set; }
        public Dimensions ResolvedDimension { get; } = new Dimensions(YGValueUndefined, YGValueUndefined);

        public NodeStyle Style
        {
            get => _style;
            set
            {
                if (_style != value)
                {
                    if (_style != null)
                        _style.Owner = null;

                    _style       = value;
                    _style.Owner = this;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public bool Equals(YGNode other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            var result =
                Equals(Context, other.Context)     &&
                HasNewLayout == other.HasNewLayout &&
                _isDirty     == other._isDirty     &&
                NodeType     == other.NodeType     &&
                LineIndex    == other.LineIndex;
            result = result &&
                Equals(_style, other._style);
            result = result &&
                Equals(_layout, other._layout);
            result = result &&
                Children.SequenceEqual(other.Children);
            result = result                       &&
                Config            == other.Config &&
                ResolvedDimension == other.ResolvedDimension;
            return result;
        }

        public void ClearChildren()
        {
            if (Children.Count == 0)
                return;

            _children.Clear();
            MarkDirtyAndPropagate();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as YGNode);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) NodeType;
                hashCode = (hashCode * 397) ^ LineIndex;
                hashCode = (hashCode * 397) ^ (ResolvedDimension != null ? ResolvedDimension.GetHashCode() : 0);
                return hashCode;
            }
        }

        public float GetLeadingBorder(FlexDirectionType axis)
        {
            if (axis.IsRow()                                   &&
                Style.Border.Start.Unit != ValueUnit.Undefined &&
                Style.Border.Start.Value.HasValue()            &&
                Style.Border.Start.Value >= 0.0f)
                return Style.Border.Start.Value;

            var computedEdgeValue =
                Style.Border.ComputedEdgeValue(axis.ToLeadingEdge(), YGValueZero).Value;
            return NumberExtensions.FloatMax(computedEdgeValue, 0.0f);
        }

        public float? GetLeadingMargin(FlexDirectionType axis, float widthSize)
        {
            if (axis.IsRow() &&
                Style.Margin.Start.Unit != ValueUnit.Undefined)
                return ResolveValueMargin(Style.Margin.Start, widthSize);

            return ResolveValueMargin(
                Style.Margin.ComputedEdgeValue(axis.ToLeadingEdge(), YGValueZero),
                widthSize);
        }

        public float? GetLeadingPadding(FlexDirectionType axis, float widthSize)
        {
            var paddingEdgeStart =
                Style.Padding.Start.ResolveValue(widthSize);
            if (axis.IsRow()                                    &&
                Style.Padding.Start.Unit != ValueUnit.Undefined &&
                paddingEdgeStart.HasValue                       && paddingEdgeStart > 0.0f)
                return paddingEdgeStart;

            var resolvedValue = Style.Padding.ComputedEdgeValue(axis.ToLeadingEdge(), YGValueZero).ResolveValue(widthSize);
            return NumberExtensions.FloatOptionalMax(resolvedValue, 0.0f);
        }

        public float? GetLeadingPaddingAndBorder(FlexDirectionType axis, float widthSize)
        {
            return GetLeadingPadding(axis, widthSize) + GetLeadingBorder(axis);
        }

        public float? GetLeadingPosition(FlexDirectionType axis, float axisSize)
        {
            if (axis.IsRow())
            {
                var leadingPosition =
                    Style.Position.ComputedEdgeValue(EdgeType.Start, YGValueUndefined);
                if (leadingPosition.Unit != ValueUnit.Undefined)
                    return leadingPosition.ResolveValue(axisSize);
            }

            var leadingPos =
                Style.Position.ComputedEdgeValue(axis.ToLeadingEdge(), YGValueUndefined);

            return leadingPos.Unit == ValueUnit.Undefined
                ? 0
                : leadingPos.ResolveValue(axisSize);
        }

        public float? GetMarginForAxis(FlexDirectionType axis, float widthSize)
        {
            return GetLeadingMargin(axis, widthSize) + GetTrailingMargin(axis, widthSize);
        }

        public float GetTrailingBorder(FlexDirectionType flexDirection)
        {
            if (flexDirection.IsRow()                        &&
                Style.Border.End.Unit != ValueUnit.Undefined &&
                Style.Border.End.Value.HasValue()            &&
                Style.Border.End.Value >= 0.0f)
                return Style.Border.End.Value;

            var computedEdgeValue = Style.Border.ComputedEdgeValue(flexDirection.ToTrailingEdge(), YGValueZero).Value;
            return NumberExtensions.FloatMax(computedEdgeValue, 0.0f);
        }

        public float? GetTrailingMargin(FlexDirectionType axis, float widthSize)
        {
            if (axis.IsRow() &&
                Style.Margin.End.Unit != ValueUnit.Undefined)
                return ResolveValueMargin(Style.Margin.End, widthSize);

            return ResolveValueMargin(
                Style.Margin.ComputedEdgeValue(axis.ToTrailingEdge(), YGValueZero),
                widthSize);
        }

        public float? GetTrailingPadding(FlexDirectionType axis, float widthSize)
        {
            if (axis.IsRow()                                        &&
                Style.Padding.End.Unit != ValueUnit.Undefined       &&
                Style.Padding.End.ResolveValue(widthSize).HasValue &&
                Style.Padding.End.ResolveValue(widthSize) >= 0.0f)
                return Style.Padding.End.ResolveValue(widthSize);

            var resolvedValue = Style.Padding.ComputedEdgeValue(axis.ToTrailingEdge(), YGValueZero).ResolveValue(widthSize);

            return NumberExtensions.FloatOptionalMax(resolvedValue, 0.0f);
        }

        public float? GetTrailingPaddingAndBorder(FlexDirectionType axis, float widthSize)
        {
            return GetTrailingPadding(axis, widthSize) + GetTrailingBorder(axis);
        }

        public float? GetTrailingPosition(FlexDirectionType axis, float axisSize)
        {
            if (axis.IsRow())
            {
                var trailingPosition =
                    Style.Position.ComputedEdgeValue(EdgeType.End, YGValueUndefined);
                if (trailingPosition.Unit != ValueUnit.Undefined)
                    return trailingPosition.ResolveValue(axisSize);
            }

            var trailingPos =
                Style.Position.ComputedEdgeValue(axis.ToTrailingEdge(), YGValueUndefined);

            return trailingPos.Unit == ValueUnit.Undefined
                ? 0
                : trailingPos.ResolveValue(axisSize);
        }

        public bool IsLayoutTreeEqualToNode(YGNode node)
        {
            if (Children.Count != node.Children.Count)
                return false;

            if (_layout != node._layout)
                return false;

            if (Children.Count == 0)
                return true;

            for (var i = 0; i < Children.Count; ++i)
            {
                var otherNodeChildren = node._children[i];
                var isLayoutTreeEqual = _children[i].IsLayoutTreeEqualToNode(otherNodeChildren);
                if (!isLayoutTreeEqual)
                    return false;
            }

            return true;
        }

        public bool IsLeadingPositionDefined(FlexDirectionType axis)
        {
            return axis.IsRow() &&
                Style.Position.ComputedEdgeValue(EdgeType.Start, YGValueUndefined)
                    .Unit != ValueUnit.Undefined ||
                Style.Position.ComputedEdgeValue(axis.ToLeadingEdge(), YGValueUndefined)
                    .Unit != ValueUnit.Undefined;
        }

        public bool IsNodeFlexible()
        {
            return Style.PositionType == PositionType.Relative &&
                (ResolveFlexGrow() != 0 || ResolveFlexShrink() != 0);
        }

        public bool IsTrailingPosDefined(FlexDirectionType axis)
        {
            return axis.IsRow() &&
                Style.Position.ComputedEdgeValue(EdgeType.End, YGValueUndefined).Unit != ValueUnit.Undefined ||
                Style.Position.ComputedEdgeValue(axis.ToTrailingEdge(), YGValueUndefined).Unit !=
                ValueUnit.Undefined;
        }

        public YGValue MarginLeadingValue(FlexDirectionType axis)
        {
            if (axis.IsRow() && Style.Margin.Start.Unit != ValueUnit.Undefined)
                return Style.Margin.Start;
            return Style.Margin[axis.ToLeadingEdge()];
        }

        public YGValue MarginTrailingValue(FlexDirectionType axis)
        {
            if (axis.IsRow() && Style.Margin.End.Unit != ValueUnit.Undefined)
                return Style.Margin.End;
            return Style.Margin[axis.ToTrailingEdge()];
        }

        public void MarkDirty()
        {
            YogaGlobal.YGAssert(
                this,
                MeasureFunc == null,
                "Only leaf nodes with custom measure functions should manually mark themselves as dirty");

            MarkDirtyAndPropagate();
        }

        public static bool operator ==(YGNode left, YGNode right)
        {
            return (object) left == null || (object) right == null ? ReferenceEquals(left, right) : Equals(left, right);
        }

        public static bool operator !=(YGNode left, YGNode right)
        {
            return !(left == right);
        }

        public void Print(PrintOptionType options)
        {
            YogaGlobal.Log(this, LogLevel.Debug, new NodePrint(this, options).ToString());
        }

        // If both left and right are defined, then use left. Otherwise return
        // +left or -right depending on which is defined.
        public float? RelativePosition(
            FlexDirectionType axis,
            float             axisSize)
        {
            if (IsLeadingPositionDefined(axis))
                return GetLeadingPosition(axis, axisSize);

            var trailingPosition = GetTrailingPosition(axis, axisSize);
            if (trailingPosition.HasValue)
                trailingPosition = -1 * trailingPosition;
            return trailingPosition;
        }

        public void ResolveDimension()
        {
            if (Style.MaxWidth.Unit != ValueUnit.Undefined &&
                YGValueEqual(Style.MaxWidth, Style.MinWidth))
                ResolvedDimension.Width = Style.MaxWidth;
            else
                ResolvedDimension.Width = Style.Width;

            if (Style.MaxHeight.Unit != ValueUnit.Undefined &&
                YGValueEqual(Style.MaxHeight, Style.MinHeight))
                ResolvedDimension.Height = Style.MaxHeight;
            else
                ResolvedDimension.Height = Style.Height;
        }

        public DirectionType ResolveDirection(DirectionType ownerDirection)
        {
            if (Style.Direction == DirectionType.Inherit)
                return ownerDirection > DirectionType.Inherit
                    ? ownerDirection
                    : DirectionType.LTR;
            return Style.Direction;
        }

        public YGValue ResolveFlexBasisPtr()
        {
            var flexBasis = Style.FlexBasis;
            if (flexBasis.Unit != ValueUnit.Auto && flexBasis.Unit != ValueUnit.Undefined)
                return flexBasis;
            if (Style.Flex.HasValue && Style.Flex > 0.0f)
                return Config.UseWebDefaults ? YGValueAuto : YGValueZero;
            return YGValueAuto;
        }

        public float ResolveFlexGrow()
        {
            // Root nodes flexGrow should always be 0
            if (Owner == null) return 0.0f;
            if (Style.FlexGrow.HasValue)
                return Style.FlexGrow.Value;
            if (Style.Flex.HasValue && Style.Flex > 0.0f)
                return Style.Flex.Value;
            return DefaultFlexGrow;
        }

        public float ResolveFlexShrink()
        {
            if (Owner == null) return 0.0f;
            if (Style.FlexShrink.HasValue)
                return Style.FlexShrink.Value;
            if (!Config.UseWebDefaults && Style.Flex.HasValue &&
                Style.Flex < 0.0f)
                return -Style.Flex.Value;
            return Config.UseWebDefaults ? WebDefaultFlexShrink : DefaultFlexShrink;
        }

        public void SetChildren(IEnumerable<YGNode> children)
        {
            foreach (var child in _children)
                child.Owner = null;

            _children.Clear();

            foreach (var child in children)
                _children.Add(child);

            MarkDirtyAndPropagate();
        }

        public void SetPosition(DirectionType direction, float mainSize, float crossSize, float ownerWidth)
        {
            /* Root nodes should be always laid-out as LTR, so we don't return negative values. */
            var directionRespectingRoot = Owner != null ? direction : DirectionType.LTR;
            var mainAxis                = ResolveFlexDirection(Style.FlexDirection, directionRespectingRoot);
            var crossAxis               = FlexDirectionCross(mainAxis, directionRespectingRoot);

            var relativePositionMain  = RelativePosition(mainAxis,  mainSize);
            var relativePositionCross = RelativePosition(crossAxis, crossSize);

            Layout.Position[mainAxis.ToLeadingEdge()] =
                UnwrapFloatOptional(GetLeadingMargin(mainAxis, ownerWidth) + relativePositionMain);
            Layout.Position[mainAxis.ToTrailingEdge()] =
                UnwrapFloatOptional(GetTrailingMargin(mainAxis, ownerWidth) + relativePositionMain);
            Layout.Position[crossAxis.ToLeadingEdge()] =
                UnwrapFloatOptional(GetLeadingMargin(crossAxis, ownerWidth) + relativePositionCross);
            Layout.Position[crossAxis.ToTrailingEdge()] =
                UnwrapFloatOptional(GetTrailingMargin(crossAxis, ownerWidth) + relativePositionCross);
        }

        internal void MarkDirtyAndPropagate()
        {
            if (!_isDirty)
            {
                IsDirty                  = true;
                Layout.ComputedFlexBasis = null;
                Owner?.MarkDirtyAndPropagate();
            }
        }

        internal void MarkDirtyAndPropagateDownwards()
        {
            _isDirty = true;
            foreach (var child in Children)
                child.MarkDirtyAndPropagateDownwards();
        }

        private void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (YGNode child in e.NewItems)
                {
                    YogaGlobal.YGAssert(
                        this,
                        child.Owner == null,
                        "Child already has a owner, it must be removed first.");

                    YogaGlobal.YGAssert(
                        this,
                        MeasureFunc == null,
                        "Cannot add child: Nodes with measure functions cannot have children.");

                    child.Owner = this;
                }
            }

            if (e.OldItems != null)
            {
                foreach (YGNode child in e.OldItems)
                    child.Owner = null;
            }

            MarkDirtyAndPropagate();
        }

        public NodeCalculator Calc => _nodeCalculator ?? (_nodeCalculator = new NodeCalculator(this));

        public void Traverse(Action<YGNode> f)
        {
            f(this);
            foreach (var child in Children)
                child.Traverse(f);
        }

        internal float Baseline()
        {
            if (BaselineFunc != null)
            {
                var baseline = BaselineFunc(
                    this,
                    Layout.MeasuredWidth,
                    Layout.MeasuredHeight);

                YogaGlobal.YGAssert(
                    this,
                    baseline.HasValue(),
                    "Expect custom baseline function to not return NaN");

                return baseline;
            }

            YGNode baselineChild = null;
            foreach (var child in Children)
            {
                if (child.LineIndex > 0) break;

                if (child.Style.PositionType == PositionType.Absolute) continue;

                if (YGNodeAlignItem(this, child) == YGAlign.Baseline)
                {
                    baselineChild = child;
                    break;
                }

                if (baselineChild == null) baselineChild = child;
            }

            if (baselineChild == null)
                return Layout.MeasuredHeight;

            var childBaseline = baselineChild.Baseline();
            return childBaseline + baselineChild.Layout.Position.Top;
        }
    }
}
