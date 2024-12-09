using System.Collections.Generic;
using System.Linq;
using GildedRose.Items;

namespace GildedRoseKata
{
    internal sealed class GildedRose
    {
        IList<Item> Items;

        public GildedRose(IList<Item> items)
        {
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

        private static void UpdateQuality(Item item)
        {
            switch (item)
            {
                case ConjuredItem conjuredItem:
                    UpdateConjuredItemQuality(conjuredItem);
                    break;
                case AppreciatingItem appreciatingItem:
                    UpdateAppreciatingItemQuality(appreciatingItem);
                    break;
                case VelbenItem velbenItem:
                    UpdateVelbenItemQuality(velbenItem);
                    break;
                default:
                    UpdateStandardItemQuality(item);
                    break;
            }
        }

        private static void UpdateConjuredItemQuality(ConjuredItem conjuredItem) =>
            conjuredItem.Quality = conjuredItem switch
            {
                { SellIn: >= 0 } => AddQuality(conjuredItem, -2),
                _ => AddQuality(conjuredItem, -4),
            };

        private static void UpdateAppreciatingItemQuality(AppreciatingItem appreciatingItem) =>
            appreciatingItem.Quality = appreciatingItem switch
            {
                { SellIn: >= 0 } => AddQuality(appreciatingItem, +1),
                _ => AddQuality(appreciatingItem, +2),
            };

        private static void UpdateVelbenItemQuality(VelbenItem velbenItem) =>
            velbenItem.Quality = velbenItem switch
            {
                { SellIn: < 0 } => 0,
                { SellIn: < 5 } => AddQuality(velbenItem, +3),
                { SellIn: < 10 } => AddQuality(velbenItem, +2),
                _ => AddQuality(velbenItem, +1),
            };

        private static void UpdateStandardItemQuality(Item item) =>
            item.Quality = item switch
            {
                { SellIn: < 0 } => AddQuality(item, -2),
                _ => AddQuality(item, -1),
            };

        private static int AddQuality(Item item, int adjustment)
        {
            var result = item.Quality + adjustment;
            if (result < 0)
            {
                result = 0;
            }

            if (result > 50)
            {
                result = 50;
            }

            return result;
        }

        private static void AdvanceSellIn(Item item) => item.SellIn--;
    }
}
