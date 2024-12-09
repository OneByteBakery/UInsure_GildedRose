using GildedRoseKata;

namespace GildedRose.QualityCalculators.Implementations
{
    public abstract class BaseQualityCalculator<TItem> where TItem : Item
    {
        protected static int AddQuality(TItem item, int adjustment)
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
    }
}
