using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GildedRose.Items;
using GildedRoseKata;
using Xunit;

namespace GildedRose.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class GildedRoseTest
    {
        [Fact]
        public void GivenAnItem_WhenCallingPerformEndOfDayUpdates_ThenNameIsNotModified()
        {
            // Given
            const string itemName = "foo";
            var item = new Item { Name = itemName, SellIn = 0, Quality = 0 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(itemName, item.Name);
        }

        #region Standard Items

        [Fact]
        public void GivenAStandardItemWithPositiveQualityAndSellIn_WhenCallingPerformEndOfDayUpdates_ThenQualityIsReducedBy1()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 5, Quality = 10 };
            
            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();
            
            // Then
            Assert.Equal(9, item.Quality);
        }

        [Fact]
        public void GivenAStandardItemWithPositiveQualityAndSellIn_WhenCallingPerformEndOfDayUpdates_ThenSellInIsReducedBy1()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 5, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(4, item.SellIn);
        }

        [Fact]
        public void GivenAStandardItemWithPositiveQualityAndZeroSellIn_WhenCallingPerformEndOfDayUpdates_ThenQualityIsReducedBy2()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 0, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(8, item.Quality);
        }

        [Fact]
        public void GivenAStandardItemWithPositiveQualityAndZeroSellIn_WhenCallingPerformEndOfDayUpdates_ThenSellInIsReducedBy1()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 0, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(-1, item.SellIn);
        }

        [Fact]
        public void GivenAStandardItemWithZeroQuality_WhenCallingPerformEndOfDayUpdates_ThenQualityIsNotModified()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 5, Quality = 0 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(0, item.Quality);
        }

        #endregion Standard Items

        #region Appreciating Item

        [Theory]
        [InlineData("Aged Brie")]
        [InlineData("Aged Camembert")]
        public void GivenAnAppreciatingItemWithPositiveQualityAndSellIn_WhenCallingPerformEndOfDayUpdates_ThenQualityIsIncreasedBy1(string name)
        {
            // Given
            var item = new AppreciatingItem { Name = name, SellIn = 5, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(11, item.Quality);
        }

        [Theory]
        [InlineData("Aged Brie")]
        [InlineData("Aged Camembert")]
        public void GivenAnAppreciatingItemWithPositiveQualityAndSellIn_WhenCallingPerformEndOfDayUpdates_ThenSellInIsReducedBy1(string name)
        {
            // Given
            var item = new AppreciatingItem { Name = name, SellIn = 5, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(4, item.SellIn);
        }

        [Theory]
        [InlineData("Aged Brie")]
        [InlineData("Aged Camembert")]
        public void GivenAnAppreciatingItemWithPositiveQualityAndZeroSellIn_WhenCallingPerformEndOfDayUpdates_ThenQualityIsReducedBy2(string name)
        {
            // Given
            var item = new AppreciatingItem { Name = name, SellIn = 0, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(12, item.Quality);
        }

        [Theory]
        [InlineData("Aged Brie")]
        [InlineData("Aged Camembert")]
        public void GivenAnAppreciatingItemWithPositiveQualityAndZeroSellIn_WhenCallingPerformEndOfDayUpdates_ThenSellInIsReducedBy1(string name)
        {
            // Given
            var item = new AppreciatingItem { Name = name, SellIn = 0, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(-1, item.SellIn);
        }

        [Theory]
        [InlineData("Aged Brie")]
        [InlineData("Aged Camembert")]
        public void GivenAnAppreciatingItemWith50Quality_WhenCallingPerformEndOfDayUpdates_ThenQualityIsNotModified(string name)
        {
            // Given
            var item = new AppreciatingItem { Name = name, SellIn = 5, Quality = 50 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(50, item.Quality);
        }

        #endregion Appreciating Item

        #region Backstage Pass

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GivenABackstagePassWithPositiveQualityAndSellInIsGreaterThan0AndLessThan6_WhenCallingPerformEndOfDayUpdates_ThenQualityIsIncreasedBy3(int sellIn)
        {
            // Given
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(13, item.Quality);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void GivenABackstagePassWithPositiveQualityAndSellInIsGreaterThan5AndLessThan11_WhenCallingPerformEndOfDayUpdates_ThenQualityIsIncreasedBy2(int sellIn)
        {
            // Given
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(12, item.Quality);
        }

        [Fact]
        public void GivenABackstagePassWithPositiveQualityAndSellInIsGreaterThan10_WhenCallingPerformEndOfDayUpdates_ThenQualityIsIncreasedBy1()
        {
            // Given
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(11, item.Quality);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenABackstagePassWithPositiveQualityAndSellInIsNotPositive_WhenCallingPerformEndOfDayUpdates_ThenQualityIsSetTo0(int sellIn)
        {
            // Given
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(0, item.Quality);
        }

        #endregion Backstage Pass

        #region Legendary Item

        [Fact]
        public void GivenALegendaryItemWith80Quality_WhenCallingPerformEndOfDayUpdates_ThenQualityIsNotModified()
        {
            // Given
            var item = new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(80, item.Quality);
        }

        [Fact]
        public void GivenALegendaryItem_WhenCallingPerformEndOfDayUpdates_ThenSellInIsNotModified()
        {
            // Given
            var item = new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(5, item.SellIn);
        }

        [Fact]
        public void GivenALegendaryItemAndNameIsNotSulfuras_WhenCallingPerformEndOfDayUpdates_ThenQualityIsNotModified()
        {
            // Given
            var item = new LegendaryItem { Name = "Andonisus, Reaper of Souls", SellIn = 5, Quality = 80 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(80, item.Quality);
        }

        [Fact]
        public void GivenALegendaryItemAndNameIsNotSulfuras_WhenCallingPerformEndOfDayUpdates_ThenSellInIsNotModified()
        {
            // Given
            var item = new LegendaryItem { Name = "Andonisus, Reaper of Souls", SellIn = 5, Quality = 80 };

            // When
            var classUnderTest = new GildedRoseKata.GildedRose(new List<Item> { item });
            classUnderTest.PerformEndOfDayUpdates();

            // Then
            Assert.Equal(5, item.SellIn);
        }

        #endregion Legendary Item
    }
}
