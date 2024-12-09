using System.Collections.Generic;
using System.Linq;
using GildedRose;
using GildedRose.Items;
using GildedRose.QualityCalculators;

namespace GildedRoseKata
{
    internal sealed class GildedRose 
    {
        private readonly IQualityCalculator<Item> _standardQualityCalculator;
        private readonly IQualityCalculator<AppreciatingItem> _appreciatingQualityCalculator;
        private readonly IQualityCalculator<VelbenItem> _velbenQualityCalculator;
        private readonly IQualityCalculator<ConjuredItem> _conjuredQualityCalculator;
        
        IList<Item> Items;

        public GildedRose(IQualityCalculator<Item> standardQualityCalculator,
            IQualityCalculator<AppreciatingItem> appreciatingQualityCalculator,
            IQualityCalculator<VelbenItem> velbenQualityCalculator,
            IQualityCalculator<ConjuredItem> conjuredQualityCalculator,
            IList<Item> items)
        {
            _standardQualityCalculator = standardQualityCalculator;
            _appreciatingQualityCalculator = appreciatingQualityCalculator;
            _velbenQualityCalculator = velbenQualityCalculator;
            _conjuredQualityCalculator = conjuredQualityCalculator;
            Items = items;
        }

        public void PerformEndOfDayUpdates()
        {
            foreach (var item in Items.Where(item => item is not LegendaryItem))
            {
                AdvanceSellIn(item);
                UpdateQuality(item);
            }
        }

        private void UpdateQuality(Item item)
        {
            item.Quality = item switch
            {
                ConjuredItem conjuredItem => _conjuredQualityCalculator.CalculateItemQuality(conjuredItem),
                AppreciatingItem appreciatingItem => _appreciatingQualityCalculator.CalculateItemQuality(appreciatingItem),
                VelbenItem velbenItem => _velbenQualityCalculator.CalculateItemQuality(velbenItem),
                _ => _standardQualityCalculator.CalculateItemQuality(item)
            };
        }
        
        private static void AdvanceSellIn(Item item) => item.SellIn--;
    }
}
