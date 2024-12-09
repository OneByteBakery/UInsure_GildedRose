using GildedRose.Items;
using GildedRose.QualityCalculators.Implementations;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace GildedRose.Tests.QualityCalculators.Implementations;

[ExcludeFromCodeCoverage]
public sealed class VelbenQualityCalculatorTest : BaseQualityCalculatorTest<VelbenQualityCalculator, VelbenItem>
{

    #region Velben Item

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void GivenAVelbenItemWithPositiveQualityAndSellInIsGreaterThan0AndLessThan5_WhenCallingPerformEndOfDayUpdates_ThenQualityIsIncreasedBy3(int sellIn)
    {
        // Given
        var item = new VelbenItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(13, result);
    }

    [Theory]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    public void GivenAVelbenItemWithPositiveQualityAndSellInIsGreaterThan4AndLessThan10_WhenCallingPerformEndOfDayUpdates_ThenQualityIsIncreasedBy2(int sellIn)
    {
        // Given
        var item = new VelbenItem { Name = "Standard passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(12, result);
    }

    [Fact]
    public void GivenAVelbenItemWithPositiveQualityAndSellInIsGreaterThan9_WhenCallingPerformEndOfDayUpdates_ThenQualityIsIncreasedBy1()
    {
        // Given
        var item = new VelbenItem { Name = "VIP passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 10 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(11, result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    public void GivenAVelbenItemWithPositiveQualityAndSellInIsNegative_WhenCallingPerformEndOfDayUpdates_ThenQualityIsSetTo0(int sellIn)
    {
        // Given
        var item = new VelbenItem { Name = "Passes to a TAFKAL80ETC charity gig", SellIn = sellIn, Quality = 10 };

        // When
        var result = ClassUnderTest.CalculateItemQuality(item);

        // Then
        Assert.Equal(0, result);
    }

    #endregion Velben Item
}