using System;
using System.Runtime.CompilerServices;

namespace Xamarin.Yoga
{
    public static class YogaGlobal
    {
        public static void Log(YogaNode node, LogLevel level, string message)
        {
            Log(
                node?.Config,
                node,
                level,
                message);
        }

        public static void YogaAssert(bool condition, string message)
        {
            if (!condition)
                Log((YogaNode) null, LogLevel.Fatal, $"{message}\n");
        }

        public static void YogaAssert(
            YogaConfig config,
            bool       condition,
            string     message)
        {
            if (!condition)
                Log(config, LogLevel.Fatal, $"{message}\n");
        }

        public static void YogaAssert(
            YogaNode node,
            bool   condition,
            string message)
        {
            if (!condition)
                Log(node, LogLevel.Fatal, $"{message}\n");
        }

        private static void Log(YogaConfig config, LogLevel level, string message)
        {
            Log(config, null, level, message);
        }

        private static void Log(
            YogaConfig config,
            YogaNode     node,
            LogLevel   level,
            string     message)
        {
            var logConfig = config ?? YogaConfig.DefaultConfig;
            logConfig.Logger(logConfig, node, level, message);

            if (level == LogLevel.Fatal) throw new SystemException();
        }

        public static FlexDirectionType FlexDirectionCross(FlexDirectionType flexDirection, DirectionType direction)
        {
            return flexDirection.IsColumn()
                ? ResolveFlexDirection(FlexDirectionType.Row, direction)
                : FlexDirectionType.Column;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FlexDirectionType ResolveFlexDirection(FlexDirectionType flexDirection, DirectionType direction)
        {
            if (direction == DirectionType.RTL)
            {
                if (flexDirection == FlexDirectionType.Row) return FlexDirectionType.RowReverse;
                if (flexDirection == FlexDirectionType.RowReverse) return FlexDirectionType.Row;
            }

            return flexDirection;
        }

    }
}
