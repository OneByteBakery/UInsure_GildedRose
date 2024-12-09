using GildedRoseKata;

namespace GildedRose.QualityCalculators
{
    public interface IQualityCalculator<in TItem> where TItem : Item
    {
        int CalculateItemQuality(TItem item);
    }
}
