﻿using GildedRoseKata;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Xunit;

namespace GildedRoseTests
{
    [ExcludeFromCodeCoverage]
    public sealed class GildedRoseTest
    {
        [Fact]
        public void GivenAnItem_WhenCallingUpdateQuality_ThenNameIsNotModified()
        {
            // Given
            const string itemName = "foo";
            var item = new Item { Name = itemName, SellIn = 0, Quality = 0 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(itemName, item.Name);
        }

        #region Standard Items

        [Fact]
        public void GivenAStandardItemWithPositiveQualityAndSellIn_WhenCallingUpdateQuality_ThenQualityIsReducedBy1()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 5, Quality = 10 };
            
            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();
            
            // Then
            Assert.Equal(9, item.Quality);
        }

        [Fact]
        public void GivenAStandardItemWithPositiveQualityAndSellIn_WhenCallingUpdateQuality_ThenSellInIsReducedBy1()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 5, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(4, item.SellIn);
        }

        [Fact]
        public void GivenAStandardItemWithPositiveQualityAndZeroSellIn_WhenCallingUpdateQuality_ThenQualityIsReducedBy2()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 0, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(8, item.Quality);
        }

        [Fact]
        public void GivenAStandardItemWithPositiveQualityAndZeroSellIn_WhenCallingUpdateQuality_ThenSellInIsReducedBy1()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 0, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(-1, item.SellIn);
        }

        [Fact]
        public void GivenAStandardItemWithZeroQuality_WhenCallingUpdateQuality_ThenQualityIsNotModified()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 5, Quality = 0 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(0, item.Quality);
        }

        #endregion Standard Items

        #region Aged Brie

        [Fact]
        public void GivenAgedBrieWithPositiveQualityAndSellIn_WhenCallingUpdateQuality_ThenQualityIsIncreasedBy1()
        {
            // Given
            var item = new Item { Name = "Aged Brie", SellIn = 5, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(11, item.Quality);
        }

        [Fact]
        public void GivenAgedBrieWithPositiveQualityAndSellIn_WhenCallingUpdateQuality_ThenSellInIsReducedBy1()
        {
            // Given
            var item = new Item { Name = "Aged Brie", SellIn = 5, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(4, item.SellIn);
        }

        [Fact]
        public void GivenAgedBrieWithPositiveQualityAndZeroSellIn_WhenCallingUpdateQuality_ThenQualityIsReducedBy2()
        {
            // Given
            var item = new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(12, item.Quality);
        }

        [Fact]
        public void GivenAgedBrieWithPositiveQualityAndZeroSellIn_WhenCallingUpdateQuality_ThenSellInIsReducedBy1()
        {
            // Given
            var item = new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(-1, item.SellIn);
        }

        [Fact]
        public void GivenAgedBrieWith50Quality_WhenCallingUpdateQuality_ThenQualityIsNotModified()
        {
            // Given
            var item = new Item { Name = "Aged Brie", SellIn = 5, Quality = 50 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(50, item.Quality);
        }

        #endregion Aged Brie

        #region Backstage Pass

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GivenABackstagePassWithPositiveQualityAndSellInIsGreaterThan0AndLessThan6_WhenCallingUpdateQuality_ThenQualityIsIncreasedBy3(int sellIn)
        {
            // Given
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(13, item.Quality);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void GivenABackstagePassWithPositiveQualityAndSellInIsGreaterThan5AndLessThan11_WhenCallingUpdateQuality_ThenQualityIsIncreasedBy2(int sellIn)
        {
            // Given
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(12, item.Quality);
        }

        [Fact]
        public void GivenABackstagePassWithPositiveQualityAndSellInIsGreaterThan10_WhenCallingUpdateQuality_ThenQualityIsIncreasedBy1()
        {
            // Given
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(11, item.Quality);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenABackstagePassWithPositiveQualityAndSellInIsNotPositive_WhenCallingUpdateQuality_ThenQualityIsSetTo0(int sellIn)
        {
            // Given
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(0, item.Quality);
        }

        #endregion Backstage Pass

        #region Legendary Item

        [Fact]
        public void GivenALegendaryItemWith80Quality_WhenCallingUpdateQuality_ThenQualityIsNotModified()
        {
            // Given
            var item = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(80, item.Quality);
        }

        [Fact]
        public void GivenALegendaryItem_WhenCallingUpdateQuality_ThenSellInIsNotModified()
        {
            // Given
            var item = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 };

            // When
            var classUnderTest = new GildedRose(new List<Item> { item });
            classUnderTest.UpdateQuality();

            // Then
            Assert.Equal(80, item.Quality);
        }

        #endregion Legendary Item
    }
}