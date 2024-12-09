using GildedRoseKata;

namespace GildedRose.QualityCalculators.Implementations
{
    public sealed class StandardQualityCalculator : BaseQualityCalculator<Item>, IQualityCalculator<Item>
    {
        public int CalculateItemQuality(Item item) =>
            item switch
            {
                { SellIn: < 0 } => AddQuality(item, -2),
                _ => AddQuality(item, -1),
            };
    }
}
