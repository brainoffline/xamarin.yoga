using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

// ReSharper disable InconsistentNaming
#pragma warning disable 1587

namespace Xamarin.Yoga
{
    public static partial class YGGlobal
    {
        public const int YGAlignCount               = 8;
        public const int YGDimensionCount           = 2;
        public const int YGDirectionCount           = 3;
        public const int YGDisplayCount             = 2;
        public const int YGEdgeCount                = 9;
        public const int YGExperimentalFeatureCount = 1;
        public const int YGFlexDirectionCount       = 4;
        public const int YGJustifyCount             = 6;
        public const int YGLogLevelCount            = 6;
        public const int YGMeasureModeCount         = 3;
        public const int YGNodeTypeCount            = 2;
        public const int YGOverflowCount            = 3;
        public const int YGPositionTypeCount        = 2;
        public const int YGPrintOptionsCount        = 3;
        public const int YGUnitCount                = 4;
        public const int YGWrapCount                = 3;
    }


    public enum YGAlign
    {
        [Description("auto")] Auto,

        [Description("flex-start")] FlexStart,

        [Description("center")] Center,

        [Description("flex-end")] FlexEnd,

        [Description("stretch")] Stretch,

        [Description("baseline")] Baseline,

        [Description("space-between")] SpaceBetween,

        [Description("space-around")] SpaceAround,
    }

    /// WIN_EXPORT const char* YGAlignToString(const YGAlign value);
    public enum YGDimension
    {
        [Description("width")] Width,

        [Description("height")] Height,
    }

    /// WIN_EXPORT const char* YGDimensionToString(const YGDimension value);
    public enum YGDirection
    {
        [Description("NotSet-1")] NotSet = -1,

        [Description("inherit")] Inherit = 0,

        [Description("ltr")] LTR = 1,

        [Description("rtl")] RTL = 2,
    }

    /// WIN_EXPORT const char* YGDirectionToString(const YGDirection value);
    public enum YGDisplay
    {
        [Description("flex")] Flex,

        [Description("none")] None,
    }

    /// WIN_EXPORT const char* YGDisplayToString(const YGDisplay value);
    public enum YGEdge
    {
        [Description("left")] Left,

        [Description("top")] Top,

        [Description("right")] Right,

        [Description("bottom")] Bottom,

        [Description("start")] Start,

        [Description("end")] End,

        [Description("horizontal")] Horizontal,

        [Description("vertical")] Vertical,

        [Description("all")] All,
    }

    /// WIN_EXPORT const char* YGEdgeToString(const YGEdge value);
    [Flags]
    public enum YGExperimentalFeatures
    {
        [Description("web-flex-basis")] WebFlexBasis = 1
    }

    /// WIN_EXPORT const char* YGExperimentalFeatureToString(const YGExperimentalFeature value);
    public enum YGFlexDirection
    {
        [Description("column")] Column,

        [Description("column-reverse")] ColumnReverse,

        [Description("row")] Row,

        [Description("row-reverse")] RowReverse,
    }

    /// WIN_EXPORT const char* YGFlexDirectionToString(const YGFlexDirection value);
    public enum YGJustify
    {
        [Description("flex-start")] FlexStart,

        [Description("center")] Center,

        [Description("flex-end")] FlexEnd,

        [Description("space-between")] SpaceBetween,

        [Description("space-around")] SpaceAround,

        [Description("space-evenly")] SpaceEvenly,
    }

    /// WIN_EXPORT const char* YGJustifyToString(const YGJustify value);
    public enum YGLogLevel
    {
        [Description("error")] Error,

        [Description("warn")] Warn,

        [Description("info")] Info,

        [Description("debug")] Debug,

        [Description("verbose")] Verbose,

        [Description("fatal")] Fatal,
    }

    /// WIN_EXPORT const char* YGLogLevelToString(const YGLogLevel value);
    public enum YGMeasureMode
    {
        [Description("NotSet-1")] NotSet = -1,

        [Description("undefined")] Undefined = 0,

        [Description("exactly")] Exactly = 1,

        [Description("at-most")] AtMost = 2
    }

    /// WIN_EXPORT const char* YGMeasureModeToString(const YGMeasureMode value);
    public enum YGNodeType
    {
        [Description("default")] Default,

        [Description("text")] Text,
    }

    /// WIN_EXPORT const char* YGNodeTypeToString(const YGNodeType value);
    public enum YGOverflow
    {
        [Description("visible")] Visible,

        [Description("hidden")] Hidden,

        [Description("scroll")] Scroll,
    }

    /// WIN_EXPORT const char* YGOverflowToString(const YGOverflow value);
    public enum YGPositionType
    {
        [Description("relative")] Relative,

        [Description("absolute")] Absolute,
    }

    /// WIN_EXPORT const char* YGPositionTypeToString(const YGPositionType value);
    [Flags]
    public enum YGPrintOptions
    {
        [Description("layout")] Layout = 1,

        [Description("style")] Style = 2,

        [Description("children")] Children = 4,

        [Description("All")] All = Layout | Style | Children
    }

    /// WIN_EXPORT const char* YGPrintOptionsToString(const YGPrintOptions value);
    public enum YGUnit
    {
        [Description("undefined")] Undefined,

        [Description("point")] Point,

        [Description("percent")] Percent,

        [Description("auto")] Auto,
    }

    /// WIN_EXPORT const char* YGUnitToString(const YGUnit value);
    public enum YGWrap
    {
        [Description("no-wrap")] NoWrap,

        [Description("wrap")] Wrap,

        [Description("wrap-reverse")] WrapReverse,
    }

    /// WIN_EXPORT const char* YGWrapToString(const YGWrap value);
}
