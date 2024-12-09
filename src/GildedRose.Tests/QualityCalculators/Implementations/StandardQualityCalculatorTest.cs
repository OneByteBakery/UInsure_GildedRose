using GildedRoseKata;
using GildedRose.QualityCalculators.Implementations;
using Xunit;
using System.Diagnostics.CodeAnalysis;

namespace GildedRose.Tests.QualityCalculators.Implementations;

[ExcludeFromCodeCoverage]
public sealed class StandardQualityCalculatorTest : BaseQualityCalculatorTest<StandardQualityCalculator, Item>
{
    [Fact]
    public void GivenAStandardItemWithPositiveQualityAndSellIn_WhenCallingPerformEndOfDayUpdates_ThenQualityIsReducedBy1()
    {
        // Given
        var item = new Item { Name = "foo", SellIn = 5, Quality = 10 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(9, result);
    }

    [Fact]
    public void GivenAStandardItemWithPositiveQualityAndNegativeSellIn_WhenCallingPerformEndOfDayUpdates_ThenQualityIsReducedBy2()
    {
        // Given
        var item = new Item { Name = "foo", SellIn = -1, Quality = 10 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(8, result);
    }

    [Fact]
    public void GivenAStandardItemWithZeroQuality_WhenCallingPerformEndOfDayUpdates_ThenQualityIsNotModified()
    {
        // Given
        var item = new Item { Name = "foo", SellIn = 5, Quality = 0 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(0, result);
    }
}

