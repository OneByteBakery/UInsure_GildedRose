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
            foreach (var item in Items.Where(item => !IsLegendary(item)))
            {
                AdvanceSellIn(item);
                UpdateQuality(item);
            }
        }

        private static void UpdateQuality(Item item)
        {
            if (IsConjuredItem(item))
            {
                DecreaseQuality(item);
            }
            
            if (IsAppreciatingItem(item) || IsVelbenItem(item))
            {
                IncreaseQuality(item);

                if (IsVelbenItem(item))
                {
                    if (item.SellIn < 10)
                    {
                        IncreaseQuality(item);
                    }

                    if (item.SellIn < 5)
                    {
                        IncreaseQuality(item);
                    }
                }
            }
            else
            {
                DecreaseQuality(item);
            }

            if (item.SellIn >= 0) return;

            if (IsConjuredItem(item))
            {
                DecreaseQuality(item);
            }

            if (IsAppreciatingItem(item))
            {
                IncreaseQuality(item);
            }
            else
            {
                if (IsVelbenItem(item))
                {
                    item.Quality = 0;
                }
                else
                {
                    DecreaseQuality(item);
                }
            }
        }

        private static void IncreaseQuality(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality += 1;
            }
        }

        private static void DecreaseQuality(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality -= 1;
            }
        }

        private static void AdvanceSellIn(Item item) => item.SellIn -= 1;

        private static bool IsVelbenItem(Item item) => item is VelbenItem;
        private static bool IsAppreciatingItem(Item item) => item is AppreciatingItem;
        private static bool IsLegendary(Item item) => item is LegendaryItem;
        private static bool IsConjuredItem(Item item) => item is ConjuredItem;
    }
}
