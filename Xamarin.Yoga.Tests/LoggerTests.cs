using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;


    [TestClass]
    public class LoggerTests
    {
        StringBuilder sb = new StringBuilder();

        private void _unmanagedLogger(
            YogaConfig        config,
            YGNode          node,
            LogLevel      level,
            string          message)
        {
            sb.AppendFormat(message);
        }

        [TestMethod]
        public void config_print_tree_enabled()
        {
            sb.Clear();

            var config = new YogaConfig
            {
                printTree = true,
                Logger    = _unmanagedLogger
            };

            YGNode root = new YGNode(config)
            {
                Children =
                {
                    new YGNode(config),
                    new YGNode(config)
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);
            config.Logger = null;

            var expected =
                "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" >\n  " +
                "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" "      +
                "></div>\n  <div layout=\"width: 0; height: 0; top: 0; left: 0;\" "      +
                "style=\"\" ></div>\n</div>";
            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void config_print_tree_disabled()
        {
            sb.Clear();

            YogaConfig config = new YogaConfig
            {
                printTree = false,
                Logger    = _unmanagedLogger
            };

            YGNode root = new YGNode(config)
            {
                Children =
                {
                    new YGNode(config),
                    new YGNode(config)
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            config.Logger = null;

            Assert.AreEqual(string.Empty, sb.ToString());
        }

        [TestMethod]
        public void logger_default_node_should_print_no_style_info()
        {
            sb.Clear();

            YogaConfig config = new YogaConfig {Logger = _unmanagedLogger};

            YGNode root = new YGNode(config);
            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);
            root.Print(PrintOptionType.All);
            config.Logger = null;

            var expected = "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" ></div>";
            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void logger_node_with_percentage_absolute_position_and_margin()
        {
            sb.Clear();

            YGNode root = new YGNode(new YogaConfig {Logger = _unmanagedLogger})
            {
                Style =
                {
                    PositionType = PositionType.Absolute,
                    Width        = 50.Percent(),
                    Height       = 75.Percent(),
                    Flex         = 1,
                    Margin       = {Right = 10, Left = YGValue.Auto}
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            root.Print(PrintOptionType.All);

            var expected = "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"flex: 1; " +
                "margin-left: auto; margin-right: 10px; width: 50%; height: 75%; "                   +
                "position: absolute; \" ></div>";
            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void logger_node_with_children_should_print_indented()
        {
            sb.Clear();

            YogaConfig config = new YogaConfig {Logger = _unmanagedLogger};
            YGNode root = new YGNode(config)
            {
                Children =
                {
                    new YGNode(config),
                    new YGNode(config)
                }
            };

            YGNodeCalculateLayout(root, float.NaN, float.NaN, DirectionType.LTR);

            root.Print(PrintOptionType.All);
            config.Logger = null;

            var expected = "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" >\n  " +
                "<div layout=\"width: 0; height: 0; top: 0; left: 0;\" style=\"\" "                 +
                "></div>\n  <div layout=\"width: 0; height: 0; top: 0; left: 0;\" "                 +
                "style=\"\" ></div>\n</div>";
            Assert.AreEqual(expected, sb.ToString());
        }
    }
}
