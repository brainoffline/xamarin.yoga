using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGLoggerTests
    {
        StringBuilder sb = new StringBuilder();
        private void _unmanagedLogger(YGConfig config,
            YGNode                    node,
            YGLogLevel                   level,
            string                       format,
            params object[] args)
        {
            sb.AppendFormat(format, args);
        }

        [TestMethod]
        public void config_print_tree_enabled()
        {
            sb.Clear();

            var config = new YGConfig
            {
                printTree = true,
                Logger = _unmanagedLogger
            };

            YGNode root   = new YGNode(config);
            YGNode child0 = new YGNode(config);
            YGNode child1 = new YGNode(config);
            root.InsertChild(child0);
            root.InsertChild(1, child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            config.Logger = null;
            

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

            YGConfig config = new YGConfig
            {
                printTree = false,
                Logger = _unmanagedLogger
            };

            YGNode root   = new YGNode(config);
            YGNode child0 = new YGNode(config);
            YGNode child1 = new YGNode(config);
            root.InsertChild(child0);
            root.InsertChild(1, child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            config.Logger = null;
            

            var expected = "";
            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void logger_default_node_should_print_no_style_info()
        {
            sb.Clear();

            YGConfig config = new YGConfig
            {
                Logger = _unmanagedLogger
            };

            YGNode root = new YGNode(config);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            root.Print(YGPrintOptions.All);
            config.Logger = null;

            var expected = "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" ></div>";
            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void logger_node_with_percentage_absolute_position_and_margin()
        {
            sb.Clear();

            YGNode root = new YGNode(new YGConfig {Logger = _unmanagedLogger});

            root.Style.PositionType = YGPositionType.Absolute;
            root.Style.Width = 50.Percent();
            root.Style.Height = 75.Percent();
            root.Style.Flex = 1;
            root.Style.Margin.Right = 10;
            root.Style.Margin.Left = YGValue.Auto;
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            root.Print(YGPrintOptions.All);

            var expected = "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"flex: 1; "+
                "margin-left: auto; margin-right: 10px; width: 50%; height: 75%; "+
                "position: absolute; \" ></div>";
            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void logger_node_with_children_should_print_indented()
        {
            sb.Clear();

            YGConfig config = new YGConfig {Logger = _unmanagedLogger};

            YGNode root   = new YGNode(config);
            YGNode child0 = new YGNode(config);
            YGNode child1 = new YGNode(config);
            root.InsertChild(child0);
            root.InsertChild(1, child1);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);
            root.Print(YGPrintOptions.All);
            config.Logger = null;
            

            var expected = "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" >\n  "+
            "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" "+
            "></div>\n  <div layout=\"width: 0; height: 0; top: 0; left: 0;\" "+
            "style=\"\" ></div>\n</div>";
            Assert.AreEqual(expected, sb.ToString());
        }
    }
}
