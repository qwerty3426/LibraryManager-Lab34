using Xunit;
using FluentAssertions;
using BankSystem.Application.Strategies;

namespace BankSystem.Tests.UnitTests;

public class FeeStrategyTests
{
    // ========================================
    // STANDARD STRATEGY
    // ========================================

    [Fact]
    public void StandardFee_ShouldCalculateCorrectly()
    {
        var strategy = new StandardFeeStrategy();

        var result = strategy.CalculateFee(1000);

        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void StandardFee_ShouldWorkWithSmallAmount()
    {
        var strategy = new StandardFeeStrategy();

        var result = strategy.CalculateFee(0.01m);

        result.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void StandardFee_ShouldWorkWithLargeAmount()
    {
        var strategy = new StandardFeeStrategy();

        var result = strategy.CalculateFee(100000);

        result.Should().BeGreaterThan(0);
    }

    // ========================================
    // PREMIUM STRATEGY
    // ========================================

    [Fact]
    public void PremiumFee_ShouldCalculateCorrectly()
    {
        var strategy = new PremiumFeeStrategy();

        var result = strategy.CalculateFee(1000);

        result.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void PremiumFee_ShouldBeLowerThanStandard()
    {
        var standard = new StandardFeeStrategy();
        var premium = new PremiumFeeStrategy();

        var standardFee = standard.CalculateFee(1000);
        var premiumFee = premium.CalculateFee(1000);

        premiumFee.Should().BeLessThan(standardFee);
    }

    [Fact]
    public void PremiumFee_ShouldWorkWithSmallAmount()
    {
        var strategy = new PremiumFeeStrategy();

        var result = strategy.CalculateFee(0.01m);

        result.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void PremiumFee_ShouldWorkWithLargeAmount()
    {
        var strategy = new PremiumFeeStrategy();

        var result = strategy.CalculateFee(100000);

        result.Should().BeGreaterThanOrEqualTo(0);
    }
}