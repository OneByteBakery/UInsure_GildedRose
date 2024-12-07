using System.Collections.Generic;

namespace GildedRoseKata
{
    internal sealed class GildedRose
    {
        private const string SulfurasName = "Sulfuras, Hand of Ragnaros";
        private const string BackstagePassName = "Backstage passes to a TAFKAL80ETC concert";
        private const string AgedBrieName = "Aged Brie";

        IList<Item> Items;

        public GildedRose(IList<Item> items)
        {
            Items = items;
        }

        public void PerformEndOfDayUpdates()
        {
            foreach (var item in Items)
            {
                AdvanceSellIn(item);
                UpdateQuality(item);
            }
        }

        private static void UpdateQuality(Item item)
        {
            if (IsAgedBrie(item) || IsBackstagePass(item))
            {
                if (item.Quality < 50)
                {
                    item.Quality += 1;

                    if (IsBackstagePass(item))
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
                    if (!IsSulfuras(item))
                    {
                        item.Quality -= 1;
                    }
                }
            }

            if (item.SellIn >= 0) return;

            if (IsAgedBrie(item))
            {
                if (item.Quality < 50)
                {
                    item.Quality += 1;
                }
            }
            else
            {
                if (IsBackstagePass(item))
                {
                    item.Quality = 0;
                }
                else
                {
                    if (item.Quality > 0)
                    {
                        if (!IsSulfuras(item))
                        {
                            item.Quality -= 1;
                        }
                    }
                }
            }
        }

        private static void AdvanceSellIn(Item item)
        {
            if (!IsSulfuras(item))
            {
                item.SellIn -= 1;
            }
        }

        private static bool IsBackstagePass(Item item) => item.Name == BackstagePassName;
        private static bool IsAgedBrie(Item item) => item.Name == AgedBrieName;
        private static bool IsSulfuras(Item item) => item.Name == SulfurasName;
    }
}
