using GildedRose.Items;

namespace GildedRose.QualityCalculators.Implementations;

public sealed class AppreciatingQualityCalculator : BaseQualityCalculator<AppreciatingItem>, IQualityCalculator<AppreciatingItem>
{
    public int CalculateItemQuality(AppreciatingItem item) =>
        item switch
        {
            { SellIn: >= 0 } => AddQuality(item, +1),
            _ => AddQuality(item, +2),
        };
}