using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Yoga
{
    using static YGGlobal;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    public class YGNode
    {
        private object context_ = null;
        private YGPrintFunc print_ = null;
        private bool hasNewLayout_ = true;
        private YGNodeType nodeType_ = YGNodeType.Default;
        private YGMeasureFunc measure_ = null;
        private YGBaselineFunc baseline_ = null;
        private YGDirtiedFunc dirtied_ = null;
        private YGStyle style_ = new YGStyle();
        private YGLayout layout_ = new YGLayout();
        private int lineIndex_ = 0;
        private YGNodeRef owner_ = null;
        private YGVector children_ = new YGVector();
        private YGConfigRef config_ = null;
        private bool isDirty_ = false;
        private readonly YGValue[] resolvedDimensions_ = { YGValueUndefined, YGValueUndefined };

        public YGNode() { }

        public YGNode(in YGConfigRef newConfig)
        {
            config_ = newConfig;
        }

        public YGNode(YGNode node)
        {
            if (ReferenceEquals(node, this))
                return;

            foreach (YGNodeRef child in children_)
            {
                // delete child;
            }

            context_ = node.getContext();
            print_ = node.getPrintFunc();
            hasNewLayout_ = node.getHasNewLayout();
            nodeType_ = node.getNodeType();
            measure_ = node.getMeasure();
            baseline_ = node.getBaseline();
            dirtied_ = node.getDirtied();
            style_ = node.style_;
            layout_ = node.layout_;
            lineIndex_ = node.getLineIndex();
            owner_ = node.getOwner();
            children_ = node.getChildren();
            config_ = node.getConfig();
            isDirty_ = node.isDirty();
            resolvedDimensions_ = node.getResolvedDimensions();
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

        public YGStyle getStyle()
        {
            return style_;
        }

        // For Performance reasons passing as reference.
        public YGLayout getLayout()
        {
            return layout_;
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

        public YGConfigRef getConfig()
        {
            return config_;
        }

        public bool isDirty()
        {
            return isDirty_;
        }

        public YGValue[] getResolvedDimensions()
        {
            return resolvedDimensions_;
        }

        public YGValue getResolvedDimension(YGDimension dimension)
        {
            return resolvedDimensions_[(int)dimension];
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
            style_.flexDirection = direction;
        }

        public void setStyleAlignContent(YGAlign alignContent)
        {
            style_.alignContent = alignContent;
        }

        public void setBaseLineFunc(YGBaselineFunc baseLineFunc)
        {
            baseline_ = baseLineFunc;
        }

        public void setDirtiedFunc(YGDirtiedFunc dirtiedFunc)
        {
            dirtied_ = dirtiedFunc;
        }

        public void setStyle(in YGStyle style)
        {
            style_ = style;
        }

        public void setLayout(in YGLayout layout)
        {
            layout_ = layout;
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

        public void setConfig(YGConfigRef config)
        {
            config_ = config;
        }

        //void setDirty(bool                           isDirty);
        //void setLayoutLastOwnerDirection(YGDirection direction);
        //void setLayoutComputedFlexBasis(const YGFloatOptional & computedFlexBasis);

        //void setLayoutComputedFlexBasisGeneration(
        //    uint computedFlexBasisGeneration);

        //void              setLayoutMeasuredDimension(float measuredDimension, int index);
        //void              setLayoutHadOverflow(bool        hadOverflow);
        //void              setLayoutDimension(float         dimension, int index);
        //void              setLayoutDirection(YGDirection   direction);
        //void              setLayoutMargin(float            margin,   int index);
        //void              setLayoutBorder(float            border,   int index);
        //void              setLayoutPadding(float           padding,  int index);
        //void              setLayoutPosition(float          position, int index);
        //void              setPosition(
        //const YGDirection direction, 
        //const float       mainSize,  
        //const float       crossSize, 
        //const float       ownerWidth);
        //void              setAndPropogateUseLegacyFlag(bool         useLegacyFlag);
        //void              setLayoutDoesLegacyFlagAffectsLayout(bool doesLegacyFlagAffectsLayout);
        //void              setLayoutDidUseLegacyFlag(bool            didUseLegacyFlag);
        //void              markDirtyAndPropogateDownwards();

        // Other methods
        //YGValue     marginLeadingValue(const  YGFlexDirection axis) const;
        //YGValue     marginTrailingValue(const YGFlexDirection axis) const;
        //YGValue     resolveFlexBasisPtr() const;
        //void        resolveDimension();
        //YGDirection resolveDirection(const YGDirection ownerDirection);
        //void        clearChildren();

        // Replaces the occurrences of oldChild with newChild
        //void replaceChild(YGNodeRef oldChild, YGNodeRef newChild);
        //void replaceChild(YGNodeRef child, uint index);
        //void insertChild(YGNodeRef  child, uint index);

        // Removes the first occurrence of child
        //bool removeChild(YGNodeRef child);
        //void removeChild(uint index);

        //void  cloneChildrenIfNeeded();
        //void  markDirtyAndPropogate();
        //float resolveFlexGrow();
        //float resolveFlexShrink();
        //bool  isNodeFlexible();
        //bool  didUseLegacyFlag();
        //bool  isLayoutTreeEqualToNode(const YGNode & node) const;


        public YGFloatOptional getLeadingPosition(
            in YGFlexDirection axis,
            in float axisSize)
        {
            if (YGFlexDirectionIsRow(axis))
            {
                YGValue leadingPosition =
                    YGComputedEdgeValue(style_.position, YGEdge.Start, YGValueUndefined);
                if (leadingPosition.unit != YGUnit.Undefined)
                {
                    return YGResolveValue(leadingPosition, axisSize);
                }
            }

            YGValue leadingPos =
                YGComputedEdgeValue(style_.position, leading[(int)axis], YGValueUndefined);

            return leadingPos.unit == YGUnit.Undefined
                ? new YGFloatOptional(0)
                : YGResolveValue(leadingPos, axisSize);
        }

        public YGFloatOptional getTrailingPosition(
            in YGFlexDirection axis,
            in float axisSize)
        {
            if (YGFlexDirectionIsRow(axis))
            {
                YGValue trailingPosition =
                YGComputedEdgeValue(style_.position, YGEdge.End, YGValueUndefined);
                if (trailingPosition.unit != YGUnit.Undefined)
                {
                    return YGResolveValue(trailingPosition, axisSize);
                }
            }

            YGValue trailingPos =
                YGComputedEdgeValue(style_.position, trailing[(int)axis], YGValueUndefined);

            return trailingPos.unit == YGUnit.Undefined
                ? new YGFloatOptional(0)
                : YGResolveValue(trailingPos, axisSize);
        }

        public bool isLeadingPositionDefined(in YGFlexDirection axis)
        {
            return (YGFlexDirectionIsRow(axis) &&
                    YGComputedEdgeValue(style_.position, YGEdge.Start, YGValueUndefined)
                            .unit != YGUnit.Undefined) ||
                YGComputedEdgeValue(style_.position, leading[(int)axis], YGValueUndefined)
                    .unit != YGUnit.Undefined;
        }

        public bool isTrailingPosDefined(in YGFlexDirection axis)
        {
            return (YGFlexDirectionIsRow(axis) &&
                YGComputedEdgeValue(style_.position, YGEdge.End, YGValueUndefined).unit != YGUnit.Undefined) ||
                YGComputedEdgeValue(style_.position, trailing[(int)axis], YGValueUndefined).unit != YGUnit.Undefined;
        }

        public YGFloatOptional getLeadingMargin(
            in YGFlexDirection axis,
            in float widthSize)
        {
            if (YGFlexDirectionIsRow(axis) &&
                style_.margin[(int)YGEdge.Start].unit != YGUnit.Undefined)
            {
                return YGResolveValueMargin(style_.margin[(int)YGEdge.Start], widthSize);
            }

            return YGResolveValueMargin(
                YGComputedEdgeValue(style_.margin, leading[(int)axis], YGValueZero),
                widthSize);
        }

        public YGFloatOptional getTrailingMargin(
            in YGFlexDirection axis,
            in float widthSize)
        {
            if (YGFlexDirectionIsRow(axis) &&
                style_.margin[(int)YGEdge.End].unit != YGUnit.Undefined)
            {
                return YGResolveValueMargin(style_.margin[(int)YGEdge.End], widthSize);
            }

            return YGResolveValueMargin(
                YGComputedEdgeValue(style_.margin, trailing[(int)axis], YGValueZero),
                widthSize);
        }

        public YGFloatOptional getMarginForAxis(
            in YGFlexDirection axis,
            in float widthSize)
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
            int index = children_.IndexOf(oldChild);
            if (index >= 0)
                children_[index] = newChild;
        }

        public void insertChild(YGNodeRef child, int index)
        {
            children_.Insert(index, child);
        }

        public void setDirty(bool isDirty)
        {
            if (isDirty == isDirty_)
                return;

            isDirty_ = isDirty;
            if (isDirty)
                dirtied_?.Invoke(this);
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
            layout_.direction = direction;
        }

        public void setLayoutMargin(float margin, YGEdge edge)
        {
            layout_.margin[(int)edge] = margin;
        }

        public void setLayoutBorder(float border, YGEdge edge)
        {
            layout_.border[(int)edge] = border;
        }

        public void setLayoutPadding(float padding, YGEdge edge)
        {
            layout_.padding[(int)edge] = padding;
        }

        public void setLayoutLastOwnerDirection(YGDirection direction)
        {
            layout_.lastOwnerDirection = direction;
        }

        public void setLayoutComputedFlexBasis(in YGFloatOptional computedFlexBasis)
        {
            layout_.computedFlexBasis = computedFlexBasis;
        }

        public void setLayoutPosition(float position, YGEdge edge)
        {
            layout_.position[(int)edge] = position;
        }

        public void setLayoutComputedFlexBasisGeneration(int computedFlexBasisGeneration)
        {
            layout_.computedFlexBasisGeneration = computedFlexBasisGeneration;
        }

        public void setLayoutMeasuredDimension(float measuredDimension, YGDimension dimension)
        {
            layout_.measuredDimensions[(int)dimension] = measuredDimension;
        }

        public void setLayoutHadOverflow(bool hadOverflow)
        {
            layout_.hadOverflow = hadOverflow;
        }

        public void setLayoutDimension(float value, YGDimension dimension)
        {
            layout_.dimensions[(int)dimension] = value;
        }

        // If both left and right are defined, then use left. Otherwise return
        // +left or -right depending on which is defined.
        public YGFloatOptional relativePosition(
            in YGFlexDirection axis,
            in float axisSize)
        {
            if (isLeadingPositionDefined(axis))
            {
                return getLeadingPosition(axis, axisSize);
            }

            YGFloatOptional trailingPosition = getTrailingPosition(axis, axisSize);
            if (!trailingPosition.isUndefined())
            {
                trailingPosition.setValue(-1 * trailingPosition.getValue());
            }
            return trailingPosition;
        }

        public void setPosition(
            in YGDirection direction,
            in float mainSize,
            in float crossSize,
            in float ownerWidth)
        {
            /* Root nodes should be always layouted as LTR, so we don't return negative
             * values. */
            YGDirection directionRespectingRoot = owner_ != null ? direction : YGDirection.LTR;
            YGFlexDirection mainAxis = YGResolveFlexDirection(style_.flexDirection, directionRespectingRoot);
            YGFlexDirection crossAxis = YGFlexDirectionCross(mainAxis, directionRespectingRoot);

            YGFloatOptional relativePositionMain = relativePosition(mainAxis, mainSize);
            YGFloatOptional relativePositionCross = relativePosition(crossAxis, crossSize);

            setLayoutPosition(
                YGUnwrapFloatOptional(getLeadingMargin(mainAxis, ownerWidth) + relativePositionMain),
                leading[(int)mainAxis]);
            setLayoutPosition(
                YGUnwrapFloatOptional(getTrailingMargin(mainAxis, ownerWidth) + relativePositionMain),
                trailing[(int)mainAxis]);
            setLayoutPosition(
                YGUnwrapFloatOptional(getLeadingMargin(crossAxis, ownerWidth) + relativePositionCross),
                leading[(int)crossAxis]);
            setLayoutPosition(
                YGUnwrapFloatOptional(getTrailingMargin(crossAxis, ownerWidth) + relativePositionCross),
                trailing[(int)crossAxis]);
        }

        public YGValue marginLeadingValue(in YGFlexDirection axis)
        {
            if (YGFlexDirectionIsRow(axis) && style_.margin[(int)YGEdge.Start].unit != YGUnit.Undefined)
                return style_.margin[(int)YGEdge.Start];
            return style_.margin[(int)leading[(int)axis]];
        }

        public YGValue marginTrailingValue(in YGFlexDirection axis)
        {
            if (YGFlexDirectionIsRow(axis) && style_.margin[(int)YGEdge.End].unit != YGUnit.Undefined)
                return style_.margin[(int)YGEdge.End];
            return style_.margin[(int)trailing[(int)axis]];

        }

        public YGValue resolveFlexBasisPtr()
        {
            YGValue flexBasis = style_.flexBasis;
            if (flexBasis.unit != YGUnit.Auto && flexBasis.unit != YGUnit.Undefined)
            {
                return flexBasis;
            }
            if (!style_.flex.isUndefined() && style_.flex.getValue() > 0.0f)
            {
                return config_.useWebDefaults ? YGValueAuto : YGValueZero;
            }
            return YGValueAuto;
        }

        public void resolveDimension()
        {
            for (int dim = (int)YGDimension.Width; dim < YGDimensionCount; dim++)
            {
                if (getStyle().maxDimensions[dim].unit != YGUnit.Undefined &&
                    YGValueEqual(getStyle().maxDimensions[dim], style_.minDimensions[dim]))
                {
                    resolvedDimensions_[dim] = style_.maxDimensions[dim];
                }
                else
                {
                    resolvedDimensions_[dim] = style_.dimensions[dim];
                }
            }
        }

        public YGDirection resolveDirection(in YGDirection ownerDirection)
        {
            if (style_.direction == YGDirection.Inherit)
            {
                return ownerDirection > YGDirection.Inherit
                    ? ownerDirection
                    : YGDirection.LTR;
            }
            return style_.direction;
        }

        public void clearChildren()
        {
            children_.Clear();
        }

        // Other Methods

        public void cloneChildrenIfNeeded()
        {
            // YGNodeRemoveChild in yoga.cpp has a forked variant of this algorithm
            // optimized for deletions.

            int childCount = children_.Count;
            if (childCount == 0)
            {
                // This is an empty set. Nothing to clone.
                return;
            }

            YGNodeRef firstChild = children_.First();
            if (firstChild.getOwner() == this)
            {
                // If the first child has this node as its owner, we assume that it is
                // already unique. We can do this because if we have it has a child, that
                // means that its owner was at some point cloned which made that subtree
                // immutable. We also assume that all its sibling are cloned as well.
                return;
            }

            YGCloneNodeFunc cloneNodeCallback = config_.cloneNodeCallback;
            for (int i = 0; i < childCount; ++i)
            {
                YGNodeRef oldChild = children_[i];
                YGNodeRef newChild = null;
                if (cloneNodeCallback != null)
                {
                    newChild = cloneNodeCallback(oldChild, this, i);
                }
                if (newChild == null)
                {
                    newChild = YGNodeClone(oldChild);
                }
                replaceChild(newChild, i);
                newChild.setOwner(this);
            }
        }

        public void markDirtyAndPropogate()
        {
            if (!isDirty_)
            {
                setDirty(true);
                setLayoutComputedFlexBasis(new YGFloatOptional());
                owner_?.markDirtyAndPropogate();
            }
        }

        public void markDirtyAndPropogateDownwards()
        {
            isDirty_ = true;
            foreach (YGNodeRef childNode in children_)
            {
                childNode.markDirtyAndPropogateDownwards();
            };
        }

        public float resolveFlexGrow()
        {
            // Root nodes flexGrow should always be 0
            if (owner_ == null)
            {
                return 0.0f;
            }
            if (!style_.flexGrow.isUndefined())
            {
                return style_.flexGrow.getValue();
            }
            if (!style_.flex.isUndefined() && style_.flex.getValue() > 0.0f)
            {
                return style_.flex.getValue();
            }
            return kDefaultFlexGrow;
        }

        public float resolveFlexShrink()
        {
            if (owner_ == null)
            {
                return 0.0f;
            }
            if (!style_.flexShrink.isUndefined())
            {
                return style_.flexShrink.getValue();
            }
            if (!config_.useWebDefaults && !style_.flex.isUndefined() &&
                style_.flex.getValue() < 0.0f)
            {
                return -style_.flex.getValue();
            }
            return config_.useWebDefaults ? kWebDefaultFlexShrink : kDefaultFlexShrink;
        }

        public bool isNodeFlexible()
        {
            return (
                (style_.positionType == YGPositionType.Relative) &&
                (resolveFlexGrow() != 0 || resolveFlexShrink() != 0));
        }

        public float getLeadingBorder(in YGFlexDirection axis)
        {
            if (YGFlexDirectionIsRow(axis) &&
                style_.border[(int)YGEdge.Start].unit != YGUnit.Undefined &&
                !isUndefined(style_.border[(int)YGEdge.Start].value) &&
                style_.border[(int)YGEdge.Start].value >= 0.0f)
            {
                return style_.border[(int)YGEdge.Start].value;
            }

            float computedEdgeValue =
                YGComputedEdgeValue(style_.border, leading[(int)axis], YGValueZero).value;
            return YGFloatMax(computedEdgeValue, 0.0f);
        }

        public float getTrailingBorder(in YGFlexDirection flexDirection)
        {
            if (YGFlexDirectionIsRow(flexDirection) &&
                style_.border[(int)YGEdge.End].unit != YGUnit.Undefined &&
                !isUndefined(style_.border[(int)YGEdge.End].value) &&
                style_.border[(int)YGEdge.End].value >= 0.0f)
            {
                return style_.border[(int)YGEdge.End].value;
            }

            float computedEdgeValue =
                YGComputedEdgeValue(style_.border, trailing[(int)flexDirection], YGValueZero)
                    .value;
            return YGFloatMax(computedEdgeValue, 0.0f);
        }

        public YGFloatOptional getLeadingPadding(
            in YGFlexDirection axis,
            in float widthSize)
        {
            YGFloatOptional paddingEdgeStart =
      YGResolveValue(style_.padding[(int)YGEdge.Start], widthSize);
            if (YGFlexDirectionIsRow(axis) &&
                style_.padding[(int)YGEdge.Start].unit != YGUnit.Undefined &&
                !paddingEdgeStart.isUndefined() && paddingEdgeStart.getValue() > 0.0f)
            {
                return paddingEdgeStart;
            }

            YGFloatOptional resolvedValue = YGResolveValue(YGComputedEdgeValue(style_.padding, leading[(int)axis], YGValueZero), widthSize);
            return YGFloatOptionalMax(resolvedValue, new YGFloatOptional(0.0f));
        }

        public YGFloatOptional getTrailingPadding(
            in YGFlexDirection axis,
            in float widthSize)
        {
            if (YGFlexDirectionIsRow(axis) &&
                style_.padding[(int)YGEdge.End].unit != YGUnit.Undefined &&
                !YGResolveValue(style_.padding[(int)YGEdge.End], widthSize).isUndefined() &&
                YGResolveValue(style_.padding[(int)YGEdge.End], widthSize).getValue() >= 0.0f)
            {
                return YGResolveValue(style_.padding[(int)YGEdge.End], widthSize);
            }

            YGFloatOptional resolvedValue = YGResolveValue(YGComputedEdgeValue(style_.padding, trailing[(int)axis], YGValueZero),
                widthSize);

            return YGFloatOptionalMax(resolvedValue, new YGFloatOptional(0.0f));
        }

        public YGFloatOptional getLeadingPaddingAndBorder(
            in YGFlexDirection axis,
            in float widthSize)
        {
            return getLeadingPadding(axis, widthSize) + new YGFloatOptional(getLeadingBorder(axis));
        }

        public YGFloatOptional getTrailingPaddingAndBorder(
            in YGFlexDirection axis,
            in float widthSize)
        {
            return getTrailingPadding(axis, widthSize) + new YGFloatOptional(getTrailingBorder(axis));
        }

        public bool didUseLegacyFlag()
        {
            bool didUseLegacyFlag = layout_.didUseLegacyFlag;
            if (didUseLegacyFlag)
            {
                return true;
            }
            foreach (YGNodeRef child in children_)
            {
                if (child.layout_.didUseLegacyFlag)
                {
                    didUseLegacyFlag = true;
                    break;
                }
            }
            return didUseLegacyFlag;
        }

        public void setAndPropogateUseLegacyFlag(bool useLegacyFlag)
        {
            config_.useLegacyStretchBehaviour = useLegacyFlag;
            foreach (YGNodeRef child in children_)
            {
                child.getConfig().useLegacyStretchBehaviour = useLegacyFlag;
            }
        }

        public void setLayoutDoesLegacyFlagAffectsLayout(
            bool doesLegacyFlagAffectsLayout)
        {
            layout_.doesLegacyStretchFlagAffectsLayout = doesLegacyFlagAffectsLayout;
        }

        public void setLayoutDidUseLegacyFlag(bool didUseLegacyFlag)
        {
            layout_.didUseLegacyFlag = didUseLegacyFlag;
        }

        public bool isLayoutTreeEqualToNode(in YGNode node)
        {
            if (children_.Count != node.children_.Count)
                return false;

            if (layout_ != node.layout_)
                return false;

            if (children_.Count == 0)
                return true;

            bool isLayoutTreeEqual = true;
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
    }
}