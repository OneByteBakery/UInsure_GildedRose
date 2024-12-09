using GildedRose.QualityCalculators;
using GildedRoseKata;
using System.Diagnostics.CodeAnalysis;

namespace GildedRose.Tests.QualityCalculators.Implementations;

[ExcludeFromCodeCoverage]
public abstract class BaseQualityCalculatorTest<TQualityCalculator, TItem>
    where TQualityCalculator : IQualityCalculator<TItem>, new()
    where TItem : Item
{
    protected TQualityCalculator ClassUnderTest { get; } = new();
}

