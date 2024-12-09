using GildedRose.Items;
using GildedRose.QualityCalculators.Implementations;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace GildedRose.Tests.QualityCalculators.Implementations;

[ExcludeFromCodeCoverage]
public sealed class ConjuredQualityCalculatorTest : BaseQualityCalculatorTest<ConjuredQualityCalculator, ConjuredItem>
{
    [Fact]
    public void GivenAConjuredItemWithPositiveQualityAndSellIn_WhenCallingPerformEndOfDayUpdates_ThenQualityIsReducedBy2()
    {
        // Given
        var item = new ConjuredItem { Name = "foo", SellIn = 5, Quality = 10 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(8, result);
    }

    [Fact]
    public void GivenAConjuredItemWithPositiveQualityAndNegativeSellIn_WhenCallingPerformEndOfDayUpdates_ThenQualityIsReducedBy4()
    {
        // Given
        var item = new ConjuredItem { Name = "foo", SellIn = -1, Quality = 10 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(6, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GivenAConjuredItemWithQualityLessThan2_WhenCallingPerformEndOfDayUpdates_ThenQualityIsModifiedToZero(int quality)
    {
        // Given
        var item = new ConjuredItem { Name = "foo", SellIn = 5, Quality = quality };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(0, result);
    }
}