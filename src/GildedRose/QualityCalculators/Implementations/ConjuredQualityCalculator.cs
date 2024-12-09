using GildedRose.Items;

namespace GildedRose.QualityCalculators.Implementations;

public sealed class ConjuredQualityCalculator : BaseQualityCalculator<ConjuredItem>, IQualityCalculator<ConjuredItem>
{
    public int CalculateItemQuality(ConjuredItem item) =>
        item switch
        {
            { SellIn: >= 0 } => AddQuality(item, -2),
            _ => AddQuality(item, -4),
        };
}