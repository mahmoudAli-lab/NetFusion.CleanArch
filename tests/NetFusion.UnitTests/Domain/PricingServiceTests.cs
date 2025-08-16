using NetFusion.Domain.Services;
using NetFusion.Domain.ValueObjects;
using Xunit;

namespace NetFusion.UnitTests.Domain;

public class PricingServiceTests
{
    [Fact]
    public void ApplyDiscount_Should_Calculate_Correctly()
    {
        var service = new PricingService();
        var total = new Money(100, "USD");

        var discounted = service.ApplyDiscount(total, 10);

        Assert.Equal(90, discounted.Amount);
    }

    [Fact]
    public void ApplyDiscount_Should_Throw_On_Invalid_Percent()
    {
        var service = new PricingService();
        var total = new Money(100, "USD");

        Assert.Throws<ArgumentException>(() => service.ApplyDiscount(total, 150));
    }
}
