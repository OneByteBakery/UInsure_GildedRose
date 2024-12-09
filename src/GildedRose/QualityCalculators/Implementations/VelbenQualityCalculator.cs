using GildedRose.Items;

namespace GildedRose.QualityCalculators.Implementations;

public sealed class VelbenQualityCalculator : BaseQualityCalculator<VelbenItem>, IQualityCalculator<VelbenItem>
{
    public int CalculateItemQuality(VelbenItem item) =>
        item switch
        {
            { SellIn: < 0 } => 0,
            { SellIn: < 5 } => AddQuality(item, +3),
            { SellIn: < 10 } => AddQuality(item, +2),
            _ => AddQuality(item, +1),
        };
}