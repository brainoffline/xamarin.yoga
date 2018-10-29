using System;
using System.Drawing;

namespace Xamarin.Yoga
{
    using static YogaGlobal;

    public delegate SizeF MeasureFunc(
        YogaNode      node,
        float       width,
        MeasureMode widthMode,
        float       height,
        MeasureMode heightMode);

    public delegate float BaselineFunc(YogaNode node, float width, float height);

    public delegate void DirtiedFunc(YogaNode node);

    public delegate void PrintFunc(YogaNode node);

    public delegate void LoggerFunc(
        YogaConfig config,
        YogaNode     node,
        LogLevel   level,
        string     message);

    public class YogaConfig : IEquatable<YogaConfig>
    {
        public static readonly YogaConfig DefaultConfig = new YogaConfig(LogToConsole)
        {
#if DEBUG
            PrintTree = true
#endif
        };

        private float _pointScaleFactor = 1.0f;

        public bool PrintTree;

        public YogaConfig(LoggerFunc logger = null)
        {
            Logger = logger ?? LogToConsole;
        }

        public YogaConfig(YogaConfig config)
        {
            ExperimentalFeatures = config.ExperimentalFeatures;
            UseWebDefaults       = config.UseWebDefaults;
            PointScaleFactor     = config.PointScaleFactor;
            Logger               = config.Logger;
            PrintTree            = config.PrintTree;
        }

        public ExperimentalFeatures ExperimentalFeatures { get; set; }
        public LoggerFunc           Logger               { get; set; }

        public float PointScaleFactor
        {
            get => _pointScaleFactor;
            set
            {
                YogaAssert(
                    value >= 0.0f,
                    "Scale factor should not be less than zero");

                // We store points for Pixel as we will use it for rounding
                _pointScaleFactor = Math.Abs(value) < float.Epsilon ? 0.0f : value;
            }
        }

        public bool UseWebDefaults { get; set; }

        public bool Equals(YogaConfig other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            var result =
                ExperimentalFeatures == other.ExperimentalFeatures                    &&
                UseWebDefaults       == other.UseWebDefaults                          &&
                NumberExtensions.FloatEqual(PointScaleFactor, other.PointScaleFactor) &&
                PrintTree == other.PrintTree;
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

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _pointScaleFactor.GetHashCode();
                hashCode = (hashCode * 397) ^ PrintTree.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) ExperimentalFeatures;
                hashCode = (hashCode * 397) ^ (Logger != null ? Logger.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ UseWebDefaults.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(YogaConfig config1, YogaConfig config2)
        {
            return Equals(config1, config2);
        }

        public static bool operator !=(YogaConfig config1, YogaConfig config2)
        {
            return !(config1 == config2);
        }

        private static void LogToConsole(
            YogaConfig config,
            YogaNode     node,
            LogLevel   level,
            string     message)
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
