using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming
#pragma warning disable 414
#pragma warning disable 169

namespace Xamarin.Yoga
{
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;
    using static YGGlobal;

    public struct YGSize
    {
        public float width;
        public float height;
    }

    public class YGValue
    {
        public float value;
        public YGUnit unit;

        public YGValue()
        {
            value = 0f;
            unit = YGUnit.Undefined;
        }

        public YGValue(float value, YGUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is YGValue))
                return false;

            var value = (YGValue)obj;
            return YGFloatsEqual(this.value, value.value) &&
                unit == value.unit;
        }

        public override int GetHashCode()
        {
            var hashCode = -1974921375;
            hashCode = hashCode * -1521134295 + value.GetHashCode();
            hashCode = hashCode * -1521134295 + unit.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(YGValue value1, YGValue value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(YGValue value1, YGValue value2)
        {
            return !(value1 == value2);
        }

        public static bool operator ==(YGValue value1, float value2)
        {
            return value1.value.Equals(value2);
        }

        public static bool operator !=(YGValue value1, float value2)
        {
            return !(value1 == value2);
        }

        public static implicit operator YGValue(float value)
        {
            return new YGValue(value, YGUnit.Point);
        }
    }

    public delegate YGSize YGMeasureFunc(
        YGNodeRef node,
        float width,
        YGMeasureMode widthMode,
        float height,
        YGMeasureMode heightMode);

    public delegate float YGBaselineFunc(YGNodeRef node, in float width, in float height);

    public delegate void YGDirtiedFunc(YGNodeRef node);

    public delegate void YGPrintFunc(YGNodeRef node);

    public delegate void YGLogger(
        in YGConfigRef config,
        in YGNodeRef node,
        YGLogLevel level,
        string format,
        params object[] args);

    public delegate YGNodeRef YGCloneNodeFunc(YGNodeRef oldNode, YGNodeRef owner, int childIndex);


    public static partial class YGGlobal
    {
        public static readonly YGValue YGValueUndefined = new YGValue { unit = YGUnit.Undefined };
        public static readonly YGValue YGValueAuto = new YGValue { unit = YGUnit.Auto };
        public static readonly YGValue YGValueZero = new YGValue { unit = YGUnit.Point, value = 0 };

        public static void YGDefaultLog(
            in YGConfigRef config,
            in YGNodeRef node,
            YGLogLevel level,
            string format,
            params object[] args)
        {
            //YG_UNUSED(config);
            //YG_UNUSED(node);
            switch (level)
            {
                case YGLogLevel.Error:
                case YGLogLevel.Fatal:
                    Console.Error.WriteLine(format, args);
                    return;

                case YGLogLevel.Warn:
                case YGLogLevel.Info:
                case YGLogLevel.Debug:
                case YGLogLevel.Verbose:
                default:
                    Console.WriteLine(format, args);
                    return;
            }
        }


        public static bool YGFloatIsUndefined(in float value)
        {
            return isUndefined(value);
        }

        public static YGValue YGComputedEdgeValue(
            in YGValue[] edges,
            in YGEdge edge,
            in YGValue defaultValue)
        {
            if (edges[(int)edge].unit != YGUnit.Undefined) return edges[(int)edge];

            if ((edge == YGEdge.Top || edge == YGEdge.Bottom) &&
                edges[(int)YGEdge.Vertical].unit != YGUnit.Undefined)
                return edges[(int)YGEdge.Vertical];

            if ((edge == YGEdge.Left || edge == YGEdge.Right || edge == YGEdge.Start ||
                    edge == YGEdge.End) &&
                edges[(int)YGEdge.Horizontal].unit != YGUnit.Undefined)
                return edges[(int)YGEdge.Horizontal];

            if (edges[(int)YGEdge.All].unit != YGUnit.Undefined) return edges[(int)YGEdge.All];

            if (edge == YGEdge.Start || edge == YGEdge.End) return YGValueUndefined;

            return defaultValue;
        }

        public static object YGNodeGetContext(YGNodeRef node)
        {
            return node.getContext();
        }

        public static void YGNodeSetContext(YGNodeRef node, object context)
        {
            node.setContext(context);
        }

        public static YGMeasureFunc YGNodeGetMeasureFunc(YGNodeRef node)
        {
            return node.getMeasure();
        }

        public static void YGNodeSetMeasureFunc(YGNodeRef node, YGMeasureFunc measureFunc)
        {
            node.setMeasureFunc(measureFunc);
        }

        public static YGBaselineFunc YGNodeGetBaselineFunc(YGNodeRef node)
        {
            return node.getBaseline();
        }

        public static void YGNodeSetBaselineFunc(YGNodeRef node, YGBaselineFunc baselineFunc)
        {
            node.setBaseLineFunc(baselineFunc);
        }

        public static YGDirtiedFunc YGNodeGetDirtiedFunc(YGNodeRef node)
        {
            return node.getDirtied();
        }

        public static void YGNodeSetDirtiedFunc(YGNodeRef node, YGDirtiedFunc dirtiedFunc)
        {
            node.setDirtiedFunc(dirtiedFunc);
        }

        public static YGPrintFunc YGNodeGetPrintFunc(YGNodeRef node)
        {
            return node.getPrintFunc();
        }

        public static void YGNodeSetPrintFunc(YGNodeRef node, YGPrintFunc printFunc)
        {
            node.setPrintFunc(printFunc);
        }

        public static bool YGNodeGetHasNewLayout(YGNodeRef node)
        {
            return node.getHasNewLayout();
        }

        public static void YGConfigSetPrintTreeFlag(YGConfigRef config, bool enabled)
        {
            config.printTree = enabled;
        }

        public static void YGNodeSetHasNewLayout(YGNodeRef node, bool hasNewLayout)
        {
            node.setHasNewLayout(hasNewLayout);
        }

        public static YGNodeType YGNodeGetNodeType(YGNodeRef node)
        {
            return node.getNodeType();
        }

        public static void YGNodeSetNodeType(YGNodeRef node, YGNodeType nodeType)
        {
            node.setNodeType(nodeType);
        }

        public static bool YGNodeIsDirty(YGNodeRef node)
        {
            return node.isDirty();
        }

        public static bool YGNodeLayoutGetDidUseLegacyFlag(in YGNodeRef node)
        {
            return node.didUseLegacyFlag();
        }

        public static void YGNodeMarkDirtyAndPropogateToDescendants(in YGNodeRef node)
        {
            node.markDirtyAndPropogateDownwards();
        }

        public static int gNodeInstanceCount = 0;
        public static int gConfigInstanceCount = 0;

        // WIN_EXPORT 
        public static YGNodeRef YGNodeNewWithConfig(in YGConfigRef config)
        {
            var node = new YGNode();
            YGAssertWithConfig(
                config,
                node != null,
                "Could not allocate memory for node");
            gNodeInstanceCount++;

            if (config.useWebDefaults)
            {
                node.setStyleFlexDirection(YGFlexDirection.Row);
                node.setStyleAlignContent(YGAlign.Stretch);
            }

            node.setConfig(config);
            return node;
        }

        private static YGConfigRef defaultConfig = YGConfigNew();

        public static YGConfigRef YGConfigGetDefault()
        {
            return defaultConfig;
        }

        public static YGNodeRef YGNodeNew()
        {
            return YGNodeNewWithConfig(YGConfigGetDefault());
        }

        public static YGNodeRef YGNodeClone(YGNodeRef oldNode)
        {
            var node = new YGNode(oldNode);
            YGAssertWithConfig(
                oldNode.getConfig(),
                node != null,
                "Could not allocate memory for node");
            gNodeInstanceCount++;
            node.setOwner(null);
            return node;
        }

        private static YGConfigRef YGConfigClone(in YGConfig oldConfig)
        {
            var config = new YGConfig(oldConfig);
            YGAssert(config != null, "Could not allocate memory for config");
            if (config == null) throw new NullReferenceException();

            gConfigInstanceCount++;
            return config;
        }

        private static YGNodeRef YGNodeDeepClone(YGNodeRef oldNode)
        {
            var node = YGNodeClone(oldNode);
            var vec = new YGVector(oldNode.getChildren().Count);
            YGNodeRef childNode = null;
            foreach (var item in oldNode.getChildren())
            {
                childNode = YGNodeDeepClone(item);
                childNode.setOwner(node);
                vec.Add(childNode);
            }

            node.setChildren(vec);

            if (oldNode.getConfig() != null) node.setConfig(YGConfigClone(oldNode.getConfig()));

            return node;
        }

        public static void YGNodeFree(in YGNodeRef node)
        {
            var owner = node.getOwner();
            if (owner != null)
            {
                owner.removeChild(node);
                node.setOwner(null);
            }

            var childCount = YGNodeGetChildCount(node);
            for (var i = 0; i < childCount; i++)
            {
                var child = YGNodeGetChild(node, i);
                child.setOwner(null);
            }

            node.clearChildren();
            //delete node;
            gNodeInstanceCount--;
        }

        internal static void YGConfigFreeRecursive(in YGNodeRef root)
        {
            if (root.getConfig() != null) gConfigInstanceCount--;

            // Delete configs recursively for childrens
            foreach (var child in root.getChildren()) YGConfigFreeRecursive(child);
        }

        public static void YGNodeFreeRecursive(YGNodeRef root)
        {
            while (YGNodeGetChildCount(root) > 0)
            {
                var child = YGNodeGetChild(root, 0);
                if (child.getOwner() != root) break;

                YGNodeRemoveChild(root, child);
                YGNodeFreeRecursive(child);
            }

            YGNodeFree(root);
        }

        public static void YGNodeReset(ref YGNodeRef node)
        {
            YGAssertWithNode(
                node,
                YGNodeGetChildCount(node) == 0,
                "Cannot reset a node which still has children attached");
            YGAssertWithNode(
                node,
                node.getOwner() == null,
                "Cannot reset a node still attached to a owner");

            node.clearChildren();

            var config = node.getConfig();
            node = new YGNode();
            if (config.useWebDefaults)
            {
                node.setStyleFlexDirection(YGFlexDirection.Row);
                node.setStyleAlignContent(YGAlign.Stretch);
            }

            node.setConfig(config);
        }

        public static int YGNodeGetInstanceCount()
        {
            return gNodeInstanceCount;
        }

        public static int YGConfigGetInstanceCount()
        {
            return gConfigInstanceCount;
        }

        public static YGConfigRef YGConfigNew()
        {
            var config = new YGConfig(YGDefaultLog);

            gConfigInstanceCount++;
            return config;
        }

        public static void YGConfigFree(in YGConfigRef config)
        {
            //delete config;
            gConfigInstanceCount--;
        }

        public static void YGConfigCopy(in YGConfigRef dest, in YGConfigRef src)
        {
            dest.CloneFrom(src);
        }

        public static void YGNodeInsertChild(
            in YGNodeRef node,
            in YGNodeRef child,
            in int index)
        {
            YGAssertWithNode(
                node,
                child.getOwner() == null,
                "Child already has a owner, it must be removed first.");

            YGAssertWithNode(
                node,
                node.getMeasure() == null,
                "Cannot add child: Nodes with measure functions cannot have children.");

            node.cloneChildrenIfNeeded();
            node.insertChild(child, index);
            var owner = child.getOwner() != null ? null : node;
            child.setOwner(owner);
            node.markDirtyAndPropogate();
        }

        public static void YGNodeInsertSharedChild(
            in YGNodeRef node,
            in YGNodeRef child,
            in int index)
        {
            YGAssertWithNode(
                node,
                node.getMeasure() == null,
                "Cannot add child: Nodes with measure functions cannot have children.");

            node.insertChild(child, index);
            child.setOwner(null);
            node.markDirtyAndPropogate();
        }

        public static void YGNodeRemoveChild(in YGNodeRef owner, in YGNodeRef excludedChild)
        {
            // This algorithm is a forked variant from cloneChildrenIfNeeded in YGNode
            // that excludes a child.
            var childCount = YGNodeGetChildCount(owner);

            if (childCount == 0) return;

            var firstChild = YGNodeGetChild(owner, 0);
            if (firstChild.getOwner() == owner)
            {
                // If the first child has this node as its owner, we assume that it is
                // already unique. We can now try to delete a child in this list.
                if (owner.removeChild(excludedChild))
                {
                    excludedChild.setLayout(new YGNode().getLayout()); // layout is no longer valid
                    excludedChild.setOwner(null);
                    owner.markDirtyAndPropogate();
                }

                return;
            }

            // Otherwise we have to clone the node list except for the child we're trying
            // to delete. We don't want to simply clone all children, because then the
            // host will need to free the clone of the child that was just deleted.
            var cloneNodeCallback =
                owner.getConfig().cloneNodeCallback;
            var nextInsertIndex = 0;
            for (var i = 0; i < childCount; i++)
            {
                var oldChild = owner.getChild(i);
                if (excludedChild == oldChild)
                {
                    // Ignore the deleted child. Don't reset its layout or owner since it is
                    // still valid in the other owner. However, since this owner has now
                    // changed, we need to mark it as dirty.
                    owner.markDirtyAndPropogate();
                    continue;
                }

                YGNodeRef newChild = null;
                if (cloneNodeCallback != null) newChild = cloneNodeCallback(oldChild, owner, nextInsertIndex);

                if (newChild == null) newChild = YGNodeClone(oldChild);

                owner.replaceChild(newChild, nextInsertIndex);
                newChild.setOwner(owner);

                nextInsertIndex++;
            }

            while (nextInsertIndex < childCount)
            {
                owner.removeChild(nextInsertIndex);
                nextInsertIndex++;
            }
        }

        public static void YGNodeRemoveAllChildren(in YGNodeRef owner)
        {
            var childCount = YGNodeGetChildCount(owner);
            if (childCount == 0) return;

            var firstChild = YGNodeGetChild(owner, 0);
            if (firstChild.getOwner() == owner)
            {
                // If the first child has this node as its owner, we assume that this child
                // set is unique.
                for (var i = 0; i < childCount; i++)
                {
                    var oldChild = YGNodeGetChild(owner, i);
                    oldChild.setLayout(new YGNode().getLayout()); // layout is no longer valid
                    oldChild.setOwner(null);
                }

                owner.clearChildren();
                owner.markDirtyAndPropogate();
                return;
            }

            // Otherwise, we are not the owner of the child set. We don't have to do
            // anything to clear it.
            owner.setChildren(new YGVector());
            owner.markDirtyAndPropogate();
        }

        internal static void YGNodeSetChildrenInternal(
            in YGNodeRef owner,
            List<YGNodeRef> children)
        {
            if (owner == null) return;

            if (children.Count == 0)
            {
                if (YGNodeGetChildCount(owner) > 0)
                {
                    foreach (var child in owner.getChildren())
                    {
                        child.setLayout(new YGLayout());
                        child.setOwner(null);
                    }

                    owner.setChildren(new YGVector());
                    owner.markDirtyAndPropogate();
                }
            }
            else
            {
                if (YGNodeGetChildCount(owner) > 0)
                    foreach (var oldChild in owner.getChildren())
                        // Our new children may have nodes in common with the old children. We
                        // don't reset these common nodes.
                        if (!children.Contains(oldChild))
                        {
                            oldChild.setLayout(new YGLayout());
                            oldChild.setOwner(null);
                        }

                owner.setChildren(children);
                foreach (var child in children) child.setOwner(owner);

                owner.markDirtyAndPropogate();
            }
        }

        public static void YGNodeSetChildren(
            in YGNodeRef owner,
            in YGNodeRef[] c,
            in int count)
        {
            var children = c.ToList().Take(count).ToList();
            YGNodeSetChildrenInternal(owner, children);
        }

        public static void YGNodeSetChildren(
            in YGNodeRef owner,
            in List<YGNodeRef> children)
        {
            YGNodeSetChildrenInternal(owner, children);
        }

        public static YGNodeRef YGNodeGetChild(YGNodeRef node, int index)
        {
            if (index < node.getChildren().Count) return node.getChild(index);

            return null;
        }

        public static int YGNodeGetChildCount(YGNodeRef node)
        {
            return node.getChildren().Count;
        }

        public static YGNodeRef YGNodeGetOwner(YGNodeRef node)
        {
            return node.getOwner();
        }

        public static YGNodeRef YGNodeGetParent(YGNodeRef node)
        {
            return node.getOwner();
        }

        public static void YGNodeMarkDirty(YGNodeRef node)
        {
            YGAssertWithNode(
                node,
                node.getMeasure() != null,
                "Only leaf nodes with custom measure functions should manually mark themselves as dirty");

            node.markDirtyAndPropogate();
        }

        public static void YGNodeCopyStyle(YGNodeRef dstNode, in YGNodeRef srcNode)
        {
            if (!(dstNode.getStyle() == srcNode.getStyle()))
            {
                dstNode.setStyle(srcNode.getStyle());
                dstNode.markDirtyAndPropogate();
            }
        }

        public static float YGNodeStyleGetFlexGrow(YGNodeRef node)
        {
            return node.getStyle().flexGrow.isUndefined()
                ? kDefaultFlexGrow
                : node.getStyle().flexGrow.getValue();
        }

        public static float YGNodeStyleGetFlexShrink(YGNodeRef node)
        {
            return node.getStyle().flexShrink.isUndefined()
                ? (node.getConfig().useWebDefaults
                    ? kWebDefaultFlexShrink
                    : kDefaultFlexShrink)
                : node.getStyle().flexShrink.getValue();
        }

        //namespace {

        //template<typename T, T YGStyle::*P>
        //public struct StyleProp<T>
        //{
        //    static T get(YGNodeRef node)
        //    {
        //        return node.getStyle().* P;
        //    }
        //    static void set(YGNodeRef node, T newValue)
        //    {
        //        if (node.getStyle().* P != newValue)
        //        {
        //            node.getStyle().* P = newValue;
        //            node.markDirtyAndPropogate();
        //        }
        //    }
        //};

        internal static class Value
        {
            private static YGValue create(float value, YGUnit unit)
            {
                return new YGValue(
                    YGFloatSanitize(value),
                    YGFloatIsUndefined(value) ? YGUnit.Undefined : unit);
            }
        }

        /*
    template<YGStyle::Dimensions YGStyle::*P>
struct DimensionProp
    {
        template<YGDimension idx>
  static WIN_STRUCT(YGValue) get(YGNodeRef node)
        {
            YGValue value = (node.getStyle().* P)[idx];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto)
            {
                value.value = YGUndefined;
            }
            return WIN_STRUCT_REF(value);
        }

        template<YGDimension idx, YGUnit U>
  static void set(YGNodeRef node, float newValue)
        {
            YGValue value = Value::create<U>(newValue);
            if (((node.getStyle().* P)[idx].value != value.value &&
                 value.unit != YGUnit.Undefined) ||
                (node.getStyle().* P)[idx].unit != value.unit)
            {
                (node.getStyle().* P)[idx] = value;
                node.markDirtyAndPropogate();
            }
        }
    };
    */

        // } // namespace

        /*
#define YG_NODE_STYLE_PROPERTY_SETTER_UNIT_AUTO_IMPL(                        \
type, name, paramName, instanceName)                                     \
  void YGNodeStyleSet##name(const YGNodeRef node, const type paramName) {    \
    YGValue value = {                                                        \
        YGFloatSanitize(paramName),                                          \
        YGFloatIsUndefined(paramName) ? YGUnit.Undefined : YGUnitPoint,       \
    };                                                                       \
    if ((node.getStyle().instanceName.value != value.value &&               \
         value.unit != YGUnit.Undefined) ||                                   \
        node.getStyle().instanceName.unit != value.unit) {                  \
      node.getStyle().instanceName = value;                                 \
      node.markDirtyAndPropogate();                                         \
    }                                                                        \
  }                                                                          \
                                                                             \
  void YGNodeStyleSet##name##Percent(                                        \
      const YGNodeRef node, const type paramName) {                          \
    if (node.getStyle().instanceName.value != YGFloatSanitize(paramName) || \
        node.getStyle().instanceName.unit != YGUnit.Percent) {               \
      node.getStyle().instanceName = YGFloatIsUndefined(paramName)          \
          ? YGValue
{0, YGUnit.Auto }                                           \
          : YGValue{paramName, YGUnit.Percent};                               \
      node.markDirtyAndPropogate();                                         \
    }                                                                        \
  }                                                                          \
                                                                             \
  void YGNodeStyleSet##name##Auto(const YGNodeRef node) {                    \
    if (node.getStyle().instanceName.unit != YGUnit.Auto) {                  \
      node.getStyle().instanceName = {0, YGUnit.Auto};                       \
      node.markDirtyAndPropogate();                                         \
    }                                                                        \
  }

#define YG_NODE_STYLE_PROPERTY_UNIT_AUTO_IMPL(                       \
    type, name, paramName, instanceName)                             \
  YG_NODE_STYLE_PROPERTY_SETTER_UNIT_AUTO_IMPL(                      \
      float, name, paramName, instanceName)                          \
                                                                     \
  type YGNodeStyleGet##name(const YGNodeRef node) {                  \
    YGValue value = node.getStyle().instanceName;                   \
    if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) { \
      value.value = YGUndefined;                                     \
    }                                                                \
    return value;                                                    \
  }

#define YG_NODE_STYLE_EDGE_PROPERTY_UNIT_AUTO_IMPL(type, name, instanceName) \
  void YGNodeStyleSet##name##Auto(const YGNodeRef node, const YGEdge edge) { \
    if (node.getStyle().instanceName[edge].unit != YGUnit.Auto) {            \
      node.getStyle().instanceName[edge] = {0, YGUnit.Auto};                 \
      node.markDirtyAndPropogate();                                         \
    }                                                                        \
  }

#define YG_NODE_STYLE_EDGE_PROPERTY_UNIT_IMPL(                           \
    type, name, paramName, instanceName)                                 \
  void YGNodeStyleSet##name(                                             \
      const YGNodeRef node, const YGEdge edge, const float paramName) {  \
    YGValue value = {                                                    \
        YGFloatSanitize(paramName),                                      \
        YGFloatIsUndefined(paramName) ? YGUnit.Undefined : YGUnitPoint,   \
    };                                                                   \
    if ((node.getStyle().instanceName[edge].value != value.value &&     \
         value.unit != YGUnit.Undefined) ||                               \
        node.getStyle().instanceName[edge].unit != value.unit) {        \
      node.getStyle().instanceName[edge] = value;                       \
      node.markDirtyAndPropogate();                                     \
    }                                                                    \
  }                                                                      \
                                                                         \
  void YGNodeStyleSet##name##Percent(                                    \
      const YGNodeRef node, const YGEdge edge, const float paramName) {  \
    YGValue value = {                                                    \
        YGFloatSanitize(paramName),                                      \
        YGFloatIsUndefined(paramName) ? YGUnit.Undefined : YGUnit.Percent, \
    };                                                                   \
    if ((node.getStyle().instanceName[edge].value != value.value &&     \
         value.unit != YGUnit.Undefined) ||                               \
        node.getStyle().instanceName[edge].unit != value.unit) {        \
      node.getStyle().instanceName[edge] = value;                       \
      node.markDirtyAndPropogate();                                     \
    }                                                                    \
  }                                                                      \
                                                                         \
  WIN_STRUCT(type)                                                       \
  YGNodeStyleGet##name(const YGNodeRef node, const YGEdge edge) {        \
    YGValue value = node.getStyle().instanceName[edge];                 \
    if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) {     \
      value.value = YGUndefined;                                         \
    }                                                                    \
    return WIN_STRUCT_REF(value);                                        \
  }

#define YG_NODE_LAYOUT_PROPERTY_IMPL(type, name, instanceName) \
  type YGNodeLayoutGet##name(const YGNodeRef node) {           \
    return node.getLayout().instanceName;                     \
  }

#define YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(type, name, instanceName) \
  type YGNodeLayoutGet##name(const YGNodeRef node, const YGEdge edge) { \
    YGAssertWithNode(                                                   \
        node,                                                           \
        edge <= YGEdgeEnd,                                              \
        "Cannot get layout properties of multi-edge shorthands");       \
                                                                        \
    if (edge == YGEdgeLeft) {                                           \
      if (node.getLayout().direction == RTL) {              \
        return node.getLayout().instanceName[YGEdgeEnd];               \
      } else {                                                          \
        return node.getLayout().instanceName[YGEdgeStart];             \
      }                                                                 \
    }                                                                   \
                                                                        \
    if (edge == YGEdgeRight) {                                          \
      if (node.getLayout().direction == RTL) {              \
        return node.getLayout().instanceName[YGEdgeStart];             \
      } else {                                                          \
        return node.getLayout().instanceName[YGEdgeEnd];               \
      }                                                                 \
    }                                                                   \
                                                                        \
    return node.getLayout().instanceName[edge];                        \
  }
  */

        public static void YGNodeStyleSetDirection(
            in YGNodeRef node,
            in YGDirection direction)
        {
            if (node.getStyle().direction != direction)
            {
                node.getStyle().direction = direction;
                node.markDirtyAndPropogate();
            }
        }

        public static YGDirection YGNodeStyleGetDirection(in YGNodeRef node)
        {
            return node.getStyle().direction;
        }

        public static void YGNodeStyleSetFlexDirection(
            in YGNodeRef node,
            in YGFlexDirection flexDirection)
        {
            if (node.getStyle().flexDirection != flexDirection)
            {
                node.getStyle().flexDirection = flexDirection;
                node.markDirtyAndPropogate();
            }
        }

        public static YGFlexDirection YGNodeStyleGetFlexDirection(in YGNodeRef node)
        {
            return node.getStyle().flexDirection;
        }

        public static void YGNodeStyleSetJustifyContent(
            in YGNodeRef node,
            in YGJustify justifyContent)
        {
            if (node.getStyle().justifyContent != justifyContent)
            {
                node.getStyle().justifyContent = justifyContent;
                node.markDirtyAndPropogate();
            }
        }

        public static YGJustify YGNodeStyleGetJustifyContent(in YGNodeRef node)
        {
            return node.getStyle().justifyContent;
        }

        public static void YGNodeStyleSetAlignContent(
            in YGNodeRef node,
            in YGAlign alignContent)
        {
            if (node.getStyle().alignContent != alignContent)
            {
                node.getStyle().alignContent = alignContent;
                node.markDirtyAndPropogate();
            }
        }

        public static YGAlign YGNodeStyleGetAlignContent(in YGNodeRef node)
        {
            return node.getStyle().alignContent;
        }

        public static void YGNodeStyleSetAlignItems(in YGNodeRef node, in YGAlign alignItems)
        {
            if (node.getStyle().alignItems != alignItems)
            {
                node.getStyle().alignItems = alignItems;
                node.markDirtyAndPropogate();
            }
        }

        public static YGAlign YGNodeStyleGetAlignItems(in YGNodeRef node)
        {
            return node.getStyle().alignItems;
        }

        public static void YGNodeStyleSetAlignSelf(in YGNodeRef node, in YGAlign alignSelf)
        {
            if (node.getStyle().alignSelf != alignSelf)
            {
                node.getStyle().alignSelf = alignSelf;
                node.markDirtyAndPropogate();
            }
        }

        public static YGAlign YGNodeStyleGetAlignSelf(in YGNodeRef node)
        {
            return node.getStyle().alignSelf;
        }

        public static void YGNodeStyleSetPositionType(
            in YGNodeRef node,
            in YGPositionType positionType)
        {
            if (node.getStyle().positionType != positionType)
            {
                node.getStyle().positionType = positionType;
                node.markDirtyAndPropogate();
            }
        }

        public static YGPositionType YGNodeStyleGetPositionType(in YGNodeRef node)
        {
            return node.getStyle().positionType;
        }

        public static void YGNodeStyleSetFlexWrap(in YGNodeRef node, in YGWrap flexWrap)
        {
            if (node.getStyle().flexWrap != flexWrap)
            {
                node.getStyle().flexWrap = flexWrap;
                node.markDirtyAndPropogate();
            }
        }

        public static YGWrap YGNodeStyleGetFlexWrap(in YGNodeRef node)
        {
            return node.getStyle().flexWrap;
        }

        public static void YGNodeStyleSetOverflow(in YGNodeRef node, in YGOverflow overflow)
        {
            if (node.getStyle().overflow != overflow)
            {
                node.getStyle().overflow = overflow;
                node.markDirtyAndPropogate();
            }
        }

        public static YGOverflow YGNodeStyleGetOverflow(in YGNodeRef node)
        {
            return node.getStyle().overflow;
        }

        public static void YGNodeStyleSetDisplay(in YGNodeRef node, in YGDisplay display)
        {
            if (node.getStyle().display != display)
            {
                node.getStyle().display = display;
                node.markDirtyAndPropogate();
            }
        }

        public static YGDisplay YGNodeStyleGetDisplay(in YGNodeRef node)
        {
            return node.getStyle().display;
        }

        // TODO(T26792433): Change the API to accept YGFloatOptional.
        public static void YGNodeStyleSetFlex(in YGNodeRef node, in float flex)
        {
            if (node.getStyle().flex != flex)
            {
                node.getStyle().flex = YGFloatIsUndefined(flex) ? new YGFloatOptional() : new YGFloatOptional(flex);
                node.markDirtyAndPropogate();
            }
        }

        // TODO(T26792433): Change the API to accept YGFloatOptional.
        public static float YGNodeStyleGetFlex(in YGNodeRef node)
        {
            return node.getStyle().flex.isUndefined()
                ? YGUndefined
                : node.getStyle().flex.getValue();
        }

        // TODO(T26792433): Change the API to accept YGFloatOptional.
        public static void YGNodeStyleSetFlexGrow(in YGNodeRef node, in float flexGrow)
        {
            if (node.getStyle().flexGrow != flexGrow)
            {
                node.getStyle().flexGrow = YGFloatIsUndefined(flexGrow)
                    ? new YGFloatOptional()
                    : new YGFloatOptional(flexGrow);
                node.markDirtyAndPropogate();
            }
        }

        // TODO(T26792433): Change the API to accept YGFloatOptional.
        public static void YGNodeStyleSetFlexShrink(in YGNodeRef node, in float flexShrink)
        {
            if (node.getStyle().flexShrink != flexShrink)
            {
                node.getStyle().flexShrink = YGFloatIsUndefined(flexShrink)
                    ? new YGFloatOptional()
                    : new YGFloatOptional(flexShrink);
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetFlexBasis(in YGNodeRef node)
        {
            var flexBasis = node.getStyle().flexBasis;
            if (flexBasis.unit == YGUnit.Undefined || flexBasis.unit == YGUnit.Auto) flexBasis.value = YGUndefined;

            return flexBasis;
        }

        public static void YGNodeStyleSetFlexBasis(in YGNodeRef node, in float flexBasis)
        {
            var value = new YGValue(
                YGFloatSanitize(flexBasis),
                YGFloatIsUndefined(flexBasis) ? YGUnit.Undefined : YGUnit.Point);

            if (node.getStyle().flexBasis.value != value.value && value.unit != YGUnit.Undefined ||
                node.getStyle().flexBasis.unit != value.unit)
            {
                node.getStyle().flexBasis = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetFlexBasisPercent(
            in YGNodeRef node,
            in float flexBasisPercent)
        {
            if (node.getStyle().flexBasis.value != flexBasisPercent ||
                node.getStyle().flexBasis.unit != YGUnit.Percent)
            {
                node.getStyle().flexBasis = YGFloatIsUndefined(flexBasisPercent)
                    ? new YGValue(0, YGUnit.Auto)
                    : new YGValue(flexBasisPercent, YGUnit.Percent);
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetFlexBasisAuto(in YGNodeRef node)
        {
            if (node.getStyle().flexBasis.unit != YGUnit.Auto)
            {
                node.getStyle().flexBasis = new YGValue(0, YGUnit.Auto);
                node.markDirtyAndPropogate();
            }
        }

        //YG_NODE_STYLE_EDGE_PROPERTY_UNIT_IMPL(YGValue,      Position, position, position);
        public static void YGNodeStyleSetPosition(in YGNodeRef node, in YGEdge edge, in float position)
        {
            var value = new YGValue(
                YGFloatSanitize(position),
                YGFloatIsUndefined(position) ? YGUnit.Undefined : YGUnit.Point);

            if (node.getStyle().position[(int)edge].value != value.value && value.unit != YGUnit.Undefined ||
                node.getStyle().position[(int)edge].unit != value.unit)
            {
                node.getStyle().position[(int)edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetPositionPercent(
            in YGNodeRef node, in YGEdge edge, in float position)
        {
            var value = new YGValue(
                YGFloatSanitize(position),
                YGFloatIsUndefined(position) ? YGUnit.Undefined : YGUnit.Percent);
            if (node.getStyle().position[(int)edge].value != value.value && value.unit != YGUnit.Undefined ||
                node.getStyle().position[(int)edge].unit != value.unit)
            {
                node.getStyle().position[(int)edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetPosition(in YGNodeRef node, in YGEdge edge)
        {
            var value = node.getStyle().position[(int)edge];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) value.value = YGUndefined;
            return value;
        }

        // YG_NODE_STYLE_EDGE_PROPERTY_UNIT_IMPL(YGValue,      Margin,   margin,   margin);
        public static void YGNodeStyleSetMargin(in YGNodeRef node, in YGEdge edge, in float margin)
        {
            var value = new YGValue(
                YGFloatSanitize(margin),
                YGFloatIsUndefined(margin) ? YGUnit.Undefined : YGUnit.Point);

            if (node.getStyle().margin[(int)edge].value != value.value && value.unit != YGUnit.Undefined ||
                node.getStyle().margin[(int)edge].unit != value.unit)
            {
                node.getStyle().margin[(int)edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetMarginPercent(
            YGNodeRef node,
            YGEdge edge,
            float margin)
        {
            var value = new YGValue(
                YGFloatSanitize(margin),
                YGFloatIsUndefined(margin) ? YGUnit.Undefined : YGUnit.Percent);
            if (node.getStyle().margin[(int)edge].value != value.value && value.unit != YGUnit.Undefined ||
                node.getStyle().margin[(int)edge].unit != value.unit)
            {
                node.getStyle().margin[(int)edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetMargin(in YGNodeRef node, in YGEdge edge)
        {
            var value = node.getStyle().margin[(int)edge];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) value.value = YGUndefined;
            return value;
        }

        // YG_NODE_STYLE_EDGE_PROPERTY_UNIT_AUTO_IMPL(YGValue, Margin,   margin);
        public static void YGNodeStyleSetMarginAuto(in YGNodeRef node, in YGEdge edge)
        {
            if (node.getStyle().margin[(int)edge].unit != YGUnit.Auto)
            {
                node.getStyle().margin[(int)edge] = YGValueAuto;
                node.markDirtyAndPropogate();
            }
        }

        //YG_NODE_STYLE_EDGE_PROPERTY_UNIT_IMPL(YGValue,      Padding,  padding, padding);
        public static void YGNodeStyleSetPadding(in YGNodeRef node, in YGEdge edge, in float padding)
        {
            var value = new YGValue(
                YGFloatSanitize(padding),
                YGFloatIsUndefined(padding) ? YGUnit.Undefined : YGUnit.Point);

            if (node.getStyle().padding[(int)edge].value != value.value && value.unit != YGUnit.Undefined ||
                node.getStyle().padding[(int)edge].unit != value.unit)
            {
                node.getStyle().padding[(int)edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetPaddingPercent(
            in YGNodeRef node, in YGEdge edge, in float padding)
        {
            var value = new YGValue(
                YGFloatSanitize(padding),
                YGFloatIsUndefined(padding) ? YGUnit.Undefined : YGUnit.Percent);
            if (node.getStyle().padding[(int)edge].value != value.value && value.unit != YGUnit.Undefined ||
                node.getStyle().padding[(int)edge].unit != value.unit)
            {
                node.getStyle().padding[(int)edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetPadding(in YGNodeRef node, in YGEdge edge)
        {
            var value = node.getStyle().padding[(int)edge];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) value.value = YGUndefined;
            return value;
        }


        // TODO(T26792433): Change the API to accept YGFloatOptional.
        public static void YGNodeStyleSetBorder(
            in YGNodeRef node,
            in YGEdge edge,
            in float border)
        {
            var value = new YGValue(
                YGFloatSanitize(border),
                YGFloatIsUndefined(border) ? YGUnit.Undefined : YGUnit.Point);
            if (node.getStyle().border[(int)edge].value != value.value &&
                value.unit                              != YGUnit.Undefined ||
                node.getStyle().border[(int)edge].unit != value.unit)
            {
                node.getStyle().border[(int)edge] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static float YGNodeStyleGetBorder(in YGNodeRef node, in YGEdge edge)
        {
            if (node.getStyle().border[(int)edge].unit == YGUnit.Undefined ||
                node.getStyle().border[(int)edge].unit == YGUnit.Auto)
                return YGUndefined;

            return node.getStyle().border[(int)edge].value;
        }

        // Yoga specific properties, not compatible with flexbox specification

        // TODO(T26792433): Change the API to accept YGFloatOptional.
        public static float YGNodeStyleGetAspectRatio(in YGNodeRef node)
        {
            var op = node.getStyle().aspectRatio;
            return op.isUndefined() ? YGUndefined : op.getValue();
        }

        // TODO(T26792433): Change the API to accept YGFloatOptional.
        public static void YGNodeStyleSetAspectRatio(in YGNodeRef node, in float aspectRatio)
        {
            if (node.getStyle().aspectRatio != aspectRatio)
            {
                node.getStyle().aspectRatio = new YGFloatOptional(aspectRatio);
                node.markDirtyAndPropogate();
            }
        }

        // YG_NODE_STYLE_PROPERTY_UNIT_AUTO_IMPL(YGValue,Width,width,dimensions[YGDimension.Width]);
        public static void YGNodeStyleSetWidth(YGNodeRef node, YGValue width)
        {
            var value = new YGValue(
                YGFloatSanitize(width.value),
                YGFloatIsUndefined(width.value) ? YGUnit.Undefined : YGUnit.Point);
            if (node.getStyle().dimensions[YGDimension.Width].value != value.value && value.unit != YGUnit.Undefined ||
                node.getStyle().dimensions[YGDimension.Width].unit != value.unit)
            {
                node.getStyle().dimensions[YGDimension.Width] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetWidthPercent(
            in YGNodeRef node, in YGValue width)
        {
            if (node.getStyle().dimensions[YGDimension.Width].value != YGFloatSanitize(width.value) ||
                node.getStyle().dimensions[YGDimension.Width].unit != YGUnit.Percent)
            {
                node.getStyle().dimensions[YGDimension.Width] = YGFloatIsUndefined(width.value)
                    ? new YGValue(0, YGUnit.Auto)
                    : new YGValue(width.value, YGUnit.Percent);
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetWidthAuto(in YGNodeRef node)
        {
            if (node.getStyle().dimensions[YGDimension.Width].unit != YGUnit.Auto)
            {
                node.getStyle().dimensions[YGDimension.Width] = new YGValue(0, YGUnit.Auto);
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetWidth(in YGNodeRef node)
        {
            var value = node.getStyle().dimensions[YGDimension.Width];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) value.value = YGUndefined;
            return value;
        }


        //YG_NODE_STYLE_PROPERTY_UNIT_AUTO_IMPL(YGValue,Height,height,dimensions[ YGDimension.Height]);
        public static void YGNodeStyleSetHeight(YGNodeRef node, YGValue height)
        {
            var value = new YGValue(
                YGFloatSanitize(height.value),
                YGFloatIsUndefined(height.value) ? YGUnit.Undefined : YGUnit.Point);
            if (node.getStyle().dimensions[YGDimension.Height].value != value.value && value.unit != YGUnit.Undefined ||
                node.getStyle().dimensions[YGDimension.Height].unit != value.unit)
            {
                node.getStyle().dimensions[YGDimension.Height] = value;
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetHeightPercent(
            in YGNodeRef node, in YGValue height)
        {
            if (node.getStyle().dimensions[YGDimension.Height].value != YGFloatSanitize(height.value) ||
                node.getStyle().dimensions[YGDimension.Height].unit != YGUnit.Percent)
            {
                node.getStyle().dimensions[YGDimension.Height] = YGFloatIsUndefined(height.value)
                    ? new YGValue(0, YGUnit.Auto)
                    : new YGValue(height.value, YGUnit.Percent);
                node.markDirtyAndPropogate();
            }
        }

        public static void YGNodeStyleSetHeightAuto(in YGNodeRef node)
        {
            if (node.getStyle().dimensions[YGDimension.Height].unit != YGUnit.Auto)
            {
                node.getStyle().dimensions[YGDimension.Height] = new YGValue(0, YGUnit.Auto);
                node.markDirtyAndPropogate();
            }
        }

        public static YGValue YGNodeStyleGetHeight(in YGNodeRef node)
        {
            var value = node.getStyle().dimensions[YGDimension.Height];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto) value.value = YGUndefined;
            return value;
        }



        internal static void YGNodeStyleSetDimensions(
            in YGNodeRef node,
            in Dimensions dimensions,
            YGDimension dim,
            in float value,
            YGUnit unit)
        {
            var newValue = new YGValue(YGFloatSanitize(value), YGFloatIsUndefined(value) ? YGUnit.Undefined : unit);
            var current = dimensions[dim];
            if (current != newValue)
            {
                dimensions[dim] = newValue;
                node.markDirtyAndPropogate();
            }
        }

        internal static YGValue YGNodeStyleGetDimensions(
            in Dimensions dimensions,
            YGDimension dim)
        {
            var value = dimensions[dim];
            if (value.unit == YGUnit.Undefined || value.unit == YGUnit.Auto)
                value.value = YGUndefined;
            return value;
        }

        public static void YGNodeStyleSetMinWidth(in YGNodeRef node, in float minWidth)
        {
            YGNodeStyleSetDimensions(node, node.getStyle().minDimensions, YGDimension.Width, minWidth, YGUnit.Point);
        }

        public static void YGNodeStyleSetMinWidthPercent(in YGNodeRef node, in float minWidth)
        {
            YGNodeStyleSetDimensions(node, node.getStyle().minDimensions, YGDimension.Width, minWidth, YGUnit.Percent);
        }

        public static YGValue YGNodeStyleGetMinWidth(in YGNodeRef node)
        {
            return YGNodeStyleGetDimensions(node.getStyle().minDimensions, YGDimension.Width);
        }

        public static void YGNodeStyleSetMinHeight(in YGNodeRef node, in float minHeight)
        {
            YGNodeStyleSetDimensions(node, node.getStyle().minDimensions, YGDimension.Height, minHeight, YGUnit.Point);
        }

        public static void YGNodeStyleSetMinHeightPercent(in YGNodeRef node, in float minHeight)
        {
            YGNodeStyleSetDimensions(node, node.getStyle().minDimensions, YGDimension.Height, minHeight, YGUnit.Percent);
        }

        public static YGValue YGNodeStyleGetMinHeight(in YGNodeRef node)
        {
            return YGNodeStyleGetDimensions(node.getStyle().minDimensions, YGDimension.Height);
        }

        public static void YGNodeStyleSetMaxWidth(in YGNodeRef node, in float maxWidth)
        {
            YGNodeStyleSetDimensions(node, node.getStyle().maxDimensions, YGDimension.Width, maxWidth, YGUnit.Point);
        }

        public static void YGNodeStyleSetMaxWidthPercent(in YGNodeRef node, in float maxWidth)
        {
            YGNodeStyleSetDimensions(node, node.getStyle().maxDimensions, YGDimension.Width, maxWidth, YGUnit.Percent);
        }

        public static YGValue YGNodeStyleGetMaxWidth(in YGNodeRef node)
        {
            return YGNodeStyleGetDimensions(node.getStyle().maxDimensions, YGDimension.Width);
        }

        public static void YGNodeStyleSetMaxHeight(in YGNodeRef node, in float maxHeight)
        {
            YGNodeStyleSetDimensions(node, node.getStyle().maxDimensions, YGDimension.Height, maxHeight, YGUnit.Point);
        }

        public static void YGNodeStyleSetMaxHeightPercent(in YGNodeRef node, in float maxHeight)
        {
            YGNodeStyleSetDimensions(node, node.getStyle().maxDimensions, YGDimension.Height, maxHeight, YGUnit.Percent);
        }

        public static YGValue YGNodeStyleGetMaxHeight(in YGNodeRef node)
        {
            return YGNodeStyleGetDimensions(node.getStyle().maxDimensions, YGDimension.Height);
        }

        //YG_NODE_LAYOUT_PROPERTY_IMPL(float, Left, position[YGEdge.Left]);
        public static float YGNodeLayoutGetLeft(in YGNodeRef node)
        {
            return node.getLayout().position[(int)YGEdge.Left];
        }

        //YG_NODE_LAYOUT_PROPERTY_IMPL(float,       Top,         position[   YGEdge.Top]);
        public static float YGNodeLayoutGetTop(in YGNodeRef node)
        {
            return node.getLayout().position[(int)YGEdge.Top];
        }

        //YG_NODE_LAYOUT_PROPERTY_IMPL(float,       Right,       position[   YGEdge.Right]);
        public static float YGNodeLayoutGetRight(in YGNodeRef node)
        {
            return node.getLayout().position[(int)YGEdge.Right];
        }

        // YG_NODE_LAYOUT_PROPERTY_IMPL(float,       Bottom,      position[   YGEdge.Bottom]);
        public static float YGNodeLayoutGetBottom(in YGNodeRef node)
        {
            return node.getLayout().position[(int)YGEdge.Bottom];
        }

        // YG_NODE_LAYOUT_PROPERTY_IMPL(float,       Width,       dimensions[ YGDimension.Width]);
        public static float YGNodeLayoutGetWidth(in YGNodeRef node)
        {
            return node.getLayout().dimensions[(int)YGDimension.Width];
        }

        //YG_NODE_LAYOUT_PROPERTY_IMPL(float,       Height,      dimensions[ YGDimension.Height]);
        public static float YGNodeLayoutGetHeight(in YGNodeRef node)
        {
            return node.getLayout().dimensions[(int)YGDimension.Height];
        }

        //YG_NODE_LAYOUT_PROPERTY_IMPL(YGDirection, Direction,   direction);
        public static YGDirection YGNodeLayoutGetDirection(in YGNodeRef node)
        {
            return node.getLayout().direction;
        }

        // YG_NODE_LAYOUT_PROPERTY_IMPL(bool,        HadOverflow, hadOverflow);
        public static bool YGNodeLayoutGetHadOverflow(in YGNodeRef node)
        {
            return node.getLayout().hadOverflow;
        }

        // YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(float, Margin,  margin);
        public static float YGNodeLayoutGetMargin(YGNodeRef node, YGEdge edge)
        {
            YGAssertWithNode(
                node,
                edge <= YGEdge.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
                case YGEdge.Left when node.getLayout().direction == YGDirection.RTL:
                    return node.getLayout().margin[(int)YGEdge.End];
                case YGEdge.Left:
                    return node.getLayout().margin[(int)YGEdge.Start];
                case YGEdge.Right when node.getLayout().direction == YGDirection.RTL:
                    return node.getLayout().margin[(int)YGEdge.Start];
                case YGEdge.Right:
                    return node.getLayout().margin[(int)YGEdge.End];
            }
            return node.getLayout().margin[(int)edge];
        }


        // YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(float, Border,  border);
        public static float YGNodeLayoutGetBorder(in YGNodeRef node, in YGEdge edge)
        {
            YGAssertWithNode(
                node,
                edge <= YGEdge.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
                case YGEdge.Left when node.getLayout().direction == YGDirection.RTL:
                    return node.getLayout().border[(int)YGEdge.End];
                case YGEdge.Left:
                    return node.getLayout().border[(int)YGEdge.Start];
                case YGEdge.Right when node.getLayout().direction == YGDirection.RTL:
                    return node.getLayout().border[(int)YGEdge.Start];
                case YGEdge.Right:
                    return node.getLayout().border[(int)YGEdge.End];
            }
            return node.getLayout().border[(int)edge];
        }

        // YG_NODE_LAYOUT_RESOLVED_PROPERTY_IMPL(float, Padding, padding);
        public static float YGNodeLayoutGetPadding(in YGNodeRef node, in YGEdge edge)
        {
            YGAssertWithNode(
                node,
                edge <= YGEdge.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
                case YGEdge.Left when node.getLayout().direction == YGDirection.RTL:
                    return node.getLayout().padding[(int)YGEdge.End];
                case YGEdge.Left:
                    return node.getLayout().padding[(int)YGEdge.Start];
                case YGEdge.Right when node.getLayout().direction == YGDirection.RTL:
                    return node.getLayout().padding[(int)YGEdge.Start];
                case YGEdge.Right:
                    return node.getLayout().padding[(int)YGEdge.End];
            }
            return node.getLayout().padding[(int)edge];
        }


        public static bool YGNodeLayoutGetDidLegacyStretchFlagAffectLayout(in YGNodeRef node)
        {
            return node.getLayout().doesLegacyStretchFlagAffectsLayout;
        }

        public static int gCurrentGenerationCount = 0;

        internal static void YGNodePrintInternal(
            in YGNodeRef node,
            in YGPrintOptions options)
        {
            var sb = YGNodeToString(null, node, options, 0);

            YGLog(node, YGLogLevel.Debug, sb.ToString());
        }

        public static void YGNodePrint(in YGNodeRef node, in YGPrintOptions options)
        {
            YGNodePrintInternal(node, options);
        }

        public static YGEdge[] leading = { YGEdge.Top, YGEdge.Bottom, YGEdge.Left, YGEdge.Right };
        public static YGEdge[] trailing = { YGEdge.Bottom, YGEdge.Top, YGEdge.Right, YGEdge.Left };

        internal static YGEdge[] pos = { YGEdge.Top, YGEdge.Bottom, YGEdge.Left, YGEdge.Right };
        internal static YGDimension[] dim = { YGDimension.Height, YGDimension.Height, YGDimension.Width, YGDimension.Width };

        internal static float YGNodePaddingAndBorderForAxis(
            in YGNodeRef node,
            in YGFlexDirection axis,
            in float widthSize)
        {
            return YGUnwrapFloatOptional(
                node.getLeadingPaddingAndBorder(axis, widthSize) +
                node.getTrailingPaddingAndBorder(axis, widthSize));
        }

        internal static YGAlign YGNodeAlignItem(
            in YGNodeRef node,
            in YGNodeRef child)
        {
            var align = child.getStyle().alignSelf == YGAlign.Auto
                ? node.getStyle().alignItems
                : child.getStyle().alignSelf;
            if (align == YGAlign.Baseline &&
                YGFlexDirectionIsColumn(node.getStyle().flexDirection))
                return YGAlign.FlexStart;

            return align;
        }

        internal static float YGBaseline(in YGNodeRef node)
        {
            if (node.getBaseline() != null)
            {
                var baseline = node.getBaseline()(
                    node,
                    node.getLayout().measuredDimensions[(int)YGDimension.Width],
                    node.getLayout().measuredDimensions[(int)YGDimension.Height]);
                YGAssertWithNode(
                    node,
                    !YGFloatIsUndefined(baseline),
                    "Expect custom baseline function to not return NaN");
                return baseline;
            }

            YGNodeRef baselineChild = null;
            var childCount = YGNodeGetChildCount(node);
            for (var i = 0; i < childCount; i++)
            {
                var child = YGNodeGetChild(node, i);
                if (child.getLineIndex() > 0) break;

                if (child.getStyle().positionType == YGPositionType.Absolute) continue;

                if (YGNodeAlignItem(node, child) == YGAlign.Baseline)
                {
                    baselineChild = child;
                    break;
                }

                if (baselineChild == null) baselineChild = child;
            }

            if (baselineChild == null) return node.getLayout().measuredDimensions[(int)YGDimension.Height];

            var childBaseline = YGBaseline(baselineChild);
            return childBaseline + baselineChild.getLayout().position[(int)YGEdge.Top];
        }

        internal static bool YGIsBaselineLayout(in YGNodeRef node)
        {
            if (YGFlexDirectionIsColumn(node.getStyle().flexDirection)) return false;

            if (node.getStyle().alignItems == YGAlign.Baseline) return true;

            var childCount = YGNodeGetChildCount(node);
            for (var i = 0; i < childCount; i++)
            {
                var child = YGNodeGetChild(node, i);
                if (child.getStyle().positionType == YGPositionType.Relative &&
                    child.getStyle().alignSelf == YGAlign.Baseline)
                    return true;
            }

            return false;
        }

        // inline
        internal static float YGNodeDimWithMargin(
            in YGNodeRef node,
            in YGFlexDirection axis,
            in float widthSize)
        {
            return node.getLayout().measuredDimensions[(int)dim[(int)axis]] +
                YGUnwrapFloatOptional(
                    node.getLeadingMargin(axis, widthSize) +
                    node.getTrailingMargin(axis, widthSize));
        }

        // inline
        internal static bool YGNodeIsStyleDimDefined(
            in YGNodeRef node,
            in YGFlexDirection axis,
            in float ownerSize)
        {
            var isUndefined =
                YGFloatIsUndefined(node.getResolvedDimension(dim[(int)axis]).value);
            return !(
                node.getResolvedDimension(dim[(int)axis]).unit == YGUnit.Auto ||
                node.getResolvedDimension(dim[(int)axis]).unit == YGUnit.Undefined ||
                node.getResolvedDimension(dim[(int)axis]).unit == YGUnit.Point &&
                !isUndefined                                                   && node.getResolvedDimension(dim[(int)axis]).value < 0.0f ||
                node.getResolvedDimension(dim[(int)axis]).unit == YGUnit.Percent &&
                !isUndefined                                                     &&
                (node.getResolvedDimension(dim[(int)axis]).value < 0.0f ||
                    YGFloatIsUndefined(ownerSize)));
        }

        // inline
        internal static bool YGNodeIsLayoutDimDefined(
            in YGNodeRef node,
            in YGFlexDirection axis)
        {
            var value = node.getLayout().measuredDimensions[(int)dim[(int)axis]];
            return !YGFloatIsUndefined(value) && value >= 0.0f;
        }

        internal static YGFloatOptional YGNodeBoundAxisWithinMinAndMax(
            in YGNodeRef node,
            in YGFlexDirection axis,
            in float value,
            in float axisSize)
        {
            var min = YGFloatOptional.Empty;
            var max = YGFloatOptional.Empty;

            if (YGFlexDirectionIsColumn(axis))
            {
                min = YGResolveValue(
                    node.getStyle().minDimensions[YGDimension.Height],
                    axisSize);
                max = YGResolveValue(
                    node.getStyle().maxDimensions[YGDimension.Height],
                    axisSize);
            }
            else if (YGFlexDirectionIsRow(axis))
            {
                min = YGResolveValue(
                    node.getStyle().minDimensions[YGDimension.Width],
                    axisSize);
                max = YGResolveValue(
                    node.getStyle().maxDimensions[YGDimension.Width],
                    axisSize);
            }

            if (!max.isUndefined() && max.getValue() >= 0 && value > max.getValue()) return max;

            if (!min.isUndefined() && min.getValue() >= 0 && value < min.getValue()) return min;

            return new YGFloatOptional(value);
        }

        // Like YGNodeBoundAxisWithinMinAndMax but also ensures that the value doesn't
        // go below the padding and border amount.
        // inline
        internal static float YGNodeBoundAxis(
            in YGNodeRef node,
            in YGFlexDirection axis,
            in float value,
            in float axisSize,
            in float widthSize)
        {
            return YGFloatMax(
                YGUnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(node, axis, value, axisSize)),
                YGNodePaddingAndBorderForAxis(node, axis, widthSize));
        }

        internal static void YGNodeSetChildTrailingPosition(
            in YGNodeRef node,
            in YGNodeRef child,
            in YGFlexDirection axis)
        {
            var size = child.getLayout().measuredDimensions[(int)dim[(int)axis]];
            child.setLayoutPosition(
                node.getLayout().measuredDimensions[(int)dim[(int)axis]] - size -
                child.getLayout().position[(int)pos[(int)axis]],
                trailing[(int)axis]);
        }

        internal static void YGConstrainMaxSizeForMode(
            in YGNodeRef node,
            in YGFlexDirection axis,
            in float ownerAxisSize,
            in float ownerWidth,
            ref YGMeasureMode mode,
            ref float size)
        {
            var maxSize =
                YGResolveValue(node.getStyle().maxDimensions[dim[(int)axis]], ownerAxisSize) +
                node.getMarginForAxis(axis, ownerWidth);
            switch (mode)
            {
                case YGMeasureMode.Exactly:
                case YGMeasureMode.AtMost:
                    size = maxSize.isUndefined() || size < maxSize.getValue()
                        ? size
                        : maxSize.getValue();
                    break;
                case YGMeasureMode.Undefined:
                    if (!maxSize.isUndefined())
                    {
                        mode = YGMeasureMode.AtMost;
                        size = maxSize.getValue();
                    }

                    break;
            }
        }

        internal static void YGNodeComputeFlexBasisForChild(
            in YGNodeRef node,
            in YGNodeRef child,
            in float width,
            in YGMeasureMode widthMode,
            in float height,
            in float ownerWidth,
            in float ownerHeight,
            in YGMeasureMode heightMode,
            in YGDirection direction,
            in YGConfigRef config)
        {
            var mainAxis = YGResolveFlexDirection(node.getStyle().flexDirection, direction);
            var isMainAxisRow = YGFlexDirectionIsRow(mainAxis);
            var mainAxisSize = isMainAxisRow ? width : height;
            var mainAxisownerSize = isMainAxisRow ? ownerWidth : ownerHeight;

            float childWidth;
            float childHeight;
            YGMeasureMode childWidthMeasureMode;
            YGMeasureMode childHeightMeasureMode;

            var resolvedFlexBasis = YGResolveValue(child.resolveFlexBasisPtr(), mainAxisownerSize);
            var isRowStyleDimDefined =
                YGNodeIsStyleDimDefined(child, YGFlexDirection.Row, ownerWidth);
            var isColumnStyleDimDefined =
                YGNodeIsStyleDimDefined(child, YGFlexDirection.Column, ownerHeight);

            if (!resolvedFlexBasis.isUndefined() && !YGFloatIsUndefined(mainAxisSize))
            {
                if (child.getLayout().computedFlexBasis.isUndefined() ||
                    YGConfigIsExperimentalFeatureEnabled(child.getConfig(), YGExperimentalFeature.WebFlexBasis) &&
                    child.getLayout().computedFlexBasisGeneration !=
                    gCurrentGenerationCount)
                {
                    var paddingAndBorder = new YGFloatOptional(YGNodePaddingAndBorderForAxis(child, mainAxis, ownerWidth));
                    child.setLayoutComputedFlexBasis(YGFloatOptionalMax(resolvedFlexBasis, paddingAndBorder));
                }
            }
            else if (isMainAxisRow && isRowStyleDimDefined)
            {
                // The width is definite, so use that as the flex basis.
                var paddingAndBorder = new YGFloatOptional(
                    YGNodePaddingAndBorderForAxis(child, YGFlexDirection.Row, ownerWidth));

                child.setLayoutComputedFlexBasis(
                    YGFloatOptionalMax(
                        YGResolveValue(child.getResolvedDimension(YGDimension.Width), ownerWidth),
                        paddingAndBorder));
            }
            else if (!isMainAxisRow && isColumnStyleDimDefined)
            {
                // The height is definite, so use that as the flex basis.
                var paddingAndBorder = new YGFloatOptional(
                    YGNodePaddingAndBorderForAxis(
                        child,
                        YGFlexDirection.Column,
                        ownerWidth));
                child.setLayoutComputedFlexBasis(
                    YGFloatOptionalMax(
                        YGResolveValue(child.getResolvedDimension(YGDimension.Height), ownerHeight),
                        paddingAndBorder));
            }
            else
            {
                // Compute the flex basis and hypothetical main size (i.e. the clamped
                // flex basis).
                childWidth = YGUndefined;
                childHeight = YGUndefined;
                childWidthMeasureMode = YGMeasureMode.Undefined;
                childHeightMeasureMode = YGMeasureMode.Undefined;

                var marginRow = YGUnwrapFloatOptional(
                    child.getMarginForAxis(YGFlexDirection.Row, ownerWidth));
                var marginColumn = YGUnwrapFloatOptional(
                    child.getMarginForAxis(YGFlexDirection.Column, ownerWidth));

                if (isRowStyleDimDefined)
                {
                    childWidth =
                        YGUnwrapFloatOptional(
                            YGResolveValue(
                                child.getResolvedDimension(YGDimension.Width),
                                ownerWidth)) +
                        marginRow;
                    childWidthMeasureMode = YGMeasureMode.Exactly;
                }

                if (isColumnStyleDimDefined)
                {
                    childHeight =
                        YGUnwrapFloatOptional(
                            YGResolveValue(
                                child.getResolvedDimension(YGDimension.Height),
                                ownerHeight)) +
                        marginColumn;
                    childHeightMeasureMode = YGMeasureMode.Exactly;
                }

                // The W3C spec doesn't say anything about the 'overflow' property,
                // but all major browsers appear to implement the following logic.
                if (!isMainAxisRow && node.getStyle().overflow == YGOverflow.Scroll ||
                    node.getStyle().overflow != YGOverflow.Scroll)
                    if (YGFloatIsUndefined(childWidth) && !YGFloatIsUndefined(width))
                    {
                        childWidth            = width;
                        childWidthMeasureMode = YGMeasureMode.AtMost;
                    }

                if (isMainAxisRow && node.getStyle().overflow == YGOverflow.Scroll ||
                    node.getStyle().overflow != YGOverflow.Scroll)
                    if (YGFloatIsUndefined(childHeight) && !YGFloatIsUndefined(height))
                    {
                        childHeight            = height;
                        childHeightMeasureMode = YGMeasureMode.AtMost;
                    }

                if (!child.getStyle().aspectRatio.isUndefined())
                {
                    if (!isMainAxisRow && childWidthMeasureMode == YGMeasureMode.Exactly)
                    {
                        childHeight = marginColumn +
                            (childWidth - marginRow) / child.getStyle().aspectRatio.getValue();
                        childHeightMeasureMode = YGMeasureMode.Exactly;
                    }
                    else if (
                        isMainAxisRow && childHeightMeasureMode == YGMeasureMode.Exactly)
                    {
                        childWidth = marginRow +
                            (childHeight - marginColumn) *
                            child.getStyle().aspectRatio.getValue();
                        childWidthMeasureMode = YGMeasureMode.Exactly;
                    }
                }

                // If child has no defined size in the cross axis and is set to stretch,
                // set the cross
                // axis to be measured exactly with the available inner width

                var hasExactWidth = !YGFloatIsUndefined(width) && widthMode == YGMeasureMode.Exactly;
                var childWidthStretch =
                    YGNodeAlignItem(node, child) == YGAlign.Stretch &&
                    childWidthMeasureMode != YGMeasureMode.Exactly;
                if (!isMainAxisRow && !isRowStyleDimDefined && hasExactWidth &&
                    childWidthStretch)
                {
                    childWidth = width;
                    childWidthMeasureMode = YGMeasureMode.Exactly;
                    if (!child.getStyle().aspectRatio.isUndefined())
                    {
                        childHeight =
                            (childWidth - marginRow) / child.getStyle().aspectRatio.getValue();
                        childHeightMeasureMode = YGMeasureMode.Exactly;
                    }
                }

                var hasExactHeight = !YGFloatIsUndefined(height) && heightMode == YGMeasureMode.Exactly;
                var childHeightStretch =
                    YGNodeAlignItem(node, child) == YGAlign.Stretch &&
                    childHeightMeasureMode != YGMeasureMode.Exactly;
                if (isMainAxisRow && !isColumnStyleDimDefined && hasExactHeight &&
                    childHeightStretch)
                {
                    childHeight = height;
                    childHeightMeasureMode = YGMeasureMode.Exactly;

                    if (!child.getStyle().aspectRatio.isUndefined())
                    {
                        childWidth = (childHeight - marginColumn) *
                            child.getStyle().aspectRatio.getValue();
                        childWidthMeasureMode = YGMeasureMode.Exactly;
                    }
                }

                YGConstrainMaxSizeForMode(
                    child,
                    YGFlexDirection.Row,
                    ownerWidth,
                    ownerWidth,
                    ref childWidthMeasureMode,
                    ref childWidth);
                YGConstrainMaxSizeForMode(
                    child,
                    YGFlexDirection.Column,
                    ownerHeight,
                    ownerWidth,
                    ref childHeightMeasureMode,
                    ref childHeight);

                // Measure the child
                YGLayoutNodeInternal(
                    child,
                    childWidth,
                    childHeight,
                    direction,
                    childWidthMeasureMode,
                    childHeightMeasureMode,
                    ownerWidth,
                    ownerHeight,
                    false,
                    "measure",
                    config);

                child.setLayoutComputedFlexBasis(
                    new YGFloatOptional(
                        YGFloatMax(
                            child.getLayout().measuredDimensions[(int)dim[(int)mainAxis]],
                            YGNodePaddingAndBorderForAxis(child, mainAxis, ownerWidth))));
            }

            child.setLayoutComputedFlexBasisGeneration(gCurrentGenerationCount);
        }

        internal static void YGNodeAbsoluteLayoutChild(
            in YGNodeRef node,
            in YGNodeRef child,
            in float width,
            in YGMeasureMode widthMode,
            in float height,
            in YGDirection direction,
            in YGConfigRef config)
        {
            var mainAxis = YGResolveFlexDirection(node.getStyle().flexDirection, direction);
            var crossAxis = YGFlexDirectionCross(mainAxis, direction);
            var isMainAxisRow = YGFlexDirectionIsRow(mainAxis);

            var childWidth = YGUndefined;
            var childHeight = YGUndefined;
            var childWidthMeasureMode = YGMeasureMode.Undefined;
            var childHeightMeasureMode = YGMeasureMode.Undefined;

            var marginRow =
                YGUnwrapFloatOptional(child.getMarginForAxis(YGFlexDirection.Row, width));
            var marginColumn = YGUnwrapFloatOptional(
                child.getMarginForAxis(YGFlexDirection.Column, width));

            if (YGNodeIsStyleDimDefined(child, YGFlexDirection.Row, width))
            {
                childWidth = YGUnwrapFloatOptional(
                        YGResolveValue(
                            child.getResolvedDimension(YGDimension.Width),
                            width)) +
                    marginRow;
            }
            else
            {
                // If the child doesn't have a specified width, compute the width based
                // on the left/right
                // offsets if they're defined.
                if (child.isLeadingPositionDefined(YGFlexDirection.Row) &&
                    child.isTrailingPosDefined(YGFlexDirection.Row))
                {
                    childWidth = node.getLayout().measuredDimensions[(int)YGDimension.Width] -
                        (node.getLeadingBorder(YGFlexDirection.Row) +
                            node.getTrailingBorder(YGFlexDirection.Row)) -
                        YGUnwrapFloatOptional(
                            child.getLeadingPosition(YGFlexDirection.Row, width) +
                            child.getTrailingPosition(YGFlexDirection.Row, width));
                    childWidth =
                        YGNodeBoundAxis(child, YGFlexDirection.Row, childWidth, width, width);
                }
            }

            if (YGNodeIsStyleDimDefined(child, YGFlexDirection.Column, height))
            {
                childHeight = YGUnwrapFloatOptional(
                        YGResolveValue(
                            child.getResolvedDimension(YGDimension.Height),
                            height)) +
                    marginColumn;
            }
            else
            {
                // If the child doesn't have a specified height, compute the height
                // based on the top/bottom
                // offsets if they're defined.
                if (child.isLeadingPositionDefined(YGFlexDirection.Column) &&
                    child.isTrailingPosDefined(YGFlexDirection.Column))
                {
                    childHeight =
                        node.getLayout().measuredDimensions[(int)YGDimension.Height] -
                        (node.getLeadingBorder(YGFlexDirection.Column) +
                            node.getTrailingBorder(YGFlexDirection.Column)) -
                        YGUnwrapFloatOptional(
                            child.getLeadingPosition(YGFlexDirection.Column, height) +
                            child.getTrailingPosition(YGFlexDirection.Column, height));
                    childHeight = YGNodeBoundAxis(
                        child,
                        YGFlexDirection.Column,
                        childHeight,
                        height,
                        width);
                }
            }

            // Exactly one dimension needs to be defined for us to be able to do aspect
            // ratio calculation. One dimension being the anchor and the other being
            // flexible.
            if (YGFloatIsUndefined(childWidth) ^ YGFloatIsUndefined(childHeight))
                if (!child.getStyle().aspectRatio.isUndefined())
                {
                    if (YGFloatIsUndefined(childWidth))
                        childWidth = marginRow +
                            (childHeight - marginColumn) *
                            child.getStyle().aspectRatio.getValue();
                    else if (YGFloatIsUndefined(childHeight))
                        childHeight = marginColumn +
                            (childWidth - marginRow) / child.getStyle().aspectRatio.getValue();
                }

            // If we're still missing one or the other dimension, measure the content.
            if (YGFloatIsUndefined(childWidth) || YGFloatIsUndefined(childHeight))
            {
                childWidthMeasureMode = YGFloatIsUndefined(childWidth)
                    ? YGMeasureMode.Undefined
                    : YGMeasureMode.Exactly;
                childHeightMeasureMode = YGFloatIsUndefined(childHeight)
                    ? YGMeasureMode.Undefined
                    : YGMeasureMode.Exactly;

                // If the size of the owner is defined then try to constrain the absolute
                // child to that size as well. This allows text within the absolute child to
                // wrap to the size of its owner. This is the same behavior as many browsers
                // implement.
                if (!isMainAxisRow && YGFloatIsUndefined(childWidth) &&
                    widthMode != YGMeasureMode.Undefined && !YGFloatIsUndefined(width) &&
                    width > 0)
                {
                    childWidth = width;
                    childWidthMeasureMode = YGMeasureMode.AtMost;
                }

                YGLayoutNodeInternal(
                    child,
                    childWidth,
                    childHeight,
                    direction,
                    childWidthMeasureMode,
                    childHeightMeasureMode,
                    childWidth,
                    childHeight,
                    false,
                    "abs-measure",
                    config);
                childWidth = child.getLayout().measuredDimensions[(int)YGDimension.Width] +
                    YGUnwrapFloatOptional(
                        child.getMarginForAxis(YGFlexDirection.Row, width));
                childHeight = child.getLayout().measuredDimensions[(int)YGDimension.Height] +
                    YGUnwrapFloatOptional(
                        child.getMarginForAxis(YGFlexDirection.Column, width));
            }

            YGLayoutNodeInternal(
                child,
                childWidth,
                childHeight,
                direction,
                YGMeasureMode.Exactly,
                YGMeasureMode.Exactly,
                childWidth,
                childHeight,
                true,
                "abs-layout",
                config);

            if (child.isTrailingPosDefined(mainAxis) &&
                !child.isLeadingPositionDefined(mainAxis))
                child.setLayoutPosition(
                    node.getLayout().measuredDimensions[(int)dim[(int)mainAxis]]    -
                    child.getLayout().measuredDimensions[(int)dim[(int)mainAxis]]   -
                    node.getTrailingBorder(mainAxis)                                -
                    YGUnwrapFloatOptional(child.getTrailingMargin(mainAxis, width)) -
                    YGUnwrapFloatOptional(
                        child.getTrailingPosition(
                            mainAxis,
                            isMainAxisRow ? width : height)),
                    leading[(int)mainAxis]);
            else if (
                !child.isLeadingPositionDefined(mainAxis) &&
                node.getStyle().justifyContent == YGJustify.Center)
                child.setLayoutPosition(
                    (node.getLayout().measuredDimensions[(int)dim[(int)mainAxis]] -
                        child.getLayout().measuredDimensions[(int)dim[(int)mainAxis]]) /
                    2.0f,
                    leading[(int)mainAxis]);
            else if (
                !child.isLeadingPositionDefined(mainAxis) &&
                node.getStyle().justifyContent == YGJustify.FlexEnd)
                child.setLayoutPosition(
                    node.getLayout().measuredDimensions[(int)dim[(int)mainAxis]] -
                    child.getLayout().measuredDimensions[(int)dim[(int)mainAxis]],
                    leading[(int)mainAxis]);

            if (child.isTrailingPosDefined(crossAxis) &&
                !child.isLeadingPositionDefined(crossAxis))
                child.setLayoutPosition(
                    node.getLayout().measuredDimensions[(int)dim[(int)crossAxis]]    -
                    child.getLayout().measuredDimensions[(int)dim[(int)crossAxis]]   -
                    node.getTrailingBorder(crossAxis)                                -
                    YGUnwrapFloatOptional(child.getTrailingMargin(crossAxis, width)) -
                    YGUnwrapFloatOptional(
                        child.getTrailingPosition(
                            crossAxis,
                            isMainAxisRow ? height : width)),
                    leading[(int)crossAxis]);
            else if (
                !child.isLeadingPositionDefined(crossAxis) &&
                YGNodeAlignItem(node, child) == YGAlign.Center)
                child.setLayoutPosition(
                    (node.getLayout().measuredDimensions[(int)dim[(int)crossAxis]] -
                        child.getLayout().measuredDimensions[(int)dim[(int)crossAxis]]) /
                    2.0f,
                    leading[(int)crossAxis]);
            else if (
                !child.isLeadingPositionDefined(crossAxis) &&
                (YGNodeAlignItem(node, child) == YGAlign.FlexEnd) ^
                (node.getStyle().flexWrap     == YGWrap.WrapReverse))
                child.setLayoutPosition(
                    node.getLayout().measuredDimensions[(int)dim[(int)crossAxis]] -
                    child.getLayout().measuredDimensions[(int)dim[(int)crossAxis]],
                    leading[(int)crossAxis]);
        }

        internal static void YGNodeWithMeasureFuncSetMeasuredDimensions(
            in YGNodeRef node,
            in float availableWidth,
            in float availableHeight,
            in YGMeasureMode widthMeasureMode,
            in YGMeasureMode heightMeasureMode,
            in float ownerWidth,
            in float ownerHeight)
        {
            YGAssertWithNode(
                node,
                node.getMeasure() != null,
                "Expected node to have custom measure function");

            var paddingAndBorderAxisRow = YGNodePaddingAndBorderForAxis(node, YGFlexDirection.Row, availableWidth);
            var paddingAndBorderAxisColumn = YGNodePaddingAndBorderForAxis(node, YGFlexDirection.Column, availableWidth);
            var marginAxisRow = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Row, availableWidth));
            var marginAxisColumn = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Column, availableWidth));

            // We want to make sure we don't call measure with negative size
            var innerWidth = YGFloatIsUndefined(availableWidth)
                ? availableWidth
                : YGFloatMax(0, availableWidth - marginAxisRow - paddingAndBorderAxisRow);
            var innerHeight = YGFloatIsUndefined(availableHeight)
                ? availableHeight
                : YGFloatMax(0, availableHeight - marginAxisColumn - paddingAndBorderAxisColumn);

            if (widthMeasureMode == YGMeasureMode.Exactly &&
                heightMeasureMode == YGMeasureMode.Exactly)
            {
                // Don't bother sizing the text if both dimensions are already defined.
                node.setLayoutMeasuredDimension(
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Row,
                        availableWidth - marginAxisRow,
                        ownerWidth,
                        ownerWidth),
                    YGDimension.Width);
                node.setLayoutMeasuredDimension(
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Column,
                        availableHeight - marginAxisColumn,
                        ownerHeight,
                        ownerWidth),
                    YGDimension.Height);
            }
            else
            {
                // Measure the text under the current constraints.
                var measuredSize = node.getMeasure()(node, innerWidth, widthMeasureMode, innerHeight, heightMeasureMode);

                node.setLayoutMeasuredDimension(
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Row,
                        widthMeasureMode == YGMeasureMode.Undefined ||
                        widthMeasureMode == YGMeasureMode.AtMost
                            ? measuredSize.width + paddingAndBorderAxisRow
                            : availableWidth - marginAxisRow,
                        ownerWidth,
                        ownerWidth),
                    YGDimension.Width);

                node.setLayoutMeasuredDimension(
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Column,
                        heightMeasureMode == YGMeasureMode.Undefined ||
                        heightMeasureMode == YGMeasureMode.AtMost
                            ? measuredSize.height + paddingAndBorderAxisColumn
                            : availableHeight - marginAxisColumn,
                        ownerHeight,
                        ownerWidth),
                    YGDimension.Height);
            }
        }

        // For nodes with no children, use the available values if they were provided,
        // or the minimum size as indicated by the padding and border sizes.
        internal static void YGNodeEmptyContainerSetMeasuredDimensions(
            in YGNodeRef node,
            in float availableWidth,
            in float availableHeight,
            in YGMeasureMode widthMeasureMode,
            in YGMeasureMode heightMeasureMode,
            in float ownerWidth,
            in float ownerHeight)
        {
            var paddingAndBorderAxisRow = YGNodePaddingAndBorderForAxis(node, YGFlexDirection.Row, ownerWidth);
            var paddingAndBorderAxisColumn = YGNodePaddingAndBorderForAxis(node, YGFlexDirection.Column, ownerWidth);
            var marginAxisRow = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Row, ownerWidth));
            var marginAxisColumn = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Column, ownerWidth));

            node.setLayoutMeasuredDimension(
                YGNodeBoundAxis(
                    node,
                    YGFlexDirection.Row,
                    widthMeasureMode == YGMeasureMode.Undefined ||
                    widthMeasureMode == YGMeasureMode.AtMost
                        ? paddingAndBorderAxisRow
                        : availableWidth - marginAxisRow,
                    ownerWidth,
                    ownerWidth),
                YGDimension.Width);

            node.setLayoutMeasuredDimension(
                YGNodeBoundAxis(
                    node,
                    YGFlexDirection.Column,
                    heightMeasureMode == YGMeasureMode.Undefined ||
                    heightMeasureMode == YGMeasureMode.AtMost
                        ? paddingAndBorderAxisColumn
                        : availableHeight - marginAxisColumn,
                    ownerHeight,
                    ownerWidth),
                YGDimension.Height);
        }

        internal static bool YGNodeFixedSizeSetMeasuredDimensions(
            in YGNodeRef node,
            in float availableWidth,
            in float availableHeight,
            in YGMeasureMode widthMeasureMode,
            in YGMeasureMode heightMeasureMode,
            in float ownerWidth,
            in float ownerHeight)
        {
            if (!YGFloatIsUndefined(availableWidth)      &&
                widthMeasureMode == YGMeasureMode.AtMost && availableWidth <= 0.0f ||
                !YGFloatIsUndefined(availableHeight)      &&
                heightMeasureMode == YGMeasureMode.AtMost && availableHeight <= 0.0f ||
                widthMeasureMode  == YGMeasureMode.Exactly &&
                heightMeasureMode == YGMeasureMode.Exactly)
            {
                var marginAxisColumn = YGUnwrapFloatOptional(
                    node.getMarginForAxis(YGFlexDirection.Column, ownerWidth));
                var marginAxisRow = YGUnwrapFloatOptional(
                    node.getMarginForAxis(YGFlexDirection.Row, ownerWidth));

                node.setLayoutMeasuredDimension(
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Row,
                        YGFloatIsUndefined(availableWidth) ||
                        widthMeasureMode == YGMeasureMode.AtMost &&
                        availableWidth   < 0.0f
                            ? 0.0f
                            : availableWidth - marginAxisRow,
                        ownerWidth,
                        ownerWidth),
                    YGDimension.Width);

                node.setLayoutMeasuredDimension(
                    YGNodeBoundAxis(
                        node,
                        YGFlexDirection.Column,
                        YGFloatIsUndefined(availableHeight) ||
                        heightMeasureMode == YGMeasureMode.AtMost &&
                        availableHeight   < 0.0f
                            ? 0.0f
                            : availableHeight - marginAxisColumn,
                        ownerHeight,
                        ownerWidth),
                    YGDimension.Height);
                return true;
            }

            return false;
        }

        private static void YGZeroOutLayoutRecursivly(in YGNodeRef node)
        {
            node.setLayout(new YGLayout());
            node.setHasNewLayout(true);

            node.cloneChildrenIfNeeded();
            var childCount = YGNodeGetChildCount(node);
            for (var i = 0; i < childCount; i++)
            {
                var child = node.getChild(i);
                YGZeroOutLayoutRecursivly(child);
            }
        }

        internal static float YGNodeCalculateAvailableInnerDim(
            in YGNodeRef node,
            YGFlexDirection axis,
            float availableDim,
            float ownerDim)
        {
            var direction =
                YGFlexDirectionIsRow(axis) ? YGFlexDirection.Row : YGFlexDirection.Column;
            var dimension =
                YGFlexDirectionIsRow(axis) ? YGDimension.Width : YGDimension.Height;

            var margin = YGUnwrapFloatOptional(node.getMarginForAxis(direction, ownerDim));
            var paddingAndBorder = YGNodePaddingAndBorderForAxis(node, direction, ownerDim);

            var availableInnerDim = availableDim - margin - paddingAndBorder;
            // Max dimension overrides predefined dimension value; Min dimension in turn
            // overrides both of the above
            if (!YGFloatIsUndefined(availableInnerDim))
            {
                // We want to make sure our available height does not violate min and max
                // constraints
                var minDimensionOptional =
                    YGResolveValue(node.getStyle().minDimensions[dimension], ownerDim);
                var minInnerDim = minDimensionOptional.isUndefined()
                    ? 0.0f
                    : minDimensionOptional.getValue() - paddingAndBorder;

                var maxDimensionOptional =
                    YGResolveValue(node.getStyle().maxDimensions[dimension], ownerDim);

                var maxInnerDim = maxDimensionOptional.isUndefined()
                    ? float.MaxValue
                    : maxDimensionOptional.getValue() - paddingAndBorder;
                availableInnerDim =
                    YGFloatMax(YGFloatMin(availableInnerDim, maxInnerDim), minInnerDim);
            }

            return availableInnerDim;
        }

        private static void YGNodeComputeFlexBasisForChildren(
            in YGNodeRef node,
            in float availableInnerWidth,
            in float availableInnerHeight,
            YGMeasureMode widthMeasureMode,
            YGMeasureMode heightMeasureMode,
            YGDirection direction,
            YGFlexDirection mainAxis,
            in YGConfigRef config,
            bool performLayout,
            float totalOuterFlexBasis)
        {
            YGNodeRef singleFlexChild = null;
            var children = node.getChildren();
            var measureModeMainDim =
                YGFlexDirectionIsRow(mainAxis) ? widthMeasureMode : heightMeasureMode;
            // If there is only one child with flexGrow + flexShrink it means we can set
            // the computedFlexBasis to 0 instead of measuring and shrinking / flexing the
            // child to exactly match the remaining space
            if (measureModeMainDim == YGMeasureMode.Exactly)
                foreach (var child in children)
                    if (child.isNodeFlexible())
                    {
                        if (singleFlexChild != null                        ||
                            YGFloatsEqual(child.resolveFlexGrow(),   0.0f) ||
                            YGFloatsEqual(child.resolveFlexShrink(), 0.0f))
                        {
                            // There is already a flexible child, or this flexible child doesn't
                            // have flexGrow and flexShrink, abort
                            singleFlexChild = null;
                            break;
                        }
                        else
                        {
                            singleFlexChild = child;
                        }
                    }

            foreach (var child in children)
            {
                child.resolveDimension();
                if (child.getStyle().display == YGDisplay.None)
                {
                    YGZeroOutLayoutRecursivly(child);
                    child.setHasNewLayout(true);
                    child.setDirty(false);
                    continue;
                }

                if (performLayout)
                {
                    // Set the initial position (relative to the owner).
                    var childDirection = child.resolveDirection(direction);
                    var mainDim = YGFlexDirectionIsRow(mainAxis)
                        ? availableInnerWidth
                        : availableInnerHeight;
                    var crossDim = YGFlexDirectionIsRow(mainAxis)
                        ? availableInnerHeight
                        : availableInnerWidth;
                    child.setPosition(
                        childDirection,
                        mainDim,
                        crossDim,
                        availableInnerWidth);
                }

                if (child.getStyle().positionType == YGPositionType.Absolute) continue;

                if (child == singleFlexChild)
                {
                    child.setLayoutComputedFlexBasisGeneration(gCurrentGenerationCount);
                    child.setLayoutComputedFlexBasis(new YGFloatOptional(0));
                }
                else
                {
                    YGNodeComputeFlexBasisForChild(
                        node,
                        child,
                        availableInnerWidth,
                        widthMeasureMode,
                        availableInnerHeight,
                        availableInnerWidth,
                        availableInnerHeight,
                        heightMeasureMode,
                        direction,
                        config);
                }

                totalOuterFlexBasis += YGUnwrapFloatOptional(
                    child.getLayout().computedFlexBasis +
                    child.getMarginForAxis(mainAxis, availableInnerWidth));
            }
        }

        // This function assumes that all the children of node have their
        // computedFlexBasis properly computed(To do this use
        // YGNodeComputeFlexBasisForChildren function).
        // This function calculates YGCollectFlexItemsRowMeasurement
        internal static YGCollectFlexItemsRowValues YGCalculateCollectFlexItemsRowValues(
            in YGNodeRef node,
            in YGDirection ownerDirection,
            in float mainAxisownerSize,
            in float availableInnerWidth,
            in float availableInnerMainDim,
            in int startOfLineIndex,
            in int lineCount)
        {
            var flexAlgoRowMeasurement = new YGCollectFlexItemsRowValues
            {
                relativeChildren = new YGVector(node.getChildren().Count)
            };

            float sizeConsumedOnCurrentLineIncludingMinConstraint = 0;
            var mainAxis = YGResolveFlexDirection(node.getStyle().flexDirection, node.resolveDirection(ownerDirection));
            var isNodeFlexWrap = node.getStyle().flexWrap != YGWrap.NoWrap;

            // Add items to the current line until it's full or we run out of items.
            var endOfLineIndex = startOfLineIndex;
            for (; endOfLineIndex < node.getChildren().Count; endOfLineIndex++)
            {
                var child = node.getChild(endOfLineIndex);
                if (child.getStyle().display == YGDisplay.None ||
                    child.getStyle().positionType == YGPositionType.Absolute)
                    continue;

                child.setLineIndex(lineCount);
                var childMarginMainAxis = YGUnwrapFloatOptional(child.getMarginForAxis(mainAxis, availableInnerWidth));
                var flexBasisWithMinAndMaxConstraints = YGUnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        child,
                        mainAxis,
                        YGUnwrapFloatOptional(child.getLayout().computedFlexBasis),
                        mainAxisownerSize));

                // If this is a multi-line flow and this item pushes us over the
                // available size, we've
                // hit the end of the current line. Break out of the loop and lay out
                // the current line.
                if (sizeConsumedOnCurrentLineIncludingMinConstraint +
                    flexBasisWithMinAndMaxConstraints + childMarginMainAxis >
                    availableInnerMainDim &&
                    isNodeFlexWrap && flexAlgoRowMeasurement.itemsOnLine > 0)
                    break;

                sizeConsumedOnCurrentLineIncludingMinConstraint +=
                    flexBasisWithMinAndMaxConstraints + childMarginMainAxis;
                flexAlgoRowMeasurement.sizeConsumedOnCurrentLine +=
                    flexBasisWithMinAndMaxConstraints + childMarginMainAxis;
                flexAlgoRowMeasurement.itemsOnLine++;

                if (child.isNodeFlexible())
                {
                    flexAlgoRowMeasurement.totalFlexGrowFactors += child.resolveFlexGrow();

                    // Unlike the grow factor, the shrink factor is scaled relative to the
                    // child dimension.
                    flexAlgoRowMeasurement.totalFlexShrinkScaledFactors +=
                        -child.resolveFlexShrink() *
                        YGUnwrapFloatOptional(child.getLayout().computedFlexBasis);
                }

                flexAlgoRowMeasurement.relativeChildren.Add(child);
            }

            // The total flex factor needs to be floored to 1.
            if (flexAlgoRowMeasurement.totalFlexGrowFactors > 0 &&
                flexAlgoRowMeasurement.totalFlexGrowFactors < 1)
                flexAlgoRowMeasurement.totalFlexGrowFactors = 1;

            // The total flex shrink factor needs to be floored to 1.
            if (flexAlgoRowMeasurement.totalFlexShrinkScaledFactors > 0 &&
                flexAlgoRowMeasurement.totalFlexShrinkScaledFactors < 1)
                flexAlgoRowMeasurement.totalFlexShrinkScaledFactors = 1;

            flexAlgoRowMeasurement.endOfLineIndex = endOfLineIndex;
            return flexAlgoRowMeasurement;
        }

        // It distributes the free space to the flexible items and ensures that the size
        // of the flex items abide the min and max constraints. At the end of this
        // function the child nodes would have proper size. Prior using this function
        // please ensure that YGDistributeFreeSpaceFirstPass is called.
        internal static float YGDistributeFreeSpaceSecondPass(
            YGCollectFlexItemsRowValues collectedFlexItemsValues,
            in YGNodeRef node,
            in YGFlexDirection mainAxis,
            in YGFlexDirection crossAxis,
            in float mainAxisownerSize,
            in float availableInnerMainDim,
            in float availableInnerCrossDim,
            in float availableInnerWidth,
            in float availableInnerHeight,
            in bool flexBasisOverflows,
            in YGMeasureMode measureModeCrossDim,
            in bool performLayout,
            in YGConfigRef config)
        {
            float childFlexBasis = 0;
            float flexShrinkScaledFactor = 0;
            float flexGrowFactor = 0;
            float deltaFreeSpace = 0;
            var isMainAxisRow = YGFlexDirectionIsRow(mainAxis);
            var isNodeFlexWrap = node.getStyle().flexWrap != YGWrap.NoWrap;

            foreach (var currentRelativeChild in collectedFlexItemsValues.relativeChildren)
            {
                childFlexBasis = YGUnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        currentRelativeChild,
                        mainAxis,
                        YGUnwrapFloatOptional(
                            currentRelativeChild.getLayout().computedFlexBasis),
                        mainAxisownerSize));
                var updatedMainSize = childFlexBasis;

                if (!YGFloatIsUndefined(collectedFlexItemsValues.remainingFreeSpace) &&
                    collectedFlexItemsValues.remainingFreeSpace < 0)
                {
                    flexShrinkScaledFactor =
                        -currentRelativeChild.resolveFlexShrink() * childFlexBasis;
                    // Is this child able to shrink?
                    if (flexShrinkScaledFactor != 0)
                    {
                        float childSize;

                        if (!YGFloatIsUndefined(
                                collectedFlexItemsValues.totalFlexShrinkScaledFactors) &&
                            collectedFlexItemsValues.totalFlexShrinkScaledFactors == 0)
                            childSize = childFlexBasis + flexShrinkScaledFactor;
                        else
                            childSize = childFlexBasis +
                                collectedFlexItemsValues.remainingFreeSpace /
                                collectedFlexItemsValues.totalFlexShrinkScaledFactors *
                                flexShrinkScaledFactor;

                        updatedMainSize = YGNodeBoundAxis(
                            currentRelativeChild,
                            mainAxis,
                            childSize,
                            availableInnerMainDim,
                            availableInnerWidth);
                    }
                }
                else if (
                    !YGFloatIsUndefined(collectedFlexItemsValues.remainingFreeSpace) &&
                    collectedFlexItemsValues.remainingFreeSpace > 0)
                {
                    flexGrowFactor = currentRelativeChild.resolveFlexGrow();

                    // Is this child able to grow?
                    if (!YGFloatIsUndefined(flexGrowFactor) && flexGrowFactor != 0)
                        updatedMainSize = YGNodeBoundAxis(
                            currentRelativeChild,
                            mainAxis,
                            childFlexBasis +
                            collectedFlexItemsValues.remainingFreeSpace /
                            collectedFlexItemsValues.totalFlexGrowFactors *
                            flexGrowFactor,
                            availableInnerMainDim,
                            availableInnerWidth);
                }

                deltaFreeSpace += updatedMainSize - childFlexBasis;

                var marginMain = YGUnwrapFloatOptional(currentRelativeChild.getMarginForAxis(mainAxis, availableInnerWidth));
                var marginCross = YGUnwrapFloatOptional(currentRelativeChild.getMarginForAxis(crossAxis, availableInnerWidth));

                float childCrossSize;
                var childMainSize = updatedMainSize + marginMain;
                YGMeasureMode childCrossMeasureMode;
                var childMainMeasureMode = YGMeasureMode.Exactly;

                if (!currentRelativeChild.getStyle().aspectRatio.isUndefined())
                {
                    childCrossSize = isMainAxisRow
                        ? (childMainSize - marginMain) /
                        currentRelativeChild.getStyle().aspectRatio.getValue()
                        : (childMainSize - marginMain) *
                        currentRelativeChild.getStyle().aspectRatio.getValue();
                    childCrossMeasureMode = YGMeasureMode.Exactly;

                    childCrossSize += marginCross;
                }
                else if (
                    !YGFloatIsUndefined(availableInnerCrossDim) &&
                    !YGNodeIsStyleDimDefined(
                        currentRelativeChild,
                        crossAxis,
                        availableInnerCrossDim) &&
                    measureModeCrossDim == YGMeasureMode.Exactly &&
                    !(isNodeFlexWrap && flexBasisOverflows) &&
                    YGNodeAlignItem(node, currentRelativeChild) == YGAlign.Stretch &&
                    currentRelativeChild.marginLeadingValue(crossAxis).unit !=
                    YGUnit.Auto &&
                    currentRelativeChild.marginTrailingValue(crossAxis).unit !=
                    YGUnit.Auto)
                {
                    childCrossSize = availableInnerCrossDim;
                    childCrossMeasureMode = YGMeasureMode.Exactly;
                }
                else if (!YGNodeIsStyleDimDefined(
                    currentRelativeChild,
                    crossAxis,
                    availableInnerCrossDim))
                {
                    childCrossSize = availableInnerCrossDim;
                    childCrossMeasureMode = YGFloatIsUndefined(childCrossSize)
                        ? YGMeasureMode.Undefined
                        : YGMeasureMode.AtMost;
                }
                else
                {
                    childCrossSize =
                        YGUnwrapFloatOptional(
                            YGResolveValue(
                                currentRelativeChild.getResolvedDimension(dim[(int)crossAxis]),
                                availableInnerCrossDim)) +
                        marginCross;
                    var isLoosePercentageMeasurement =
                        currentRelativeChild.getResolvedDimension(dim[(int)crossAxis]).unit ==
                        YGUnit.Percent &&
                        measureModeCrossDim != YGMeasureMode.Exactly;
                    childCrossMeasureMode =
                        YGFloatIsUndefined(childCrossSize) || isLoosePercentageMeasurement
                            ? YGMeasureMode.Undefined
                            : YGMeasureMode.Exactly;
                }

                YGConstrainMaxSizeForMode(
                    currentRelativeChild,
                    mainAxis,
                    availableInnerMainDim,
                    availableInnerWidth,
                    ref childMainMeasureMode,
                    ref childMainSize);
                YGConstrainMaxSizeForMode(
                    currentRelativeChild,
                    crossAxis,
                    availableInnerCrossDim,
                    availableInnerWidth,
                    ref childCrossMeasureMode,
                    ref childCrossSize);

                var requiresStretchLayout =
                    !YGNodeIsStyleDimDefined(
                        currentRelativeChild,
                        crossAxis,
                        availableInnerCrossDim) &&
                    YGNodeAlignItem(node, currentRelativeChild) == YGAlign.Stretch &&
                    currentRelativeChild.marginLeadingValue(crossAxis).unit !=
                    YGUnit.Auto &&
                    currentRelativeChild.marginTrailingValue(crossAxis).unit != YGUnit.Auto;

                var childWidth = isMainAxisRow ? childMainSize : childCrossSize;
                var childHeight = !isMainAxisRow ? childMainSize : childCrossSize;

                var childWidthMeasureMode = isMainAxisRow ? childMainMeasureMode : childCrossMeasureMode;
                var childHeightMeasureMode = !isMainAxisRow ? childMainMeasureMode : childCrossMeasureMode;

                // Recursively call the layout algorithm for this child with the updated
                // main size.
                YGLayoutNodeInternal(
                    currentRelativeChild,
                    childWidth,
                    childHeight,
                    node.getLayout().direction,
                    childWidthMeasureMode,
                    childHeightMeasureMode,
                    availableInnerWidth,
                    availableInnerHeight,
                    performLayout && !requiresStretchLayout,
                    "flex",
                    config);
                node.setLayoutHadOverflow(
                    node.getLayout().hadOverflow |
                    currentRelativeChild.getLayout().hadOverflow);
            }

            return deltaFreeSpace;
        }

        // It distributes the free space to the flexible items.For those flexible items
        // whose min and max constraints are triggered, those flex item's clamped size
        // is removed from the remaingfreespace.
        internal static void YGDistributeFreeSpaceFirstPass(
            YGCollectFlexItemsRowValues collectedFlexItemsValues,
            in YGFlexDirection mainAxis,
            in float mainAxisownerSize,
            in float availableInnerMainDim,
            in float availableInnerWidth)
        {
            float flexShrinkScaledFactor = 0;
            float flexGrowFactor = 0;
            float baseMainSize = 0;
            float boundMainSize = 0;
            float deltaFreeSpace = 0;

            foreach (var currentRelativeChild in collectedFlexItemsValues.relativeChildren)
            {
                var childFlexBasis = YGUnwrapFloatOptional(
                    YGNodeBoundAxisWithinMinAndMax(
                        currentRelativeChild,
                        mainAxis,
                        YGUnwrapFloatOptional(
                            currentRelativeChild.getLayout().computedFlexBasis),
                        mainAxisownerSize));

                if (collectedFlexItemsValues.remainingFreeSpace < 0)
                {
                    flexShrinkScaledFactor =
                        -currentRelativeChild.resolveFlexShrink() * childFlexBasis;

                    // Is this child able to shrink?
                    if (!YGFloatIsUndefined(flexShrinkScaledFactor) &&
                        flexShrinkScaledFactor != 0)
                    {
                        baseMainSize = childFlexBasis +
                            collectedFlexItemsValues.remainingFreeSpace /
                            collectedFlexItemsValues.totalFlexShrinkScaledFactors *
                            flexShrinkScaledFactor;
                        boundMainSize = YGNodeBoundAxis(
                            currentRelativeChild,
                            mainAxis,
                            baseMainSize,
                            availableInnerMainDim,
                            availableInnerWidth);
                        if (!YGFloatIsUndefined(baseMainSize) &&
                            !YGFloatIsUndefined(boundMainSize) &&
                            baseMainSize != boundMainSize)
                        {
                            // By excluding this item's size and flex factor from remaining,
                            // this item's
                            // min/max constraints should also trigger in the second pass
                            // resulting in the
                            // item's size calculation being identical in the first and second
                            // passes.
                            deltaFreeSpace += boundMainSize - childFlexBasis;
                            collectedFlexItemsValues.totalFlexShrinkScaledFactors -=
                                flexShrinkScaledFactor;
                        }
                    }
                }
                else if (
                    !YGFloatIsUndefined(collectedFlexItemsValues.remainingFreeSpace) &&
                    collectedFlexItemsValues.remainingFreeSpace > 0)
                {
                    flexGrowFactor = currentRelativeChild.resolveFlexGrow();

                    // Is this child able to grow?
                    if (!YGFloatIsUndefined(flexGrowFactor) && flexGrowFactor != 0)
                    {
                        baseMainSize = childFlexBasis +
                            collectedFlexItemsValues.remainingFreeSpace /
                            collectedFlexItemsValues.totalFlexGrowFactors * flexGrowFactor;
                        boundMainSize = YGNodeBoundAxis(
                            currentRelativeChild,
                            mainAxis,
                            baseMainSize,
                            availableInnerMainDim,
                            availableInnerWidth);

                        if (!YGFloatIsUndefined(baseMainSize) &&
                            !YGFloatIsUndefined(boundMainSize) &&
                            baseMainSize != boundMainSize)
                        {
                            // By excluding this item's size and flex factor from remaining,
                            // this item's
                            // min/max constraints should also trigger in the second pass
                            // resulting in the
                            // item's size calculation being identical in the first and second
                            // passes.
                            deltaFreeSpace += boundMainSize - childFlexBasis;
                            collectedFlexItemsValues.totalFlexGrowFactors -= flexGrowFactor;
                        }
                    }
                }
            }

            collectedFlexItemsValues.remainingFreeSpace -= deltaFreeSpace;
        }

        // Do two passes over the flex items to figure out how to distribute the
        // remaining space.
        // The first pass finds the items whose min/max constraints trigger,
        // freezes them at those
        // sizes, and excludes those sizes from the remaining space. The second
        // pass sets the size
        // of each flexible item. It distributes the remaining space amongst the
        // items whose min/max
        // constraints didn't trigger in pass 1. For the other items, it sets
        // their sizes by forcing
        // their min/max constraints to trigger again.
        //
        // This two pass approach for resolving min/max constraints deviates from
        // the spec. The
        // spec (https://www.w3.org/TR/YG-flexbox-1/#resolve-flexible-lengths)
        // describes a process
        // that needs to be repeated a variable number of times. The algorithm
        // implemented here
        // won't handle all cases but it was simpler to implement and it mitigates
        // performance
        // concerns because we know exactly how many passes it'll do.
        //
        // At the end of this function the child nodes would have the proper size
        // assigned to them.
        //
        private static void YGResolveFlexibleLength(
            in YGNodeRef node,
            YGCollectFlexItemsRowValues collectedFlexItemsValues,
            in YGFlexDirection mainAxis,
            in YGFlexDirection crossAxis,
            in float mainAxisownerSize,
            in float availableInnerMainDim,
            in float availableInnerCrossDim,
            in float availableInnerWidth,
            in float availableInnerHeight,
            in bool flexBasisOverflows,
            in YGMeasureMode measureModeCrossDim,
            in bool performLayout,
            in YGConfigRef config)
        {
            var originalFreeSpace = collectedFlexItemsValues.remainingFreeSpace;
            // First pass: detect the flex items whose min/max constraints trigger
            YGDistributeFreeSpaceFirstPass(
                collectedFlexItemsValues,
                mainAxis,
                mainAxisownerSize,
                availableInnerMainDim,
                availableInnerWidth);

            // Second pass: resolve the sizes of the flexible items
            var distributedFreeSpace = YGDistributeFreeSpaceSecondPass(
                collectedFlexItemsValues,
                node,
                mainAxis,
                crossAxis,
                mainAxisownerSize,
                availableInnerMainDim,
                availableInnerCrossDim,
                availableInnerWidth,
                availableInnerHeight,
                flexBasisOverflows,
                measureModeCrossDim,
                performLayout,
                config);

            collectedFlexItemsValues.remainingFreeSpace =
                originalFreeSpace - distributedFreeSpace;
        }

        internal static void YGJustifyMainAxis(
            in YGNodeRef node,
            YGCollectFlexItemsRowValues collectedFlexItemsValues,
            in int startOfLineIndex,
            in YGFlexDirection mainAxis,
            in YGFlexDirection crossAxis,
            in YGMeasureMode measureModeMainDim,
            in YGMeasureMode measureModeCrossDim,
            in float mainAxisownerSize,
            in float ownerWidth,
            in float availableInnerMainDim,
            in float availableInnerCrossDim,
            in float availableInnerWidth,
            in bool performLayout)
        {
            var style = node.getStyle();
            var leadingPaddingAndBorderMain = YGUnwrapFloatOptional(node.getLeadingPaddingAndBorder(mainAxis, ownerWidth));
            var trailingPaddingAndBorderMain = YGUnwrapFloatOptional(node.getTrailingPaddingAndBorder(mainAxis, ownerWidth));
            // If we are using "at most" rules in the main axis, make sure that
            // remainingFreeSpace is 0 when min main dimension is not given
            if (measureModeMainDim == YGMeasureMode.AtMost &&
                collectedFlexItemsValues.remainingFreeSpace > 0)
            {
                if (style.minDimensions[dim[(int)mainAxis]].unit != YGUnit.Undefined &&
                    !YGResolveValue(style.minDimensions[dim[(int)mainAxis]], mainAxisownerSize)
                        .isUndefined())
                {
                    // This condition makes sure that if the size of main dimension(after
                    // considering child nodes main dim, leading and trailing padding etc)
                    // falls below min dimension, then the remainingFreeSpace is reassigned
                    // considering the min dimension

                    // `minAvailableMainDim` denotes minimum available space in which child
                    // can be laid out, it will exclude space consumed by padding and border.
                    var minAvailableMainDim = YGUnwrapFloatOptional(
                            YGResolveValue(
                                style.minDimensions[dim[(int)mainAxis]],
                                mainAxisownerSize)) -
                        leadingPaddingAndBorderMain - trailingPaddingAndBorderMain;
                    var occupiedSpaceByChildNodes = availableInnerMainDim - collectedFlexItemsValues.remainingFreeSpace;
                    collectedFlexItemsValues.remainingFreeSpace =
                        YGFloatMax(0, minAvailableMainDim - occupiedSpaceByChildNodes);
                }
                else
                {
                    collectedFlexItemsValues.remainingFreeSpace = 0;
                }
            }

            var numberOfAutoMarginsOnCurrentLine = 0;
            for (var i = startOfLineIndex;
                i < collectedFlexItemsValues.endOfLineIndex;
                i++)
            {
                var child = node.getChild(i);
                if (child.getStyle().positionType == YGPositionType.Relative)
                {
                    if (child.marginLeadingValue(mainAxis).unit == YGUnit.Auto) numberOfAutoMarginsOnCurrentLine++;

                    if (child.marginTrailingValue(mainAxis).unit == YGUnit.Auto) numberOfAutoMarginsOnCurrentLine++;
                }
            }

            // In order to position the elements in the main axis, we have two
            // controls. The space between the beginning and the first element
            // and the space between each two elements.
            float leadingMainDim = 0;
            float betweenMainDim = 0;
            var justifyContent = node.getStyle().justifyContent;

            if (numberOfAutoMarginsOnCurrentLine == 0)
                switch (justifyContent)
                {
                case YGJustify.Center:
                    leadingMainDim = collectedFlexItemsValues.remainingFreeSpace / 2;
                    break;
                case YGJustify.FlexEnd:
                    leadingMainDim = collectedFlexItemsValues.remainingFreeSpace;
                    break;
                case YGJustify.SpaceBetween:
                    if (collectedFlexItemsValues.itemsOnLine > 1)
                        betweenMainDim =
                            YGFloatMax(collectedFlexItemsValues.remainingFreeSpace, 0) /
                            (collectedFlexItemsValues.itemsOnLine - 1);
                    else
                        betweenMainDim = 0;

                    break;
                case YGJustify.SpaceEvenly:
                    // Space is distributed evenly across all elements
                    betweenMainDim = collectedFlexItemsValues.remainingFreeSpace /
                        (collectedFlexItemsValues.itemsOnLine + 1);
                    leadingMainDim = betweenMainDim;
                    break;
                case YGJustify.SpaceAround:
                    // Space on the edges is half of the space between elements
                    betweenMainDim = collectedFlexItemsValues.remainingFreeSpace /
                        collectedFlexItemsValues.itemsOnLine;
                    leadingMainDim = betweenMainDim / 2;
                    break;
                case YGJustify.FlexStart:
                    break;
                }

            collectedFlexItemsValues.mainDim =
                leadingPaddingAndBorderMain + leadingMainDim;
            collectedFlexItemsValues.crossDim = 0;

            float maxAscentForCurrentLine = 0;
            float maxDescentForCurrentLine = 0;
            var isNodeBaselineLayout = YGIsBaselineLayout(node);
            for (var i = startOfLineIndex;
                i < collectedFlexItemsValues.endOfLineIndex;
                i++)
            {
                var child = node.getChild(i);
                var childStyle = child.getStyle();
                var childLayout = child.getLayout();
                if (childStyle.display == YGDisplay.None) continue;

                if (childStyle.positionType == YGPositionType.Absolute &&
                    child.isLeadingPositionDefined(mainAxis))
                {
                    if (performLayout)
                        child.setLayoutPosition(
                            YGUnwrapFloatOptional(
                                child.getLeadingPosition(mainAxis, availableInnerMainDim)) +
                            node.getLeadingBorder(mainAxis)                                +
                            YGUnwrapFloatOptional(
                                child.getLeadingMargin(mainAxis, availableInnerWidth)),
                            pos[(int)mainAxis]);
                }
                else
                {
                    // Now that we placed the element, we need to update the variables.
                    // We need to do that only for relative elements. Absolute elements
                    // do not take part in that phase.
                    if (childStyle.positionType == YGPositionType.Relative)
                    {
                        if (child.marginLeadingValue(mainAxis).unit == YGUnit.Auto)
                            collectedFlexItemsValues.mainDim +=
                                collectedFlexItemsValues.remainingFreeSpace /
                                numberOfAutoMarginsOnCurrentLine;

                        if (performLayout)
                            child.setLayoutPosition(
                                childLayout.position[(int)pos[(int)mainAxis]] +
                                collectedFlexItemsValues.mainDim,
                                pos[(int)mainAxis]);

                        if (child.marginTrailingValue(mainAxis).unit == YGUnit.Auto)
                            collectedFlexItemsValues.mainDim +=
                                collectedFlexItemsValues.remainingFreeSpace /
                                numberOfAutoMarginsOnCurrentLine;

                        var canSkipFlex =
                            !performLayout && measureModeCrossDim == YGMeasureMode.Exactly;
                        if (canSkipFlex)
                        {
                            // If we skipped the flex step, then we can't rely on the
                            // measuredDims because
                            // they weren't computed. This means we can't call
                            // YGNodeDimWithMargin.
                            collectedFlexItemsValues.mainDim += betweenMainDim +
                                YGUnwrapFloatOptional(
                                    child.getMarginForAxis(
                                        mainAxis,
                                        availableInnerWidth)) +
                                YGUnwrapFloatOptional(childLayout.computedFlexBasis);
                            collectedFlexItemsValues.crossDim = availableInnerCrossDim;
                        }
                        else
                        {
                            // The main dimension is the sum of all the elements dimension plus
                            // the spacing.
                            collectedFlexItemsValues.mainDim += betweenMainDim +
                                YGNodeDimWithMargin(child, mainAxis, availableInnerWidth);

                            if (isNodeBaselineLayout)
                            {
                                // If the child is baseline aligned then the cross dimension is
                                // calculated by adding maxAscent and maxDescent from the baseline.
                                var ascent = YGBaseline(child) +
                                    YGUnwrapFloatOptional(
                                        child.getLeadingMargin(
                                            YGFlexDirection.Column,
                                            availableInnerWidth));
                                var descent =
                                    child.getLayout().measuredDimensions[(int)YGDimension.Height] +
                                    YGUnwrapFloatOptional(
                                        child.getMarginForAxis(
                                            YGFlexDirection.Column,
                                            availableInnerWidth)) -
                                    ascent;

                                maxAscentForCurrentLine =
                                    YGFloatMax(maxAscentForCurrentLine, ascent);
                                maxDescentForCurrentLine =
                                    YGFloatMax(maxDescentForCurrentLine, descent);
                            }
                            else
                            {
                                // The cross dimension is the max of the elements dimension since
                                // there can only be one element in that cross dimension in the case
                                // when the items are not baseline aligned
                                collectedFlexItemsValues.crossDim = YGFloatMax(
                                    collectedFlexItemsValues.crossDim,
                                    YGNodeDimWithMargin(child, crossAxis, availableInnerWidth));
                            }
                        }
                    }
                    else if (performLayout)
                    {
                        child.setLayoutPosition(
                            childLayout.position[(int)pos[(int)mainAxis]] +
                            node.getLeadingBorder(mainAxis) + leadingMainDim,
                            pos[(int)mainAxis]);
                    }
                }
            }

            collectedFlexItemsValues.mainDim += trailingPaddingAndBorderMain;

            if (isNodeBaselineLayout)
                collectedFlexItemsValues.crossDim =
                    maxAscentForCurrentLine + maxDescentForCurrentLine;
        }

        //
        // This is the main routine that implements a subset of the flexbox layout
        // algorithm
        // described in the W3C YG documentation: https://www.w3.org/TR/YG3-flexbox/.
        //
        // Limitations of this algorithm, compared to the full standard:
        //  * Display property is always assumed to be 'flex' except for Text nodes,
        //  which
        //    are assumed to be 'inline-flex'.
        //  * The 'zIndex' property (or any form of z ordering) is not supported. Nodes
        //  are
        //    stacked in document order.
        //  * The 'order' property is not supported. The order of flex items is always
        //  defined
        //    by document order.
        //  * The 'visibility' property is always assumed to be 'visible'. Values of
        //  'collapse'
        //    and 'hidden' are not supported.
        //  * There is no support for forced breaks.
        //  * It does not support vertical inline directions (top-to-bottom or
        //  bottom-to-top text).
        //
        // Deviations from standard:
        //  * Section 4.5 of the spec indicates that all flex items have a default
        //  minimum
        //    main size. For text blocks, for example, this is the width of the widest
        //    word.
        //    Calculating the minimum width is expensive, so we forego it and assume a
        //    default
        //    minimum main size of 0.
        //  * Min/Max sizes in the main axis are not honored when resolving flexible
        //  lengths.
        //  * The spec indicates that the default value for 'flexDirection' is 'row',
        //  but
        //    the algorithm below assumes a default of 'column'.
        //
        // Input parameters:
        //    - node: current node to be sized and layed out
        //    - availableWidth & availableHeight: available size to be used for sizing
        //    the node
        //      or YGUndefined if the size is not available; interpretation depends on
        //      layout
        //      flags
        //    - ownerDirection: the inline (text) direction within the owner
        //    (left-to-right or
        //      right-to-left)
        //    - widthMeasureMode: indicates the sizing rules for the width (see below
        //    for explanation)
        //    - heightMeasureMode: indicates the sizing rules for the height (see below
        //    for explanation)
        //    - performLayout: specifies whether the caller is interested in just the
        //    dimensions
        //      of the node or it requires the entire node and its subtree to be layed
        //      out
        //      (with final positions)
        //
        // Details:
        //    This routine is called recursively to lay out subtrees of flexbox
        //    elements. It uses the
        //    information in node.style, which is treated as a read-only input. It is
        //    responsible for
        //    setting the layout.direction and layout.measuredDimensions fields for the
        //    input node as well
        //    as the layout.position and layout.lineIndex fields for its child nodes.
        //    The
        //    layout.measuredDimensions field includes any border or padding for the
        //    node but does
        //    not include margins.
        //
        //    The spec describes four different layout modes: "fill available", "max
        //    content", "min
        //    content",
        //    and "fit content". Of these, we don't use "min content" because we don't
        //    support default
        //    minimum main sizes (see above for details). Each of our measure modes maps
        //    to a layout mode
        //    from the spec (https://www.w3.org/TR/YG3-sizing/#terms):
        //      - YGMeasureMode.Undefined: max content
        //      - YGMeasureMode.Exactly: fill available
        //      - YGMeasureMode.AtMost: fit content
        //
        //    When calling YGNodelayoutImpl and YGLayoutNodeInternal, if the caller
        //    passes an available size of undefined then it must also pass a measure
        //    mode of YGMeasureMode.Undefined in that dimension.
        //
        internal static void YGNodelayoutImpl(
            in YGNodeRef node,
            in float availableWidth,
            in float availableHeight,
            in YGDirection ownerDirection,
            in YGMeasureMode widthMeasureMode,
            in YGMeasureMode heightMeasureMode,
            in float ownerWidth,
            in float ownerHeight,
            in bool performLayout,
            in YGConfigRef config)
        {
            YGAssertWithNode(
                node,
                YGFloatIsUndefined(availableWidth)
                    ? widthMeasureMode == YGMeasureMode.Undefined
                    : true,
                "availableWidth is indefinite so widthMeasureMode must be YGMeasureMode.Undefined");
            YGAssertWithNode(
                node,
                YGFloatIsUndefined(availableHeight)
                    ? heightMeasureMode == YGMeasureMode.Undefined
                    : true,
                "availableHeight is indefinite so heightMeasureMode must be YGMeasureMode.Undefined");

            // Set the resolved resolution in the node's layout.
            var direction = node.resolveDirection(ownerDirection);
            node.setLayoutDirection(direction);

            var flexRowDirection = YGResolveFlexDirection(YGFlexDirection.Row, direction);
            var flexColumnDirection = YGResolveFlexDirection(YGFlexDirection.Column, direction);

            node.setLayoutMargin(
                YGUnwrapFloatOptional(
                    node.getLeadingMargin(flexRowDirection, ownerWidth)),
                YGEdge.Start);
            node.setLayoutMargin(
                YGUnwrapFloatOptional(
                    node.getTrailingMargin(flexRowDirection, ownerWidth)),
                YGEdge.End);
            node.setLayoutMargin(
                YGUnwrapFloatOptional(
                    node.getLeadingMargin(flexColumnDirection, ownerWidth)),
                YGEdge.Top);
            node.setLayoutMargin(
                YGUnwrapFloatOptional(
                    node.getTrailingMargin(flexColumnDirection, ownerWidth)),
                YGEdge.Bottom);

            node.setLayoutBorder(node.getLeadingBorder(flexRowDirection), YGEdge.Start);
            node.setLayoutBorder(node.getTrailingBorder(flexRowDirection), YGEdge.End);
            node.setLayoutBorder(node.getLeadingBorder(flexColumnDirection), YGEdge.Top);
            node.setLayoutBorder(
                node.getTrailingBorder(flexColumnDirection),
                YGEdge.Bottom);

            node.setLayoutPadding(
                YGUnwrapFloatOptional(
                    node.getLeadingPadding(flexRowDirection, ownerWidth)),
                YGEdge.Start);
            node.setLayoutPadding(
                YGUnwrapFloatOptional(
                    node.getTrailingPadding(flexRowDirection, ownerWidth)),
                YGEdge.End);
            node.setLayoutPadding(
                YGUnwrapFloatOptional(
                    node.getLeadingPadding(flexColumnDirection, ownerWidth)),
                YGEdge.Top);
            node.setLayoutPadding(
                YGUnwrapFloatOptional(
                    node.getTrailingPadding(flexColumnDirection, ownerWidth)),
                YGEdge.Bottom);

            if (node.getMeasure() != null)
            {
                YGNodeWithMeasureFuncSetMeasuredDimensions(
                    node,
                    availableWidth,
                    availableHeight,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight);
                return;
            }

            var childCount = YGNodeGetChildCount(node);
            if (childCount == 0)
            {
                YGNodeEmptyContainerSetMeasuredDimensions(
                    node,
                    availableWidth,
                    availableHeight,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight);
                return;
            }

            // If we're not being asked to perform a full layout we can skip the algorithm
            // if we already know the size
            if (!performLayout &&
                YGNodeFixedSizeSetMeasuredDimensions(
                    node,
                    availableWidth,
                    availableHeight,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight))
                return;

            // At this point we know we're going to perform work. Ensure that each child
            // has a mutable copy.
            node.cloneChildrenIfNeeded();
            // Reset layout flags, as they could have changed.
            node.setLayoutHadOverflow(false);

            // STEP 1: CALCULATE VALUES FOR REMAINDER OF ALGORITHM
            var mainAxis = YGResolveFlexDirection(node.getStyle().flexDirection, direction);
            var crossAxis = YGFlexDirectionCross(mainAxis, direction);
            var isMainAxisRow = YGFlexDirectionIsRow(mainAxis);
            var isNodeFlexWrap = node.getStyle().flexWrap != YGWrap.NoWrap;

            var mainAxisownerSize = isMainAxisRow ? ownerWidth : ownerHeight;
            var crossAxisownerSize = isMainAxisRow ? ownerHeight : ownerWidth;

            var leadingPaddingAndBorderCross = YGUnwrapFloatOptional(node.getLeadingPaddingAndBorder(crossAxis, ownerWidth));
            var paddingAndBorderAxisMain = YGNodePaddingAndBorderForAxis(node, mainAxis, ownerWidth);
            var paddingAndBorderAxisCross = YGNodePaddingAndBorderForAxis(node, crossAxis, ownerWidth);

            var measureModeMainDim = isMainAxisRow ? widthMeasureMode : heightMeasureMode;
            var measureModeCrossDim = isMainAxisRow ? heightMeasureMode : widthMeasureMode;

            var paddingAndBorderAxisRow = isMainAxisRow ? paddingAndBorderAxisMain : paddingAndBorderAxisCross;
            var paddingAndBorderAxisColumn = isMainAxisRow ? paddingAndBorderAxisCross : paddingAndBorderAxisMain;

            var marginAxisRow = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Row, ownerWidth));
            var marginAxisColumn = YGUnwrapFloatOptional(node.getMarginForAxis(YGFlexDirection.Column, ownerWidth));

            var minInnerWidth = YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.getStyle().minDimensions[YGDimension.Width],
                        ownerWidth)) -
                paddingAndBorderAxisRow;
            var maxInnerWidth =
                YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.getStyle().maxDimensions[YGDimension.Width],
                        ownerWidth)) -
                paddingAndBorderAxisRow;
            var minInnerHeight =
                YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.getStyle().minDimensions[YGDimension.Height],
                        ownerHeight)) -
                paddingAndBorderAxisColumn;
            var maxInnerHeight =
                YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.getStyle().maxDimensions[YGDimension.Height],
                        ownerHeight)) -
                paddingAndBorderAxisColumn;

            var minInnerMainDim = isMainAxisRow ? minInnerWidth : minInnerHeight;
            var maxInnerMainDim = isMainAxisRow ? maxInnerWidth : maxInnerHeight;

            // STEP 2: DETERMINE AVAILABLE SIZE IN MAIN AND CROSS DIRECTIONS

            var availableInnerWidth = YGNodeCalculateAvailableInnerDim(
                node,
                YGFlexDirection.Row,
                availableWidth,
                ownerWidth);
            var availableInnerHeight = YGNodeCalculateAvailableInnerDim(
                node,
                YGFlexDirection.Column,
                availableHeight,
                ownerHeight);

            var availableInnerMainDim = isMainAxisRow ? availableInnerWidth : availableInnerHeight;
            var availableInnerCrossDim = isMainAxisRow ? availableInnerHeight : availableInnerWidth;

            float totalOuterFlexBasis = 0;

            // STEP 3: DETERMINE FLEX BASIS FOR EACH ITEM

            YGNodeComputeFlexBasisForChildren(
                node,
                availableInnerWidth,
                availableInnerHeight,
                widthMeasureMode,
                heightMeasureMode,
                direction,
                mainAxis,
                config,
                performLayout,
                totalOuterFlexBasis);

            var flexBasisOverflows = measureModeMainDim == YGMeasureMode.Undefined
                ? false
                : totalOuterFlexBasis > availableInnerMainDim;
            if (isNodeFlexWrap && flexBasisOverflows &&
                measureModeMainDim == YGMeasureMode.AtMost)
                measureModeMainDim = YGMeasureMode.Exactly;
            // STEP 4: COLLECT FLEX ITEMS INTO FLEX LINES

            // Indexes of children that represent the first and last items in the line.
            var startOfLineIndex = 0;
            var endOfLineIndex = 0;

            // Number of lines.
            var lineCount = 0;

            // Accumulated cross dimensions of all lines so far.
            float totalLineCrossDim = 0;

            // Max main dimension of all the lines.
            float maxLineMainDim = 0;
            YGCollectFlexItemsRowValues collectedFlexItemsValues;
            for (;
                endOfLineIndex < childCount;
                lineCount++, startOfLineIndex = endOfLineIndex)
            {
                collectedFlexItemsValues = YGCalculateCollectFlexItemsRowValues(
                    node,
                    ownerDirection,
                    mainAxisownerSize,
                    availableInnerWidth,
                    availableInnerMainDim,
                    startOfLineIndex,
                    lineCount);
                endOfLineIndex = collectedFlexItemsValues.endOfLineIndex;

                // If we don't need to measure the cross axis, we can skip the entire flex
                // step.
                var canSkipFlex = !performLayout && measureModeCrossDim == YGMeasureMode.Exactly;

                // STEP 5: RESOLVING FLEXIBLE LENGTHS ON MAIN AXIS
                // Calculate the remaining available space that needs to be allocated.
                // If the main dimension size isn't known, it is computed based on
                // the line length, so there's no more space left to distribute.

                var sizeBasedOnContent = false;
                // If we don't measure with exact main dimension we want to ensure we don't
                // violate min and max
                if (measureModeMainDim != YGMeasureMode.Exactly)
                {
                    if (!YGFloatIsUndefined(minInnerMainDim) &&
                        collectedFlexItemsValues.sizeConsumedOnCurrentLine <
                        minInnerMainDim)
                    {
                        availableInnerMainDim = minInnerMainDim;
                    }
                    else if (
                        !YGFloatIsUndefined(maxInnerMainDim) &&
                        collectedFlexItemsValues.sizeConsumedOnCurrentLine >
                        maxInnerMainDim)
                    {
                        availableInnerMainDim = maxInnerMainDim;
                    }
                    else
                    {
                        if (!node.getConfig().useLegacyStretchBehaviour &&
                            (YGFloatIsUndefined(
                                    collectedFlexItemsValues.totalFlexGrowFactors) &&
                                collectedFlexItemsValues.totalFlexGrowFactors == 0 ||
                                YGFloatIsUndefined(node.resolveFlexGrow()) &&
                                node.resolveFlexGrow() == 0))
                            availableInnerMainDim =
                                collectedFlexItemsValues.sizeConsumedOnCurrentLine;

                        if (node.getConfig().useLegacyStretchBehaviour) node.setLayoutDidUseLegacyFlag(true);

                        sizeBasedOnContent = !node.getConfig().useLegacyStretchBehaviour;
                    }
                }

                if (!sizeBasedOnContent && !YGFloatIsUndefined(availableInnerMainDim))
                    collectedFlexItemsValues.remainingFreeSpace = availableInnerMainDim -
                        collectedFlexItemsValues.sizeConsumedOnCurrentLine;
                else if (collectedFlexItemsValues.sizeConsumedOnCurrentLine < 0)
                    collectedFlexItemsValues.remainingFreeSpace =
                        -collectedFlexItemsValues.sizeConsumedOnCurrentLine;

                if (!canSkipFlex)
                    YGResolveFlexibleLength(
                        node,
                        collectedFlexItemsValues,
                        mainAxis,
                        crossAxis,
                        mainAxisownerSize,
                        availableInnerMainDim,
                        availableInnerCrossDim,
                        availableInnerWidth,
                        availableInnerHeight,
                        flexBasisOverflows,
                        measureModeCrossDim,
                        performLayout,
                        config);

                node.setLayoutHadOverflow(
                    node.getLayout().hadOverflow |
                    (collectedFlexItemsValues.remainingFreeSpace < 0));

                // STEP 6: MAIN-AXIS JUSTIFICATION & CROSS-AXIS SIZE DETERMINATION

                // At this point, all the children have their dimensions set in the main
                // axis.
                // Their dimensions are also set in the cross axis with the exception of
                // items
                // that are aligned "stretch". We need to compute these stretch values and
                // set the final positions.

                YGJustifyMainAxis(
                    node,
                    collectedFlexItemsValues,
                    startOfLineIndex,
                    mainAxis,
                    crossAxis,
                    measureModeMainDim,
                    measureModeCrossDim,
                    mainAxisownerSize,
                    ownerWidth,
                    availableInnerMainDim,
                    availableInnerCrossDim,
                    availableInnerWidth,
                    performLayout);

                var containerCrossAxis = availableInnerCrossDim;
                if (measureModeCrossDim == YGMeasureMode.Undefined ||
                    measureModeCrossDim == YGMeasureMode.AtMost)
                    containerCrossAxis =
                        YGNodeBoundAxis(
                            node,
                            crossAxis,
                            collectedFlexItemsValues.crossDim + paddingAndBorderAxisCross,
                            crossAxisownerSize,
                            ownerWidth) -
                        paddingAndBorderAxisCross;

                // If there's no flex wrap, the cross dimension is defined by the container.
                if (!isNodeFlexWrap && measureModeCrossDim == YGMeasureMode.Exactly) collectedFlexItemsValues.crossDim = availableInnerCrossDim;

                // Clamp to the min/max size specified on the container.
                collectedFlexItemsValues.crossDim =
                    YGNodeBoundAxis(
                        node,
                        crossAxis,
                        collectedFlexItemsValues.crossDim + paddingAndBorderAxisCross,
                        crossAxisownerSize,
                        ownerWidth) -
                    paddingAndBorderAxisCross;

                // STEP 7: CROSS-AXIS ALIGNMENT
                // We can skip child alignment if we're just measuring the container.
                if (performLayout)
                    for (var i = startOfLineIndex; i < endOfLineIndex; i++)
                    {
                        var child = node.getChild(i);
                        if (child.getStyle().display == YGDisplay.None) continue;

                        if (child.getStyle().positionType == YGPositionType.Absolute)
                        {
                            // If the child is absolutely positioned and has a
                            // top/left/bottom/right set, override
                            // all the previously computed positions to set it correctly.
                            var isChildLeadingPosDefined =
                                child.isLeadingPositionDefined(crossAxis);
                            if (isChildLeadingPosDefined)
                                child.setLayoutPosition(
                                    YGUnwrapFloatOptional(
                                        child.getLeadingPosition(
                                            crossAxis,
                                            availableInnerCrossDim)) +
                                    node.getLeadingBorder(crossAxis) +
                                    YGUnwrapFloatOptional(
                                        child.getLeadingMargin(
                                            crossAxis,
                                            availableInnerWidth)),
                                    pos[(int)crossAxis]);

                            // If leading position is not defined or calculations result in Nan,
                            // default to border + margin
                            if (!isChildLeadingPosDefined ||
                                YGFloatIsUndefined(child.getLayout().position[(int)pos[(int)crossAxis]]))
                                child.setLayoutPosition(
                                    node.getLeadingBorder(crossAxis) +
                                    YGUnwrapFloatOptional(
                                        child.getLeadingMargin(
                                            crossAxis,
                                            availableInnerWidth)),
                                    pos[(int)crossAxis]);
                        }
                        else
                        {
                            var leadingCrossDim = leadingPaddingAndBorderCross;

                            // For a relative children, we're either using alignItems (owner) or
                            // alignSelf (child) in order to determine the position in the cross
                            // axis
                            var alignItem = YGNodeAlignItem(node, child);

                            // If the child uses align stretch, we need to lay it out one more
                            // time, this time
                            // forcing the cross-axis size to be the computed cross size for the
                            // current line.
                            if (alignItem                                 == YGAlign.Stretch &&
                                child.marginLeadingValue(crossAxis).unit  != YGUnit.Auto     &&
                                child.marginTrailingValue(crossAxis).unit != YGUnit.Auto)
                            {
                                // If the child defines a definite size for its cross axis, there's
                                // no need to stretch.
                                if (!YGNodeIsStyleDimDefined(
                                    child,
                                    crossAxis,
                                    availableInnerCrossDim))
                                {
                                    var childMainSize = child.getLayout().measuredDimensions[(int)dim[(int)mainAxis]];
                                    var childCrossSize =
                                        !child.getStyle().aspectRatio.isUndefined()
                                            ? YGUnwrapFloatOptional(
                                                child.getMarginForAxis(
                                                    crossAxis,
                                                    availableInnerWidth)) +
                                            (isMainAxisRow
                                                ? childMainSize /
                                                child.getStyle().aspectRatio.getValue()
                                                : childMainSize *
                                                child.getStyle().aspectRatio.getValue())
                                            : collectedFlexItemsValues.crossDim;

                                    childMainSize += YGUnwrapFloatOptional(
                                        child.getMarginForAxis(mainAxis, availableInnerWidth));

                                    var childMainMeasureMode  = YGMeasureMode.Exactly;
                                    var childCrossMeasureMode = YGMeasureMode.Exactly;
                                    YGConstrainMaxSizeForMode(
                                        child,
                                        mainAxis,
                                        availableInnerMainDim,
                                        availableInnerWidth,
                                        ref childMainMeasureMode,
                                        ref childMainSize);
                                    YGConstrainMaxSizeForMode(
                                        child,
                                        crossAxis,
                                        availableInnerCrossDim,
                                        availableInnerWidth,
                                        ref childCrossMeasureMode,
                                        ref childCrossSize);

                                    var childWidth  = isMainAxisRow ? childMainSize : childCrossSize;
                                    var childHeight = !isMainAxisRow ? childMainSize : childCrossSize;

                                    var childWidthMeasureMode =
                                        YGFloatIsUndefined(childWidth) ? YGMeasureMode.Undefined : YGMeasureMode.Exactly;
                                    var childHeightMeasureMode =
                                        YGFloatIsUndefined(childHeight) ? YGMeasureMode.Undefined : YGMeasureMode.Exactly;

                                    YGLayoutNodeInternal(
                                        child,
                                        childWidth,
                                        childHeight,
                                        direction,
                                        childWidthMeasureMode,
                                        childHeightMeasureMode,
                                        availableInnerWidth,
                                        availableInnerHeight,
                                        true,
                                        "stretch",
                                        config);
                                }
                            }
                            else
                            {
                                var remainingCrossDim = containerCrossAxis - YGNodeDimWithMargin(child, crossAxis, availableInnerWidth);

                                if (child.marginLeadingValue(crossAxis).unit  == YGUnit.Auto &&
                                    child.marginTrailingValue(crossAxis).unit == YGUnit.Auto)
                                {
                                    leadingCrossDim += YGFloatMax(0.0f, remainingCrossDim / 2);
                                }
                                else if (
                                    child.marginTrailingValue(crossAxis).unit == YGUnit.Auto)
                                {
                                    // No-Op
                                }
                                else if (
                                    child.marginLeadingValue(crossAxis).unit == YGUnit.Auto)
                                {
                                    leadingCrossDim += YGFloatMax(0.0f, remainingCrossDim);
                                }
                                else if (alignItem == YGAlign.FlexStart)
                                {
                                    // No-Op
                                }
                                else if (alignItem == YGAlign.Center)
                                {
                                    leadingCrossDim += remainingCrossDim / 2;
                                }
                                else
                                {
                                    leadingCrossDim += remainingCrossDim;
                                }
                            }

                            // And we apply the position
                            child.setLayoutPosition(
                                child.getLayout().position[(int)pos[(int)crossAxis]] + totalLineCrossDim +
                                leadingCrossDim,
                                pos[(int)crossAxis]);
                        }
                    }

                totalLineCrossDim += collectedFlexItemsValues.crossDim;
                maxLineMainDim =
                    YGFloatMax(maxLineMainDim, collectedFlexItemsValues.mainDim);
            }

            // STEP 8: MULTI-LINE CONTENT ALIGNMENT
            // currentLead stores the size of the cross dim
            if (performLayout && (lineCount > 1 || YGIsBaselineLayout(node)))
            {
                float crossDimLead = 0;
                var currentLead = leadingPaddingAndBorderCross;
                if (!YGFloatIsUndefined(availableInnerCrossDim))
                {
                    var remainingAlignContentDim = availableInnerCrossDim - totalLineCrossDim;
                    switch (node.getStyle().alignContent)
                    {
                        case YGAlign.FlexEnd:
                            currentLead += remainingAlignContentDim;
                            break;
                        case YGAlign.Center:
                            currentLead += remainingAlignContentDim / 2;
                            break;
                        case YGAlign.Stretch:
                            if (availableInnerCrossDim > totalLineCrossDim) crossDimLead = remainingAlignContentDim / lineCount;

                            break;
                        case YGAlign.SpaceAround:
                            if (availableInnerCrossDim > totalLineCrossDim)
                            {
                                currentLead += remainingAlignContentDim / (2 * lineCount);
                                if (lineCount > 1) crossDimLead = remainingAlignContentDim / lineCount;
                            }
                            else
                            {
                                currentLead += remainingAlignContentDim / 2;
                            }

                            break;
                        case YGAlign.SpaceBetween:
                            if (availableInnerCrossDim > totalLineCrossDim && lineCount > 1) crossDimLead = remainingAlignContentDim / (lineCount - 1);

                            break;
                        case YGAlign.Auto:
                        case YGAlign.FlexStart:
                        case YGAlign.Baseline:
                            break;
                    }
                }

                var endIndex = 0;
                for (var i = 0; i < lineCount; i++)
                {
                    var startIndex = endIndex;
                    int ii;

                    // compute the line's height and find the endIndex
                    float lineHeight = 0;
                    float maxAscentForCurrentLine = 0;
                    float maxDescentForCurrentLine = 0;
                    for (ii = startIndex; ii < childCount; ii++)
                    {
                        var child = node.getChild(ii);
                        if (child.getStyle().display == YGDisplay.None) continue;

                        if (child.getStyle().positionType == YGPositionType.Relative)
                        {
                            if (child.getLineIndex() != i) break;

                            if (YGNodeIsLayoutDimDefined(child, crossAxis))
                                lineHeight = YGFloatMax(
                                    lineHeight,
                                    child.getLayout().measuredDimensions[(int)dim[(int)crossAxis]] +
                                    YGUnwrapFloatOptional(
                                        child.getMarginForAxis(
                                            crossAxis,
                                            availableInnerWidth)));

                            if (YGNodeAlignItem(node, child) == YGAlign.Baseline)
                            {
                                var ascent = YGBaseline(child) +
                                    YGUnwrapFloatOptional(
                                        child.getLeadingMargin(
                                            YGFlexDirection.Column,
                                            availableInnerWidth));
                                var descent =
                                    child.getLayout().measuredDimensions[(int)YGDimension.Height] +
                                    YGUnwrapFloatOptional(
                                        child.getMarginForAxis(
                                            YGFlexDirection.Column,
                                            availableInnerWidth)) -
                                    ascent;
                                maxAscentForCurrentLine =
                                    YGFloatMax(maxAscentForCurrentLine, ascent);
                                maxDescentForCurrentLine =
                                    YGFloatMax(maxDescentForCurrentLine, descent);
                                lineHeight = YGFloatMax(
                                    lineHeight,
                                    maxAscentForCurrentLine + maxDescentForCurrentLine);
                            }
                        }
                    }

                    endIndex = ii;
                    lineHeight += crossDimLead;

                    if (performLayout)
                        for (ii = startIndex; ii < endIndex; ii++)
                        {
                            var child = node.getChild(ii);
                            if (child.getStyle().display == YGDisplay.None) continue;

                            if (child.getStyle().positionType == YGPositionType.Relative)
                                switch (YGNodeAlignItem(node, child))
                                {
                                case YGAlign.FlexStart:
                                {
                                    child.setLayoutPosition(
                                        currentLead +
                                        YGUnwrapFloatOptional(
                                            child.getLeadingMargin(
                                                crossAxis,
                                                availableInnerWidth)),
                                        pos[(int)crossAxis]);
                                    break;
                                }
                                case YGAlign.FlexEnd:
                                {
                                    child.setLayoutPosition(
                                        currentLead + lineHeight -
                                        YGUnwrapFloatOptional(
                                            child.getTrailingMargin(
                                                crossAxis,
                                                availableInnerWidth)) -
                                        child.getLayout().measuredDimensions[(int)dim[(int)crossAxis]],
                                        pos[(int)crossAxis]);
                                    break;
                                }
                                case YGAlign.Center:
                                {
                                    var childHeight =
                                        child.getLayout().measuredDimensions[(int)dim[(int)crossAxis]];

                                    child.setLayoutPosition(
                                        currentLead + (lineHeight - childHeight) / 2,
                                        pos[(int)crossAxis]);
                                    break;
                                }
                                case YGAlign.Stretch:
                                {
                                    child.setLayoutPosition(
                                        currentLead +
                                        YGUnwrapFloatOptional(
                                            child.getLeadingMargin(
                                                crossAxis,
                                                availableInnerWidth)),
                                        pos[(int)crossAxis]);

                                    // Remeasure child with the line height as it as been only
                                    // measured with the owners height yet.
                                    if (!YGNodeIsStyleDimDefined(
                                        child,
                                        crossAxis,
                                        availableInnerCrossDim))
                                    {
                                        var childWidth = isMainAxisRow
                                            ? child.getLayout().measuredDimensions[(int)YGDimension.Width] +
                                            YGUnwrapFloatOptional(
                                                child.getMarginForAxis(
                                                    mainAxis,
                                                    availableInnerWidth))
                                            : lineHeight;

                                        var childHeight = !isMainAxisRow
                                            ? child.getLayout().measuredDimensions[(int)YGDimension.Height] +
                                            YGUnwrapFloatOptional(
                                                child.getMarginForAxis(
                                                    crossAxis,
                                                    availableInnerWidth))
                                            : lineHeight;

                                        if (!(YGFloatsEqual(
                                                childWidth,
                                                child.getLayout().measuredDimensions[(int)YGDimension.Width]) &&
                                            YGFloatsEqual(
                                                childHeight,
                                                child.getLayout().measuredDimensions[(int)YGDimension.Height])))
                                            YGLayoutNodeInternal(
                                                child,
                                                childWidth,
                                                childHeight,
                                                direction,
                                                YGMeasureMode.Exactly,
                                                YGMeasureMode.Exactly,
                                                availableInnerWidth,
                                                availableInnerHeight,
                                                true,
                                                "multiline-stretch",
                                                config);
                                    }

                                    break;
                                }
                                case YGAlign.Baseline:
                                {
                                    child.setLayoutPosition(
                                        currentLead + maxAscentForCurrentLine - YGBaseline(child) +
                                        YGUnwrapFloatOptional(
                                            child.getLeadingPosition(
                                                YGFlexDirection.Column,
                                                availableInnerCrossDim)),
                                        YGEdge.Top);

                                    break;
                                }
                                case YGAlign.Auto:
                                case YGAlign.SpaceBetween:
                                case YGAlign.SpaceAround:
                                    break;
                                }
                        }

                    currentLead += lineHeight;
                }
            }

            // STEP 9: COMPUTING FINAL DIMENSIONS

            node.setLayoutMeasuredDimension(
                YGNodeBoundAxis(
                    node,
                    YGFlexDirection.Row,
                    availableWidth - marginAxisRow,
                    ownerWidth,
                    ownerWidth),
                YGDimension.Width);

            node.setLayoutMeasuredDimension(
                YGNodeBoundAxis(
                    node,
                    YGFlexDirection.Column,
                    availableHeight - marginAxisColumn,
                    ownerHeight,
                    ownerWidth),
                YGDimension.Height);

            // If the user didn't specify a width or height for the node, set the
            // dimensions based on the children.
            if (measureModeMainDim == YGMeasureMode.Undefined ||
                node.getStyle().overflow != YGOverflow.Scroll &&
                measureModeMainDim       == YGMeasureMode.AtMost)
                node.setLayoutMeasuredDimension(
                    YGNodeBoundAxis(
                        node,
                        mainAxis,
                        maxLineMainDim,
                        mainAxisownerSize,
                        ownerWidth),
                    dim[(int)mainAxis]);
            else if (
                measureModeMainDim == YGMeasureMode.AtMost &&
                node.getStyle().overflow == YGOverflow.Scroll)
                node.setLayoutMeasuredDimension(
                    YGFloatMax(
                        YGFloatMin(
                            availableInnerMainDim + paddingAndBorderAxisMain,
                            YGUnwrapFloatOptional(
                                YGNodeBoundAxisWithinMinAndMax(
                                    node,
                                    mainAxis,
                                    maxLineMainDim,
                                    mainAxisownerSize))),
                        paddingAndBorderAxisMain),
                    dim[(int)mainAxis]);

            if (measureModeCrossDim == YGMeasureMode.Undefined ||
                node.getStyle().overflow != YGOverflow.Scroll &&
                measureModeCrossDim      == YGMeasureMode.AtMost)
                node.setLayoutMeasuredDimension(
                    YGNodeBoundAxis(
                        node,
                        crossAxis,
                        totalLineCrossDim + paddingAndBorderAxisCross,
                        crossAxisownerSize,
                        ownerWidth),
                    dim[(int)crossAxis]);
            else if (
                measureModeCrossDim == YGMeasureMode.AtMost &&
                node.getStyle().overflow == YGOverflow.Scroll)
                node.setLayoutMeasuredDimension(
                    YGFloatMax(
                        YGFloatMin(
                            availableInnerCrossDim + paddingAndBorderAxisCross,
                            YGUnwrapFloatOptional(
                                YGNodeBoundAxisWithinMinAndMax(
                                    node,
                                    crossAxis,
                                    totalLineCrossDim + paddingAndBorderAxisCross,
                                    crossAxisownerSize))),
                        paddingAndBorderAxisCross),
                    dim[(int)crossAxis]);

            // As we only wrapped in normal direction yet, we need to reverse the
            // positions on wrap-reverse.
            if (performLayout && node.getStyle().flexWrap == YGWrap.WrapReverse)
                for (var i = 0; i < childCount; i++)
                {
                    var child = YGNodeGetChild(node, i);
                    if (child.getStyle().positionType == YGPositionType.Relative)
                        child.setLayoutPosition(
                            node.getLayout().measuredDimensions[(int)dim[(int)crossAxis]] -
                            child.getLayout().position[(int)pos[(int)crossAxis]]          -
                            child.getLayout().measuredDimensions[(int)dim[(int)crossAxis]],
                            pos[(int)crossAxis]);
                }

            if (performLayout)
            {
                // STEP 10: SIZING AND POSITIONING ABSOLUTE CHILDREN
                foreach (var child in node.getChildren())
                {
                    if (child.getStyle().positionType != YGPositionType.Absolute) continue;

                    YGNodeAbsoluteLayoutChild(
                        node,
                        child,
                        availableInnerWidth,
                        isMainAxisRow ? measureModeMainDim : measureModeCrossDim,
                        availableInnerHeight,
                        direction,
                        config);
                }

                // STEP 11: SETTING TRAILING POSITIONS FOR CHILDREN
                var needsMainTrailingPos = mainAxis == YGFlexDirection.RowReverse ||
                    mainAxis == YGFlexDirection.ColumnReverse;
                var needsCrossTrailingPos = crossAxis == YGFlexDirection.RowReverse ||
                    crossAxis == YGFlexDirection.ColumnReverse;

                // Set trailing position if necessary.
                if (needsMainTrailingPos || needsCrossTrailingPos)
                    for (var i = 0; i < childCount; i++)
                    {
                        var child = node.getChild(i);
                        if (child.getStyle().display == YGDisplay.None) continue;

                        if (needsMainTrailingPos) YGNodeSetChildTrailingPosition(node, child, mainAxis);

                        if (needsCrossTrailingPos) YGNodeSetChildTrailingPosition(node, child, crossAxis);
                    }
            }
        }

        internal static int gDepth = 0;
        internal static bool gPrintChanges = false;
        internal static bool gPrintSkips = false;

        internal static string spacer = "                                                            ";

        internal static string YGSpacer(in int level)
        {
            var spacerLen = spacer.Length;
            if (level > spacerLen)
                return spacer;
            return spacer.Substring(spacerLen - level);
        }

        internal static string YGMeasureModeName(
            in YGMeasureMode mode,
            in bool performLayout)
        {
            string[] kMeasureModeNames = { "UNDEFINED", "EXACTLY", "AT_MOST" };
            string[] kLayoutModeNames = { "LAY_UNDEFINED", "LAY_EXACTLY", "LAY_AT_", "MOST" };

            if ((int)mode >= YGMeasureModeCount) return "";

            return performLayout ? kLayoutModeNames[(int)mode] : kMeasureModeNames[(int)mode];
        }

        // inline
        internal static bool YGMeasureModeSizeIsExactAndMatchesOldMeasuredSize(
            YGMeasureMode sizeMode,
            float size,
            float lastComputedSize)
        {
            return sizeMode == YGMeasureMode.Exactly &&
                YGFloatsEqual(size, lastComputedSize);
        }

        // inline
        internal static bool YGMeasureModeOldSizeIsUnspecifiedAndStillFits(
            YGMeasureMode sizeMode,
            float size,
            YGMeasureMode lastSizeMode,
            float lastComputedSize)
        {
            return sizeMode == YGMeasureMode.AtMost &&
                lastSizeMode == YGMeasureMode.Undefined &&
                (size >= lastComputedSize || YGFloatsEqual(size, lastComputedSize));
        }

        // inline
        internal static bool YGMeasureModeNewMeasureSizeIsStricterAndStillValid(
            YGMeasureMode sizeMode,
            float size,
            YGMeasureMode lastSizeMode,
            float lastSize,
            float lastComputedSize)
        {
            return lastSizeMode == YGMeasureMode.AtMost &&
                sizeMode == YGMeasureMode.AtMost && !YGFloatIsUndefined(lastSize) &&
                !YGFloatIsUndefined(size) && !YGFloatIsUndefined(lastComputedSize) &&
                lastSize > size &&
                (lastComputedSize <= size || YGFloatsEqual(size, lastComputedSize));
        }

        public static float YGRoundValueToPixelGrid(
            in float value,
            in float pointScaleFactor,
            in bool forceCeil,
            in bool forceFloor)
        {
            var scaledValue = value * pointScaleFactor;
            var fractial = scaledValue % 1.0f;
            if (YGFloatsEqual(fractial, 0))
                scaledValue = scaledValue - fractial;
            else if (YGFloatsEqual(fractial, 1.0f))
                scaledValue = scaledValue - fractial + 1.0f;
            else if (forceCeil)
                scaledValue = scaledValue - fractial + 1.0f;
            else if (forceFloor)
                scaledValue = scaledValue - fractial;
            else
                scaledValue = scaledValue - fractial +
                    (!YGFloatIsUndefined(fractial) &&
                        (fractial > 0.5f || YGFloatsEqual(fractial, 0.5f))
                            ? 1.0f
                            : 0.0f);

            return YGFloatIsUndefined(scaledValue) ||
                YGFloatIsUndefined(pointScaleFactor)
                ? YGUndefined
                : scaledValue / pointScaleFactor;
        }

        public static bool YGNodeCanUseCachedMeasurement(
            in YGMeasureMode widthMode,
            in float width,
            in YGMeasureMode heightMode,
            in float height,
            in YGMeasureMode lastWidthMode,
            in float lastWidth,
            in YGMeasureMode lastHeightMode,
            in float lastHeight,
            in float lastComputedWidth,
            in float lastComputedHeight,
            in float marginRow,
            in float marginColumn,
            in YGConfigRef config)
        {
            if (!YGFloatIsUndefined(lastComputedHeight) && lastComputedHeight < 0 ||
                !YGFloatIsUndefined(lastComputedWidth) && lastComputedWidth < 0)
                return false;

            var useRoundedComparison =
                config != null && config.pointScaleFactor != 0;
            var effectiveWidth = useRoundedComparison
                ? YGRoundValueToPixelGrid(width, config.pointScaleFactor, false, false)
                : width;
            var effectiveHeight = useRoundedComparison
                ? YGRoundValueToPixelGrid(height, config.pointScaleFactor, false, false)
                : height;
            var effectiveLastWidth = useRoundedComparison
                ? YGRoundValueToPixelGrid(
                    lastWidth,
                    config.pointScaleFactor,
                    false,
                    false)
                : lastWidth;
            var effectiveLastHeight = useRoundedComparison
                ? YGRoundValueToPixelGrid(
                    lastHeight,
                    config.pointScaleFactor,
                    false,
                    false)
                : lastHeight;

            var hasSameWidthSpec = lastWidthMode == widthMode &&
                YGFloatsEqual(effectiveLastWidth, effectiveWidth);
            var hasSameHeightSpec = lastHeightMode == heightMode &&
                YGFloatsEqual(effectiveLastHeight, effectiveHeight);

            var widthIsCompatible =
                hasSameWidthSpec ||
                YGMeasureModeSizeIsExactAndMatchesOldMeasuredSize(
                    widthMode,
                    width - marginRow,
                    lastComputedWidth) ||
                YGMeasureModeOldSizeIsUnspecifiedAndStillFits(
                    widthMode,
                    width - marginRow,
                    lastWidthMode,
                    lastComputedWidth) ||
                YGMeasureModeNewMeasureSizeIsStricterAndStillValid(
                    widthMode,
                    width - marginRow,
                    lastWidthMode,
                    lastWidth,
                    lastComputedWidth);

            var heightIsCompatible =
                hasSameHeightSpec ||
                YGMeasureModeSizeIsExactAndMatchesOldMeasuredSize(
                    heightMode,
                    height - marginColumn,
                    lastComputedHeight) ||
                YGMeasureModeOldSizeIsUnspecifiedAndStillFits(
                    heightMode,
                    height - marginColumn,
                    lastHeightMode,
                    lastComputedHeight) ||
                YGMeasureModeNewMeasureSizeIsStricterAndStillValid(
                    heightMode,
                    height - marginColumn,
                    lastHeightMode,
                    lastHeight,
                    lastComputedHeight);

            return widthIsCompatible && heightIsCompatible;
        }

        //
        // This is a wrapper around the YGNodelayoutImpl function. It determines
        // whether the layout request is redundant and can be skipped.
        //
        // Parameters:
        //  Input parameters are the same as YGNodelayoutImpl (see above)
        //  Return parameter is true if layout was performed, false if skipped
        //
        public static bool YGLayoutNodeInternal(
            in YGNodeRef node,
            in float availableWidth,
            in float availableHeight,
            in YGDirection ownerDirection,
            in YGMeasureMode widthMeasureMode,
            in YGMeasureMode heightMeasureMode,
            in float ownerWidth,
            in float ownerHeight,
            in bool performLayout,
            in string reason,
            in YGConfigRef config)
        {
            var layout = node.getLayout();

            gDepth++;

            var needToVisitNode =
                node.isDirty() && layout.generationCount != gCurrentGenerationCount ||
                layout.lastOwnerDirection != ownerDirection;

            if (needToVisitNode)
            {
                // Invalidate the cached results.
                layout.nextCachedMeasurementsIndex = 0;
                layout.cachedLayout.widthMeasureMode = YGMeasureMode.Undefined;
                layout.cachedLayout.heightMeasureMode = YGMeasureMode.Undefined;
                layout.cachedLayout.computedWidth = -1;
                layout.cachedLayout.computedHeight = -1;
            }

            YGCachedMeasurement cachedResults = null;

            // Determine whether the results are already cached. We maintain a separate
            // cache for layouts and measurements. A layout operation modifies the
            // positions
            // and dimensions for nodes in the subtree. The algorithm assumes that each
            // node
            // gets layed out a maximum of one time per tree layout, but multiple
            // measurements
            // may be required to resolve all of the flex dimensions.
            // We handle nodes with measure functions specially here because they are the
            // most
            // expensive to measure, so it's worth avoiding redundant measurements if at
            // all possible.
            if (node.getMeasure() != null)
            {
                var marginAxisRow = YGUnwrapFloatOptional(
                    node.getMarginForAxis(YGFlexDirection.Row, ownerWidth));
                var marginAxisColumn = YGUnwrapFloatOptional(
                    node.getMarginForAxis(YGFlexDirection.Column, ownerWidth));

                // First, try to use the layout cache.
                if (YGNodeCanUseCachedMeasurement(
                    widthMeasureMode,
                    availableWidth,
                    heightMeasureMode,
                    availableHeight,
                    layout.cachedLayout.widthMeasureMode,
                    layout.cachedLayout.availableWidth,
                    layout.cachedLayout.heightMeasureMode,
                    layout.cachedLayout.availableHeight,
                    layout.cachedLayout.computedWidth,
                    layout.cachedLayout.computedHeight,
                    marginAxisRow,
                    marginAxisColumn,
                    config))
                    cachedResults = layout.cachedLayout;
                else
                    for (var i = 0; i < layout.nextCachedMeasurementsIndex; i++)
                        if (YGNodeCanUseCachedMeasurement(
                            widthMeasureMode,
                            availableWidth,
                            heightMeasureMode,
                            availableHeight,
                            layout.cachedMeasurements[i].widthMeasureMode,
                            layout.cachedMeasurements[i].availableWidth,
                            layout.cachedMeasurements[i].heightMeasureMode,
                            layout.cachedMeasurements[i].availableHeight,
                            layout.cachedMeasurements[i].computedWidth,
                            layout.cachedMeasurements[i].computedHeight,
                            marginAxisRow,
                            marginAxisColumn,
                            config))
                        {
                            cachedResults = layout.cachedMeasurements[i];
                            break;
                        }
            }
            else if (performLayout)
            {
                if (YGFloatsEqual(layout.cachedLayout.availableWidth, availableWidth) &&
                    YGFloatsEqual(layout.cachedLayout.availableHeight, availableHeight) &&
                    layout.cachedLayout.widthMeasureMode == widthMeasureMode &&
                    layout.cachedLayout.heightMeasureMode == heightMeasureMode)
                    cachedResults = layout.cachedLayout;
            }
            else
            {
                for (var i = 0; i < layout.nextCachedMeasurementsIndex; i++)
                    if (YGFloatsEqual(
                            layout.cachedMeasurements[i].availableWidth,
                            availableWidth) &&
                        YGFloatsEqual(
                            layout.cachedMeasurements[i].availableHeight,
                            availableHeight)                                              &&
                        layout.cachedMeasurements[i].widthMeasureMode == widthMeasureMode &&
                        layout.cachedMeasurements[i].heightMeasureMode ==
                        heightMeasureMode)
                    {
                        cachedResults = layout.cachedMeasurements[i];
                        break;
                    }
            }

            if (!needToVisitNode && cachedResults != null)
            {
                layout.measuredDimensions[(int)YGDimension.Width] = cachedResults.computedWidth;
                layout.measuredDimensions[(int)YGDimension.Height] =
                    cachedResults.computedHeight;

                if (gPrintChanges && gPrintSkips)
                {
                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        $"{YGSpacer(gDepth)}{gDepth}.[[skipped] ");

                    node.getPrintFunc()?.Invoke(node);

                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        "wm: {YGMeasureModeName(widthMeasureMode,  performLayout)}, hm: {YGMeasureModeName(heightMeasureMode, performLayout)}, aw: {availableWidth} ah: {availableHeight} => d: ({cachedResults.computedWidth}, {cachedResults.computedHeight}) {reason}\n");
                }
            }
            else
            {
                if (gPrintChanges)
                {
                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        $"{YGSpacer(gDepth)}{gDepth}.{(needToVisitNode ? " * " : "")}");

                    node.getPrintFunc()?.Invoke(node);

                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        $"wm: {YGMeasureModeName(widthMeasureMode, performLayout)}, hm: {YGMeasureModeName(heightMeasureMode, performLayout)}, aw: {availableWidth} ah: {availableHeight} {reason}\n");
                }

                YGNodelayoutImpl(
                    node,
                    availableWidth,
                    availableHeight,
                    ownerDirection,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight,
                    performLayout,
                    config);

                if (gPrintChanges)
                {
                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        $"{YGSpacer(gDepth)}{gDepth}.]{(needToVisitNode ? "*" : "")}");

                    node.getPrintFunc()?.Invoke(node);

                    YGLog(
                        node,
                        YGLogLevel.Verbose,
                        "wm: {YGMeasureModeName(widthMeasureMode,  performLayout)}, hm: {YGMeasureModeName(heightMeasureMode, performLayout)}, d: ({layout.measuredDimensions[YGDimension.Width]}, {layout.measuredDimensions[YGDimension.Height]}) {reason}\n");
                }

                layout.lastOwnerDirection = ownerDirection;

                if (cachedResults == null)
                {
                    if (layout.nextCachedMeasurementsIndex == YG_MAX_CACHED_RESULT_COUNT)
                    {
                        if (gPrintChanges) YGLog(node, YGLogLevel.Verbose, "Out of cache entries!\n");

                        layout.nextCachedMeasurementsIndex = 0;
                    }

                    YGCachedMeasurement newCacheEntry;
                    if (performLayout)
                    {
                        // Use the single layout cache entry.
                        newCacheEntry = layout.cachedLayout;
                    }
                    else
                    {
                        // Allocate a new measurement cache entry.
                        newCacheEntry = layout.cachedMeasurements[layout.nextCachedMeasurementsIndex];
                        layout.nextCachedMeasurementsIndex++;
                    }

                    newCacheEntry.availableWidth = availableWidth;
                    newCacheEntry.availableHeight = availableHeight;
                    newCacheEntry.widthMeasureMode = widthMeasureMode;
                    newCacheEntry.heightMeasureMode = heightMeasureMode;
                    newCacheEntry.computedWidth = layout.measuredDimensions[(int)YGDimension.Width];
                    newCacheEntry.computedHeight = layout.measuredDimensions[(int)YGDimension.Height];
                }
            }

            if (performLayout)
            {
                node.setLayoutDimension(
                    node.getLayout().measuredDimensions[(int)YGDimension.Width],
                    YGDimension.Width);
                node.setLayoutDimension(
                    node.getLayout().measuredDimensions[(int)YGDimension.Height],
                    YGDimension.Height);

                node.setHasNewLayout(true);
                node.setDirty(false);
            }

            gDepth--;
            layout.generationCount = gCurrentGenerationCount;
            return needToVisitNode || cachedResults == null;
        }

        public static void YGConfigSetPointScaleFactor(
            in YGConfigRef config,
            in float pixelsInPoint)
        {
            YGAssertWithConfig(
                config,
                pixelsInPoint >= 0.0f,
                "Scale factor should not be less than zero");

            // We store points for Pixel as we will use it for rounding
            if (pixelsInPoint == 0.0f)
                config.pointScaleFactor = 0.0f;
            else
                config.pointScaleFactor = pixelsInPoint;
        }

        internal static void YGRoundToPixelGrid(
            in YGNodeRef node,
            in float pointScaleFactor,
            in float absoluteLeft,
            in float absoluteTop)
        {
            if (pointScaleFactor == 0.0f) return;

            var nodeLeft = node.getLayout().position[(int)YGEdge.Left];
            var nodeTop = node.getLayout().position[(int)YGEdge.Top];

            var nodeWidth = node.getLayout().dimensions[(int)YGDimension.Width];
            var nodeHeight = node.getLayout().dimensions[(int)YGDimension.Height];

            var absoluteNodeLeft = absoluteLeft + nodeLeft;
            var absoluteNodeTop = absoluteTop + nodeTop;

            var absoluteNodeRight = absoluteNodeLeft + nodeWidth;
            var absoluteNodeBottom = absoluteNodeTop + nodeHeight;

            // If a node has a custom measure function we never want to round down its
            // size as this could lead to unwanted text truncation.
            var textRounding = node.getNodeType() == YGNodeType.Text;

            node.setLayoutPosition(
                YGRoundValueToPixelGrid(nodeLeft, pointScaleFactor, false, textRounding),
                YGEdge.Left);

            node.setLayoutPosition(
                YGRoundValueToPixelGrid(nodeTop, pointScaleFactor, false, textRounding),
                YGEdge.Top);

            // We multiply dimension by scale factor and if the result is close to the
            // whole number, we don't have any fraction To verify if the result is close
            // to whole number we want to check both floor and ceil numbers
            var hasFractionalWidth =
                !YGFloatsEqual(nodeWidth * pointScaleFactor % 1.0f, 0) &&
                !YGFloatsEqual(nodeWidth * pointScaleFactor % 1.0f, 1.0f);
            var hasFractionalHeight =
                !YGFloatsEqual(nodeHeight * pointScaleFactor % 1.0f, 0f) &&
                !YGFloatsEqual(nodeHeight * pointScaleFactor % 1.0f, 1f);

            node.setLayoutDimension(
                YGRoundValueToPixelGrid(
                    absoluteNodeRight,
                    pointScaleFactor,
                    textRounding && hasFractionalWidth,
                    textRounding && !hasFractionalWidth) -
                YGRoundValueToPixelGrid(
                    absoluteNodeLeft,
                    pointScaleFactor,
                    false,
                    textRounding),
                YGDimension.Width);

            node.setLayoutDimension(
                YGRoundValueToPixelGrid(
                    absoluteNodeBottom,
                    pointScaleFactor,
                    textRounding && hasFractionalHeight,
                    textRounding && !hasFractionalHeight) -
                YGRoundValueToPixelGrid(
                    absoluteNodeTop,
                    pointScaleFactor,
                    false,
                    textRounding),
                YGDimension.Height);

            var childCount = YGNodeGetChildCount(node);
            for (var i = 0; i < childCount; i++)
                YGRoundToPixelGrid(
                    YGNodeGetChild(node, i),
                    pointScaleFactor,
                    absoluteNodeLeft,
                    absoluteNodeTop);
        }

        public static void YGNodeCalculateLayout(
            YGNodeRef node,
            float ownerWidth,
            float ownerHeight,
            YGDirection ownerDirection)
        {
            // Increment the generation count. This will force the recursive routine to
            // visit
            // all dirty nodes at least once. Subsequent visits will be skipped if the
            // input
            // parameters don't change.
            gCurrentGenerationCount++;
            node.resolveDimension();
            var width = YGUndefined;
            var widthMeasureMode = YGMeasureMode.Undefined;
            if (YGNodeIsStyleDimDefined(node, YGFlexDirection.Row, ownerWidth))
            {
                width = YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.getResolvedDimension(dim[(int)YGFlexDirection.Row]),
                        ownerWidth) +
                    node.getMarginForAxis(YGFlexDirection.Row, ownerWidth));
                widthMeasureMode = YGMeasureMode.Exactly;
            }
            else if (!YGResolveValue(
                    node.getStyle().maxDimensions[YGDimension.Width],
                    ownerWidth)
                .isUndefined())
            {
                width = YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.getStyle().maxDimensions[YGDimension.Width],
                        ownerWidth));
                widthMeasureMode = YGMeasureMode.AtMost;
            }
            else
            {
                width = ownerWidth;
                widthMeasureMode = YGFloatIsUndefined(width)
                    ? YGMeasureMode.Undefined
                    : YGMeasureMode.Exactly;
            }

            var height = YGUndefined;
            var heightMeasureMode = YGMeasureMode.Undefined;
            if (YGNodeIsStyleDimDefined(node, YGFlexDirection.Column, ownerHeight))
            {
                height = YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.getResolvedDimension(dim[(int)YGFlexDirection.Column]),
                        ownerHeight) +
                    node.getMarginForAxis(YGFlexDirection.Column, ownerWidth));
                heightMeasureMode = YGMeasureMode.Exactly;
            }
            else if (!YGResolveValue(
                    node.getStyle().maxDimensions[YGDimension.Height],
                    ownerHeight)
                .isUndefined())
            {
                height = YGUnwrapFloatOptional(
                    YGResolveValue(
                        node.getStyle().maxDimensions[YGDimension.Height],
                        ownerHeight));
                heightMeasureMode = YGMeasureMode.AtMost;
            }
            else
            {
                height = ownerHeight;
                heightMeasureMode = YGFloatIsUndefined(height)
                    ? YGMeasureMode.Undefined
                    : YGMeasureMode.Exactly;
            }

            if (YGLayoutNodeInternal(
                node,
                width,
                height,
                ownerDirection,
                widthMeasureMode,
                heightMeasureMode,
                ownerWidth,
                ownerHeight,
                true,
                "initial",
                node.getConfig()))
            {
                node.setPosition(
                    node.getLayout().direction,
                    ownerWidth,
                    ownerHeight,
                    ownerWidth);
                YGRoundToPixelGrid(node, node.getConfig().pointScaleFactor, 0.0f, 0.0f);

                if (node.getConfig().printTree)
                    YGNodePrint(
                        node,
                        YGPrintOptions.Layout | YGPrintOptions.Children | YGPrintOptions.Style);
            }

            // We want to get rid off `useLegacyStretchBehaviour` from YGConfig. But we
            // aren't sure whether client's of yoga have gotten rid off this flag or not.
            // So logging this in YGLayout would help to find out the call sites depending
            // on this flag. This check would be removed once we are sure no one is
            // dependent on this flag anymore. The flag
            // `shouldDiffLayoutWithoutLegacyStretchBehaviour` in YGConfig will help to
            // run experiments.
            if (node.getConfig().shouldDiffLayoutWithoutLegacyStretchBehaviour &&
                node.didUseLegacyFlag())
            {
                var originalNode = YGNodeDeepClone(node);
                originalNode.resolveDimension();
                // Recursively mark nodes as dirty
                originalNode.markDirtyAndPropogateDownwards();
                gCurrentGenerationCount++;
                // Rerun the layout, and calculate the diff
                originalNode.setAndPropogateUseLegacyFlag(false);
                if (YGLayoutNodeInternal(
                    originalNode,
                    width,
                    height,
                    ownerDirection,
                    widthMeasureMode,
                    heightMeasureMode,
                    ownerWidth,
                    ownerHeight,
                    true,
                    "initial",
                    originalNode.getConfig()))
                {
                    originalNode.setPosition(
                        originalNode.getLayout().direction,
                        ownerWidth,
                        ownerHeight,
                        ownerWidth);
                    YGRoundToPixelGrid(
                        originalNode,
                        originalNode.getConfig().pointScaleFactor,
                        0.0f,
                        0.0f);

                    // Set whether the two layouts are different or not.
                    node.setLayoutDoesLegacyFlagAffectsLayout(!originalNode.isLayoutTreeEqualToNode(node));

                    if (originalNode.getConfig().printTree)
                        YGNodePrint(
                            originalNode,
                            YGPrintOptions.Layout | YGPrintOptions.Children | YGPrintOptions.Style);
                }

                YGConfigFreeRecursive(originalNode);
                YGNodeFreeRecursive(originalNode);
            }
        }

        public static void YGConfigSetLogger(in YGConfigRef config, YGLogger logger)
        {
            config.logger = logger ?? YGDefaultLog;
        }

        public static void YGConfigSetShouldDiffLayoutWithoutLegacyStretchBehaviour(
            in YGConfigRef config,
            in bool shouldDiffLayout)
        {
            config.shouldDiffLayoutWithoutLegacyStretchBehaviour = shouldDiffLayout;
        }

        internal static void YGVLog(
            in YGConfigRef config,
            in YGNodeRef node,
            YGLogLevel level,
            string format, params object[] args)
        {
            var logConfig =
                config != null ? config : YGConfigGetDefault();
            logConfig.logger(logConfig, node, level, format, args);

            if (level == YGLogLevel.Fatal) throw new SystemException();
        }

        public static void YGLogWithConfig(in YGConfigRef config, YGLogLevel level, in string message)
        {
            YGVLog(config, null, level, message);
        }

        public static void YGLog(in YGNodeRef node, YGLogLevel level, in string message)
        {
            YGVLog(
                node == null ? null : node.getConfig(),
                node, level, message);
        }

        public static void YGAssert(in bool condition, in string message)
        {
            if (!condition) YGLog(null, YGLogLevel.Fatal, $"{message}\n");
        }

        public static void YGAssertWithNode(
            in YGNodeRef node,
            in bool condition,
            in string message)
        {
            if (!condition) YGLog(node, YGLogLevel.Fatal, $"{message}\n");
        }

        public static void YGAssertWithConfig(
            in YGConfigRef config,
            in bool condition,
            in string message)
        {
            if (!condition) YGLogWithConfig(config, YGLogLevel.Fatal, "{message}\n");
        }

        public static void YGConfigSetExperimentalFeatureEnabled(
            in YGConfigRef config,
            in YGExperimentalFeature feature,
            in bool enabled)
        {
            config.experimentalFeatures[(int)feature] = enabled;
        }

        // inline
        public static bool YGConfigIsExperimentalFeatureEnabled(
            in YGConfigRef config,
            in YGExperimentalFeature feature)
        {
            return config.experimentalFeatures[(int)feature];
        }

        public static void YGConfigSetUseWebDefaults(in YGConfigRef config, in bool enabled)
        {
            config.useWebDefaults = enabled;
        }

        public static void YGConfigSetUseLegacyStretchBehaviour(
            in YGConfigRef config,
            in bool useLegacyStretchBehaviour)
        {
            config.useLegacyStretchBehaviour = useLegacyStretchBehaviour;
        }

        public static bool YGConfigGetUseWebDefaults(in YGConfigRef config)
        {
            return config.useWebDefaults;
        }

        public static void YGConfigSetContext(in YGConfigRef config, object context)
        {
            config.context = context;
        }

        public static object YGConfigGetContext(in YGConfigRef config)
        {
            return config.context;
        }

        public static void YGConfigSetCloneNodeFunc(
            in YGConfigRef config,
            in YGCloneNodeFunc callback)
        {
            config.cloneNodeCallback = callback;
        }

        internal static void YGTraverseChildrenPreOrder(
            in YGVector children,
            in Action<YGNodeRef> f)
        {
            foreach (var node in children)
            {
                f(node);
                YGTraverseChildrenPreOrder(node.getChildren(), f);
            }
        }

        public static void YGTraversePreOrder(
            in YGNodeRef node,
            Action<YGNodeRef> f)
        {
            if (node == null) return;

            f(node);
            YGTraverseChildrenPreOrder(node.getChildren(), f);
        }
    }

    /*
    // YGNode
    WIN_EXPORT YGNodeRef YGNodeNew(void);
    WIN_EXPORT YGNodeRef YGNodeNewWithConfig(const YGConfigRef config);
    WIN_EXPORT YGNodeRef YGNodeClone(const YGNodeRef node);
    WIN_EXPORT void YGNodeFree(const YGNodeRef node);
    WIN_EXPORT void YGNodeFreeRecursive(const YGNodeRef node);
    WIN_EXPORT void YGNodeReset(const YGNodeRef node);
    WIN_EXPORT int YGNodeGetInstanceCount(void);

    WIN_EXPORT void YGNodeInsertChild(
    const YGNodeRef node,
        const YGNodeRef child,
        const UInt32 index);

    // This function inserts the child YGNodeRef as a children of the node received
    // by parameter and set the Owner of the child object to null. This function is
    // expected to be called when using Yoga in persistent mode in order to share a
    // YGNodeRef object as a child of two different Yoga trees. The child YGNodeRef
    // is expected to be referenced from its original owner and from a clone of its
    // original owner.
    WIN_EXPORT void YGNodeInsertSharedChild(
    const YGNodeRef node,
        const YGNodeRef child,
        const UInt32 index);
    WIN_EXPORT void YGNodeRemoveChild(const YGNodeRef node, const YGNodeRef child);
    WIN_EXPORT void YGNodeRemoveAllChildren(const YGNodeRef node);
    WIN_EXPORT YGNodeRef YGNodeGetChild(const YGNodeRef node, const UInt32 index);
    WIN_EXPORT YGNodeRef YGNodeGetOwner(const YGNodeRef node);
    WIN_EXPORT YGNodeRef YGNodeGetParent(const YGNodeRef node);
    WIN_EXPORT UInt32 YGNodeGetChildCount(const YGNodeRef node);
    WIN_EXPORT void YGNodeSetChildren(
        YGNodeRef const owner,
        const YGNodeRef children[],
        const UInt32 count);

    WIN_EXPORT void YGNodeCalculateLayout(
    const YGNodeRef node,
        const float availableWidth,
        const float availableHeight,
        const YGDirection ownerDirection);

    // Mark a node as dirty. Only valid for nodes with a custom measure function
    // set.
    // YG knows when to mark all other nodes as dirty but because nodes with
    // measure functions
    // depends on information not known to YG they must perform this dirty
    // marking manually.
    WIN_EXPORT void YGNodeMarkDirty(const YGNodeRef node);

    // This function marks the current node and all its descendants as dirty. This
    // function is added to test yoga benchmarks. This function is not expected to
    // be used in production as calling `YGCalculateLayout` will cause the
    // recalculation of each and every node.
    WIN_EXPORT void YGNodeMarkDirtyAndPropogateToDescendants(const YGNodeRef node);

    WIN_EXPORT void YGNodePrint(const YGNodeRef node, const YGPrintOptions options);

    WIN_EXPORT bool YGFloatIsUndefined(const float value);

    WIN_EXPORT bool YGNodeCanUseCachedMeasurement(
    const YGMeasureMode widthMode,
        const float width,
        const YGMeasureMode heightMode,
        const float height,
        const YGMeasureMode lastWidthMode,
        const float lastWidth,
        const YGMeasureMode lastHeightMode,
        const float lastHeight,
        const float lastComputedWidth,
        const float lastComputedHeight,
        const float marginRow,
        const float marginColumn,
        const YGConfigRef config);

    WIN_EXPORT void YGNodeCopyStyle(
    const YGNodeRef dstNode,
        const YGNodeRef srcNode);

    void* YGNodeGetContext(YGNodeRef node);
    void YGNodeSetContext(YGNodeRef node, void* context);
    void YGConfigSetPrintTreeFlag(YGConfigRef config, bool enabled);
    YGMeasureFunc YGNodeGetMeasureFunc(YGNodeRef node);
    void YGNodeSetMeasureFunc(YGNodeRef node, YGMeasureFunc measureFunc);
    YGBaselineFunc YGNodeGetBaselineFunc(YGNodeRef node);
    void YGNodeSetBaselineFunc(YGNodeRef node, YGBaselineFunc baselineFunc);
    YGDirtiedFunc YGNodeGetDirtiedFunc(YGNodeRef node);
    void YGNodeSetDirtiedFunc(YGNodeRef node, YGDirtiedFunc dirtiedFunc);
    YGPrintFunc YGNodeGetPrintFunc(YGNodeRef node);
    void YGNodeSetPrintFunc(YGNodeRef node, YGPrintFunc printFunc);
    bool YGNodeGetHasNewLayout(YGNodeRef node);
    void YGNodeSetHasNewLayout(YGNodeRef node, bool hasNewLayout);
    YGNodeType YGNodeGetNodeType(YGNodeRef node);
    void YGNodeSetNodeType(YGNodeRef node, YGNodeType nodeType);
    bool YGNodeIsDirty(YGNodeRef node);
    bool YGNodeLayoutGetDidUseLegacyFlag(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetDirection(
    const YGNodeRef node,
        const YGDirection direction);
    WIN_EXPORT YGDirection YGNodeStyleGetDirection(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetFlexDirection(
    const YGNodeRef node,
        const YGFlexDirection flexDirection);
    WIN_EXPORT YGFlexDirection YGNodeStyleGetFlexDirection(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetJustifyContent(
    const YGNodeRef node,
        const YGJustify justifyContent);
    WIN_EXPORT YGJustify YGNodeStyleGetJustifyContent(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetAlignContent(
    const YGNodeRef node,
        const YGAlign alignContent);
    WIN_EXPORT YGAlign YGNodeStyleGetAlignContent(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetAlignItems(
    const YGNodeRef node,
        const YGAlign alignItems);
    WIN_EXPORT YGAlign YGNodeStyleGetAlignItems(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetAlignSelf(
    const YGNodeRef node,
        const YGAlign alignSelf);
    WIN_EXPORT YGAlign YGNodeStyleGetAlignSelf(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetPositionType(
    const YGNodeRef node,
        const YGPositionType positionType);
    WIN_EXPORT YGPositionType YGNodeStyleGetPositionType(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetFlexWrap(
    const YGNodeRef node,
        const YGWrap flexWrap);
    WIN_EXPORT YGWrap YGNodeStyleGetFlexWrap(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetOverflow(
    const YGNodeRef node,
        const YGOverflow overflow);
    WIN_EXPORT YGOverflow YGNodeStyleGetOverflow(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetDisplay(
    const YGNodeRef node,
        const YGDisplay display);
    WIN_EXPORT YGDisplay YGNodeStyleGetDisplay(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetFlex(const YGNodeRef node, const float flex);
    WIN_EXPORT float YGNodeStyleGetFlex(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetFlexGrow(
    const YGNodeRef node,
        const float flexGrow);
    WIN_EXPORT float YGNodeStyleGetFlexGrow(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetFlexShrink(
    const YGNodeRef node,
        const float flexShrink);
    WIN_EXPORT float YGNodeStyleGetFlexShrink(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetFlexBasis(
    const YGNodeRef node,
        const float flexBasis);
    WIN_EXPORT void YGNodeStyleSetFlexBasisPercent(
    const YGNodeRef node,
        const float flexBasis);
    WIN_EXPORT void YGNodeStyleSetFlexBasisAuto(const YGNodeRef node);
    WIN_EXPORT YGValue YGNodeStyleGetFlexBasis(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetPosition(
    const YGNodeRef node,
        const YGEdge edge,
        const float position);
    WIN_EXPORT void YGNodeStyleSetPositionPercent(
    const YGNodeRef node,
        const YGEdge edge,
        const float position);
    WIN_EXPORT WIN_STRUCT(YGValue)
    YGNodeStyleGetPosition(const YGNodeRef node, const YGEdge edge);

    WIN_EXPORT void YGNodeStyleSetMargin(
    const YGNodeRef node,
        const YGEdge edge,
        const float margin);
    WIN_EXPORT void YGNodeStyleSetMarginPercent(
    const YGNodeRef node,
        const YGEdge edge,
        const float margin);
    WIN_EXPORT void YGNodeStyleSetMarginAuto(
    const YGNodeRef node,
        const YGEdge edge);
    WIN_EXPORT YGValue
YGNodeStyleGetMargin(const YGNodeRef node, const YGEdge edge);

    WIN_EXPORT void YGNodeStyleSetPadding(
    const YGNodeRef node,
        const YGEdge edge,
        const float padding);
    WIN_EXPORT void YGNodeStyleSetPaddingPercent(
    const YGNodeRef node,
        const YGEdge edge,
        const float padding);
    WIN_EXPORT YGValue
YGNodeStyleGetPadding(const YGNodeRef node, const YGEdge edge);

    WIN_EXPORT void YGNodeStyleSetBorder(
    const YGNodeRef node,
        const YGEdge edge,
        const float border);
    WIN_EXPORT float YGNodeStyleGetBorder(const YGNodeRef node, const YGEdge edge);

    WIN_EXPORT void YGNodeStyleSetWidth(const YGNodeRef node, const float width);
    WIN_EXPORT void YGNodeStyleSetWidthPercent(
    const YGNodeRef node,
        const float width);
    WIN_EXPORT void YGNodeStyleSetWidthAuto(const YGNodeRef node);
    WIN_EXPORT YGValue YGNodeStyleGetWidth(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetHeight(const YGNodeRef node, const float height);
    WIN_EXPORT void YGNodeStyleSetHeightPercent(
    const YGNodeRef node,
        const float height);
    WIN_EXPORT void YGNodeStyleSetHeightAuto(const YGNodeRef node);
    WIN_EXPORT YGValue YGNodeStyleGetHeight(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetMinWidth(
    const YGNodeRef node,
        const float minWidth);
    WIN_EXPORT void YGNodeStyleSetMinWidthPercent(
    const YGNodeRef node,
        const float minWidth);
    WIN_EXPORT YGValue YGNodeStyleGetMinWidth(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetMinHeight(
    const YGNodeRef node,
        const float minHeight);
    WIN_EXPORT void YGNodeStyleSetMinHeightPercent(
    const YGNodeRef node,
        const float minHeight);
    WIN_EXPORT YGValue YGNodeStyleGetMinHeight(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetMaxWidth(
    const YGNodeRef node,
        const float maxWidth);
    WIN_EXPORT void YGNodeStyleSetMaxWidthPercent(
    const YGNodeRef node,
        const float maxWidth);
    WIN_EXPORT YGValue YGNodeStyleGetMaxWidth(const YGNodeRef node);

    WIN_EXPORT void YGNodeStyleSetMaxHeight(
    const YGNodeRef node,
        const float maxHeight);
    WIN_EXPORT void YGNodeStyleSetMaxHeightPercent(
    const YGNodeRef node,
        const float maxHeight);
    WIN_EXPORT YGValue YGNodeStyleGetMaxHeight(const YGNodeRef node);

    // Yoga specific properties, not compatible with flexbox specification
    // Aspect ratio control the size of the undefined dimension of a node.
    // Aspect ratio is encoded as a floating point value width/height. e.g. A value
    // of 2 leads to a node with a width twice the size of its height while a value
    // of 0.5 gives the opposite effect.
    //
    // - On a node with a set width/height aspect ratio control the size of the
    // unset dimension
    // - On a node with a set flex basis aspect ratio controls the size of the node
    // in the cross axis if unset
    // - On a node with a measure function aspect ratio works as though the measure
    // function measures the flex basis
    // - On a node with flex grow/shrink aspect ratio controls the size of the node
    // in the cross axis if unset
    // - Aspect ratio takes min/max dimensions into account
    WIN_EXPORT void YGNodeStyleSetAspectRatio(
    const YGNodeRef node,
        const float aspectRatio);
    WIN_EXPORT float YGNodeStyleGetAspectRatio(const YGNodeRef node);

    WIN_EXPORT float YGNodeLayoutGetLeft(const YGNodeRef node);
    WIN_EXPORT float YGNodeLayoutGetTop(const YGNodeRef node);
    WIN_EXPORT float YGNodeLayoutGetRight(const YGNodeRef node);
    WIN_EXPORT float YGNodeLayoutGetBottom(const YGNodeRef node);
    WIN_EXPORT float YGNodeLayoutGetWidth(const YGNodeRef node);
    WIN_EXPORT float YGNodeLayoutGetHeight(const YGNodeRef node);
    WIN_EXPORT YGDirection YGNodeLayoutGetDirection(const YGNodeRef node);
    WIN_EXPORT bool YGNodeLayoutGetHadOverflow(const YGNodeRef node);
    bool YGNodeLayoutGetDidLegacyStretchFlagAffectLayout(const YGNodeRef node);

    // Get the computed values for these nodes after performing layout. If they were
    // set using point values then the returned value will be the same as
    // YGNodeStyleGetXXX. However if they were set using a percentage value then the
    // returned value is the computed value used during layout.
    WIN_EXPORT float YGNodeLayoutGetMargin(const YGNodeRef node, const YGEdge edge);
    WIN_EXPORT float YGNodeLayoutGetBorder(const YGNodeRef node, const YGEdge edge);
    WIN_EXPORT float YGNodeLayoutGetPadding(
    const YGNodeRef node,
        const YGEdge edge);

    WIN_EXPORT void YGConfigSetLogger(const YGConfigRef config, YGLogger logger);
    WIN_EXPORT void
    YGLog(const YGNodeRef node, YGLogLevel level, const char* message, ...);
    WIN_EXPORT void YGLogWithConfig(
    const YGConfigRef config,
        YGLogLevel level,
        const char* format,
        ...);
    WIN_EXPORT void YGAssert(const bool condition, const char* message);
    WIN_EXPORT void YGAssertWithNode(
    const YGNodeRef node,
        const bool condition,
        const char* message);
    WIN_EXPORT void YGAssertWithConfig(
    const YGConfigRef config,
        const bool condition,
        const char* message);
    // Set this to number of pixels in 1 point to round calculation results
    // If you want to avoid rounding - set PointScaleFactor to 0
    WIN_EXPORT void YGConfigSetPointScaleFactor(
    const YGConfigRef config,
        const float pixelsInPoint);
    void YGConfigSetShouldDiffLayoutWithoutLegacyStretchBehaviour(
    const YGConfigRef config,
        const bool shouldDiffLayout);

    // Yoga previously had an error where containers would take the maximum space
    // possible instead of the minimum like they are supposed to. In practice this
    // resulted in implicit behaviour similar to align-self: stretch; Because this
    // was such a long-standing bug we must allow legacy users to switch back to
    // this behaviour.
    WIN_EXPORT void YGConfigSetUseLegacyStretchBehaviour(
    const YGConfigRef config,
        const bool useLegacyStretchBehaviour);

    // YGConfig
    WIN_EXPORT YGConfigRef YGConfigNew(void);
    WIN_EXPORT void YGConfigFree(const YGConfigRef config);
    WIN_EXPORT void YGConfigCopy(const YGConfigRef dest, const YGConfigRef src);
    WIN_EXPORT int YGConfigGetInstanceCount(void);

    WIN_EXPORT void YGConfigSetExperimentalFeatureEnabled(
    const YGConfigRef config,
        const YGExperimentalFeature feature,
        const bool enabled);
    WIN_EXPORT bool YGConfigIsExperimentalFeatureEnabled(
    const YGConfigRef config,
        const YGExperimentalFeature feature);

    // Using the web defaults is the prefered configuration for new projects.
    // Usage of non web defaults should be considered as legacy.
    WIN_EXPORT void YGConfigSetUseWebDefaults(
    const YGConfigRef config,
        const bool enabled);
    WIN_EXPORT bool YGConfigGetUseWebDefaults(const YGConfigRef config);

    WIN_EXPORT void YGConfigSetCloneNodeFunc(
    const YGConfigRef config,
        const YGCloneNodeFunc callback);

    // Export only for C#
    WIN_EXPORT YGConfigRef YGConfigGetDefault(void);

    WIN_EXPORT void YGConfigSetContext(const YGConfigRef config, void* context);
    WIN_EXPORT void* YGConfigGetContext(const YGConfigRef config);

    WIN_EXPORT float YGRoundValueToPixelGrid(
    const float value,
        const float pointScaleFactor,
        const bool forceCeil,
        const bool forceFloor);


// Calls f on each node in the tree including the given node argument.
extern void YGTraversePreOrder(
    YGNodeRef const node,
    std::function<void(YGNodeRef node)>&& f);

extern void YGNodeSetChildren(
    YGNodeRef const owner,
    const std::vector<YGNodeRef>& children);

    */
}
