using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Xamarin.Yoga
{
    using static YGGlobal;
    using static YGConst;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    public class YGNode : IEquatable<YGNode>
    {
        public YGConfigRef Config { get; set; }

        public           string         Name { get; set; }
        private          object         context_            = null;
        private          YGPrintFunc    print_              = null;
        private          bool           hasNewLayout_       = true;
        private          YGNodeType     nodeType_           = YGNodeType.Default;
        private          YGMeasureFunc  measure_            = null;
        private          YGBaselineFunc baseline_           = null;
        private          YGDirtiedFunc  dirtied_            = null;
        private          YGLayout       layout_             = new YGLayout();
        private          int            lineIndex_          = 0;
        private          YGNodeRef      owner_              = null;
        private          YGVector       children_           = new YGVector();
        private          bool           isDirty_            = false;
        private readonly YGValue[]      resolvedDimensions_ = {YGConst.YGValueUndefined, YGConst.YGValueUndefined};

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

            context_            = node.getContext();
            print_              = node.getPrintFunc();
            hasNewLayout_       = node.getHasNewLayout();
            nodeType_           = node.getNodeType();
            measure_            = node.getMeasure();
            baseline_           = node.getBaseline();
            dirtied_            = node.getDirtied();
            Style               = new YGStyle(node.Style);
            layout_             = new YGLayout(node.layout_);
            lineIndex_          = node.getLineIndex();
            owner_              = null;
            Config              = new YGConfigRef(node.Config);
            isDirty_            = node.IsDirty;
            resolvedDimensions_ = (YGValue[]) node.getResolvedDimensions().Clone();

            foreach (var child in node.getChildren())
                children_.Add(new YGNode(child));
        }

        // Getters
        public object getContext()
        {
            return context_;
        }

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

        public YGMeasureFunc getMeasure()
        {
            return measure_;
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

        public YGValue[] getResolvedDimensions()
        {
            return resolvedDimensions_;
        }

        public YGValue getResolvedDimension(YGDimension dimension)
        {
            return resolvedDimensions_[(int) dimension];
        }

        // Methods related to positions, margin, padding and border
        //public YGFloatOptional getLeadingPosition(
        //    in YGFlexDirection axis,
        //    in float           axisSize);
        //bool                  isLeadingPositionDefined(const YGFlexDirection & axis) const;
        //bool                  isTrailingPosDefined(const     YGFlexDirection & axis) const;
        //YGFloatOptional       getTrailingPosition(
        //const YGFlexDirection & axis,
        //const float           & axisSize) const;
        //YGFloatOptional       getLeadingMargin(
        //const YGFlexDirection & axis,
        //const float           & widthSize) const;
        //YGFloatOptional       getTrailingMargin(
        //const YGFlexDirection & axis,
        //const float           & widthSize) const;
        //float                 getLeadingBorder(const  YGFlexDirection & flexDirection) const;
        //float                 getTrailingBorder(const YGFlexDirection & flexDirection) const;
        //YGFloatOptional       getLeadingPadding(
        //const YGFlexDirection & axis,
        //const float           & widthSize) const;
        //YGFloatOptional       getTrailingPadding(
        //const YGFlexDirection & axis,
        //const float           & widthSize) const;
        //YGFloatOptional       getLeadingPaddingAndBorder(
        //const YGFlexDirection & axis,
        //const float           & widthSize) const;
        //YGFloatOptional       getTrailingPaddingAndBorder(
        //const YGFlexDirection & axis,
        //const float           & widthSize) const;
        //YGFloatOptional       getMarginForAxis(
        //const YGFlexDirection & axis,
        //const float           & widthSize) const;

        // Setters
        public void setContext(object context)
        {
            context_ = context;
        }

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


        public YGFloatOptional getLeadingPosition(
            in YGFlexDirection axis,
            in float           axisSize)
        {
            if (YGFlexDirectionIsRow(axis))
            {
                var leadingPosition =
                    YGComputedEdgeValue(Style.position, YGEdge.Start, YGConst.YGValueUndefined);
                if (leadingPosition.unit != YGUnit.Undefined) return YGResolveValue(leadingPosition, axisSize);
            }

            var leadingPos =
                YGComputedEdgeValue(Style.position, leading[(int) axis], YGConst.YGValueUndefined);

            return leadingPos.unit == YGUnit.Undefined
                ? new YGFloatOptional(0)
                : YGResolveValue(leadingPos, axisSize);
        }

        public YGFloatOptional getTrailingPosition(
            in YGFlexDirection axis,
            in float           axisSize)
        {
            if (YGFlexDirectionIsRow(axis))
            {
                var trailingPosition =
                    YGComputedEdgeValue(Style.position, YGEdge.End, YGConst.YGValueUndefined);
                if (trailingPosition.unit != YGUnit.Undefined) return YGResolveValue(trailingPosition, axisSize);
            }

            var trailingPos =
                YGComputedEdgeValue(Style.position, trailing[(int) axis], YGConst.YGValueUndefined);

            return trailingPos.unit == YGUnit.Undefined
                ? new YGFloatOptional(0)
                : YGResolveValue(trailingPos, axisSize);
        }

        public bool isLeadingPositionDefined(in YGFlexDirection axis)
        {
            return YGFlexDirectionIsRow(axis) &&
                YGComputedEdgeValue(Style.position, YGEdge.Start, YGConst.YGValueUndefined)
                    .unit != YGUnit.Undefined ||
                YGComputedEdgeValue(Style.position, leading[(int) axis], YGConst.YGValueUndefined)
                    .unit != YGUnit.Undefined;
        }

        public bool isTrailingPosDefined(in YGFlexDirection axis)
        {
            return YGFlexDirectionIsRow(axis) &&
                YGComputedEdgeValue(Style.position, YGEdge.End, YGConst.YGValueUndefined).unit != YGUnit.Undefined ||
                YGComputedEdgeValue(Style.position, trailing[(int) axis], YGConst.YGValueUndefined).unit != YGUnit.Undefined;
        }

        public YGFloatOptional getLeadingMargin(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            if (YGFlexDirectionIsRow(axis) &&
                Style.margin[(int) YGEdge.Start].unit != YGUnit.Undefined)
                return YGResolveValueMargin(Style.margin[(int) YGEdge.Start], widthSize);

            return YGResolveValueMargin(
                YGComputedEdgeValue(Style.margin, leading[(int) axis], YGConst.YGValueZero),
                widthSize);
        }

        public YGFloatOptional getTrailingMargin(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            if (YGFlexDirectionIsRow(axis) &&
                Style.margin[(int) YGEdge.End].unit != YGUnit.Undefined)
                return YGResolveValueMargin(Style.margin[(int) YGEdge.End], widthSize);

            return YGResolveValueMargin(
                YGComputedEdgeValue(Style.margin, trailing[(int) axis], YGConst.YGValueZero),
                widthSize);
        }

        public YGFloatOptional getMarginForAxis(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            return getLeadingMargin(axis, widthSize) + getTrailingMargin(axis, widthSize);
        }

        // Setters

        public void setMeasureFunc(YGMeasureFunc measureFunc)
        {
            if (measureFunc == null)
            {
                measure_ = null;
                // TODO: t18095186 Move nodeType to opt-in function and mark appropriate
                // places in Litho
                nodeType_ = YGNodeType.Default;
            }
            else
            {
                YGAssertWithNode(
                    this,
                    children_.Count == 0,
                    "Cannot set measure function: Nodes with measure functions cannot have children.");
                measure_ = measureFunc;
                // TODO: t18095186 Move nodeType to opt-in function and mark appropriate
                // places in Litho
                setNodeType(YGNodeType.Text);
            }

            measure_ = measureFunc;
        }

        public void replaceChild(YGNodeRef child, int index)
        {
            children_[index] = child;
        }

        public void replaceChild(YGNodeRef oldChild, YGNodeRef newChild)
        {
            var index = children_.IndexOf(oldChild);
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

        public void setLayoutMargin(float margin, YGEdge edge)
        {
            layout_.margin[(int) edge] = margin;
        }

        public void setLayoutBorder(float border, YGEdge edge)
        {
            layout_.border[(int) edge] = border;
        }

        public void setLayoutPadding(float padding, YGEdge edge)
        {
            layout_.padding[(int) edge] = padding;
        }

        // If both left and right are defined, then use left. Otherwise return
        // +left or -right depending on which is defined.
        public YGFloatOptional relativePosition(
            in YGFlexDirection axis,
            in float           axisSize)
        {
            if (isLeadingPositionDefined(axis)) return getLeadingPosition(axis, axisSize);

            var trailingPosition = getTrailingPosition(axis, axisSize);
            if (!trailingPosition.isUndefined()) trailingPosition.setValue(-1 * trailingPosition.getValue());
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
            var directionRespectingRoot = owner_ != null ? direction : YGDirection.LTR;
            var mainAxis                = YGResolveFlexDirection(Style.flexDirection, directionRespectingRoot);
            var crossAxis               = YGFlexDirectionCross(mainAxis, directionRespectingRoot);

            var relativePositionMain  = relativePosition(mainAxis,  mainSize);
            var relativePositionCross = relativePosition(crossAxis, crossSize);

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
            if (YGFlexDirectionIsRow(axis) && Style.margin[(int) YGEdge.Start].unit != YGUnit.Undefined)
                return Style.margin[(int) YGEdge.Start];
            return Style.margin[(int) leading[(int) axis]];
        }

        public YGValue marginTrailingValue(in YGFlexDirection axis)
        {
            if (YGFlexDirectionIsRow(axis) && Style.margin[(int) YGEdge.End].unit != YGUnit.Undefined)
                return Style.margin[(int) YGEdge.End];
            return Style.margin[(int) trailing[(int) axis]];
        }

        public YGValue resolveFlexBasisPtr()
        {
            var flexBasis = Style.flexBasis;
            if (flexBasis.unit != YGUnit.Auto && flexBasis.unit        != YGUnit.Undefined) return flexBasis;
            if (!Style.flex.isUndefined()     && Style.flex.getValue() > 0.0f) return Config.UseWebDefaults ? YGConst.YGValueAuto : YGConst.YGValueZero;
            return YGConst.YGValueAuto;
        }

        public void resolveDimension()
        {
            for (var dim = (int) YGDimension.Width; dim < YGDimensionCount; dim++)
            {
                if (Style.maxDimensions[dim].unit != YGUnit.Undefined &&
                    YGValueEqual(Style.maxDimensions[dim], Style.minDimensions[dim]))
                    resolvedDimensions_[dim] = Style.maxDimensions[dim];
                else
                    resolvedDimensions_[dim] = Style.dimensions[dim];
            }
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

            var childCount = children_.Count;
            if (childCount == 0) return;

            var firstChild = children_.First();
            if (firstChild.getOwner() == this) return;

            for (var i = 0; i < childCount; ++i)
            {
                var       oldChild = children_[i];
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
                Layout.ComputedFlexBasis = new YGFloatOptional();
                owner_?.markDirtyAndPropogate();
            }
        }

        public void markDirtyAndPropogateDownwards()
        {
            isDirty_ = true;
            foreach (var childNode in children_) childNode.markDirtyAndPropogateDownwards();
            ;
        }

        public float resolveFlexGrow()
        {
            // Root nodes flexGrow should always be 0
            if (owner_ == null) return 0.0f;
            if (!Style.flexGrow.isUndefined()) return Style.flexGrow.getValue();
            if (!Style.flex.isUndefined() && Style.flex.getValue() > 0.0f) return Style.flex.getValue();
            return kDefaultFlexGrow;
        }

        public float resolveFlexShrink()
        {
            if (owner_ == null) return 0.0f;
            if (!Style.flexShrink.isUndefined()) return Style.flexShrink.getValue();
            if (!Config.UseWebDefaults && !Style.flex.isUndefined() &&
                Style.flex.getValue() < 0.0f)
                return -Style.flex.getValue();
            return Config.UseWebDefaults ? kWebDefaultFlexShrink : kDefaultFlexShrink;
        }

        public bool isNodeFlexible()
        {
            return Style.positionType == YGPositionType.Relative &&
                (resolveFlexGrow() != 0 || resolveFlexShrink() != 0);
        }

        public float getLeadingBorder(in YGFlexDirection axis)
        {
            if (YGFlexDirectionIsRow(axis)                                &&
                Style.border[(int) YGEdge.Start].unit != YGUnit.Undefined &&
                !isUndefined(Style.border[(int) YGEdge.Start].value)      &&
                Style.border[(int) YGEdge.Start].value >= 0.0f)
                return Style.border[(int) YGEdge.Start].value;

            var computedEdgeValue =
                YGComputedEdgeValue(Style.border, leading[(int) axis], YGConst.YGValueZero).value;
            return YGFloatMax(computedEdgeValue, 0.0f);
        }

        public float getTrailingBorder(in YGFlexDirection flexDirection)
        {
            if (YGFlexDirectionIsRow(flexDirection)                     &&
                Style.border[(int) YGEdge.End].unit != YGUnit.Undefined &&
                !isUndefined(Style.border[(int) YGEdge.End].value)      &&
                Style.border[(int) YGEdge.End].value >= 0.0f)
                return Style.border[(int) YGEdge.End].value;

            var computedEdgeValue =
                YGComputedEdgeValue(Style.border, trailing[(int) flexDirection], YGConst.YGValueZero)
                    .value;
            return YGFloatMax(computedEdgeValue, 0.0f);
        }

        public YGFloatOptional getLeadingPadding(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            var paddingEdgeStart =
                YGResolveValue(Style.padding[(int) YGEdge.Start], widthSize);
            if (YGFlexDirectionIsRow(axis)                                 &&
                Style.padding[(int) YGEdge.Start].unit != YGUnit.Undefined &&
                !paddingEdgeStart.isUndefined()                            && paddingEdgeStart.getValue() > 0.0f)
                return paddingEdgeStart;

            var resolvedValue = YGResolveValue(YGComputedEdgeValue(Style.padding, leading[(int) axis], YGConst.YGValueZero), widthSize);
            return YGFloatOptionalMax(resolvedValue, new YGFloatOptional(0.0f));
        }

        public YGFloatOptional getTrailingPadding(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            if (YGFlexDirectionIsRow(axis)                                                &&
                Style.padding[(int) YGEdge.End].unit != YGUnit.Undefined                  &&
                !YGResolveValue(Style.padding[(int) YGEdge.End], widthSize).isUndefined() &&
                YGResolveValue(Style.padding[(int) YGEdge.End], widthSize).getValue() >= 0.0f)
                return YGResolveValue(Style.padding[(int) YGEdge.End], widthSize);

            var resolvedValue = YGResolveValue(
                YGComputedEdgeValue(Style.padding, trailing[(int) axis], YGConst.YGValueZero),
                widthSize);

            return YGFloatOptionalMax(resolvedValue, new YGFloatOptional(0.0f));
        }

        public YGFloatOptional getLeadingPaddingAndBorder(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            return getLeadingPadding(axis, widthSize) + new YGFloatOptional(getLeadingBorder(axis));
        }

        public YGFloatOptional getTrailingPaddingAndBorder(
            in YGFlexDirection axis,
            in float           widthSize)
        {
            return getTrailingPadding(axis, widthSize) + new YGFloatOptional(getTrailingBorder(axis));
        }

        public bool isLayoutTreeEqualToNode(YGNode node)
        {
            if (children_.Count != node.children_.Count)
                return false;

            if (layout_ != node.layout_)
                return false;

            if (children_.Count == 0)
                return true;

            var       isLayoutTreeEqual = true;
            YGNodeRef otherNodeChildren = null;
            for (var i = 0; i < children_.Count; ++i)
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

            var result =
                Equals(context_, other.context_)     &&
                Equals(print_,   other.print_)       &&
                hasNewLayout_ == other.hasNewLayout_ &&
                nodeType_     == other.nodeType_     &&
                Equals(measure_, other.measure_);
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
            result = result                &&
                Config   == other.Config   &&
                isDirty_ == other.isDirty_ &&
                resolvedDimensions_.SequenceEqual(other.resolvedDimensions_);
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
                var hashCode = (int) nodeType_;
                hashCode = (hashCode * 397) ^ (measure_?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ lineIndex_;
                hashCode = (hashCode * 397) ^ (resolvedDimensions_ != null ? resolvedDimensions_.GetHashCode() : 0);
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
