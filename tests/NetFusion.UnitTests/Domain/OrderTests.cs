using NetFusion.Domain.Aggregates.OrderAggregate;
using NetFusion.Domain.ValueObjects;
using Xunit;

namespace NetFusion.UnitTests.Domain;

public class OrderTests
{
    [Fact]
    public void CreateOrder_Should_Raise_OrderCreatedEvent()
    {
        var order = new Order(Guid.NewGuid());
        Assert.Single(order.Events);
        Assert.IsType<NetFusion.Domain.Aggregates.OrderAggregate.Events.OrderCreatedEvent>(order.Events.First());
    }

    [Fact]
    public void AddItem_Should_Increase_Total()
    {
        var order = new Order(Guid.NewGuid());
        order.AddItem("Laptop", 2, new Money(1000, "USD"));

        Assert.Equal(2000, order.Total.Amount);
    }

    [Fact]
    public void MarkAsPaid_Should_Raise_OrderPaidEvent()
    {
        var order = new Order(Guid.NewGuid());
        order.AddItem("Laptop", 1, new Money(1200, "USD"));

        order.MarkAsPaid();

        Assert.True(order.IsPaid);
        Assert.Contains(order.Events, e => e.GetType().Name == "OrderPaidEvent");
    }
}
