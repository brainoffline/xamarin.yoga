using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga
{
    public static partial class YogaGlobal
    {
        public static void YGAssert(bool condition, string message)
        {
            if (!condition)
                Log((YGNode) null, LogLevel.Fatal, $"{message}\n");
        }

        public static void YGAssert(
            YogaConfig config,
            bool       condition,
            string     message)
        {
            if (!condition)
                Log(config, LogLevel.Fatal, $"{message}\n");
        }

        public static void YGAssert(
            YGNode node,
            bool   condition,
            string message)
        {
            if (!condition)
                Log(node, LogLevel.Fatal, $"{message}\n");
        }

        public static void Log(YGNode node, LogLevel level, string message)
        {
            Log(node?.Config,
                node,
                level,
                message);
        }

        private static void Log(YogaConfig config, LogLevel level, string message)
        {
            Log(config, null, level, message);
        }

        private static void Log(
            YogaConfig config,
            YGNode     node,
            LogLevel   level,
            string     message)
        {
            var logConfig = config ?? YogaConfig.DefaultConfig;
            logConfig.Logger(logConfig, node, level, message);

            if (level == LogLevel.Fatal) throw new SystemException();
        }
    }
}
