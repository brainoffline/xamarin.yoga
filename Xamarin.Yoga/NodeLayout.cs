using System;

namespace Xamarin.Yoga
{
    using static NumberExtensions;

    public class NodeLayout : IEquatable<NodeLayout>
    {
        // This value was chosen based on empirical data. Even the most complicated
        // layouts should not require more than 16 entries to fit within the cache.
        private const   int                 MaxCachedResultCount = 16;
        public readonly CachedMeasurement[] CachedMeasurements   = new CachedMeasurement[MaxCachedResultCount];

        public NodeLayout()
        {
            Direction                   = DirectionType.Inherit;
            ComputedFlexBasisGeneration = 0;
            ComputedFlexBasis           = null;
            HadOverflow                 = false;
            GenerationCount             = 0;

            LastOwnerDirection          = DirectionType.NotSet;
            NextCachedMeasurementsIndex = 0;

            for (var i = 0; i < MaxCachedResultCount; i++) CachedMeasurements[i] = new CachedMeasurement();
        }

        public NodeLayout(NodeLayout other)
        {
            Width                       = other.Width;
            Height                      = other.Height;
            MeasuredWidth               = other.MeasuredWidth;
            MeasuredHeight              = other.MeasuredHeight;
            Position                    = other.Position.Clone();
            Margin                      = other.Margin.Clone();
            Border                      = other.Border.Clone();
            Padding                     = other.Padding.Clone();
            Direction                   = other.Direction;
            ComputedFlexBasisGeneration = other.ComputedFlexBasisGeneration;
            ComputedFlexBasis           = other.ComputedFlexBasis;
            HadOverflow                 = other.HadOverflow;

            LastOwnerDirection          = other.LastOwnerDirection;
            NextCachedMeasurementsIndex = other.NextCachedMeasurementsIndex;
            CachedLayout                = other.CachedLayout.Clone();

            for (var i = 0; i < MaxCachedResultCount; i++)
                CachedMeasurements[i] = other.CachedMeasurements[i].Clone();
        }

        public LayoutEdges       Border       { get; } = new LayoutEdges();
        public CachedMeasurement CachedLayout { get; } = new CachedMeasurement();

        public bool          CachedMeasurementFull       => NextCachedMeasurementsIndex >= MaxCachedResultCount;
        public float?        ComputedFlexBasis           { get; set; }
        public int           ComputedFlexBasisGeneration { get; set; }
        public DirectionType Direction                   { get; set; }

        // Instead of recomputing the entire layout every single time, we
        // cache some information to break early when nothing changed
        public int           GenerationCount             { get; set; }
        public bool          HadOverflow                 { get; set; }
        public float         Height                      { get; set; } = Single.NaN;
        public DirectionType LastOwnerDirection          { get; set; }
        public LayoutEdges   Margin                      { get; }      = new LayoutEdges();
        public float         MeasuredHeight              { get; set; } = Single.NaN;
        public float         MeasuredWidth               { get; set; } = Single.NaN;
        public int           NextCachedMeasurementsIndex { get; private set; }
        public LayoutEdges   Padding                     { get; }      = new LayoutEdges();
        public Position      Position                    { get; }      = new Position();
        public float         Width                       { get; set; } = Single.NaN;

