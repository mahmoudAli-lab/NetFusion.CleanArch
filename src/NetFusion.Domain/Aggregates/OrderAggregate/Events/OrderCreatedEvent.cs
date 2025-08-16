namespace NetFusion.Domain.Aggregates.OrderAggregate.Events
{
    public record OrderCreatedEvent(Guid OrderId, Guid CustomerId);
}
