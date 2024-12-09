using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GildedRose.Items;
using GildedRose.QualityCalculators;
using GildedRose.QualityCalculators.Implementations;
using GildedRoseKata;
using Moq;
using Xunit;

namespace GildedRose.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class GildedRoseTest
    {
        private readonly Mock<IQualityCalculator<Item>> _standardQualityCalculator = new();
        private readonly Mock<IQualityCalculator<AppreciatingItem>> _appreciatingQualityCalculator = new();
        private readonly Mock<IQualityCalculator<VelbenItem>> _velbenQualityCalculator = new();
        private readonly Mock<IQualityCalculator<ConjuredItem>> _conjuredQualityCalculator = new();
        
        private GildedRoseKata.GildedRose ClassUnderTest(List<Item> items)
        {
            return new GildedRoseKata.GildedRose(
                _standardQualityCalculator.Object,
                _appreciatingQualityCalculator.Object,
                _velbenQualityCalculator.Object,
                _conjuredQualityCalculator.Object,
                items
            );
        }

        public GildedRoseTest()
        {
            _standardQualityCalculator
                .Setup(x => x.CalculateItemQuality(It.IsAny<Item>()))
                .Returns(1);

            _appreciatingQualityCalculator
                .Setup(x => x.CalculateItemQuality(It.IsAny<AppreciatingItem>()))
                .Returns(2);

            _velbenQualityCalculator
                .Setup(x => x.CalculateItemQuality(It.IsAny<VelbenItem>()))
                .Returns(3);

            _conjuredQualityCalculator
                .Setup(x => x.CalculateItemQuality(It.IsAny<ConjuredItem>()))
                .Returns(4);
        }

        [Fact]
        public void GivenAnItem_WhenCallingPerformEndOfDayUpdates_ThenNameIsNotModified()
        {
            // Given
            const string itemName = "foo";
            var item = new Item { Name = itemName, SellIn = 0, Quality = 0 };

            // When
            ClassUnderTest([item]).PerformEndOfDayUpdates();

            // Then
            Assert.Equal(itemName, item.Name);
        }
        
        [Fact]
        public void GivenAStandardItemWithPositiveQualityAndZeroSellIn_WhenCallingPerformEndOfDayUpdates_ThenSellInIsReducedBy1AndQualityIsUpdated()
        {
            // Given
            var item = new Item { Name = "foo", SellIn = 0, Quality = 10 };

            // When
            ClassUnderTest([item]).PerformEndOfDayUpdates();

            // Then
            Assert.Equal(-1, item.SellIn);
            Assert.Equal(1, item.Quality);
            _standardQualityCalculator.Verify(x => x.CalculateItemQuality(item), Times.Once);
        }

        [Fact]
        public void GivenAnAppreciatingItemWithPositiveQualityAndZeroSellIn_WhenCallingPerformEndOfDayUpdates_ThenSellInIsReducedBy1AndQualityIsUpdated()
        {
            // Given
            var item = new AppreciatingItem { Name = "Aged Brie", SellIn = 0, Quality = 10 };

            // When
            ClassUnderTest([item]).PerformEndOfDayUpdates();

            // Then
            Assert.Equal(-1, item.SellIn);
            Assert.Equal(2, item.Quality);
            _appreciatingQualityCalculator.Verify(x => x.CalculateItemQuality(item), Times.Once);
        }

        [Fact]
        public void GivenAVelbenItemWithPositiveQualityAndSellIn_WhenCallingPerformEndOfDayUpdates_ThenSellInIsReducedBy1AndQualityIsUpdated()
        {
            // Given
            var item = new VelbenItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 10 };

            // When
            ClassUnderTest([item]).PerformEndOfDayUpdates();

            // Then
            Assert.Equal(4, item.SellIn);
            Assert.Equal(3, item.Quality);
            _velbenQualityCalculator.Verify(x => x.CalculateItemQuality(item), Times.Once);
        }

        [Fact]
        public void GivenAConjuredItemWithPositiveQualityAndSellIn_WhenCallingPerformEndOfDayUpdates_ThenSellInIsReducedBy1AndQualityIsUpdated()
        {
            // Given
            var item = new ConjuredItem { Name = "Mana Cake", SellIn = 5, Quality = 10 };

            // When
            ClassUnderTest([item]).PerformEndOfDayUpdates();

            // Then
            Assert.Equal(4, item.SellIn);
            Assert.Equal(4, item.Quality);
            _conjuredQualityCalculator.Verify(x => x.CalculateItemQuality(item), Times.Once);
        }

        #region Legendary Item

        [Fact]
        public void GivenALegendaryItem_WhenCallingPerformEndOfDayUpdates_ThenSellInIsNotModified()
        {
            // Given
            var item = new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 };

            // When
            ClassUnderTest([item]).PerformEndOfDayUpdates();

            // Then
            Assert.Equal(5, item.SellIn);
        }

        [Fact]
        public void GivenALegendaryItemWith80Quality_WhenCallingPerformEndOfDayUpdates_ThenQualityIsNotModified()
        {
            // Given
            var item = new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 };

            // When
            ClassUnderTest([item]).PerformEndOfDayUpdates();

            // Then
            Assert.Equal(80, item.Quality);
            _appreciatingQualityCalculator.VerifyNoOtherCalls();
            _standardQualityCalculator.VerifyNoOtherCalls();
            _velbenQualityCalculator.VerifyNoOtherCalls();
            _conjuredQualityCalculator.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenALegendaryItemAndNameIsNotSulfuras_WhenCallingPerformEndOfDayUpdates_ThenQualityIsNotModified()
        {
            // Given
            var item = new LegendaryItem { Name = "Andonisus, Reaper of Souls", SellIn = 5, Quality = 80 };

            // When
            ClassUnderTest([item]).PerformEndOfDayUpdates();

            // Then
            Assert.Equal(80, item.Quality);
            _appreciatingQualityCalculator.VerifyNoOtherCalls();
            _standardQualityCalculator.VerifyNoOtherCalls();
            _velbenQualityCalculator.VerifyNoOtherCalls();
            _conjuredQualityCalculator.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenALegendaryItemAndNameIsNotSulfuras_WhenCallingPerformEndOfDayUpdates_ThenSellInIsNotModified()
        {
            // Given
            var item = new LegendaryItem { Name = "Andonisus, Reaper of Souls", SellIn = 5, Quality = 80 };

            // When
            ClassUnderTest([item]).PerformEndOfDayUpdates();

            // Then
            Assert.Equal(5, item.SellIn);
        }

        #endregion Legendary Item

    }
}