        /// <inheritdoc />
        public bool Equals(NodeLayout other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var isEqual =
                Position == other.Position                                       &&
                Margin   == other.Margin                                         &&
                Border   == other.Border                                         &&
                Padding  == other.Padding                                        &&
                FloatEqual(Width,  other.Width)                                  &&
                FloatEqual(Height, other.Height)                                 &&
                Direction                   == other.Direction                   &&
                HadOverflow                 == other.HadOverflow                 &&
                LastOwnerDirection          == other.LastOwnerDirection          &&
                NextCachedMeasurementsIndex == other.NextCachedMeasurementsIndex &&
                CachedLayout                == other.CachedLayout                &&
                FloatOptionalEqual(ComputedFlexBasis, other.ComputedFlexBasis);

            for (var i = 0; i < MaxCachedResultCount && isEqual; ++i)
                isEqual = isEqual && CachedMeasurements[i] == other.CachedMeasurements[i];

            if (MeasuredWidth.HasValue() || other.MeasuredWidth.HasValue())
                isEqual = isEqual && FloatEqual(MeasuredWidth, other.MeasuredWidth);
            if (MeasuredHeight.HasValue() || other.MeasuredHeight.HasValue())
                isEqual = isEqual && FloatEqual(MeasuredHeight, other.MeasuredHeight);
            return isEqual;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is NodeLayout layout)
                return Equals(layout);
            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Position != null ? Position.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (int) Width;
                hashCode = (hashCode * 397) ^ (int) Height;
                hashCode = (hashCode * 397) ^ (int) MeasuredWidth;
                hashCode = (hashCode * 397) ^ (int) MeasuredHeight;
                hashCode = (hashCode * 397) ^ (Margin  != null ? Margin.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Border  != null ? Border.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Padding != null ? Padding.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Direction;
                hashCode = (hashCode * 397) ^ ComputedFlexBasisGeneration;
                hashCode = (hashCode * 397) ^ ComputedFlexBasis.GetHashCode();
                hashCode = (hashCode * 397) ^ HadOverflow.GetHashCode();
                hashCode = (hashCode * 397) ^ GenerationCount;
                hashCode = (hashCode * 397) ^ (int) LastOwnerDirection;
                hashCode = (hashCode * 397) ^ NextCachedMeasurementsIndex;
                hashCode = (hashCode * 397) ^ (CachedMeasurements != null ? CachedMeasurements.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CachedLayout       != null ? CachedLayout.GetHashCode() : 0);
                return hashCode;
            }
        }

        public float GetMeasuredDimension(DimensionType dim)
        {
            return dim == DimensionType.Width ? MeasuredWidth : MeasuredHeight;
        }

        public CachedMeasurement GetNextCachedMeasurement()
        {
            var cache = CachedMeasurements[NextCachedMeasurementsIndex];
            if (cache == null)
                cache = CachedMeasurements[NextCachedMeasurementsIndex] = new CachedMeasurement();
            NextCachedMeasurementsIndex++;
            return cache;
        }

        public void InvalidateCache()
        {
            NextCachedMeasurementsIndex    = 0;
            CachedLayout.WidthMeasureMode  = MeasureMode.NotSet;
            CachedLayout.HeightMeasureMode = MeasureMode.NotSet;
            CachedLayout.ComputedWidth     = -1;
            CachedLayout.ComputedHeight    = -1;
        }

        public static bool operator ==(NodeLayout left, NodeLayout right)
        {
            if ((object) left == null || (object) right == null)
                return ReferenceEquals(left, right);
            return Equals(left, right);
        }

        public static bool operator !=(NodeLayout left, NodeLayout right)
        {
            return !Equals(left, right);
        }

        public void ResetNextCachedMeasurement()
        {
            NextCachedMeasurementsIndex = 0;
        }


        public void SetDimension(DimensionType dim, float value)
        {
            if (dim == DimensionType.Width)
                Width = value;
            else
                Height = value;
        }

        public void SetMeasuredDimension(DimensionType dim, float value)
        {
            if (dim == DimensionType.Width)
                MeasuredWidth = value;
            else
                MeasuredHeight = value;
        }

        public float YGNodeLayoutGetBorder(EdgeType edge)
        {
            YogaGlobal.YogaAssert(
                edge <= EdgeType.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case EdgeType.Left when Direction == DirectionType.RTL:
                return Border.End;
            case EdgeType.Left:
                return Border.Start;
            case EdgeType.Right when Direction == DirectionType.RTL:
                return Border.Start;
            case EdgeType.Right:
                return Border.End;
            }

            return Border[edge];
        }

        public float YGNodeLayoutGetPadding(EdgeType edge)
        {
            YogaGlobal.YogaAssert(
                edge <= EdgeType.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case EdgeType.Left when Direction == DirectionType.RTL:
                return Padding.End;
            case EdgeType.Left:
                return Padding.Start;
            case EdgeType.Right when Direction == DirectionType.RTL:
                return Padding.Start;
            case EdgeType.Right:
                return Padding.End;
            }

            return Padding[edge];
        }

        public float GetMargin(EdgeType edge)
        {
            YogaGlobal.YogaAssert(
                edge <= EdgeType.End,
                "Cannot get layout properties of multi-edge shorthands");

            switch (edge)
            {
            case EdgeType.Left when Direction == DirectionType.RTL:
                return Margin.End;
            case EdgeType.Left:
                return Margin.Start;
            case EdgeType.Right when Direction == DirectionType.RTL:
                return Margin.Start;
            case EdgeType.Right:
                return Margin.End;
            }

            return Margin[edge];
        }

    }
}
