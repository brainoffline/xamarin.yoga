// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Xamarin.Yoga
{
    using static YGGlobal;

    public class YGConfig : IEquatable<YGConfig>
    {
        public static readonly YGConfig DefaultConfig = new YGConfig(YGDefaultLog);

        public YGExperimentalFeatures ExperimentalFeatures { get; set; }
        public bool                   UseWebDefaults { get; set; }

        public float                  pointScaleFactor                              = 1.0f;
        public YGLogger               logger                                        = null;
        public object                 context                                       = null;
        public bool                   printTree                                     = false;

        public YGConfig(YGLogger logger = null)
        {
            this.logger = logger ?? YGDefaultLog;
        }

        public YGConfig(YGConfig config)
        {
            ExperimentalFeatures                          = config.ExperimentalFeatures;
            UseWebDefaults                                = config.UseWebDefaults;
            pointScaleFactor                              = config.pointScaleFactor;
            logger                                        = config.logger;
            context                                       = config.context;
            printTree                                     = config.printTree;
        }

        public bool Equals(YGConfig other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            var result =
                ExperimentalFeatures                          == other.ExperimentalFeatures                          &&
                UseWebDefaults                                == other.UseWebDefaults                                &&
                YGFloatsEqual(pointScaleFactor, other.pointScaleFactor)                                              &&
                EqualityComparer<object>.Default.Equals(context, other.context)                                      &&
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
            return Equals(config1, config2);
        }

        public static bool operator !=(YGConfig config1, YGConfig config2)
        {
            return !(config1 == config2);
        }
    }
}
