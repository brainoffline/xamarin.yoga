using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Yoga
{
    using static YGConst;
    using static YGGlobal;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    public class YGNode : IEquatable<YGNode>
    {
        public YGConfigRef Config  { get; set; }
        public string      Name    { get; set; }
        public object      Context { get; set; }

        private YGPrintFunc    print_             = null;
        private bool           hasNewLayout_      = true;
        private YGNodeType     nodeType_          = YGNodeType.Default;
        private YGBaselineFunc baseline_          = null;
        private YGDirtiedFunc  dirtied_           = null;
        private YGLayout       layout_            = new YGLayout();
        private int            lineIndex_         = 0;
        private YGNodeRef      owner_             = null;
        private YGVector       children_          = new YGVector();
        private bool           isDirty_           = false;
        private Dimensions     ResolvedDimensions = new Dimensions(YGValueUndefined, YGValueUndefined);

        public YGNode() { }

        public YGNode(YGConfigRef newConfig)
        {
            Config = newConfig;
        }

        public YGNode(YGNode node)
        {
            if (ReferenceEquals(node, this))
                return;

            children_.Clear();

            Context            = node.Context;
            print_             = node.getPrintFunc();
            hasNewLayout_      = node.getHasNewLayout();
            nodeType_          = node.getNodeType();
            MeasureFunc        = node.MeasureFunc;
            baseline_          = node.getBaseline();
            dirtied_           = node.getDirtied();
            Style              = new YGStyle(node.Style);
            layout_            = new YGLayout(node.layout_);
            lineIndex_         = node.getLineIndex();
            owner_             = null;
            Config             = new YGConfigRef(node.Config);
            isDirty_           = node.IsDirty;
            ResolvedDimensions = node.getResolvedDimensions().Clone();

            foreach (YGNodeRef child in node.getChildren())
                children_.Add(new YGNode(child));
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
                    nodeType_ = YGNodeType.Default;
                }
                else
                {
                    YGAssertWithNode(
                        this,
                        children_.Count == 0,
                        "Cannot set measure function: Nodes with measure functions cannot have children.");

                    // TODO: t18095186 Move nodeType to opt-in function and mark appropriate
                    // places in Litho
                    setNodeType(YGNodeType.Text);
                }

                _measureFunc = value;
            }
        }

        // Getters
        public YGPrintFunc getPrintFunc()
        {
            return print_;
        }

        public bool getHasNewLayout()
        {
            return hasNewLayout_;
        }

        public YGNodeType getNodeType()
        {
            return nodeType_;
        }

        public YGBaselineFunc getBaseline()
        {
            return baseline_;
        }

        public YGDirtiedFunc getDirtied()
        {
            return dirtied_;
        }

        public YGStyle Style { get; set; } = new YGStyle();


        // For Performance reasons passing as reference.
        public YGLayout Layout
        {
            get => layout_;
            set => layout_ = value;
        }

        public int getLineIndex()
        {
            return lineIndex_;
        }

        // returns the YGNodeRef that owns this YGNode. An owner is used to identify
        // the YogaTree that a YGNode belongs to.
        // This method will return the parent of the YGNode when a YGNode only belongs
        // to one YogaTree or null when the YGNode is shared between two or more
        // YogaTrees.
        public YGNodeRef getOwner()
        {
            return owner_;
        }

        [Obsolete("use getOwner() instead")]
        public YGNodeRef getParent()
        {
            return getOwner();
        }

        public YGVector getChildren()
        {
            return children_;
        }

        public YGNodeRef getChild(int index)
        {
            return children_[index];
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
                    dirtied_?.Invoke(this);
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

        public void setPrintFunc(YGPrintFunc printFunc)
        {
            print_ = printFunc;
        }

        public void setHasNewLayout(bool hasNewLayout)
        {
            hasNewLayout_ = hasNewLayout;
        }

        public void setNodeType(YGNodeType nodeType)
        {
            nodeType_ = nodeType;
        }

        public void setStyleFlexDirection(YGFlexDirection direction)
        {
            Style.flexDirection = direction;
        }

        public void setStyleAlignContent(YGAlign alignContent)
        {
            Style.alignContent = alignContent;
        }

        public void setBaseLineFunc(YGBaselineFunc baseLineFunc)
        {
            baseline_ = baseLineFunc;
        }

        public void setDirtiedFunc(YGDirtiedFunc dirtiedFunc)
        {
            dirtied_ = dirtiedFunc;
        }

        public void setLineIndex(int lineIndex)
        {
            lineIndex_ = lineIndex;
        }

        public void setOwner(YGNodeRef owner)
        {
            owner_ = owner;
        }

        public void setChildren(in YGVector children)
        {
            children_ = children;
        }

        // TODO: rvalue override for setChildren

        public void StyleSetPosition(Edges position)
        {
            if (Style.Position != position)
            {
                Style.Position = position;
                markDirtyAndPropogate();
            }
        }

        public void StyleSetMargin(Edges margin)
        {
            if (Style.Margin != margin)
            {
                Style.Margin = margin;
                markDirtyAndPropogate();
            }
        }

        public void StyleSetPadding(Edges padding)
        {
            if (Style.Padding != padding)
            {
                Style.Padding = padding;
                markDirtyAndPropogate();
            }
        }

        public void StyleSetBorder(Edges border)
        {
            if (Style.Border != border)
            {
                Style.Border = border;
                markDirtyAndPropogate();
            }
        }

        public void StyleSetDimensions(float width, float height)
        {
            if (Style.Dimensions.Width != width ||
                Style.Dimensions.Height != height)
            {
                Style.Dimensions.Width = width;
                Style.Dimensions.Height = height;
                markDirtyAndPropogate();
            }
        }

        public void StyleSetAspectRatio(float? aspectRatio)
        {
            if (!YGFloatOptionalEqual(Style.AspectRatio, aspectRatio))
            {
                Style.AspectRatio = aspectRatio;
                markDirtyAndPropogate();
            }
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
                Style.Position.ComputedEdgeValue(trailing[(int) axis], YGConst.YGValueUndefined).unit != YGUnit.Undefined;
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

        // Setters

        public void replaceChild(YGNodeRef child, int index)
        {
            children_[index] = child;
        }

        public void replaceChild(YGNodeRef oldChild, YGNodeRef newChild)
        {
            int index = children_.IndexOf(oldChild);
            if (index >= 0)
                children_[index] = newChild;
        }

        public void insertChild(YGNodeRef child, int index)
        {
            children_.Insert(index, child);
        }

        public bool removeChild(YGNodeRef child)
        {
            if (children_.Contains(child))
            {
                children_.Remove(child);
                return true;
            }

            return false;
        }

        public void removeChild(int index)
        {
            children_.RemoveAt(index);
        }

        public void setLayoutDirection(YGDirection direction)
        {
            layout_.Direction = direction;
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

        public void setPosition(
            in YGDirection direction,
            in float       mainSize,
            in float       crossSize,
            in float       ownerWidth)
        {
            /* Root nodes should be always layouted as LTR, so we don't return negative
             * values. */
            YGDirection     directionRespectingRoot = owner_ != null ? direction : YGDirection.LTR;
            YGFlexDirection mainAxis                = YGResolveFlexDirection(Style.flexDirection, directionRespectingRoot);
            YGFlexDirection crossAxis               = YGFlexDirectionCross(mainAxis, directionRespectingRoot);

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
            YGValue flexBasis = Style.flexBasis;
            if (flexBasis.unit != YGUnit.Auto && flexBasis.unit != YGUnit.Undefined)
                return flexBasis;
            if (Style.flex.HasValue && Style.flex > 0.0f)
                return Config.UseWebDefaults ? YGConst.YGValueAuto : YGConst.YGValueZero;
            return YGConst.YGValueAuto;
        }

        public void resolveDimension()
        {
            if (Style.MaxDimensions.Width.unit != YGUnit.Undefined &&
                YGValueEqual(Style.MaxDimensions.Width, Style.MinDimensions.Width))
                ResolvedDimensions.Width = Style.MaxDimensions.Width;
            else
                ResolvedDimensions.Width = Style.Dimensions.Width;

            if (Style.MaxDimensions.Height.unit != YGUnit.Undefined &&
                YGValueEqual(Style.MaxDimensions.Height, Style.MinDimensions.Height))
                ResolvedDimensions.Height = Style.MaxDimensions.Height;
            else
                ResolvedDimensions.Height = Style.Dimensions.Height;
        }

        public YGDirection resolveDirection(in YGDirection ownerDirection)
        {
            if (Style.direction == YGDirection.Inherit)
                return ownerDirection > YGDirection.Inherit
                    ? ownerDirection
                    : YGDirection.LTR;
            return Style.direction;
        }

        public void ClearChildren()
        {
            children_.Clear();
        }

        // Other Methods

        public void cloneChildrenIfNeeded()
        {
            // YGNodeRemoveChild in yoga.cpp has a forked variant of this algorithm
            // optimized for deletions.

            int childCount = children_.Count;
            if (childCount == 0) return;

            YGNodeRef firstChild = children_.First();
            if (firstChild.getOwner() == this) return;

            for (int i = 0; i < childCount; ++i)
            {
                YGNodeRef oldChild = children_[i];
                YGNodeRef newChild = YGNodeClone(oldChild);
                replaceChild(newChild, i);
                newChild.setOwner(this);
            }
        }

        public void markDirtyAndPropogate()
        {
            if (!isDirty_)
            {
                IsDirty                  = true;
                Layout.ComputedFlexBasis = null;
                owner_?.markDirtyAndPropogate();
            }
        }

        public void markDirtyAndPropogateDownwards()
        {
            isDirty_ = true;
            foreach (YGNodeRef childNode in children_) childNode.markDirtyAndPropogateDownwards();
            ;
        }

        public float resolveFlexGrow()
        {
            // Root nodes flexGrow should always be 0
            if (owner_ == null) return 0.0f;
            if (Style.flexGrow.HasValue)
                return Style.flexGrow.Value;
            if (Style.flex.HasValue && Style.flex > 0.0f)
                return Style.flex.Value;
            return kDefaultFlexGrow;
        }

        public float resolveFlexShrink()
        {
            if (owner_ == null) return 0.0f;
            if (Style.flexShrink.HasValue)
                return Style.flexShrink.Value;
            if (!Config.UseWebDefaults && Style.flex.HasValue &&
                Style.flex < 0.0f)
                return -Style.flex.Value;
            return Config.UseWebDefaults ? kWebDefaultFlexShrink : kDefaultFlexShrink;
        }

        public bool isNodeFlexible()
        {
            return Style.positionType == YGPositionType.Relative &&
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

            float? resolvedValue = YGResolveValue(Style.Padding.ComputedEdgeValue(leading[(int) axis], YGConst.YGValueZero), widthSize);
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

        public bool isLayoutTreeEqualToNode(YGNode node)
        {
            if (children_.Count != node.children_.Count)
                return false;

            if (layout_ != node.layout_)
                return false;

            if (children_.Count == 0)
                return true;

            bool      isLayoutTreeEqual = true;
            YGNodeRef otherNodeChildren = null;
            for (int i = 0; i < children_.Count; ++i)
            {
                otherNodeChildren = node.children_[i];
                isLayoutTreeEqual = children_[i].isLayoutTreeEqualToNode(otherNodeChildren);
                if (!isLayoutTreeEqual)
                    return false;
            }

            return isLayoutTreeEqual;
        }

        /// <inheritdoc />
        public bool Equals(YGNode other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            bool result =
                Equals(Context, other.Context)       &&
                Equals(print_,  other.print_)        &&
                hasNewLayout_ == other.hasNewLayout_ &&
                nodeType_     == other.nodeType_;
            result = result                        &&
                Equals(baseline_, other.baseline_) &&
                Equals(dirtied_,  other.dirtied_);
            result = result &&
                Equals(Style, other.Style);
            result = result &&
                Equals(layout_, other.layout_);
            result = result &&
                lineIndex_ == other.lineIndex_;
            result = result &&
                children_.SequenceEqual(other.children_);
            result = result                          &&
                Config             == other.Config   &&
                isDirty_           == other.isDirty_ &&
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
                int hashCode = (int) nodeType_;
                hashCode = (hashCode * 397) ^ lineIndex_;
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
