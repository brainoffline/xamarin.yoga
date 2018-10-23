using System;
using System.ComponentModel;

// ReSharper disable InconsistentNaming
#pragma warning disable 1587

namespace Xamarin.Yoga
{
    public enum YGAlign
    {
        [Description("auto")] Auto,

        [Description("flex-start")] FlexStart,

        [Description("center")] Center,

        [Description("flex-end")] FlexEnd,

        [Description("stretch")] Stretch,

        [Description("baseline")] Baseline,

        [Description("space-between")] SpaceBetween,

        [Description("space-around")] SpaceAround
    }

    public enum DimensionType
    {
        [Description("width")] Width,

        [Description("height")] Height
    }

    public enum DirectionType
    {
        [Description("NotSet-1")] NotSet = -1,

        [Description("inherit")] Inherit = 0,

        [Description("ltr")] LTR = 1,

        [Description("rtl")] RTL = 2
    }

    public enum DisplayType
    {
        [Description("flex")] Flex,

        [Description("none")] None
    }

    public enum EdgeType
    {
        [Description("left")] Left,

        [Description("top")] Top,

        [Description("right")] Right,

        [Description("bottom")] Bottom,

        [Description("start")] Start,

        [Description("end")] End,

        [Description("horizontal")] Horizontal,

        [Description("vertical")] Vertical,

        [Description("all")] All
    }

    [Flags]
    public enum ExperimentalFeatures
    {
        [Description("web-flex-basis")] WebFlexBasis = 1
    }

    public enum FlexDirectionType
    {
        [Description("column")] Column,

        [Description("column-reverse")] ColumnReverse,

        [Description("row")] Row,

        [Description("row-reverse")] RowReverse
    }

    public enum JustifyType
    {
        [Description("flex-start")] FlexStart,

        [Description("center")] Center,

        [Description("flex-end")] FlexEnd,

        [Description("space-between")] SpaceBetween,

        [Description("space-around")] SpaceAround,

        [Description("space-evenly")] SpaceEvenly
    }

    public enum LogLevel
    {
        [Description("error")] Error,

        [Description("warn")] Warn,

        [Description("info")] Info,

        [Description("debug")] Debug,

        [Description("verbose")] Verbose,

        [Description("fatal")] Fatal
    }

    public enum MeasureMode
    {
        [Description("NotSet-1")] NotSet = -1,

        [Description("undefined")] Undefined = 0,

        [Description("exactly")] Exactly = 1,

        [Description("at-most")] AtMost = 2
    }

    public enum NodeType
    {
        [Description("default")] Default,

        [Description("text")] Text
    }

    public enum OverflowType
    {
        [Description("visible")] Visible,

        [Description("hidden")] Hidden,

        [Description("scroll")] Scroll
    }

    public enum PositionType
    {
        [Description("relative")] Relative,

        [Description("absolute")] Absolute
    }

    [Flags]
    public enum PrintOptionType
    {
        [Description("layout")] Layout = 1,

        [Description("style")] Style = 2,

        [Description("children")] Children = 4,

        [Description("All")] All = Layout | Style | Children
    }

    public enum ValueUnit
    {
        [Description("undefined")] Undefined,

        [Description("point")] Point,

        [Description("percent")] Percent,

        [Description("auto")] Auto
    }

    public enum WrapType
    {
        [Description("no-wrap")] NoWrap,

        [Description("wrap")] Wrap,

        [Description("wrap-reverse")] WrapReverse
    }
}
