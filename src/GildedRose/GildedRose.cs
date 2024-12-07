using System.Collections.Generic;

namespace GildedRoseKata
{
    public sealed class GildedRose
    {
        private const string SulfurasName = "Sulfuras, Hand of Ragnaros";
        private const string BackstagePassName = "Backstage passes to a TAFKAL80ETC concert";
        private const string AgedBrieName = "Aged Brie";

        IList<Item> Items;

        public GildedRose(IList<Item> items)
        {
            Items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (!IsAgedBrie(item) && !IsBackstagePass(item))
                {
                    if (item.Quality > 0)
                    {
                        if (!IsSulfuras(item))
                        {
                            item.Quality -= 1;
                        }
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality += 1;

                        if (IsBackstagePass(item))
                        {
                            if (item.SellIn < 11)
                            {
                                if (item.Quality < 50)
                                {
                                    item.Quality += 1;
                                }
                            }

                            if (item.SellIn < 6)
                            {
                                if (item.Quality < 50)
                                {
                                    item.Quality += 1;
                                }
                            }
                        }
                    }
                }

                if (!IsSulfuras(item))
                {
                    item.SellIn -= 1;
                }

                if (item.SellIn < 0)
                {
                    if (!IsAgedBrie(item))
                    {
                        if (!IsBackstagePass(item))
                        {
                            if (item.Quality > 0)
                            {
                                if (!IsSulfuras(item))
                                {
                                    item.Quality -= 1;
                                }
                            }
                        }
                        else
                        {
                            item.Quality = 0;
                        }
                    }
                    else
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality += 1;
                        }
                    }
                }
            }
        }

        private static bool IsBackstagePass(Item item) => item.Name == BackstagePassName;
        private static bool IsAgedBrie(Item item) => item.Name == AgedBrieName;
        private static bool IsSulfuras(Item item) => item.Name == SulfurasName;
    }
}
