namespace NetFusion.Domain.Aggregates.OrderAggregate.Events
{
    public record OrderPaidEvent(Guid OrderId, decimal Amount);
}
