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
            if (IsAppreciatingItem(item) || IsVelbenItem(item))
            {
                if (item.Quality < 50)
                {
                    item.Quality += 1;

                    if (IsVelbenItem(item))
                    {
                        if (item.SellIn < 10)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality += 1;
                            }
                        }

                        if (item.SellIn < 5)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality += 1;
                            }
                        }
                    }
                }
            }
            else
            {
                if (item.Quality > 0)
                {
                    item.Quality -= 1;
                }
            }

            if (item.SellIn >= 0) return;

            if (IsAppreciatingItem(item))
            {
                if (item.Quality < 50)
                {
                    item.Quality += 1;
                }
            }
            else
            {
                if (IsVelbenItem(item))
                {
                    item.Quality = 0;
                }
                else
                {
                    if (item.Quality > 0)
                    {
                        item.Quality -= 1;
                    }
                }
            }
        }

        private static void AdvanceSellIn(Item item) => item.SellIn -= 1;

        private static bool IsVelbenItem(Item item) => item is VelbenItem;
        private static bool IsAppreciatingItem(Item item) => item is AppreciatingItem;
        private static bool IsLegendary(Item item) => item is LegendaryItem;
    }
}
