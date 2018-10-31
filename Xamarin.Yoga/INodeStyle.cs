namespace Xamarin.Yoga {
    public interface INodeStyle
    {
        AlignType         AlignContent   { get; set; }
        AlignType         AlignItems     { get; set; }
        AlignType         AlignSelf      { get; set; }
        float?            AspectRatio    { get; set; }
        Edges             Border         { get; set; }
        DirectionType     Direction      { get; set; }
        DisplayType       Display        { get; set; }
        float?            Flex           { get; set; }
        Value             FlexBasis      { get; set; }
        FlexDirectionType FlexDirection  { get; set; }
        float?            FlexGrow       { get; set; }
        float?            FlexShrink     { get; set; }
        WrapType          FlexWrap       { get; set; }
        Value             Height         { get; set; }
        JustifyType       JustifyContent { get; set; }
        Edges             Margin         { get; set; }
        Value             MaxHeight      { get; set; }
        Value             MaxWidth       { get; set; }
        Value             MinHeight      { get; set; }
        Value             MinWidth       { get; set; }
        OverflowType      Overflow       { get; set; }
        Edges             Padding        { get; set; }
        Edges             Position       { get; set; }
        PositionType      PositionType   { get; set; }
        Value             Width          { get; set; }

        Value Dimension(DimensionType    dim);
        Value MaxDimension(DimensionType dim);
        Value MinDimension(DimensionType dim);

        /// <summary>Return raw flex shrink value.  May be null</summary>
        float? GetFlexShrink();
    }
}