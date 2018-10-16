using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    [TestClass]
    public class YGLoggerTests
    {
        //namespace {
        StringBuilder sb = new StringBuilder();

        private void _unmanagedLogger(YGConfigRef config,
            YGNodeRef                    node,
            YGLogLevel                   level,
            string                       format,
            params object[] args)
        {
            sb.AppendFormat(format, args);
        }
        //}

        [TestMethod]
        public void config_print_tree_enabled()
        {
            
            sb.Clear();

            YGConfigRef config = new YGConfig();
            YGConfigSetPrintTreeFlag(config, true);
            YGConfigSetLogger(config, _unmanagedLogger);
            YGNodeRef root   = YGNodeNewWithConfig(config);
            YGNodeRef child0 = YGNodeNewWithConfig(config);
            YGNodeRef child1 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root, child0, 0);
            YGNodeInsertChild(root, child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);
            YGConfigSetLogger(config, null);
            YGNodeFreeRecursive(root);

            var expected =
                "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" >\n  "+
            "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" "+
            "></div>\n  <div layout=\"width: 0; height: 0; top: 0; left: 0;\" "+
            "style=\"\" ></div>\n</div>";
            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void config_print_tree_disabled()
        {
            sb.Clear();
            
            YGConfigRef config = new YGConfig();
            YGConfigSetPrintTreeFlag(config, false);
            YGConfigSetLogger(config, _unmanagedLogger);
            YGNodeRef root   = YGNodeNewWithConfig(config);
            YGNodeRef child0 = YGNodeNewWithConfig(config);
            YGNodeRef child1 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root, child0, 0);
            YGNodeInsertChild(root, child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);
            YGConfigSetLogger(config, null);
            YGNodeFreeRecursive(root);

            var expected = "";
            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void logger_default_node_should_print_no_style_info()
        {
            sb.Clear();

            YGConfigRef config = new YGConfig();
            YGConfigSetLogger(config, _unmanagedLogger);
            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);
            root.Print(YGPrintOptions.All);
            YGConfigSetLogger(config, null);
            YGNodeFree(root);

            var expected = "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" ></div>";
            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void logger_node_with_percentage_absolute_position_and_margin()
        {
            sb.Clear();

            YGConfigRef config = new YGConfig();
            YGConfigSetLogger(config, _unmanagedLogger);
            YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetPositionType(root, YGPositionType.Absolute);
            YGNodeStyleSetWidthPercent(root, 50);
            YGNodeStyleSetHeightPercent(root, 75);
            YGNodeStyleSetFlex(root, 1);
            YGNodeStyleSetMargin(root, YGEdge.Right, 10);
            YGNodeStyleSetMarginAuto(root, YGEdge.Left);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);
            root.Print(YGPrintOptions.All);
            YGConfigSetLogger(config, null);
            YGNodeFree(root);

            var expected = "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"flex: 1; "+
            "margin-left: auto; margin-right: 10px; width: 50%; height: 75%; "+
            "position: absolute; \" ></div>";
            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void logger_node_with_children_should_print_indented()
        {
            sb.Clear();

            YGConfigRef config = new YGConfig();
            YGConfigSetLogger(config, _unmanagedLogger);
            YGNodeRef root   = YGNodeNewWithConfig(config);
            YGNodeRef child0 = YGNodeNewWithConfig(config);
            YGNodeRef child1 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root, child0, 0);
            YGNodeInsertChild(root, child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);
            root.Print(YGPrintOptions.All);
            YGConfigSetLogger(config, null);
            YGNodeFreeRecursive(root);

            var expected = "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" >\n  "+
            "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" "+
            "></div>\n  <div layout=\"width: 0; height: 0; top: 0; left: 0;\" "+
            "style=\"\" ></div>\n</div>";
            Assert.AreEqual(expected, sb.ToString());
        }
    }
}
