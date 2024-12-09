using System.Diagnostics.CodeAnalysis;
using GildedRose.Items;
using GildedRose.QualityCalculators.Implementations;
using Xunit;

namespace GildedRose.Tests.QualityCalculators.Implementations;

[ExcludeFromCodeCoverage]
public sealed class AppreciatingQualityCalculatorTest : BaseQualityCalculatorTest<AppreciatingQualityCalculator, AppreciatingItem>
{
    #region Appreciating Item

    [Theory]
    [InlineData("Aged Brie")]
    [InlineData("Aged Camembert")]
    public void GivenAnAppreciatingItemWithPositiveQualityAndSellIn_WhenCallingPerformEndOfDayUpdates_ThenQualityIsIncreasedBy1(string name)
    {
        // Given
        var item = new AppreciatingItem { Name = name, SellIn = 5, Quality = 10 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(11, result);
    }

    [Theory]
    [InlineData("Aged Brie")]
    [InlineData("Aged Camembert")]
    public void GivenAnAppreciatingItemWithPositiveQualityAndNegativeSellIn_WhenCallingPerformEndOfDayUpdates_ThenQualityIsReducedBy2(string name)
    {
        // Given
        var item = new AppreciatingItem { Name = name, SellIn = -1, Quality = 10 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(12, result);
    }

    [Theory]
    [InlineData("Aged Brie")]
    [InlineData("Aged Camembert")]
    public void GivenAnAppreciatingItemWith50Quality_WhenCallingPerformEndOfDayUpdates_ThenQualityIsNotModified(string name)
    {
        // Given
        var item = new AppreciatingItem { Name = name, SellIn = 5, Quality = 50 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(50, result);
    }

    #endregion Appreciating Item

}