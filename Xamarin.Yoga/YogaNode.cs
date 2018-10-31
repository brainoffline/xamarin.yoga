using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Xamarin.Yoga
{
    using static NumberExtensions;
    using static YogaConst;


    public class YogaNode : IEquatable<YogaNode>, INodeStyle
    {
        private static readonly Value ValueAuto      = new Value(float.NaN, ValueUnit.Auto);
        private static readonly Value ValueUndefined = new Value(float.NaN, ValueUnit.Undefined);


        private          AlignType                      _alignContent = AlignType.FlexStart;
        private          AlignType                      _alignItems   = AlignType.Stretch;
        private          AlignType                      _alignSelf    = AlignType.Auto;
        private          float?                         _aspectRatio;
        private          Edges                          _border;
        private readonly ObservableCollection<YogaNode> _children   = new ObservableCollection<YogaNode>();
        private readonly Dimensions                     _dimensions = new Dimensions(ValueAuto, ValueAuto);
        private          DirectionType                  _direction  = DirectionType.Inherit;
        private          DisplayType                    _display    = DisplayType.Flex;
        private          float?                         _flex;
        private          Value                          _flexBasis     = ValueAuto;
        private          FlexDirectionType              _flexDirection = FlexDirectionType.Column;
        private          float?                         _flexGrow;
        private          float?                         _flexShrink;
        private          WrapType                       _flexWrap = WrapType.NoWrap;
        private          bool                           _isDirty;
        private          JustifyType                    _justifyContent = JustifyType.FlexStart;
        private          NodeLayout                     _layout         = new NodeLayout();
        private          Edges                          _margin;
        private readonly Dimensions                     _maxDimensions = new Dimensions(ValueUndefined, ValueUndefined);
        private          MeasureFunc                    _measureFunc;
        private readonly Dimensions                     _minDimensions = new Dimensions(ValueUndefined, ValueUndefined);
        private          NodeCalculator                 _nodeCalculator;
        private          OverflowType                   _overflow = OverflowType.Visible;
        private          YogaNode                       _owner;
        private          Edges                          _padding;
        private          Edges                          _position;
        private          PositionType                   _positionType = PositionType.Relative;

        public YogaNode() : this(YogaConfig.DefaultConfig) { }

        public YogaNode(YogaConfig newConfig)
        {
            _border   = new Edges {Owner = this};
            _margin   = new Edges {Owner = this};
            _padding  = new Edges {Owner = this};
            _position = new Edges {Owner = this};

            Config = newConfig;

            if (Config.UseWebDefaults)
            {
                FlexDirection = FlexDirectionType.Row;
                AlignContent  = AlignType.Stretch;
            }

            _children.CollectionChanged += ChildrenChanged;
        }

        public YogaNode(YogaNode node)
        {
            if (ReferenceEquals(node, this))
                return;

            //_style = new NodeStyle(node.Style) {Owner = this};
            AlignContent   = node.AlignContent;
            AlignItems     = node.AlignItems;
            AlignSelf      = node.AlignSelf;
            AspectRatio    = node.AspectRatio;
            Border         = node.Border.Clone();
            Direction      = node.Direction;
            Display        = node.Display;
            Flex           = node.Flex;
            FlexBasis      = node.FlexBasis;
            FlexDirection  = node.FlexDirection;
            FlexGrow       = node.FlexGrow;
            FlexShrink     = node.GetFlexShrink();
            FlexWrap       = node.FlexWrap;
            Height         = node.Height;
            JustifyContent = node.JustifyContent;
            Margin         = node.Margin.Clone();
            MaxHeight      = node.MaxHeight;
            MaxWidth       = node.MaxWidth;
            MinHeight      = node.MinHeight;
            MinWidth       = node.MinWidth;
            Overflow       = node.Overflow;
            Padding        = node.Padding.Clone();
            Position       = node.Position.Clone();
            PositionType   = node.PositionType;
            Width          = node.Width;

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
                _children.Add(new YogaNode(child));

            _children.CollectionChanged += ChildrenChanged;
        }

        public BaselineFunc         BaselineFunc { get; set; }
        public NodeCalculator       Calc         => _nodeCalculator ?? (_nodeCalculator = new NodeCalculator(this));
        public Collection<YogaNode> Children     => _children;
        public YogaConfig           Config       { get; }
        public object               Context      { get; set; }
        public DirtiedFunc          DirtiedFunc  { get; set; }
        public bool                 HasNewLayout { get; set; } = true;

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
            private set => _layout = value;
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
                    YogaGlobal.YogaAssert(
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

        public YogaNode Owner
        {
            get => _owner;
            private set
            {
                if (_owner != value)
                {
                    Layout = null;
                    _owner = value;
                }
            }
        }

        public PrintFunc  PrintFunc         { get; set; }
        public Dimensions ResolvedDimension { get; } = new Dimensions(ValueUndefined, ValueUndefined);

        //public NodeStyle Style
        //{
        //    get => _style;
        //    set
        //    {
        //        if (_style != value)
        //        {
        //            if (_style != null)
        //                _style.Owner = null;

        //            _style       = value;
        //            _style.Owner = this;
        //            MarkDirtyAndPropagate();
        //        }
        //    }
        //}

        /// <inheritdoc />
        public bool Equals(YogaNode other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            var result =
                Equals(Context, other.Context)     &&
                HasNewLayout == other.HasNewLayout &&
                _isDirty     == other._isDirty     &&
                NodeType     == other.NodeType     &&
                LineIndex    == other.LineIndex;
            result = result                         &&
                Flex.Equals(other.Flex)             &&
                FlexGrow.Equals(other.FlexGrow)     &&
                FlexShrink.Equals(other.FlexShrink) &&
                AlignContent == other.AlignContent;
            result = result                      &&
                Equals(Margin,   other.Margin)   &&
                Equals(Position, other.Position) &&
                Equals(Padding,  other.Padding)  &&
                Equals(Border,   other.Border);
            result = result                    &&
                AlignItems == other.AlignItems &&
                AlignSelf  == other.AlignSelf  &&
                Direction  == other.Direction  &&
                Display    == other.Display;
            result = result                            &&
                Equals(FlexBasis, other.FlexBasis)     &&
                FlexDirection  == other.FlexDirection  &&
                FlexWrap       == other.FlexWrap       &&
                JustifyContent == other.JustifyContent &&
                Overflow       == other.Overflow       &&
                PositionType   == other.PositionType   &&
                AspectRatio.Equals(other.AspectRatio);
            result = result                        &&
                Equals(Width,     other.Width)     &&
                Equals(Height,    other.Height)    &&
                Equals(MinWidth,  other.MinWidth)  &&
                Equals(MinHeight, other.MinHeight) &&
                Equals(MaxWidth,  other.MaxWidth)  &&
                Equals(MaxHeight, other.MaxHeight);


            result = result &&
                Equals(_layout, other._layout);
            result = result &&
                Children.SequenceEqual(other.Children);
            result = result                       &&
                Config            == other.Config &&
                ResolvedDimension == other.ResolvedDimension;
            return result;
        }


        /// <inheritdoc />
        public AlignType AlignContent
        {
            get => _alignContent;
            set
            {
                if (_alignContent != value)
                {
                    _alignContent = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public AlignType AlignItems
        {
            get => _alignItems;
            set
            {
                if (_alignItems != value)
                {
                    _alignItems = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public AlignType AlignSelf
        {
            get => _alignSelf;
            set
            {
                if (_alignSelf != value)
                {
                    _alignSelf = value;
                    MarkDirtyAndPropagate();
                }
            }
        }


        /// <inheritdoc />
        // Yoga specific properties, not compatible with flexbox specification
        public float? AspectRatio
        {
            get => _aspectRatio;
            set
            {
                if (!FloatOptionalEqual(_aspectRatio, value))
                {
                    _aspectRatio = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Edges Border
        {
            get => _border;
            set
            {
                if (_border != value)
                {
                    _border       = value;
                    _border.Owner = this;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public DirectionType Direction
        {
            get => _direction;
            set
            {
                if (_direction != value)
                {
                    _direction = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public DisplayType Display
        {
            get => _display;
            set
            {
                if (_display != value)
                {
                    _display = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public float? Flex
        {
            get => _flex;
            set
            {
                if (!FloatOptionalEqual(_flex, value))
                {
                    _flex = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Value FlexBasis
        {
            get => _flexBasis;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (value.Unit == ValueUnit.Percent && value.Number.IsNaN())
                    value = Value.Auto;

                if (_flexBasis != value)
                {
                    _flexBasis = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public FlexDirectionType FlexDirection
        {
            get => _flexDirection;
            set
            {
                if (_flexDirection != value)
                {
                    _flexDirection = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public float? FlexGrow
        {
            get => _flexGrow;
            set
            {
                if (!FloatOptionalEqual(_flexGrow, value))
                {
                    _flexGrow = value.IsNaN() ? null : value;
                    MarkDirtyAndPropagate();
                }
            }
        }


        /// <inheritdoc />
        public float? FlexShrink
        {
            get
            {
                if (!_flexShrink.HasValue)
                {
                    if (Config?.UseWebDefaults ?? false)
                        return WebDefaultFlexShrink;
                    return DefaultFlexShrink;
                }

                return _flexShrink;
            }
            set
            {
                if (!FloatOptionalEqual(_flexShrink, value))
                {
                    _flexShrink = value.IsNaN() ? null : value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public WrapType FlexWrap
        {
            get => _flexWrap;
            set
            {
                if (_flexWrap != value)
                {
                    _flexWrap = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Value Height
        {
            get => _dimensions.Height;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (value.Unit == ValueUnit.Percent && value.Number.IsNaN())
                    value = Value.Auto;

                if (_dimensions.Height != value)
                {
                    _dimensions.Height = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public JustifyType JustifyContent
        {
            get => _justifyContent;
            set
            {
                if (_justifyContent != value)
                {
                    _justifyContent = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Edges Margin
        {
            get => _margin;
            set
            {
                if (_margin != value)
                {
                    _margin       = value;
                    _margin.Owner = this;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Value MaxHeight
        {
            get => _maxDimensions.Height;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (_maxDimensions.Height != value)
                {
                    _maxDimensions.Height = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Value MaxWidth
        {
            get => _maxDimensions.Width;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (_maxDimensions.Width != value)
                {
                    _maxDimensions.Width = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Value MinHeight
        {
            get => _minDimensions.Height;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (_minDimensions.Height != value)
                {
                    _minDimensions.Height = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Value MinWidth
        {
            get => _minDimensions.Width;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (_minDimensions.Width != value)
                {
                    _minDimensions.Width = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public OverflowType Overflow
        {
            get => _overflow;
            set
            {
                if (_overflow != value)
                {
                    _overflow = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Edges Padding
        {
            get => _padding;
            set
            {
                if (_padding != value)
                {
                    _padding       = value;
                    _padding.Owner = this;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Edges Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position       = value;
                    _position.Owner = this;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public PositionType PositionType
        {
            get => _positionType;
            set
            {
                if (_positionType != value)
                {
                    _positionType = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        /// <inheritdoc />
        public Value Width
        {
            get => _dimensions.Width;
            set
            {
                if ((value.Unit == ValueUnit.Undefined || value.Unit == ValueUnit.Auto) && !value.Number.IsNaN())
                    value = new Value(float.NaN, value.Unit);
                if (value.Unit == ValueUnit.Percent && value.Number.IsNaN())
                    value = Value.Auto;

                if (_dimensions.Width != value)
                {
                    _dimensions.Width = value;
                    MarkDirtyAndPropagate();
                }
            }
        }

        public Value Dimension(DimensionType dim)
        {
            return _dimensions[dim];
        }

        public Value MaxDimension(DimensionType dim)
        {
            return _maxDimensions[dim];
        }

        public Value MinDimension(DimensionType dim)
        {
            return _minDimensions[dim];
        }

        /// <inheritdoc />
        public float? GetFlexShrink()
        {
            return _flexShrink;
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
            return Equals(obj as YogaNode);
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
            if (axis.IsRow()                             &&
                Border.Start.Unit != ValueUnit.Undefined &&
                Border.Start.Number.HasValue()           &&
                Border.Start.Number >= 0.0f)
                return Border.Start.Number;

            var computedEdgeValue =
                Border.ComputedEdgeValue(axis.ToLeadingEdge(), ValueZero).Number;
            return FloatMax(computedEdgeValue, 0.0f);
        }

        public float GetLeadingMargin(FlexDirectionType axis, float widthSize)
        {
            if (axis.IsRow() &&
                Margin.Start.Unit != ValueUnit.Undefined)
                return ResolveValueMargin(Margin.Start, widthSize);

            return ResolveValueMargin(
                Margin.ComputedEdgeValue(axis.ToLeadingEdge(), ValueZero),
                widthSize);
        }

        public float GetLeadingPadding(FlexDirectionType axis, float widthSize)
        {
            var paddingEdgeStart =
                Padding.Start.ResolveValue(widthSize);
            if (axis.IsRow()                              &&
                Padding.Start.Unit != ValueUnit.Undefined &&
                paddingEdgeStart.HasValue()               && paddingEdgeStart > 0.0f)
                return paddingEdgeStart;

            var resolvedValue = Padding.ComputedEdgeValue(axis.ToLeadingEdge(), ValueZero).ResolveValue(widthSize);
            return FloatMax(resolvedValue, 0.0f);
        }

        public float GetLeadingPaddingAndBorder(FlexDirectionType axis, float widthSize)
        {
            return GetLeadingPadding(axis, widthSize) + GetLeadingBorder(axis);
        }

        public float GetLeadingPosition(FlexDirectionType axis, float axisSize)
        {
            if (axis.IsRow())
            {
                var leadingPosition =
                    Position.ComputedEdgeValue(EdgeType.Start, ValueUndefined);
                if (leadingPosition.Unit != ValueUnit.Undefined)
                    return leadingPosition.ResolveValue(axisSize);
            }

            var leadingPos =
                Position.ComputedEdgeValue(axis.ToLeadingEdge(), ValueUndefined);

            return leadingPos.Unit == ValueUnit.Undefined
                ? 0
                : leadingPos.ResolveValue(axisSize);
        }

        public float GetMarginForAxis(FlexDirectionType axis, float widthSize)
        {
            return GetLeadingMargin(axis, widthSize) + GetTrailingMargin(axis, widthSize);
        }

        public float GetTrailingBorder(FlexDirectionType flexDirection)
        {
            if (flexDirection.IsRow()                  &&
                Border.End.Unit != ValueUnit.Undefined &&
                Border.End.Number.HasValue()           &&
                Border.End.Number >= 0.0f)
                return Border.End.Number;

            var computedEdgeValue = Border.ComputedEdgeValue(flexDirection.ToTrailingEdge(), ValueZero).Number;
            return FloatMax(computedEdgeValue, 0.0f);
        }

        public float GetTrailingMargin(FlexDirectionType axis, float widthSize)
        {
            if (axis.IsRow() &&
                Margin.End.Unit != ValueUnit.Undefined)
                return ResolveValueMargin(Margin.End, widthSize);

            return ResolveValueMargin(
                Margin.ComputedEdgeValue(axis.ToTrailingEdge(), ValueZero),
                widthSize);
        }

        public float GetTrailingPadding(FlexDirectionType axis, float widthSize)
        {
            if (axis.IsRow()                                   &&
                Padding.End.Unit != ValueUnit.Undefined        &&
                Padding.End.ResolveValue(widthSize).HasValue() &&
                Padding.End.ResolveValue(widthSize) >= 0.0f)
                return Padding.End.ResolveValue(widthSize);

            var resolvedValue = Padding.ComputedEdgeValue(axis.ToTrailingEdge(), ValueZero).ResolveValue(widthSize);

            return FloatMax(resolvedValue, 0.0f);
        }

        public float GetTrailingPaddingAndBorder(FlexDirectionType axis, float widthSize)
        {
            return GetTrailingPadding(axis, widthSize) + GetTrailingBorder(axis);
        }

        public float GetTrailingPosition(FlexDirectionType axis, float axisSize)
        {
            if (axis.IsRow())
            {
                var trailingPosition = Position.ComputedEdgeValue(EdgeType.End, ValueUndefined);
                if (trailingPosition.Unit != ValueUnit.Undefined)
                    return trailingPosition.ResolveValue(axisSize);
            }

            var trailingPos = Position.ComputedEdgeValue(axis.ToTrailingEdge(), ValueUndefined);
            return trailingPos.Unit == ValueUnit.Undefined
                ? 0
                : trailingPos.ResolveValue(axisSize);
        }

        public bool IsLayoutTreeEqualToNode(YogaNode node)
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
                Position.ComputedEdgeValue(EdgeType.Start, ValueUndefined).Unit != ValueUnit.Undefined ||
                Position.ComputedEdgeValue(axis.ToLeadingEdge(), ValueUndefined).Unit != ValueUnit.Undefined;
        }

        public bool IsNodeFlexible()
        {
            return PositionType == PositionType.Relative &&
                (!FloatEqual(ResolveFlexGrow(), 0) || !FloatEqual(ResolveFlexShrink(), 0));
        }

        public bool IsTrailingPosDefined(FlexDirectionType axis)
        {
            return axis.IsRow() &&
                Position.ComputedEdgeValue(EdgeType.End, ValueUndefined).Unit != ValueUnit.Undefined ||
                Position.ComputedEdgeValue(axis.ToTrailingEdge(), ValueUndefined).Unit !=
                ValueUnit.Undefined;
        }

        public Value MarginLeadingValue(FlexDirectionType axis)
        {
            if (axis.IsRow() && Margin.Start.Unit != ValueUnit.Undefined)
                return Margin.Start;
            return Margin[axis.ToLeadingEdge()];
        }

        public Value MarginTrailingValue(FlexDirectionType axis)
        {
            if (axis.IsRow() && Margin.End.Unit != ValueUnit.Undefined)
                return Margin.End;
            return Margin[axis.ToTrailingEdge()];
        }

        public void MarkDirty()
        {
            YogaGlobal.YogaAssert(
                this,
                MeasureFunc == null,
                "Only leaf nodes with custom measure functions should manually mark themselves as dirty");

            MarkDirtyAndPropagate();
        }

        public static bool operator ==(YogaNode left, YogaNode right)
        {
            return (object) left == null || (object) right == null ? ReferenceEquals(left, right) : Equals(left, right);
        }

        public static bool operator !=(YogaNode left, YogaNode right)
        {
            return !(left == right);
        }

        public void Print(PrintOptionType options)
        {
            YogaGlobal.Log(this, LogLevel.Debug, new NodePrint(this, options).ToString());
        }

        public void ResolveDimension()
        {
            if (MaxWidth.Unit != ValueUnit.Undefined &&
                ValueEqual(MaxWidth, MinWidth))
                ResolvedDimension.Width = MaxWidth;
            else
                ResolvedDimension.Width = Width;

            if (MaxHeight.Unit != ValueUnit.Undefined &&
                ValueEqual(MaxHeight, MinHeight))
                ResolvedDimension.Height = MaxHeight;
            else
                ResolvedDimension.Height = Height;
        }

        public DirectionType ResolveDirection(DirectionType ownerDirection)
        {
            if (Direction == DirectionType.Inherit)
                return ownerDirection > DirectionType.Inherit
                    ? ownerDirection
                    : DirectionType.LTR;
            return Direction;
        }

        public Value ResolveFlexBasisPtr()
        {
            var flexBasis = FlexBasis;
            if (flexBasis.Unit != ValueUnit.Auto && flexBasis.Unit != ValueUnit.Undefined)
                return flexBasis;
            if (Flex.HasValue && Flex > 0.0f)
                return Config.UseWebDefaults ? ValueAuto : ValueZero;
            return ValueAuto;
        }

        public float ResolveFlexGrow()
        {
            // Root nodes flexGrow should always be 0
            if (Owner == null) return 0.0f;
            if (FlexGrow.HasValue)
                return FlexGrow.Value;
            if (Flex.HasValue && Flex > 0.0f)
                return Flex.Value;
            return DefaultFlexGrow;
        }

        public float ResolveFlexShrink()
        {
            if (Owner == null) return 0.0f;
            if (FlexShrink.HasValue)
                return FlexShrink.Value;
            if (!Config.UseWebDefaults && Flex.HasValue &&
                Flex < 0.0f)
                return -Flex.Value;
            return Config.UseWebDefaults ? WebDefaultFlexShrink : DefaultFlexShrink;
        }

        public void SetChildren(IEnumerable<YogaNode> children)
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
            // Root nodes should be always laid-out as LTR, so we don't return negative values. 
            var directionRespectingRoot = Owner != null ? direction : DirectionType.LTR;
            var mainAxis                = EnumExtensions.ResolveFlexDirection(FlexDirection, directionRespectingRoot);
            var crossAxis               = EnumExtensions.FlexDirectionCross(mainAxis, directionRespectingRoot);

            var relativePositionMain  = RelativePosition(mainAxis,  mainSize);
            var relativePositionCross = RelativePosition(crossAxis, crossSize);

            Layout.Position[mainAxis.ToLeadingEdge()]   = GetLeadingMargin(mainAxis, ownerWidth)   + relativePositionMain;
            Layout.Position[mainAxis.ToTrailingEdge()]  = GetTrailingMargin(mainAxis, ownerWidth)  + relativePositionMain;
            Layout.Position[crossAxis.ToLeadingEdge()]  = GetLeadingMargin(crossAxis, ownerWidth)  + relativePositionCross;
            Layout.Position[crossAxis.ToTrailingEdge()] = GetTrailingMargin(crossAxis, ownerWidth) + relativePositionCross;
        }

        public void SetStyle(INodeStyle style)
        {
            Direction      = style.Direction;
            FlexDirection  = style.FlexDirection;
            JustifyContent = style.JustifyContent;
            AlignContent   = style.AlignContent;
            AlignItems     = style.AlignItems;
            AlignSelf      = style.AlignSelf;
            PositionType   = style.PositionType;
            FlexWrap       = style.FlexWrap;
            Overflow       = style.Overflow;
            Display        = style.Display;
            Flex           = style.Flex;
            FlexGrow       = style.FlexGrow;
            FlexShrink     = style.GetFlexShrink();
            FlexBasis      = style.FlexBasis;
            Margin         = style.Margin.Clone();
            Position       = style.Position.Clone();
            Padding        = style.Padding.Clone();
            Border         = style.Border.Clone();
            Width          = style.Width;
            Height         = style.Height;
            MinWidth       = style.MinWidth;
            MinHeight      = style.MinHeight;
            MaxWidth       = style.MaxWidth;
            MaxHeight      = style.MaxHeight;
            AspectRatio    = style.AspectRatio;
        }

        public void Traverse(Action<YogaNode> f)
        {
            f(this);
            foreach (var child in Children)
                child.Traverse(f);
        }

        internal AlignType AlignChild(YogaNode child)
        {
            var align = child.AlignSelf == AlignType.Auto
                ? AlignItems
                : child.AlignSelf;
            if (align == AlignType.Baseline && FlexDirection.IsColumn())
                return AlignType.FlexStart;

            return align;
        }

        internal float Baseline()
        {
            if (BaselineFunc != null)
            {
                var baseline = BaselineFunc(
                    this,
                    Layout.MeasuredWidth,
                    Layout.MeasuredHeight);

                YogaGlobal.YogaAssert(
                    this,
                    baseline.HasValue(),
                    "Expect custom baseline function to not return NaN");

                return baseline;
            }

            YogaNode baselineChild = null;
            foreach (var child in Children)
            {
                if (child.LineIndex > 0) break;

                if (child.PositionType == PositionType.Absolute) continue;

                if (AlignChild(child) == AlignType.Baseline)
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

        // Like YGNodeBoundAxisWithinMinAndMax but also ensures that the value doesn't
        // go below the padding and border amount.
        // inline
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal float BoundAxis(
            FlexDirectionType axis,
            float             value,
            float             axisSize,
            float             widthSize)
        {
            return FloatMax(
                BoundAxisWithinMinAndMax(axis, value, axisSize),
                PaddingAndBorderForAxis(axis, widthSize));
        }

        internal float BoundAxisWithinMinAndMax(
            FlexDirectionType axis,
            float             value,
            float             axisSize)
        {
            var min = float.NaN;
            var max = float.NaN;

            if (axis.IsColumn())
            {
                min = MinHeight.ResolveValue(axisSize);
                max = MaxHeight.ResolveValue(axisSize);
            }
            else if (axis.IsRow())
            {
                min = MinWidth.ResolveValue(axisSize);
                max = MaxWidth.ResolveValue(axisSize);
            }

            if (max.HasValue() && max >= 0 && value > max)
                return max;

            if (min.HasValue() && min >= 0 && value < min)
                return min;

            return value;
        }

        // inline
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal float DimensionWithMargin(FlexDirectionType axis, float widthSize)
        {
            return Layout.GetMeasuredDimension(axis.ToDimension()) +
                GetLeadingMargin(axis, widthSize)                  +
                GetTrailingMargin(axis, widthSize);
        }

        internal bool IsBaselineLayout()
        {
            if (FlexDirection.IsColumn())
                return false;

            if (AlignItems == AlignType.Baseline)
                return true;

            foreach (var child in Children)
            {
                if (child.PositionType == PositionType.Relative &&
                    child.AlignSelf    == AlignType.Baseline)
                    return true;
            }

            return false;
        }

        // inline
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool IsLayoutDimensionDefined(FlexDirectionType axis)
        {
            var value = Layout.GetMeasuredDimension(axis.ToDimension());
            return value.HasValue() && value >= 0.0f;
        }

        // inline
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool IsStyleDimensionDefined(
            FlexDirectionType axis,
            float             ownerSize)
        {
            var isUndefined = ResolvedDimension[axis.ToDimension()].IsNaN();
            return !(
                ResolvedDimension[axis.ToDimension()].Unit == ValueUnit.Auto      ||
                ResolvedDimension[axis.ToDimension()].Unit == ValueUnit.Undefined ||
                ResolvedDimension[axis.ToDimension()].Unit == ValueUnit.Point &&
                !isUndefined                                                  &&
                ResolvedDimension[axis.ToDimension()].Number < 0.0f ||
                ResolvedDimension[axis.ToDimension()].Unit == ValueUnit.Percent &&
                !isUndefined                                                    &&
                (ResolvedDimension[axis.ToDimension()].Number < 0.0f || ownerSize.IsNaN()));
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

        internal float PaddingAndBorderForAxis(FlexDirectionType axis, float widthSize)
        {
            return GetLeadingPaddingAndBorder(axis, widthSize)
                + GetTrailingPaddingAndBorder(axis, widthSize);
        }

        internal void ZeroOutLayoutRecursively()
        {
            Layout = new NodeLayout
            {
                MeasuredWidth  = 0,
                MeasuredHeight = 0,
                Width          = 0,
                Height         = 0
            };
            HasNewLayout = true;

            foreach (var child in Children)
                child.ZeroOutLayoutRecursively();
        }

        private void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (YogaNode child in e.NewItems)
                {
                    YogaGlobal.YogaAssert(
                        this,
                        child.Owner == null,
                        "Child already has a owner, it must be removed first.");

                    YogaGlobal.YogaAssert(
                        this,
                        MeasureFunc == null,
                        "Cannot add child: Nodes with measure functions cannot have children.");

                    child.Owner = this;
                }
            }

            if (e.OldItems != null)
            {
                foreach (YogaNode child in e.OldItems)
                    child.Owner = null;
            }

            MarkDirtyAndPropagate();
        }

        // If both left and right are defined, then use left. Otherwise return
        // +left or -right depending on which is defined.
        private float RelativePosition(
            FlexDirectionType axis,
            float             axisSize)
        {
            if (IsLeadingPositionDefined(axis))
                return GetLeadingPosition(axis, axisSize);

            var trailingPosition = GetTrailingPosition(axis, axisSize);
            if (trailingPosition.HasValue())
                trailingPosition = -1 * trailingPosition;
            return trailingPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float ResolveValueMargin(Value value, float ownerSize)
        {
            return value.Unit == ValueUnit.Auto
                ? 0
                : value.ResolveValue(ownerSize);
        }
    }
}
