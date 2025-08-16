using NetFusion.Domain.ValueObjects;
using Xunit;

namespace NetFusion.UnitTests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Adding_Money_Should_Keep_Same_Currency()
    {
        var m1 = new Money(50, "USD");
        var m2 = new Money(100, "USD");

        var result = m1 + m2;

        Assert.Equal(150, result.Amount);
        Assert.Equal("USD", result.Currency);
    }

    [Fact]
    public void Adding_Different_Currencies_Should_Throw()
    {
        var m1 = new Money(50, "USD");
        var m2 = new Money(100, "EUR");

        Assert.Throws<InvalidOperationException>(() => { var _ = m1 + m2; });
    }
}
