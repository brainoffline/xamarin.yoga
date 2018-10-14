// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Xamarin.Yoga
{
    using static YGGlobal;

    public class YGConfig : IEquatable<YGConfig>
    {
        public bool[]          experimentalFeatures                          = {false};
        public bool            useWebDefaults                                = false;
        public bool            useLegacyStretchBehaviour                     = false;
        public bool            shouldDiffLayoutWithoutLegacyStretchBehaviour = false;
        public float           pointScaleFactor                              = 1.0f;
        public YGLogger        logger                                        = null;
        public YGCloneNodeFunc cloneNodeCallback                             = null;
        public object          context                                       = null;
        public bool            printTree                                     = false;

        public YGConfig() { }

        public YGConfig(YGLogger logger)
        {
            this.logger = logger;
        }

        public YGConfig(YGConfig config)
        {
            CloneFrom(config);
        }

        public void CloneFrom(YGConfig config)
        {
            experimentalFeatures                          = config.experimentalFeatures;
            useWebDefaults                                = config.useWebDefaults;
            useLegacyStretchBehaviour                     = config.useLegacyStretchBehaviour;
            shouldDiffLayoutWithoutLegacyStretchBehaviour = config.shouldDiffLayoutWithoutLegacyStretchBehaviour;
            pointScaleFactor                              = config.pointScaleFactor;
            logger                                        = config.logger;
            cloneNodeCallback                             = config.cloneNodeCallback;
            context                                       = config.context;
            printTree                                     = config.printTree;
        }

        public bool Equals(YGConfig other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            var result =
                EqualityComparer<bool[]>.Default.Equals(experimentalFeatures, other.experimentalFeatures);
            result = result &
                useWebDefaults == other.useWebDefaults                                                               &&
                useLegacyStretchBehaviour                     == other.useLegacyStretchBehaviour                     &&
                shouldDiffLayoutWithoutLegacyStretchBehaviour == other.shouldDiffLayoutWithoutLegacyStretchBehaviour &&
                YGFloatsEqual(pointScaleFactor, other.pointScaleFactor);
            result = result &
                EqualityComparer<object>.Default.Equals(context, other.context);
            result = result &
                printTree == other.printTree;
            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is YGConfig config)
                return Equals(config);
            return false;
        }

        public static bool operator ==(YGConfig config1, YGConfig config2)
        {
            return EqualityComparer<YGConfig>.Default.Equals(config1, config2);
        }

        public static bool operator !=(YGConfig config1, YGConfig config2)
        {
            return !(config1 == config2);
        }
    }
}
