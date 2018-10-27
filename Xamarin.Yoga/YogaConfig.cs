// ReSharper disable InconsistentNaming

using System;
using System.Drawing;

namespace Xamarin.Yoga
{
    using static YGGlobal;

    public delegate SizeF MeasureFunc(
        YGNode      node,
        float       width,
        MeasureMode widthMode,
        float       height,
        MeasureMode heightMode);

    public delegate float BaselineFunc(YGNode node, float width, float height);

    public delegate void DirtiedFunc(YGNode node);

    public delegate void PrintFunc(YGNode node);

    public delegate void LoggerFunc(
        YogaConfig config,
        YGNode     node,
        LogLevel   level,
        string     message);

    public class YogaConfig : IEquatable<YogaConfig>
    {
        public static readonly YogaConfig DefaultConfig = new YogaConfig(YGDefaultLog)
        {
            printTree = true
        };

        public float pointScaleFactor = 1.0f;
        public bool  printTree;

        public YogaConfig(LoggerFunc logger = null)
        {
            Logger = logger ?? YGDefaultLog;
        }

        public YogaConfig(YogaConfig config)
        {
            ExperimentalFeatures = config.ExperimentalFeatures;
            UseWebDefaults       = config.UseWebDefaults;
            pointScaleFactor     = config.pointScaleFactor;
            Logger               = config.Logger;
            printTree            = config.printTree;
        }

        public ExperimentalFeatures ExperimentalFeatures { get; set; }
        public LoggerFunc           Logger               { get; set; }
        public bool                 UseWebDefaults       { get; set; }

        public bool Equals(YogaConfig other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            var result =
                ExperimentalFeatures == other.ExperimentalFeatures   &&
                UseWebDefaults       == other.UseWebDefaults         &&
                FloatEqual(pointScaleFactor, other.pointScaleFactor) &&
                printTree == other.printTree;
            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is YogaConfig config)
                return Equals(config);
            return false;
        }

        public static bool operator ==(YogaConfig config1, YogaConfig config2)
        {
            return Equals(config1, config2);
        }

        public static bool operator !=(YogaConfig config1, YogaConfig config2)
        {
            return !(config1 == config2);
        }

        private static void YGDefaultLog(
            YogaConfig      config,
            YGNode          node,
            LogLevel        level,
            string          message)
        {
            switch (level)
            {
            case LogLevel.Error:
            case LogLevel.Fatal:
                Console.Error.Write(message);
                return;

            case LogLevel.Warn:
            case LogLevel.Info:
            case LogLevel.Debug:
            case LogLevel.Verbose:
            default:
                Console.Write(message);
                return;
            }
        }
    }
}
