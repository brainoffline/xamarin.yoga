// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel.Design;

namespace Xamarin.Yoga
{
    public class YGConfig
    {
        public bool[]          experimentalFeatures                          = {false};
        public bool            useWebDefaults                                = false;
        public bool            useLegacyStretchBehaviour                     = false;
        public bool            shouldDiffLayoutWithoutLegacyStretchBehaviour = false;
        public float           pointScaleFactor                              = 1.0f;
        public YGLogger        logger                                        = null;
        public YGCloneNodeFunc cloneNodeCallback                             = null;
        public object          context;
        public bool            printTree = true;

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
    }
}
