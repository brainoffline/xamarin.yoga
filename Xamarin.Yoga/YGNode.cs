using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Xamarin.Yoga
{
    using static YGConst;
    using static YGGlobal;


    public class YGNode : IEquatable<YGNode>
    {
        private YGStyle _style;
        private YGNode  _owner;

        public YGConfig Config  { get; set; }
        public string   Name    { get; set; }
        public object   Context { get; set; }

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

        public YGPrintFunc PrintFunc { get; set; }
        public YGNodeType  NodeType  { get; set; } = YGNodeType.Default;
        public int         LineIndex { get; set; }
        public YGDirtiedFunc DirtiedFunc { get; set; }
        public YGBaselineFunc BaselineFunc { get; set; }
        public bool           HasNewLayout { get; set; } = true;

        private YGLayout       layout_   = new YGLayout();

        private bool       isDirty_           = false;
        private Dimensions ResolvedDimensions = new Dimensions(YGValueUndefined, YGValueUndefined);

        public YGNode() : this(YGConfig.DefaultConfig) { }

        public YGNode(YGConfig newConfig)
        {
            _style = new YGStyle
            {
                Owner = this
            };

            Config = newConfig;

            if (Config.UseWebDefaults)
            {
                Style.FlexDirection = YGFlexDirection.Row;
                Style.AlignContent  = YGAlign.Stretch;
            }

            _children.CollectionChanged += ChildrenChanged;
        }

        public YGNode(YGNode node)
        {
            if (ReferenceEquals(node, this))
                return;

            _style = new YGStyle(node.Style) {Owner = this};

            Context            = node.Context;
            PrintFunc          = node.PrintFunc;
            HasNewLayout       = node.HasNewLayout;
            NodeType           = node.NodeType;
            MeasureFunc        = node.MeasureFunc;
            BaselineFunc       = node.BaselineFunc;
            DirtiedFunc        = node.DirtiedFunc;
            layout_            = new YGLayout(node.layout_);
            LineIndex          = node.LineIndex;
            Owner              = null;
            Config             = new YGConfig(node.Config);
            isDirty_           = node.IsDirty;
            ResolvedDimensions = node.getResolvedDimensions().Clone();

            foreach (var child in node.Children)
                _children.Add(new YGNode(child));

            _children.CollectionChanged += ChildrenChanged;
        }

        public YGStyle Style
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

        private readonly ObservableCollection<YGNode> _children = new ObservableCollection<YGNode>();
        public           Collection<YGNode>           Children => _children;

        private void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (YGNode child in e.NewItems)
                {
                    YGAssertWithNode(
                        this,
                        child.Owner == null,
                        "Child already has a owner, it must be removed first.");

                    YGAssertWithNode(
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

        public void Print(YGPrintOptions options)
        {
            YGLog(this, YGLogLevel.Debug, new NodePrint(this, options).ToString());
        }

        private YGMeasureFunc _measureFunc = null;

        public YGMeasureFunc MeasureFunc
        {
            get => _measureFunc;
            set
            {
                if (value == null)
                {
                    NodeType = YGNodeType.Default;
                }
                else
                {
                    YGAssertWithNode(
                        this,
                        Children.Count == 0,
                        "Cannot set measure function: Nodes with measure functions cannot have children.");

                    // TODO: t18095186 Move nodeType to opt-in function and mark appropriate
                    // places in Litho
                    NodeType = YGNodeType.Text;
                }

                _measureFunc = value;
            }
        }

        // For Performance reasons passing as reference.
        public YGLayout Layout
        {
            get => layout_ = layout_ ?? new YGLayout();
            set => layout_ = value;
        }

        public bool IsDirty
        {
            get => isDirty_;
            set
            {
                if (value == isDirty_)
                    return;

                isDirty_ = value;
                if (value)
                    DirtiedFunc?.Invoke(this);
            }
        }

        public Dimensions getResolvedDimensions()
        {
            return ResolvedDimensions;
        }

        public YGValue getResolvedDimension(YGDimension dimension)
        {
            return ResolvedDimensions[dimension];
        }

        // Setters

        public void SetChildren(IEnumerable<YGNode> children)
        {
            _children.Clear();

            foreach (var child in children)
                _children.Add(child);

            MarkDirtyAndPropagate();
        }


        public float? getLeadingPosition(
            in YGFlexDirection axis,
            in float           axisSize)
        {
            if (YGFlexDirectionIsRow(axis))
            {
                YGValue leadingPosition =
                    Style.Position.ComputedEdgeValue(YGEdge.Start, YGConst.YGValueUndefined);
                if (leadingPosition.unit != YGUnit.Undefined) return YGResolveValue(leadingPosition, axisSize);
            }

            YGValue leadingPos =
                Style.Position.ComputedEdgeValue(leading[(int) axis], YGConst.YGValueUndefined);

            return leadingPos.unit == YGUnit.Undefined
                ? new float?(0)
                : YGResolveValue(leadingPos, axisSize);
        }

        public float? getTrailingPosition(
            in YGFlexDirection axis,
            in float           axisSize)
        {
            if (YGFlexDirectionIsRow(axis))
            {
                YGValue trailingPosition =
                    Style.Position.ComputedEdgeValue(YGEdge.End, YGConst.YGValueUndefined);
                if (trailingPosition.unit != YGUnit.Undefined) return YGResolveValue(trailingPosition, axisSize);
            }

            YGValue trailingPos =
                Style.Position.ComputedEdgeValue(trailing[(int) axis], YGConst.YGValueUndefined);

            return trailingPos.unit == YGUnit.Undefined
                ? new float?(0)
                : YGResolveValue(trailingPos, axisSize);
        }

        public bool isLeadingPositionDefined(in YGFlexDirection axis)
        {
            return YGFlexDirectionIsRow(axis) &&
                Style.Position.ComputedEdgeValue(YGEdge.Start, YGConst.YGValueUndefined)
                    .unit != YGUnit.Undefined ||
                Style.Position.ComputedEdgeValue(leading[(int) axis], YGConst.YGValueUndefined)
                    .unit != YGUnit.Undefined;
        }

        public bool isTrailingPosDefined(in YGFlexDirection axis)
        {
            return YGFlexDirectionIsRow(axis) &&
                Style.Position.ComputedEdgeValue(YGEdge.End, YGConst.YGValueUndefined).unit != YGUnit.Undefined ||
                Style.Position.ComputedEdgeValue(trailing[(int) axis], YGConst.YGValueUndefined).unit !=
                YGUnit.Undefined;
        }

        public float? getLeadingMargin(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            if (YGFlexDirectionIsRow(axis) &&
                Style.Margin.Start.unit != YGUnit.Undefined)
                return YGResolveValueMargin(Style.Margin.Start, widthSize);

            return YGResolveValueMargin(
                Style.Margin.ComputedEdgeValue(leading[(int) axis], YGConst.YGValueZero),
                widthSize);
        }

        public float? getTrailingMargin(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            if (YGFlexDirectionIsRow(axis) &&
                Style.Margin.End.unit != YGUnit.Undefined)
                return YGResolveValueMargin(Style.Margin.End, widthSize);

            return YGResolveValueMargin(
                Style.Margin.ComputedEdgeValue(trailing[(int) axis], YGConst.YGValueZero),
                widthSize);
        }

        public float? getMarginForAxis(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            return getLeadingMargin(axis, widthSize) + getTrailingMargin(axis, widthSize);
        }

        // If both left and right are defined, then use left. Otherwise return
        // +left or -right depending on which is defined.
        public float? relativePosition(
            in YGFlexDirection axis,
            in float           axisSize)
        {
            if (isLeadingPositionDefined(axis))
                return getLeadingPosition(axis, axisSize);

            float? trailingPosition = getTrailingPosition(axis, axisSize);
            if (trailingPosition.HasValue)
                trailingPosition = -1 * trailingPosition;
            return trailingPosition;
        }

        public void SetPosition(
            YGDirection direction,
            float       mainSize,
            float       crossSize,
            float       ownerWidth)
        {
            /* Root nodes should be always layouted as LTR, so we don't return negative
             * values. */
            YGDirection directionRespectingRoot = Owner != null ? direction : YGDirection.LTR;
            YGFlexDirection mainAxis =
                YGResolveFlexDirection(Style.FlexDirection, directionRespectingRoot);
            YGFlexDirection crossAxis = YGFlexDirectionCross(mainAxis, directionRespectingRoot);

            float? relativePositionMain  = relativePosition(mainAxis,  mainSize);
            float? relativePositionCross = relativePosition(crossAxis, crossSize);

            Layout.Position[leading[(int) mainAxis]] =
                YGUnwrapFloatOptional(getLeadingMargin(mainAxis, ownerWidth) + relativePositionMain);
            Layout.Position[trailing[(int) mainAxis]] =
                YGUnwrapFloatOptional(getTrailingMargin(mainAxis, ownerWidth) + relativePositionMain);
            Layout.Position[leading[(int) crossAxis]] =
                YGUnwrapFloatOptional(getLeadingMargin(crossAxis, ownerWidth) + relativePositionCross);
            Layout.Position[trailing[(int) crossAxis]] =
                YGUnwrapFloatOptional(getTrailingMargin(crossAxis, ownerWidth) + relativePositionCross);
        }

        public YGValue marginLeadingValue(in YGFlexDirection axis)
        {
            if (YGFlexDirectionIsRow(axis) && Style.Margin.Start.unit != YGUnit.Undefined)
                return Style.Margin.Start;
            return Style.Margin[leading[(int) axis]];
        }

        public YGValue marginTrailingValue(in YGFlexDirection axis)
        {
            if (YGFlexDirectionIsRow(axis) && Style.Margin.End.unit != YGUnit.Undefined)
                return Style.Margin.End;
            return Style.Margin[trailing[(int) axis]];
        }

        public YGValue resolveFlexBasisPtr()
        {
            YGValue flexBasis = Style.FlexBasis;
            if (flexBasis.unit != YGUnit.Auto && flexBasis.unit != YGUnit.Undefined)
                return flexBasis;
            if (Style.Flex.HasValue && Style.Flex > 0.0f)
                return Config.UseWebDefaults ? YGConst.YGValueAuto : YGConst.YGValueZero;
            return YGConst.YGValueAuto;
        }

        public void resolveDimension()
        {
            if (Style.MaxWidth.unit != YGUnit.Undefined &&
                YGValueEqual(Style.MaxWidth, Style.MinWidth))
                ResolvedDimensions.Width = Style.MaxWidth;
            else
                ResolvedDimensions.Width = Style.Width;

            if (Style.MaxHeight.unit != YGUnit.Undefined &&
                YGValueEqual(Style.MaxHeight, Style.MinHeight))
                ResolvedDimensions.Height = Style.MaxHeight;
            else
                ResolvedDimensions.Height = Style.Height;
        }

        public YGDirection ResolveDirection(YGDirection ownerDirection)
        {
            if (Style.Direction == YGDirection.Inherit)
                return ownerDirection > YGDirection.Inherit
                    ? ownerDirection
                    : YGDirection.LTR;
            return Style.Direction;
        }

        public void ClearChildren()
        {
            if (Children.Count == 0)
                return;

            _children.Clear();
            MarkDirtyAndPropagate();
        }

        public void MarkDirty()
        {
            YGAssertWithNode(
                this,
                MeasureFunc == null,
                "Only leaf nodes with custom measure functions should manually mark themselves as dirty");

            MarkDirtyAndPropagate();
        }

        internal void MarkDirtyAndPropagate()
        {
            if (!isDirty_)
            {
                IsDirty                  = true;
                Layout.ComputedFlexBasis = null;
                Owner?.MarkDirtyAndPropagate();
            }
        }

        internal void MarkDirtyAndPropagateDownwards()
        {
            isDirty_ = true;
            foreach (var child in Children)
                child.MarkDirtyAndPropagateDownwards();
        }

        public float resolveFlexGrow()
        {
            // Root nodes flexGrow should always be 0
            if (Owner == null) return 0.0f;
            if (Style.FlexGrow.HasValue)
                return Style.FlexGrow.Value;
            if (Style.Flex.HasValue && Style.Flex > 0.0f)
                return Style.Flex.Value;
            return kDefaultFlexGrow;
        }

        public float resolveFlexShrink()
        {
            if (Owner == null) return 0.0f;
            if (Style.FlexShrink.HasValue)
                return Style.FlexShrink.Value;
            if (!Config.UseWebDefaults && Style.Flex.HasValue &&
                Style.Flex < 0.0f)
                return -Style.Flex.Value;
            return Config.UseWebDefaults ? kWebDefaultFlexShrink : kDefaultFlexShrink;
        }

        public bool isNodeFlexible()
        {
            return Style.PositionType == YGPositionType.Relative &&
                (resolveFlexGrow() != 0 || resolveFlexShrink() != 0);
        }

        public float getLeadingBorder(in YGFlexDirection axis)
        {
            if (YGFlexDirectionIsRow(axis)                  &&
                Style.Border.Start.unit != YGUnit.Undefined &&
                Style.Border.Start.value.HasValue()         &&
                Style.Border.Start.value >= 0.0f)
                return Style.Border.Start.value;

            float computedEdgeValue =
                Style.Border.ComputedEdgeValue(leading[(int) axis], YGConst.YGValueZero).value;
            return YGFloatMax(computedEdgeValue, 0.0f);
        }

        public float getTrailingBorder(in YGFlexDirection flexDirection)
        {
            if (YGFlexDirectionIsRow(flexDirection)        &&
                Style.Border.End.unit  != YGUnit.Undefined && Style.Border.End.value.HasValue() &&
                Style.Border.End.value >= 0.0f)
                return Style.Border.End.value;

            float computedEdgeValue =
                Style.Border.ComputedEdgeValue(trailing[(int) flexDirection], YGConst.YGValueZero).value;
            return YGFloatMax(computedEdgeValue, 0.0f);
        }

        public float? getLeadingPadding(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            float? paddingEdgeStart =
                YGResolveValue(Style.Padding.Start, widthSize);
            if (YGFlexDirectionIsRow(axis)                   &&
                Style.Padding.Start.unit != YGUnit.Undefined &&
                paddingEdgeStart.HasValue                    && paddingEdgeStart > 0.0f)
                return paddingEdgeStart;

            float? resolvedValue = YGResolveValue(
                Style.Padding.ComputedEdgeValue(leading[(int) axis], YGConst.YGValueZero),
                widthSize);
            return FloatOptionalMax(resolvedValue, 0.0f);
        }

        public float? getTrailingPadding(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            if (YGFlexDirectionIsRow(axis)                            &&
                Style.Padding.End.unit != YGUnit.Undefined            &&
                YGResolveValue(Style.Padding.End, widthSize).HasValue &&
                YGResolveValue(Style.Padding.End, widthSize) >= 0.0f)
                return YGResolveValue(Style.Padding.End, widthSize);

            float? resolvedValue = YGResolveValue(
                Style.Padding.ComputedEdgeValue(trailing[(int) axis], YGConst.YGValueZero),
                widthSize);

            return FloatOptionalMax(resolvedValue, 0.0f);
        }

        public float? getLeadingPaddingAndBorder(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            return getLeadingPadding(axis, widthSize) + new float?(getLeadingBorder(axis));
        }

        public float? getTrailingPaddingAndBorder(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            return getTrailingPadding(axis, widthSize) + new float?(getTrailingBorder(axis));
        }


        public float LayoutGetMargin(YGEdge edge)
        {
            YGAssertWithNode(
                this,
                edge <= YGEdge.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case YGEdge.Left when Layout.Direction == YGDirection.RTL:
                return Layout.Margin.End;
            case YGEdge.Left:
                return Layout.Margin.Start;
            case YGEdge.Right when Layout.Direction == YGDirection.RTL:
                return Layout.Margin.Start;
            case YGEdge.Right:
                return Layout.Margin.End;
            }

            return Layout.Margin[edge];
        }


        public bool IsLayoutTreeEqualToNode(YGNode node)
        {
            if (Children.Count != node.Children.Count)
                return false;

            if (layout_ != node.layout_)
                return false;

            if (Children.Count == 0)
                return true;

            for (int i = 0; i < Children.Count; ++i)
            {
                var otherNodeChildren = node._children[i];
                var isLayoutTreeEqual = _children[i].IsLayoutTreeEqualToNode(otherNodeChildren);
                if (!isLayoutTreeEqual)
                    return false;
            }

            return true;
        }

        /// <inheritdoc />
        public bool Equals(YGNode other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            bool result =
                Equals(Context,   other.Context)   &&
                HasNewLayout == other.HasNewLayout &&
                isDirty_     == other.isDirty_     &&
                NodeType     == other.NodeType     &&
                LineIndex    == other.LineIndex;
            result = result &&
                Equals(Style, other.Style);
            result = result &&
                Equals(layout_, other.layout_);
            result = result &&
                Children.SequenceEqual(other.Children);
            result = result                          &&
                Config             == other.Config   &&
                ResolvedDimensions == other.ResolvedDimensions;
            return result;
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
                int hashCode = (int) NodeType;
                hashCode = (hashCode * 397) ^ LineIndex;
                hashCode = (hashCode * 397) ^ (ResolvedDimensions != null ? ResolvedDimensions.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(YGNode left, YGNode right)
        {
            return (object) left == null || (object) right == null ? ReferenceEquals(left, right) : Equals(left, right);
        }

        public static bool operator !=(YGNode left, YGNode right)
        {
            return !(left == right);
        }
    }
}
